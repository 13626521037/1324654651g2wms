
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


namespace WMS.ViewModel.InventoryManagement.InventoryFreezeLineVMs
{
    public partial class InventoryFreezeLineInventoryFreezeDetailListVM1 : BasePagedListVM<InventoryFreezeLine, InventoryFreezeLineDetailSearcher1>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("InventoryFreezeLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("InventoryFreezeLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryFreezeLine>> InitGridHeader()
        {
            return new List<GridColumn<InventoryFreezeLine>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["Page.ID"].Value),
                this.MakeGridHeader(x => x.CreateTime).SetEditType(EditTypeEnum.Datetime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.UpdateTime).SetEditType(EditTypeEnum.Datetime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.CreateBy).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.UpdateBy).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => x.BaseInventoryId).SetEditType(EditTypeEnum.ComboBox,DC.Set<BaseInventory>().GetSelectListItems(Wtm, x => x.BatchNumber,SortByName:true)).SetTitle(@Localizer["Page.库存信息"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["_Admin.Remark"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        
        public override IOrderedQueryable<InventoryFreezeLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.InventoryFreezeId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryFreezeLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryFreezeLine>()
                .Where(x => id == x.InventoryFreezeId)

                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryFreezeLineDetailSearcher1 : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryFreezeLine._InventoryFreeze")]
        public string InventoryFreezeId { get; set; }
    }

}

