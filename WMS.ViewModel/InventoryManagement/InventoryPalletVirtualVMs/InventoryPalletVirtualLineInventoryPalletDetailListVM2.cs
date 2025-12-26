
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
using Microsoft.EntityFrameworkCore;
using WMS.Util;


namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualLineVMs
{
    public partial class InventoryPalletVirtualLineInventoryPalletDetailListVM2 : BasePagedListVM<InventoryPalletVirtualLine_DetailView2, InventoryPalletVirtualLineDetailSearcher2>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryPalletVirtualLine_DetailView2>> InitGridHeader()
        {
            return new List<GridColumn<InventoryPalletVirtualLine_DetailView2>>{
                
                // this.MakeGridHeader(x => x.ID,width: 100).SetTitle(@Localizer["Page.ID"].Value),   //.SetEditType(EditTypeEnum.TextBox)
                this.MakeGridHeader(x => x.DocLineNo, width: 80).SetTitle(@Localizer["Sys.RowIndex"].Value),   // .SetEditType(EditTypeEnum.TextBox)
                this.MakeGridHeader(x => x.InventoryPalletVirtualLine_BaseInventory_ItemMaster_Code, width:90),    // .SetEditType(EditTypeEnum.ComboBox,DC.Set<BaseInventory>().GetSelectListItems(Wtm, x => x.BatchNumber,SortByName:true))
                this.MakeGridHeader(x => x.InventoryPalletVirtualLine_BaseInventory_SerialNumber, width:240),    // .SetEditType(EditTypeEnum.ComboBox,DC.Set<BaseInventory>().GetSelectListItems(Wtm, x => x.BatchNumber,SortByName:true))
                this.MakeGridHeader(x => x.InventoryPalletVirtualLine_BaseInventory_BatchNumber, width:150),
                this.MakeGridHeader(x => x.InventoryPalletVirtualLine_BaseInventory_Seiban, width:120),
                this.MakeGridHeader(x => x.Memo).SetTitle(@Localizer["_Admin.Remark"].Value),   // .SetEditType(EditTypeEnum.TextBox)

            };
        }

        
        public override IOrderedQueryable<InventoryPalletVirtualLine_DetailView2> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.InventoryPalletId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryPalletVirtualLine_DetailView2>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryPalletVirtualLine>()
                .Where(x => id == x.InventoryPalletId)
                .Include(x => x.BaseInventory.ItemMaster)
                .Select(x => new InventoryPalletVirtualLine_DetailView2
                {
                    ID = x.ID,
                    DocLineNo = x.DocLineNo,
                    InventoryPalletVirtualLine_BaseInventory_SerialNumber = Common.AddInventoryDialog(x.BaseInventory),
                    InventoryPalletVirtualLine_BaseInventory_BatchNumber = x.BaseInventory.BatchNumber,
                    InventoryPalletVirtualLine_BaseInventory_Seiban = x.BaseInventory.Seiban,
                    InventoryPalletVirtualLine_BaseInventory_ItemMaster_Code = x.BaseInventory.ItemMaster.Code,
                    Memo = x.Memo
                })
                .OrderBy(x=>x.DocLineNo);
            return query;
        }

    }

    public partial class InventoryPalletVirtualLineDetailSearcher2 : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryPalletVirtualLine._InventoryPallet")]
        public string InventoryPalletId { get; set; }
    }

    public class InventoryPalletVirtualLine_DetailView2 : InventoryPalletVirtualLine
    {
        [Display(Name = "批号")]
        public string InventoryPalletVirtualLine_BaseInventory_BatchNumber { get; set; }

        [Display(Name = "番号")]
        public string InventoryPalletVirtualLine_BaseInventory_Seiban { get; set; }

        [Display(Name = "序列号")]
        public string InventoryPalletVirtualLine_BaseInventory_SerialNumber { get; set; }

        [Display(Name = "料号")]
        public string InventoryPalletVirtualLine_BaseInventory_ItemMaster_Code { get; set; }
    }
}

