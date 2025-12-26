
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseUnitVMs
{
    public partial class BaseUnitSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseUnitSTempSelected { get; set; }
        [Display(Name = "_Model._BaseUnit._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseUnit._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseUnit._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseUnit._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._BaseUnit._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}