
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

namespace WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs
{
    public partial class KnifeGrindOutBatchVM : BaseBatchVM<KnifeGrindOut, KnifeGrindOut_BatchEdit>
    {
        public KnifeGrindOutBatchVM()
        {
            ListVM = new KnifeGrindOutListVM();
            LinkedVM = new KnifeGrindOut_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeGrindOut_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeGrindOutBTempSelected { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._HandledBy")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._ApprovedTime")]
        public DateTime? ApprovedTime { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}