
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseWhLocationVMs
{
    public partial class BaseWhLocationTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseWhLocation._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseWhLocation>(x => x.Code);
        [Display(Name = "_Model._BaseWhLocation._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseWhLocation>(x => x.Name);
        [Display(Name = "_Model._BaseWhLocation._WhArea")]
        public ExcelPropety WhArea_Excel = ExcelPropety.CreateProperty<BaseWhLocation>(x => x.WhAreaId);
        [Display(Name = "_Model._BaseWhLocation._AreaType")]
        public ExcelPropety AreaType_Excel = ExcelPropety.CreateProperty<BaseWhLocation>(x => x.AreaType);
        [Display(Name = "_Model._BaseWhLocation._Locked")]
        public ExcelPropety Locked_Excel = ExcelPropety.CreateProperty<BaseWhLocation>(x => x.Locked);
        [Display(Name = "_Model._BaseWhLocation._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<BaseWhLocation>(x => x.IsEffective);
        [Display(Name = "_Model._BaseWhLocation._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseWhLocation>(x => x.Memo);
        [Display(Name = "_Model._BaseWhLocation._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseWhLocation>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseWhLocation._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseWhLocation>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseWhLocation._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseWhLocation>(x => x.CreateBy);
        [Display(Name = "_Model._BaseWhLocation._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseWhLocation>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            WhArea_Excel.DataType = ColumnDataType.ComboBox;
            //WhArea_Excel.ListItems = DC.Set<BaseWhArea>().GetSelectListItems(Wtm, y => y.Code.CodeCombinName(y.Name));
            WhArea_Excel.ListItems = DC.Set<BaseWhArea>().GetSelectListItems(Wtm, y => y.Code);

        }

    }

    public class BaseWhLocationImportVM : BaseImportVM<BaseWhLocationTemplateVM, BaseWhLocation>
    {
            //import

    }

}