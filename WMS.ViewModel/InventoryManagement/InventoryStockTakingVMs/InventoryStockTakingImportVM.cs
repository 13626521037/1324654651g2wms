
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs
{
    public partial class InventoryStockTakingTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryStockTaking._ErpID")]
        public ExcelPropety ErpID_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.ErpID);
        [Display(Name = "_Model._InventoryStockTaking._ErpDocNo")]
        public ExcelPropety ErpDocNo_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.ErpDocNo);
        [Display(Name = "_Model._InventoryStockTaking._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.DocNo);
        [Display(Name = "_Model._InventoryStockTaking._Dimension")]
        public ExcelPropety Dimension_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.Dimension);
        [Display(Name = "_Model._InventoryStockTaking._Wh")]
        public ExcelPropety Wh_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.WhId);
        [Display(Name = "_Model._InventoryStockTaking._SubmitTime")]
        public ExcelPropety SubmitTime_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.SubmitTime, true);
        [Display(Name = "_Model._InventoryStockTaking._SubmitUser")]
        public ExcelPropety SubmitUser_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.SubmitUser);
        [Display(Name = "_Model._InventoryStockTaking._ApproveTime")]
        public ExcelPropety ApproveTime_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.ApproveTime, true);
        [Display(Name = "_Model._InventoryStockTaking._ApproveUser")]
        public ExcelPropety ApproveUser_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.ApproveUser);
        [Display(Name = "_Model._InventoryStockTaking._CloseTime")]
        public ExcelPropety CloseTime_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.CloseTime, true);
        [Display(Name = "_Model._InventoryStockTaking._CloseUser")]
        public ExcelPropety CloseUser_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.CloseUser);
        [Display(Name = "_Model._InventoryStockTaking._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.Status);
        [Display(Name = "_Model._InventoryStockTaking._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.Memo);
        [Display(Name = "_Model._InventoryStockTaking._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryStockTaking._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryStockTaking._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryStockTaking._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryStockTaking>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Wh_Excel.DataType = ColumnDataType.ComboBox;
            Wh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class InventoryStockTakingImportVM : BaseImportVM<InventoryStockTakingTemplateVM, InventoryStockTaking>
    {
            //import

    }

}