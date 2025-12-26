using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.ProductionManagement.ProductionReturnIssueLineVMs;
using WMS.Model.ProductionManagement;
using WMS.Model;
namespace WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs
{
    public partial class ProductionReturnIssueVM : BaseCRUDVM<ProductionReturnIssue>
    {

        public List<string> ProductionManagementProductionReturnIssueFTempSelected { get; set; }
        public ProductionReturnIssueLineProductionReturnIssueDetailListVM ProductionReturnIssueLineProductionReturnIssueList { get; set; }

        public ProductionReturnIssueVM()
        {

            SetInclude(x => x.Organization);
            ProductionReturnIssueLineProductionReturnIssueList = new ProductionReturnIssueLineProductionReturnIssueDetailListVM();
            ProductionReturnIssueLineProductionReturnIssueList.DetailGridPrix = "Entity.ProductionReturnIssueLine_ProductionReturnIssue";

        }

        protected override void InitVM()
        {

            ProductionReturnIssueLineProductionReturnIssueList.CopyContext(this);

        }

        public override DuplicatedInfo<ProductionReturnIssue> SetDuplicatedCheck()
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
            if (Entity.Status != ProductionReturnIssueStatusEnum.NotReceive)
            {
                MSD.AddModelError("", $"当前单据状态“{Entity.Status.GetEnumDisplayName()}”不允许删除");
                return;
            }
            base.DoDelete();
        }
    }
}
