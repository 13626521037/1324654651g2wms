
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


namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingErpDiffLineVMs
{
    public partial class InventoryStockTakingErpDiffLineStockTakingDetailListVM : BasePagedListVM<InventoryStockTakingErpDiffLine, InventoryStockTakingErpDiffLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryStockTakingErpDiffLine>> InitGridHeader()
        {
            return new List<GridColumn<InventoryStockTakingErpDiffLine>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value),
                this.MakeGridHeader(x => x.ItemId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.Seiban).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.番号"].Value),
                this.MakeGridHeader(x => x.WmsQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.本系统数量"].Value),
                this.MakeGridHeader(x => x.ErpQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ERP数量"].Value),
                this.MakeGridHeader(x => x.CreateTime).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.CreateBy).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.CreateBy"].Value),
            };
        }

        
        public override IOrderedQueryable<InventoryStockTakingErpDiffLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.StockTakingId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryStockTakingErpDiffLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryStockTakingErpDiffLine>()
                .Where(x => id == x.StockTakingId)

                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryStockTakingErpDiffLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryStockTakingErpDiffLine._StockTaking")]
        public string StockTakingId { get; set; }
    }

}

