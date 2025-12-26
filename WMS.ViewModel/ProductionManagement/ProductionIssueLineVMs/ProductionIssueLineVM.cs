using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.Model.ProductionManagement;
using WMS.Model;
namespace WMS.ViewModel.ProductionManagement.ProductionIssueLineVMs
{
    public partial class ProductionIssueLineVM : BaseCRUDVM<ProductionIssueLine>
    {
        
        public List<string> ProductionManagementProductionIssueLineFTempSelected { get; set; }

        public ProductionIssueLineVM()
        {
            
        }

        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<ProductionIssueLine> SetDuplicatedCheck()
        {
            return null;

        }

        public override async Task DoAddAsync()        
        {
            
            await base.DoAddAsync();

        }

        public override async Task DoEditAsync(bool updateAllFields = false)
        {
            
            await base.DoEditAsync();

        }

        public override async Task DoDeleteAsync()
        {
            await base.DoDeleteAsync();

        }
    }
}
