
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs
{
    public partial class BaseSeibanCustomerRelationTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseSeibanCustomerRelation._Customer")]
        public ExcelPropety Customer_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.CustomerId);
        [Display(Name = "_Model._BaseSeibanCustomerRelation._RandomCode")]
        public ExcelPropety RandomCode_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.RandomCode);
        [Display(Name = "_Model._BaseSeibanCustomerRelation._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.Memo);
        [Display(Name = "_Model._BaseSeibanCustomerRelation._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.SourceSystemId);
        [Display(Name = "_Model._BaseSeibanCustomerRelation._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._BaseSeibanCustomerRelation._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.Code);
        [Display(Name = "_Model._BaseSeibanCustomerRelation._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.Name);
        [Display(Name = "_Model._BaseSeibanCustomerRelation._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseSeibanCustomerRelation._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseSeibanCustomerRelation._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.CreateBy);
        [Display(Name = "_Model._BaseSeibanCustomerRelation._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.UpdateBy);
        [Display(Name = "_Model._BaseSeibanCustomerRelation._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<BaseSeibanCustomerRelation>(x => x.IsValid);

	    protected override void InitVM()
        {
            
            Customer_Excel.DataType = ColumnDataType.ComboBox;
            Customer_Excel.ListItems = DC.Set<BaseCustomer>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class BaseSeibanCustomerRelationImportVM : BaseImportVM<BaseSeibanCustomerRelationTemplateVM, BaseSeibanCustomerRelation>
    {
            //import

    }

}