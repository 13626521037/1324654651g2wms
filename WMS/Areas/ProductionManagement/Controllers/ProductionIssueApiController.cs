using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.ProductionManagement.ProductionIssueVMs;
using WMS.Model.ProductionManagement;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Util;
using Newtonsoft.Json;
using WMS.Model.InventoryManagement;


namespace WMS.Controllers
{
    [Area("ProductionManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("生产领料单")]
    [ApiController]
    [Route("api/ProductionIssue")]
    public partial class ProductionIssueApiController : BaseApiController
    {
        [ActionDescription("获取单据列表")]
        [HttpPost("GetList")]
        public IActionResult GetList(int type)
        {
            ReturnResult<List<ProductionIssueReturn>> rr = new ReturnResult<List<ProductionIssueReturn>>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            ProductionIssueApiVM vm = Wtm.CreateVM<ProductionIssueApiVM>();
            rr.Entity = vm.GetList(type);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [ActionDescription("创建单据（ERP调用）")]
        [HttpPost("Create")]
        public IActionResult Create(ProductionIssue entity)
        {
            ReturnResult rr = new ReturnResult();
            if (entity == null)
            {
                rr.SetFail("参数不能为空");
                return Ok(rr);
            }
            ProductionIssueApiVM vm = Wtm.CreateVM<ProductionIssueApiVM>();
            vm.Create(entity);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("单据是否存在")]
        [HttpPost("IsDocExist")]
        public IActionResult IsDocExist(string id)
        {
            ReturnResult rr = new ReturnResult();
            ProductionIssueApiVM vm = Wtm.CreateVM<ProductionIssueApiVM>();
            if (!vm.IsDocExist(id))
            {
                rr.SetFail("Doc not exist");
            }
            else
            {
                rr.SetSuccess(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("扫码获取单据")]
        [HttpPost("GetDoc")]
        public IActionResult GetDoc(string docNo)
        {
            ReturnResult<ProductionIssueReturn> rr = new ReturnResult<ProductionIssueReturn>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            if (string.IsNullOrEmpty(docNo))
            {
                rr.SetFail("单据号不能为空");
                return Ok(rr);
            }
            ProductionIssueApiVM vm = Wtm.CreateVM<ProductionIssueApiVM>();
            rr.Entity = vm.GetDoc(docNo);
            if (rr.Entity != null)
            {
                vm.GetSuggestLocs(rr.Entity);   // 获取推荐库位
                vm.GetOffDetails(rr.Entity);   // 获取下架明细
            }
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            var ret = Ok(rr);
            return ret;
        }

        [ActionDescription("扫码获取单据（审核）")]
        [HttpPost("GetDocForApprove")]
        public IActionResult GetDocForApprove(string docNo)
        {
            ReturnResult<ProductionIssueReturn> rr = new ReturnResult<ProductionIssueReturn>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            if (string.IsNullOrEmpty(docNo))
            {
                rr.SetFail("单据号不能为空");
                return Ok(rr);
            }
            ProductionIssueApiVM vm = Wtm.CreateVM<ProductionIssueApiVM>();
            rr.Entity = vm.GetDocForApprove(docNo);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("[PDA] 下架操作")]
        [HttpPost("SaveOffDetails")]
        public IActionResult SaveOffDetails(ProductionIssueSaveOffPara data, bool autoSplit)
        {
            ReturnResult<List<InventorySplitSaveReturn>> rr = new ReturnResult<List<InventorySplitSaveReturn>>();
            ProductionIssueApiVM vm = Wtm.CreateVM<ProductionIssueApiVM>();
            vm.SaveOffDetails(data, autoSplit);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            if (autoSplit)
            {
                rr.Entity = vm.AutoSplitResult;
            }
            return Ok(rr);
        }

        [ActionDescription("[PDA] 审核单据")]
        [HttpPost("Approve")]
        public IActionResult Approve(Guid? id)
        {
            ReturnResult rr = new ReturnResult();
            ProductionIssueApiVM vm = Wtm.CreateVM<ProductionIssueApiVM>();
            vm.Approve(id);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
