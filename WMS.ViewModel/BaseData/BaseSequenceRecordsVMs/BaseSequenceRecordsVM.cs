using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.ViewModel.BaseData.BaseSequenceRecordsDetailVMs;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseSequenceRecordsVMs
{
    public partial class BaseSequenceRecordsVM : BaseCRUDVM<BaseSequenceRecords>
    {
        
        public List<string> BaseDataBaseSequenceRecordsFTempSelected { get; set; }
        public BaseSequenceRecordsDetailBaseSequenceRecordsDetailListVM BaseSequenceRecordsDetailBaseSequenceRecordsList { get; set; }

        public BaseSequenceRecordsVM()
        {
            
            SetInclude(x => x.SequenceDefine);
            BaseSequenceRecordsDetailBaseSequenceRecordsList = new BaseSequenceRecordsDetailBaseSequenceRecordsDetailListVM();
            BaseSequenceRecordsDetailBaseSequenceRecordsList.DetailGridPrix = "Entity.BaseSequenceRecordsDetail_BaseSequenceRecords";

        }

        protected override void InitVM()
        {
            
            BaseSequenceRecordsDetailBaseSequenceRecordsList.CopyContext(this);

        }

        public override DuplicatedInfo<BaseSequenceRecords> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.SegmentFlag));
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
