
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


namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeLineVMs
{
    public partial class InventoryUnfreezeLineInventoryUnfreezeDetailListVM1 : BasePagedListVM<InventoryUnfreezeLine, InventoryUnfreezeLineDetailSearcher1>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("InventoryUnfreezeLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("InventoryUnfreezeLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryUnfreezeLine>> InitGridHeader()
        {
            return new List<GridColumn<InventoryUnfreezeLine>>{
                
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

        
        public override IOrderedQueryable<InventoryUnfreezeLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.InventoryUnfreezeId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryUnfreezeLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryUnfreezeLine>()
                .Where(x => id == x.InventoryUnfreezeId)

                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryUnfreezeLineDetailSearcher1 : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryUnfreezeLine._InventoryUnfreeze")]
        public string InventoryUnfreezeId { get; set; }
    }

}

