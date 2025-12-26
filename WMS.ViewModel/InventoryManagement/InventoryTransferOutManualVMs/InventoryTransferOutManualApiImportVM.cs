using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using WMS.Model;


namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs
{
    public partial class InventoryTransferOutManualApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventoryTransferOutManual._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.CreatePerson);
        [Display(Name = "_Model._InventoryTransferOutManual._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.OrganizationId);
        [Display(Name = "_Model._InventoryTransferOutManual._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.BusinessDate);
        [Display(Name = "_Model._InventoryTransferOutManual._SubmitDate")]
        public ExcelPropety SubmitDate_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.SubmitDate);
        [Display(Name = "_Model._InventoryTransferOutManual._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.DocNo);
        [Display(Name = "_Model._InventoryTransferOutManual._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.DocType);
        [Display(Name = "_Model._InventoryTransferOutManual._TransInOrganization")]
        public ExcelPropety TransInOrganization_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.TransInOrganizationId);
        [Display(Name = "_Model._InventoryTransferOutManual._TranInWh")]
        public ExcelPropety TransInWh_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.TransInWhId);
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutOrganization")]
        public ExcelPropety TransOutOrganization_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.TransOutOrganizationId);
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutWh")]
        public ExcelPropety TransOutWh_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.TransOutWhId);
        [Display(Name = "_Model._InventoryTransferOutManual._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.Status);
        [Display(Name = "_Model._InventoryTransferOutManual._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.Memo);
        [Display(Name = "来源系统主键")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.SourceSystemId);
        [Display(Name = "最后修改时间")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.LastUpdateTime);

	    protected override void InitVM()
        {
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.Name);
            TransInOrganization_Excel.DataType = ColumnDataType.ComboBox;
            TransInOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.Name);
            TransInWh_Excel.DataType = ColumnDataType.ComboBox;
            TransInWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.Name);
            TransOutOrganization_Excel.DataType = ColumnDataType.ComboBox;
            TransOutOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.Name);
            TransOutWh_Excel.DataType = ColumnDataType.ComboBox;
            TransOutWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class InventoryTransferOutManualApiImportVM : BaseImportVM<InventoryTransferOutManualApiTemplateVM, InventoryTransferOutManual>
    {

    }

}
