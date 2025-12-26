using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.SalesManagement;
using WMS.Model.BaseData;
using WMS.Model;


namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs
{
    public partial class SalesReturnReceivementApiListVM : BasePagedListVM<SalesReturnReceivementApi_View, SalesReturnReceivementApiSearcher>
    {

        protected override IEnumerable<IGridColumn<SalesReturnReceivementApi_View>> InitGridHeader()
        {
            return new List<GridColumn<SalesReturnReceivementApi_View>>{
                this.MakeGridHeader(x => x.CreatePerson),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.BusinessDate),
                this.MakeGridHeader(x => x.SubmitDate),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.DocType),
                this.MakeGridHeader(x => x.EnglishShortName_view),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeader(x => x.SourceSystemId),
                this.MakeGridHeader(x => x.LastUpdateTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<SalesReturnReceivementApi_View> GetSearchQuery()
        {
            var query = DC.Set<SalesReturnReceivement>()
                .Select(x => new SalesReturnReceivementApi_View
                {
				    ID = x.ID,
                    CreatePerson = x.CreatePerson,
                    Name_view = x.Organization.Name,
                    BusinessDate = x.BusinessDate,
                    SubmitDate = x.SubmitDate,
                    DocNo = x.DocNo,
                    DocType = x.DocType,
                    EnglishShortName_view = x.Customer.EnglishShortName,
                    Status = x.Status,
                    Memo = x.Memo,
                    SourceSystemId = x.SourceSystemId,
                    LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class SalesReturnReceivementApi_View : SalesReturnReceivement{
        [Display(Name = "名称")]
        public String Name_view { get; set; }
        [Display(Name = "_Model._BaseCustomer._EnglishShortName")]
        public String EnglishShortName_view { get; set; }

    }
}
