
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
using WMS.Model.BaseData;
namespace WMS.ViewModel.KnifeManagement.KnifeVMs
{
    public partial class KnifeSearcher : BaseSearcher
    {

        public List<string> KnifeManagementKnifeSTempSelected { get; set; }
        [Display(Name = "_Model._Knife._CreatedDate")]
        public DateRange CreatedDate { get; set; }
        [Display(Name = "序列号")]
        public string SerialNumber { get; set; }
        [Display(Name = "_Model._Knife._Status")]
        public KnifeStatusEnum? Status { get; set; }
        [Display(Name = "_Model._Knife._CurrentCheckOutBy")]
        public Guid? CurrentCheckOutById { get; set; }
        public List<ComboSelectListItem> AllCurrentCheckOutBys { get; set; }
        [Display(Name = "_Model._Knife._HandledBy")]
        public string HandledById { get; set; }
        public List<ComboSelectListItem> AllHandledBys { get; set; }
        [Display(Name = "_Model._Knife._LastOperationDate")]
        public DateRange LastOperationDate { get; set; }
        [Display(Name = "_Model._Knife._WhLocation")]
        public Guid? WhLocationId { get; set; }
        [Display(Name = "存储地点")]
        public Guid? WareHouseId { get; set; }
        public List<ComboSelectListItem> AllWhLocations { get; set; }
        [Display(Name = "_Model._Knife._GrindCount")]
        public decimal? GrindCount { get; set; }
        [Display(Name = "_Model._Knife._InitialLife")]
        public decimal? InitialLife { get; set; }
        [Display(Name = "_Model._Knife._CurrentLife")]
        public decimal? CurrentLife { get; set; }
        [Display(Name = "_Model._Knife._TotalUsedDays")]
        public decimal? TotalUsedDays { get; set; }
        [Display(Name = "_Model._Knife._RemainingUsedDays")]
        public decimal? RemainingUsedDays { get; set; }
        [Display(Name = "_Model._Knife._ItemMaster")]
        public string? ItemMasterCode { get; set; }
        public List<ComboSelectListItem> AllItemMasters { get; set; }
        [Display(Name = "_Model._Knife._MiscShipLineID")]
        public string MiscShipLineID { get; set; }
        [Display(Name = "_Model._Knife._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._Knife._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._Knife._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._Knife._UpdateBy")]
        public string UpdateBy { get; set; }
        [Display(Name = "在库状态")]
        public KnifeInStockStatusEnum? InStockStatus { get; set; }

        [Display(Name = "实际料号")]
        public string ActualItemCode { get; set; }
        [Display(Name = "修磨刀具号")]
        public string GrindKnifeNO { get; set; }
        

        protected override void InitVM()
        {


        }
    }

}