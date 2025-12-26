using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseSysNoticeVMs
{
    public partial class BaseSysNoticeVM : BaseCRUDVM<BaseSysNotice>
    {
        
        public List<string> BaseDataBaseSysNoticeFTempSelected { get; set; }

        public BaseSysNoticeVM()
        {
            
        }

        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<BaseSysNotice> SetDuplicatedCheck()
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
