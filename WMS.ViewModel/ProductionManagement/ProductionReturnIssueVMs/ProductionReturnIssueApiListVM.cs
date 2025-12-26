using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.ProductionManagement;
using WMS.Model.BaseData;
using WMS.Model;


namespace WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs
{
    public partial class ProductionReturnIssueApiListVM : BasePagedListVM<ProductionReturnIssueApi_View, ProductionReturnIssueApiSearcher>
    {

        protected override IEnumerable<IGridColumn<ProductionReturnIssueApi_View>> InitGridHeader()
        {
            return new List<GridColumn<ProductionReturnIssueApi_View>>{
                this.MakeGridHeader(x => x.CreatePerson),
                this.MakeGridHeader(x => x.CodeAndName_view),
                this.MakeGridHeader(x => x.BusinessDate),
                this.MakeGridHeader(x => x.SubmitDate),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.DocType),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeader(x => x.SourceSystemId),
                this.MakeGridHeader(x => x.LastUpdateTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<ProductionReturnIssueApi_View> GetSearchQuery()
        {
            var query = DC.Set<ProductionReturnIssue>()
                .Select(x => new ProductionReturnIssueApi_View
                {
				    ID = x.ID,
                    CreatePerson = x.CreatePerson,
                    CodeAndName_view = x.Organization.CodeAndName,
                    BusinessDate = x.BusinessDate,
                    SubmitDate = x.SubmitDate,
                    DocNo = x.DocNo,
                    DocType = x.DocType,
                    Status = x.Status,
                    Memo = x.Memo,
                    SourceSystemId = x.SourceSystemId,
                    LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class ProductionReturnIssueApi_View : ProductionReturnIssue{
        public String CodeAndName_view { get; set; }

    }
}
