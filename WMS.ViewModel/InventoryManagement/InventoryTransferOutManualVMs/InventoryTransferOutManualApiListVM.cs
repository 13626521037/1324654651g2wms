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


namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs
{
    public partial class InventoryTransferOutManualApiListVM : BasePagedListVM<InventoryTransferOutManualApi_View, InventoryTransferOutManualApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryTransferOutManualApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferOutManualApi_View>>{
                this.MakeGridHeader(x => x.CreatePerson),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.BusinessDate),
                this.MakeGridHeader(x => x.SubmitDate),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.DocType),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.Name_view3),
                this.MakeGridHeader(x => x.Name_view4),
                this.MakeGridHeader(x => x.Name_view5),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeader(x => x.SourceSystemId),
                this.MakeGridHeader(x => x.LastUpdateTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryTransferOutManualApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryTransferOutManual>()
                .Select(x => new InventoryTransferOutManualApi_View
                {
				    ID = x.ID,
                    CreatePerson = x.CreatePerson,
                    Name_view = x.Organization.Name,
                    BusinessDate = x.BusinessDate,
                    SubmitDate = x.SubmitDate,
                    DocNo = x.DocNo,
                    DocType = x.DocType,
                    Name_view2 = x.TransInOrganization.Name,
                    Name_view3 = x.TransInWh.Name,
                    Name_view4 = x.TransOutOrganization.Name,
                    Name_view5 = x.TransOutWh.Name,
                    Status = x.Status,
                    Memo = x.Memo,
                    SourceSystemId = x.SourceSystemId,
                    LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventoryTransferOutManualApi_View : InventoryTransferOutManual{
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
