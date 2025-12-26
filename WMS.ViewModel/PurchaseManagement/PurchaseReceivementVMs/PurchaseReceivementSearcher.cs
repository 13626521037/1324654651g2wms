
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
namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs
{
    public partial class PurchaseReceivementSearcher : BaseSearcher
    {
        
        public List<string> PurchaseManagementPurchaseReceivementSTempSelected { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._SubmitDate")]
        public DateRange SubmitDate { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._DocType")]
        public string DocType { get; set; }
        //[Display(Name = "_Model._PurchaseReceivement._Supplier")]
        //public Guid? SupplierId { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Supplier")]
        public string Supplier { get; set; }
        public List<ComboSelectListItem> AllSuppliers { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._BizType")]
        public BizTypeEnum? BizType { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._InspectStatus")]
        public PurchaseReceivementInspectStatusEnum? InspectStatus { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Status")]
        public PurchaseReceivementStatusEnum? Status { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}