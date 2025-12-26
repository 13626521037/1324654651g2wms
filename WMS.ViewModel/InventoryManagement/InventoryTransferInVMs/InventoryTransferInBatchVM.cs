
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

namespace WMS.ViewModel.InventoryManagement.InventoryTransferInVMs
{
    public partial class InventoryTransferInBatchVM : BaseBatchVM<InventoryTransferIn, InventoryTransferIn_BatchEdit>
    {
        public InventoryTransferInBatchVM()
        {
            ListVM = new InventoryTransferInListVM();
            LinkedVM = new InventoryTransferIn_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryTransferIn_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryTransferInBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._ErpID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransInOrganization")]
        public Guid? TransInOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransInWh")]
        public Guid? TransInWhId { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransferOut")]
        public Guid? TransferOut { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransferOutType")]
        public InventoryTransferOutTypeEnum? TransferOutType { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._Status")]
        public InventoryTransferInStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}