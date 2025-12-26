using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;


namespace WMS.ViewModel.BaseData.BaseSysParaVMs
{
    public partial class BaseSysParaBatchVM : BaseBatchVM<BaseSysPara, BaseSysPara_BatchEdit>
    {
        public BaseSysParaBatchVM()
        {
            ListVM = new BaseSysParaListVM();
            LinkedVM = new BaseSysPara_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseSysPara_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
