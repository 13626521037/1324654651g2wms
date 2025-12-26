using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryFreezeVMs
{
    public partial class InventoryFreezeApiSearcher : BaseSearcher
    {
        [Display(Name = "_Model._InventoryFreeze._DocNo")]
        public String DocNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
