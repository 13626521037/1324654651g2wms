using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using WMS.Model.BaseData;
using WMS.Model;
using Microsoft.Data.SqlClient;
namespace WMS.ViewModel.BaseData.BaseWhAreaVMs
{
    public partial class BaseWhAreaVM : BaseCRUDVM<BaseWhArea>
    {
        
        public List<string> BaseDataBaseWhAreaFTempSelected { get; set; }

        public BaseWhAreaVM()
        {
            
            SetInclude(x => x.WareHouse);

        }

        protected override void InitVM()
        {
            

        }

        public override DuplicatedInfo<BaseWhArea> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.Code));
            return rv;
        }

        public override async Task DoAddAsync()        
        {
            try
            {
                await base.DoAddAsync();
            }
            catch(Exception ex)
            {
                if(ex.InnerException != null && ex.InnerException is SqlException && ex.InnerException.Message.Contains("IX_BaseWhArea_Code"))
                {
                    MSD.AddModelError("Error", "当前存储地点已经存在相同编码的库区");
                }
                else
                {
                    MSD.AddModelError("Error", ex.Message);
                }
            }
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
