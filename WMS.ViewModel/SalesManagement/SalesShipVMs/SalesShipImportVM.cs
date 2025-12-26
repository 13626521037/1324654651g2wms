
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.SalesManagement.SalesShipVMs
{
    public partial class SalesShipTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._SalesShip._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.CreatePerson);
        [Display(Name = "_Model._SalesShip._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.OrganizationId);
        [Display(Name = "_Model._SalesShip._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.BusinessDate, true);
        [Display(Name = "_Model._SalesShip._SubmitDate")]
        public ExcelPropety SubmitDate_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.SubmitDate, true);
        [Display(Name = "_Model._SalesShip._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.DocNo);
        [Display(Name = "_Model._SalesShip._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.DocType);
        [Display(Name = "_Model._SalesShip._Operators")]
        public ExcelPropety Operators_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.Operators);
        [Display(Name = "_Model._SalesShip._Customer")]
        public ExcelPropety Customer_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.CustomerId);
        [Display(Name = "_Model._SalesShip._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.Status);
        [Display(Name = "_Model._SalesShip._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.Memo);
        [Display(Name = "_Model._SalesShip._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.SourceSystemId);
        [Display(Name = "_Model._SalesShip._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._SalesShip._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.CreateTime, true);
        [Display(Name = "_Model._SalesShip._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.UpdateTime, true);
        [Display(Name = "_Model._SalesShip._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.CreateBy);
        [Display(Name = "_Model._SalesShip._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<SalesShip>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Customer_Excel.DataType = ColumnDataType.ComboBox;
            Customer_Excel.ListItems = DC.Set<BaseCustomer>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class SalesShipImportVM : BaseImportVM<SalesShipTemplateVM, SalesShip>
    {
            //import

    }

}