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


namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs
{
    public partial class InventoryOtherShipApiListVM : BasePagedListVM<InventoryOtherShipApi_View, InventoryOtherShipApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryOtherShipApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryOtherShipApi_View>>{
                this.MakeGridHeader(x => x.ErpID),
                this.MakeGridHeader(x => x.BusinessDate),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.Name_view3),
                this.MakeGridHeader(x => x.Name_view4),
                this.MakeGridHeader(x => x.Name_view5),
                this.MakeGridHeader(x => x.Name_view6),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryOtherShipApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryOtherShip>()
                .Select(x => new InventoryOtherShipApi_View
                {
				    ID = x.ID,
                    ErpID = x.ErpID,
                    BusinessDate = x.BusinessDate,
                    DocNo = x.DocNo,
                    Name_view = x.DocType.Name,
                    Name_view2 = x.BenefitOrganization.Name,
                    Name_view3 = x.BenefitDepartment.Name,
                    Name_view4 = x.BenefitPerson.Name,
                    Name_view5 = x.Wh.Name,
                    Name_view6 = x.OwnerOrganization.Name,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventoryOtherShipApi_View : InventoryOtherShip{
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
        [Display(Name = "名称")]
        public String Name_view6 { get; set; }

    }
}
