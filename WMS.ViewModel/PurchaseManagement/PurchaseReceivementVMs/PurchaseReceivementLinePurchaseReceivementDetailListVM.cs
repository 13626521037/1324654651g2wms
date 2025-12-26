
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
using Microsoft.EntityFrameworkCore;
using WMS.Util;


namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementLineVMs
{
    public partial class PurchaseReceivementLinePurchaseReceivementDetailListVM : BasePagedListVM<PurchaseReceivementLine_DetailView, PurchaseReceivementLineDetailSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }

        protected override IEnumerable<IGridColumn<PurchaseReceivementLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseReceivementLine_DetailView>>{

                this.MakeGridHeader(x => x.SourceSystemId, width: 120).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.来源系统主键"].Value).SetHide(),
                this.MakeGridHeader(x => x.LastUpdateTime, width: 140).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.最后修改时间"].Value).SetHide(),
                this.MakeGridHeader(x => x.ID, width: 245).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.DocLineNo, width: 60).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.ItemCode, width: 85).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.Qty, width: 60).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.QualifiedQty, width: 60).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.合格数量"].Value),
                this.MakeGridHeader(x => x.UnqualifiedRejectQty, width: 60).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["不合格"].Value),
                this.MakeGridHeader(x => x.UnqualifiedAcceptQty, width: 75).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["不合格接收"].Value),
                this.MakeGridHeader(x => x.ConcessionAcceptQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["让步接收"].Value),
                this.MakeGridHeader(x => x.ToBeReceiveQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["待收货"].Value),
                this.MakeGridHeader(x => x.ToBeInspectQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["待检验"].Value),
                this.MakeGridHeader(x => x.ToBeInWhQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["待入库"].Value),
                this.MakeGridHeader(x => x.InspectQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["已检验"].Value),
                this.MakeGridHeader(x => x.ReceiveQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["已收货"].Value),
                this.MakeGridHeader(x => x.InWhQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["已入库"].Value),
                this.MakeGridHeader(x => x.UnqualifiedRejectDeal, width: 105).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["不合格退料处理"].Value),
                this.MakeGridHeader(x => x.UnqualifiedAcceptDeal, width: 105).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["不合格接收处理"].Value),
                this.MakeGridHeader(x => x.WhCode, width: 85).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.Status).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.InspectStatus).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.检验状态"].Value),
                this.MakeGridHeader(x => x.InspectorId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.检验员"].Value),

            };
        }


        public override IOrderedQueryable<PurchaseReceivementLine_DetailView> GetSearchQuery()
        {

            var id = (Guid?)Searcher.PurchaseReceivementId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<PurchaseReceivementLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<PurchaseReceivementLine>()
                .Where(x => id == x.PurchaseReceivementId)
                .Select(x => new PurchaseReceivementLine_DetailView
                 {
                     ID = x.ID,
                     PurchaseReceivementId = x.PurchaseReceivementId,
                     SourceSystemId = x.SourceSystemId,
                     LastUpdateTime = x.LastUpdateTime,
                     DocLineNo = x.DocLineNo,
                     ItemCode = x.ItemMaster.Code,
                     WhCode = x.WareHouse.Code,
                     Qty = x.Qty.TrimZero(),
                     QualifiedQty = x.QualifiedQty.TrimZero(),
                     UnqualifiedRejectQty = x.UnqualifiedRejectQty.TrimZero(),
                     UnqualifiedAcceptQty = x.UnqualifiedAcceptQty.TrimZero(),
                     ConcessionAcceptQty = x.ConcessionAcceptQty.TrimZero(),
                     ToBeReceiveQty = x.ToBeReceiveQty.TrimZero(),
                     ToBeInspectQty = x.ToBeInspectQty.TrimZero(),
                     ToBeInWhQty = x.ToBeInWhQty.TrimZero(),
                     InspectQty = x.InspectQty.TrimZero(),
                     ReceiveQty = x.ReceiveQty.TrimZero(),
                     InWhQty = x.InWhQty.TrimZero(),
                     UnqualifiedRejectDeal = x.UnqualifiedRejectDeal,
                     UnqualifiedAcceptDeal = x.UnqualifiedAcceptDeal,
                     Status = x.Status,
                     InspectStatus = x.InspectStatus,
                     InspectorId = x.InspectorId,
                 })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class PurchaseReceivementLineDetailSearcher : BaseSearcher
    {

        [Display(Name = "_Model._PurchaseReceivementLine._PurchaseReceivement")]
        public string PurchaseReceivementId { get; set; }
    }

    public class PurchaseReceivementLine_DetailView : PurchaseReceivementLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }

        [Display(Name = "存储地点")]
        public string WhCode { get; set; }
    }
}

