
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.SalesManagement.SalesRMAVMs
{
    public partial class SalesRMATemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._SalesRMA._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.CreatePerson);
        [Display(Name = "_Model._SalesRMA._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.OrganizationId);
        [Display(Name = "_Model._SalesRMA._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.BusinessDate, true);
        [Display(Name = "_Model._SalesRMA._ApproveDate")]
        public ExcelPropety ApproveDate_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.ApproveDate, true);
        [Display(Name = "_Model._SalesRMA._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.DocNo);
        [Display(Name = "_Model._SalesRMA._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.DocType);
        [Display(Name = "_Model._SalesRMA._Operators")]
        public ExcelPropety Operators_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.Operators);
        [Display(Name = "_Model._SalesRMA._Customer")]
        public ExcelPropety Customer_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.CustomerId);
        [Display(Name = "_Model._SalesRMA._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.Status);
        [Display(Name = "_Model._SalesRMA._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.Memo);
        [Display(Name = "_Model._SalesRMA._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.SourceSystemId);
        [Display(Name = "_Model._SalesRMA._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._SalesRMA._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.CreateTime, true);
        [Display(Name = "_Model._SalesRMA._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.UpdateTime, true);
        [Display(Name = "_Model._SalesRMA._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.CreateBy);
        [Display(Name = "_Model._SalesRMA._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<SalesRMA>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Customer_Excel.DataType = ColumnDataType.ComboBox;
            Customer_Excel.ListItems = DC.Set<BaseCustomer>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class SalesRMAImportVM : BaseImportVM<SalesRMATemplateVM, SalesRMA>
    {
            //import

    }

}