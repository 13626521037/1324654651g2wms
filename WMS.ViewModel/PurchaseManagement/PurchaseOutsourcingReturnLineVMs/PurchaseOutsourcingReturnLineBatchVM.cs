
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

namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnLineVMs
{
    public partial class PurchaseOutsourcingReturnLineBatchVM : BaseBatchVM<PurchaseOutsourcingReturnLine, PurchaseOutsourcingReturnLine_BatchEdit>
    {
        public PurchaseOutsourcingReturnLineBatchVM()
        {
            ListVM = new PurchaseOutsourcingReturnLineListVM();
            LinkedVM = new PurchaseOutsourcingReturnLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseOutsourcingReturnLine_BatchEdit : BaseVM
    {

        
        public List<string> PurchaseManagementPurchaseOutsourcingReturnLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}