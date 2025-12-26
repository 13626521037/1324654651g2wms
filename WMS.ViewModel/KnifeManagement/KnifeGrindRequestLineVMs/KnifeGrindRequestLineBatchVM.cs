
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

namespace WMS.ViewModel.KnifeManagement.KnifeGrindRequestLineVMs
{
    public partial class KnifeGrindRequestLineBatchVM : BaseBatchVM<KnifeGrindRequestLine, KnifeGrindRequestLine_BatchEdit>
    {
        public KnifeGrindRequestLineBatchVM()
        {
            ListVM = new KnifeGrindRequestLineListVM();
            LinkedVM = new KnifeGrindRequestLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeGrindRequestLine_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeGrindRequestLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}