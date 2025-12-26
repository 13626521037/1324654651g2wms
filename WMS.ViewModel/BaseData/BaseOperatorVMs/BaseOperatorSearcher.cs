
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseOperatorVMs
{
    public partial class BaseOperatorSearcher : BaseSearcher
    {
        [Display(Name = "编码")]
        public string Code { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        public List<string> BaseDataBaseOperatorSTempSelected { get; set; }
        [Display(Name = "_Model._BaseOperator._JobID")]
        public string JobID { get; set; }
        [Display(Name = "_Model._BaseOperator._OAAccount")]
        public string OAAccount { get; set; }
        [Display(Name = "_Model._BaseOperator._IDCard")]
        public string IDCard { get; set; }
        [Display(Name = "_Model._BaseOperator._TempAuthCode")]
        public string TempAuthCode { get; set; }
        [Display(Name = "_Model._BaseOperator._TACExpired")]
        public DateRange TACExpired { get; set; }
        [Display(Name = "部门")]
        public string Department { get; set; }
        //[Display(Name = "_Model._BaseOperator._Department")]
        //public Guid? DepartmentId { get; set; }
        //public List<ComboSelectListItem> AllDepartments { get; set; }
        [Display(Name = "_Model._BaseOperator._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseOperator._Phone")]
        public string Phone { get; set; }
        [Display(Name = "_Model._BaseOperator._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._BaseOperator._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }
        [Display(Name = "_Model._BaseOperator._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._BaseOperator._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._BaseOperator._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._BaseOperator._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {


        }
    }

}