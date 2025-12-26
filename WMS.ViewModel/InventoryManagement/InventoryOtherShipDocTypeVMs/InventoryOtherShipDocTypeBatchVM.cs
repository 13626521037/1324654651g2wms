
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

namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs
{
    public partial class InventoryOtherShipDocTypeBatchVM : BaseBatchVM<InventoryOtherShipDocType, InventoryOtherShipDocType_BatchEdit>
    {
        public InventoryOtherShipDocTypeBatchVM()
        {
            ListVM = new InventoryOtherShipDocTypeListVM();
            LinkedVM = new InventoryOtherShipDocType_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryOtherShipDocType_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryOtherShipDocTypeBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}