
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeTaskLogVMs
{
    public partial class KnifeTaskLogBatchVM : BaseBatchVM<KnifeTaskLog, KnifeTaskLog_BatchEdit>
    {
        public KnifeTaskLogBatchVM()
        {
            ListVM = new KnifeTaskLogListVM();
            LinkedVM = new KnifeTaskLog_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeTaskLog_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeTaskLogBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}