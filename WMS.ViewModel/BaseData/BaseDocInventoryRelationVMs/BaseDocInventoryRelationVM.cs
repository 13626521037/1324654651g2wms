using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseInventoryVMs;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs
{
    public partial class BaseDocInventoryRelationVM : BaseCRUDVM<BaseDocInventoryRelation>
    {
        
        public List<string> BaseDataBaseDocInventoryRelationFTempSelected { get; set; }

        public BaseDocInventoryRelationVM()
        {
            
            SetInclude(x => x.Inventory);

        }

        protected override void InitVM()
        {
            

        }

        public override DuplicatedInfo<BaseDocInventoryRelation> SetDuplicatedCheck()
        {
            return null;

        }

        public override async Task DoAddAsync()        
        {
            
            await base.DoAddAsync();

        }

        public override async Task DoEditAsync(bool updateAllFields = false)
        {
            
            await base.DoEditAsync();

        }

        public override async Task DoDeleteAsync()
        {
            await base.DoDeleteAsync();

        }
    }
}
