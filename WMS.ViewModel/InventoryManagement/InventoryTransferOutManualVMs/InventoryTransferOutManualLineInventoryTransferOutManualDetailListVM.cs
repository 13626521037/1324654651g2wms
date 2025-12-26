
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

using WMS.Model.BaseData;
using WMS.Util;


namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutManualLineVMs
{
    public partial class InventoryTransferOutManualLineInventoryTransferOutManualDetailListVM : BasePagedListVM<InventoryTransferOutManualLine_DetailView, InventoryTransferOutManualLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryTransferOutManualLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferOutManualLine_DetailView>>{
                
                //this.MakeGridHeader(x => x.SourceSystemId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.来源系统主键"].Value),
                //this.MakeGridHeader(x => x.LastUpdateTime).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.最后修改时间"].Value),
                //this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value),
                this.MakeGridHeader(x => x.DocLineNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                //this.MakeGridHeader(x => x.NewDocLineNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.新行号"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.Seiban).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.番号"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.ToBeOffQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待下架数量"].Value),
                this.MakeGridHeader(x => x.ToBeShipQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待出货数量"].Value),
                this.MakeGridHeader(x => x.OffQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已下架数量"].Value),
                this.MakeGridHeader(x => x.ShippedQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已出货数量"].Value),
                this.MakeGridHeader(x => x.TransInQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.调入数量"].Value),
                this.MakeGridHeader(x => x.Status).SetEditType(EditTypeEnum.Text,typeof(InventoryTransferOutManualLineStatusEnum).ToListItems(null,true)).SetTitle(@Localizer["Page.状态"].Value),

            };
        }

        
        public override IOrderedQueryable<InventoryTransferOutManualLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.InventoryTransferOutManualId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryTransferOutManualLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryTransferOutManualLine>()
                .Where(x => id == x.InventoryTransferOutManualId)
                .Select(x => new InventoryTransferOutManualLine_DetailView
                {
                     ID = x.ID,
                     DocLineNo = x.DocLineNo,
                     NewDocLineNo = x.NewDocLineNo,
                     ItemMasterId = x.ItemMasterId,
                     ItemCode = x.ItemMaster.Code,
                     Seiban = x.Seiban,
                     Qty = x.Qty.TrimZero(),
                     ToBeOffQty = x.ToBeOffQty.TrimZero(),
                     ToBeShipQty = x.ToBeShipQty.TrimZero(),
                     OffQty = x.OffQty.TrimZero(),
                     ShippedQty = x.ShippedQty.TrimZero(),
                     TransInQty = x.TransInQty.TrimZero(),
                     Status = x.Status,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryTransferOutManualLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryTransferOutManualLine._InventoryTransferOutManual")]
        public string InventoryTransferOutManualId { get; set; }
    }


    public class InventoryTransferOutManualLine_DetailView: InventoryTransferOutManualLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }
    }
}

