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
using WMS.ViewModel.KnifeManagement.KnifeCheckInVMs;

namespace WMS.KnifeManagement.Controllers
{
    public partial class KnifeCheckInController : BaseController
    {
        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckIn.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckInVMs.KnifeCheckInVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckIn.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckInVMs.KnifeCheckInVM>(id);
            vm.KnifeCheckInLineKnifeCheckInList1.Searcher.KnifeCheckInId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckIn.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckInVMs.KnifeCheckInListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckIn.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckInVMs.KnifeCheckInVM>(id);
            vm.KnifeCheckInLineKnifeCheckInList2.Searcher.KnifeCheckInId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckIn.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckInVMs.KnifeCheckInImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckIn.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckInVMs.KnifeCheckInBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchKnifeCheckIn")]
        [HttpPost]
        public IActionResult SearchKnifeCheckIn(WMS.ViewModel.KnifeManagement.KnifeCheckInVMs.KnifeCheckInSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckInVMs.KnifeCheckInListVM>(passInit: true);
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
        public IActionResult KnifeCheckInExportExcel(WMS.ViewModel.KnifeManagement.KnifeCheckInVMs.KnifeCheckInListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


