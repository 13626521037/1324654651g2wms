
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

namespace WMS.ViewModel.ProductionManagement.ProductionIssueLineVMs
{
    public partial class ProductionIssueLineBatchVM : BaseBatchVM<ProductionIssueLine, ProductionIssueLine_BatchEdit>
    {
        public ProductionIssueLineBatchVM()
        {
            ListVM = new ProductionIssueLineListVM();
            LinkedVM = new ProductionIssueLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ProductionIssueLine_BatchEdit : BaseVM
    {

        
        public List<string> ProductionManagementProductionIssueLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}