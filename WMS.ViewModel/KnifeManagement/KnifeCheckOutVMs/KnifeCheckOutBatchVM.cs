
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
using WMS.Model.BaseData;

namespace WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs
{
    public partial class KnifeCheckOutBatchVM : BaseBatchVM<KnifeCheckOut, KnifeCheckOut_BatchEdit>
    {
        public KnifeCheckOutBatchVM()
        {
            ListVM = new KnifeCheckOutListVM();
            LinkedVM = new KnifeCheckOut_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeCheckOut_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeCheckOutBTempSelected { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._DocType")]
        public KnifeCheckOutTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._CheckOutBy")]
        public Guid? CheckOutById { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._HandledBy")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._ApprovedTime")]
        public DateTime? ApprovedTime { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}