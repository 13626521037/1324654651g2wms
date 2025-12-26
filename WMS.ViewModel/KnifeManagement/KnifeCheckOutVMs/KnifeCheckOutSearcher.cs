
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
namespace WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs
{
    public partial class KnifeCheckOutSearcher : BaseSearcher
    {
        
        public List<string> KnifeManagementKnifeCheckOutSTempSelected { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._DocType")]
        public KnifeCheckOutTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._CheckOutBy")]
        public Guid? CheckOutById { get; set; }
        public List<ComboSelectListItem> AllCheckOutBys { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._HandledBy")]
        public string HandledById { get; set; }
        public List<ComboSelectListItem> AllHandledBys { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._Status")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._ApprovedTime")]
        public DateRange ApprovedTime { get; set; }
        [Display(Name = "存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}