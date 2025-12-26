
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using WMS.Util;


namespace WMS.ViewModel.InventoryManagement.InventoryAdjustLineVMs
{
    public partial class InventoryAdjustLineInvAdjustDetailListVM : BasePagedListVM<InventoryAdjustLine_DetailView, InventoryAdjustLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryAdjustLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<InventoryAdjustLine_DetailView>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.ItemCode, width: 90).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.ItemName).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.SPECS).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.LocationCode, width:80).SetEditType(EditTypeEnum.Text),
                //this.MakeGridHeader(x => x.StockTakingLineId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.盘点单行"].Value),
                //this.MakeGridHeader(x => x.InventoryId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.库存信息"].Value),
                //this.MakeGridHeader(x => x.LocationId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.库位"].Value),
                this.MakeGridHeader(x => x.Qty, width:75).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.原库存数量"].Value),
                this.MakeGridHeader(x => x.StockTakingQty, width:75).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.盘点数量"].Value),
                this.MakeGridHeader(x => x.DiffQty, width:75).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.盈亏数量"].Value),
                this.MakeGridHeader(x => x.GainLossStatus, width:75).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.盈亏状态"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value).SetHide(),

            };
        }

        
        public override IOrderedQueryable<InventoryAdjustLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.InvAdjustId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryAdjustLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryAdjustLine>()
                .Where(x => id == x.InvAdjustId)
                .Select(x => new InventoryAdjustLine_DetailView
                {
                     ID = x.ID,
                     ItemCode = x.StockTakingLine.ItemMaster.Code,
                     ItemName = x.StockTakingLine.ItemMaster.Name,
                     SPECS = x.StockTakingLine.ItemMaster.SPECS,
                     LocationCode = x.Location.Code,
                     //StockTakingLineId = x.StockTakingLineId,
                     //InventoryId = x.InventoryId,
                     //LocationId = x.LocationId,
                     Qty = x.Qty.TrimZero(),
                     StockTakingQty = x.StockTakingQty.TrimZero(),
                     DiffQty = x.DiffQty.TrimZero(),
                     GainLossStatus = x.GainLossStatus,
                     Memo = x.Memo
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryAdjustLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryAdjustLine._InvAdjust")]
        public string InvAdjustId { get; set; }
    }

    public class InventoryAdjustLine_DetailView: InventoryAdjustLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }

        [Display(Name = "品名")]
        public string ItemName { get; set; }

        [Display(Name = "规格")]
        public string SPECS { get; set; }

        [Display(Name = "库位")]
        public string LocationCode { get; set; }
    }
}

