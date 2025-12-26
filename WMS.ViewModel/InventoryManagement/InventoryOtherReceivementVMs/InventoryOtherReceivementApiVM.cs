using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using Elsa;
using WMS.Model.BaseData;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using Microsoft.EntityFrameworkCore;
using WMS.DataAccess;
using WMS.Model;
using EFCore.BulkExtensions;


namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs
{
    public partial class InventoryOtherReceivementApiVM : BaseCRUDVM<InventoryOtherReceivement>
    {

        public InventoryOtherReceivementApiVM()
        {
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

        /// <summary>
        /// 获取杂发单（用于生成杂收单）
        /// </summary>
        /// <param name="docNo">其它出库单（杂发单）单号</param>
        /// <returns></returns>
        public GetMiscShipForMiscRcvResult GetMiscShipForMiscRcv(string docNo)
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
            U9Return<GetMiscShipForMiscRcvResult> u9Return = apiHelper.GetMiscShipForMiscRcv(docNo);
            if (u9Return.Success)
            {
                var temp = this.GetEntityBySourceSystemId<InventoryOtherReceivement, BaseOrganization>(u9Return.Entity.BenefitOrgID);   // 转换受益组织
                if (temp == null)
                {
                    MSD.AddModelError("", $"本系统中受益组织({u9Return.Entity.BenefitOrgID})不存在或已禁用，请尝试同步组织信息");
                    return null;
                }
                u9Return.Entity.BenefitOrgID = temp.ID.ToString();
                u9Return.Entity.BenefitOrgCode = temp.Code;
                u9Return.Entity.BenefitOrgName = temp.Name;

                var temp2 = this.GetEntityBySourceSystemId<InventoryOtherReceivement, BaseOrganization>(u9Return.Entity.SourceOrgID);   // 转换来源组织
                if (temp2 == null)
                {
                    MSD.AddModelError("", $"本系统中来源组织({u9Return.Entity.SourceOrgID})不存在或已禁用，请尝试同步组织信息");
                    return null;
                }
                u9Return.Entity.SourceOrgID = temp2.ID.ToString();
                u9Return.Entity.SourceOrgCode = temp2.Code;
                u9Return.Entity.SourceOrgName = temp2.Name;

                var temp3 = this.GetEntityBySourceSystemId<InventoryOtherReceivement, BaseWareHouse>(u9Return.Entity.SourceWhID);   // 转换来源存储地点
                if (temp3 == null)
                {
                    MSD.AddModelError("", $"本系统中来源存储地点({u9Return.Entity.SourceWhID})不存在或已禁用，请尝试同步组织信息");
                    return null;
                }
                u9Return.Entity.SourceWhID = temp3.ID.ToString();
                u9Return.Entity.SourceWhCode = temp3.Code;
                u9Return.Entity.SourceWhName = temp3.Name;

                foreach (var line in u9Return.Entity.Lines)
                {
                    var temp4 = this.GetEntityBySourceSystemId<InventoryOtherReceivement, BaseItemMaster>(line.ItemID);   // 转换料品
                    if (temp4 == null)
                    {
                        MSD.AddModelError("", $"本系统中料品({line.ItemID})不存在或已禁用，请尝试同步料号信息");
                        return null;
                    }
                    line.ItemID = temp4.ID.ToString();
                    line.ItemName = temp4.Name;
                    line.ItemCode = temp4.Code;
                    line.SPECS = temp4.SPECS;
                }
                // 判断受益组织是否匹配（转换完了才能进行匹配，不然没有值）
                if (u9Return.Entity.BenefitOrgID != loginWh.OrganizationId.ToString())
                {
                    MSD.AddModelError("", $"单据{docNo}的受益组织与登录存储地点的组织不匹配，请尝试重新登录");
                    return null;
                }
                //TransferOutMatch(u9Return.Entity);    // 匹配杂发单的扫码记录（来自调出调入的逻辑），暂时先不做
                return u9Return.Entity;
            }
            else
            {
                MSD.AddModelError("", u9Return.Msg);
                return null;
            }
        }

        /// <summary>
        /// 创建单据
        /// </summary>
        /// <param name="para"></param>
        public void CreateDoc(CreateMiscRcvPara data)
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            BaseWareHouse wh = DC.Set<BaseWareHouse>()
                .Include(x => x.Organization)
                .Where(x => x.ID == whid && x.IsEffective == EffectiveEnum.Effective && x.IsValid == true)
                .AsNoTracking()
                .FirstOrDefault();
            if (wh == null)
            {
                MSD.AddModelError("", "存储地点信息无效，请尝试重新登录");
                return;
            }
            // 验证数据有效性
            if (data == null)
            {
                MSD.AddModelError("", "参数不能为空");
                return;
            }
            if (data.lines == null || data.lines.Count == 0)
            {
                MSD.AddModelError("", "参数行数据不能为空");
                return;
            }
            if (data.scrap != 0 && data.scrap != 1)
            {
                MSD.AddModelError("", "参数是否报废字段值错误");
                return;
            }
            data.org = wh.Organization.Code;
            foreach (var line in data.lines)
            {
                if (line.Qty <= 0)
                {
                    MSD.AddModelError("", $"行上的数量必须大于0");
                    return;
                }
                if (line.SrcID <= 0)
                {
                    MSD.AddModelError("", $"行上的来源杂发单行ID不能为空");
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
                // 新增的库存信息集合（后面统一插入数据库）
                List<BaseInventory> newInventoryList = new List<BaseInventory>();
                Dictionary<string, (decimal, decimal)> barcodeOccupyQtyDict = new Dictionary<string, (decimal, decimal)>();  // 条码库存占用（第一个数字是库存数量，第二个数字是本次占用数量）
                List<(Guid, Guid, decimal)> lineIdToInvIdDict = new List<(Guid, Guid, decimal)>();  // 行ID与库存ID的对应关系（第一个Guid是行ID，第二个Guid是库存ID，第三个数字是本次收货数量）
                InventoryOtherReceivement entity = new InventoryOtherReceivement
                {
                    // ID = Guid.NewGuid(),
                    BusinessDate = DateTime.Now.Date,
                    IsScrap = data.scrap == 1 ? true : false,
                    Memo = data.memo,
                    InventoryOtherReceivementLine_InventoryOtherReceivement = new List<InventoryOtherReceivementLine>()
                };
                List<string> itemCodes = new List<string>();
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, wh.Organization.Code, LoginUserInfo.Name);
                // 循环调入行参数
                foreach (var dataLine in data.lines)
                {
                    BaseItemMaster item = DC.Set<BaseItemMaster>().Where(x => x.Code == dataLine.ItemCode && x.OrganizationId == wh.Organization.ID).AsNoTracking().FirstOrDefault();
                    if (item == null)
                    {
                        MSD.AddModelError("", $"料号“{dataLine.ItemCode}”在当前登录组织中不存在，可尝试同步料号信息");
                        return;
                    }
                    if (!itemCodes.Contains(item.Code))     // 防止相同料号重复添加
                    {
                        itemCodes.Add(item.Code);
                    }
                    InventoryOtherReceivementLine line = new InventoryOtherReceivementLine
                    {
                        // ID = Guid.NewGuid(),
                        ItemMasterId = item.ID,
                        Qty = dataLine.Qty,
                    };
                    entity.InventoryOtherReceivementLine_InventoryOtherReceivement.Add(line);
                    decimal receiveQty = 0; // 本次收货数量
                    // 循环收货子行参数
                    int i = 0;
                    foreach (var dataSubLine in dataLine.SubLines)
                    {
                        // 获取上架库位
                        BaseWhLocation location = DC.Set<BaseWhLocation>()
                            .Where(x => x.WhArea.WareHouseId == whid && x.ID == dataSubLine.LocationID && x.IsEffective == EffectiveEnum.Effective)
                            .AsNoTracking()
                            .FirstOrDefault();
                        if (location == null)
                        {
                            MSD.AddModelError("", "库位不存在（或不属于当前存储地点）");
                            return;
                        }
                        if (location.Locked == true)
                        {
                            MSD.AddModelError("", "当前库位已被盘点锁定，无法进行上架操作");
                            return;
                        }

                        i++;
                        if (string.IsNullOrEmpty(dataSubLine.BarCode))
                        {
                            MSD.AddModelError("", $"“参数条码不能为空");  // 前端传参错误
                            return;
                        }
                        string[] splits = dataSubLine.BarCode.Split('@');
                        if (splits.Length != 4)
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
                                MSD.AddModelError("", $"当前条码“{dataSubLine.SerialNumber}”在系统中已有库存，无法进行上机操作");    // 正常不会出现
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
                        // 库位增加库存
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
                    else if (receiveQty < line.Qty)   // 本次总收货数量小于行数量（正常不会出现。前端数据构造错误才会出现）
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”，总收货数量“{receiveQty}”小于行上的数量“{line.Qty}”。（前端数据构造错误）");
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
                var result = apiHelper.GetKnifeInfo(itemCodes);
                if (result.Entity != null && result.Entity.MatchDatas != null && result.Entity.MatchDatas.Count > 0)
                {
                    var matchData = result.Entity.MatchDatas.FindAll(x => x.IsActive && x.LedgerIncluded == true);
                    if (matchData != null && matchData.Count > 0)
                    {
                        MSD.AddModelError("", $"以下料号为高价值刀具，无法进行杂收操作。({matchData.Select(x => x.ItemCode).Aggregate((a, b) => a + "," + b)})");
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
                var doc = DC.Set<InventoryOtherReceivement>().Include(x => x.InventoryOtherReceivementLine_InventoryOtherReceivement).Where(x => x.ID == entity.ID).FirstOrDefault();
                if (doc == null)
                {
                    MSD.AddModelError("", "创建杂收单失败");
                    return;
                }

                ((DataContext)DC).BulkInsert(newInventoryList);

                if (!MSD.IsValid)
                {
                    return;
                }

                // 创建U9单据

                data.businessId = doc.ID;
                U9Return<CreateMiscRcvResult> u9Return = apiHelper.CreateMiscRcvTrans(data);
                if (!u9Return.Success)
                {
                    MSD.AddModelError("", u9Return.Msg);
                    return;
                }
                else
                {
                    if (DC.Set<InventoryTransferIn>().Any(x => x.ErpID == u9Return.Entity.ErpID))
                    {
                        MSD.AddModelError("", $"重复操作，请查看杂收单据：{u9Return.Entity.DocNo}");
                        return;
                    }
                    doc.ErpID = u9Return.Entity.ErpID;
                    doc.DocNo = u9Return.Entity.DocNo;
                    for (int i = 0; i < doc.InventoryOtherReceivementLine_InventoryOtherReceivement.Count; i++)
                    {
                        doc.InventoryOtherReceivementLine_InventoryOtherReceivement[i].ErpID = u9Return.Entity.Lines[i].ErpID;
                        doc.InventoryOtherReceivementLine_InventoryOtherReceivement[i].DocLineNo = u9Return.Entity.Lines[i].DocLineNo;
                    }
                }

                foreach (var inventory in newInventoryList)
                {
                    // 创建库存流水
                    if (!this.CreateInvLog(OperationTypeEnum.InventoryOtherReceivementCreate, doc.DocNo, null, inventory.ID, null, inventory.Qty))
                    {
                        return;
                    }
                }
                foreach (var item in lineIdToInvIdDict)
                {
                    // 创建单据库存关联关系
                    if (!this.CreateInvRelation(DocTypeEnum.InventoryOtherReceivement, item.Item2, doc.ID, item.Item1, item.Item3))
                    {
                        return;
                    }
                }

                DC.SaveChanges();

                trans.Commit();
            }
        }
    }
}
