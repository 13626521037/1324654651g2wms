
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

namespace WMS.ViewModel.KnifeManagement.KnifeScrapLineVMs
{
    public partial class KnifeScrapLineBatchVM : BaseBatchVM<KnifeScrapLine, KnifeScrapLine_BatchEdit>
    {
        public KnifeScrapLineBatchVM()
        {
            ListVM = new KnifeScrapLineListVM();
            LinkedVM = new KnifeScrapLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeScrapLine_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeScrapLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}