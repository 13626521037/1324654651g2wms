
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

namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementLineVMs
{
    public partial class PurchaseReceivementLineBatchVM : BaseBatchVM<PurchaseReceivementLine, PurchaseReceivementLine_BatchEdit>
    {
        public PurchaseReceivementLineBatchVM()
        {
            ListVM = new PurchaseReceivementLineListVM();
            LinkedVM = new PurchaseReceivementLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseReceivementLine_BatchEdit : BaseVM
    {

        
        public List<string> PurchaseManagementPurchaseReceivementLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}