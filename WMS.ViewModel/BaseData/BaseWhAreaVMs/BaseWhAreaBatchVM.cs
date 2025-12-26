
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

namespace WMS.ViewModel.BaseData.BaseWhAreaVMs
{
    public partial class BaseWhAreaBatchVM : BaseBatchVM<BaseWhArea, BaseWhArea_BatchEdit>
    {
        public BaseWhAreaBatchVM()
        {
            ListVM = new BaseWhAreaListVM();
            LinkedVM = new BaseWhArea_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseWhArea_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseWhAreaBTempSelected { get; set; }
        [Display(Name = "_Model._BaseWhArea._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseWhArea._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseWhArea._WareHouse")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._BaseWhArea._AreaType")]
        public WhAreaEnum? AreaType { get; set; }
        [Display(Name = "_Model._BaseWhArea._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseWhArea._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}