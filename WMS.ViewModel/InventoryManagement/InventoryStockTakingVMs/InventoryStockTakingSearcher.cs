
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
namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs
{
    public partial class InventoryStockTakingSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryStockTakingSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._ErpID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._ErpDocNo")]
        public string ErpDocNo { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Dimension")]
        public InventoryStockTakingDimensionEnum? Dimension { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Wh")]
        public Guid? WhId { get; set; }
        [Display(Name = "存储地点")]
        public string Wh { get; set; }
        [Display(Name = "库位")]
        public string Location { get; set; }
        public List<ComboSelectListItem> AllWhs { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._SubmitTime")]
        public DateRange SubmitTime { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._SubmitUser")]
        public string SubmitUser { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._ApproveTime")]
        public DateRange ApproveTime { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._ApproveUser")]
        public string ApproveUser { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._CloseTime")]
        public DateRange CloseTime { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._CloseUser")]
        public string CloseUser { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Status")]
        public InventoryStockTakingStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}