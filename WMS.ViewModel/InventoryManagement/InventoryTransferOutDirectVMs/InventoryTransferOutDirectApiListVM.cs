using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;


namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs
{
    public partial class InventoryTransferOutDirectApiListVM : BasePagedListVM<InventoryTransferOutDirectApi_View, InventoryTransferOutDirectApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryTransferOutDirectApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferOutDirectApi_View>>{
                this.MakeGridHeader(x => x.ErpID),
                this.MakeGridHeader(x => x.BusinessDate),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.Name_view3),
                this.MakeGridHeader(x => x.Name_view4),
                this.MakeGridHeader(x => x.Name_view5),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryTransferOutDirectApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryTransferOutDirect>()
                .Select(x => new InventoryTransferOutDirectApi_View
                {
				    ID = x.ID,
                    ErpID = x.ErpID,
                    BusinessDate = x.BusinessDate,
                    Name_view = x.DocType.Name,
                    DocNo = x.DocNo,
                    Name_view2 = x.TransInOrganization.Name,
                    Name_view3 = x.TransInWh.Name,
                    Name_view4 = x.TransOutWh.Name,
                    Name_view5 = x.TransOutOrganization.Name,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventoryTransferOutDirectApi_View : InventoryTransferOutDirect{
        [Display(Name = "名称")]
        public String Name_view { get; set; }
        [Display(Name = "名称")]
        public String Name_view2 { get; set; }
        [Display(Name = "名称")]
        public String Name_view3 { get; set; }
        [Display(Name = "名称")]
        public String Name_view4 { get; set; }
        [Display(Name = "名称")]
        public String Name_view5 { get; set; }

    }
}
