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
    public partial class BaseInventoryApiSearcher : BaseSearcher
    {
        [Display(Name = "_Model._BaseInventory._BatchNumber")]
        public String BatchNumber { get; set; }
        [Display(Name = "_Model._BaseInventory._SerialNumber")]
        public String SerialNumber { get; set; }
        [Display(Name = "_Model._BaseInventory._Seiban")]
        public String Seiban { get; set; }

        protected override void InitVM()
        {
        }

    }
}
