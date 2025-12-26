
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
namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs
{
    public partial class InventoryOtherShipSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryOtherShipSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._ErpID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._DocType")]
        public Guid? DocTypeId { get; set; }
        public List<ComboSelectListItem> AllDocTypes { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitOrganization")]
        public Guid? BenefitOrganizationId { get; set; }
        public List<ComboSelectListItem> AllBenefitOrganizations { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitDepartment")]
        public Guid? BenefitDepartmentId { get; set; }
        public List<ComboSelectListItem> AllBenefitDepartments { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitPerson")]
        public Guid? BenefitPersonId { get; set; }
        public List<ComboSelectListItem> AllBenefitPersons { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._Wh")]
        public Guid? WhId { get; set; }
        public List<ComboSelectListItem> AllWhs { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._OwnerOrganization")]
        public Guid? OwnerOrganizationId { get; set; }
        public List<ComboSelectListItem> AllOwnerOrganizations { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}