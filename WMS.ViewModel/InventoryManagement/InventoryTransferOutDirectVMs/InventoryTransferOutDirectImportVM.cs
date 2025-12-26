
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

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs
{
    public partial class InventoryTransferOutDirectTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryTransferOutDirect._ErpID")]
        public ExcelPropety ErpID_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.ErpID);
        [Display(Name = "_Model._InventoryTransferOutDirect._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.BusinessDate, true);
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
        [Display(Name = "_Model._InventoryTransferOutDirect._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryTransferOutDirect._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryTransferOutDirect._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryTransferOutDirect._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirect>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            DocType_Excel.DataType = ColumnDataType.ComboBox;
            DocType_Excel.ListItems = DC.Set<InventoryTransferOutDirectDocType>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            TransInOrganization_Excel.DataType = ColumnDataType.ComboBox;
            TransInOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            TransInWh_Excel.DataType = ColumnDataType.ComboBox;
            TransInWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            TransOutWh_Excel.DataType = ColumnDataType.ComboBox;
            TransOutWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            TransOutOrganization_Excel.DataType = ColumnDataType.ComboBox;
            TransOutOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class InventoryTransferOutDirectImportVM : BaseImportVM<InventoryTransferOutDirectTemplateVM, InventoryTransferOutDirect>
    {
            //import

    }

}