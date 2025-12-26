
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

namespace WMS.ViewModel.KnifeManagement.KnifeScrapVMs
{
    public partial class KnifeScrapBatchVM : BaseBatchVM<KnifeScrap, KnifeScrap_BatchEdit>
    {
        public KnifeScrapBatchVM()
        {
            ListVM = new KnifeScrapListVM();
            LinkedVM = new KnifeScrap_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeScrap_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeScrapBTempSelected { get; set; }
        [Display(Name = "_Model._KnifeScrap._DocType")]
        public KnifeScrapTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._KnifeScrap._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeScrap._HandledBy")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeScrap._ApprovedTime")]
        public DateTime? ApprovedTime { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}