
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs
{
    public partial class PurchaseReceivementTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._PurchaseReceivement._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.CreatePerson);
        [Display(Name = "_Model._PurchaseReceivement._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.OrganizationId);
        [Display(Name = "_Model._PurchaseReceivement._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.BusinessDate, true);
        [Display(Name = "_Model._PurchaseReceivement._SubmitDate")]
        public ExcelPropety SubmitDate_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.SubmitDate, true);
        [Display(Name = "_Model._PurchaseReceivement._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.DocNo);
        [Display(Name = "_Model._PurchaseReceivement._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.DocType);
        [Display(Name = "_Model._PurchaseReceivement._Supplier")]
        public ExcelPropety Supplier_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.SupplierId);
        [Display(Name = "_Model._PurchaseReceivement._BizType")]
        public ExcelPropety BizType_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.BizType);
        [Display(Name = "_Model._PurchaseReceivement._InspectStatus")]
        public ExcelPropety InspectStatus_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.InspectStatus);
        [Display(Name = "_Model._PurchaseReceivement._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.Status);
        [Display(Name = "_Model._PurchaseReceivement._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.Memo);
        [Display(Name = "_Model._PurchaseReceivement._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.SourceSystemId);
        [Display(Name = "_Model._PurchaseReceivement._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._PurchaseReceivement._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.CreateTime, true);
        [Display(Name = "_Model._PurchaseReceivement._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.UpdateTime, true);
        [Display(Name = "_Model._PurchaseReceivement._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.CreateBy);
        [Display(Name = "_Model._PurchaseReceivement._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<PurchaseReceivement>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Supplier_Excel.DataType = ColumnDataType.ComboBox;
            Supplier_Excel.ListItems = DC.Set<BaseSupplier>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class PurchaseReceivementImportVM : BaseImportVM<PurchaseReceivementTemplateVM, PurchaseReceivement>
    {
            //import

    }

}