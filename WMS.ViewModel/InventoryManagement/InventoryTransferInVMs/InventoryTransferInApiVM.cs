using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using WMS.Util;
using Elsa;
using Microsoft.EntityFrameworkCore;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.IdentityModel.Tokens;
using WMS.Model;
using WMS.DataAccess;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Components.Web;
using System.Linq.Expressions;
using WMS.Model.InventoryManagement.Return;


namespace WMS.ViewModel.InventoryManagement.InventoryTransferInVMs
{
    public partial class InventoryTransferInApiVM : BaseCRUDVM<InventoryTransferIn>
    {
        #region 系统标准方法
        public InventoryTransferInApiVM()
        {
            SetInclude(x => x.TransInOrganization);
            SetInclude(x => x.TransInWh);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
        #endregion


        /// <summary>
        /// 获取单据列表
        /// </summary>
        /// <param name="type">0-待收货，1-待检验，2-待审核</param>
        /// <returns></returns>
        public List<TransferInReturn> GetList()
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return null;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            if (whid == Guid.Empty)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录2");
                return null;
            }
            List<TransferInReturn> list = new List<TransferInReturn>();
            Expression<Func<InventoryTransferIn, bool>> queryable;
            queryable = x => x.Status == InventoryTransferInStatusEnum.AllReceive && x.TransInWhId == whid;
            list = DC.Set<InventoryTransferIn>()
               .Where(queryable)
               .AsNoTracking()
               .Select(x => new TransferInReturn
               {
                   ID = x.ID,
                   DocNo = x.DocNo,
                   DocType = x.DocType,
                   Status = x.Status.GetEnumDisplayName(),
                   BusinessDate = ((DateTime)x.BusinessDate).ToString("yyyy-MM-dd"),
                   SumQty = (int)(x.InventoryTransferInLine_InventoryTransferIn.Sum(y => y.Qty) ?? 0),
                   CreateTime = x.CreateTime
               }).OrderByDescending(x => x.CreateTime).ToList();
            if (list.Count > 50)
            {
                // 只返回前50条
                list = list.Take(50).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取单据（审核）
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public TransferInReturn GetDocForApprove(string docNo)
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return null;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            var whVm = Wtm.CreateVM<BaseWareHouseVM>();
            BaseWareHouse loginWh = whVm.GetEntityById(whid);
            if (loginWh == null)
            {
                MSD.AddModelError("", "存储地点信息无效，请尝试重新登录");
                return null;
            }

            InventoryTransferIn doc = DC.Set<InventoryTransferIn>()
                .Where(x => x.DocNo == docNo)
                .Include(x => x.TransInOrganization)
                .Include(x => x.TransInWh)
                .Include(x => x.InventoryTransferInLine_InventoryTransferIn)
                .ThenInclude(x => x.ItemMaster)
                .AsNoTracking().FirstOrDefault();
            if (doc != null)    // 单据在本系统中存在
            {
                // 判断行存储地点释放匹配当前登录用户的存储地点
                if (doc.TransInWhId != whid)
                {
                    // 存储地点不匹配，返回错误信息
                    MSD.AddModelError("", $"单据{docNo}的存储地点为{doc.TransInWh.Code}，与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                if (doc.Status != InventoryTransferInStatusEnum.AllReceive)
                {
                    MSD.AddModelError("", $"单据{docNo}的状态为{doc.Status.GetEnumDisplayName()}，无法进行审核");
                    return null;
                }
                TransferInReturn result = new TransferInReturn(doc);
                // 获取单据库存关联关系数据
                var relations = DC.Set<BaseDocInventoryRelation>()
                    .AsNoTracking()
                    .Include(x => x.Inventory)
                    .Where(x => x.BusinessId == doc.ID)
                    .ToList();
                result.SetSubLines(relations);
                GetSuggestLocs(result);
                return result;
            }
            else
            {
                MSD.AddModelError("", "单据不存在");
                return null;
            }
        }

        /// <summary>
        /// 获取推荐库位
        /// </summary>
        /// <param name="doc"></param>
        public void GetSuggestLocs(TransferInReturn doc)
        {
            foreach (var line in doc.Lines)
            {
                line.SuggestLocs = new List<TransferInReturnLineSuggestLoc>();
                List<BaseInventory> invs = this.GetItemInventory(line.ItemID, doc.WareHouseID);
                if (invs.Count > 0)
                {
                    invs = invs.OrderByDescending(x => x.CreateTime).ToList(); // 降序
                    foreach (var inv in invs)
                    {
                        if (!line.SuggestLocs.Exists(x => x.LocCode == inv.WhLocation.Code))
                        {
                            line.SuggestLocs.Add(new TransferInReturnLineSuggestLoc
                            {
                                AreaCode = inv.WhLocation.WhArea.Code,
                                LocCode = inv.WhLocation.Code,
                            });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取调出单（用于生成调入单）
        /// </summary>
        /// <param name="docNo">调出单单号</param>
        /// <returns></returns>
        public GetTransferOutForTransferInResult GetTransferOutForTransferIn(string docNo)
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return null;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            var whVm = Wtm.CreateVM<BaseWareHouseVM>();
            BaseWareHouse loginWh = whVm.GetEntityById(whid);
            if (loginWh == null)
            {
                MSD.AddModelError("", "存储地点信息无效，请尝试重新登录");
                return null;
            }

            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<GetTransferOutForTransferInResult> u9Return = apiHelper.GetTransferOutForTransferIn(docNo);
            if (u9Return.Success)
            {
                string temp = this.GetIdBySourceSystemId<InventoryTransferIn, BaseOrganization>(u9Return.Entity.TransferInOrg)?.ToString();   // 转换组织
                if (string.IsNullOrEmpty(temp))
                {
                    MSD.AddModelError("", $"本系统中调入组织({u9Return.Entity.TransferInOrg})不存在，请尝试同步组织信息");
                    return null;
                }
                u9Return.Entity.TransferInOrg = temp;
                temp = this.GetIdBySourceSystemId<InventoryTransferIn, BaseWareHouse>(u9Return.Entity.TransferInWh)?.ToString();   // 转换存储地点
                if (string.IsNullOrEmpty(temp))
                {
                    MSD.AddModelError("", $"本系统中调入存储地点({u9Return.Entity.TransferInWh})不存在，请尝试同步存储地点信息");
                    return null;
                }
                u9Return.Entity.TransferInWh = temp;
                var transInWh = DC.Set<BaseWareHouse>().Where(x => x.ID == Guid.Parse(u9Return.Entity.TransferInWh)).FirstOrDefault();
                if (transInWh == null)
                {
                    MSD.AddModelError("", $"调入存储地点({u9Return.Entity.TransferInWh})不存在");
                    return null;
                }

                foreach (var line in u9Return.Entity.Lines)
                {
                    temp = this.GetIdBySourceSystemId<InventoryTransferIn, BaseItemMaster>(line.ItemID)?.ToString();   // 转换料品
                    if (string.IsNullOrEmpty(temp))
                    {
                        MSD.AddModelError("", $"本系统中料号({line.ItemCode})不存在，请尝试同步料号信息");
                        return null;
                    }
                    line.ItemID = temp;
                }
                // 判断调入存储地点是否匹配（转换完了才能进行匹配，不然没有值）
                if (u9Return.Entity.TransferInWh != whid.ToString())
                {
                    MSD.AddModelError("", $"单据{docNo}的调入存储地点{transInWh.Code}与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                TransferOutMatch(u9Return.Entity);
                return u9Return.Entity;
            }
            else
            {
                MSD.AddModelError("", u9Return.Msg);
                return null;
            }
        }

        /// <summary>
        /// 匹配来源调出单的扫码数据
        /// </summary>
        /// <param name="result"></param>
        private void TransferOutMatch(GetTransferOutForTransferInResult result)
        {
            if (result == null || string.IsNullOrEmpty(result.DocNo))
            {
                return;
            }
            result.IsG2TransferOut = false;
            // 匹配直接调出单
            InventoryTransferOutDirect transferOutDirect = DC.Set<InventoryTransferOutDirect>()
                .Where(x => x.DocNo == result.DocNo)
                .Include(x => x.InventoryTransferOutDirectLine_InventoryTransferOutDirect)
                .AsNoTracking()
                .FirstOrDefault();
            if (transferOutDirect != null)
            {
                result.IsG2TransferOut = true;
                List<(string, decimal, decimal)> barCodeQtys = new List<(string, decimal, decimal)>();
                List<BaseDocInventoryRelation> relations = DC.Set<BaseDocInventoryRelation>()
                    .AsNoTracking()
                    .Include(x => x.Inventory)
                    .Where(x => x.BusinessId == transferOutDirect.ID)
                    .ToList();
                if (relations != null && relations.Count > 0)
                {
                    foreach(var resultLine in result.Lines)
                    {
                        // 直接调出单不涉及拆行操作（行号与U9一致）
                        var transferOutDirectLine = transferOutDirect.InventoryTransferOutDirectLine_InventoryTransferOutDirect.Where(x => x.DocLineNo == resultLine.DocLineNo).FirstOrDefault();
                        if (transferOutDirectLine == null)
                        {
                            result.IsG2TransferOut = false;
                            return;
                        }
                        var matchRelations = relations.FindAll(x => x.BusinessLineId == transferOutDirectLine.ID);
                        if (matchRelations != null && matchRelations.Count > 0)
                        {
                            resultLine.SubLines = new List<GetTransferOutForTransferInSubLineResult>();
                            foreach (var matchRelation in matchRelations)
                            {
                                BaseBarCode barCode = DC.Set<BaseBarCode>().Where(y => y.Sn == matchRelation.Inventory.SerialNumber).AsNoTracking().FirstOrDefault();
                                if(barCode == null)
                                {
                                    result.IsG2TransferOut = false;
                                    return;
                                }
                                resultLine.SubLines.Add(new GetTransferOutForTransferInSubLineResult
                                {
                                    BatchNumber = matchRelation.Inventory.BatchNumber,
                                    Seiban = matchRelation.Inventory.Seiban,
                                    SeibanRandom = matchRelation.Inventory.SeibanRandom,
                                    BarCode = barCode.Code,
                                    BarcodeOccupyQty = (decimal)matchRelation.Qty,
                                });
                                if (barCodeQtys.Exists(x => x.Item1 == barCode.Code))
                                {
                                    var temp = barCodeQtys.Find(x => x.Item1 == barCode.Code);
                                    temp.Item3 += (decimal)matchRelation.Qty;
                                }
                                else
                                {
                                    barCodeQtys.Add((barCode.Code, (decimal)barCode.Qty, (decimal)matchRelation.Qty));
                                }
                            }
                        }
                    }
                }
                // 本张单据的条码必须使用完毕
                foreach (var barCodeQty in barCodeQtys)
                {
                    if (barCodeQty.Item2 != barCodeQty.Item3)
                    {
                        result.IsG2TransferOut = false;
                        return;
                    }
                }
                return;
            }
            // 匹配手动调出单
            InventoryTransferOutManual transferOutManual = DC.Set<InventoryTransferOutManual>()
                .Where(x => x.DocNo == result.DocNo)
                .Include(x => x.InventoryTransferOutManualLine_InventoryTransferOutManual)
                .AsNoTracking()
                .FirstOrDefault();
            if (transferOutManual != null)
            {
                result.IsG2TransferOut = true;
                List<(string, decimal, decimal)> barCodeQtys = new List<(string, decimal, decimal)>();
                List<BaseDocInventoryRelation> relations = DC.Set<BaseDocInventoryRelation>()
                    .AsNoTracking()
                    .Include(x => x.Inventory)
                    .Where(x => x.BusinessId == transferOutManual.ID)
                    .ToList();
                if (relations != null && relations.Count > 0)
                {
                    foreach (var resultLine in result.Lines)
                    {
                        // 手动调出单有拆行操作（行号左匹配）
                        var transferOutDirectLine = transferOutManual.InventoryTransferOutManualLine_InventoryTransferOutManual
                            .Where(x => resultLine.DocLineNo.ToString().IndexOf(x.DocLineNo.ToString()) == 0) 
                            .FirstOrDefault();
                        if (transferOutDirectLine == null)
                        {
                            result.IsG2TransferOut = false;
                            return;
                        }
                        var matchRelations = relations.FindAll(x => x.BusinessLineId == transferOutDirectLine.ID);
                        if (matchRelations != null && matchRelations.Count > 0)
                        {
                            resultLine.SubLines = new List<GetTransferOutForTransferInSubLineResult>();
                            foreach (var matchRelation in matchRelations)
                            {
                                BaseBarCode barCode = DC.Set<BaseBarCode>().Where(y => y.Sn == matchRelation.Inventory.SerialNumber).AsNoTracking().FirstOrDefault();
                                if (barCode == null)
                                {
                                    result.IsG2TransferOut = false;
                                    return;
                                }
                                resultLine.SubLines.Add(new GetTransferOutForTransferInSubLineResult
                                {
                                    BatchNumber = matchRelation.Inventory.BatchNumber,
                                    Seiban = matchRelation.Inventory.Seiban,
                                    SeibanRandom = matchRelation.Inventory.SeibanRandom,
                                    BarCode = barCode.Code,
                                    BarcodeOccupyQty = (decimal)matchRelation.Qty,
                                });
                                if (barCodeQtys.Exists(x => x.Item1 == barCode.Code))
                                {
                                    var temp = barCodeQtys.Find(x => x.Item1 == barCode.Code);
                                    temp.Item3 += (decimal)matchRelation.Qty;
                                }
                                else
                                {
                                    barCodeQtys.Add((barCode.Code, (decimal)barCode.Qty, (decimal)matchRelation.Qty));
                                }
                            }
                        }
                    }
                }
                // 本张单据的条码必须使用完毕
                foreach (var barCodeQty in barCodeQtys)
                {
                    if (barCodeQty.Item2 != barCodeQty.Item3)
                    {
                        result.IsG2TransferOut = false;
                        return;
                    }
                }
                return;
            }
        }

        /// <summary>
        /// 判断单据是否存在
        /// </summary>
        /// <param name="sourceSystemId">来源系统单据ID</param>
        /// <returns></returns>
        public bool IsDocExist(string sourceSystemId)
        {
            var entity = DC.Set<InventoryTransferIn>().Where(x => x.ErpID == sourceSystemId).AsNoTracking().FirstOrDefault();
            if (entity == null)
            {
                return false;
            }
            else
            {
                if (entity.Status == InventoryTransferInStatusEnum.AllInWh)
                {
                    MSD.AddModelError("", $"G2WMS中已存在此单据，且已入库，无法进行操作");
                    return true;
                }
                else
                {
                    MSD.AddModelError("", $"G2WMS中已存在此单据，请从G2WMS进行操作");
                    return true;
                }
            }
        }

        /// <summary>
        /// 创建调入单
        /// </summary>
        /// <param name="para"></param>
        public void CreateTransferIn(CreateTransferInPara data)
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            // 验证数据有效性
            if (data == null || data.transferInDTOList == null)
            {
                MSD.AddModelError("", "参数不能为空");
                return;
            }
            if (data.transferInDTOList.TransferInLineDTOList == null || data.transferInDTOList.TransferInLineDTOList.Count == 0)
            {
                MSD.AddModelError("", "参数行数据不能为空");
                return;
            }
            data.transferInDTOList.BusinessDate = DateTime.Now.ToString("yyyy-MM-dd");
            data.transferInDTOList.SrcDocType = 1;  //分公司调拨两步式
            if (data.transferInDTOList.OrgId == null || data.transferInDTOList.OrgId == Guid.Empty)
            {
                MSD.AddModelError("", "请选择调入组织");
                return;
            }
            BaseOrganization org = DC.Set<BaseOrganization>().Where(x => x.ID == data.transferInDTOList.OrgId).AsNoTracking().FirstOrDefault();
            if (org == null)
            {
                MSD.AddModelError("", "调入组织不存在");
                return;
            }
            if (!org.IsValid || org.IsEffective == EffectiveEnum.Ineffective)
            {
                MSD.AddModelError("", "调入组织已失效");
                return;
            }
            data.transferInDTOList.OrgCode = org.Code;
            BaseWareHouse wareHouse = DC.Set<BaseWareHouse>().Where(x => x.ID == data.transferInDTOList.TransferInLineDTOList[0].TransInWhId).AsNoTracking().FirstOrDefault();
            if (wareHouse == null)
            {
                MSD.AddModelError("", "调入存储地点不存在");
                return;
            }
            if (!wareHouse.IsValid || wareHouse.IsEffective == EffectiveEnum.Ineffective)
            {
                MSD.AddModelError("", "调入存储地点已失效");
                return;
            }
            // 调入存储地点与当前登录用户的存储地点不匹配
            if (wareHouse.ID != whid)
            {
                MSD.AddModelError("", "单据的调入存储地点与当前登录用户的存储地点不匹配");
                return;
            }
            foreach (var line in data.transferInDTOList.TransferInLineDTOList)
            {
                if (line.StoreUOMQty <= 0)
                {
                    MSD.AddModelError("", $"行上的数量必须大于0");
                    return;
                }
                if (line.TransInWhId != wareHouse.ID)
                {
                    MSD.AddModelError("", $"行上的调入存储地点不一致"); // 前端存储地点字段属于头，不会出现
                    return;
                }
                if (string.IsNullOrEmpty(line.SrcDocNo))
                {
                    MSD.AddModelError("", $"行上的来源单号不能为空");
                    return;
                }
                if (line.SrcDocLineNo == null)
                {
                    MSD.AddModelError("", $"行上的来源行号不能为空");
                    return;
                }
                if (line.SubLines == null || line.SubLines.Count == 0)
                {
                    MSD.AddModelError("", $"行上的子行数据不能为空");
                    return;
                }
            }
            using (var trans = DC.BeginTransaction())
            {
                // 获取当前存储地点的待收库位
                BaseWhLocation location = DC.Set<BaseWhLocation>()
                    .Where(x => x.WhArea.WareHouseId == whid && x.AreaType == Model.WhLocationEnum.WaitForReceive && x.IsEffective == EffectiveEnum.Effective)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (location == null)
                {
                    MSD.AddModelError("", "当前存储地点未配置待收库位，无法进行收货操作");
                    return;
                }
                if (location.Locked == true)
                {
                    MSD.AddModelError("", "当前库位已被盘点锁定，无法进行收货操作");
                    return;
                }
                // 新增的库存信息集合（后面统一插入数据库）
                List<BaseInventory> newInventoryList = new List<BaseInventory>();
                Dictionary<string, (decimal, decimal)> barcodeOccupyQtyDict = new Dictionary<string, (decimal, decimal)>();  // 条码库存占用（第一个数字是库存数量，第二个数字是本次占用数量）
                List<(Guid, Guid, decimal)> lineIdToInvIdDict = new List<(Guid, Guid, decimal)>();  // 行ID与库存ID的对应关系（第一个Guid是行ID，第二个Guid是库存ID，第三个数字是本次收货数量）
                InventoryTransferIn entity = new InventoryTransferIn
                {
                    ID = Guid.NewGuid(),
                    BusinessDate = DateTime.Now.Date,
                    TransInOrganizationId = data.transferInDTOList.OrgId,
                    TransInWhId = data.transferInDTOList.TransferInLineDTOList[0].TransInWhId,
                    Status = InventoryTransferInStatusEnum.AllReceive,
                    InventoryTransferInLine_InventoryTransferIn = new List<InventoryTransferInLine>(),
                    Memo = data.transferInDTOList.Memo,
                };
                // 循环调入行参数
                foreach (var dataLine in data.transferInDTOList.TransferInLineDTOList)
                {
                    BaseItemMaster item = DC.Set<BaseItemMaster>().Where(x => x.Code == dataLine.ItemCode && x.OrganizationId == org.ID).AsNoTracking().FirstOrDefault();
                    if (item == null)
                    {
                        MSD.AddModelError("", $"料号“{dataLine.ItemCode}”在调入组织中不存在，可尝试同步料号信息");
                        return;
                    }
                    InventoryTransferInLine line = new InventoryTransferInLine
                    {
                        ID = Guid.NewGuid(),
                        ItemMasterId = item.ID,
                        Qty = dataLine.StoreUOMQty,
                        Status = InventoryTransferInLineStatusEnum.AllReceive,
                    };
                    entity.InventoryTransferInLine_InventoryTransferIn.Add(line);
                    decimal receiveQty = 0; // 本次收货数量
                    // 循环收货子行参数
                    int i = 0;
                    foreach (var dataSubLine in dataLine.SubLines)
                    {
                        i++;
                        if (string.IsNullOrEmpty(dataSubLine.BarCode))
                        {
                            MSD.AddModelError("", $"“参数条码不能为空");  // 前端传参错误
                            return;
                        }
                        string[] splits = dataSubLine.BarCode.Split('@');
                        if(splits.Length != 4)
                        {
                            MSD.AddModelError("", $"“{dataSubLine.BarCode}”条码格式不正确，请检查");
                            return;
                        }
                        dataSubLine.SerialNumber = splits[3];
                        dataSubLine.Qty = decimal.Parse(splits[2]);
                        if (splits[1] != item.Code)
                        {
                            MSD.AddModelError("", $"“{dataSubLine.BarCode}”条码与料号“{item.Code}”不匹配");
                            return;
                        }
                        if (dataSubLine.Qty <= 0)
                        {
                            MSD.AddModelError("", $"“{dataSubLine.BarCode}”条码数量必须大于0");
                            return;
                        }
                        //if (dataSubLine.SerialNumber != splits[3])
                        //{
                        //    MSD.AddModelError("", $"“{dataSubLine.BarCode}”条码与序列号“{dataSubLine.SerialNumber}”不匹配");
                        //    return;
                        //}
                        // 使用字典对条码库存占用进行累加
                        if (barcodeOccupyQtyDict.ContainsKey(dataSubLine.SerialNumber))
                        {
                            if (barcodeOccupyQtyDict.TryGetValue(dataSubLine.SerialNumber, out (decimal, decimal) v))
                            {
                                if (barcodeOccupyQtyDict[dataSubLine.SerialNumber].Item1 != v.Item1) // 同一个条码在不同行的条码总数量不一致
                                {
                                    MSD.AddModelError("", $"条码“{dataSubLine.SerialNumber}”在不同行中传参数量不一致，请联系管理员");    // 如果出现，是前端代码有问题
                                    return;
                                }
                                barcodeOccupyQtyDict[dataSubLine.SerialNumber] = (v.Item1, v.Item2 + dataSubLine.BarcodeOccupyQty);
                            }
                        }
                        else
                        {
                            // 判断条码是否已被使用
                            if (!this.IsSNNotUsed(dataSubLine.SerialNumber))
                            {
                                MSD.AddModelError("", $"当前条码“{dataSubLine.SerialNumber}”在系统中已有库存，无法进行调入操作");    // 正常不会出现
                                return;
                            }

                            barcodeOccupyQtyDict.Add(dataSubLine.SerialNumber, ((decimal)dataSubLine.Qty, dataSubLine.BarcodeOccupyQty));
                        }
                        if (barcodeOccupyQtyDict[dataSubLine.SerialNumber].Item1 < barcodeOccupyQtyDict[dataSubLine.SerialNumber].Item2)
                        {
                            MSD.AddModelError("", $"“{dataSubLine.SerialNumber}”条码数量“{barcodeOccupyQtyDict[dataSubLine.SerialNumber].Item1}”不能满足截止当前行(第{i}行)的总需求数量“{barcodeOccupyQtyDict[dataSubLine.SerialNumber].Item2}”");
                            return;
                        }
                        receiveQty += dataSubLine.BarcodeOccupyQty;
                        // 待收库位增加库存
                        if (newInventoryList.Any(x => x.SerialNumber == dataSubLine.SerialNumber))
                        {
                            var inventory = newInventoryList.Find(x => x.SerialNumber == dataSubLine.SerialNumber);
                            inventory.Qty += dataSubLine.BarcodeOccupyQty;
                            lineIdToInvIdDict.Add((line.ID, inventory.ID, dataSubLine.BarcodeOccupyQty));
                        }
                        else
                        {
                            newInventoryList.Add(new BaseInventory
                            {
                                ID = Guid.NewGuid(),
                                ItemMasterId = item.ID,
                                WhLocationId = location.ID,
                                BatchNumber = dataSubLine.BatchNumber,
                                SerialNumber = dataSubLine.SerialNumber,
                                Seiban = dataSubLine.Seiban,
                                SeibanRandom = dataSubLine.SeibanRandom,
                                Qty = dataSubLine.BarcodeOccupyQty,
                                IsAbandoned = false,
                                ItemSourceType = splits[0] == "1" ? ItemSourceTypeEnum.Buy : ItemSourceTypeEnum.Make,
                                FrozenStatus = FrozenStatusEnum.Normal,
                                CreateBy = LoginUserInfo.ITCode,
                                CreateTime = DateTime.Now
                            });
                            lineIdToInvIdDict.Add((line.ID, newInventoryList.Last().ID, dataSubLine.BarcodeOccupyQty));
                        }
                    }

                    if (receiveQty > line.Qty)   // 本次总收货数量大于行数量
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”，总收货数量“{receiveQty}”大于行上的数量“{line.Qty}”");
                    }
                    else if (receiveQty < line.Qty)   // 本次总收货数量小于行数量
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”，总收货数量“{receiveQty}”小于行上的数量“{line.Qty}”。单行必须全部收货，请检查");
                    }
                }
                // 验证所有条码均被完全使用
                foreach (var item in barcodeOccupyQtyDict)
                {
                    if (item.Value.Item1 < item.Value.Item2)
                    {
                        MSD.AddModelError("", $"“{item.Key}”条码数量“{item.Value.Item1}”不能满足总需求数量“{item.Value.Item2}”");
                        return;
                    }
                    else if (item.Value.Item1 > item.Value.Item2)
                    {
                        MSD.AddModelError("", $"“{item.Key}”条码数量“{item.Value.Item1}”，总需求数量“{item.Value.Item2}”，条码数量必须被完全收货");
                        return;
                    }
                }

                Entity = entity;
                DoAdd();
                if (!MSD.IsValid)
                {
                    return;
                }
                // 重新获取过来，下面要修改用
                var doc = DC.Set<InventoryTransferIn>().Include(x => x.InventoryTransferInLine_InventoryTransferIn).Where(x => x.ID == entity.ID).FirstOrDefault();
                if (doc == null)
                {
                    MSD.AddModelError("", "创建调入单失败");
                    return;
                }

                ((DataContext)DC).BulkInsert(newInventoryList);

                if (!MSD.IsValid)
                {
                    return;
                }

                // 创建U9单据
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, org.Code, LoginUserInfo.Name);
                data.businessId = doc.ID;
                U9Return<CreateTransferInResult> u9Return = apiHelper.CreateTransferIn(data);
                if (!u9Return.Success)
                {
                    MSD.AddModelError("", u9Return.Msg);
                    return;
                }
                else
                {
                    if (DC.Set<InventoryTransferIn>().Any(x => x.ErpID == u9Return.Entity.ErpID))
                    {
                        MSD.AddModelError("", $"重复操作，请查看调入单据：{u9Return.Entity.DocNo}");
                        return;
                    }
                    doc.ErpID = u9Return.Entity.ErpID;
                    doc.DocType = u9Return.Entity.DocType;
                    doc.DocNo = u9Return.Entity.DocNo;
                    for (int i = 0; i < doc.InventoryTransferInLine_InventoryTransferIn.Count; i++)
                    {
                        doc.InventoryTransferInLine_InventoryTransferIn[i].ErpID = u9Return.Entity.Lines[i].ErpID;
                        doc.InventoryTransferInLine_InventoryTransferIn[i].DocLineNo = u9Return.Entity.Lines[i].DocLineNo;
                    }
                }

                foreach (var inventory in newInventoryList)
                {
                    // 创建库存流水
                    if (!this.CreateInvLog(OperationTypeEnum.InventoryTransferInCreate, doc.DocNo, null, inventory.ID, null, inventory.Qty))
                    {
                        return;
                    }
                }
                foreach (var item in lineIdToInvIdDict)
                {
                    // 创建单据库存关联关系
                    if (!this.CreateInvRelation(DocTypeEnum.InventoryTransferIn, item.Item2, doc.ID, item.Item1, item.Item3)) 
                    {
                        return;
                    }
                }

                DC.SaveChanges();

                trans.Commit();
            }
        }

        /// <summary>
        /// 审核并上架
        /// </summary>
        /// <param name="docId"></param>
        public void ApproveAndPutaway(TransferInApprovePara data)
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            if (whid == Guid.Empty)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录2");
                return;
            }
            if (data == null)
            {
                MSD.AddModelError("", "参数不能为空");
                return;
            }

            using (var trans = DC.BeginTransaction())
            {
                var doc = DC.Set<InventoryTransferIn>()
                    .Include(x => x.InventoryTransferInLine_InventoryTransferIn)
                    .Include(x => x.TransInOrganization)
                    .Where(x => x.ID == data.ID)
                    .FirstOrDefault();
                if (doc == null)
                {
                    MSD.AddModelError("", "单据不存在");
                    return;
                }
                if (doc.Status != InventoryTransferInStatusEnum.AllReceive)
                {
                    MSD.AddModelError("", "单据未全部收货，无法进行审核上架操作");
                    return;
                }
                if (doc.Status == InventoryTransferInStatusEnum.AllInWh || doc.Status == InventoryTransferInStatusEnum.PartInWh)
                {
                    MSD.AddModelError("", "单据已入库，无法进行审核上架操作");
                    return;
                }
                string orgCode = doc.TransInOrganization.Code;

                // 获取关联的单据库存关联关系中的所有条码
                var invIds = DC.Set<BaseDocInventoryRelation>()
                    .Where(x => x.BusinessId == doc.ID && x.DocType == DocTypeEnum.InventoryTransferIn)
                    .AsNoTracking()
                    .Select(x => x.InventoryId)
                    .Distinct().ToList();
                if (invIds.Count == 0)
                {
                    MSD.AddModelError("", "单据收货信息丢失。可尝试取消收货操作后，重新收货");
                    return;
                }
                // 获取参数中的所有库存信息
                var dataInvIds = data.Details.Select(x => x.InventoryID).Distinct().ToList();
                if (dataInvIds.Count == 0)
                {
                    MSD.AddModelError("", "参数中库存信息数据缺失");  // 前端应避免出现这种情况
                    return;
                }
                if (dataInvIds.Count != dataInvIds.Count)
                {
                    MSD.AddModelError("", "参数中存在库存信息重复的数据");  // 前端应避免出现这种情况
                    return;
                }
                // 验证参数中的库存信息是否与关联的单据库存关联关系匹配
                if (dataInvIds.Count != invIds.Count || !dataInvIds.All(x => invIds.Contains(x)))
                {
                    MSD.AddModelError("", "参数中库存信息与单据关联的库存信息不匹配");  // 前端应避免出现这种情况
                    return;
                }
                // 将待收库位的条码更新到正式库位中
                foreach (var item in data.Details)
                {
                    var location = DC.Set<BaseWhLocation>()
                        .AsNoTracking()
                        .Include(x => x.WhArea.WareHouse)
                        .Where(x => x.ID == item.LocationID).FirstOrDefault();
                    if (location == null)
                    {
                        MSD.AddModelError("", $"库位“{item.LocationID}”不存在");
                        return;
                    }
                    if (location.IsEffective == EffectiveEnum.Ineffective)
                    {
                        MSD.AddModelError("", $"库位“{location.Code}”已失效，不能进行上架操作");
                        return;
                    }
                    if (location.Locked == true)
                    {
                        MSD.AddModelError("", $"库位“{location.Code}”已被盘点锁定，不能进行上架操作");
                        return;
                    }
                    if (location.AreaType != WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"库位“{location.Code}”不是正常库位，不能进行上架操作");
                        return;
                    }
                    if (whid != location.WhArea.WareHouseId)
                    {
                        MSD.AddModelError("", $"库位“{location.Code}”所在存储地点非当前登录存储地点");
                        return;
                    }
                    var oldInv = DC.Set<BaseInventory>()
                        .Where(x => x.ID == item.InventoryID)
                        .FirstOrDefault();
                    if (oldInv == null)
                    {
                        MSD.AddModelError("", $"库存“{item.InventoryID}”不存在");
                        return;
                    }
                    if (oldInv.IsAbandoned == true)
                    {
                        MSD.AddModelError("", $"库存ID“{item.InventoryID}”已被弃用，请刷新后重试");
                        return;
                    }
                    if (oldInv.Qty == 0)
                    {
                        MSD.AddModelError("", $"库存“{item.InventoryID}”数量为0，无需上架");
                        return;
                    }
                    var oldLocation = DC.Set<BaseWhLocation>().Where(x => x.ID == oldInv.WhLocationId).Include(x => x.WhArea.WareHouse).AsNoTracking().FirstOrDefault();
                    if (oldLocation == null)
                    {
                        MSD.AddModelError("", $"库存“{item.InventoryID}”原库位不存在");
                        return;
                    }
                    if (oldLocation.WhArea.WareHouseId != location.WhArea.WareHouseId)
                    {
                        MSD.AddModelError("", $"条码“{oldInv.SerialNumber}”待收库位与正式库位的存储地点不一致");
                        return;
                    }
                    // 将待收库位的库存信息复制到正式库位中
                    var newInv = this.CopyInventory(oldInv, location.ID);

                    // 待收库位库存信息清零、作废
                    oldInv.Qty = 0;
                    oldInv.IsAbandoned = true;
                    oldInv.UpdateBy = LoginUserInfo.ITCode;
                    oldInv.UpdateTime = DateTime.Now;
                    DC.SaveChanges();

                    DC.AddEntity(newInv);
                    

                    // 创建库存流水
                    this.CreateInvLog(OperationTypeEnum.InventoryTransferInApprove, doc.DocNo, oldInv.ID, newInv.ID, -newInv.Qty, newInv.Qty);
                }
                // 更新单据状态、行状态
                doc.Status = InventoryTransferInStatusEnum.AllInWh;
                doc.InventoryTransferInLine_InventoryTransferIn.ForEach(x =>
                {
                    x.Status = InventoryTransferInLineStatusEnum.AllInWh;
                });

                DC.SaveChanges();
                if (!MSD.IsValid)
                {
                    return;
                }
                // 调用U9接口
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, orgCode, LoginUserInfo.Name);
                U9Return u9Return = apiHelper.ApproveTransferIn(doc.DocNo);
                if (u9Return.Success)
                {
                    trans.Commit();
                }
                else
                {
                    MSD.AddModelError("", u9Return.Msg);
                    return;
                }
            }
        }
    }
}
