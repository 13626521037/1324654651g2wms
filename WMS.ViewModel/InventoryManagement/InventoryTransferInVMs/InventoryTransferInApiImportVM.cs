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


namespace WMS.ViewModel.InventoryManagement.InventoryTransferInVMs
{
    public partial class InventoryTransferInApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventoryTransferIn._ErpID")]
        public ExcelPropety ErpID_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.ErpID);
        [Display(Name = "_Model._InventoryTransferIn._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.BusinessDate);
        [Display(Name = "_Model._InventoryTransferIn._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.DocType);
        [Display(Name = "_Model._InventoryTransferIn._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.DocNo);
        [Display(Name = "_Model._InventoryTransferIn._TransInOrganization")]
        public ExcelPropety TransInOrganization_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.TransInOrganizationId);
        [Display(Name = "_Model._InventoryTransferIn._TransInWh")]
        public ExcelPropety TransInWh_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.TransInWhId);
        [Display(Name = "_Model._InventoryTransferIn._TransferOutType")]
        public ExcelPropety TransferOutType_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.TransferOutType);
        [Display(Name = "_Model._InventoryTransferIn._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.Status);
        [Display(Name = "_Model._InventoryTransferIn._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.Memo);

	    protected override void InitVM()
        {
            TransInOrganization_Excel.DataType = ColumnDataType.ComboBox;
            TransInOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.Name);
            TransInWh_Excel.DataType = ColumnDataType.ComboBox;
            TransInWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class InventoryTransferInApiImportVM : BaseImportVM<InventoryTransferInApiTemplateVM, InventoryTransferIn>
    {

    }

}
