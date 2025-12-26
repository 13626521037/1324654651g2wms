
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

namespace WMS.ViewModel.KnifeManagement.KnifeCombineVMs
{
    public partial class KnifeCombineBatchVM : BaseBatchVM<KnifeCombine, KnifeCombine_BatchEdit>
    {
        public KnifeCombineBatchVM()
        {
            ListVM = new KnifeCombineListVM();
            LinkedVM = new KnifeCombine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeCombine_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeCombineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}