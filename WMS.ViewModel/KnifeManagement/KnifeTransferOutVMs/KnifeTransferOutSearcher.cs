
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
using WMS.Model.BaseData;
namespace WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs
{
    public partial class KnifeTransferOutSearcher : BaseSearcher
    {
        
        public List<string> KnifeManagementKnifeTransferOutSTempSelected { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._HandledBy")]
        public string HandledById { get; set; }
        public List<ComboSelectListItem> AllHandledBys { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._ApprovedTime")]
        public DateRange ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._ToWH")]
        public Guid? ToWHId { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._FromWH")]
        public Guid? FromWHId { get; set; }
        public List<ComboSelectListItem> AllFromWHs { get; set; }

        public List<ComboSelectListItem> AllToWHs { get; set; }
        [Display(Name = "存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}