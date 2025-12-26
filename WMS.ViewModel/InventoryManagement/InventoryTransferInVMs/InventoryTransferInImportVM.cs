
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

namespace WMS.ViewModel.InventoryManagement.InventoryTransferInVMs
{
    public partial class InventoryTransferInTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryTransferIn._ErpID")]
        public ExcelPropety ErpID_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.ErpID);
        [Display(Name = "_Model._InventoryTransferIn._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.BusinessDate, true);
        [Display(Name = "_Model._InventoryTransferIn._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.DocType);
        [Display(Name = "_Model._InventoryTransferIn._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.DocNo);
        [Display(Name = "_Model._InventoryTransferIn._TransInOrganization")]
        public ExcelPropety TransInOrganization_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.TransInOrganizationId);
        [Display(Name = "_Model._InventoryTransferIn._TransInWh")]
        public ExcelPropety TransInWh_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.TransInWhId);
        [Display(Name = "_Model._InventoryTransferIn._TransferOut")]
        public ExcelPropety TransferOut_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.TransferOut);
        [Display(Name = "_Model._InventoryTransferIn._TransferOutType")]
        public ExcelPropety TransferOutType_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.TransferOutType);
        [Display(Name = "_Model._InventoryTransferIn._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.Status);
        [Display(Name = "_Model._InventoryTransferIn._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.Memo);
        [Display(Name = "_Model._InventoryTransferIn._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryTransferIn._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryTransferIn._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryTransferIn._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryTransferIn>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            TransInOrganization_Excel.DataType = ColumnDataType.ComboBox;
            TransInOrganization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            TransInWh_Excel.DataType = ColumnDataType.ComboBox;
            TransInWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class InventoryTransferInImportVM : BaseImportVM<InventoryTransferInTemplateVM, InventoryTransferIn>
    {
            //import

    }

}