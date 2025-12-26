using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.BaseData.BaseInventoryVMs;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Util;
using NPOI.SS.Formula.Functions;


namespace WMS.Controllers
{
    [Area("BaseData")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("库存信息API")]
    [ApiController]
    [Route("api/BaseInventory")]
    public partial class BaseInventoryApiController : BaseApiController
    {
        #region 自动生成代码
        //[ActionDescription("Sys.Search")]
        //[HttpPost("Search")]
        //public IActionResult Search(BaseInventoryApiSearcher searcher)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var vm = Wtm.CreateVM<BaseInventoryApiListVM>(passInit: true);
        //        vm.Searcher = searcher;
        //        return Content(vm.GetJson());
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState.GetErrorJson());
        //    }
        //}

        //[ActionDescription("Sys.Get")]
        //[HttpGet("{id}")]
        //public BaseInventoryApiVM Get(string id)
        //{
        //    var vm = Wtm.CreateVM<BaseInventoryApiVM>(id);
        //    return vm;
        //}

        //[ActionDescription("Sys.Create")]
        //[HttpPost("Add")]
        //public IActionResult Add(BaseInventoryApiVM vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState.GetErrorJson());
        //    }
        //    else
        //    {
        //        vm.DoAdd();
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState.GetErrorJson());
        //        }
        //        else
        //        {
        //            return Ok(vm.Entity);
        //        }
        //    }

        //}

        //[ActionDescription("Sys.Edit")]
        //[HttpPut("Edit")]
        //public IActionResult Edit(BaseInventoryApiVM vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState.GetErrorJson());
        //    }
        //    else
        //    {
        //        vm.DoEdit(false);
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState.GetErrorJson());
        //        }
        //        else
        //        {
        //            return Ok(vm.Entity);
        //        }
        //    }
        //}

        //[HttpPost("BatchDelete")]
        //[ActionDescription("Sys.Delete")]
        //public IActionResult BatchDelete(string[] ids)
        //{
        //    var vm = Wtm.CreateVM<BaseInventoryApiBatchVM>();
        //    if (ids != null && ids.Count() > 0)
        //    {
        //        vm.Ids = ids;
        //    }
        //    else
        //    {
        //        return Ok();
        //    }
        //    if (!ModelState.IsValid || !vm.DoBatchDelete())
        //    {
        //        return BadRequest(ModelState.GetErrorJson());
        //    }
        //    else
        //    {
        //        return Ok(ids.Count());
        //    }
        //}


        //[ActionDescription("Sys.Export")]
        //[HttpPost("ExportExcel")]
        //public IActionResult ExportExcel(BaseInventoryApiSearcher searcher)
        //{
        //    var vm = Wtm.CreateVM<BaseInventoryApiListVM>();
        //    vm.Searcher = searcher;
        //    vm.SearcherMode = ListVMSearchModeEnum.Export;
        //    return vm.GetExportData();
        //}

        //[ActionDescription("Sys.CheckExport")]
        //[HttpPost("ExportExcelByIds")]
        //public IActionResult ExportExcelByIds(string[] ids)
        //{
        //    var vm = Wtm.CreateVM<BaseInventoryApiListVM>();
        //    if (ids != null && ids.Count() > 0)
        //    {
        //        vm.Ids = new List<string>(ids);
        //        vm.SearcherMode = ListVMSearchModeEnum.CheckExport;
        //    }
        //    return vm.GetExportData();
        //}

        //[ActionDescription("Sys.DownloadTemplate")]
        //[HttpGet("GetExcelTemplate")]
        //public IActionResult GetExcelTemplate()
        //{
        //    var vm = Wtm.CreateVM<BaseInventoryApiImportVM>();
        //    var qs = new Dictionary<string, string>();
        //    foreach (var item in Request.Query.Keys)
        //    {
        //        qs.Add(item, Request.Query[item]);
        //    }
        //    vm.SetParms(qs);
        //    var data = vm.GenerateTemplate(out string fileName);
        //    return File(data, "application/vnd.ms-excel", fileName);
        //}

        //[ActionDescription("Sys.Import")]
        //[HttpPost("Import")]
        //public ActionResult Import(BaseInventoryApiImportVM vm)
        //{
        //    if (vm != null && (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData()))
        //    {
        //        return BadRequest(vm.GetErrorJson());
        //    }
        //    else
        //    {
        //        return Ok(vm?.EntityList?.Count ?? 0);
        //    }
        //}


        //[HttpGet("GetBaseItemMasters")]
        //public ActionResult GetBaseItemMasters()
        //{
        //    return Ok(DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, x => x.Name));
        //}

        //[HttpGet("GetBaseWhLocations")]
        //public ActionResult GetBaseWhLocations()
        //{
        //    return Ok(DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, x => x.Name));
        //}

        #endregion

        /// <summary>
        /// 根据序列号获取库存信息
        /// </summary>
        /// <param name="sn">序列号</param>
        /// <returns></returns>
        [HttpPost("GetDataBySn")]
        [ActionDescription("根据序列号获取库存信息")]
        public ActionResult GetDataBySn(string sn, bool includeBarCode = false)
        {
            ReturnResult<BaseInventoryReturn> rr = new ReturnResult<BaseInventoryReturn>();
            if (string.IsNullOrEmpty(sn))
            {
                rr.SetFail("条码不能为空");
                return Ok(rr);
            }
            var vm = Wtm.CreateVM<BaseInventoryApiVM>();
            rr.Entity = vm.GetDataBySn(sn, includeBarCode);
            if (rr.Entity == null)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        /// <summary>
        /// 创建一条库存信息（开发测试专用，权限勿赋值给用户使用）
        /// </summary>
        /// <param name="data">库存信息数据</param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ActionDescription("创建一条库存信息（测试用）")]
        public ActionResult Create(string itemCode, string locationCode, decimal qty, string seiban, bool isAbandoned, int itemSourceType, int frozenStatus)
        {
            ReturnResult rr = new ReturnResult();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            BaseInventoryApiVM vm = Wtm.CreateVM<BaseInventoryApiVM>();
            vm.Create(itemCode, locationCode, qty, seiban, isAbandoned, itemSourceType, frozenStatus);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            return Ok(rr);
        }

        /// <summary>
        /// 获取库存或托盘信息通用接口
        /// </summary>
        /// <param name="code">条码或托盘码</param>
        /// <param name="type">0：混合（全部），1：库存条码（全部），2：库存条码（不含托盘内的库存条码），3：库存条码（只看托盘内的库存条码），4：托盘码，5：混合（不含托盘内的库存条码），6：混合（只看托盘内的库存条码）</param>
        /// <param name="locationType">库位类型（-1：不限，0：正常库位）</param>
        /// <param name="takingType">盘点类型（-1：不限，0：未盘点锁定， 1：已盘点锁定）</param>
        /// <param name="isFreeze">是否冻结（false：不冻结，true：冻结）</param>
        /// <returns></returns>
        [HttpPost("GetInvOrPalletData")]
        [ActionDescription("获取库存或托盘信息通用接口")]
        public ActionResult GetInvOrPalletData(string code, int type, int locationType, int takingType, bool isFreeze = false)
        {
            ReturnResult<InvOrPalletReturn> rr = new ReturnResult<InvOrPalletReturn>();
            if (string.IsNullOrEmpty(code))
            {
                rr.SetFail("参数code不能为空");
                return Ok(rr);
            }
            var vm = Wtm.CreateVM<BaseInventoryApiVM>();
            rr.Entity = vm.GetInvOrPalletData(code, type, locationType, takingType, isFreeze);
            if (rr.Entity == null)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
