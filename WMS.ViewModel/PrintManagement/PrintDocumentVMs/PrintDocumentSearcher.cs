
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PrintManagement;
using WMS.Model;
namespace WMS.ViewModel.PrintManagement.PrintDocumentVMs
{
    public partial class PrintDocumentSearcher : BaseSearcher
    {
        
        public List<string> PrintManagementPrintDocumentSTempSelected { get; set; }
        [Display(Name = "_Model._PrintDocument._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PrintDocument._DocLineNo")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._PrintDocument._ItemID")]
        public long? ItemID { get; set; }
        [Display(Name = "_Model._PrintDocument._ItemCode")]
        public string ItemCode { get; set; }
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
        [Display(Name = "_Model._PrintDocument._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._PrintDocument._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._PrintDocument._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._PrintDocument._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}