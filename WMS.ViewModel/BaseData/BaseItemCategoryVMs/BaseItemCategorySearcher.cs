
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseItemCategoryVMs
{
    public partial class BaseItemCategorySearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseItemCategorySTempSelected { get; set; }
        [Display(Name = "_Model._BaseItemCategory._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._BaseItemCategory._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseItemCategory._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseItemCategory._AnalysisType")]
        public Guid? AnalysisTypeId { get; set; }
        public List<ComboSelectListItem> AllAnalysisTypes { get; set; }
        [Display(Name = "_Model._BaseItemCategory._Department")]
        public Guid? DepartmentId { get; set; }
        public List<ComboSelectListItem> AllDepartments { get; set; }
        [Display(Name = "_Model._BaseItemCategory._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._BaseItemCategory._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}