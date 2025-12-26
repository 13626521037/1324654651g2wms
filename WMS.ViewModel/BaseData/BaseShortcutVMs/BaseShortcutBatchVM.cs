
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

namespace WMS.ViewModel.BaseData.BaseShortcutVMs
{
    public partial class BaseShortcutBatchVM : BaseBatchVM<BaseShortcut, BaseShortcut_BatchEdit>
    {
        public BaseShortcutBatchVM()
        {
            ListVM = new BaseShortcutListVM();
            LinkedVM = new BaseShortcut_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseShortcut_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseShortcutBTempSelected { get; set; }
        [Display(Name = "_Model._BaseShortcut._Menu")]
        public Guid? MenuId { get; set; }
        [Display(Name = "_Model._BaseShortcut._User")]
        public string UserId { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}