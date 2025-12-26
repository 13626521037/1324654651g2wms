
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

namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs
{
    public partial class InventoryPalletVirtualBatchVM : BaseBatchVM<InventoryPalletVirtual, InventoryPalletVirtual_BatchEdit>
    {
        public InventoryPalletVirtualBatchVM()
        {
            ListVM = new InventoryPalletVirtualListVM();
            LinkedVM = new InventoryPalletVirtual_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryPalletVirtual_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryPalletVirtualBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Status")]
        public FrozenStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Location")]
        public Guid? LocationId { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._SysVersion")]
        public int? SysVersion { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}