
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;
using WMS.Model.BaseData;
namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs
{
    public partial class SalesReturnReceivementSearcher : BaseSearcher
    {
        
        public List<string> SalesManagementSalesReturnReceivementSTempSelected { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._SubmitDate")]
        public DateRange SubmitDate { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._Customer")]
        public Guid? CustomerId { get; set; }
        public List<ComboSelectListItem> AllCustomers { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._Customer")]
        public string Customer { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._Status")]
        public SalesReturnReceivementStatusEnum? Status { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}