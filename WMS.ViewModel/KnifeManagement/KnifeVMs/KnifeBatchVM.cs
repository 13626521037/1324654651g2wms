
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

namespace WMS.ViewModel.KnifeManagement.KnifeVMs
{
    public partial class KnifeBatchVM : BaseBatchVM<Knife, Knife_BatchEdit>
    {
        public KnifeBatchVM()
        {
            ListVM = new KnifeListVM();
            LinkedVM = new Knife_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Knife_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}