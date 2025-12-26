
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSequenceRecordsVMs
{
    public partial class BaseSequenceRecordsBatchVM : BaseBatchVM<BaseSequenceRecords, BaseSequenceRecords_BatchEdit>
    {
        public BaseSequenceRecordsBatchVM()
        {
            ListVM = new BaseSequenceRecordsListVM();
            LinkedVM = new BaseSequenceRecords_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseSequenceRecords_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseSequenceRecordsBTempSelected { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._SequenceDefine")]
        public Guid? SequenceDefineId { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._SerialValue")]
        public int? SerialValue { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}