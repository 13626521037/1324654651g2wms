using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;


namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs
{
    public partial class InventoryOtherShipApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventoryOtherShip._ErpID")]
        public ExcelPropety ErpID_Excel = ExcelPropety.CreateProperty<InventoryOtherShip>(x => x.ErpID);
        [Display(Name = "_Model._InventoryOtherShip._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<InventoryOtherShip>(x => x.BusinessDate);
        [Display(Name = "_Model._InventoryOtherShip._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryOtherShip>(x => x.DocNo);
        [Display(Name = "_Model._InventoryOtherShip._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<InventoryOtherShip>(x => x.DocTypeId);
        [Display(Name = "_Model._InventoryOtherShip._BenefitOrganization")]
        public ExcelPropety BenefitOrganization_Excel = ExcelPropety.CreateProperty<InventoryOtherShip>(x => x.BenefitOrganizationId);
        [Display(Name = "_Model._InventoryOtherShip._BenefitDepartment")]
        public ExcelPropety BenefitDepartment_Excel = ExcelPropety.CreateProperty<InventoryOtherShip>(x => x.BenefitDepartmentId);
        [Display(Name = "_Model._InventoryOtherShip._BenefitPerson")]
        public ExcelPropety BenefitPerson_Excel = ExcelPropety.CreateProperty<InventoryOtherShip>(x => x.BenefitPersonId);
        [Display(Name = "_Model._InventoryOtherShip._Wh")]
        public ExcelPropety Wh_Excel = ExcelPropety.CreateProperty<InventoryOtherShip>(x => x.WhId);
        [Display(Name = "_Model._InventoryOtherShip._OwnerOrganization")]
        public ExcelPropety OwnerOrganization_Excel = ExcelPropety.CreateProperty<InventoryOtherShip>(x => x.OwnerOrganizationId);
        [Display(Name = "_Model._InventoryOtherShip._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryOtherShip>(x => x.Memo);

	    protected override void InitVM()
        {
            DocType_Excel.DataType = ColumnDataType.ComboBox;
            DocType_Excel.ListItems = DC.Set<InventoryOtherShipDocType>().GetSelectListItems(Wtm, y => y.Name);
            BenefitOrganization_Excel.DataType = ColumnDataType.ComboBox;
            BenefitOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.Name);
            BenefitDepartment_Excel.DataType = ColumnDataType.ComboBox;
            BenefitDepartment_Excel.ListItems = DC.Set<BaseDepartment>().GetSelectListItems(Wtm, y => y.Name);
            BenefitPerson_Excel.DataType = ColumnDataType.ComboBox;
            BenefitPerson_Excel.ListItems = DC.Set<BaseOperator>().GetSelectListItems(Wtm, y => y.Name);
            Wh_Excel.DataType = ColumnDataType.ComboBox;
            Wh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.Name);
            OwnerOrganization_Excel.DataType = ColumnDataType.ComboBox;
            OwnerOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class InventoryOtherShipApiImportVM : BaseImportVM<InventoryOtherShipApiTemplateVM, InventoryOtherShip>
    {

    }

}
