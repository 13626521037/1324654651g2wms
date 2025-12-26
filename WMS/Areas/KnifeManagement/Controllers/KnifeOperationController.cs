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
using WMS.ViewModel.KnifeManagement.KnifeOperationVMs;

namespace WMS.KnifeManagement.Controllers
{
    public partial class KnifeOperationController : BaseController
    {
        
        [ActionDescription("_Page.KnifeManagement.KnifeOperation.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeOperationVMs.KnifeOperationListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeOperation.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeOperationVMs.KnifeOperationVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeOperation.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeOperationVMs.KnifeOperationImportVM>();
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchKnifeOperation")]
        [HttpPost]
        public IActionResult SearchKnifeOperation(WMS.ViewModel.KnifeManagement.KnifeOperationVMs.KnifeOperationSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeOperationVMs.KnifeOperationListVM>(passInit: true);
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
        public IActionResult KnifeOperationExportExcel(WMS.ViewModel.KnifeManagement.KnifeOperationVMs.KnifeOperationListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


