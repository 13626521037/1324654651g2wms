
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs
{
    public partial class BaseDocInventoryRelationSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseDocInventoryRelationSTempSelected { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._DocType")]
        public DocTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._Inventory")]
        public Guid? InventoryId { get; set; }
        public List<ComboSelectListItem> AllInventorys { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._BusinessId")]
        public Guid? BusinessId { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._Qty")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}