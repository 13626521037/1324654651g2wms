
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

namespace WMS.ViewModel.BaseData.BaseUserWhRelationVMs
{
    public partial class BaseUserWhRelationBatchVM : BaseBatchVM<BaseUserWhRelation, BaseUserWhRelation_BatchEdit>
    {
        public BaseUserWhRelationBatchVM()
        {
            ListVM = new BaseUserWhRelationListVM();
            LinkedVM = new BaseUserWhRelation_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseUserWhRelation_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseUserWhRelationBTempSelected { get; set; }
        [Display(Name = "_Model._BaseUserWhRelation._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}