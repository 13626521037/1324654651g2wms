
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

namespace WMS.ViewModel.KnifeManagement.KnifeTransferInLineVMs
{
    public partial class KnifeTransferInLineBatchVM : BaseBatchVM<KnifeTransferInLine, KnifeTransferInLine_BatchEdit>
    {
        public KnifeTransferInLineBatchVM()
        {
            ListVM = new KnifeTransferInLineListVM();
            LinkedVM = new KnifeTransferInLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeTransferInLine_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeTransferInLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}