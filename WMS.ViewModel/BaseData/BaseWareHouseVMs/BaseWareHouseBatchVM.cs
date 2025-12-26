
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseWareHouseVMs
{
    public partial class BaseWareHouseBatchVM : BaseBatchVM<BaseWareHouse, BaseWareHouse_BatchEdit>
    {
        public BaseWareHouseBatchVM()
        {
            ListVM = new BaseWareHouseListVM();
            LinkedVM = new BaseWareHouse_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseWareHouse_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseWareHouseBTempSelected { get; set; }
        [Display(Name = "_Model._BaseWareHouse._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseWareHouse._IsProduct")]
        public bool? IsProduct { get; set; }
        [Display(Name = "_Model._BaseWareHouse._ShipType")]
        public WhShipTypeEnum? ShipType { get; set; }
        [Display(Name = "_Model._BaseWareHouse._IsStacking")]
        public bool? IsStacking { get; set; }
        [Display(Name = "_Model._BaseWareHouse._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseWareHouse._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}