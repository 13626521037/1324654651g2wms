
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;
namespace WMS.ViewModel.ProductionManagement.ProductionReturnIssueLineVMs
{
    public partial class ProductionReturnIssueLineSearcher : BaseSearcher
    {
        
        public List<string> ProductionManagementProductionReturnIssueLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}