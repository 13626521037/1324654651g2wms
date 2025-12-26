
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

namespace WMS.ViewModel.BaseData.BaseSysNoticeVMs
{
    public partial class BaseSysNoticeBatchVM : BaseBatchVM<BaseSysNotice, BaseSysNotice_BatchEdit>
    {
        public BaseSysNoticeBatchVM()
        {
            ListVM = new BaseSysNoticeListVM();
            LinkedVM = new BaseSysNotice_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseSysNotice_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseSysNoticeBTempSelected { get; set; }
        [Display(Name = "_Model._BaseSysNotice._Title")]
        public string Title { get; set; }
        [Display(Name = "_Model._BaseSysNotice._Content")]
        public string Content { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}