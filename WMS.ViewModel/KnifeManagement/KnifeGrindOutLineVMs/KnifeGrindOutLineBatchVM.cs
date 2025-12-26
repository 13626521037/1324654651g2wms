
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

namespace WMS.ViewModel.KnifeManagement.KnifeGrindOutLineVMs
{
    public partial class KnifeGrindOutLineBatchVM : BaseBatchVM<KnifeGrindOutLine, KnifeGrindOutLine_BatchEdit>
    {
        public KnifeGrindOutLineBatchVM()
        {
            ListVM = new KnifeGrindOutLineListVM();
            LinkedVM = new KnifeGrindOutLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeGrindOutLine_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeGrindOutLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}