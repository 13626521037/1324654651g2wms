
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
namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs
{
    public partial class InventoryTransferOutManualSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryTransferOutManualSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._SubmitDate")]
        public DateRange SubmitDate { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransInOrganization")]
        public Guid? TransInOrganizationId { get; set; }
        public List<ComboSelectListItem> AllTransInOrganizations { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransInWh")]
        public Guid? TransInWhId { get; set; }
        public List<ComboSelectListItem> AllTransInWhs { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutOrganization")]
        public Guid? TransOutOrganizationId { get; set; }
        public List<ComboSelectListItem> AllTransOutOrganizations { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutWh")]
        public Guid? TransOutWhId { get; set; }
        public List<ComboSelectListItem> AllTransOutWhs { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._Status")]
        public InventoryTransferOutManualStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}