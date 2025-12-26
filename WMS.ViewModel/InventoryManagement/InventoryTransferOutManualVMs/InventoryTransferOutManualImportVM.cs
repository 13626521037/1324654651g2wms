
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs
{
    public partial class InventoryTransferOutManualTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryTransferOutManual._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.CreatePerson);
        [Display(Name = "_Model._InventoryTransferOutManual._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.OrganizationId);
        [Display(Name = "_Model._InventoryTransferOutManual._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.BusinessDate, true);
        [Display(Name = "_Model._InventoryTransferOutManual._SubmitDate")]
        public ExcelPropety SubmitDate_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.SubmitDate, true);
        [Display(Name = "_Model._InventoryTransferOutManual._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.DocNo);
        [Display(Name = "_Model._InventoryTransferOutManual._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.DocType);
        [Display(Name = "_Model._InventoryTransferOutManual._TransInOrganization")]
        public ExcelPropety TransInOrganization_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.TransInOrganizationId);
        [Display(Name = "_Model._InventoryTransferOutManual._TransInWh")]
        public ExcelPropety TransInWh_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.TransInWhId);
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutOrganization")]
        public ExcelPropety TransOutOrganization_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.TransOutOrganizationId);
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutWh")]
        public ExcelPropety TransOutWh_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.TransOutWhId);
        [Display(Name = "_Model._InventoryTransferOutManual._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.Status);
        [Display(Name = "_Model._InventoryTransferOutManual._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.Memo);
        [Display(Name = "_Model._InventoryTransferOutManual._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.SourceSystemId);
        [Display(Name = "_Model._InventoryTransferOutManual._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._InventoryTransferOutManual._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryTransferOutManual._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryTransferOutManual._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryTransferOutManual._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryTransferOutManual>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            TransInOrganization_Excel.DataType = ColumnDataType.ComboBox;
            TransInOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            TransInWh_Excel.DataType = ColumnDataType.ComboBox;
            TransInWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            TransOutOrganization_Excel.DataType = ColumnDataType.ComboBox;
            TransOutOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            TransOutWh_Excel.DataType = ColumnDataType.ComboBox;
            TransOutWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class InventoryTransferOutManualImportVM : BaseImportVM<InventoryTransferOutManualTemplateVM, InventoryTransferOutManual>
    {
            //import

    }

}