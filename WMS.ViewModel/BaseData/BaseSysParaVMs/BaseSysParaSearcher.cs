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
    public partial class BaseSysParaSearcher : BaseSearcher
    {
        [Display(Name = "编码")]
        public String Code { get; set; }
        [Display(Name = "名称")]
        public String Name { get; set; }
        [Display(Name = "备注")]
        public String Memo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
