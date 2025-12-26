
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

namespace WMS.ViewModel.BaseData.BaseSequenceRecordsDetailVMs
{
    public partial class BaseSequenceRecordsDetailBatchVM : BaseBatchVM<BaseSequenceRecordsDetail, BaseSequenceRecordsDetail_BatchEdit>
    {
        public BaseSequenceRecordsDetailBatchVM()
        {
            ListVM = new BaseSequenceRecordsDetailListVM();
            LinkedVM = new BaseSequenceRecordsDetail_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseSequenceRecordsDetail_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseSequenceRecordsDetailBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}