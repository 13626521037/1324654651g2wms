
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.PurchaseManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs
{
    public partial class PurchaseReceivementBatchVM : BaseBatchVM<PurchaseReceivement, PurchaseReceivement_BatchEdit>
    {
        public PurchaseReceivementBatchVM()
        {
            ListVM = new PurchaseReceivementListVM();
            LinkedVM = new PurchaseReceivement_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseReceivement_BatchEdit : BaseVM
    {

        
        public List<string> PurchaseManagementPurchaseReceivementBTempSelected { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._SubmitDate")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Supplier")]
        public Guid? SupplierId { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._BizType")]
        public BizTypeEnum? BizType { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._InspectStatus")]
        public PurchaseReceivementInspectStatusEnum? InspectStatus { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Status")]
        public PurchaseReceivementStatusEnum? Status { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}