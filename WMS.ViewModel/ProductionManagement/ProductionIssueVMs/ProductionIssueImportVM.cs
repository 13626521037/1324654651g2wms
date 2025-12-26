
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.ProductionManagement.ProductionIssueVMs
{
    public partial class ProductionIssueTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._ProductionIssue._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.CreatePerson);
        [Display(Name = "_Model._ProductionIssue._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.OrganizationId);
        [Display(Name = "_Model._ProductionIssue._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.BusinessDate, true);
        [Display(Name = "_Model._ProductionIssue._SubmitDate")]
        public ExcelPropety SubmitDate_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.SubmitDate, true);
        [Display(Name = "_Model._ProductionIssue._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.DocNo);
        [Display(Name = "_Model._ProductionIssue._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.DocType);
        [Display(Name = "_Model._ProductionIssue._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.Status);
        [Display(Name = "_Model._ProductionIssue._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.Memo);
        [Display(Name = "_Model._ProductionIssue._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.SourceSystemId);
        [Display(Name = "_Model._ProductionIssue._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._ProductionIssue._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.CreateTime, true);
        [Display(Name = "_Model._ProductionIssue._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.UpdateTime, true);
        [Display(Name = "_Model._ProductionIssue._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.CreateBy);
        [Display(Name = "_Model._ProductionIssue._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class ProductionIssueImportVM : BaseImportVM<ProductionIssueTemplateVM, ProductionIssue>
    {
            //import

    }

}