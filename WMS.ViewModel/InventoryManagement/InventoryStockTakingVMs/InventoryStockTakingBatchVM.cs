
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

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs
{
    public partial class InventoryStockTakingBatchVM : BaseBatchVM<InventoryStockTaking, InventoryStockTaking_BatchEdit>
    {
        public InventoryStockTakingBatchVM()
        {
            ListVM = new InventoryStockTakingListVM();
            LinkedVM = new InventoryStockTaking_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryStockTaking_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryStockTakingBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._ErpID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._ErpDocNo")]
        public short? ErpDocNo { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Dimension")]
        public InventoryStockTakingDimensionEnum? Dimension { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Wh")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._SubmitTime")]
        public DateTime? SubmitTime { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._SubmitUser")]
        public string SubmitUser { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._ApproveTime")]
        public DateTime? ApproveTime { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._ApproveUser")]
        public string ApproveUser { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._CloseTime")]
        public DateTime? CloseTime { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._CloseUser")]
        public string CloseUser { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Status")]
        public InventoryStockTakingStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}