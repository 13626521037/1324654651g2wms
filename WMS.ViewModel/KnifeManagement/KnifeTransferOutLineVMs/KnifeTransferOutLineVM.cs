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
namespace WMS.ViewModel.KnifeManagement.KnifeTransferOutLineVMs
{
    public partial class KnifeTransferOutLineVM : BaseCRUDVM<KnifeTransferOutLine>
    {
        
        public List<string> KnifeManagementKnifeTransferOutLineFTempSelected { get; set; }

        public KnifeTransferOutLineVM()
        {
            
        }

        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<KnifeTransferOutLine> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.KnifeTransferOutId));
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
