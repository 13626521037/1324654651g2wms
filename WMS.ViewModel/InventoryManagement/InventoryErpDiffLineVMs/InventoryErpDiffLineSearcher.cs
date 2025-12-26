
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryErpDiffLineVMs
{
    public partial class InventoryErpDiffLineSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryErpDiffLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}