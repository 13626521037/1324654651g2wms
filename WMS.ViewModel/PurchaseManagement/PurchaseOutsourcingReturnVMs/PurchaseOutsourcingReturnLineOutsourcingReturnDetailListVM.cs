
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;

using WMS.Model.BaseData;
using WMS.Util;


namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnLineVMs
{
    public partial class PurchaseOutsourcingReturnLineOutsourcingReturnDetailListVM : BasePagedListVM<PurchaseOutsourcingReturnLine_DetailView, PurchaseOutsourcingReturnLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<PurchaseOutsourcingReturnLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseOutsourcingReturnLine_DetailView>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.DocLineNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.ToBeReceiveQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待收货数量"].Value),
                this.MakeGridHeader(x => x.ToBeInWhQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待入库数量"].Value),
                this.MakeGridHeader(x => x.ReceiveQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已收货数量"].Value),
                this.MakeGridHeader(x => x.InWhQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已入库数量"].Value),
                this.MakeGridHeader(x => x.WhCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.Status).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.状态"].Value),
                //this.MakeGridHeader(x => x.SourceSystemId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.来源系统主键"].Value),
                //this.MakeGridHeader(x => x.LastUpdateTime).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.最后修改时间"].Value),

            };
        }

        
        public override IOrderedQueryable<PurchaseOutsourcingReturnLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.OutsourcingReturnId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<PurchaseOutsourcingReturnLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<PurchaseOutsourcingReturnLine>()
                .Where(x => id == x.OutsourcingReturnId)
                .Select(x => new PurchaseOutsourcingReturnLine_DetailView
                {
                    ID = x.ID,
                    OutsourcingReturnId = x.OutsourcingReturnId,
                    DocLineNo = x.DocLineNo,
                    ItemMasterId = x.ItemMasterId,
                    Qty = x.Qty.TrimZero(),
                    ToBeReceiveQty = x.ToBeReceiveQty.TrimZero(),
                    ToBeInWhQty = x.ToBeInWhQty.TrimZero(),
                    ReceiveQty = x.ReceiveQty.TrimZero(),
                    InWhQty = x.InWhQty.TrimZero(),
                    WareHouseId = x.WareHouseId,
                    Status = x.Status,
                    ItemCode = x.ItemMaster.Code,
                    WhCode = x.WareHouse.Code,
                    SourceSystemId = x.SourceSystemId,
                    LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class PurchaseOutsourcingReturnLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._OutsourcingReturn")]
        public string OutsourcingReturnId { get; set; }
    }

    public class PurchaseOutsourcingReturnLine_DetailView : PurchaseOutsourcingReturnLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }

        [Display(Name = "存储地点")]
        public string WhCode { get; set; }
    }
}

