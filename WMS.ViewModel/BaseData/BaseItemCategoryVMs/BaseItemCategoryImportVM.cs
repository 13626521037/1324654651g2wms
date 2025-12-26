
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseItemCategoryVMs
{
    public partial class BaseItemCategoryTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseItemCategory._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.OrganizationId);
        [Display(Name = "_Model._BaseItemCategory._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.Code);
        [Display(Name = "_Model._BaseItemCategory._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.Name);
        [Display(Name = "_Model._BaseItemCategory._AnalysisType")]
        public ExcelPropety AnalysisType_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.AnalysisTypeId);
        [Display(Name = "_Model._BaseItemCategory._Department")]
        public ExcelPropety Department_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.DepartmentId);
        [Display(Name = "_Model._BaseItemCategory._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.SourceSystemId);
        [Display(Name = "_Model._BaseItemCategory._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._BaseItemCategory._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseItemCategory._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseItemCategory._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.CreateBy);
        [Display(Name = "_Model._BaseItemCategory._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.UpdateBy);
        [Display(Name = "_Model._BaseItemCategory._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<BaseItemCategory>(x => x.IsValid);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            AnalysisType_Excel.DataType = ColumnDataType.ComboBox;
            AnalysisType_Excel.ListItems = DC.Set<BaseAnalysisType>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Department_Excel.DataType = ColumnDataType.ComboBox;
            Department_Excel.ListItems = DC.Set<BaseDepartment>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class BaseItemCategoryImportVM : BaseImportVM<BaseItemCategoryTemplateVM, BaseItemCategory>
    {
            //import

    }

}