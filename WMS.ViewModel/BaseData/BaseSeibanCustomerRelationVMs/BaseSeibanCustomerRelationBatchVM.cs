
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs
{
    public partial class BaseSeibanCustomerRelationBatchVM : BaseBatchVM<BaseSeibanCustomerRelation, BaseSeibanCustomerRelation_BatchEdit>
    {
        public BaseSeibanCustomerRelationBatchVM()
        {
            ListVM = new BaseSeibanCustomerRelationListVM();
            LinkedVM = new BaseSeibanCustomerRelation_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseSeibanCustomerRelation_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseSeibanCustomerRelationBTempSelected { get; set; }
        [Display(Name = "_Model._BaseSeibanCustomerRelation._Customer")]
        public Guid? CustomerId { get; set; }
        [Display(Name = "_Model._BaseSeibanCustomerRelation._RandomCode")]
        public string RandomCode { get; set; }
        [Display(Name = "_Model._BaseSeibanCustomerRelation._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}