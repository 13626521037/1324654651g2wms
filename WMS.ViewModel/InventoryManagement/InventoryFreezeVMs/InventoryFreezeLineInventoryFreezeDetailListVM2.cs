
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
using WMS.ViewModel.InventoryManagement.InventoryFreezeVMs;
using WMS.Util;


namespace WMS.ViewModel.InventoryManagement.InventoryFreezeLineVMs
{
    public partial class InventoryFreezeLineInventoryFreezeDetailListVM2 : BasePagedListVM<InventoryFreezeLine_DetailView2, InventoryFreezeLineDetailSearcher2>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryFreezeLine_DetailView2>> InitGridHeader()
        {
            return new List<GridColumn<InventoryFreezeLine_DetailView2>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.BaseInventory_BatchNumber),
                this.MakeGridHeader(x => x.BaseInventory_SerialNumber),
                this.MakeGridHeader(x => x.BaseInventory_ItemMaster_Code),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),

            };
        }

        
        public override IOrderedQueryable<InventoryFreezeLine_DetailView2> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.InventoryFreezeId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryFreezeLine_DetailView2>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryFreezeLine>()
                .Where(x => id == x.InventoryFreezeId)
                .Select(x => new InventoryFreezeLine_DetailView2
                {
                    ID = x.ID,
                    BaseInventory_BatchNumber = x.BaseInventory.BatchNumber,
                    BaseInventory_SerialNumber = Common.AddInventoryDialog(x.BaseInventory),
                    BaseInventory_ItemMaster_Code = x.BaseInventory.ItemMaster.Code,
                    Qty = x.Qty.TrimZero(),
                    Memo = x.Memo
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryFreezeLineDetailSearcher2 : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryFreezeLine._InventoryFreeze")]
        public string InventoryFreezeId { get; set; }
    }


    public class InventoryFreezeLine_DetailView2 : InventoryFreezeLine
    {
        [Display(Name = "批号")]
        public string BaseInventory_BatchNumber { get; set; }

        [Display(Name = "序列号")]
        public string BaseInventory_SerialNumber { get; set; }

        [Display(Name = "料号")]
        public string BaseInventory_ItemMaster_Code { get; set; }
    }
}

