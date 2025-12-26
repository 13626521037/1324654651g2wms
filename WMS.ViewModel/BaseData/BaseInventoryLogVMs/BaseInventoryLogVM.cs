using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseInventoryVMs;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseInventoryLogVMs
{
    public partial class BaseInventoryLogVM : BaseCRUDVM<BaseInventoryLog>
    {
        
        public List<string> BaseDataBaseInventoryLogFTempSelected { get; set; }

        public BaseInventoryLogVM()
        {
            
            SetInclude(x => x.SourceInventory);
            SetInclude(x => x.TargetInventory);

        }

        protected override void InitVM()
        {
            

        }

        public override DuplicatedInfo<BaseInventoryLog> SetDuplicatedCheck()
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
