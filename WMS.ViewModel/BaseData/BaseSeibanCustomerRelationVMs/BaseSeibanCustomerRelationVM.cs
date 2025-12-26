using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseCustomerVMs;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs
{
    public partial class BaseSeibanCustomerRelationVM : BaseCRUDVM<BaseSeibanCustomerRelation>
    {

        public List<string> BaseDataBaseSeibanCustomerRelationFTempSelected { get; set; }

        public BaseSeibanCustomerRelationVM()
        {

            SetInclude(x => x.Customer);

        }

        protected override void InitVM()
        {


        }

        public override DuplicatedInfo<BaseSeibanCustomerRelation> SetDuplicatedCheck()
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

        /// <summary>
        /// 获取番号信息
        /// </summary>
        /// <param name="seiban"></param>
        /// <returns></returns>
        public BaseSeibanCustomerRelation GetSeibanInfo(string seiban)
        {
            var data = DC.Set<BaseSeibanCustomerRelation>().AsNoTracking().Where(x => x.Code == seiban).FirstOrDefault();
            if (data != null)
            {
                return data;
            }
            data = SyncByCode(seiban);

            return data;
        }

        /// <summary>
        /// 通过番号编码同步U9数据
        /// </summary>
        /// <param name="seiban"></param>
        /// <returns></returns>
        public BaseSeibanCustomerRelation SyncByCode(string seiban)
        {
            // 尝试同步数据
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<List<BaseSeibanCustomerRelation>> u9Return = apiHelper.GetSeibans(seiban, null);
            if (u9Return.Success)
            {
                // 写入数据库
                if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                {
                    foreach (var item in u9Return.Entity)
                    {
                        var exist = DC.Set<BaseSeibanCustomerRelation>().AsNoTracking().Where(x => x.Code == item.Code).FirstOrDefault();
                        if (exist == null)
                        {
                            if (string.IsNullOrEmpty(item.Name))    // Name存储的是客户编码
                            {
                                continue;
                            }
                            item.CustomerId = DC.Set<BaseCustomer>().AsNoTracking().Where(x => x.Code == item.Name).Select(x => x.ID).FirstOrDefault();
                            if (item.CustomerId == null)
                            {
                                MSD.AddModelError("", $"客户编码{item.Name}不存在，请先尝试同步客户信息");
                                return null;
                            }
                            Entity = item;
                            DoAdd();
                        }
                    }
                    var data = DC.Set<BaseSeibanCustomerRelation>().AsNoTracking().Where(x => x.Code == seiban).FirstOrDefault();
                    return data;
                }
            }

            return null;
        }
    }
}
