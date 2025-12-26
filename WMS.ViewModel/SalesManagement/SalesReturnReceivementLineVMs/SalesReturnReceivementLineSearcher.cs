
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;
namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementLineVMs
{
    public partial class SalesReturnReceivementLineSearcher : BaseSearcher
    {
        
        public List<string> SalesManagementSalesReturnReceivementLineSTempSelected { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}