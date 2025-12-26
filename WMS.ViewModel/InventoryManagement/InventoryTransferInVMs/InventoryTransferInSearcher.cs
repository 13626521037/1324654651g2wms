
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
namespace WMS.ViewModel.InventoryManagement.InventoryTransferInVMs
{
    public partial class InventoryTransferInSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryTransferInSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._ErpID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransInOrganization")]
        public Guid? TransInOrganizationId { get; set; }
        public List<ComboSelectListItem> AllTransInOrganizations { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransInWh")]
        public Guid? TransInWhId { get; set; }
        public List<ComboSelectListItem> AllTransInWhs { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransferOut")]
        public Guid? TransferOut { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransferOutType")]
        public InventoryTransferOutTypeEnum? TransferOutType { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._Status")]
        public InventoryTransferInStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}