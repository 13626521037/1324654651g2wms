
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingErpDiffLineVMs
{
    public partial class InventoryStockTakingErpDiffLineSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryStockTakingErpDiffLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}