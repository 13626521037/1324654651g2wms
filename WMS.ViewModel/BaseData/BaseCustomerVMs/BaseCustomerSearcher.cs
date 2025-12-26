
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseCustomerVMs
{
    public partial class BaseCustomerSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseCustomerSTempSelected { get; set; }
        [Display(Name = "_Model._BaseCustomer._EnglishShortName")]
        public string EnglishShortName { get; set; }
        [Display(Name = "_Model._BaseCustomer._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._BaseCustomer._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseCustomer._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._BaseCustomer._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }
        [Display(Name = "_Model._BaseCustomer._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseCustomer._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseCustomer._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._BaseCustomer._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._BaseCustomer._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._BaseCustomer._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}