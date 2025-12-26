
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

namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs
{
    public partial class PurchaseOutsourcingReturnTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._PurchaseOutsourcingReturn._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.CreatePerson);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.OrganizationId);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.BusinessDate, true);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._SubmitDate")]
        public ExcelPropety SubmitDate_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.SubmitDate, true);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.DocNo);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.DocType);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Supplier")]
        public ExcelPropety Supplier_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.SupplierId);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.Status);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.Memo);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.SourceSystemId);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.CreateTime, true);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.UpdateTime, true);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.CreateBy);
        [Display(Name = "_Model._PurchaseOutsourcingReturn._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingReturn>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Supplier_Excel.DataType = ColumnDataType.ComboBox;
            Supplier_Excel.ListItems = DC.Set<BaseSupplier>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class PurchaseOutsourcingReturnImportVM : BaseImportVM<PurchaseOutsourcingReturnTemplateVM, PurchaseOutsourcingReturn>
    {
            //import

    }

}