
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

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs
{
    public partial class InventoryTransferOutDirectDocTypeTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.CreatePerson);
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.OrganizationId);
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.Code);
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.Name);
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.Memo);
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.SourceSystemId);
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.UpdateBy);
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<InventoryTransferOutDirectDocType>(x => x.IsValid);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class InventoryTransferOutDirectDocTypeImportVM : BaseImportVM<InventoryTransferOutDirectDocTypeTemplateVM, InventoryTransferOutDirectDocType>
    {
            //import

    }

}