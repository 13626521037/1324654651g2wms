
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

namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs
{
    public partial class InventoryOtherShipBatchVM : BaseBatchVM<InventoryOtherShip, InventoryOtherShip_BatchEdit>
    {
        public InventoryOtherShipBatchVM()
        {
            ListVM = new InventoryOtherShipListVM();
            LinkedVM = new InventoryOtherShip_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryOtherShip_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryOtherShipBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._ErpID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._DocType")]
        public Guid? DocTypeId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitOrganization")]
        public Guid? BenefitOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitDepartment")]
        public Guid? BenefitDepartmentId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitPerson")]
        public Guid? BenefitPersonId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._Wh")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._OwnerOrganization")]
        public Guid? OwnerOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}