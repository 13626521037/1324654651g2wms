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


namespace WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs
{
    public partial class PurchaseReturnApiListVM : BasePagedListVM<PurchaseReturnApi_View, PurchaseReturnApiSearcher>
    {

        protected override IEnumerable<IGridColumn<PurchaseReturnApi_View>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseReturnApi_View>>{
                this.MakeGridHeader(x => x.LastUpdatePerson),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.BusinessDate),
                this.MakeGridHeader(x => x.CreateDate),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.DocType),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeader(x => x.SourceSystemId),
                this.MakeGridHeader(x => x.LastUpdateTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<PurchaseReturnApi_View> GetSearchQuery()
        {
            var query = DC.Set<PurchaseReturn>()
                .Select(x => new PurchaseReturnApi_View
                {
				    ID = x.ID,
                    LastUpdatePerson = x.LastUpdatePerson,
                    Name_view = x.Organization.Name,
                    BusinessDate = x.BusinessDate,
                    CreateDate = x.CreateDate,
                    DocNo = x.DocNo,
                    DocType = x.DocType,
                    Name_view2 = x.Supplier.Name,
                    Status = x.Status,
                    Memo = x.Memo,
                    SourceSystemId = x.SourceSystemId,
                    LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class PurchaseReturnApi_View : PurchaseReturn{
        [Display(Name = "名称")]
        public String Name_view { get; set; }
        [Display(Name = "_Model._BaseSupplier._Name")]
        public String Name_view2 { get; set; }

    }
}
