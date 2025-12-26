
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

namespace WMS.ViewModel.BaseData.BaseOperatorVMs
{
    public partial class BaseOperatorBatchVM : BaseBatchVM<BaseOperator, BaseOperator_BatchEdit>
    {
        public BaseOperatorBatchVM()
        {
            ListVM = new BaseOperatorListVM();
            LinkedVM = new BaseOperator_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseOperator_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseOperatorBTempSelected { get; set; }
        [Display(Name = "_Model._BaseOperator._JobID")]
        public string JobID { get; set; }
        [Display(Name = "_Model._BaseOperator._OAAccount")]
        public string OAAccount { get; set; }
        [Display(Name = "_Model._BaseOperator._IDCard")]
        public string IDCard { get; set; }
        [Display(Name = "_Model._BaseOperator._TempAuthCode")]
        public string TempAuthCode { get; set; }
        [Display(Name = "_Model._BaseOperator._TACExpired")]
        public DateTime? TACExpired { get; set; }
        [Display(Name = "_Model._BaseOperator._Department")]
        public Guid? DepartmentId { get; set; }
        [Display(Name = "_Model._BaseOperator._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseOperator._Phone")]
        public string Phone { get; set; }
        [Display(Name = "_Model._BaseOperator._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}