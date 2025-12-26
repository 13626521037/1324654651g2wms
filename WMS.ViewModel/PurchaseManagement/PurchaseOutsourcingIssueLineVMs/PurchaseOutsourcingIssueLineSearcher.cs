
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;
namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueLineVMs
{
    public partial class PurchaseOutsourcingIssueLineSearcher : BaseSearcher
    {
        
        public List<string> PurchaseManagementPurchaseOutsourcingIssueLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}