using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;


namespace WMS.ViewModel.BaseData.BaseInventoryVMs
{
    public partial class BaseInventoryApiBatchVM : BaseBatchVM<BaseInventory, BaseInventoryApi_BatchEdit>
    {
        public BaseInventoryApiBatchVM()
        {
            ListVM = new BaseInventoryApiListVM();
            LinkedVM = new BaseInventoryApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseInventoryApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
