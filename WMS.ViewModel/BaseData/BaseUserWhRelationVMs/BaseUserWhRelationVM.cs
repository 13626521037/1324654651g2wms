using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel._Admin.FrameworkUserVMs;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseUserWhRelationVMs
{
    public partial class BaseUserWhRelationVM : BaseCRUDVM<BaseUserWhRelation>
    {
        
        public List<string> BaseDataBaseUserWhRelationFTempSelected { get; set; }

        public BaseUserWhRelationVM()
        {
            
            SetInclude(x => x.User);
            SetInclude(x => x.Wh);

        }

        protected override void InitVM()
        {
            

        }

        public override DuplicatedInfo<BaseUserWhRelation> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.UserId), SimpleField(x => x.WhId));
            return rv;

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
