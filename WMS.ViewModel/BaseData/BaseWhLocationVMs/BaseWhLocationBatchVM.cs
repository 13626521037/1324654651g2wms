
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

namespace WMS.ViewModel.BaseData.BaseWhLocationVMs
{
    public partial class BaseWhLocationBatchVM : BaseBatchVM<BaseWhLocation, BaseWhLocation_BatchEdit>
    {
        public BaseWhLocationBatchVM()
        {
            ListVM = new BaseWhLocationListVM();
            LinkedVM = new BaseWhLocation_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseWhLocation_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseWhLocationBTempSelected { get; set; }
        [Display(Name = "_Model._BaseWhLocation._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseWhLocation._AreaType")]
        public WhLocationEnum? AreaType { get; set; }
        [Display(Name = "_Model._BaseWhLocation._Locked")]
        public bool? Locked { get; set; }
        [Display(Name = "_Model._BaseWhLocation._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseWhLocation._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}