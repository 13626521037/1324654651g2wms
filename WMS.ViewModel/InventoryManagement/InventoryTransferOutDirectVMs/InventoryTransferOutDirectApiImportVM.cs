using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;


namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs
{
    public partial class InventoryTransferOutDirectApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventoryTransferOutDirect._ErpID")]
        public ExcelPropety ErpID_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.ErpID);
        [Display(Name = "_Model._InventoryTransferOutDirect._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.BusinessDate);
        [Display(Name = "_Model._InventoryTransferOutDirect._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.DocTypeId);
        [Display(Name = "_Model._InventoryTransferOutDirect._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.DocNo);
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInOrganization")]
        public ExcelPropety TransInOrganization_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.TransInOrganizationId);
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInWh")]
        public ExcelPropety TransInWh_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.TransInWhId);
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutWh")]
        public ExcelPropety TransOutWh_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.TransOutWhId);
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutOrganization")]
        public ExcelPropety TransOutOrganization_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.TransOutOrganizationId);
        [Display(Name = "_Model._InventoryTransferOutDirect._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.Memo);

	    protected override void InitVM()
        {
            DocType_Excel.DataType = ColumnDataType.ComboBox;
            DocType_Excel.ListItems = DC.Set<InventoryTransferOutDirectDocType>().GetSelectListItems(Wtm, y => y.Name);
            TransInOrganization_Excel.DataType = ColumnDataType.ComboBox;
            TransInOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.Name);
            TransInWh_Excel.DataType = ColumnDataType.ComboBox;
            TransInWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.Name);
            TransOutWh_Excel.DataType = ColumnDataType.ComboBox;
            TransOutWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.Name);
            TransOutOrganization_Excel.DataType = ColumnDataType.ComboBox;
            TransOutOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class InventoryTransferOutDirectApiImportVM : BaseImportVM<InventoryTransferOutDirectApiTemplateVM, InventoryTransferOutDirect>
    {

    }

}
