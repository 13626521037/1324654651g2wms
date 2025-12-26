
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

namespace WMS.ViewModel.BaseData.BaseAnalysisTypeVMs
{
    public partial class BaseAnalysisTypeBatchVM : BaseBatchVM<BaseAnalysisType, BaseAnalysisType_BatchEdit>
    {
        public BaseAnalysisTypeBatchVM()
        {
            ListVM = new BaseAnalysisTypeListVM();
            LinkedVM = new BaseAnalysisType_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseAnalysisType_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseAnalysisTypeBTempSelected { get; set; }
        [Display(Name = "_Model._BaseAnalysisType._Name")]
        public string Name { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}