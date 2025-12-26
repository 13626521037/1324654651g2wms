
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

namespace WMS.ViewModel.PrintManagement.PrintMOVMs
{
    public partial class PrintMOTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._PrintMO._CustomerCode")]
        public ExcelPropety CustomerCode_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.CustomerCode);
        [Display(Name = "_Model._PrintMO._OrderWhName")]
        public ExcelPropety OrderWhName_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.OrderWhName);
        [Display(Name = "_Model._PrintMO._CustomerSPECS")]
        public ExcelPropety CustomerSPECS_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.CustomerSPECS);
        [Display(Name = "_Model._PrintMO._Item")]
        public ExcelPropety Item_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.ItemId);
        [Display(Name = "_Model._PrintMO._Seiban")]
        public ExcelPropety Seiban_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.Seiban);
        [Display(Name = "_Model._PrintMO._SeibanRandom")]
        public ExcelPropety SeibanRandom_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.SeibanRandom);
        [Display(Name = "_Model._PrintMO._BatchNumber")]
        public ExcelPropety BatchNumber_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.BatchNumber);
        [Display(Name = "_Model._PrintMO._Qty")]
        public ExcelPropety Qty_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.Qty);
        [Display(Name = "_Model._PrintMO._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.DocNo);
        [Display(Name = "_Model._PrintMO._DocNoChange")]
        public ExcelPropety DocNoChange_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.DocNoChange);
        [Display(Name = "_Model._PrintMO._LocationName")]
        public ExcelPropety LocationName_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.LocationName);
        [Display(Name = "_Model._PrintMO._Org")]
        public ExcelPropety Org_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.OrgId);
        [Display(Name = "_Model._PrintMO._SyncItem")]
        public ExcelPropety SyncItem_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.SyncItem);
        [Display(Name = "_Model._PrintMO._SyncOrg")]
        public ExcelPropety SyncOrg_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.SyncOrg);
        [Display(Name = "_Model._PrintMO._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.CreateTime, true);
        [Display(Name = "_Model._PrintMO._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.UpdateTime, true);
        [Display(Name = "_Model._PrintMO._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.CreateBy);
        [Display(Name = "_Model._PrintMO._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<PrintMO>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Item_Excel.DataType = ColumnDataType.ComboBox;
            Item_Excel.ListItems = DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Org_Excel.DataType = ColumnDataType.ComboBox;
            Org_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class PrintMOImportVM : BaseImportVM<PrintMOTemplateVM, PrintMO>
    {
            //import

    }

}