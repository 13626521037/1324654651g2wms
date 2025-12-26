
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseUserWhRelationVMs
{
    public partial class BaseUserWhRelationTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseUserWhRelation._User")]
        public ExcelPropety User_Excel = ExcelPropety.CreateProperty<BaseUserWhRelation>(x => x.UserId);
        [Display(Name = "_Model._BaseUserWhRelation._Wh")]
        public ExcelPropety Wh_Excel = ExcelPropety.CreateProperty<BaseUserWhRelation>(x => x.WhId);
        [Display(Name = "_Model._BaseUserWhRelation._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseUserWhRelation>(x => x.Memo);
        [Display(Name = "_Model._BaseUserWhRelation._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseUserWhRelation>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseUserWhRelation._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseUserWhRelation>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseUserWhRelation._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseUserWhRelation>(x => x.CreateBy);
        [Display(Name = "_Model._BaseUserWhRelation._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseUserWhRelation>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Wh_Excel.DataType = ColumnDataType.ComboBox;
            Wh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.Code.CodeCombinName(y.Name));

        }

    }

    public class BaseUserWhRelationImportVM : BaseImportVM<BaseUserWhRelationTemplateVM, BaseUserWhRelation>
    {
            //import

    }

}