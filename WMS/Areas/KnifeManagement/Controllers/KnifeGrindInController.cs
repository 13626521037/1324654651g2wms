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
using WMS.ViewModel.KnifeManagement.KnifeGrindInVMs;

namespace WMS.KnifeManagement.Controllers
{
    public partial class KnifeGrindInController : BaseController
    {
        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindIn.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindInVMs.KnifeGrindInVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindIn.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindInVMs.KnifeGrindInVM>(id);
            vm.KnifeGrindInLineKnifeGrindInList1.Searcher.KnifeGrindInId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindIn.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindInVMs.KnifeGrindInListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindIn.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindInVMs.KnifeGrindInVM>(id);
            vm.KnifeGrindInLineKnifeGrindInList2.Searcher.KnifeGrindInId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindIn.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindInVMs.KnifeGrindInImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindIn.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindInVMs.KnifeGrindInBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchKnifeGrindIn")]
        [HttpPost]
        public IActionResult SearchKnifeGrindIn(WMS.ViewModel.KnifeManagement.KnifeGrindInVMs.KnifeGrindInSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindInVMs.KnifeGrindInListVM>(passInit: true);
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
        public IActionResult KnifeGrindInExportExcel(WMS.ViewModel.KnifeManagement.KnifeGrindInVMs.KnifeGrindInListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


