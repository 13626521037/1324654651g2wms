
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

namespace WMS.ViewModel.BaseData.BaseSequenceDefineVMs
{
    public partial class BaseSequenceDefineBatchVM : BaseBatchVM<BaseSequenceDefine, BaseSequenceDefine_BatchEdit>
    {
        public BaseSequenceDefineBatchVM()
        {
            ListVM = new BaseSequenceDefineListVM();
            LinkedVM = new BaseSequenceDefine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseSequenceDefine_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseSequenceDefineBTempSelected { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._DocType")]
        public DocTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}