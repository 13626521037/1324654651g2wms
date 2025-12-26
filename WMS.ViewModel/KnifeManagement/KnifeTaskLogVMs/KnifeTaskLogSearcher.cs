
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
namespace WMS.ViewModel.KnifeManagement.KnifeTaskLogVMs
{
    public partial class KnifeTaskLogSearcher : BaseSearcher
    {
        
        public List<string> KnifeManagementKnifeTaskLogSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}