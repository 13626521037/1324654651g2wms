
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PrintManagement;
using WMS.Model;
using WMS.Model.BaseData;
namespace WMS.ViewModel.PrintManagement.PrintSupplierVMs
{
    public partial class PrintSupplierSearcher : BaseSearcher
    {
        
        public List<string> PrintManagementPrintSupplierSTempSelected { get; set; }
        [Display(Name = "_Model._PrintSupplier._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PrintSupplier._DocLineNo")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._PrintSupplier._Item")]
        public Guid? ItemId { get; set; }
        [Display(Name = "料号")]
        public string ItemCode { get; set; }
        public List<ComboSelectListItem> AllItems { get; set; }
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
        public List<ComboSelectListItem> AllSuppliers { get; set; }
        [Display(Name = "_Model._PrintSupplier._SyncSupplier")]
        public long? SyncSupplier { get; set; }
        [Display(Name = "_Model._PrintSupplier._SyncItem")]
        public long? SyncItem { get; set; }
        [Display(Name = "_Model._PrintSupplier._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._PrintSupplier._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._PrintSupplier._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._PrintSupplier._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}