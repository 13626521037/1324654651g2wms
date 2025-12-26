
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

namespace WMS.ViewModel.SalesManagement.SalesShipLineVMs
{
    public partial class SalesShipLineBatchVM : BaseBatchVM<SalesShipLine, SalesShipLine_BatchEdit>
    {
        public SalesShipLineBatchVM()
        {
            ListVM = new SalesShipLineListVM();
            LinkedVM = new SalesShipLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SalesShipLine_BatchEdit : BaseVM
    {

        
        public List<string> SalesManagementSalesShipLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}