
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
namespace WMS.ViewModel.KnifeManagement.KnifeCombineVMs
{
    public partial class KnifeCombineSearcher : BaseSearcher
    {

        public List<string> KnifeManagementKnifeCombineSTempSelected { get; set; }
        [Display(Name = "_Model._KnifeCombine._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeCombine._CheckOutBy")]
        public Guid? CheckOutById { get; set; }
        [Display(Name = "_Model._KnifeCombine._HandledBy")]
        public string HandledById { get; set; }
        public List<ComboSelectListItem> AllHandledBys { get; set; }
        [Display(Name = "_Model._KnifeCombine._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeCombine._ApprovedTime")]
        public DateRange ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeCombine._CloseTime")]
        public DateRange CloseTime { get; set; }
        [Display(Name = "_Model._KnifeCombine._CombineKnifeNo")]
        public string CombineKnifeNo { get; set; }
        [Display(Name = "成员刀号(单个)")]
        public string MemberKnifeNo { get; set; }
        [Display(Name = "存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._KnifeCombine._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._KnifeCombine._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._KnifeCombine._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._KnifeCombine._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {


        }
    }

}