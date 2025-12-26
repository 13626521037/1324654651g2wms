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
using WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs;

namespace WMS.KnifeManagement.Controllers
{
    public partial class KnifeGrindOutController : BaseController
    {
        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindOut.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs.KnifeGrindOutVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindOut.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs.KnifeGrindOutVM>(id);
            vm.KnifeGrindOutLineKnifeGrindOutList1.Searcher.KnifeGrindOutId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindOut.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs.KnifeGrindOutListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindOut.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs.KnifeGrindOutVM>(id);
            vm.KnifeGrindOutLineKnifeGrindOutList2.Searcher.KnifeGrindOutId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindOut.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs.KnifeGrindOutImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeGrindOut.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs.KnifeGrindOutBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchKnifeGrindOut")]
        [HttpPost]
        public IActionResult SearchKnifeGrindOut(WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs.KnifeGrindOutSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs.KnifeGrindOutListVM>(passInit: true);
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
        public IActionResult KnifeGrindOutExportExcel(WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs.KnifeGrindOutListVM vm)
        {
            return vm.GetExportData();
        }

        /*[ActionDescription("批量生单")]
        [HttpPost("BatchCreate")]
        public ActionResult BatchCreate()
        {
            //访问U9接口获取list 然后传给前端展示
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs.KnifeGrindOutVM>();
            return PartialView(vm);
        }*/
    }
}


