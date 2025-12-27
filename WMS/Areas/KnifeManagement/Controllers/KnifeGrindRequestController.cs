using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using System.Collections.Generic;
using WMS.Model;
using WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs;
using WMS.Model.KnifeManagement;

namespace WMS.KnifeManagement.Controllers
{
    public partial class KnifeGrindRequestController : BaseController
    {
        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindRequest.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs.KnifeGrindRequestVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindRequest.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs.KnifeGrindRequestVM>(id);
            vm.KnifeGrindRequestLineKnifeGrindRequestList1.Searcher.KnifeGrindRequestId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindRequest.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs.KnifeGrindRequestListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindRequest.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs.KnifeGrindRequestVM>(id);
            vm.Entity = DC.Set<KnifeGrindRequest>()
                .Include(x=>x.KnifeGrindRequestLine_KnifeGrindRequest)
                .FirstOrDefault(x=>x.ID.ToString()==id);
            string firstLineU9PODocNo = vm.Entity.KnifeGrindRequestLine_KnifeGrindRequest[0].U9PODocNo;
            ViewBag.code = firstLineU9PODocNo;
            vm.KnifeGrindRequestLineKnifeGrindRequestList2.Searcher.KnifeGrindRequestId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindRequest.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs.KnifeGrindRequestImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindRequest.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs.KnifeGrindRequestBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchKnifeGrindRequest")]
        [HttpPost]
        public IActionResult SearchKnifeGrindRequest(WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs.KnifeGrindRequestSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs.KnifeGrindRequestListVM>(passInit: true);
            if (ModelState.IsValid)
            {
                vm.Searcher = searcher;
                return Content(vm.GetJson(false));
            }
            else
            {
                return Content(vm.GetError());
            }
        }
        #endregion

        [ActionDescription("Sys.Export")]
        [HttpPost]
        public IActionResult KnifeGrindRequestExportExcel(WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs.KnifeGrindRequestListVM vm)
        {
            return vm.GetExportData();
        }

        #region 批量修磨出库
        [ActionDescription("批量修磨出库")]
        [HttpPost]
        public ActionResult BatchGrindOut(string[] IDs)
        {
            var vm = Wtm.CreateVM<KnifeGrindRequestBatchGrindOutVM>(Ids: IDs);
            return PartialView(vm);
        }

        [ActionDescription("执行批量修磨出库")]
        [HttpPost]
        public ActionResult DoBatchGrindOut(KnifeGrindRequestBatchGrindOutVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid)
            {
                return FFResult().Alert("参数验证失败");
            }
            
            var errorMsg = vm.DoBatchGrindOut();
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return FFResult().Alert(errorMsg);
            }
            
            return FFResult().CloseDialog().RefreshGrid().Alert("批量修磨出库成功");
        }
        #endregion
        
    }
}


