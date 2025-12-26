using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.PurchaseManagement;
using WMS.Model.BaseData;
using WMS.Model;


namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs
{
    public partial class PurchaseReceivementApiListVM : BasePagedListVM<PurchaseReceivementApi_View, PurchaseReceivementApiSearcher>
    {

        protected override IEnumerable<IGridColumn<PurchaseReceivementApi_View>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseReceivementApi_View>>{
                this.MakeGridHeader(x => x.CreatePerson),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.BusinessDate),
                this.MakeGridHeader(x => x.SubmitDate),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.DocType),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.BizType),
                this.MakeGridHeader(x => x.InspectStatus),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeader(x => x.SourceSystemId),
                this.MakeGridHeader(x => x.LastUpdateTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<PurchaseReceivementApi_View> GetSearchQuery()
        {
            var query = DC.Set<PurchaseReceivement>()
                .Select(x => new PurchaseReceivementApi_View
                {
				    ID = x.ID,
                    CreatePerson = x.CreatePerson,
                    Name_view = x.Organization.Name,
                    BusinessDate = x.BusinessDate,
                    SubmitDate = x.SubmitDate,
                    DocNo = x.DocNo,
                    DocType = x.DocType,
                    Name_view2 = x.Supplier.Name,
                    BizType = x.BizType,
                    InspectStatus = x.InspectStatus,
                    Status = x.Status,
                    Memo = x.Memo,
                    SourceSystemId = x.SourceSystemId,
                    LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class PurchaseReceivementApi_View : PurchaseReceivement{
        [Display(Name = "名称")]
        public String Name_view { get; set; }
        [Display(Name = "_Model._BaseSupplier._Name")]
        public String Name_view2 { get; set; }

    }
}
