
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSupplierVMs
{
    public partial class BaseSupplierTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseSupplier._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.Code);
        [Display(Name = "_Model._BaseSupplier._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.Name);
        [Display(Name = "_Model._BaseSupplier._ShortName")]
        public ExcelPropety ShortName_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.ShortName);
        [Display(Name = "_Model._BaseSupplier._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.OrganizationId);
        [Display(Name = "_Model._BaseSupplier._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.IsEffective);
        [Display(Name = "_Model._BaseSupplier._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.Memo);
        [Display(Name = "_Model._BaseSupplier._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.SourceSystemId);
        [Display(Name = "_Model._BaseSupplier._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._BaseSupplier._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseSupplier._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseSupplier._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.CreateBy);
        [Display(Name = "_Model._BaseSupplier._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.UpdateBy);
        [Display(Name = "_Model._BaseSupplier._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<BaseSupplier>(x => x.IsValid);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class BaseSupplierImportVM : BaseImportVM<BaseSupplierTemplateVM, BaseSupplier>
    {
            //import

    }

}