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
using WMS.ViewModel.KnifeManagement.KnifeTransferInVMs;

namespace WMS.KnifeManagement.Controllers
{
    public partial class KnifeTransferInController : BaseController
    {
        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferIn.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferInVMs.KnifeTransferInVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferIn.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferInVMs.KnifeTransferInVM>(id);
            vm.KnifeTransferInLineKnifeTransferInList1.Searcher.KnifeTransferInId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferIn.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferInVMs.KnifeTransferInListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferIn.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferInVMs.KnifeTransferInVM>(id);
            vm.KnifeTransferInLineKnifeTransferInList2.Searcher.KnifeTransferInId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferIn.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferInVMs.KnifeTransferInImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeTransferIn.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferInVMs.KnifeTransferInBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchKnifeTransferIn")]
        [HttpPost]
        public IActionResult SearchKnifeTransferIn(WMS.ViewModel.KnifeManagement.KnifeTransferInVMs.KnifeTransferInSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeTransferInVMs.KnifeTransferInListVM>(passInit: true);
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
        public IActionResult KnifeTransferInExportExcel(WMS.ViewModel.KnifeManagement.KnifeTransferInVMs.KnifeTransferInListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


