
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectLineVMs
{
    public partial class InventoryTransferOutDirectLineSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryTransferOutDirectLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}