using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseWhAreaVMs;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseWhLocationVMs
{
    public partial class BaseWhLocationVM : BaseCRUDVM<BaseWhLocation>
    {
        
        public List<string> BaseDataBaseWhLocationFTempSelected { get; set; }

        /// <summary>
        /// 多级联动使用
        /// </summary>
        public Guid WareHouseId { get; set; }

        public BaseWhLocationVM()
        {
            SetInclude(x => x.WhArea.WareHouse);

        }

        protected override void InitVM()
        {
            WareHouseId = Entity?.WhArea?.WareHouseId ?? Guid.Empty;
        }

        public override DuplicatedInfo<BaseWhLocation> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.Code));
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
