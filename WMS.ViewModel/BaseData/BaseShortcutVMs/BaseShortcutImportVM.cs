
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseShortcutVMs
{
    public partial class BaseShortcutTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseShortcut._Menu")]
        public ExcelPropety Menu_Excel = ExcelPropety.CreateProperty<BaseShortcut>(x => x.MenuId);
        [Display(Name = "_Model._BaseShortcut._User")]
        public ExcelPropety User_Excel = ExcelPropety.CreateProperty<BaseShortcut>(x => x.UserId);
        [Display(Name = "_Model._BaseShortcut._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseShortcut>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseShortcut._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseShortcut>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseShortcut._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseShortcut>(x => x.CreateBy);
        [Display(Name = "_Model._BaseShortcut._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseShortcut>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Menu_Excel.DataType = ColumnDataType.ComboBox;
            Menu_Excel.ListItems = DC.Set<FrameworkMenu>().GetSelectListItems(Wtm, y => y.PageName.ToString());

        }

    }

    public class BaseShortcutImportVM : BaseImportVM<BaseShortcutTemplateVM, BaseShortcut>
    {
            //import

    }

}