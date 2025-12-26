
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

namespace WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs
{
    public partial class BaseDocInventoryRelationBatchVM : BaseBatchVM<BaseDocInventoryRelation, BaseDocInventoryRelation_BatchEdit>
    {
        public BaseDocInventoryRelationBatchVM()
        {
            ListVM = new BaseDocInventoryRelationListVM();
            LinkedVM = new BaseDocInventoryRelation_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseDocInventoryRelation_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseDocInventoryRelationBTempSelected { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._DocType")]
        public DocTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._Inventory")]
        public Guid? InventoryId { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._BusinessId")]
        public Guid? BusinessId { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._Qty")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}