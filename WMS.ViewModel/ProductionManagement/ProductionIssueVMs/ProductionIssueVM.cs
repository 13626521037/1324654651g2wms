using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.ProductionManagement.ProductionIssueLineVMs;
using WMS.Model.ProductionManagement;
using WMS.Model;
namespace WMS.ViewModel.ProductionManagement.ProductionIssueVMs
{
    public partial class ProductionIssueVM : BaseCRUDVM<ProductionIssue>
    {

        public List<string> ProductionManagementProductionIssueFTempSelected { get; set; }
        public ProductionIssueLineProductionIssueDetailListVM ProductionIssueLineProductionIssueList { get; set; }

        public ProductionIssueVM()
        {

            SetInclude(x => x.Organization);
            ProductionIssueLineProductionIssueList = new ProductionIssueLineProductionIssueDetailListVM();
            ProductionIssueLineProductionIssueList.DetailGridPrix = "Entity.ProductionIssueLine_ProductionIssue";

        }

        protected override void InitVM()
        {

            ProductionIssueLineProductionIssueList.CopyContext(this);

        }

        public override DuplicatedInfo<ProductionIssue> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.DocNo));
            return rv;

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

        public override void DoDelete()
        {
            if (Entity.Status != ProductionIssueStatusEnum.InWh)
            {
                MSD.AddModelError("", $"当前单据状态“{Entity.Status.GetEnumDisplayName()}”不允许删除");
                return;
            }
            base.DoDelete();
        }
    }
}
