
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

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs
{
    public partial class InventoryTransferOutDirectDocTypeBatchVM : BaseBatchVM<InventoryTransferOutDirectDocType, InventoryTransferOutDirectDocType_BatchEdit>
    {
        public InventoryTransferOutDirectDocTypeBatchVM()
        {
            ListVM = new InventoryTransferOutDirectDocTypeListVM();
            LinkedVM = new InventoryTransferOutDirectDocType_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryTransferOutDirectDocType_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryTransferOutDirectDocTypeBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}