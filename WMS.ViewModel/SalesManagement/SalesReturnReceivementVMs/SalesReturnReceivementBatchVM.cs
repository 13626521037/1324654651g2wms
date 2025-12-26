
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

namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs
{
    public partial class SalesReturnReceivementBatchVM : BaseBatchVM<SalesReturnReceivement, SalesReturnReceivement_BatchEdit>
    {
        public SalesReturnReceivementBatchVM()
        {
            ListVM = new SalesReturnReceivementListVM();
            LinkedVM = new SalesReturnReceivement_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SalesReturnReceivement_BatchEdit : BaseVM
    {

        
        public List<string> SalesManagementSalesReturnReceivementBTempSelected { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._SubmitDate")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._Customer")]
        public Guid? CustomerId { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._Status")]
        public SalesReturnReceivementStatusEnum? Status { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}