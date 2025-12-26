using EFCore.BulkExtensions;
using Elsa;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.DataAccess;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.PurchaseManagement;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;


namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs
{
    public partial class PurchaseOutsourcingReturnApiVM : BaseCRUDVM<PurchaseOutsourcingReturn>
    {
        #region 系统标准方法

        public PurchaseOutsourcingReturnApiVM()
        {
            SetInclude(x => x.Organization);
            SetInclude(x => x.Supplier);
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
        /// <param name="type">0-待收货，1-待审核</param>
        /// <returns></returns>
        public List<PurchaseOutsourcingReturnReturn> GetList(int type = 0)
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
            if (type != 0 && type != 1)
            {
                MSD.AddModelError("", "参数错误");
                return null;
            }
            List<PurchaseOutsourcingReturnReturn> list = new List<PurchaseOutsourcingReturnReturn>();
            Expression<Func<PurchaseOutsourcingReturn, bool>> queryable;
            if (type == 0)  // 待收货（未全部收货）。需匹配登录用户的存储地点
            {
                queryable = x => (x.Status == Model.PurchaseOutsourcingReturnStatusEnum.NotReceive
                    || x.Status == PurchaseOutsourcingReturnStatusEnum.PartReceive)
                    && x.PurchaseOutsourcingReturnLine_OutsourcingReturn.Any(y => y.WareHouseId == whid);
            }
            else
            {
                queryable = x => x.Status == PurchaseOutsourcingReturnStatusEnum.AllReceive 
                    && x.PurchaseOutsourcingReturnLine_OutsourcingReturn.Any(y => y.WareHouseId == whid);
            }
            list = DC.Set<PurchaseOutsourcingReturn>()
               .Where(queryable)
               .AsNoTracking()
               .Select(x => new PurchaseOutsourcingReturnReturn
               {
                   ID = x.ID,
                   DocNo = x.DocNo,
                   DocType = x.DocType,
                   Status = x.Status.GetEnumDisplayName(),
                   BusinessDate = ((DateTime)x.BusinessDate).ToString("yyyy-MM-dd"),
                   Supplier = x.Supplier.Name,
                   SumQty = (int)(x.PurchaseOutsourcingReturnLine_OutsourcingReturn.Sum(y => y.Qty) ?? 0),
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
        /// 获取单据（收货）
        /// </summary>
        /// <param name="docNo">单据编号</param>
        public PurchaseOutsourcingReturnReturn GetDoc(string docNo)
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

            PurchaseOutsourcingReturn doc = DC.Set<PurchaseOutsourcingReturn>()
                .Where(x => x.DocNo == docNo)
                .Include(x => x.Organization)
                .Include(x => x.Supplier)
                .Include(x => x.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                .ThenInclude(x => x.WareHouse)
                .Include(x => x.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                .ThenInclude(x => x.ItemMaster)
                .AsNoTracking().FirstOrDefault();
            if (doc != null)    // 单据在本系统中存在
            {
                // 判断行存储地点释放匹配当前登录用户的存储地点
                PurchaseOutsourcingReturnLine line = doc.PurchaseOutsourcingReturnLine_OutsourcingReturn.Find(x => x.WareHouseId != whid);
                if (line != null)
                {
                    // 存在行存储地点不匹配，返回错误信息
                    MSD.AddModelError("", $"单据{docNo}的存储地点为{line.WareHouse.Code}，与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                if (doc.Status != PurchaseOutsourcingReturnStatusEnum.NotReceive && doc.Status != PurchaseOutsourcingReturnStatusEnum.PartReceive)
                {
                    MSD.AddModelError("", $"当前单据状态“{doc.Status.GetEnumDisplayName()}”，无法进行收货");
                    return null;
                }
                return new PurchaseOutsourcingReturnReturn(doc);
            }
            // 本系统不存在时，尝试从ERP获取
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<PurchaseOutsourcingReturn> u9Return = apiHelper.GetPurchaseOutsourcingReturn(docNo);
            if (u9Return.Success)
            {
                // 引用类型外部系统ID转换
                string r = this.SSIdAttrToId(u9Return.Entity, u9Return.Entity.Organization, "Organization");    // 转换组织
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                r = this.SSIdAttrToId(u9Return.Entity, u9Return.Entity.Supplier, "Supplier");   // 转换供应商
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                foreach (var line in u9Return.Entity.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                {
                    r = this.SSIdAttrToId(line, line.ItemMaster, "ItemMaster");   // 转换物料
                    if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                    r = this.SSIdAttrToId(line, line.WareHouse, "WareHouse");   // 转换存储地点
                    if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                }
                // 判断存储地点是否匹配（转换完了才能进行匹配，不然没有值）
                foreach (var line in u9Return.Entity.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                {
                    if (line.WareHouseId != whid)
                    {
                        // 存在行存储地点不匹配，返回错误信息
                        MSD.AddModelError("", $"单据{docNo}的存储地点与当前登录用户的存储地点{loginWh.Code}不匹配");
                        return null;
                    }
                    line.Status = PurchaseOutsourcingReturnLineStatusEnum.NotReceive;
                    line.ToBeReceiveQty = line.Qty;
                    line.ToBeInWhQty = 0;
                    line.ReceiveQty = 0;
                    line.InWhQty = 0;
                }
                // 将单据保存到本系统中
                Entity = u9Return.Entity;
                Entity.Status = PurchaseOutsourcingReturnStatusEnum.NotReceive;
                DoAdd();
                if (!MSD.IsValid)
                {
                    return null;
                }
                doc = DC.Set<PurchaseOutsourcingReturn>()
                    .Where(x => x.DocNo == docNo)
                    .Include(x => x.Organization)
                    .Include(x => x.Supplier)
                    .Include(x => x.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                    .ThenInclude(x => x.WareHouse)
                    .Include(x => x.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                    .ThenInclude(x => x.ItemMaster)
                    .AsNoTracking().FirstOrDefault();   // 要重新获取，否则有些外键实体会为null
                if (doc == null)
                {
                    MSD.AddModelError("", "单据已同步，但未能正常显示。请尝试退出后重试");    // 不会出现这种情况
                    return null;
                }
                return new PurchaseOutsourcingReturnReturn(doc);
            }
            else
            {
                MSD.AddModelError("", u9Return.Msg);
                return null;
            }
        }

        /// <summary>
        /// 获取单据（审核）
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public PurchaseOutsourcingReturnReturn GetDocForApprove(string docNo)
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

            PurchaseOutsourcingReturn doc = DC.Set<PurchaseOutsourcingReturn>()
                .Where(x => x.DocNo == docNo)
                .Include(x => x.Organization)
                .Include(x => x.Supplier)
                .Include(x => x.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                .ThenInclude(x => x.WareHouse)
                .Include(x => x.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                .ThenInclude(x => x.ItemMaster)
                .AsNoTracking().FirstOrDefault();
            if (doc != null)    // 单据在本系统中存在
            {
                // 判断行存储地点释放匹配当前登录用户的存储地点
                PurchaseOutsourcingReturnLine line = doc.PurchaseOutsourcingReturnLine_OutsourcingReturn.Find(x => x.WareHouseId != whid);
                if (line != null)
                {
                    // 存在行存储地点不匹配，返回错误信息
                    MSD.AddModelError("", $"单据{docNo}的存储地点为{line.WareHouse.Code}，与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                //if (doc.InspectStatus != PurchaseReceivementInspectStatusEnum.AllInspected)
                //{
                //    MSD.AddModelError("", $"单据{docNo}的检验状态为{doc.InspectStatus.GetEnumDisplayName()}，无法进行审核");
                //    return null;
                //}
                if (doc.Status != PurchaseOutsourcingReturnStatusEnum.AllReceive)
                {
                    MSD.AddModelError("", $"单据{docNo}的状态为{doc.Status.GetEnumDisplayName()}，无法进行审核");
                    return null;
                }
                PurchaseOutsourcingReturnReturn result = new PurchaseOutsourcingReturnReturn(doc);
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
        public void GetSuggestLocs(PurchaseOutsourcingReturnReturn doc)
        {
            foreach (var line in doc.Lines)
            {
                line.SuggestLocs = new List<PurchaseOutsourcingReturnReturnLineSuggestLoc>();
                List<BaseInventory> invs = this.GetItemInventory(line.ItemID, doc.WareHouseID);
                if (invs.Count > 0)
                {
                    invs = invs.OrderByDescending(x => x.CreateTime).ToList(); // 降序
                    foreach (var inv in invs)
                    {
                        if (!line.SuggestLocs.Exists(x => x.LocCode == inv.WhLocation.Code))
                        {
                            line.SuggestLocs.Add(new PurchaseOutsourcingReturnReturnLineSuggestLoc
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
        /// 判断单据是否存在
        /// </summary>
        /// <param name="sourceSystemId">来源系统单据ID</param>
        /// <returns></returns>
        public bool IsDocExist(string sourceSystemId)
        {
            var entity = DC.Set<PurchaseOutsourcingReturn>().Where(x => x.SourceSystemId == sourceSystemId).AsNoTracking().FirstOrDefault();
            if (entity == null)
            {
                return false;
            }
            else
            {
                if (entity.Status == PurchaseOutsourcingReturnStatusEnum.NotReceive)
                {
                    MSD.AddModelError("", $"G2WMS中已存在此单据，请先删除G2WMS中的单据，再进行操作");
                    return true;
                }
                else
                {
                    MSD.AddModelError("", $"G2WMS中已存在此单据，且已有收货记录，U9单据禁止手动操作");
                    return true;
                }
            }
        }

        /// <summary>
        /// 创建单据（ERP调用）
        /// </summary>
        /// <param name="entity"></param>
        public void Create(PurchaseOutsourcingReturn entity)
        {
            MSD.Clear();    // 看下是否需要判断
            var existEntity = DC.Set<PurchaseOutsourcingReturn>().Where(x => x.DocNo == entity.DocNo).FirstOrDefault();
            if (existEntity != null)
            {
                MSD.AddModelError("", "单据编号已存在");
                return;
            }
            // 引用类型外部系统ID转换
            string r = this.SSIdAttrToId(entity, entity.Organization, "Organization");    // 组织
            if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
            r = this.SSIdAttrToId(entity, entity.Supplier, "Supplier");   // 供应商
            if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
            foreach (var line in entity.PurchaseOutsourcingReturnLine_OutsourcingReturn)
            {
                r = this.SSIdAttrToId(line, line.ItemMaster, "ItemMaster");   // 转换物料
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
                r = this.SSIdAttrToId(line, line.WareHouse, "WareHouse");   // 转换存储地点
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
            }
            foreach (var line in entity.PurchaseOutsourcingReturnLine_OutsourcingReturn)
            {
                line.Status = PurchaseOutsourcingReturnLineStatusEnum.NotReceive;
                line.ToBeReceiveQty = line.Qty;
                line.ToBeInWhQty = 0;
                line.ReceiveQty = 0;
                line.InWhQty = 0;
            }
            // 将单据保存到本系统中
            Entity = entity;
            Entity.Status = PurchaseOutsourcingReturnStatusEnum.NotReceive;
            DoAdd();
        }

        /// <summary>
        /// 收货操作
        /// </summary>
        /// <param name="data"></param>
        public void Receiving(PurchaseOutsourcingReturnReceivingPara data)
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            // 验证数据有效性
            if (data == null)
            {
                MSD.AddModelError("", "参数不能为空");
                return;
            }
            if (data.Lines == null || data.Lines.Count == 0)
            {
                MSD.AddModelError("", "参数行数据不能为空");
                return;
            }
            using (var trans = DC.BeginTransaction())
            {
                PurchaseOutsourcingReturn doc = DC.Set<PurchaseOutsourcingReturn>()             //.AsNoTracking()
                    .Include(x => x.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                    .ThenInclude(x => x.ItemMaster)
                    .Where(x => x.ID == data.ID)
                    .FirstOrDefault();
                if (doc == null)
                {
                    MSD.AddModelError("", "单据不存在");
                    return;
                }
                if (doc.PurchaseOutsourcingReturnLine_OutsourcingReturn.Any(x => x.WareHouseId != whid))
                {
                    MSD.AddModelError("", "单据存储地点不匹配，可尝试重新选择登录存储地点");
                    return;
                }
                if (doc.Status != PurchaseOutsourcingReturnStatusEnum.NotReceive && doc.Status != PurchaseOutsourcingReturnStatusEnum.PartReceive)
                {
                    MSD.AddModelError("", "单据已完成收货业务，无法继续进行收货操作");
                    return;
                }
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
                // 循环收货行参数
                foreach (var dataLine in data.Lines)
                {
                    var line = doc.PurchaseOutsourcingReturnLine_OutsourcingReturn.Find(x => x.ID == dataLine.ID);
                    if (line == null)
                    {
                        MSD.AddModelError("", $"行数据“{dataLine.ID}”不存在");
                        return;
                    }
                    if (line.Status != PurchaseOutsourcingReturnLineStatusEnum.NotReceive && line.Status != PurchaseOutsourcingReturnLineStatusEnum.PartReceive)
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”已完成收货业务，无法进行收货操作");
                        return;
                    }
                    if (line.ToBeReceiveQty <= 0)
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”待收货数量为0，无需收货");
                        return;
                    }
                    if (dataLine.SubLines == null || dataLine.SubLines.Count == 0)
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”收货子行数据不能为空");
                        return;
                    }
                    decimal receiveQty = 0; // 本次收货数量
                                            // 循环收货子行参数
                    foreach (var dataSubLine in dataLine.SubLines)
                    {
                        var barcode = DC.Set<BaseBarCode>().Include(x => x.Item).Where(x => x.ID == dataSubLine.BarcodeID).AsNoTracking().FirstOrDefault();
                        if (barcode == null)
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”，子行“{dataSubLine.BarcodeID}”条码不存在");
                            return;
                        }
                        if (barcode.Item.Code != line.ItemMaster.Code)
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”，子行“{barcode.Code}”条码与行物料不匹配");
                            return;
                        }
                        if (barcode.DocNo != doc.DocNo)
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”，子行“{barcode.Code}”条码与单据不匹配");
                            return;
                        }
                        // 使用字典对条码库存占用进行累加
                        if (barcodeOccupyQtyDict.ContainsKey(barcode.Code))
                        {
                            if (barcodeOccupyQtyDict.TryGetValue(barcode.Code, out (decimal, decimal) v))
                            {
                                barcodeOccupyQtyDict[barcode.Code] = (v.Item1, v.Item2 + dataSubLine.BarcodeOccupyQty);
                            }
                        }
                        else
                        {
                            // 判断条码是否已被使用
                            if (!this.IsSNNotUsed(barcode.Sn))
                            {
                                MSD.AddModelError("", $"条码“{barcode.Code}”已被使用，请使用新条码");
                                return;
                            }

                            barcodeOccupyQtyDict.Add(barcode.Code, ((decimal)barcode.Qty, dataSubLine.BarcodeOccupyQty));
                        }
                        if (barcode.Qty < barcodeOccupyQtyDict[barcode.Code].Item2)
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”，“{barcode.Code}”条码数量“{barcode.Qty}”不能满足截止当前行{line.DocLineNo}的总需求数量“{barcodeOccupyQtyDict[barcode.Code].Item2}”");
                            return;
                        }
                        receiveQty += dataSubLine.BarcodeOccupyQty;
                        // 待收库位增加库存
                        if (newInventoryList.Any(x => x.SerialNumber == barcode.Sn))
                        {
                            var inventory = newInventoryList.Find(x => x.SerialNumber == barcode.Sn);
                            inventory.Qty += dataSubLine.BarcodeOccupyQty;
                            lineIdToInvIdDict.Add((line.ID, inventory.ID, dataSubLine.BarcodeOccupyQty));
                        }
                        else
                        {
                            newInventoryList.Add(new BaseInventory
                            {
                                ID = Guid.NewGuid(),
                                ItemMasterId = line.ItemMasterId,
                                WhLocationId = location.ID,
                                BatchNumber = doc.DocNo,
                                SerialNumber = barcode.Sn,
                                Seiban = barcode.Seiban,
                                SeibanRandom = barcode.SeibanRandom,
                                Qty = dataSubLine.BarcodeOccupyQty,
                                IsAbandoned = false,
                                ItemSourceType = ItemSourceTypeEnum.Buy,
                                FrozenStatus = FrozenStatusEnum.Normal,
                                CreateBy = LoginUserInfo.ITCode,
                                CreateTime = DateTime.Now
                            });
                            lineIdToInvIdDict.Add((line.ID, newInventoryList.Last().ID, dataSubLine.BarcodeOccupyQty));
                        }
                    }

                    // 更新行已收货数量和待收货数量
                    if (receiveQty > line.ToBeReceiveQty)   // 本次总收货数量大于待收货数量
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”，总收货数量“{receiveQty}”大于待收货数量“{line.ToBeReceiveQty}”");
                        return;
                    }
                    else
                    {
                        line.ReceiveQty += receiveQty;
                        line.ToBeReceiveQty -= receiveQty;
                        line.ToBeInWhQty = line.ReceiveQty - line.InWhQty;  // 待入库数量=已收货数量-已入库数量
                    }
                    // 设置行状态
                    if (line.ToBeReceiveQty == 0)
                    {
                        line.Status = PurchaseOutsourcingReturnLineStatusEnum.AllReceive;
                    }
                    else if (line.ReceiveQty == 0)
                    {
                        line.Status = PurchaseOutsourcingReturnLineStatusEnum.NotReceive;
                    }
                    else
                    {
                        line.Status = PurchaseOutsourcingReturnLineStatusEnum.PartReceive;
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
                // 整行拒收的自动设置为已收货
                //foreach (var line in doc.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                //{
                //    if (line.Status == PurchaseOutsourcingReturnLineStatusEnum.NotReceive
                //        && line.ToBeReceiveQty == 0)
                //    {
                //        line.Status = PurchaseOutsourcingReturnLineStatusEnum.AllReceive;
                //    }
                //}

                // 设置单据状态
                if (doc.PurchaseOutsourcingReturnLine_OutsourcingReturn.All(x => x.Status == PurchaseOutsourcingReturnLineStatusEnum.AllReceive))   // 全部行已收货
                {
                    doc.Status = PurchaseOutsourcingReturnStatusEnum.AllReceive;
                }
                else if (doc.PurchaseOutsourcingReturnLine_OutsourcingReturn.All(x => x.Status == PurchaseOutsourcingReturnLineStatusEnum.NotReceive))  // 全部行未收货（收货动作不会出现这种情况）
                {
                    doc.Status = PurchaseOutsourcingReturnStatusEnum.NotReceive;
                }
                else
                {
                    doc.Status = PurchaseOutsourcingReturnStatusEnum.PartReceive;
                }

                ((DataContext)DC).BulkInsert(newInventoryList);

                foreach (var inventory in newInventoryList)
                {
                    // 创建库存流水
                    if (!this.CreateInvLog(OperationTypeEnum.OutsourcingReturnReceive, doc.DocNo, null, inventory.ID, null, inventory.Qty))
                    {
                        return;
                    }
                }
                foreach (var item in lineIdToInvIdDict)
                {
                    // 创建单据库存关联关系
                    if (!this.CreateInvRelation(DocTypeEnum.OutsourcingReturn, item.Item2, doc.ID, item.Item1, item.Item3))  // 采购收货单收货操作
                    {
                        return;
                    }
                }

                DC.SaveChanges();
                trans.Commit();
            }
        }

        /// <summary>
        /// 取消收货（只有整单取消收货，不允许行取消收货）
        /// </summary>
        /// <param name="docId"></param>
        public void CancelReceive(Guid? docId)
        {
            if (docId == null || docId == Guid.Empty)
            {
                MSD.AddModelError("", "参数不能为空");
                return;
            }
            using (var trans = DC.BeginTransaction())
            {
                var doc = DC.Set<PurchaseOutsourcingReturn>().Include(x => x.PurchaseOutsourcingReturnLine_OutsourcingReturn).Where(x => x.ID == docId).FirstOrDefault();
                if (doc == null)
                {
                    MSD.AddModelError("", "单据不存在");
                    return;
                }
                if (doc.Status == PurchaseOutsourcingReturnStatusEnum.NotReceive)
                {
                    MSD.AddModelError("", "单据尚未进行收货业务，无法进行取消收货操作");
                    return;
                }
                if (doc.Status == PurchaseOutsourcingReturnStatusEnum.AllInWh || doc.Status == PurchaseOutsourcingReturnStatusEnum.PartInWh)
                {
                    MSD.AddModelError("", "单据已入库，无法进行取消收货操作");
                    return;
                }
                // 新增的库存信息集合（后面统一插入数据库）
                //List<BaseInventory> newInventoryList = new List<BaseInventory>();
                // 取消收货
                foreach (var line in doc.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                {
                    if (line.Status == PurchaseOutsourcingReturnLineStatusEnum.NotReceive)
                    {
                        //MSD.AddModelError("", $"行“{line.DocLineNo}”尚未进行收货业务，无法进行取消收货操作");
                        //return;
                        continue;
                    }
                    if (line.ReceiveQty <= 0)
                    {
                        //MSD.AddModelError("", $"行“{line.DocLineNo}”已收货数量为0，无需取消收货");
                        //return;
                        continue;
                    }
                    else if (line.Status == PurchaseOutsourcingReturnLineStatusEnum.PartInWh || line.Status == PurchaseOutsourcingReturnLineStatusEnum.AllInWh) // 不会出现。当前设置逻辑为必须一次性全部入库。单据头判断已经进行了过滤
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”已入库，无法进行取消收货操作");
                        return;
                    }
                    if (line.InWhQty > 0)
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”已入库，无法进行取消收货操作2");
                        return;
                    }
                    line.ToBeReceiveQty = line.ToBeReceiveQty + line.ToBeInWhQty;
                    line.ReceiveQty = line.ReceiveQty - line.ToBeInWhQty;
                    line.ToBeInWhQty = 0;
                    line.Status = PurchaseOutsourcingReturnLineStatusEnum.NotReceive;
                }
                // 设置单据状态
                if (doc.PurchaseOutsourcingReturnLine_OutsourcingReturn.All(x => x.Status == PurchaseOutsourcingReturnLineStatusEnum.AllReceive))   // 全部行已收货（取消收货动作不会出现这种情况）
                {
                    doc.Status = PurchaseOutsourcingReturnStatusEnum.AllReceive;
                }
                else if (doc.PurchaseOutsourcingReturnLine_OutsourcingReturn.All(x => x.Status == PurchaseOutsourcingReturnLineStatusEnum.NotReceive))  // 全部行未收货
                {
                    doc.Status = PurchaseOutsourcingReturnStatusEnum.NotReceive;
                }
                else
                {
                    doc.Status = PurchaseOutsourcingReturnStatusEnum.PartReceive;
                }
                // 查询单据库存关联关系
                var invRelations = DC.Set<BaseDocInventoryRelation>()
                    .Where(x => x.BusinessId == doc.ID && x.DocType == DocTypeEnum.OutsourcingReturn).ToList();

                foreach (var relation in invRelations)
                {
                    var inventory = DC.Set<BaseInventory>().Where(y => y.ID == relation.InventoryId).FirstOrDefault();
                    if (inventory == null || inventory.IsAbandoned == true)
                    {
                        // 删除单据库存关联关系
                        DC.DeleteEntity(relation);
                        continue;
                    }
                    // 创建库存流水
                    if (!this.CreateInvLog(OperationTypeEnum.OutsourcingReturnReceiveCancel, doc.DocNo, relation.InventoryId, null, -inventory.Qty, null))
                    {
                        return;
                    }
                    // 待收库位的库存信息清零
                    inventory.Qty = 0;
                    inventory.IsAbandoned = true;
                    inventory.UpdateBy = LoginUserInfo.ITCode;
                    inventory.UpdateTime = DateTime.Now;
                    // 删除单据库存关联关系
                    DC.DeleteEntity(relation);
                }
                DC.SaveChanges();
                trans.Commit();
            }
        }

        /// <summary>
        /// 审核并上架
        /// </summary>
        /// <param name="docId"></param>
        public void ApproveAndPutaway(PurchaseOutsourcingReturnApprovePara data)
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
                var doc = DC.Set<PurchaseOutsourcingReturn>()
                    .Include(x => x.PurchaseOutsourcingReturnLine_OutsourcingReturn)
                    .Include(x => x.Organization)
                    .Where(x => x.ID == data.ID)
                    .FirstOrDefault();
                if (doc == null)
                {
                    MSD.AddModelError("", "单据不存在");
                    return;
                }
                if (doc.Status != PurchaseOutsourcingReturnStatusEnum.AllReceive)
                {
                    MSD.AddModelError("", "单据未全部收货，无法进行审核上架操作");
                    return;
                }
                if (doc.Status == PurchaseOutsourcingReturnStatusEnum.AllInWh || doc.Status == PurchaseOutsourcingReturnStatusEnum.PartInWh)
                {
                    MSD.AddModelError("", "单据已入库，无法进行审核上架操作");
                    return;
                }
                string orgCode = doc.Organization.Code;

                // 获取关联的单据库存关联关系中的所有条码
                var invIds = DC.Set<BaseDocInventoryRelation>()
                    .Where(x => x.BusinessId == doc.ID && x.DocType == DocTypeEnum.OutsourcingReturn)
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
                    this.CreateInvLog(OperationTypeEnum.OutsourcingReturnApprove, doc.DocNo, oldInv.ID, newInv.ID, -newInv.Qty, newInv.Qty);
                }
                // 更新单据状态、行状态
                doc.Status = PurchaseOutsourcingReturnStatusEnum.AllInWh;
                doc.PurchaseOutsourcingReturnLine_OutsourcingReturn.ForEach(x =>
                {
                    x.Status = PurchaseOutsourcingReturnLineStatusEnum.AllInWh;
                    x.InWhQty = x.ToBeInWhQty;
                    x.ToBeInWhQty = 0;
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
                U9Return u9Return = apiHelper.ApproveOutsourcingOrReturn(doc.DocNo);
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
