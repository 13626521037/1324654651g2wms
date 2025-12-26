
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseDepartmentVMs
{
    public partial class BaseDepartmentTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseDepartment._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.Code);
        [Display(Name = "_Model._BaseDepartment._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.Name);
        [Display(Name = "_Model._BaseDepartment._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.OrganizationId);
        [Display(Name = "_Model._BaseDepartment._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.IsEffective);
        [Display(Name = "_Model._BaseDepartment._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.Memo);
        [Display(Name = "_Model._BaseDepartment._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.SourceSystemId);
        [Display(Name = "_Model._BaseDepartment._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._BaseDepartment._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseDepartment._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseDepartment._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.CreateBy);
        [Display(Name = "_Model._BaseDepartment._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.UpdateBy);
        [Display(Name = "_Model._BaseDepartment._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<BaseDepartment>(x => x.IsValid);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class BaseDepartmentImportVM : BaseImportVM<BaseDepartmentTemplateVM, BaseDepartment>
    {
            //import

    }

}