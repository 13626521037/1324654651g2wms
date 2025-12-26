using Elsa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WMS.Model._Admin;

namespace WMS.ViewModel
{
    public static class FFResultExtensions
    {
        /// <summary>
        /// 获取Html格式的ModelState所有错误信息
        /// </summary>
        /// <param name="fResult"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static FResult AlertAllErrors(this FResult fResult, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            return fResult.Alert(string.Join("<br/>", modelState.GetErrorJson().Form.Select(x => x.Value).ToList()), "错误信息");
        }
    }
}
