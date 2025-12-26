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


namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs
{
    public partial class ProductionRcvRptApiListVM : BasePagedListVM<ProductionRcvRptApi_View, ProductionRcvRptApiSearcher>
    {

        protected override IEnumerable<IGridColumn<ProductionRcvRptApi_View>> InitGridHeader()
        {
            return new List<GridColumn<ProductionRcvRptApi_View>>{
                this.MakeGridHeader(x => x.ErpID),
                this.MakeGridHeader(x => x.CodeAndName_view),
                this.MakeGridHeader(x => x.BusinessDate),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.CodeAndName_view2),
                this.MakeGridHeader(x => x.CodeAndName_view3),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<ProductionRcvRptApi_View> GetSearchQuery()
        {
            var query = DC.Set<ProductionRcvRpt>()
                .Select(x => new ProductionRcvRptApi_View
                {
				    ID = x.ID,
                    ErpID = x.ErpID,
                    CodeAndName_view = x.Organization.CodeAndName,
                    BusinessDate = x.BusinessDate,
                    DocNo = x.DocNo,
                    CodeAndName_view2 = x.Wh.CodeAndName,
                    CodeAndName_view3 = x.OrderWh.CodeAndName,
                    Status = x.Status,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class ProductionRcvRptApi_View : ProductionRcvRpt{
        public String CodeAndName_view { get; set; }
        public String CodeAndName_view2 { get; set; }
        public String CodeAndName_view3 { get; set; }

    }
}
