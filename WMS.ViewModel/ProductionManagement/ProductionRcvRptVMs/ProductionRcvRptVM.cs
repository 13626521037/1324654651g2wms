using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using WMS.ViewModel.ProductionManagement.ProductionRcvRptLineVMs;
using WMS.Model.ProductionManagement;
using WMS.Model;
namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs
{
    public partial class ProductionRcvRptVM : BaseCRUDVM<ProductionRcvRpt>
    {
        
        public List<string> ProductionManagementProductionRcvRptFTempSelected { get; set; }
        public ProductionRcvRptLineProductionRcvRptDetailListVM ProductionRcvRptLineProductionRcvRptList { get; set; }

        public ProductionRcvRptVM()
        {
            
            SetInclude(x => x.Organization);
            SetInclude(x => x.Wh);
            SetInclude(x => x.OrderWh);
            ProductionRcvRptLineProductionRcvRptList = new ProductionRcvRptLineProductionRcvRptDetailListVM();
            ProductionRcvRptLineProductionRcvRptList.DetailGridPrix = "Entity.ProductionRcvRptLine_ProductionRcvRpt";

        }

        protected override void InitVM()
        {
            
            ProductionRcvRptLineProductionRcvRptList.CopyContext(this);

        }

        public override DuplicatedInfo<ProductionRcvRpt> SetDuplicatedCheck()
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
