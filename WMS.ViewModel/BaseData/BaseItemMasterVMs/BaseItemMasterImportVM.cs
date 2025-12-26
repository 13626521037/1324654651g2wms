
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseItemMasterVMs
{
    public partial class BaseItemMasterTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseItemMaster._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.OrganizationId);
        [Display(Name = "_Model._BaseItemMaster._ItemCategory")]
        public ExcelPropety ItemCategory_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.ItemCategoryId);
        [Display(Name = "_Model._BaseItemMaster._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.Code);
        [Display(Name = "_Model._BaseItemMaster._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.Name);
        [Display(Name = "_Model._BaseItemMaster._SPECS")]
        public ExcelPropety SPECS_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.SPECS);
        [Display(Name = "_Model._BaseItemMaster._MateriaModel")]
        public ExcelPropety MateriaModel_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.MateriaModel);
        [Display(Name = "_Model._BaseItemMaster._Description")]
        public ExcelPropety Description_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.Description);
        [Display(Name = "_Model._BaseItemMaster._StockUnit")]
        public ExcelPropety StockUnit_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.StockUnitId);
        [Display(Name = "_Model._BaseItemMaster._ProductionOrg")]
        public ExcelPropety ProductionOrg_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.ProductionOrgId);
        [Display(Name = "_Model._BaseItemMaster._ProductionDept")]
        public ExcelPropety ProductionDept_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.ProductionDeptId);
        [Display(Name = "_Model._BaseItemMaster._Wh")]
        public ExcelPropety Wh_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.WhId);
        [Display(Name = "_Model._BaseItemMaster._FormAttribute")]
        public ExcelPropety FormAttribute_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.FormAttribute);
        [Display(Name = "_Model._BaseItemMaster._MateriaAttribute")]
        public ExcelPropety MateriaAttribute_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.MateriaAttribute);
        [Display(Name = "_Model._BaseItemMaster._GearRatio")]
        public ExcelPropety GearRatio_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.GearRatio);
        [Display(Name = "_Model._BaseItemMaster._Power")]
        public ExcelPropety Power_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.Power);
        [Display(Name = "_Model._BaseItemMaster._SafetyStockQty")]
        public ExcelPropety SafetyStockQty_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.SafetyStockQty);
        [Display(Name = "_Model._BaseItemMaster._FixedLT")]
        public ExcelPropety FixedLT_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.FixedLT);
        [Display(Name = "_Model._BaseItemMaster._BuildBatch")]
        public ExcelPropety BuildBatch_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.BuildBatch);
        [Display(Name = "_Model._BaseItemMaster._NotAnalysisQty")]
        public ExcelPropety NotAnalysisQty_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.NotAnalysisQty);
        [Display(Name = "_Model._BaseItemMaster._AnalysisType")]
        public ExcelPropety AnalysisType_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.AnalysisTypeId);
        [Display(Name = "_Model._BaseItemMaster._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.IsEffective);
        [Display(Name = "_Model._BaseItemMaster._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.SourceSystemId);
        [Display(Name = "_Model._BaseItemMaster._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._BaseItemMaster._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseItemMaster._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseItemMaster._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.CreateBy);
        [Display(Name = "_Model._BaseItemMaster._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.UpdateBy);
        [Display(Name = "_Model._BaseItemMaster._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<BaseItemMaster>(x => x.IsValid);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            ItemCategory_Excel.DataType = ColumnDataType.ComboBox;
            ItemCategory_Excel.ListItems = DC.Set<BaseItemCategory>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            StockUnit_Excel.DataType = ColumnDataType.ComboBox;
            StockUnit_Excel.ListItems = DC.Set<BaseUnit>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            ProductionOrg_Excel.DataType = ColumnDataType.ComboBox;
            ProductionOrg_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            ProductionDept_Excel.DataType = ColumnDataType.ComboBox;
            ProductionDept_Excel.ListItems = DC.Set<BaseDepartment>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Wh_Excel.DataType = ColumnDataType.ComboBox;
            Wh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            AnalysisType_Excel.DataType = ColumnDataType.ComboBox;
            AnalysisType_Excel.ListItems = DC.Set<BaseAnalysisType>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class BaseItemMasterImportVM : BaseImportVM<BaseItemMasterTemplateVM, BaseItemMaster>
    {
            //import

    }

}