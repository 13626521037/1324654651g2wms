using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;


namespace WMS.ViewModel.KnifeManagement.KnifeVMs
{
    public partial class KnifeApiBatchVM : BaseBatchVM<Knife, KnifeApi_BatchEdit>
    {
        public KnifeApiBatchVM()
        {
            ListVM = new KnifeApiListVM();
            LinkedVM = new KnifeApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class KnifeApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
