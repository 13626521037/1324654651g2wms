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
using WMS.Model.ProductionManagement;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseBarCodeVMs;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;


namespace WMS.ViewModel.ProductionManagement.ProductionIssueVMs
{
    public partial class ProductionIssueApiVM : BaseCRUDVM<ProductionIssue>
    {
        /// <summary>
        /// 自动拆码结果
        /// </summary>
        public List<InventorySplitSaveReturn> AutoSplitResult { get; set; }

        #region 原生代码
        public ProductionIssueApiVM()
        {
            SetInclude(x => x.Organization);
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
        public List<ProductionIssueReturn> GetList(int type = 0)
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
            List<ProductionIssueReturn> list = new List<ProductionIssueReturn>();
            Expression<Func<ProductionIssue, bool>> queryable;
            if (type == 0)  // 待下架（未全部下架）。需匹配登录用户的存储地点
            {
                queryable = x => (x.Status == ProductionIssueStatusEnum.InWh
                    || x.Status == ProductionIssueStatusEnum.PartOff)
                    && x.ProductionIssueLine_ProductionIssue.Any(y => y.WhId == whid);
            }
            else
            {
                queryable = x => x.Status == ProductionIssueStatusEnum.AllOff
                    && x.ProductionIssueLine_ProductionIssue.Any(y => y.WhId == whid);
            }
            list = DC.Set<ProductionIssue>()
               .Where(queryable)
               .AsNoTracking()
               .Select(x => new ProductionIssueReturn
               {
                   ID = x.ID,
                   DocNo = x.DocNo,
                   DocType = x.DocType,
                   Status = x.Status.GetEnumDisplayName(),
                   BusinessDate = ((DateTime)x.BusinessDate).ToString("yyyy-MM-dd"),
                   SumQty = (int)(x.ProductionIssueLine_ProductionIssue.Sum(y => y.Qty) ?? 0),
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
        /// 判断单据是否存在
        /// </summary>
        /// <param name="sourceSystemId">来源系统单据ID</param>
        /// <returns></returns>
        public bool IsDocExist(string sourceSystemId)
        {
            var entity = DC.Set<ProductionIssue>().Where(x => x.SourceSystemId == sourceSystemId).AsNoTracking().FirstOrDefault();
            if (entity == null)
            {
                return false;
            }
            else
            {
                if (entity.Status == Model.ProductionIssueStatusEnum.InWh)
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
        /// 创建单据（ERP调用）
        /// </summary>
        /// <param name="entity"></param>
        public void Create(ProductionIssue entity)
        {
            MSD.Clear();    // 看下是否需要判断
            var existEntity = DC.Set<ProductionIssue>().Where(x => x.DocNo == entity.DocNo).FirstOrDefault();
            if (existEntity != null)
            {
                MSD.AddModelError("", "单据编号已存在");
                return;
            }
            // 引用类型外部系统ID转换
            string r = this.SSIdAttrToId(entity, entity.Organization, "Organization");    // 组织
            if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
            foreach (var line in entity.ProductionIssueLine_ProductionIssue)
            {
                r = this.SSIdAttrToId(line, line.ItemMaster, "ItemMaster");   // 转换物料
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
                r = this.SSIdAttrToId(line, line.Wh, "Wh");   // 转换存储地点
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
            }
            foreach (var line in entity.ProductionIssueLine_ProductionIssue)
            {
                line.Status = Model.ProductionIssueLineStatusEnum.InWh;
                line.ToBeOffQty = line.Qty;
                line.ToBeShipQty = 0;
                line.OffQty = 0;
                line.ShippedQty = 0;
            }
            // 将单据保存到本系统中
            Entity = entity;
            Entity.Status = Model.ProductionIssueStatusEnum.InWh;
            DoAdd();
        }

        /// <summary>
        /// 获取单据
        /// </summary>
        /// <param name="docNo">单据编号</param>
        public ProductionIssueReturn GetDoc(string docNo)
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

            ProductionIssue doc = DC.Set<ProductionIssue>()
                .Where(x => x.DocNo == docNo)
                .Include(x => x.Organization)
                //.Include(x => x.TransInWh)
                //.Include(x => x.TransOutOrganization)
                //.Include(x => x.TransOutWh)
                .Include(x => x.ProductionIssueLine_ProductionIssue)
                .ThenInclude(x => x.ItemMaster)
                .Include(x => x.ProductionIssueLine_ProductionIssue)
                .ThenInclude(x => x.Wh)
                .AsNoTracking().FirstOrDefault();
            if (doc != null)    // 单据在本系统中存在
            {
                // 判断发料存储地点是否匹配当前登录用户的存储地点
                if (doc.ProductionIssueLine_ProductionIssue.Any(y => y.WhId != whid))
                {
                    MSD.AddModelError("", $"单据{docNo}的发料存储地点与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                // 判断单据状态：只有在库和部分下架的才允许操作
                if (doc.Status != ProductionIssueStatusEnum.InWh && doc.Status != ProductionIssueStatusEnum.PartOff)
                {
                    MSD.AddModelError("", $"当前单据状态“{doc.Status.GetEnumDisplayName()}”不允许进行下架操作");
                    return null;
                }
                return new ProductionIssueReturn(doc);
            }
            // 本系统不存在时，尝试从ERP获取
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<ProductionIssue> u9Return = apiHelper.GetProductionIssue(docNo);
            if (u9Return.Success)
            {
                // 引用类型外部系统ID转换
                string r = this.SSIdAttrToId(u9Return.Entity, u9Return.Entity.Organization, "Organization");    // 组织
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                foreach (var line in u9Return.Entity.ProductionIssueLine_ProductionIssue)
                {
                    r = this.SSIdAttrToId(line, line.ItemMaster, "ItemMaster");   // 转换物料
                    if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                    r = this.SSIdAttrToId(line, line.Wh, "Wh");   // 转换存储地点
                    if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                }
                // 判断调出存储地点是否匹配（转换完了才能进行匹配，不然没有值）
                if (u9Return.Entity.ProductionIssueLine_ProductionIssue.Any(y => y.WhId != whid))
                {
                    MSD.AddModelError("", $"单据{docNo}的发料存储地点与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                foreach (var line in u9Return.Entity.ProductionIssueLine_ProductionIssue)
                {
                    line.Status = ProductionIssueLineStatusEnum.InWh;
                    line.ToBeOffQty = line.Qty;
                    line.ToBeShipQty = 0;
                    line.OffQty = 0;
                    line.ShippedQty = 0;
                }
                // 将单据保存到本系统中
                Entity = u9Return.Entity;
                Entity.Status = ProductionIssueStatusEnum.InWh;
                DoAdd();
                if (!MSD.IsValid)
                {
                    return null;
                }
                doc = DC.Set<ProductionIssue>()
                    .Where(x => x.DocNo == docNo)
                    .Include(x => x.Organization)
                    .Include(x => x.ProductionIssueLine_ProductionIssue)
                    .ThenInclude(x => x.ItemMaster)
                    .AsNoTracking().FirstOrDefault();   // 要重新获取，否则有些外键实体会为null
                if (doc == null)
                {
                    MSD.AddModelError("", "单据已同步，但未能正常显示。请尝试退出后重试");    // 不会出现这种情况
                    return null;
                }
                return new ProductionIssueReturn(doc);
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
        public ProductionIssueReturn GetDocForApprove(string docNo)
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

            ProductionIssue doc = DC.Set<ProductionIssue>()
                .Where(x => x.DocNo == docNo)
                .Include(x => x.Organization)
                .Include(x => x.ProductionIssueLine_ProductionIssue)
                .ThenInclude(x => x.ItemMaster)
                .AsNoTracking().FirstOrDefault();
            if (doc != null)    // 单据在本系统中存在
            {
                // 判断存储地点是否匹配当前登录用户的存储地点
                if (doc.ProductionIssueLine_ProductionIssue.Any(y => y.WhId != whid))
                {
                    MSD.AddModelError("", $"单据{docNo}的发料存储地点与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                if (doc.Status != ProductionIssueStatusEnum.AllOff)
                {
                    MSD.AddModelError("", $"单据{docNo}的状态为{doc.Status.GetEnumDisplayName()}，无法进行审核");
                    return null;
                }
                ProductionIssueReturn result = new ProductionIssueReturn(doc);
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
        public void GetOffDetails(ProductionIssueReturn doc)
        {
            var invRelations = DC.Set<BaseDocInventoryRelation>().AsNoTracking().Include(x => x.Inventory.WhLocation.WhArea.WareHouse)
                .Where(x => x.BusinessId == doc.ID && x.DocType == DocTypeEnum.ProductionIssue).ToList();
            if (invRelations == null || invRelations.Count == 0)
            {
                return;
            }
            foreach (var line in doc.Lines)
            {
                line.SubLines = new List<ProductionIssueReturnSubLine>();
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
                    line.SubLines.Add(new ProductionIssueReturnSubLine
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
        public void GetSuggestLocs(ProductionIssueReturn doc)
        {
            foreach (var line in doc.Lines)
            {
                line.SuggestLocs = new List<ProductionIssueReturnLineSuggestLoc>();
                List<BaseInventory> invs = this.GetItemInventory(line.ItemID, line.WhId);
                if (invs.Count > 0)
                {
                    // invs = invs.OrderBy(x => x.CreateTime).ToList(); // 默认升序
                    foreach (var inv in invs)
                    {
                        line.SuggestLocs.Add(new ProductionIssueReturnLineSuggestLoc
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
        /// 保存下架明细
        /// </summary>
        /// <param name="doc"></param>
        public void SaveOffDetails(ProductionIssueSaveOffPara data, bool autoSplit)
        {
            AutoSplitResult = null;
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            BaseWareHouse wh = DC.Set<BaseWareHouse>().Include(x => x.Organization).Where(x => x.ID == whid).AsNoTracking().FirstOrDefault();
            if (wh == null)
            {
                MSD.AddModelError("", "当前存储地点无效");
                return;
            }
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
                try
                {
                    ProductionIssue doc = DC.Set<ProductionIssue>()             //.AsNoTracking()
                        .Include(x => x.ProductionIssueLine_ProductionIssue)
                        .Where(x => x.ID == data.ID)
                        .FirstOrDefault();
                    if (doc == null)
                    {
                        MSD.AddModelError("", "单据不存在");
                        return;
                    }
                    if (doc.ProductionIssueLine_ProductionIssue[0].WhId != whid)
                    {
                        MSD.AddModelError("", "单据存储地点不匹配，可尝试重新选择登录存储地点");
                        return;
                    }
                    if (doc.Status != ProductionIssueStatusEnum.InWh && doc.Status != ProductionIssueStatusEnum.PartOff)
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
                    Dictionary<BaseInventory, decimal> invsQty = new Dictionary<BaseInventory, decimal>();  // 库存ID和数量的字典（创建条码时使用）

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
                        var docline = doc.ProductionIssueLine_ProductionIssue.Find(x => x.ID == dataLine.ID);
                        if (docline == null)
                        {
                            MSD.AddModelError("", $"行数据“{dataLine.ID}”不存在");
                            return;
                        }
                        if (docline.Status != ProductionIssueLineStatusEnum.InWh && docline.Status != ProductionIssueLineStatusEnum.PartOff)
                        {
                            MSD.AddModelError("", $"行“{docline.DocLineNo}”已完成下架业务，无法进行下架操作");
                            return;
                        }
                        if (docline.ToBeOffQty <= 0)
                        {
                            MSD.AddModelError("", $"行“{docline.DocLineNo}”待下架数量为0，无需下架");
                            return;
                        }
                        if (dataLine.SubLines == null || dataLine.SubLines.Count == 0)
                        {
                            MSD.AddModelError("", $"行“{docline.DocLineNo}”下架子行数据不能为空");
                            return;
                        }
                        decimal offQty = 0; // 行——本次下架数量
                                            // 循环子行参数
                        foreach (var dataSubLine in dataLine.SubLines)
                        {
                            BaseInventory originalInv = originalInvs.Find(x => x.ID == dataSubLine.InvID);  // 先从已缓存的库存信息中查找
                            if (originalInv == null)
                            {
                                originalInv = DC.Set<BaseInventory>().Include(x => x.ItemMaster.StockUnit).Where(x => x.ID == dataSubLine.InvID && x.IsAbandoned == false).FirstOrDefault();
                                if (originalInv == null)
                                {
                                    MSD.AddModelError("", $"行“{docline.DocLineNo}”，子行“{dataSubLine.InvID}”库存信息不存在");
                                    return;
                                }
                                if (originalInv.FrozenStatus == FrozenStatusEnum.Freezed)
                                {
                                    MSD.AddModelError("", $"行“{docline.DocLineNo}”，子行“{originalInv.SerialNumber}”库存已冻结，无法进行下架操作");
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
                                        MSD.AddModelError("", $"只允许从正常库位进行下架操作。行“{docline.DocLineNo}”，下架明细中“{originalInv.SerialNumber}”所在的库位类型为“{originalLoc.AreaType.GetEnumDisplayName()}”，无法进行下架操作");
                                        return;
                                    }
                                    if (originalLoc.Locked == true)
                                    {
                                        MSD.AddModelError("", $"行“{docline.DocLineNo}”，下架明细中“{originalInv.SerialNumber}”所在的库位已被盘点锁定，无法进行下架操作");
                                        return;
                                    }
                                    if (originalLoc.WhArea.WareHouseId != whid)
                                    {
                                        MSD.AddModelError("", $"下架明细中“{originalInv.SerialNumber}”所在的库位不属于当前登录的存储地点，无法进行下架操作");
                                        return;
                                    }
                                }
                                originalInvs.Add(originalInv);
                                invsQty.Add(originalInv, (decimal)originalInv.Qty);
                            }
                            if (originalInv.ItemMasterId != docline.ItemMasterId)
                            {
                                MSD.AddModelError("", $"行“{docline.DocLineNo}”，子行“{originalInv.SerialNumber}”条码对应物料与行物料不匹配");
                                return;
                            }
                            //if (originalInv.DocNo != doc.DocNo)
                            //{
                            //    MSD.AddModelError("", $"行“{docline.DocLineNo}”，子行“{originalInv.ID}”条码与单据不匹配");
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
                                MSD.AddModelError("", $"行“{docline.DocLineNo}”，“{originalInv.SerialNumber}”条码数量“{invOccupyQtyDict[originalInv.ID].Item1}”不能满足截止当前行{docline.DocLineNo}的总需求数量“{invOccupyQtyDict[originalInv.ID].Item2}”");
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
                                lineIdToInvIdDict.Add((docline.ID, inventory.ID, dataSubLine.OffQty));
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
                                lineIdToInvIdDict.Add((docline.ID, newInventoryList.Last().ID, dataSubLine.OffQty));
                            }
                        }

                        // 更新行已下架数量和待下架数量
                        if (offQty > docline.ToBeOffQty)   // 本次总下架数量大于待下架数量
                        {
                            MSD.AddModelError("", $"行“{docline.DocLineNo}”，总下架数量“{offQty}”大于待下架数量“{docline.ToBeOffQty}”");
                        }
                        else
                        {
                            docline.OffQty += offQty;
                            docline.ToBeOffQty -= offQty;
                            docline.ToBeShipQty = docline.OffQty - docline.ShippedQty;  // 待出货数量=已下架数量-已出货数量
                        }
                        // 设置行状态
                        if (docline.ToBeOffQty == 0)
                        {
                            docline.Status = ProductionIssueLineStatusEnum.AllOff;
                        }
                        else if (docline.OffQty == 0)
                        {
                            docline.Status = ProductionIssueLineStatusEnum.InWh;
                        }
                        else
                        {
                            docline.Status = ProductionIssueLineStatusEnum.PartOff;
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

                    // 处理条码（做委外退料时会用到）
                    BaseBarCodeVM barCodeVM = Wtm.CreateVM<BaseBarCodeVM>();
                    // List<LsWmsBarCodePara> paras = new List<LsWmsBarCodePara>();
                    foreach (var inv in originalInvs)
                    {
                        // 判断条码中是否存在此序列号，如果不存在，则新增。存在，则更新番号、批号
                        BaseBarCode barCode = DC.Set<BaseBarCode>().Where(x => x.Sn == inv.SerialNumber).FirstOrDefault();
                        if (barCode == null)
                        {
                            barCodeVM.CreateBarCode(true, inv.ItemMaster.Code, invsQty[inv], orgCode: wh.Organization.Code, sourceType: (int)inv.ItemSourceType, barCode: inv.SerialNumber, seiban: inv.Seiban);
                        }
                        else
                        {
                            barCode.Seiban = inv.Seiban;
                            barCode.SeibanRandom = inv.SeibanRandom;
                            barCode.BatchNumber = inv.BatchNumber;
                            barCode.Qty = invsQty[inv];
                        }
                        //paras.Add(new LsWmsBarCodePara(doc.DocNo, inv.ItemMaster.Code, inv.ItemMaster.Name, inv.ItemMaster.SPECS, invsQty[inv], inv.SerialNumber
                        //        , wh.Organization.Code, wh.Organization.Name, inv.ItemMaster.StockUnit.Name, wh.Code, wh.Name, inv.Seiban, inv.SeibanRandom));
                    }
                    // 将条码传链溯WMS系统
                    // LsWmsHelper.WriteBarCode(paras);

                    // 设置单据状态
                    if (doc.ProductionIssueLine_ProductionIssue.All(x => x.Status == ProductionIssueLineStatusEnum.AllOff))   // 全部行已下架
                    {
                        doc.Status = ProductionIssueStatusEnum.AllOff;
                    }
                    else if (doc.ProductionIssueLine_ProductionIssue.All(x => x.Status == ProductionIssueLineStatusEnum.InWh))  // 全部行未下架（下架动作不会出现这种情况）
                    {
                        doc.Status = ProductionIssueStatusEnum.InWh;
                    }
                    else
                    {
                        doc.Status = ProductionIssueStatusEnum.PartOff;
                    }
                    DC.SaveChanges();
                    ((DataContext)DC).BulkInsert(newInventoryList);

                    for (int i = 0; i < newInventoryList.Count; i++)
                    {
                        // 创建库存流水
                        if (!this.CreateInvLog(OperationTypeEnum.ProductionIssueOff, doc.DocNo, sourceInvIds[i], newInventoryList[i].ID, -newInventoryList[i].Qty, newInventoryList[i].Qty))
                        {
                            return;
                        }
                    }
                    foreach (var item in lineIdToInvIdDict)
                    {
                        // 创建单据库存关联关系
                        if (!this.CreateInvRelation(DocTypeEnum.ProductionIssue, item.Item2, doc.ID, item.Item1, item.Item3))
                        {
                            return;
                        }
                    }

                    DC.SaveChanges();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    MSD.AddModelError("", $"保存出错，原因:{ex.Message}");
                    return;
                }
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
                    var doc = DC.Set<ProductionIssue>().Include(x => x.ProductionIssueLine_ProductionIssue).Where(x => x.ID == docId).FirstOrDefault();
                    if (doc == null)
                    {
                        MSD.AddModelError("", "单据不存在");
                        return;
                    }
                    if (doc.Status == ProductionIssueStatusEnum.InWh)
                    {
                        MSD.AddModelError("", "单据尚未进行下架业务，无法进行取消下架操作");
                        return;
                    }
                    if (doc.Status == ProductionIssueStatusEnum.PartShipped || doc.Status == ProductionIssueStatusEnum.AllShipped)
                    {
                        MSD.AddModelError("", "单据已出货，无法进行取消下架操作");
                        return;
                    }
                    // 取消下架
                    foreach (var line in doc.ProductionIssueLine_ProductionIssue)
                    {
                        if (line.Status == ProductionIssueLineStatusEnum.InWh)
                        {
                            continue;
                        }
                        if (line.OffQty <= 0)
                        {
                            continue;
                        }
                        else if (line.Status == ProductionIssueLineStatusEnum.PartShipped || line.Status == ProductionIssueLineStatusEnum.AllShipped) // 头部已过滤，不会出现这种情况
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
                        line.Status = ProductionIssueLineStatusEnum.InWh;
                    }
                    // 设置单据状态
                    if (doc.ProductionIssueLine_ProductionIssue.All(x => x.Status == ProductionIssueLineStatusEnum.AllOff))   // 全部行已下架（取消下架动作不会出现这种情况）
                    {
                        doc.Status = ProductionIssueStatusEnum.AllOff;
                    }
                    else if (doc.ProductionIssueLine_ProductionIssue.All(x => x.Status == ProductionIssueLineStatusEnum.InWh))  // 全部行未下架
                    {
                        doc.Status = ProductionIssueStatusEnum.InWh;
                    }
                    else
                    {
                        doc.Status = ProductionIssueStatusEnum.PartOff;
                    }
                    // 查询单据库存关联关系
                    var invRelations = DC.Set<BaseDocInventoryRelation>()
                        .Where(x => x.BusinessId == doc.ID && x.DocType == DocTypeEnum.ProductionIssue).ToList();

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
                        if (!this.CreateInvLog(OperationTypeEnum.ProductionIssueOffCancel, doc.DocNo, relation.InventoryId, originalInv.ID, -inventory.Qty, inventory.Qty))
                        {
                            return;
                        }
                        // 待发库位的库存信息清零
                        decimal qty = (decimal)inventory.Qty;   // 备份数量
                        inventory.Qty = 0;
                        inventory.IsAbandoned = true;
                        inventory.UpdateBy = LoginUserInfo.ITCode;
                        inventory.UpdateTime = DateTime.Now;
                        DC.SaveChanges();   // 弃用的情况必须先保存。否则会触发“序列号”带条件的（IsAbandoned=1）唯一性约束。

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
                MSD.AddModelError("", $"操作出错，原因:{ex.Message}");
                return;
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
                var doc = DC.Set<ProductionIssue>().Include(x => x.ProductionIssueLine_ProductionIssue).ThenInclude(x => x.ItemMaster).Include(x => x.Organization).Where(x => x.ID == docId).FirstOrDefault();
                if (doc == null)
                {
                    MSD.AddModelError("", "单据不存在");
                    return;
                }
                if (doc.Status != ProductionIssueStatusEnum.AllOff)
                {
                    MSD.AddModelError("", $"单据当前状态“{doc.Status.GetEnumDisplayName()}”不允许进行审核操作");
                    return;
                }
                if (doc.Status == ProductionIssueStatusEnum.PartShipped || doc.Status == ProductionIssueStatusEnum.AllShipped)
                {
                    MSD.AddModelError("", "单据已出货，无法进行审核操作");
                    return;
                }
                // 传U9接口的参数
                //ApprovePurchaseOutsourcingIssuePara para = new ApprovePurchaseOutsourcingIssuePara
                //{
                //    docNo = doc.DocNo,
                //    businessId = doc.ID,
                //};
                string orgCode = doc.Organization.Code;
                List<BaseDocInventoryRelation> invRelations = DC.Set<BaseDocInventoryRelation>()
                    .Where(x => x.BusinessId == doc.ID && x.DocType == DocTypeEnum.ProductionIssue).ToList();
                // 审核操作
                // 此处需要进行拆行和合并操作（暂时只做拆行操作，否则新行的来源行号会有2个，不好生成新行号。已与林杰确认，所有下架审核的业务都只需要做拆行动作，无需合并 2025-06-25）
                foreach (var line in doc.ProductionIssueLine_ProductionIssue)
                {
                    line.Status = ProductionIssueLineStatusEnum.AllShipped;
                    line.ShippedQty = line.OffQty;
                    line.ToBeShipQty = 0;
                    line.UpdateBy = LoginUserInfo.ITCode;
                    line.UpdateTime = DateTime.Now;
                    // List<ApproveTransferOutLinePara> newLines = new List<ApproveTransferOutLinePara>();   // 根据番号进行汇总成新行的参数（新行只传U9，WMS仍保留原行）
                    // int newLineNo = (int)line.DocLineNo * 100 + 1;  // 新行号（如：原行号为10，则新行号从1001开始）
                    // 遍历单据库存关联关系表，找到对应行的库存信息
                    var matchRelations = invRelations.FindAll(x => x.BusinessLineId == line.ID).ToList();
                    foreach (var relation in matchRelations)
                    {
                        var inventory = DC.Set<BaseInventory>().Where(y => y.ID == relation.InventoryId).FirstOrDefault();
                        if (inventory == null)  // 不要判断Abandoned，因为不同行可能会用到相同的库存信息。新行的创建仍需进行
                        {
                            continue;
                        }
                        // var existNewLine = newLines.Find(x => x.SeibanCode == inventory.Seiban);
                        //if (existNewLine == null)
                        //{
                        //    newLines.Add(new ApproveTransferOutLinePara
                        //    {
                        //        DocLineNo = line.DocLineNo,
                        //        SeibanCode = inventory.Seiban,
                        //        StoreUOMQty = relation.Qty,
                        //        ItemInfo_ItemCode = line.ItemMaster.Code,
                        //        NewDocLineNo = newLineNo
                        //    });
                        //    newLineNo++;
                        //}
                        //else
                        //{
                        //    existNewLine.StoreUOMQty += relation.Qty;
                        //}
                        // 创建库存流水
                        this.CreateInvLog(OperationTypeEnum.ProductionIssueApprove, doc.DocNo, inventory.ID, null, -relation.Qty, null);
                        inventory.Qty -= relation.Qty;
                        inventory.IsAbandoned = true;
                        inventory.UpdateBy = LoginUserInfo.ITCode;
                        inventory.UpdateTime = DateTime.Now;
                    }
                    //// 核对一下参数的总数量与行的总数量是否一致（不会出现。冗余校验）
                    //if (newLines.Sum(x => x.StoreUOMQty) != line.Qty)
                    //{
                    //    MSD.AddModelError("", $"行“{line.DocLineNo}”的总下架数量“{line.Qty}”与实际下架数量“{newLines.Sum(x => x.StoreUOMQty)}”不一致");
                    //    return;
                    //}
                    // para.transOutLineDTOList.AddRange(newLines);
                }
                doc.Status = ProductionIssueStatusEnum.AllShipped;
                doc.UpdateBy = LoginUserInfo.ITCode;
                doc.UpdateTime = DateTime.Now;
                DC.SaveChanges();
                // 调用U9接口
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, orgCode, LoginUserInfo.Name);
                U9Return u9Return = apiHelper.ApproveProductionIssue(doc.DocNo);
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
