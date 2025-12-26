
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

namespace WMS.ViewModel.KnifeManagement.KnifeLifesVMs
{
    public partial class KnifeLifesBatchVM : BaseBatchVM<KnifeLifes, KnifeLifes_BatchEdit>
    {
        public KnifeLifesBatchVM()
        {
            ListVM = new KnifeLifesListVM();
            LinkedVM = new KnifeLifes_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeLifes_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeLifesBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}