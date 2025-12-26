
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
using WMS.Model.BaseData;

namespace WMS.ViewModel.SalesManagement.SalesRMAVMs
{
    public partial class SalesRMABatchVM : BaseBatchVM<SalesRMA, SalesRMA_BatchEdit>
    {
        public SalesRMABatchVM()
        {
            ListVM = new SalesRMAListVM();
            LinkedVM = new SalesRMA_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SalesRMA_BatchEdit : BaseVM
    {

        
        public List<string> SalesManagementSalesRMABTempSelected { get; set; }
        [Display(Name = "_Model._SalesRMA._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._SalesRMA._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._SalesRMA._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._SalesRMA._ApproveDate")]
        public DateTime? ApproveDate { get; set; }
        [Display(Name = "_Model._SalesRMA._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._SalesRMA._Operators")]
        public string Operators { get; set; }
        [Display(Name = "_Model._SalesRMA._Customer")]
        public Guid? CustomerId { get; set; }
        [Display(Name = "_Model._SalesRMA._Status")]
        public SalesRMAStatusEnum? Status { get; set; }
        [Display(Name = "_Model._SalesRMA._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}