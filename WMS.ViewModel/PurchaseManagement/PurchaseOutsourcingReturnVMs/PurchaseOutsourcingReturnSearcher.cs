
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
namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs
{
    public partial class PurchaseOutsourcingReturnSearcher : BaseSearcher
    {
        
        public List<string> PurchaseManagementPurchaseOutsourcingReturnSTempSelected { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._SubmitDate")]
        public DateRange SubmitDate { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Supplier")]
        public Guid? SupplierId { get; set; }
        public List<ComboSelectListItem> AllSuppliers { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Supplier")]
        public string Supplier { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Status")]
        public PurchaseOutsourcingReturnStatusEnum? Status { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._SourceSystemId")]
        public string SourceSystemId { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}