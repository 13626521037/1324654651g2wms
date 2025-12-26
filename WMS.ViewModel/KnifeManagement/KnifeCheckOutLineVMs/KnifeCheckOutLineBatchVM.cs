
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

namespace WMS.ViewModel.KnifeManagement.KnifeCheckOutLineVMs
{
    public partial class KnifeCheckOutLineBatchVM : BaseBatchVM<KnifeCheckOutLine, KnifeCheckOutLine_BatchEdit>
    {
        public KnifeCheckOutLineBatchVM()
        {
            ListVM = new KnifeCheckOutLineListVM();
            LinkedVM = new KnifeCheckOutLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeCheckOutLine_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeCheckOutLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}