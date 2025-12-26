
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

namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementLineVMs
{
    public partial class SalesReturnReceivementLineBatchVM : BaseBatchVM<SalesReturnReceivementLine, SalesReturnReceivementLine_BatchEdit>
    {
        public SalesReturnReceivementLineBatchVM()
        {
            ListVM = new SalesReturnReceivementLineListVM();
            LinkedVM = new SalesReturnReceivementLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SalesReturnReceivementLine_BatchEdit : BaseVM
    {

        
        public List<string> SalesManagementSalesReturnReceivementLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}