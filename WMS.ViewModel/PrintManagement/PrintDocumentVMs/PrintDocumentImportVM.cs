
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PrintManagement;
using WMS.Model;

namespace WMS.ViewModel.PrintManagement.PrintDocumentVMs
{
    public partial class PrintDocumentTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._PrintDocument._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.DocNo);
        [Display(Name = "_Model._PrintDocument._DocLineNo")]
        public ExcelPropety DocLineNo_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.DocLineNo);
        [Display(Name = "_Model._PrintDocument._ItemID")]
        public ExcelPropety ItemID_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.ItemID);
        [Display(Name = "_Model._PrintDocument._ItemCode")]
        public ExcelPropety ItemCode_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.ItemCode);
        [Display(Name = "_Model._PrintDocument._ItemName")]
        public ExcelPropety ItemName_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.ItemName);
        [Display(Name = "_Model._PrintDocument._SPECS")]
        public ExcelPropety SPECS_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.SPECS);
        [Display(Name = "_Model._PrintDocument._Description")]
        public ExcelPropety Description_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.Description);
        [Display(Name = "_Model._PrintDocument._UnitName")]
        public ExcelPropety UnitName_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.UnitName);
        [Display(Name = "_Model._PrintDocument._Qty")]
        public ExcelPropety Qty_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.Qty);
        [Display(Name = "_Model._PrintDocument._ReceivedQty")]
        public ExcelPropety ReceivedQty_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.ReceivedQty);
        [Display(Name = "_Model._PrintDocument._ValidQty")]
        public ExcelPropety ValidQty_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.ValidQty);
        [Display(Name = "_Model._PrintDocument._Seiban")]
        public ExcelPropety Seiban_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.Seiban);
        [Display(Name = "_Model._PrintDocument._SeibanRandom")]
        public ExcelPropety SeibanRandom_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.SeibanRandom);
        [Display(Name = "_Model._PrintDocument._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.CreateTime, true);
        [Display(Name = "_Model._PrintDocument._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.UpdateTime, true);
        [Display(Name = "_Model._PrintDocument._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.CreateBy);
        [Display(Name = "_Model._PrintDocument._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<PrintDocument>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
        }

    }

    public class PrintDocumentImportVM : BaseImportVM<PrintDocumentTemplateVM, PrintDocument>
    {
            //import

    }

}