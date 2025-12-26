using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs
{
    public partial class InventoryUnfreezeApiSearcher : BaseSearcher
    {
        [Display(Name = "_Model._InventoryUnfreeze._DocNo")]
        public String DocNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
