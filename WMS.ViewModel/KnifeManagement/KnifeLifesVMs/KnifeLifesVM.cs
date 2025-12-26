using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.Model.KnifeManagement;
using WMS.Model;
namespace WMS.ViewModel.KnifeManagement.KnifeLifesVMs
{
    public partial class KnifeLifesVM : BaseCRUDVM<KnifeLifes>
    {
        
        public List<string> KnifeManagementKnifeLifesFTempSelected { get; set; }

        public KnifeLifesVM()
        {
            
        }

        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<KnifeLifes> SetDuplicatedCheck()
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
