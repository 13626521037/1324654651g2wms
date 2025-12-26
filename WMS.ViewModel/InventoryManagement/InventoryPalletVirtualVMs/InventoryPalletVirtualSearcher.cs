
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;
namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs
{
    public partial class InventoryPalletVirtualSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryPalletVirtualSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Status")]
        public FrozenStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Location")]
        public Guid? LocationId { get; set; }
        public List<ComboSelectListItem> AllLocations { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._SysVersion")]
        public int? SysVersion { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}