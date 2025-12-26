
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseWhAreaVMs
{
    public partial class BaseWhAreaSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseWhAreaSTempSelected { get; set; }
        [Display(Name = "_Model._BaseWhArea._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseWhArea._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseWhArea._WareHouse")]
        public Guid? WareHouseId { get; set; }
        public List<ComboSelectListItem> AllWareHouses { get; set; }
        [Display(Name = "_Model._BaseWhArea._AreaType")]
        public WhAreaEnum? AreaType { get; set; }
        [Display(Name = "_Model._BaseWhArea._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}