
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


namespace WMS.ViewModel.InventoryManagement.InventorySplitSingleLineVMs
{
    public partial class InventorySplitSingleLineSplitSingleDetailListVM : BasePagedListVM<InventorySplitSingleLine_DetailView, InventorySplitSingleLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<InventorySplitSingleLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<InventorySplitSingleLine_DetailView>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value),
                //this.MakeGridHeader(x => x.NewInvId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.新库存信息"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.SerialNumber).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),

            };
        }

        
        public override IOrderedQueryable<InventorySplitSingleLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.SplitSingleId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventorySplitSingleLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventorySplitSingleLine>()
                .Where(x => id == x.SplitSingleId)
                .Select(x => new InventorySplitSingleLine_DetailView
                {
                     ID = x.ID,
                     NewInvId = x.NewInvId,
                     SerialNumber = Common.AddInventoryDialog(x.NewInv),
                     Qty = x.Qty.TrimZero(),
                     Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventorySplitSingleLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._InventorySplitSingleLine._SplitSingle")]
        public string SplitSingleId { get; set; }
    }

    public class InventorySplitSingleLine_DetailView : InventorySplitSingleLine
    {
        [Display(Name = "序列号")]
        public string SerialNumber { get; set; }
    }
}

