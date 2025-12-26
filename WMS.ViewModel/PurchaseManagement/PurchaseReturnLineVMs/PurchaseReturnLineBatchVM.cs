
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

namespace WMS.ViewModel.PurchaseManagement.PurchaseReturnLineVMs
{
    public partial class PurchaseReturnLineBatchVM : BaseBatchVM<PurchaseReturnLine, PurchaseReturnLine_BatchEdit>
    {
        public PurchaseReturnLineBatchVM()
        {
            ListVM = new PurchaseReturnLineListVM();
            LinkedVM = new PurchaseReturnLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseReturnLine_BatchEdit : BaseVM
    {

        
        public List<string> PurchaseManagementPurchaseReturnLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}