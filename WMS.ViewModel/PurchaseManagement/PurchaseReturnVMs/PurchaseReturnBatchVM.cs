
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

namespace WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs
{
    public partial class PurchaseReturnBatchVM : BaseBatchVM<PurchaseReturn, PurchaseReturn_BatchEdit>
    {
        public PurchaseReturnBatchVM()
        {
            ListVM = new PurchaseReturnListVM();
            LinkedVM = new PurchaseReturn_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseReturn_BatchEdit : BaseVM
    {

        
        public List<string> PurchaseManagementPurchaseReturnBTempSelected { get; set; }
        [Display(Name = "_Model._PurchaseReturn._LastUpdatePerson")]
        public string LastUpdatePerson { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._PurchaseReturn._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._PurchaseReturn._CreateDate")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "_Model._PurchaseReturn._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Supplier")]
        public Guid? SupplierId { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Status")]
        public PurchaseReturnStatusEnum? Status { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}