
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

namespace WMS.ViewModel.PrintManagement.PrintSupplierVMs
{
    public partial class PrintSupplierBatchVM : BaseBatchVM<PrintSupplier, PrintSupplier_BatchEdit>
    {
        public PrintSupplierBatchVM()
        {
            ListVM = new PrintSupplierListVM();
            LinkedVM = new PrintSupplier_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PrintSupplier_BatchEdit : BaseVM
    {

        
        public List<string> PrintManagementPrintSupplierBTempSelected { get; set; }
        [Display(Name = "_Model._PrintSupplier._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PrintSupplier._DocLineNo")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._PrintSupplier._Item")]
        public Guid? ItemId { get; set; }
        [Display(Name = "_Model._PrintSupplier._Qty")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._PrintSupplier._ReceivedQty")]
        public decimal? ReceivedQty { get; set; }
        [Display(Name = "_Model._PrintSupplier._ValidQty")]
        public decimal? ValidQty { get; set; }
        [Display(Name = "_Model._PrintSupplier._BatchNumber")]
        public string BatchNumber { get; set; }
        [Display(Name = "_Model._PrintSupplier._Seiban")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._PrintSupplier._SeibanRandom")]
        public string SeibanRandom { get; set; }
        [Display(Name = "_Model._PrintSupplier._Weight")]
        public decimal? Weight { get; set; }
        [Display(Name = "_Model._PrintSupplier._Supplier")]
        public Guid? SupplierId { get; set; }
        [Display(Name = "_Model._PrintSupplier._SyncSupplier")]
        public long? SyncSupplier { get; set; }
        [Display(Name = "_Model._PrintSupplier._SyncItem")]
        public long? SyncItem { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}