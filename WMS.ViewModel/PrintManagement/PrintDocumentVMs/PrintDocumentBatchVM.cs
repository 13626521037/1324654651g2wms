
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

namespace WMS.ViewModel.PrintManagement.PrintDocumentVMs
{
    public partial class PrintDocumentBatchVM : BaseBatchVM<PrintDocument, PrintDocument_BatchEdit>
    {
        public PrintDocumentBatchVM()
        {
            ListVM = new PrintDocumentListVM();
            LinkedVM = new PrintDocument_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PrintDocument_BatchEdit : BaseVM
    {

        
        public List<string> PrintManagementPrintDocumentBTempSelected { get; set; }
        [Display(Name = "_Model._PrintDocument._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PrintDocument._DocLineNo")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._PrintDocument._ItemID")]
        public long? ItemID { get; set; }
        [Display(Name = "_Model._PrintDocument._ItemCode")]
        public string ItemCode { get; set; }
        [Display(Name = "_Model._PrintDocument._ItemName")]
        public string ItemName { get; set; }
        [Display(Name = "_Model._PrintDocument._SPECS")]
        public string SPECS { get; set; }
        [Display(Name = "_Model._PrintDocument._Description")]
        public string Description { get; set; }
        [Display(Name = "_Model._PrintDocument._UnitName")]
        public string UnitName { get; set; }
        [Display(Name = "_Model._PrintDocument._Qty")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._PrintDocument._ReceivedQty")]
        public decimal? ReceivedQty { get; set; }
        [Display(Name = "_Model._PrintDocument._ValidQty")]
        public decimal? ValidQty { get; set; }
        [Display(Name = "_Model._PrintDocument._Seiban")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._PrintDocument._SeibanRandom")]
        public string SeibanRandom { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}