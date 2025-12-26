
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

namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipLineVMs
{
    public partial class InventoryOtherShipLineBatchVM : BaseBatchVM<InventoryOtherShipLine, InventoryOtherShipLine_BatchEdit>
    {
        public InventoryOtherShipLineBatchVM()
        {
            ListVM = new InventoryOtherShipLineListVM();
            LinkedVM = new InventoryOtherShipLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryOtherShipLine_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryOtherShipLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}