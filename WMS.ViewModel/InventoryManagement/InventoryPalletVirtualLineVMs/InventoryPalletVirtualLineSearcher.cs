
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualLineVMs
{
    public partial class InventoryPalletVirtualLineSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryPalletVirtualLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}