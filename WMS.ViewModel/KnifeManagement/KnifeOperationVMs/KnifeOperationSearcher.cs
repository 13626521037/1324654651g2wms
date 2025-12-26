
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
namespace WMS.ViewModel.KnifeManagement.KnifeOperationVMs
{
    public partial class KnifeOperationSearcher : BaseSearcher
    {

        public List<string> KnifeManagementKnifeOperationSTempSelected { get; set; }
        [Display(Name = "_Model._KnifeOperation._Knife")]
        public Guid? KnifeId { get; set; }
        public List<ComboSelectListItem> AllKnifes { get; set; }
        [Display(Name = "_Model._KnifeOperation._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeOperation._OperationType")]
        public KnifeOperationTypeEnum? OperationType { get; set; }
        [Display(Name = "_Model._KnifeOperation._OperationTime")]
        public DateRange OperationTime { get; set; }
        [Display(Name = "_Model._KnifeOperation._OperationBy")]
        public Guid? OperationById { get; set; }
        public List<ComboSelectListItem> AllOperationBys { get; set; }
        [Display(Name = "_Model._KnifeOperation._HandledBy")]
        public string HandledById { get; set; }
        public List<ComboSelectListItem> AllHandledBys { get; set; }
        [Display(Name = "_Model._KnifeOperation._UsedDays")]
        public decimal? UsedDays { get; set; }
        [Display(Name = "_Model._KnifeOperation._RemainingDays")]
        public decimal? RemainingDays { get; set; }
        [Display(Name = "_Model._KnifeOperation._WhLocation")]
        public Guid? WhLocationId { get; set; }
        [Display(Name = "存储地点")]
        public Guid? WareHouseId { get; set; }
        public List<ComboSelectListItem> AllWhLocations { get; set; }
        [Display(Name = "_Model._KnifeOperation._GrindNum")]
        public decimal? GrindNum { get; set; }
        [Display(Name = "_Model._KnifeOperation._U9SourceLineID")]
        public string U9SourceLineID { get; set; }
        [Display(Name = "_Model._KnifeOperation._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._KnifeOperation._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._KnifeOperation._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._KnifeOperation._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {


        }
    }

}