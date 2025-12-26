
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseShortcutVMs
{
    public partial class BaseShortcutSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseShortcutSTempSelected { get; set; }
        [Display(Name = "_Model._BaseShortcut._Menu")]
        public Guid? MenuId { get; set; }
        public List<ComboSelectListItem> AllMenus { get; set; }
        [Display(Name = "_Model._BaseShortcut._User")]
        public string UserId { get; set; }
        public List<ComboSelectListItem> AllUsers { get; set; }
        [Display(Name = "_Model._BaseShortcut._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._BaseShortcut._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._BaseShortcut._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._BaseShortcut._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}