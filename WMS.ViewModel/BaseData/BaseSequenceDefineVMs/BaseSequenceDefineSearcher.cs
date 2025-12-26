
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseSequenceDefineVMs
{
    public partial class BaseSequenceDefineSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseSequenceDefineSTempSelected { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._DocType")]
        public DocTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}