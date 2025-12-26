
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

namespace WMS.ViewModel.KnifeManagement.KnifeCheckInVMs
{
    public partial class KnifeCheckInBatchVM : BaseBatchVM<KnifeCheckIn, KnifeCheckIn_BatchEdit>
    {
        public KnifeCheckInBatchVM()
        {
            ListVM = new KnifeCheckInListVM();
            LinkedVM = new KnifeCheckIn_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeCheckIn_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeCheckInBTempSelected { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._DocType")]
        public KnifeCheckInTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._CheckInBy")]
        public Guid? CheckInById { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._HandledBy")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._ApprovedTime")]
        public DateTime? ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._CombineKnifeNo")]
        public string CombineKnifeNo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}