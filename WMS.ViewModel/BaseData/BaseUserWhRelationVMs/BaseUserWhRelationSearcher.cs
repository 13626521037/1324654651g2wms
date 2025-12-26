
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseUserWhRelationVMs
{
    public partial class BaseUserWhRelationSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseUserWhRelationSTempSelected { get; set; }
        [Display(Name = "_Model._BaseUserWhRelation._User")]
        public string UserId { get; set; }
        public List<ComboSelectListItem> AllUsers { get; set; }
        [Display(Name = "_Model._BaseUserWhRelation._Wh")]
        public Guid? WhId { get; set; }
        public List<ComboSelectListItem> AllWhs { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}