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
namespace WMS.ViewModel.BaseData.BaseSequenceRecordsDetailVMs
{
    public partial class BaseSequenceRecordsDetailVM : BaseCRUDVM<BaseSequenceRecordsDetail>
    {
        
        public List<string> BaseDataBaseSequenceRecordsDetailFTempSelected { get; set; }

        public BaseSequenceRecordsDetailVM()
        {
            
        }

        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<BaseSequenceRecordsDetail> SetDuplicatedCheck()
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
