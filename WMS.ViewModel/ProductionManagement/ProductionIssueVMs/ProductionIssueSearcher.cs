
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;
using WMS.Model.BaseData;
namespace WMS.ViewModel.ProductionManagement.ProductionIssueVMs
{
    public partial class ProductionIssueSearcher : BaseSearcher
    {
        
        public List<string> ProductionManagementProductionIssueSTempSelected { get; set; }
        [Display(Name = "_Model._ProductionIssue._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._ProductionIssue._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._ProductionIssue._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._ProductionIssue._SubmitDate")]
        public DateRange SubmitDate { get; set; }
        [Display(Name = "_Model._ProductionIssue._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._ProductionIssue._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._ProductionIssue._Status")]
        public ProductionIssueStatusEnum? Status { get; set; }
        [Display(Name = "_Model._ProductionIssue._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._ProductionIssue._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}