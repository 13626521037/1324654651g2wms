
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs
{
    public partial class BaseDocInventoryRelationTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseDocInventoryRelation._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<BaseDocInventoryRelation>(x => x.DocType);
        [Display(Name = "_Model._BaseDocInventoryRelation._Inventory")]
        public ExcelPropety Inventory_Excel = ExcelPropety.CreateProperty<BaseDocInventoryRelation>(x => x.InventoryId);
        [Display(Name = "_Model._BaseDocInventoryRelation._BusinessId")]
        public ExcelPropety BusinessId_Excel = ExcelPropety.CreateProperty<BaseDocInventoryRelation>(x => x.BusinessId);
        [Display(Name = "_Model._BaseDocInventoryRelation._Qty")]
        public ExcelPropety Qty_Excel = ExcelPropety.CreateProperty<BaseDocInventoryRelation>(x => x.Qty);
        [Display(Name = "_Model._BaseDocInventoryRelation._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseDocInventoryRelation>(x => x.Memo);
        [Display(Name = "_Model._BaseDocInventoryRelation._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseDocInventoryRelation>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseDocInventoryRelation._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseDocInventoryRelation>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseDocInventoryRelation._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseDocInventoryRelation>(x => x.CreateBy);
        [Display(Name = "_Model._BaseDocInventoryRelation._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseDocInventoryRelation>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Inventory_Excel.DataType = ColumnDataType.ComboBox;
            Inventory_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber.ToString());

        }

    }

    public class BaseDocInventoryRelationImportVM : BaseImportVM<BaseDocInventoryRelationTemplateVM, BaseDocInventoryRelation>
    {
            //import

    }

}