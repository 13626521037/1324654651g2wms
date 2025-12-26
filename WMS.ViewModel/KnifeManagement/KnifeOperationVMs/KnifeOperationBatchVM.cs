
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

namespace WMS.ViewModel.KnifeManagement.KnifeOperationVMs
{
    public partial class KnifeOperationBatchVM : BaseBatchVM<KnifeOperation, KnifeOperation_BatchEdit>
    {
        public KnifeOperationBatchVM()
        {
            ListVM = new KnifeOperationListVM();
            LinkedVM = new KnifeOperation_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeOperation_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeOperationBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}