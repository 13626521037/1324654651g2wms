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
using WMS.Model.InventoryManagement;
using WMS.Model.SalesManagement;
using WMS.Util;
using WMS.Util.U9Para;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;


namespace WMS.ViewModel.SalesManagement.SalesShipVMs
{
    public partial class SalesShipApiVM : BaseCRUDVM<SalesShip>
    {
        /// <summary>
        /// 自动拆码结果
        /// </summary>
        public List<InventorySplitSaveReturn> AutoSplitResult { get; set; }

        #region 原生代码

        public SalesShipApiVM()
        {
            SetInclude(x => x.Organization);
            SetInclude(x => x.Customer);
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
        /// <param name="type">0-待下架，1-待审核</param>
        /// <returns></returns>
        public List<SalesShipReturn> GetList(int type = 0)
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
            List<SalesShipReturn> list = new List<SalesShipReturn>();
            Expression<Func<SalesShip, bool>> queryable;
            if (type == 0)  // 待下架（未全部下架）。需匹配登录用户的存储地点
            {
                queryable = x => (x.Status == SalesShipStatusEnum.InWh
                    || x.Status == SalesShipStatusEnum.PartOff)
                    && x.SalesShipLine_Ship.Any(y => y.WareHouseId == whid);
            }
            else
            {
                queryable = x => x.Status == SalesShipStatusEnum.AllOff
                    && x.SalesShipLine_Ship.Any(y => y.WareHouseId == whid);
            }
            list = DC.Set<SalesShip>()
               .Where(queryable)
               .AsNoTracking()
               .Select(x => new SalesShipReturn
               {
                   ID = x.ID,
                   DocNo = x.DocNo,
                   DocType = x.DocType,
                   Status = x.Status.GetEnumDisplayName(),
                   BusinessDate = ((DateTime)x.BusinessDate).ToString("yyyy-MM-dd"),
                   Customer = x.Customer.Name,
                   SumQty = (int)(x.SalesShipLine_Ship.Sum(y => y.Qty) ?? 0),
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
        /// 创建单据（ERP调用）
        /// </summary>
        /// <param name="entity"></param>
        public void Create(SalesShip entity)
        {
            MSD.Clear();    // 看下是否需要判断
            var existEntity = DC.Set<SalesShip>().Where(x => x.DocNo == entity.DocNo).FirstOrDefault();
            if (existEntity != null)
            {
                MSD.AddModelError("", "单据编号已存在");
                return;
            }
            // 引用类型外部系统ID转换
            string r = this.SSIdAttrToId(entity, entity.Organization, "Organization");    // 组织
            if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
            r = this.SSIdAttrToId(entity, entity.Customer, "Customer");   // 供应商
            if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
            foreach (var line in entity.SalesShipLine_Ship)
            {
                r = this.SSIdAttrToId(line, line.ItemMaster, "ItemMaster");   // 转换物料
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
                r = this.SSIdAttrToId(line, line.WareHouse, "WareHouse");   // 转换存储地点
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
            }
            foreach (var line in entity.SalesShipLine_Ship)
            {
                line.Status = Model.SalesShipLineStatusEnum.InWh;
                line.ToBeOffQty = line.Qty;
                line.ToBeShipQty = 0;
                line.OffQty = 0;
                line.ShippedQty = 0;
            }
            // 将单据保存到本系统中
            Entity = entity;
            Entity.Status = Model.SalesShipStatusEnum.InWh;
            DoAdd();
        }

        /// <summary>
        /// 获取单据
        /// </summary>
        /// <param name="docNo">单据编号</param>
        public SalesShipReturn GetDoc(string docNo)
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

            SalesShip doc = DC.Set<SalesShip>()
                .Where(x => x.DocNo == docNo)
                .Include(x => x.Organization)
                .Include(x => x.Customer)
                //.Include(x => x.TransInWh)
                //.Include(x => x.TransOutOrganization)
                //.Include(x => x.TransOutWh)
                .Include(x => x.SalesShipLine_Ship)
                .ThenInclude(x => x.ItemMaster)
                .Include(x => x.SalesShipLine_Ship)
                .ThenInclude(x => x.WareHouse)
                .AsNoTracking().FirstOrDefault();
            if (doc != null)    // 单据在本系统中存在
            {
                // 判断发料存储地点是否匹配当前登录用户的存储地点
                if (doc.SalesShipLine_Ship.Any(y => y.WareHouseId != whid))
                {
                    MSD.AddModelError("", $"单据{docNo}的发料存储地点与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                // 判断单据状态：只有在库和部分下架的才允许操作
                if (doc.Status != SalesShipStatusEnum.InWh && doc.Status != SalesShipStatusEnum.PartOff)
                {
                    MSD.AddModelError("", $"当前单据状态“{doc.Status.GetEnumDisplayName()}”不允许进行下架操作");
                    return null;
                }
                return new SalesShipReturn(doc);
            }
            // 本系统不存在时，尝试从ERP获取
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<SalesShip> u9Return = apiHelper.GetSalesShip(docNo);
            if (u9Return.Success)
            {
                // 引用类型外部系统ID转换
                string r = this.SSIdAttrToId(u9Return.Entity, u9Return.Entity.Organization, "Organization");    // 组织
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                r = this.SSIdAttrToId(u9Return.Entity, u9Return.Entity.Customer, "Customer");   // 调出组织
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                foreach (var line in u9Return.Entity.SalesShipLine_Ship)
                {
                    r = this.SSIdAttrToId(line, line.ItemMaster, "ItemMaster");   // 转换物料
                    if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                    r = this.SSIdAttrToId(line, line.WareHouse, "WareHouse");   // 转换存储地点
                    if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                }
                // 判断调出存储地点是否匹配（转换完了才能进行匹配，不然没有值）
                if (u9Return.Entity.SalesShipLine_Ship.Any(y => y.WareHouseId != whid))
                {
                    MSD.AddModelError("", $"单据{docNo}的发料存储地点与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                foreach (var line in u9Return.Entity.SalesShipLine_Ship)
                {
                    line.Status = SalesShipLineStatusEnum.InWh;
                    line.ToBeOffQty = line.Qty;
                    line.ToBeShipQty = 0;
                    line.OffQty = 0;
                    line.ShippedQty = 0;
                }
                // 将单据保存到本系统中
                Entity = u9Return.Entity;
                Entity.Status = SalesShipStatusEnum.InWh;
                DoAdd();
                if (!MSD.IsValid)
                {
                    return null;
                }
                doc = DC.Set<SalesShip>()
                    .Where(x => x.DocNo == docNo)
                    .Include(x => x.Organization)
                    .Include(x => x.Customer)
                    .Include(x => x.SalesShipLine_Ship)
                    .ThenInclude(x => x.ItemMaster)
                    .AsNoTracking().FirstOrDefault();   // 要重新获取，否则有些外键实体会为null
                if (doc == null)
                {
                    MSD.AddModelError("", "单据已同步，但未能正常显示。请尝试退出后重试");    // 不会出现这种情况
                    return null;
                }
                return new SalesShipReturn(doc);
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
        public SalesShipReturn GetDocForApprove(string docNo)
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

            SalesShip doc = DC.Set<SalesShip>()
                .Where(x => x.DocNo == docNo)
                .Include(x => x.Organization)
                .Include(x => x.Customer)
                .Include(x => x.SalesShipLine_Ship)
                .ThenInclude(x => x.WareHouse)
                .Include(x => x.SalesShipLine_Ship)
                .ThenInclude(x => x.ItemMaster)
                .AsNoTracking().FirstOrDefault();
            if (doc != null)    // 单据在本系统中存在
            {
                // 判断行存储地点释放匹配当前登录用户的存储地点
                SalesShipLine line = doc.SalesShipLine_Ship.Find(x => x.WareHouseId != whid);
                if (line != null)
                {
                    // 存在行存储地点不匹配，返回错误信息
                    MSD.AddModelError("", $"单据{docNo}的存储地点为{line.WareHouse.Code}，与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                if (doc.Status != SalesShipStatusEnum.AllOff)
                {
                    MSD.AddModelError("", $"单据{docNo}的状态为{doc.Status.GetEnumDisplayName()}，无法进行审核");
                    return null;
                }
                SalesShipReturn result = new SalesShipReturn(doc);
                GetOffDetails(result);
                return result;
            }
            else
            {
                MSD.AddModelError("", "单据不存在");
                return null;
            }
        }

        /// <summary>
        /// 获取已下架明细（根据返回前端的单据格式）
        /// </summary>
        /// <param name="doc"></param>
        public void GetOffDetails(SalesShipReturn doc)
        {
            var invRelations = DC.Set<BaseDocInventoryRelation>().AsNoTracking().Include(x => x.Inventory.WhLocation.WhArea.WareHouse)
                .Where(x => x.BusinessId == doc.ID && x.DocType == DocTypeEnum.Ship).ToList();
            if (invRelations == null || invRelations.Count == 0)
            {
                return;
            }
            foreach (var line in doc.Lines)
            {
                line.SubLines = new List<SalesShipReturnSubLine>();
                invRelations.FindAll(x => x.BusinessLineId == line.ID).ForEach(x =>
                {
                    // 查找最近的库存流水，并获取来源库存信息
                    var invLog = DC.Set<BaseInventoryLog>().Where(y => y.TargetInventoryId == x.Inventory.ID && y.SourceInventoryId != null)
                        .Include(y => y.SourceInventory.WhLocation)
                        .OrderByDescending(y => y.CreateTime).FirstOrDefault();
                    if (invLog == null)
                    {
                        MSD.AddModelError("", $"“{x.Inventory.SerialNumber}”库存流水记录丢失，无法继续操作。请联系管理员"); // 正常不会出现
                        return;
                    }
                    var originalInv = invLog.SourceInventory;
                    // 根据序列号查找原库存信息
                    //var originalInv = DC.Set<BaseInventory>()
                    //    .Include(x => x.WhLocation)
                    //    .Where(y => y.SerialNumber == x.Inventory.SerialNumber && y.IsAbandoned == false
                    //        && y.WhLocation.WhArea.WareHouseId == x.Inventory.WhLocation.WhArea.WareHouseId && y.WhLocation.AreaType == WhLocationEnum.Normal)
                    //    .AsNoTracking()
                    //    .FirstOrDefault();
                    //if (originalInv == null)
                    //{
                    //    MSD.AddModelError("", $"“{x.Inventory.SerialNumber}”原库存信息不存在，无法继续操作。请联系管理员"); // 正常不会出现
                    //    return;
                    //}
                    line.SubLines.Add(new SalesShipReturnSubLine
                    {
                        // 最原始的库位，非当前实际所在的“待发库位”。仅做展示用，审核仅匹配序列号，与ID无关
                        InvID = originalInv.ID,
                        LocCode = originalInv.WhLocation.Code,
                        Batch = originalInv.BatchNumber,
                        SN = originalInv.SerialNumber,
                        TotalQty = (decimal)originalInv.Qty + (decimal)x.Qty,   // TotalQty有啥用？
                        OccupyQty = (decimal)x.Qty,
                    });
                });
            }
        }

        /// <summary>
        /// 获取推荐库位（根据返回前端的单据格式）
        /// </summary>
        /// <param name="doc"></param>
        public void GetSuggestLocs(SalesShipReturn doc)
        {
            foreach (var line in doc.Lines)
            {
                line.SuggestLocs = new List<SalesShipReturnLineSuggestLoc>();
                List<BaseInventory> invs = this.GetItemInventory(line.ItemID, line.WareHouseID, line.Seiban);   // 注意：成品考虑番号
                if (invs.Count > 0)
                {
                    // invs = invs.OrderBy(x => x.CreateTime).ToList(); // 默认升序
                    foreach (var inv in invs)
                    {
                        line.SuggestLocs.Add(new SalesShipReturnLineSuggestLoc
                        {
                            AreaCode = inv.WhLocation.WhArea.Code,
                            LocCode = inv.WhLocation.Code,
                            Sn = inv.SerialNumber,
                            Batch = inv.BatchNumber,
                            Qty = (decimal)inv.Qty,
                        });
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
            var entity = DC.Set<SalesShip>().Where(x => x.SourceSystemId == sourceSystemId).AsNoTracking().FirstOrDefault();
            if (entity == null)
            {
                return false;
            }
            else
            {
                if (entity.Status == SalesShipStatusEnum.InWh)
                {
                    MSD.AddModelError("", $"G2WMS中已存在此单据，请先删除G2WMS中的单据，再进行操作");
                    return true;
                }
                else
                {
                    MSD.AddModelError("", $"G2WMS中已存在此单据，且已有下架记录，U9单据禁止手动操作");
                    return true;
                }
            }
        }

        /// <summary>
        /// 保存下架明细（根据前端传入的单据格式）
        /// </summary>
        /// <param name="doc"></param>
        public void SaveOffDetails(SalesShipDataPara data, bool autoSplit)
        {
            AutoSplitResult = null;
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
                SalesShip doc = DC.Set<SalesShip>()             //.AsNoTracking()
                    .Include(x => x.SalesShipLine_Ship)
                    .Where(x => x.ID == data.ID)
                    .FirstOrDefault();
                if (doc == null)
                {
                    MSD.AddModelError("", "单据不存在");
                    return;
                }
                if (doc.SalesShipLine_Ship.Any(y => y.WareHouseId != whid))
                {
                    MSD.AddModelError("", "单据存储地点不匹配，可尝试重新选择登录存储地点");
                    return;
                }
                if (doc.Status != SalesShipStatusEnum.InWh && doc.Status != SalesShipStatusEnum.PartOff)
                {
                    MSD.AddModelError("", "单据已完成下架业务，无法继续进行下架操作");
                    return;
                }
                // 获取当前存储地点的待发库位
                BaseWhLocation location = DC.Set<BaseWhLocation>()
                    .Where(x => x.WhArea.WareHouseId == whid && x.AreaType == Model.WhLocationEnum.WaitForDeliver && x.IsEffective == EffectiveEnum.Effective)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (location == null)
                {
                    MSD.AddModelError("", "当前存储地点未配置待发库位，无法进行下架操作");
                    return;
                }

                List<BaseInventory> newInventoryList = new List<BaseInventory>();   // 新增的库存信息集合（后面统一插入数据库）
                List<Guid> sourceInvIds = new List<Guid>();  // 与newInventoryList变量一一对应，用来写库存流水时用
                Dictionary<Guid, (decimal, decimal, string)> invOccupyQtyDict = new Dictionary<Guid, (decimal, decimal, string)>();  // 库存占用（第一个数字是库存数量，第二个数字是本次占用数量，第三个字符串是序列号）
                List<(Guid, Guid, decimal)> lineIdToInvIdDict = new List<(Guid, Guid, decimal)>();  // 行ID与库存ID的对应关系（第一个Guid是行ID，第二个Guid是库存ID，第三个数字是本次下架数量）
                List<BaseInventory> originalInvs = new List<BaseInventory>();   // 将操作过的库存信息缓存起来，避免重复跟踪报错

                #region 自动拆码
                if (autoSplit)
                {
                    List<BasePickPara> paras = new List<BasePickPara>();
                    // 按序列号汇总总需求数量
                    foreach (var dataLine in data.Lines)
                    {
                        if (dataLine.SubLines != null && dataLine.SubLines.Count > 0)
                        {
                            foreach (var dataSubLine in dataLine.SubLines)
                            {
                                BasePickPara para = paras.Find(x => x.InvID == dataSubLine.InvID);
                                if (para == null)
                                {
                                    para = new BasePickPara();
                                    para.OffQty = dataSubLine.OffQty;
                                    para.InvID = dataSubLine.InvID;
                                    paras.Add(para);
                                }
                                else
                                {
                                    para.OffQty += dataSubLine.OffQty;
                                }
                            }
                        }
                    }
                    List<InventorySplitSaveReturn> rets = this.InvSplit(paras, doc.DocNo + " 下架自动拆分", trans); // 所有条码全部添加进来。无需拆码的条码直接返回原库存信息
                    if (!MSD.IsValid)
                    {
                        return;
                    }
                    if (rets != null && rets.Count > 0)
                    {
                        AutoSplitResult = rets;
                        foreach (var ret in rets)
                        {
                            foreach (var dataLine in data.Lines)
                            {
                                if (dataLine.SubLines != null && dataLine.SubLines.Count > 0)
                                {
                                    foreach (var dataSubLine in dataLine.SubLines)
                                    {
                                        // 按拆分前库存信息查找对应的子行，并更新为拆分后新的库存信息
                                        if (dataSubLine.InvID == ret.OldInvId)
                                        {
                                            dataSubLine.InvID = ret.NewInvId;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                // 循环行参数
                foreach (var dataLine in data.Lines)
                {
                    var line = doc.SalesShipLine_Ship.Find(x => x.ID == dataLine.ID);
                    if (line == null)
                    {
                        MSD.AddModelError("", $"行数据“{dataLine.ID}”不存在");
                        return;
                    }
                    if (line.Status != SalesShipLineStatusEnum.InWh && line.Status != SalesShipLineStatusEnum.PartOff)
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”已完成下架业务，无法进行下架操作");
                        return;
                    }
                    if (line.ToBeOffQty <= 0)
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”待下架数量为0，无需下架");
                        return;
                    }
                    if (dataLine.SubLines == null || dataLine.SubLines.Count == 0)
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”下架子行数据不能为空");
                        return;
                    }
                    decimal offQty = 0; // 行——本次下架数量
                    // 循环子行参数
                    foreach (var dataSubLine in dataLine.SubLines)
                    {
                        BaseInventory originalInv = originalInvs.Find(x => x.ID == dataSubLine.InvID);  // 先从已缓存的库存信息中查找
                        if (originalInv == null)
                        {
                            originalInv = DC.Set<BaseInventory>().Where(x => x.ID == dataSubLine.InvID && x.IsAbandoned == false).FirstOrDefault();
                            if (originalInv == null)
                            {
                                MSD.AddModelError("", $"行“{line.DocLineNo}”，子行“{dataSubLine.InvID}”库存信息不存在");
                                return;
                            }
                            if (originalInv.FrozenStatus == FrozenStatusEnum.Freezed)
                            {
                                MSD.AddModelError("", $"行“{line.DocLineNo}”，子行“{originalInv.SerialNumber}”库存已冻结，无法进行下架操作");
                                return;
                            }
                            var originalLoc = DC.Set<BaseWhLocation>()
                                .Include(x => x.WhArea)
                                .Where(x => x.ID == originalInv.WhLocationId)
                                .AsNoTracking()
                                .FirstOrDefault();
                            if (originalLoc != null)
                            {
                                if (originalLoc.AreaType != WhLocationEnum.Normal)
                                {
                                    MSD.AddModelError("", $"只允许从正常库位进行下架操作。行“{line.DocLineNo}”，下架明细中“{originalInv.SerialNumber}”所在的库位类型为“{originalLoc.AreaType.GetEnumDisplayName()}”，无法进行下架操作");
                                    return;
                                }
                                if (originalLoc.Locked == true)
                                {
                                    MSD.AddModelError("", $"行“{line.DocLineNo}”，下架明细中“{originalInv.SerialNumber}”所在的库位已被盘点锁定，无法进行下架操作");
                                    return;
                                }
                                if (originalLoc.WhArea.WareHouseId != whid)
                                {
                                    MSD.AddModelError("", $"下架明细中“{originalInv.SerialNumber}”所在的库位不属于当前登录的存储地点，无法进行下架操作");
                                    return;
                                }
                            }
                            originalInvs.Add(originalInv);
                        }
                        if (originalInv.ItemMasterId != line.ItemMasterId)
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”，子行“{originalInv.SerialNumber}”条码对应物料与行物料不匹配");
                            return;
                        }
                        //if (originalInv.DocNo != doc.DocNo)
                        //{
                        //    MSD.AddModelError("", $"行“{line.DocLineNo}”，子行“{originalInv.ID}”条码与单据不匹配");
                        //    return;
                        //}
                        // 使用字典对条码库存占用进行累加
                        if (invOccupyQtyDict.ContainsKey(originalInv.ID))
                        {
                            if (invOccupyQtyDict.TryGetValue(originalInv.ID, out (decimal, decimal, string) v))
                            {
                                invOccupyQtyDict[originalInv.ID] = (v.Item1, v.Item2 + dataSubLine.OffQty, v.Item3);
                            }
                        }
                        else
                        {
                            invOccupyQtyDict.Add(originalInv.ID, ((decimal)originalInv.Qty, dataSubLine.OffQty, originalInv.SerialNumber));
                        }
                        if (invOccupyQtyDict[originalInv.ID].Item1 < invOccupyQtyDict[originalInv.ID].Item2)
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”，“{originalInv.SerialNumber}”条码数量“{invOccupyQtyDict[originalInv.ID].Item1}”不能满足截止当前行{line.DocLineNo}的总需求数量“{invOccupyQtyDict[originalInv.ID].Item2}”");
                            return;
                        }
                        offQty += dataSubLine.OffQty;
                        originalInv.Qty -= dataSubLine.OffQty;
                        originalInv.UpdateBy = LoginUserInfo.ITCode;
                        originalInv.UpdateTime = DateTime.Now;
                        // 待收库位增加库存
                        if (newInventoryList.Any(x => x.SerialNumber == originalInv.SerialNumber))
                        {
                            var inventory = newInventoryList.Find(x => x.SerialNumber == originalInv.SerialNumber);
                            inventory.Qty += dataSubLine.OffQty;
                            lineIdToInvIdDict.Add((line.ID, inventory.ID, dataSubLine.OffQty));
                        }
                        else
                        {
                            newInventoryList.Add(new BaseInventory
                            {
                                ID = Guid.NewGuid(),
                                ItemMasterId = originalInv.ItemMasterId,
                                WhLocationId = location.ID,
                                BatchNumber = originalInv.BatchNumber,
                                SerialNumber = originalInv.SerialNumber,
                                Seiban = originalInv.Seiban,
                                SeibanRandom = originalInv.SeibanRandom,
                                Qty = dataSubLine.OffQty,
                                IsAbandoned = false,
                                ItemSourceType = originalInv.ItemSourceType,
                                FrozenStatus = FrozenStatusEnum.Normal,
                                CreateBy = LoginUserInfo.ITCode,
                                CreateTime = DateTime.Now
                            });
                            sourceInvIds.Add(originalInv.ID);   // 必须与newInventoryList同时增加。以保证对应关系
                            lineIdToInvIdDict.Add((line.ID, newInventoryList.Last().ID, dataSubLine.OffQty));
                        }
                    }

                    // 更新行已下架数量和待下架数量
                    if (offQty > line.ToBeOffQty)   // 本次总下架数量大于待下架数量
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”，总下架数量“{offQty}”大于待下架数量“{line.ToBeOffQty}”");
                    }
                    else
                    {
                        line.OffQty += offQty;
                        line.ToBeOffQty -= offQty;
                        line.ToBeShipQty = line.OffQty - line.ShippedQty;  // 待出货数量=已下架数量-已出货数量
                    }
                    // 设置行状态
                    if (line.ToBeOffQty == 0)
                    {
                        line.Status = SalesShipLineStatusEnum.AllOff;
                    }
                    else if (line.OffQty == 0)
                    {
                        line.Status = SalesShipLineStatusEnum.InWh;
                    }
                    else
                    {
                        line.Status = SalesShipLineStatusEnum.PartOff;
                    }
                }

                // 判断所有库存信息是否都已下架完。下机操作不允许库存有剩余（所有下架动作都需要进行这个判定）
                foreach (var inv in originalInvs)
                {
                    // 前端已做过校验。正常不会报这个错误
                    if (inv.Qty > 0)
                    {
                        MSD.AddModelError("", $"序列号：{inv.SerialNumber}对应的条码数量未全部下架。请检查数据");
                        return;
                    }
                    inv.IsAbandoned = true;
                }

                // 设置单据状态
                if (doc.SalesShipLine_Ship.All(x => x.Status == SalesShipLineStatusEnum.AllOff))   // 全部行已下架
                {
                    doc.Status = SalesShipStatusEnum.AllOff;
                }
                else if (doc.SalesShipLine_Ship.All(x => x.Status == SalesShipLineStatusEnum.InWh))  // 全部行未下架（下架动作不会出现这种情况）
                {
                    doc.Status = SalesShipStatusEnum.InWh;
                }
                else
                {
                    doc.Status = SalesShipStatusEnum.PartOff;
                }
                DC.SaveChanges();   // 2025-09-16 必须这里先保存。否则会库存信息的序列号触发唯一性索引错误（原库存信息的IsAbandoned设为true要先生效）

                ((DataContext)DC).BulkInsert(newInventoryList);

                for (int i = 0; i < newInventoryList.Count; i++)
                {
                    // 创建库存流水
                    if (!this.CreateInvLog(OperationTypeEnum.ShipOff, doc.DocNo, sourceInvIds[i], newInventoryList[i].ID, -newInventoryList[i].Qty, newInventoryList[i].Qty))
                    {
                        return;
                    }
                }
                foreach (var item in lineIdToInvIdDict)
                {
                    // 创建单据库存关联关系
                    if (!this.CreateInvRelation(DocTypeEnum.Ship, item.Item2, doc.ID, item.Item1, item.Item3))
                    {
                        return;
                    }
                }

                DC.SaveChanges();
                trans.Commit();
            }
        }

        /// <summary>
        /// 取消下架（只允许整单取消）
        /// </summary>
        /// <param name="docId"></param>
        public void CancelOff(Guid? docId)
        {
            if (docId == null || docId == Guid.Empty)
            {
                MSD.AddModelError("", "参数不能为空");
                return;
            }
            try
            {
                using (var trans = DC.BeginTransaction())
                {
                    var doc = DC.Set<SalesShip>().Include(x => x.SalesShipLine_Ship).Where(x => x.ID == docId).FirstOrDefault();
                    if (doc == null)
                    {
                        MSD.AddModelError("", "单据不存在");
                        return;
                    }
                    if (doc.Status == SalesShipStatusEnum.InWh)
                    {
                        MSD.AddModelError("", "单据尚未进行下架业务，无法进行取消下架操作");
                        return;
                    }
                    if (doc.Status == SalesShipStatusEnum.PartShipped || doc.Status == SalesShipStatusEnum.AllShipped)
                    {
                        MSD.AddModelError("", "单据已出货，无法进行取消下架操作");
                        return;
                    }
                    // 取消下架
                    foreach (var line in doc.SalesShipLine_Ship)
                    {
                        if (line.Status == SalesShipLineStatusEnum.InWh)
                        {
                            continue;
                        }
                        if (line.OffQty <= 0)
                        {
                            continue;
                        }
                        else if (line.Status == SalesShipLineStatusEnum.PartShipped || line.Status == SalesShipLineStatusEnum.AllShipped) // 头部已过滤，不会出现这种情况
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”已出库，无法进行取消下架操作");
                            return;
                        }
                        if (line.ShippedQty > 0)
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”已出库，无法进行取消下架操作2");
                            return;
                        }
                        line.ToBeOffQty = line.ToBeOffQty + line.ToBeShipQty;
                        line.OffQty = line.OffQty - line.ToBeShipQty;
                        line.ToBeShipQty = 0;
                        line.Status = SalesShipLineStatusEnum.InWh;
                    }
                    // 设置单据状态
                    if (doc.SalesShipLine_Ship.All(x => x.Status == SalesShipLineStatusEnum.AllOff))   // 全部行已下架（取消下架动作不会出现这种情况）
                    {
                        doc.Status = SalesShipStatusEnum.AllOff;
                    }
                    else if (doc.SalesShipLine_Ship.All(x => x.Status == SalesShipLineStatusEnum.InWh))  // 全部行未下架
                    {
                        doc.Status = SalesShipStatusEnum.InWh;
                    }
                    else
                    {
                        doc.Status = SalesShipStatusEnum.PartOff;
                    }
                    // 查询单据库存关联关系
                    var invRelations = DC.Set<BaseDocInventoryRelation>()
                        .Where(x => x.BusinessId == doc.ID && x.DocType == DocTypeEnum.Ship).ToList();

                    BaseWhLocation location = null;
                    foreach (var relation in invRelations)
                    {
                        var inventory = DC.Set<BaseInventory>().Where(y => y.ID == relation.InventoryId).FirstOrDefault();
                        if (inventory == null || inventory.IsAbandoned == true)
                        {
                            // 删除单据库存关联关系
                            DC.DeleteEntity(relation);
                            continue;
                        }
                        if (location == null)
                        {
                            location = DC.Set<BaseWhLocation>().Include(x => x.WhArea.WareHouse).Where(x => x.ID == inventory.WhLocationId).AsNoTracking().FirstOrDefault();
                            if (location == null)
                            {
                                MSD.AddModelError("", $"库位“{inventory.WhLocationId}”不存在，无法进行取消下架操作。请联系管理员");
                                return;
                            }
                            if (location.Locked == true)
                            {
                                MSD.AddModelError("", $"库位“{location.Code}”已被盘点锁定，无法进行取消下架操作。请联系管理员");
                                return;
                            }
                        }
                        // 查找最近的库存流水，并获取来源库存信息
                        var invLog = DC.Set<BaseInventoryLog>().Where(x => x.TargetInventoryId == inventory.ID && x.SourceInventoryId != null)
                            .Include(x => x.SourceInventory)
                            .OrderByDescending(x => x.CreateTime).FirstOrDefault();
                        if (invLog == null)
                        {
                            MSD.AddModelError("", $"“{inventory.SerialNumber}”库存流水记录丢失，无法进行取消下架操作。请联系管理员"); // 正常不会出现
                            return;
                        }
                        var originalInv = invLog.SourceInventory;

                        // 创建库存流水
                        if (!this.CreateInvLog(OperationTypeEnum.ShipOffCancel, doc.DocNo, relation.InventoryId, originalInv.ID, -inventory.Qty, inventory.Qty))
                        {
                            return;
                        }
                        // 待发库位的库存信息清零
                        decimal qty = (decimal)inventory.Qty;   // 备份数量
                        inventory.Qty = 0;
                        inventory.IsAbandoned = true;
                        inventory.UpdateBy = LoginUserInfo.ITCode;
                        inventory.UpdateTime = DateTime.Now;
                        DC.SaveChanges();

                        // 原库存数量增加
                        originalInv.IsAbandoned = false;
                        originalInv.Qty += qty;
                        originalInv.UpdateBy = LoginUserInfo.ITCode;
                        originalInv.UpdateTime = DateTime.Now;

                        // 删除单据库存关联关系
                        DC.DeleteEntity(relation);
                    }
                    DC.SaveChanges();
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                MSD.AddModelError("", "操作失败，" + ex.Message);
            }
        }

        /// <summary>
        /// 审核操作
        /// </summary>
        public void Approve(Guid? docId)
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
            if (docId == null || docId == Guid.Empty)
            {
                MSD.AddModelError("", "参数不能为空");
                return;
            }
            using (var trans = DC.BeginTransaction())
            {
                var doc = DC.Set<SalesShip>().Include(x => x.SalesShipLine_Ship).Include(x => x.Organization).Where(x => x.ID == docId).FirstOrDefault();
                if (doc == null)
                {
                    MSD.AddModelError("", "单据不存在");
                    return;
                }
                if (doc.Status != SalesShipStatusEnum.AllOff)
                {
                    MSD.AddModelError("", $"单据当前状态“{doc.Status.GetEnumDisplayName()}”不允许进行审核操作");
                    return;
                }
                if (doc.Status == SalesShipStatusEnum.PartShipped || doc.Status == SalesShipStatusEnum.AllShipped)
                {
                    MSD.AddModelError("", "单据已出货，无法进行审核操作");
                    return;
                }
                var org = doc.Organization;
                string orgCode = doc.Organization.Code;
                List<BaseDocInventoryRelation> invRelations = DC.Set<BaseDocInventoryRelation>()
                    .Where(x => x.BusinessId == doc.ID && x.DocType == DocTypeEnum.Ship).ToList();
                List<ApproveShipLinePara> lineParas = new();  // 审核时的行参数
                // 审核操作
                foreach (var line in doc.SalesShipLine_Ship)
                {
                    line.Status = SalesShipLineStatusEnum.AllShipped;
                    line.ShippedQty = line.OffQty;
                    line.ToBeShipQty = 0;
                    line.UpdateBy = LoginUserInfo.ITCode;
                    line.UpdateTime = DateTime.Now;

                    var matchRelations = invRelations.FindAll(x => x.BusinessLineId == line.ID).ToList();
                    foreach (var relation in matchRelations)
                    {
                        var inventory = DC.Set<BaseInventory>().Where(y => y.ID == relation.InventoryId).FirstOrDefault();
                        if (inventory == null || inventory.IsAbandoned == true)
                        {
                            continue;
                        }
                        
                        // 创建库存流水
                        this.CreateInvLog(OperationTypeEnum.ShipApprove, doc.DocNo, inventory.ID, null, -relation.Qty, null);
                        inventory.Qty -= relation.Qty;
                        inventory.IsAbandoned = true;
                        inventory.UpdateBy = LoginUserInfo.ITCode;
                        inventory.UpdateTime = DateTime.Now;

                        lineParas.Add(new ApproveShipLinePara
                        {
                            DocLineNo = (int)line.DocLineNo,
                            SeibanCode = inventory.Seiban,
                            SaleQty = (decimal)relation.Qty
                        });
                    }
                }
                doc.Status = SalesShipStatusEnum.AllShipped;
                doc.UpdateBy = LoginUserInfo.ITCode;
                doc.UpdateTime = DateTime.Now;
                DC.SaveChanges();
                doc.Organization = org;
                // 调用U9接口
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, orgCode, LoginUserInfo.Name);
                ApproveShipPara para = new ApproveShipPara
                {
                    docNo = doc.DocNo,
                    orgCode = doc.Organization.Code,
                    shipLineDTOList = lineParas,
                };
                U9Return u9Return = apiHelper.ApproveShip(para);
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
