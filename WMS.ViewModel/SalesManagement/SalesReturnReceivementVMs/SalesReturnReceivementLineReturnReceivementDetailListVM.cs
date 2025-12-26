
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.SalesManagement;
using WMS.Util;


namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementLineVMs
{
    public partial class SalesReturnReceivementLineReturnReceivementDetailListVM : BasePagedListVM<SalesReturnReceivementLine_DetailView, SalesReturnReceivementLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<SalesReturnReceivementLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<SalesReturnReceivementLine_DetailView>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.DocLineNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.Seiban).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.番号"].Value),
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

        
        public override IOrderedQueryable<SalesReturnReceivementLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.ReturnReceivementId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<SalesReturnReceivementLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<SalesReturnReceivementLine>()
                .Where(x => id == x.ReturnReceivementId)
                .Select(x => new SalesReturnReceivementLine_DetailView
                {
                     ID = x.ID,
                     DocLineNo = x.DocLineNo,
                     ItemMasterId = x.ItemMasterId,
                     ItemCode = x.ItemMaster.Code,
                     Seiban = x.Seiban,
                     Qty = x.Qty.TrimZero(),
                     ToBeReceiveQty = x.ToBeReceiveQty.TrimZero(),
                     ToBeInWhQty = x.ToBeInWhQty.TrimZero(),
                     ReceiveQty = x.ReceiveQty.TrimZero(),
                     InWhQty = x.InWhQty.TrimZero(),
                     WareHouseId = x.WareHouseId,
                     WhCode = x.WareHouse.Code,
                     Status = x.Status,
                     LastUpdateTime = x.LastUpdateTime,
                     SourceSystemId = x.SourceSystemId,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class SalesReturnReceivementLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._SalesReturnReceivementLine._ReturnReceivement")]
        public string ReturnReceivementId { get; set; }
    }

    public class SalesReturnReceivementLine_DetailView : SalesReturnReceivementLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }

        [Display(Name = "存储地点")]
        public string WhCode { get; set; }
    }
}

