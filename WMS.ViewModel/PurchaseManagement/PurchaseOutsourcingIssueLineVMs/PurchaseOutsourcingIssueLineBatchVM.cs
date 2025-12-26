
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

namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueLineVMs
{
    public partial class PurchaseOutsourcingIssueLineBatchVM : BaseBatchVM<PurchaseOutsourcingIssueLine, PurchaseOutsourcingIssueLine_BatchEdit>
    {
        public PurchaseOutsourcingIssueLineBatchVM()
        {
            ListVM = new PurchaseOutsourcingIssueLineListVM();
            LinkedVM = new PurchaseOutsourcingIssueLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseOutsourcingIssueLine_BatchEdit : BaseVM
    {

        
        public List<string> PurchaseManagementPurchaseOutsourcingIssueLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}