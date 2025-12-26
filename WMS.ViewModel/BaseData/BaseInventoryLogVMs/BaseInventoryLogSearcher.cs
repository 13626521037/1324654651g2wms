
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseInventoryLogVMs
{
    public partial class BaseInventoryLogSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseInventoryLogSTempSelected { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._OperationType")]
        public OperationTypeEnum? OperationType { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._DocNo")]
        public string DocNo { get; set; }
        //[Display(Name = "_Model._BaseInventoryLog._SourceInventory")]
        //public Guid? SourceInventoryId { get; set; }
        //public List<ComboSelectListItem> AllSourceInventorys { get; set; }
        //[Display(Name = "_Model._BaseInventoryLog._TargetInventory")]
        //public Guid? TargetInventoryId { get; set; }
        //public List<ComboSelectListItem> AllTargetInventorys { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._Qty")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._UpdateBy")]
        public string UpdateBy { get; set; }
        [Display(Name = "序列号")]
        public string Sn { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}