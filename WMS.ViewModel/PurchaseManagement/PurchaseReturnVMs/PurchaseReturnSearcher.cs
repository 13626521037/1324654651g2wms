
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
namespace WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs
{
    public partial class PurchaseReturnSearcher : BaseSearcher
    {
        
        public List<string> PurchaseManagementPurchaseReturnSTempSelected { get; set; }
        [Display(Name = "_Model._PurchaseReturn._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._PurchaseReturn._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._PurchaseReturn._LastUpdatePerson")]
        public string LastUpdatePerson { get; set; }
        [Display(Name = "_Model._PurchaseReturn._CreateDate")]
        public DateRange CreateDate { get; set; }
        [Display(Name = "_Model._PurchaseReturn._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Supplier")]
        public Guid? SupplierId { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Supplier")]
        public string Supplier { get; set; }
        public List<ComboSelectListItem> AllSuppliers { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Status")]
        public PurchaseReturnStatusEnum? Status { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}