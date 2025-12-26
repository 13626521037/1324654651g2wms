
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

namespace WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs
{
    public partial class InventoryErpDiffBatchVM : BaseBatchVM<InventoryErpDiff, InventoryErpDiff_BatchEdit>
    {
        public InventoryErpDiffBatchVM()
        {
            ListVM = new InventoryErpDiffListVM();
            LinkedVM = new InventoryErpDiff_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryErpDiff_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryErpDiffBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Wh")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Item")]
        public Guid? ItemId { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Seiban")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._WmsQty")]
        public decimal? WmsQty { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._ErpQty")]
        public decimal? ErpQty { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}