using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;


namespace WMS.ViewModel.BaseData.BaseBarCodeVMs
{
    public partial class BaseBarCodeApiBatchVM : BaseBatchVM<BaseBarCode, BaseBarCodeApi_BatchEdit>
    {
        public BaseBarCodeApiBatchVM()
        {
            ListVM = new BaseBarCodeApiListVM();
            LinkedVM = new BaseBarCodeApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseBarCodeApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
