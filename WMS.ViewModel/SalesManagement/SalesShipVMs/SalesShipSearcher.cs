
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;
using WMS.Model.BaseData;
namespace WMS.ViewModel.SalesManagement.SalesShipVMs
{
    public partial class SalesShipSearcher : BaseSearcher
    {
        
        public List<string> SalesManagementSalesShipSTempSelected { get; set; }
        [Display(Name = "_Model._SalesShip._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._SalesShip._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._SalesShip._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._SalesShip._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._SalesShip._Customer")]
        public string Customer {  get; set; }
        [Display(Name = "_Model._SalesShip._Customer")]
        public Guid? CustomerId { get; set; }
        public List<ComboSelectListItem> AllCustomers { get; set; }
        [Display(Name = "_Model._SalesShip._Operators")]
        public string Operators { get; set; }
        [Display(Name = "_Model._SalesShip._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._SalesShip._Status")]
        public SalesShipStatusEnum? Status { get; set; }
        [Display(Name = "_Model._SalesShip._SubmitDate")]
        public DateRange SubmitDate { get; set; }
        [Display(Name = "_Model._SalesShip._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}