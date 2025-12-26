
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseWhLocationVMs
{
    public partial class BaseWhLocationSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseWhLocationSTempSelected { get; set; }
        [Display(Name = "_Model._BaseWhLocation._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseWhLocation._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseWhLocation._WhArea")]
        public Guid? WhAreaId { get; set; }
        public List<ComboSelectListItem> AllWhAreas { get; set; }
        [Display(Name = "_Model._BaseWhLocation._AreaType")]
        public WhLocationEnum? AreaType { get; set; }
        [Display(Name = "_Model._BaseWhLocation._Locked")]
        public bool? Locked { get; set; }
        [Display(Name = "_Model._BaseWhLocation._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "存储地点")]
        public Guid? WareHouseId { get; set; }
        public List<ComboSelectListItem> AllWhs { get; set; }
        protected override void InitVM()
        {
            

        }
    }

}