
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseSequenceRecordsVMs
{
    public partial class BaseSequenceRecordsSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseSequenceRecordsSTempSelected { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._SequenceDefine")]
        public Guid? SequenceDefineId { get; set; }
        public List<ComboSelectListItem> AllSequenceDefines { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._SegmentFlag")]
        public string SegmentFlag { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._SerialValue")]
        public int? SerialValue { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}