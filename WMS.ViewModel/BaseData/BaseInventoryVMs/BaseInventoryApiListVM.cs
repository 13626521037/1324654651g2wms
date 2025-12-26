using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.BaseData;
using WMS.Model;


namespace WMS.ViewModel.BaseData.BaseInventoryVMs
{
    public partial class BaseInventoryApiListVM : BasePagedListVM<BaseInventoryApi_View, BaseInventoryApiSearcher>
    {

        protected override IEnumerable<IGridColumn<BaseInventoryApi_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseInventoryApi_View>>{
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.BatchNumber),
                this.MakeGridHeader(x => x.SerialNumber),
                this.MakeGridHeader(x => x.Seiban),
                this.MakeGridHeader(x => x.Qty),
                this.MakeGridHeader(x => x.IsAbandoned),
                this.MakeGridHeader(x => x.ItemSourceType),
                this.MakeGridHeader(x => x.FrozenStatus),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BaseInventoryApi_View> GetSearchQuery()
        {
            var query = DC.Set<BaseInventory>()
                .CheckContain(Searcher.BatchNumber, x=>x.BatchNumber)
                .CheckContain(Searcher.SerialNumber, x=>x.SerialNumber)
                .CheckContain(Searcher.Seiban, x=>x.Seiban)
                .Select(x => new BaseInventoryApi_View
                {
				    ID = x.ID,
                    Name_view = x.ItemMaster.Name,
                    Name_view2 = x.WhLocation.Name,
                    BatchNumber = x.BatchNumber,
                    SerialNumber = x.SerialNumber,
                    Seiban = x.Seiban,
                    Qty = x.Qty,
                    IsAbandoned = x.IsAbandoned,
                    ItemSourceType = x.ItemSourceType,
                    FrozenStatus = x.FrozenStatus,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BaseInventoryApi_View : BaseInventory{
        [Display(Name = "名称")]
        public String Name_view { get; set; }
        [Display(Name = "_Model._BaseWhLocation._Name")]
        public String Name_view2 { get; set; }

    }
}
