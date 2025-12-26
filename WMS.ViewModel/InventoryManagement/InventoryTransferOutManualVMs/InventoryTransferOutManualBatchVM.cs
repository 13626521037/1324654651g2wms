
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs
{
    public partial class InventoryTransferOutManualBatchVM : BaseBatchVM<InventoryTransferOutManual, InventoryTransferOutManual_BatchEdit>
    {
        public InventoryTransferOutManualBatchVM()
        {
            ListVM = new InventoryTransferOutManualListVM();
            LinkedVM = new InventoryTransferOutManual_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryTransferOutManual_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryTransferOutManualBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._SubmitDate")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransInOrganization")]
        public Guid? TransInOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransInWh")]
        public Guid? TransInWhId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutOrganization")]
        public Guid? TransOutOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutWh")]
        public Guid? TransOutWhId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._Status")]
        public InventoryTransferOutManualStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}