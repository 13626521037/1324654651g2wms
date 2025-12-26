
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.ProductionManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs
{
    public partial class ProductionRcvRptBatchVM : BaseBatchVM<ProductionRcvRpt, ProductionRcvRpt_BatchEdit>
    {
        public ProductionRcvRptBatchVM()
        {
            ListVM = new ProductionRcvRptListVM();
            LinkedVM = new ProductionRcvRpt_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ProductionRcvRpt_BatchEdit : BaseVM
    {

        
        public List<string> ProductionManagementProductionRcvRptBTempSelected { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._ErpID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Wh")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._OrderWh")]
        public Guid? OrderWhId { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Status")]
        public ProductionRcvRptStatusEnum? Status { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}