
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseSequenceDefineLineVMs
{
    public partial class BaseSequenceDefineLineSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseSequenceDefineLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}