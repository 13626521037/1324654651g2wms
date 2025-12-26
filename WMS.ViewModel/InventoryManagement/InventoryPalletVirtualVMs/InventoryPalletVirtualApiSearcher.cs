using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;


namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs
{
    public partial class InventoryPalletVirtualApiSearcher : BaseSearcher
    {
        [Display(Name = "_Model._InventoryPalletVirtual._Code")]
        public String Code { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Status")]
        public FrozenStatusEnum? Status { get; set; }

        protected override void InitVM()
        {
        }

    }
}
