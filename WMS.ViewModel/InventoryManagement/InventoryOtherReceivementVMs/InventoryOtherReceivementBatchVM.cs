
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs
{
    public partial class InventoryOtherReceivementBatchVM : BaseBatchVM<InventoryOtherReceivement, InventoryOtherReceivement_BatchEdit>
    {
        public InventoryOtherReceivementBatchVM()
        {
            ListVM = new InventoryOtherReceivementListVM();
            LinkedVM = new InventoryOtherReceivement_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryOtherReceivement_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryOtherReceivementBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._ErpID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._IsScrap")]
        public bool? IsScrap { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}