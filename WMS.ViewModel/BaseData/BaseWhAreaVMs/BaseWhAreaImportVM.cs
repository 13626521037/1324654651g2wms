
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseWhAreaVMs
{
    public partial class BaseWhAreaTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseWhArea._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseWhArea>(x => x.Code);
        [Display(Name = "_Model._BaseWhArea._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseWhArea>(x => x.Name);
        [Display(Name = "_Model._BaseWhArea._WareHouse")]
        public ExcelPropety WareHouse_Excel = ExcelPropety.CreateProperty<BaseWhArea>(x => x.WareHouseId);
        [Display(Name = "_Model._BaseWhArea._AreaType")]
        public ExcelPropety AreaType_Excel = ExcelPropety.CreateProperty<BaseWhArea>(x => x.AreaType);
        [Display(Name = "_Model._BaseWhArea._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<BaseWhArea>(x => x.IsEffective);
        [Display(Name = "_Model._BaseWhArea._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseWhArea>(x => x.Memo);
        [Display(Name = "_Model._BaseWhArea._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseWhArea>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseWhArea._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseWhArea>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseWhArea._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseWhArea>(x => x.CreateBy);
        [Display(Name = "_Model._BaseWhArea._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseWhArea>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            WareHouse_Excel.DataType = ColumnDataType.ComboBox;
            //WareHouse_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.Code.CodeCombinName(y.Name));
            WareHouse_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.Code);

        }

    }

    public class BaseWhAreaImportVM : BaseImportVM<BaseWhAreaTemplateVM, BaseWhArea>
    {
            //import

    }

}