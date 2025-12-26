
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
namespace WMS.ViewModel.KnifeManagement.KnifeCombineLineVMs
{
    public partial class KnifeCombineLineSearcher : BaseSearcher
    {
        
        public List<string> KnifeManagementKnifeCombineLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}