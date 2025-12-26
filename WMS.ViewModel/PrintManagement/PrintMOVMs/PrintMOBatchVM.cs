
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.PrintManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.PrintManagement.PrintMOVMs
{
    public partial class PrintMOBatchVM : BaseBatchVM<PrintMO, PrintMO_BatchEdit>
    {
        public PrintMOBatchVM()
        {
            ListVM = new PrintMOListVM();
            LinkedVM = new PrintMO_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PrintMO_BatchEdit : BaseVM
    {

        
        public List<string> PrintManagementPrintMOBTempSelected { get; set; }
        [Display(Name = "_Model._PrintMO._CustomerCode")]
        public string CustomerCode { get; set; }
        [Display(Name = "_Model._PrintMO._OrderWhName")]
        public string OrderWhName { get; set; }
        [Display(Name = "_Model._PrintMO._CustomerSPECS")]
        public string CustomerSPECS { get; set; }
        [Display(Name = "_Model._PrintMO._Item")]
        public Guid? ItemId { get; set; }
        [Display(Name = "_Model._PrintMO._Seiban")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._PrintMO._SeibanRandom")]
        public string SeibanRandom { get; set; }
        [Display(Name = "_Model._PrintMO._BatchNumber")]
        public string BatchNumber { get; set; }
        [Display(Name = "_Model._PrintMO._Qty")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._PrintMO._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PrintMO._DocNoChange")]
        public string DocNoChange { get; set; }
        [Display(Name = "_Model._PrintMO._LocationName")]
        public string LocationName { get; set; }
        [Display(Name = "_Model._PrintMO._Org")]
        public Guid? OrgId { get; set; }
        [Display(Name = "_Model._PrintMO._SyncItem")]
        public long? SyncItem { get; set; }
        [Display(Name = "_Model._PrintMO._SyncOrg")]
        public long? SyncOrg { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}