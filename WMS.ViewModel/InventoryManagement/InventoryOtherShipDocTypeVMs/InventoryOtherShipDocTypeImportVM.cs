
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

namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs
{
    public partial class InventoryOtherShipDocTypeTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryOtherShipDocType._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.CreatePerson);
        [Display(Name = "_Model._InventoryOtherShipDocType._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.OrganizationId);
        [Display(Name = "_Model._InventoryOtherShipDocType._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.IsEffective);
        [Display(Name = "_Model._InventoryOtherShipDocType._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.Memo);
        [Display(Name = "_Model._InventoryOtherShipDocType._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.SourceSystemId);
        [Display(Name = "_Model._InventoryOtherShipDocType._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._InventoryOtherShipDocType._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.Code);
        [Display(Name = "_Model._InventoryOtherShipDocType._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.Name);
        [Display(Name = "_Model._InventoryOtherShipDocType._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryOtherShipDocType._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryOtherShipDocType._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryOtherShipDocType._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.UpdateBy);
        [Display(Name = "_Model._InventoryOtherShipDocType._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<InventoryOtherShipDocType>(x => x.IsValid);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class InventoryOtherShipDocTypeImportVM : BaseImportVM<InventoryOtherShipDocTypeTemplateVM, InventoryOtherShipDocType>
    {
            //import

    }

}