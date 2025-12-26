
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseSequenceRecordsDetailVMs
{
    public partial class BaseSequenceRecordsDetailSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseSequenceRecordsDetailSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}