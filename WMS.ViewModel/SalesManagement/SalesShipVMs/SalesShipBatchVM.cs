
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

namespace WMS.ViewModel.SalesManagement.SalesShipVMs
{
    public partial class SalesShipBatchVM : BaseBatchVM<SalesShip, SalesShip_BatchEdit>
    {
        public SalesShipBatchVM()
        {
            ListVM = new SalesShipListVM();
            LinkedVM = new SalesShip_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SalesShip_BatchEdit : BaseVM
    {

        
        public List<string> SalesManagementSalesShipBTempSelected { get; set; }
        [Display(Name = "_Model._SalesShip._CreatePerson")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._SalesShip._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._SalesShip._BusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._SalesShip._SubmitDate")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._SalesShip._DocType")]
        public string DocType { get; set; }
        [Display(Name = "_Model._SalesShip._Operators")]
        public string Operators { get; set; }
        [Display(Name = "_Model._SalesShip._Customer")]
        public Guid? CustomerId { get; set; }
        [Display(Name = "_Model._SalesShip._Status")]
        public SalesShipStatusEnum? Status { get; set; }
        [Display(Name = "_Model._SalesShip._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}