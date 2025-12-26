
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

namespace WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs
{
    public partial class KnifeGrindRequestBatchVM : BaseBatchVM<KnifeGrindRequest, KnifeGrindRequest_BatchEdit>
    {
        public KnifeGrindRequestBatchVM()
        {
            ListVM = new KnifeGrindRequestListVM();
            LinkedVM = new KnifeGrindRequest_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeGrindRequest_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeGrindRequestBTempSelected { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._HandledBy")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._ApprovedTime")]
        public DateTime? ApprovedTime { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}