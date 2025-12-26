
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

namespace WMS.ViewModel.BaseData.BaseCustomerVMs
{
    public partial class BaseCustomerBatchVM : BaseBatchVM<BaseCustomer, BaseCustomer_BatchEdit>
    {
        public BaseCustomerBatchVM()
        {
            ListVM = new BaseCustomerListVM();
            LinkedVM = new BaseCustomer_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseCustomer_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseCustomerBTempSelected { get; set; }
        [Display(Name = "_Model._BaseCustomer._ShortName")]
        public string ShortName { get; set; }
        [Display(Name = "_Model._BaseCustomer._EnglishShortName")]
        public string EnglishShortName { get; set; }
        [Display(Name = "_Model._BaseCustomer._Organization")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._BaseCustomer._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseCustomer._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}