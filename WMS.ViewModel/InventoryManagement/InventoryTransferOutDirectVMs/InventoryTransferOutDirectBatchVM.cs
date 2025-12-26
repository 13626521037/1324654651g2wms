
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

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs
{
    public partial class InventoryTransferOutDirectBatchVM : BaseBatchVM<InventoryTransferOutDirect, InventoryTransferOutDirect_BatchEdit>
    {
        public InventoryTransferOutDirectBatchVM()
        {
            ListVM = new InventoryTransferOutDirectListVM();
            LinkedVM = new InventoryTransferOutDirect_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryTransferOutDirect_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryTransferOutDirectBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._ErpID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._DocType")]
        public Guid? DocTypeId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInOrganization")]
        public Guid? TransInOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInWh")]
        public Guid? TransInWhId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutWh")]
        public Guid? TransOutWhId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutOrganization")]
        public Guid? TransOutOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}