
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

namespace WMS.ViewModel.KnifeManagement.KnifeGrindInLineVMs
{
    public partial class KnifeGrindInLineBatchVM : BaseBatchVM<KnifeGrindInLine, KnifeGrindInLine_BatchEdit>
    {
        public KnifeGrindInLineBatchVM()
        {
            ListVM = new KnifeGrindInLineListVM();
            LinkedVM = new KnifeGrindInLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeGrindInLine_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeGrindInLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}