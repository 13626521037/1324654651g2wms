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
using WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs;

namespace WMS.KnifeManagement.Controllers
{
    public partial class KnifeTransferOutController : BaseController
    {
        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferOut.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs.KnifeTransferOutVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferOut.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs.KnifeTransferOutVM>(id);
            vm.KnifeTransferOutLineKnifeTransferOutList1.Searcher.KnifeTransferOutId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferOut.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs.KnifeTransferOutListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferOut.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs.KnifeTransferOutVM>(id);
            vm.KnifeTransferOutLineKnifeTransferOutList2.Searcher.KnifeTransferOutId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferOut.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs.KnifeTransferOutImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferOut.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs.KnifeTransferOutBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchKnifeTransferOut")]
        [HttpPost]
        public IActionResult SearchKnifeTransferOut(WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs.KnifeTransferOutSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs.KnifeTransferOutListVM>(passInit: true);
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
        public IActionResult KnifeTransferOutExportExcel(WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs.KnifeTransferOutListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


