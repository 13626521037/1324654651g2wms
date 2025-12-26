
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.SalesManagement;
using WMS.Model;

namespace WMS.ViewModel.SalesManagement.SalesRMALineVMs
{
    public partial class SalesRMALineBatchVM : BaseBatchVM<SalesRMALine, SalesRMALine_BatchEdit>
    {
        public SalesRMALineBatchVM()
        {
            ListVM = new SalesRMALineListVM();
            LinkedVM = new SalesRMALine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SalesRMALine_BatchEdit : BaseVM
    {

        
        public List<string> SalesManagementSalesRMALineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}