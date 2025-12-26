
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;
using WMS.Model.BaseData;
namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs
{
    public partial class PurchaseOutsourcingIssueSearcher : BaseSearcher
    {
        
        public List<string> PurchaseManagementPurchaseOutsourcingIssueSTempSelected { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._SubmitDate")]
        public DateRange SubmitDate { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Supplier")]
        //public Guid? SupplierId { get; set; }
        public string Supplier { get; set; }
        public List<ComboSelectListItem> AllSuppliers { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Status")]
        public PurchaseOutsourcingIssueStatusEnum? Status { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._SourceSystemId")]
        public string SourceSystemId { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}