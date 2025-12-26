
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipLineVMs
{
    public partial class InventoryOtherShipLineSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryOtherShipLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}