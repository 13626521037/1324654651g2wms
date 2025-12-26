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
using WMS.ViewModel.BaseData.BaseUserWhRelationVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseUserWhRelationController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseUserWhRelation.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUserWhRelationVMs.BaseUserWhRelationVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseUserWhRelation.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUserWhRelationVMs.BaseUserWhRelationVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseUserWhRelation.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUserWhRelationVMs.BaseUserWhRelationListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseUserWhRelation.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUserWhRelationVMs.BaseUserWhRelationVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseUserWhRelation.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUserWhRelationVMs.BaseUserWhRelationImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseUserWhRelation.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUserWhRelationVMs.BaseUserWhRelationBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseUserWhRelation")]
        [HttpPost]
        public IActionResult SearchBaseUserWhRelation(WMS.ViewModel.BaseData.BaseUserWhRelationVMs.BaseUserWhRelationSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUserWhRelationVMs.BaseUserWhRelationListVM>(passInit: true);
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
        public IActionResult BaseUserWhRelationExportExcel(WMS.ViewModel.BaseData.BaseUserWhRelationVMs.BaseUserWhRelationListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


