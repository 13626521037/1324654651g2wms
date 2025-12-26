
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseWareHouseVMs
{
    public partial class BaseWareHouseSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseWareHouseSTempSelected { get; set; }
        [Display(Name = "_Model._BaseWareHouse._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseWareHouse._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseWareHouse._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._BaseWareHouse._IsProduct")]
        public bool? IsProduct { get; set; }
        [Display(Name = "_Model._BaseWareHouse._ShipType")]
        public WhShipTypeEnum? ShipType { get; set; }
        [Display(Name = "_Model._BaseWareHouse._IsStacking")]
        public bool? IsStacking { get; set; }
        [Display(Name = "_Model._BaseWareHouse._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseWareHouse._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._BaseWareHouse._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}