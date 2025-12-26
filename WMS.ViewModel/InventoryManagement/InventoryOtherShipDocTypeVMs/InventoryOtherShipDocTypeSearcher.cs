
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
namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs
{
    public partial class InventoryOtherShipDocTypeSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryOtherShipDocTypeSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}