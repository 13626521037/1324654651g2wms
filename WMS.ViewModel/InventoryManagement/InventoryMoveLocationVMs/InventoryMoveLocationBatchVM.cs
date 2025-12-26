
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
using WMS.Model.BaseData;

namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs
{
    public partial class InventoryMoveLocationBatchVM : BaseBatchVM<InventoryMoveLocation, InventoryMoveLocation_BatchEdit>
    {
        public InventoryMoveLocationBatchVM()
        {
            ListVM = new InventoryMoveLocationListVM();
            LinkedVM = new InventoryMoveLocation_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryMoveLocation_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryMoveLocationBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._InWhLocation")]
        public Guid? InWhLocationId { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}