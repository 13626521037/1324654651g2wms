
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
namespace WMS.ViewModel.KnifeManagement.KnifeCheckInVMs
{
    public partial class KnifeCheckInSearcher : BaseSearcher
    {
        
        public List<string> KnifeManagementKnifeCheckInSTempSelected { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._DocType")]
        public KnifeCheckInTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._CheckInBy")]
        public Guid? CheckInById { get; set; }
        public List<ComboSelectListItem> AllCheckInBys { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._HandledBy")]
        public string HandledById { get; set; }
        public List<ComboSelectListItem> AllHandledBys { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._ApprovedTime")]
        public DateRange ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._CombineKnifeNo")]
        public string CombineKnifeNo { get; set; }
        [Display(Name = "存储地点")]
        public Guid? WareHouseId { get; set; }

        [Display(Name = "_Model._KnifeCheckIn._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}