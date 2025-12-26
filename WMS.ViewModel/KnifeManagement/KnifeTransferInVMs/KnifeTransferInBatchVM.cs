
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

namespace WMS.ViewModel.KnifeManagement.KnifeTransferInVMs
{
    public partial class KnifeTransferInBatchVM : BaseBatchVM<KnifeTransferIn, KnifeTransferIn_BatchEdit>
    {
        public KnifeTransferInBatchVM()
        {
            ListVM = new KnifeTransferInListVM();
            LinkedVM = new KnifeTransferIn_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeTransferIn_BatchEdit : BaseVM
    {

        
        public List<string> KnifeManagementKnifeTransferInBTempSelected { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._ApprovedTime")]
        public DateTime? ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._HandledBy")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._Memo")]
        public string Memo { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._TransferOutDocNo")]
        public string TransferOutDocNo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}