
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseCustomerVMs
{
    public partial class BaseCustomerTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseCustomer._ShortName")]
        public ExcelPropety ShortName_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.ShortName);
        [Display(Name = "_Model._BaseCustomer._EnglishShortName")]
        public ExcelPropety EnglishShortName_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.EnglishShortName);
        [Display(Name = "_Model._BaseCustomer._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.OrganizationId);
        [Display(Name = "_Model._BaseCustomer._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.IsEffective);
        [Display(Name = "_Model._BaseCustomer._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.Memo);
        [Display(Name = "_Model._BaseCustomer._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.SourceSystemId);
        [Display(Name = "_Model._BaseCustomer._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._BaseCustomer._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.Code);
        [Display(Name = "_Model._BaseCustomer._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.Name);
        [Display(Name = "_Model._BaseCustomer._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseCustomer._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseCustomer._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.CreateBy);
        [Display(Name = "_Model._BaseCustomer._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.UpdateBy);
        [Display(Name = "_Model._BaseCustomer._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<BaseCustomer>(x => x.IsValid);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class BaseCustomerImportVM : BaseImportVM<BaseCustomerTemplateVM, BaseCustomer>
    {
            //import

    }

}