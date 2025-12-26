using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel._Admin.FrameworkUserVMs;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseShortcutVMs
{
    public partial class BaseShortcutVM : BaseCRUDVM<BaseShortcut>
    {
        
        public List<string> BaseDataBaseShortcutFTempSelected { get; set; }

        public BaseShortcutVM()
        {
            
            SetInclude(x => x.Menu);
            SetInclude(x => x.User);

        }

        protected override void InitVM()
        {
            

        }

        public override DuplicatedInfo<BaseShortcut> SetDuplicatedCheck()
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
