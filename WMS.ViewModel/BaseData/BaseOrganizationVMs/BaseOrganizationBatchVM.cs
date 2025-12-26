
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

namespace WMS.ViewModel.BaseData.BaseOrganizationVMs
{
    public partial class BaseOrganizationBatchVM : BaseBatchVM<BaseOrganization, BaseOrganization_BatchEdit>
    {
        public BaseOrganizationBatchVM()
        {
            ListVM = new BaseOrganizationListVM();
            LinkedVM = new BaseOrganization_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseOrganization_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseOrganizationBTempSelected { get; set; }
        [Display(Name = "_Model._BaseOrganization._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseOrganization._IsProduction")]
        public bool? IsProduction { get; set; }
        [Display(Name = "_Model._BaseOrganization._IsSale")]
        public bool? IsSale { get; set; }
        [Display(Name = "_Model._BaseOrganization._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseOrganization._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}