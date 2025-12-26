
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseInventoryVMs
{
    public partial class BaseInventorySearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseInventorySTempSelected { get; set; }
        //[Display(Name = "_Model._BaseInventory._ItemMaster")]
        //public Guid? ItemMasterId { get; set; }
        //public List<ComboSelectListItem> AllItemMasters { get; set; }
        [Display(Name = "料号")]
        public string ItemCode { get; set; }
        //[Display(Name = "_Model._BaseInventory._WhLocation")]
        //public Guid? WhLocationId { get; set; }
        //public List<ComboSelectListItem> AllWhLocations { get; set; }
        [Display(Name = "存储地点")]
        public string Wh { get; set; }
        [Display(Name = "库区")]
        public string Area { get; set; }
        [Display(Name = "库位")]
        public string Location { get; set; }
        [Display(Name = "_Model._BaseInventory._BatchNumber")]
        public string BatchNumber { get; set; }
        [Display(Name = "_Model._BaseInventory._SerialNumber")]
        public string SerialNumber { get; set; }
        [Display(Name = "_Model._BaseInventory._Seiban")]
        public string Seiban { get; set; }
        [Display(Name = "番号随机码")]
        public string SeibanRandom { get; set; }
        [Display(Name = "_Model._BaseInventory._Qty")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._BaseInventory._IsAbandoned")]
        public bool? IsAbandoned { get; set; }
        [Display(Name = "_Model._BaseInventory._ItemSourceType")]
        public ItemSourceTypeEnum? ItemSourceType { get; set; }
        [Display(Name = "_Model._BaseInventory._FrozenStatus")]
        public FrozenStatusEnum? FrozenStatus { get; set; }
        [Display(Name = "隐藏零库存")]
        public bool? HideZero { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}