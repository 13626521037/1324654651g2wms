
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
namespace WMS.ViewModel.PrintManagement.PrintMOVMs
{
    public partial class PrintMOSearcher : BaseSearcher
    {
        
        public List<string> PrintManagementPrintMOSTempSelected { get; set; }
        [Display(Name = "_Model._PrintMO._CustomerCode")]
        public string CustomerCode { get; set; }
        [Display(Name = "_Model._PrintMO._OrderWhName")]
        public string OrderWhName { get; set; }
        [Display(Name = "_Model._PrintMO._CustomerSPECS")]
        public string CustomerSPECS { get; set; }
        [Display(Name = "_Model._PrintMO._Item")]
        public Guid? ItemId { get; set; }
        [Display(Name = "料号")]
        public string ItemCode { get; set; }
        public List<ComboSelectListItem> AllItems { get; set; }
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
        public List<ComboSelectListItem> AllOrgs { get; set; }
        [Display(Name = "_Model._PrintMO._SyncItem")]
        public long? SyncItem { get; set; }
        [Display(Name = "_Model._PrintMO._SyncOrg")]
        public long? SyncOrg { get; set; }
        [Display(Name = "_Model._PrintMO._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._PrintMO._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._PrintMO._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._PrintMO._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}