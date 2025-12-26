using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model.BaseData;
using WMS.Model;


namespace WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs
{
    public partial class PurchaseReturnApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._PurchaseReturn._LastUpdatePerson")]
        public ExcelPropety LastUpdatePerson_Excel = ExcelPropety.CreateProperty<PurchaseReturn>(x => x.LastUpdatePerson);
        [Display(Name = "_Model._PurchaseReturn._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<PurchaseReturn>(x => x.OrganizationId);
        [Display(Name = "_Model._PurchaseReturn._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<PurchaseReturn>(x => x.BusinessDate);
        [Display(Name = "_Model._PurchaseReturn._CreateDate")]
        public ExcelPropety CreateDate_Excel = ExcelPropety.CreateProperty<PurchaseReturn>(x => x.CreateDate);
        [Display(Name = "_Model._PurchaseReturn._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<PurchaseReturn>(x => x.DocNo);
        [Display(Name = "_Model._PurchaseReturn._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<PurchaseReturn>(x => x.DocType);
        [Display(Name = "_Model._PurchaseReturn._Supplier")]
        public ExcelPropety Supplier_Excel = ExcelPropety.CreateProperty<PurchaseReturn>(x => x.SupplierId);
        [Display(Name = "_Model._PurchaseReturn._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<PurchaseReturn>(x => x.Status);
        [Display(Name = "_Model._PurchaseReturn._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<PurchaseReturn>(x => x.Memo);
        [Display(Name = "来源系统主键")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<PurchaseReturn>(x => x.SourceSystemId);
        [Display(Name = "最后修改时间")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<PurchaseReturn>(x => x.LastUpdateTime);

	    protected override void InitVM()
        {
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.Name);
            Supplier_Excel.DataType = ColumnDataType.ComboBox;
            Supplier_Excel.ListItems = DC.Set<BaseSupplier>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class PurchaseReturnApiImportVM : BaseImportVM<PurchaseReturnApiTemplateVM, PurchaseReturn>
    {

    }

}
