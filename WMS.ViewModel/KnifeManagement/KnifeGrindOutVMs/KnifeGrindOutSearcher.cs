
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
namespace WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs
{
    public partial class KnifeGrindOutSearcher : BaseSearcher
    {
        
        public List<string> KnifeManagementKnifeGrindOutSTempSelected { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._HandledBy")]
        public string HandledById { get; set; }
        public List<ComboSelectListItem> AllHandledBys { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._ApprovedTime")]
        public DateRange ApprovedTime { get; set; }
        [Display(Name = "存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}