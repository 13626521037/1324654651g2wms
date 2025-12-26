
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;
namespace WMS.ViewModel.ProductionManagement.ProductionIssueLineVMs
{
    public partial class ProductionIssueLineSearcher : BaseSearcher
    {
        
        public List<string> ProductionManagementProductionIssueLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}