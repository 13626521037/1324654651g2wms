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
using WMS.Model;


namespace WMS.ViewModel.InventoryManagement.InventoryTransferInVMs
{
    public partial class InventoryTransferInApiListVM : BasePagedListVM<InventoryTransferInApi_View, InventoryTransferInApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryTransferInApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferInApi_View>>{
                this.MakeGridHeader(x => x.ErpID),
                this.MakeGridHeader(x => x.BusinessDate),
                this.MakeGridHeader(x => x.DocType),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.TransferOutType),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryTransferInApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryTransferIn>()
                .Select(x => new InventoryTransferInApi_View
                {
				    ID = x.ID,
                    ErpID = x.ErpID,
                    BusinessDate = x.BusinessDate,
                    DocType = x.DocType,
                    DocNo = x.DocNo,
                    Name_view = x.TransInOrganization.Name,
                    Name_view2 = x.TransInWh.Name,
                    TransferOutType = x.TransferOutType,
                    Status = x.Status,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventoryTransferInApi_View : InventoryTransferIn{
        [Display(Name = "名称")]
        public String Name_view { get; set; }
        [Display(Name = "名称")]
        public String Name_view2 { get; set; }

    }
}
