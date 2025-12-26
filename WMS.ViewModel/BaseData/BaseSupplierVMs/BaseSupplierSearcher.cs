
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseSupplierVMs
{
    public partial class BaseSupplierSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseSupplierSTempSelected { get; set; }
        [Display(Name = "_Model._BaseSupplier._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseSupplier._Name")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseSupplier._ShortName")]
        public string ShortName { get; set; }
        [Display(Name = "_Model._BaseSupplier._Organization")]
        public Guid? OrganizationId { get; set; }
        public List<ComboSelectListItem> AllOrganizations { get; set; }
        [Display(Name = "_Model._BaseSupplier._IsEffective")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseSupplier._SourceSystemId")]
        public string SourceSystemId { get; set; }
        [Display(Name = "_Model._BaseSupplier._LastUpdateTime")]
        public DateRange LastUpdateTime { get; set; }
        [Display(Name = "_Model._BaseSupplier._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._BaseSupplier._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._BaseSupplier._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._BaseSupplier._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}