using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Util;
using Microsoft.EntityFrameworkCore;


namespace WMS.ViewModel.BaseData.BaseSysParaVMs
{
    public partial class BaseSysParaVM : BaseCRUDVM<BaseSysPara>
    {

        public BaseSysParaVM()
        {
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }

        /// <summary>
        /// 通过系统参数编码获取参数值
        /// </summary>
        /// <param name="paraCode">参数编码</param>
        /// <returns></returns>
        public string GetParaValue(string paraCode)
        {
            var entity = DC.Set<BaseSysPara>().AsNoTracking().Where(x => x.Code == paraCode).FirstOrDefault();
            if (entity != null)
            {
                return entity.Value;
            }
            return "";
        }

        /// <summary>
        /// 通过系统参数编码获取日期类型的参数值
        /// </summary>
        /// <param name="paraCode">参数编码</param>
        /// <returns></returns>
        public DateTime? GetParaDateTimeValue(string paraCode)
        {
            var entity = DC.Set<BaseSysPara>().AsNoTracking().Where(x => x.Code == paraCode).FirstOrDefault();
            if (entity != null)
            {
                return entity.Value.ToDateTime();
            }
            return null;
        }

        public int? GetParaIntValue(string paraCode)
        {
            var entity = DC.Set<BaseSysPara>().AsNoTracking().Where(x => x.Code == paraCode).FirstOrDefault();
            if (entity != null)
            {
                return entity.Value.ToInt();
            }
            return null;
        }

        /// <summary>
        /// 通过系统参数编码设置参数值
        /// </summary>
        /// <param name="code">系统参数编码</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public ReturnResult SetParaValue(string code, string value)
        {
            ReturnResult rr = new ReturnResult();
            var entity = DC.Set<BaseSysPara>().Where(x => x.Code == code).FirstOrDefault();
            if (entity != null)
            {
                entity.Value = value;
                DC.UpdateProperty(entity, x => x.Value);
                DC.SaveChanges();
                return rr;
            }
            else
            {
                rr.SetFail($"未找到参数：{code}");
                return rr;
            }
        }
    }
}
