
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
namespace WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs
{
    public partial class KnifeGrindRequestSearcher : BaseSearcher
    {
        
        public List<string> KnifeManagementKnifeGrindRequestSTempSelected { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._HandledBy")]
        public string HandledById { get; set; }
        public List<ComboSelectListItem> AllHandledBys { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._ApprovedTime")]
        public DateRange ApprovedTime { get; set; }
        [Display(Name = "存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}