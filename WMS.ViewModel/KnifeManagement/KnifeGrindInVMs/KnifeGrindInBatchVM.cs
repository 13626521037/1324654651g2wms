
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

namespace WMS.ViewModel.KnifeManagement.KnifeGrindInVMs
{
    public partial class KnifeGrindInBatchVM : BaseBatchVM<KnifeGrindIn, KnifeGrindIn_BatchEdit>
    {
        public KnifeGrindInBatchVM()
        {
            ListVM = new KnifeGrindInListVM();
            LinkedVM = new KnifeGrindIn_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeGrindIn_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeGrindInBTempSelected { get; set; }
        [Display(Name = "_Model._KnifeGrindIn._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeGrindIn._HandledBy")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeGrindIn._ApprovedTime")]
        public DateTime? ApprovedTime { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}