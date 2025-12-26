
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseDepartmentVMs
{
    public partial class BaseDepartmentSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseDepartmentSTempSelected { get; set; }
        [Display(Name = "_Model._BaseDepartment._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseDepartment._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseDepartment._Organization")]
        public string Organization { get; set; }
        // public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._BaseDepartment._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseDepartment._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._BaseDepartment._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }
        [Display(Name = "生产相关")]
        public bool? IsMFG { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}