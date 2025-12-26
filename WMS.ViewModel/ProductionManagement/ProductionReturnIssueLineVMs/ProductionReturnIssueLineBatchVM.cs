
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

namespace WMS.ViewModel.ProductionManagement.ProductionReturnIssueLineVMs
{
    public partial class ProductionReturnIssueLineBatchVM : BaseBatchVM<ProductionReturnIssueLine, ProductionReturnIssueLine_BatchEdit>
    {
        public ProductionReturnIssueLineBatchVM()
        {
            ListVM = new ProductionReturnIssueLineListVM();
            LinkedVM = new ProductionReturnIssueLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ProductionReturnIssueLine_BatchEdit : BaseVM
    {

        
        public List<string> ProductionManagementProductionReturnIssueLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}