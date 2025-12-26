
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


namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualLineVMs
{
    public partial class InventoryPalletVirtualLineInventoryPalletDetailListVM : BasePagedListVM<InventoryPalletVirtualLine, InventoryPalletVirtualLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("InventoryPalletVirtualLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("InventoryPalletVirtualLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryPalletVirtualLine>> InitGridHeader()
        {
            return new List<GridColumn<InventoryPalletVirtualLine>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["Page.ID"].Value),
                this.MakeGridHeader(x => x.DocLineNo).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.BaseInventoryId).SetEditType(EditTypeEnum.ComboBox,DC.Set<BaseInventory>().GetSelectListItems(Wtm, x => x.BatchNumber,SortByName:true)).SetTitle(@Localizer["Page.库存信息"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["_Admin.Remark"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        
        public override IOrderedQueryable<InventoryPalletVirtualLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.InventoryPalletId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryPalletVirtualLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryPalletVirtualLine>()
                .Where(x => id == x.InventoryPalletId)

                .OrderBy(x=>x.DocLineNo);
            return query;
        }

    }

    public partial class InventoryPalletVirtualLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryPalletVirtualLine._InventoryPallet")]
        public string InventoryPalletId { get; set; }
    }

}

