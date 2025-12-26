using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.Model.SalesManagement;
using WMS.Model;
namespace WMS.ViewModel.SalesManagement.SalesRMALineVMs
{
    public partial class SalesRMALineVM : BaseCRUDVM<SalesRMALine>
    {
        
        public List<string> SalesManagementSalesRMALineFTempSelected { get; set; }

        public SalesRMALineVM()
        {
            
        }

        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<SalesRMALine> SetDuplicatedCheck()
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
