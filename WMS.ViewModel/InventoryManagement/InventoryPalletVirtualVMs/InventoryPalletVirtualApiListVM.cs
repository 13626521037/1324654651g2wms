using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;


namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs
{
    public partial class InventoryPalletVirtualApiListVM : BasePagedListVM<InventoryPalletVirtualApi_View, InventoryPalletVirtualApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryPalletVirtualApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryPalletVirtualApi_View>>{
                this.MakeGridHeader(x => x.Code),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.SysVersion),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryPalletVirtualApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryPalletVirtual>()
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .Select(x => new InventoryPalletVirtualApi_View
                {
				    ID = x.ID,
                    Code = x.Code,
                    Status = x.Status,
                    Name_view = x.Location.Name,
                    SysVersion = x.SysVersion,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventoryPalletVirtualApi_View : InventoryPalletVirtual{
        [Display(Name = "_Model._BaseWhLocation._Name")]
        public String Name_view { get; set; }

    }
}
