using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model;
using WMS.Model.SalesManagement;
using WMS.ViewModel.BaseData.BaseCustomerVMs;
using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.SalesManagement.SalesRMALineVMs;
namespace WMS.ViewModel.SalesManagement.SalesRMAVMs
{
    public partial class SalesRMAVM : BaseCRUDVM<SalesRMA>
    {
        
        public List<string> SalesManagementSalesRMAFTempSelected { get; set; }

        public SalesRMALineRMAIdDetailListVM SalesRMALineRMAIdList { get; set; }

        public SalesRMAVM()
        {
            
            SetInclude(x => x.Organization);
            SetInclude(x => x.Customer);
            SalesRMALineRMAIdList = new SalesRMALineRMAIdDetailListVM();
            SalesRMALineRMAIdList.DetailGridPrix = "Entity.SalesRMALine_RMAId";
        }

        public override void DoDelete()
        {
            if (Entity.Status != SalesRMAStatusEnum.NotReceive)
            {
                MSD.AddModelError("", $"当前单据状态“{Entity.Status.GetEnumDisplayName()}”不允许删除");
                return;
            }
            base.DoDelete();
        }
        protected override void InitVM()
        {
            SalesRMALineRMAIdList.CopyContext(this);

        }

        public override DuplicatedInfo<SalesRMA> SetDuplicatedCheck()
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
    }
}
