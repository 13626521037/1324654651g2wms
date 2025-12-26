
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
namespace WMS.ViewModel.KnifeManagement.KnifeGrindOutLineVMs
{
    public partial class KnifeGrindOutLineSearcher : BaseSearcher
    {
        
        public List<string> KnifeManagementKnifeGrindOutLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}