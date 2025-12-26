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
namespace WMS.ViewModel.KnifeManagement.KnifeTransferInLineVMs
{
    public partial class KnifeTransferInLineVM : BaseCRUDVM<KnifeTransferInLine>
    {
        
        public List<string> KnifeManagementKnifeTransferInLineFTempSelected { get; set; }

        public KnifeTransferInLineVM()
        {
            
        }

        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<KnifeTransferInLine> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.KnifeTransferInId));
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
