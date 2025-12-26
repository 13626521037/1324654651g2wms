
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PrintManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.PrintManagement.PrintSupplierVMs
{
    public partial class PrintSupplierTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._PrintSupplier._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.DocNo);
        [Display(Name = "_Model._PrintSupplier._DocLineNo")]
        public ExcelPropety DocLineNo_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.DocLineNo);
        [Display(Name = "_Model._PrintSupplier._Item")]
        public ExcelPropety Item_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.ItemId);
        [Display(Name = "_Model._PrintSupplier._Qty")]
        public ExcelPropety Qty_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.Qty);
        [Display(Name = "_Model._PrintSupplier._ReceivedQty")]
        public ExcelPropety ReceivedQty_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.ReceivedQty);
        [Display(Name = "_Model._PrintSupplier._ValidQty")]
        public ExcelPropety ValidQty_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.ValidQty);
        [Display(Name = "_Model._PrintSupplier._BatchNumber")]
        public ExcelPropety BatchNumber_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.BatchNumber);
        [Display(Name = "_Model._PrintSupplier._Seiban")]
        public ExcelPropety Seiban_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.Seiban);
        [Display(Name = "_Model._PrintSupplier._SeibanRandom")]
        public ExcelPropety SeibanRandom_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.SeibanRandom);
        [Display(Name = "_Model._PrintSupplier._Weight")]
        public ExcelPropety Weight_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.Weight);
        [Display(Name = "_Model._PrintSupplier._Supplier")]
        public ExcelPropety Supplier_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.SupplierId);
        [Display(Name = "_Model._PrintSupplier._SyncSupplier")]
        public ExcelPropety SyncSupplier_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.SyncSupplier);
        [Display(Name = "_Model._PrintSupplier._SyncItem")]
        public ExcelPropety SyncItem_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.SyncItem);
        [Display(Name = "_Model._PrintSupplier._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.CreateTime, true);
        [Display(Name = "_Model._PrintSupplier._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.UpdateTime, true);
        [Display(Name = "_Model._PrintSupplier._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.CreateBy);
        [Display(Name = "_Model._PrintSupplier._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<PrintSupplier>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Item_Excel.DataType = ColumnDataType.ComboBox;
            Item_Excel.ListItems = DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Supplier_Excel.DataType = ColumnDataType.ComboBox;
            Supplier_Excel.ListItems = DC.Set<BaseSupplier>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class PrintSupplierImportVM : BaseImportVM<PrintSupplierTemplateVM, PrintSupplier>
    {
            //import

    }

}