using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseSupplierVMs;
using WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueLineVMs;
using WMS.Model.PurchaseManagement;
using WMS.Model;
namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs
{
    public partial class PurchaseOutsourcingIssueVM : BaseCRUDVM<PurchaseOutsourcingIssue>
    {

        public List<string> PurchaseManagementPurchaseOutsourcingIssueFTempSelected { get; set; }
        public PurchaseOutsourcingIssueLineOutsourcingIssueDetailListVM PurchaseOutsourcingIssueLineOutsourcingIssueList { get; set; }

        public PurchaseOutsourcingIssueVM()
        {

            SetInclude(x => x.Organization);
            SetInclude(x => x.Supplier);
            PurchaseOutsourcingIssueLineOutsourcingIssueList = new PurchaseOutsourcingIssueLineOutsourcingIssueDetailListVM();
            PurchaseOutsourcingIssueLineOutsourcingIssueList.DetailGridPrix = "Entity.PurchaseOutsourcingIssueLine_OutsourcingIssue";

        }

        protected override void InitVM()
        {

            PurchaseOutsourcingIssueLineOutsourcingIssueList.CopyContext(this);

        }

        public override DuplicatedInfo<PurchaseOutsourcingIssue> SetDuplicatedCheck()
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

        public override void DoDelete()
        {
            if (Entity.Status != PurchaseOutsourcingIssueStatusEnum.InWh)
            {
                MSD.AddModelError("", $"当前单据状态“{Entity.Status.GetEnumDisplayName()}”不允许删除");
                return;
            }
            base.DoDelete();
        }

        public override async Task DoDeleteAsync()
        {
            await base.DoDeleteAsync();

        }
    }
}
