
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

namespace WMS.ViewModel.BaseData.BaseUnitVMs
{
    public partial class BaseUnitBatchVM : BaseBatchVM<BaseUnit, BaseUnit_BatchEdit>
    {
        public BaseUnitBatchVM()
        {
            ListVM = new BaseUnitListVM();
            LinkedVM = new BaseUnit_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseUnit_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseUnitBTempSelected { get; set; }
        [Display(Name = "_Model._BaseUnit._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseUnit._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseUnit._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}