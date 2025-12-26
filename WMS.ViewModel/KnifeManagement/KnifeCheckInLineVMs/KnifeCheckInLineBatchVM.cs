
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

namespace WMS.ViewModel.KnifeManagement.KnifeCheckInLineVMs
{
    public partial class KnifeCheckInLineBatchVM : BaseBatchVM<KnifeCheckInLine, KnifeCheckInLine_BatchEdit>
    {
        public KnifeCheckInLineBatchVM()
        {
            ListVM = new KnifeCheckInLineListVM();
            LinkedVM = new KnifeCheckInLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeCheckInLine_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeCheckInLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}