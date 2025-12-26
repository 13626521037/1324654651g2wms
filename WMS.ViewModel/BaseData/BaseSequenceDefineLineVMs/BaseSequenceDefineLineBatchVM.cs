
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

namespace WMS.ViewModel.BaseData.BaseSequenceDefineLineVMs
{
    public partial class BaseSequenceDefineLineBatchVM : BaseBatchVM<BaseSequenceDefineLine, BaseSequenceDefineLine_BatchEdit>
    {
        public BaseSequenceDefineLineBatchVM()
        {
            ListVM = new BaseSequenceDefineLineListVM();
            LinkedVM = new BaseSequenceDefineLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseSequenceDefineLine_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseSequenceDefineLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}