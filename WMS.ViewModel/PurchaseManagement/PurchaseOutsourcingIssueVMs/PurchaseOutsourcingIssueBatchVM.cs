
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

namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs
{
    public partial class PurchaseOutsourcingIssueBatchVM : BaseBatchVM<PurchaseOutsourcingIssue, PurchaseOutsourcingIssue_BatchEdit>
    {
        public PurchaseOutsourcingIssueBatchVM()
        {
            ListVM = new PurchaseOutsourcingIssueListVM();
            LinkedVM = new PurchaseOutsourcingIssue_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseOutsourcingIssue_BatchEdit : BaseVM
    {

        
        public List<string> PurchaseManagementPurchaseOutsourcingIssueBTempSelected { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._SubmitDate")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Supplier")]
        public Guid? SupplierId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Status")]
        public PurchaseOutsourcingIssueStatusEnum? Status { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}