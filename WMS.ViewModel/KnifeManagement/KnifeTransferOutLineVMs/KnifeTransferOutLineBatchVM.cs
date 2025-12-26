
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

namespace WMS.ViewModel.KnifeManagement.KnifeTransferOutLineVMs
{
    public partial class KnifeTransferOutLineBatchVM : BaseBatchVM<KnifeTransferOutLine, KnifeTransferOutLine_BatchEdit>
    {
        public KnifeTransferOutLineBatchVM()
        {
            ListVM = new KnifeTransferOutLineListVM();
            LinkedVM = new KnifeTransferOutLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeTransferOutLine_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeTransferOutLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}