
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseOrganizationVMs
{
    public partial class BaseOrganizationSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseOrganizationSTempSelected { get; set; }
        [Display(Name = "_Model._BaseOrganization._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseOrganization._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseOrganization._IsProduction")]
        public bool? IsProduction { get; set; }
        [Display(Name = "_Model._BaseOrganization._IsSale")]
        public bool? IsSale { get; set; }
        [Display(Name = "_Model._BaseOrganization._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseOrganization._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._BaseOrganization._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}