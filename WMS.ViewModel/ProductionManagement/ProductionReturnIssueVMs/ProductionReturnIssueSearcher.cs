
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
namespace WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs
{
    public partial class ProductionReturnIssueSearcher : BaseSearcher
    {
        
        public List<string> ProductionManagementProductionReturnIssueSTempSelected { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._Status")]
        public ProductionReturnIssueStatusEnum? Status { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._SubmitDate")]
        public DateRange SubmitDate { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}