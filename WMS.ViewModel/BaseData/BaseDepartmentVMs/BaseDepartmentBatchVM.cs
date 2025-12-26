
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

namespace WMS.ViewModel.BaseData.BaseDepartmentVMs
{
    public partial class BaseDepartmentBatchVM : BaseBatchVM<BaseDepartment, BaseDepartment_BatchEdit>
    {
        public BaseDepartmentBatchVM()
        {
            ListVM = new BaseDepartmentListVM();
            LinkedVM = new BaseDepartment_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseDepartment_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseDepartmentBTempSelected { get; set; }
        [Display(Name = "_Model._BaseDepartment._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseDepartment._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseDepartment._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}