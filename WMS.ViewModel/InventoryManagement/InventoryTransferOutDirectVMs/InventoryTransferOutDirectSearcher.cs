
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
namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs
{
    public partial class InventoryTransferOutDirectSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryTransferOutDirectSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._ErpID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._DocType")]
        public Guid? DocTypeId { get; set; }
        public List<ComboSelectListItem> AllDocTypes { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInOrganization")]
        public Guid? TransInOrganizationId { get; set; }
        public List<ComboSelectListItem> AllTransInOrganizations { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInWh")]
        public Guid? TransInWhId { get; set; }
        public List<ComboSelectListItem> AllTransInWhs { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutWh")]
        public Guid? TransOutWhId { get; set; }
        public List<ComboSelectListItem> AllTransOutWhs { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutOrganization")]
        public Guid? TransOutOrganizationId { get; set; }
        public List<ComboSelectListItem> AllTransOutOrganizations { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}