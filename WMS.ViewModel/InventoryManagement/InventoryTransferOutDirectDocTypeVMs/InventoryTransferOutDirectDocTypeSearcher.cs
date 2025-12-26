
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
namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs
{
    public partial class InventoryTransferOutDirectDocTypeSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryTransferOutDirectDocTypeSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "编码")]
        public string Code { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}