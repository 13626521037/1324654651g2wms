
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
namespace WMS.ViewModel.SalesManagement.SalesRMAVMs
{
    public partial class SalesRMASearcher : BaseSearcher
    {
        
        public List<string> SalesManagementSalesRMASTempSelected { get; set; }
        [Display(Name = "_Model._SalesRMA._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._SalesRMA._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._SalesRMA._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._SalesRMA._ApproveDate")]
        public DateRange ApproveDate { get; set; }
        [Display(Name = "_Model._SalesRMA._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._SalesRMA._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._SalesRMA._Operators")]
        public string Operators { get; set; }
        [Display(Name = "_Model._SalesRMA._Customer")]
        public Guid? CustomerId { get; set; }
        public List<ComboSelectListItem> AllCustomers { get; set; }
        [Display(Name = "_Model._SalesRMA._Customer")]
        public string Customer { get; set; }
        [Display(Name = "_Model._SalesRMA._Status")]
        public SalesRMAStatusEnum? Status { get; set; }
        [Display(Name = "_Model._SalesRMA._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._SalesRMA._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }
        [Display(Name = "_Model._SalesRMA._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._SalesRMA._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._SalesRMA._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._SalesRMA._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}