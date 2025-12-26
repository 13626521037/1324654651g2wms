
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

namespace WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs
{
    public partial class KnifeTransferOutBatchVM : BaseBatchVM<KnifeTransferOut, KnifeTransferOut_BatchEdit>
    {
        public KnifeTransferOutBatchVM()
        {
            ListVM = new KnifeTransferOutListVM();
            LinkedVM = new KnifeTransferOut_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeTransferOut_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeTransferOutBTempSelected { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._HandledBy")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._ApprovedTime")]
        public DateTime? ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._ToWH")]
        public Guid? ToWHId { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}