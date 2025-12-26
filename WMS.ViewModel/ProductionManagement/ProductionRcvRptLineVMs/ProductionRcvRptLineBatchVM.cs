
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

namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptLineVMs
{
    public partial class ProductionRcvRptLineBatchVM : BaseBatchVM<ProductionRcvRptLine, ProductionRcvRptLine_BatchEdit>
    {
        public ProductionRcvRptLineBatchVM()
        {
            ListVM = new ProductionRcvRptLineListVM();
            LinkedVM = new ProductionRcvRptLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ProductionRcvRptLine_BatchEdit : BaseVM
    {

        
        public List<string> ProductionManagementProductionRcvRptLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}