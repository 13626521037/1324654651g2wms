
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseSysNoticeVMs
{
    public partial class BaseSysNoticeSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseSysNoticeSTempSelected { get; set; }
        [Display(Name = "_Model._BaseSysNotice._Title")]
        public string Title { get; set; }
        [Display(Name = "_Model._BaseSysNotice._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._BaseSysNotice._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._BaseSysNotice._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._BaseSysNotice._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}