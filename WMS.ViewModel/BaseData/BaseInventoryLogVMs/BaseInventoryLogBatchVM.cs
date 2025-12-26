
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

namespace WMS.ViewModel.BaseData.BaseInventoryLogVMs
{
    public partial class BaseInventoryLogBatchVM : BaseBatchVM<BaseInventoryLog, BaseInventoryLog_BatchEdit>
    {
        public BaseInventoryLogBatchVM()
        {
            ListVM = new BaseInventoryLogListVM();
            LinkedVM = new BaseInventoryLog_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseInventoryLog_BatchEdit : BaseVM
    {

        
        public List<string> BaseDataBaseInventoryLogBTempSelected { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._OperationType")]
        public OperationTypeEnum? OperationType { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._SourceInventory")]
        public Guid? SourceInventoryId { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._TargetInventory")]
        public Guid? TargetInventoryId { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._Qty")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}