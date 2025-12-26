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
using WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseDocInventoryRelationController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseDocInventoryRelation.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs.BaseDocInventoryRelationVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseDocInventoryRelation.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs.BaseDocInventoryRelationVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseDocInventoryRelation.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs.BaseDocInventoryRelationListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseDocInventoryRelation.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs.BaseDocInventoryRelationVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseDocInventoryRelation.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs.BaseDocInventoryRelationImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseDocInventoryRelation.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs.BaseDocInventoryRelationBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseDocInventoryRelation")]
        [HttpPost]
        public IActionResult SearchBaseDocInventoryRelation(WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs.BaseDocInventoryRelationSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs.BaseDocInventoryRelationListVM>(passInit: true);
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
        public IActionResult BaseDocInventoryRelationExportExcel(WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs.BaseDocInventoryRelationListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


