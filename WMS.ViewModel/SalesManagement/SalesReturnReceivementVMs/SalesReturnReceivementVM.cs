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
using WMS.ViewModel.SalesManagement.SalesReturnReceivementLineVMs;
namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs
{
    public partial class SalesReturnReceivementVM : BaseCRUDVM<SalesReturnReceivement>
    {
        
        public List<string> SalesManagementSalesReturnReceivementFTempSelected { get; set; }
        public SalesReturnReceivementLineReturnReceivementDetailListVM SalesReturnReceivementLineReturnReceivementList { get; set; }

        public SalesReturnReceivementVM()
        {
            
            SetInclude(x => x.Organization);
            SetInclude(x => x.Customer);
            SalesReturnReceivementLineReturnReceivementList = new SalesReturnReceivementLineReturnReceivementDetailListVM();
            SalesReturnReceivementLineReturnReceivementList.DetailGridPrix = "Entity.SalesReturnReceivementLine_ReturnReceivement";
        }

        protected override void InitVM()
        {
            SalesReturnReceivementLineReturnReceivementList.CopyContext(this);

        }

        public override DuplicatedInfo<SalesReturnReceivement> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.DocNo));
            return rv;

        }

        public override void DoDelete()
        {
            if (Entity.Status != SalesReturnReceivementStatusEnum.NotReceive)
            {
                MSD.AddModelError("", $"当前单据状态“{Entity.Status.GetEnumDisplayName()}”不允许删除");
                return;
            }
            base.DoDelete();
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
