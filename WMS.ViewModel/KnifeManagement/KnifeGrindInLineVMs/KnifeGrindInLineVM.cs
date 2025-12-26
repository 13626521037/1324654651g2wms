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
namespace WMS.ViewModel.KnifeManagement.KnifeGrindInLineVMs
{
    public partial class KnifeGrindInLineVM : BaseCRUDVM<KnifeGrindInLine>
    {
        
        public List<string> KnifeManagementKnifeGrindInLineFTempSelected { get; set; }

        public KnifeGrindInLineVM()
        {
            
        }

        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<KnifeGrindInLine> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.KnifeGrindInId));
            rv.AddGroup(SimpleField(x => x.KnifeId));
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
