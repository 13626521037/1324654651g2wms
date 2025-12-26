
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

namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs
{
    public partial class PurchaseOutsourcingReturnBatchVM : BaseBatchVM<PurchaseOutsourcingReturn, PurchaseOutsourcingReturn_BatchEdit>
    {
        public PurchaseOutsourcingReturnBatchVM()
        {
            ListVM = new PurchaseOutsourcingReturnListVM();
            LinkedVM = new PurchaseOutsourcingReturn_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseOutsourcingReturn_BatchEdit : BaseVM
    {

        
        public List<string> PurchaseManagementPurchaseOutsourcingReturnBTempSelected { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._SubmitDate")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Supplier")]
        public Guid? SupplierId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Status")]
        public PurchaseOutsourcingReturnStatusEnum? Status { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}