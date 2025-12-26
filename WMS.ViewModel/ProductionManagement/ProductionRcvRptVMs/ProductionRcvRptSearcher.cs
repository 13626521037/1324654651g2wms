
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;
using WMS.Model.BaseData;
namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs
{
    public partial class ProductionRcvRptSearcher : BaseSearcher
    {
        
        public List<string> ProductionManagementProductionRcvRptSTempSelected { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Wh")]
        public Guid? WhId { get; set; }
        public List<ComboSelectListItem> AllWhs { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._OrderWh")]
        public Guid? OrderWhId { get; set; }
        public List<ComboSelectListItem> AllOrderWhs { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Status")]
        public ProductionRcvRptStatusEnum? Status { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._CreateBy")]
        public string CreateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}