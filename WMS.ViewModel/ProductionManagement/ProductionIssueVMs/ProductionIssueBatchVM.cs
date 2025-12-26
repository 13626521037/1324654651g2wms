
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.ProductionManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.ProductionManagement.ProductionIssueVMs
{
    public partial class ProductionIssueBatchVM : BaseBatchVM<ProductionIssue, ProductionIssue_BatchEdit>
    {
        public ProductionIssueBatchVM()
        {
            ListVM = new ProductionIssueListVM();
            LinkedVM = new ProductionIssue_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ProductionIssue_BatchEdit : BaseVM
    {

        
        public List<string> ProductionManagementProductionIssueBTempSelected { get; set; }
        [Display(Name = "_Model._ProductionIssue._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._ProductionIssue._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._ProductionIssue._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._ProductionIssue._SubmitDate")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._ProductionIssue._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._ProductionIssue._Status")]
        public ProductionIssueStatusEnum? Status { get; set; }
        [Display(Name = "_Model._ProductionIssue._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}