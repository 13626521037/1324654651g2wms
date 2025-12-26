
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseItemMasterVMs
{
    public partial class BaseItemMasterBatchVM : BaseBatchVM<BaseItemMaster, BaseItemMaster_BatchEdit>
    {
        public BaseItemMasterBatchVM()
        {
            ListVM = new BaseItemMasterListVM();
            LinkedVM = new BaseItemMaster_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseItemMaster_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseItemMasterBTempSelected { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ItemCategory")]
        public Guid? ItemCategoryId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseItemMaster._SPECS")]
        public string SPECS { get; set; }
        [Display(Name = "_Model._BaseItemMaster._MateriaModel")]
        public string MateriaModel { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Description")]
        public string Description { get; set; }
        [Display(Name = "_Model._BaseItemMaster._StockUnit")]
        public Guid? StockUnitId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ProductionOrg")]
        public Guid? ProductionOrgId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ProductionDept")]
        public Guid? ProductionDeptId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Wh")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._FormAttribute")]
        public ItemFormAttributeEnum? FormAttribute { get; set; }
        [Display(Name = "_Model._BaseItemMaster._MateriaAttribute")]
        public string MateriaAttribute { get; set; }
        [Display(Name = "_Model._BaseItemMaster._GearRatio")]
        public decimal? GearRatio { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Power")]
        public decimal? Power { get; set; }
        [Display(Name = "_Model._BaseItemMaster._SafetyStockQty")]
        public decimal? SafetyStockQty { get; set; }
        [Display(Name = "_Model._BaseItemMaster._FixedLT")]
        public decimal? FixedLT { get; set; }
        [Display(Name = "_Model._BaseItemMaster._BuildBatch")]
        public int? BuildBatch { get; set; }
        [Display(Name = "_Model._BaseItemMaster._NotAnalysisQty")]
        public int? NotAnalysisQty { get; set; }
        [Display(Name = "_Model._BaseItemMaster._AnalysisType")]
        public Guid? AnalysisTypeId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}