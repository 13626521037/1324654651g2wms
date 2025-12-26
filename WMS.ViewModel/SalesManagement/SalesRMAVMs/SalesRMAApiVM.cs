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
using WMS.Model.BaseData;
using WMS.Model.PurchaseManagement;
using WMS.Model.SalesManagement;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;


namespace WMS.ViewModel.SalesManagement.SalesRMAVMs
{
    public partial class SalesRMAApiVM : BaseCRUDVM<SalesRMA>
    {

        public SalesRMAApiVM()
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

        /// <summary>
        /// 获取单据列表
        /// </summary>
        /// <param name="type">0-待收货，1-待审核</param>
        /// <returns></returns>
        public List<SalesRMAReturn> GetList(int type = 0)
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
            List<SalesRMAReturn> list = new List<SalesRMAReturn>();
            Expression<Func<SalesRMA, bool>> queryable;
            if (type == 0)  // 待收货（未全部收货）。需匹配登录用户的存储地点
            {
                queryable = x => (x.Status == Model.SalesRMAStatusEnum.NotReceive
                    || x.Status == Model.SalesRMAStatusEnum.PartReceive)
                    && x.SalesRMALine_RMAId.Any(y => y.WareHouseId == whid);
            }
            else
            {
                queryable = x => x.Status == Model.SalesRMAStatusEnum.AllReceive
                    && x.SalesRMALine_RMAId.Any(y => y.WareHouseId == whid);
            }
            list = DC.Set<SalesRMA>()
               .Where(queryable)
               .AsNoTracking()
               .Select(x => new SalesRMAReturn
               {
                   ID = x.ID,
                   DocNo = x.DocNo,
                   DocType = x.DocType,
                   Status = x.Status.GetEnumDisplayName(),
                   BusinessDate = ((DateTime)x.BusinessDate).ToString("yyyy-MM-dd"),
                   Customer = x.Customer.Name,
                   SumQty = (int)(x.SalesRMALine_RMAId.Sum(y => y.Qty) ?? 0),
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
        public void Create(SalesRMA entity)
        {
            MSD.Clear();    // 看下是否需要判断
            var existEntity = DC.Set<SalesRMA>().Where(x => x.DocNo == entity.DocNo).FirstOrDefault();
            if (existEntity != null)
            {
                MSD.AddModelError("", "单据编号已存在");
                return;
            }
            // 引用类型外部系统ID转换
            string r = this.SSIdAttrToId(entity, entity.Organization, "Organization");    // 组织
            if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
            r = this.SSIdAttrToId(entity, entity.Customer, "Customer");   // 客户
            if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
            foreach (var line in entity.SalesRMALine_RMAId)
            {
                r = this.SSIdAttrToId(line, line.ItemMaster, "ItemMaster");   // 转换物料
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
                r = this.SSIdAttrToId(line, line.WareHouse, "WareHouse");   // 转换存储地点
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
            }
            foreach (var line in entity.SalesRMALine_RMAId)
            {
                line.Status = Model.SalesRMALineStatusEnum.NotReceive;
                line.ToBeReceiveQty = line.Qty;
                line.ToBeInWhQty = 0;
                line.ReceiveQty = 0;
                line.InWhQty = 0;
            }
            // 将单据保存到本系统中
            Entity = entity;
            Entity.Status = Model.SalesRMAStatusEnum.NotReceive;
            DoAdd();
        }

        /// <summary>
        /// 判断单据是否存在
        /// </summary>
        /// <param name="sourceSystemId">来源系统单据ID</param>
        /// <returns></returns>
        public bool IsDocExist(string sourceSystemId)
        {
            var entity = DC.Set<SalesRMA>().Where(x => x.SourceSystemId == sourceSystemId).AsNoTracking().FirstOrDefault();
            if (entity == null)
            {
                return false;
            }
            else
            {
                if (entity.Status == Model.SalesRMAStatusEnum.NotReceive)
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
        /// 获取单据
        /// </summary>
        /// <param name="docNo">单据编号</param>
        public SalesRMAReturn GetDoc(string docNo)
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

            SalesRMA doc = DC.Set<SalesRMA>()
                .Where(x => x.DocNo == docNo)
                .Include(x => x.Organization)
                .Include(x => x.Customer)
                .Include(x => x.SalesRMALine_RMAId)
                .ThenInclude(x => x.ItemMaster)
                .Include(x => x.SalesRMALine_RMAId)
                .ThenInclude(x => x.WareHouse)
                .AsNoTracking().FirstOrDefault();
            if (doc != null)    // 单据在本系统中存在
            {
                // 判断发料存储地点是否匹配当前登录用户的存储地点
                if (doc.SalesRMALine_RMAId.Any(y => y.WareHouseId != whid))
                {
                    MSD.AddModelError("", $"单据{docNo}的发料存储地点与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                // 判断单据状态：只有未收货和部分收货的才允许操作
                if (doc.Status != Model.SalesRMAStatusEnum.NotReceive && doc.Status != Model.SalesRMAStatusEnum.PartReceive)
                {
                    MSD.AddModelError("", $"当前单据状态“{doc.Status.GetEnumDisplayName()}”不允许进行收货操作");
                    return null;
                }
                return new SalesRMAReturn(doc);
            }
            // 本系统不存在时，尝试从ERP获取
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<SalesRMA> u9Return = apiHelper.GetRma(docNo);
            if (u9Return.Success)
            {
                // 引用类型外部系统ID转换
                string r = this.SSIdAttrToId(u9Return.Entity, u9Return.Entity.Organization, "Organization");    // 组织
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                r = this.SSIdAttrToId(u9Return.Entity, u9Return.Entity.Customer, "Customer");   // 客户
                if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                foreach (var line in u9Return.Entity.SalesRMALine_RMAId)
                {
                    r = this.SSIdAttrToId(line, line.ItemMaster, "ItemMaster");   // 转换物料
                    if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                    r = this.SSIdAttrToId(line, line.WareHouse, "WareHouse");   // 转换存储地点
                    if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return null; }
                }
                // 判断调出存储地点是否匹配（转换完了才能进行匹配，不然没有值）
                if (u9Return.Entity.SalesRMALine_RMAId.Any(y => y.WareHouseId != whid))
                {
                    MSD.AddModelError("", $"单据{docNo}的发料存储地点与当前登录用户的存储地点{loginWh.Code}不匹配");
                    return null;
                }
                foreach (var line in u9Return.Entity.SalesRMALine_RMAId)
                {
                    line.Status = Model.SalesRMALineStatusEnum.NotReceive;
                    line.ToBeReceiveQty = line.Qty;
                    line.ToBeInWhQty = 0;
                    line.ReceiveQty = 0;
                    line.InWhQty = 0;
                }
                // 将单据保存到本系统中
                Entity = u9Return.Entity;
                Entity.Status = Model.SalesRMAStatusEnum.NotReceive;
                DoAdd();
                if (!MSD.IsValid)
                {
                    return null;
                }
                doc = DC.Set<SalesRMA>()
                    .Where(x => x.DocNo == docNo)
                    .Include(x => x.Organization)
                    .Include(x => x.Customer)
                    .Include(x => x.SalesRMALine_RMAId)
                    .ThenInclude(x => x.ItemMaster)
                    .Include(x => x.SalesRMALine_RMAId)
                    .ThenInclude(x => x.WareHouse)
                    .AsNoTracking().FirstOrDefault();   // 要重新获取，否则有些外键实体会为null
                if (doc == null)
                {
                    MSD.AddModelError("", "单据已同步，但未能正常显示。请尝试退出后重试");    // 不会出现这种情况
                    return null;
                }
                return new SalesRMAReturn(doc);
            }
            else
            {
                MSD.AddModelError("", u9Return.Msg);
                return null;
            }
        }

        /// <summary>
        /// 获取推荐库位（根据返回前端的单据格式）
        /// </summary>
        /// <param name="doc"></param>
        public void GetSuggestLocs(SalesRMAReturn doc)
        {
            foreach (var line in doc.Lines)
            {
                line.SuggestLocs = new List<SalesRMAReturnLineSuggestLoc>();
                List<BaseInventory> invs = this.GetItemInventory(line.ItemID, doc.WareHouseID);
                if (invs.Count > 0)
                {
                    invs = invs.OrderByDescending(x => x.CreateTime).ToList(); // 降序
                    foreach (var inv in invs)
                    {
                        line.SuggestLocs.Add(new SalesRMAReturnLineSuggestLoc
                        {
                            AreaCode = inv.WhLocation.WhArea.Code,
                            LocCode = inv.WhLocation.Code,
                        });
                    }
                }
            }
        }
    }
}
