
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
namespace WMS.ViewModel.KnifeManagement.KnifeTransferInVMs
{
    public partial class KnifeTransferInSearcher : BaseSearcher
    {
        
        public List<string> KnifeManagementKnifeTransferInSTempSelected { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._ApprovedTime")]
        public DateRange ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._HandledBy")]
        public string HandledById { get; set; }
        public List<ComboSelectListItem> AllHandledBys { get; set; }


        [Display(Name = "调入存储地点")]
        public Guid? ToWHId { get; set; }
        [Display(Name = "调出存储地点")]
        public Guid? FromWHId { get; set; }
        public List<ComboSelectListItem> AllFromWHs { get; set; }
        public List<ComboSelectListItem> AllToWHs { get; set; }


        [Display(Name = "_Model._KnifeTransferIn._Memo")]
        public string Memo { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._TransferOutDocNo")]
        public string TransferOutDocNo { get; set; }
        [Display(Name = "存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}