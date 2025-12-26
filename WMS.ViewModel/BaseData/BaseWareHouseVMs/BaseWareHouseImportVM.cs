
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseWareHouseVMs
{
    public partial class BaseWareHouseTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseWareHouse._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.Code);
        [Display(Name = "_Model._BaseWareHouse._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.Name);
        [Display(Name = "_Model._BaseWareHouse._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.OrganizationId);
        [Display(Name = "_Model._BaseWareHouse._IsProduct")]
        public ExcelPropety IsProduct_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.IsProduct);
        [Display(Name = "_Model._BaseWareHouse._ShipType")]
        public ExcelPropety ShipType_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.ShipType);
        [Display(Name = "_Model._BaseWareHouse._IsStacking")]
        public ExcelPropety IsStacking_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.IsStacking);
        [Display(Name = "_Model._BaseWareHouse._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.IsEffective);
        [Display(Name = "_Model._BaseWareHouse._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.Memo);
        [Display(Name = "_Model._BaseWareHouse._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.SourceSystemId);
        [Display(Name = "_Model._BaseWareHouse._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._BaseWareHouse._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseWareHouse._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseWareHouse._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.CreateBy);
        [Display(Name = "_Model._BaseWareHouse._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.UpdateBy);
        [Display(Name = "_Model._BaseWareHouse._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<BaseWareHouse>(x => x.IsValid);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class BaseWareHouseImportVM : BaseImportVM<BaseWareHouseTemplateVM, BaseWareHouse>
    {
            //import

    }

}