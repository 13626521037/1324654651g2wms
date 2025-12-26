
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
namespace WMS.ViewModel.KnifeManagement.KnifeGrindInLineVMs
{
    public partial class KnifeGrindInLineSearcher : BaseSearcher
    {
        
        public List<string> KnifeManagementKnifeGrindInLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}