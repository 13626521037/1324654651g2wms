
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

namespace WMS.ViewModel.BaseData.BaseItemCategoryVMs
{
    public partial class BaseItemCategoryBatchVM : BaseBatchVM<BaseItemCategory, BaseItemCategory_BatchEdit>
    {
        public BaseItemCategoryBatchVM()
        {
            ListVM = new BaseItemCategoryListVM();
            LinkedVM = new BaseItemCategory_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseItemCategory_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseItemCategoryBTempSelected { get; set; }
        [Display(Name = "_Model._BaseItemCategory._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseItemCategory._AnalysisType")]
        public Guid? AnalysisTypeId { get; set; }
        [Display(Name = "_Model._BaseItemCategory._Department")]
        public Guid? DepartmentId { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}