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
using WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseSeibanCustomerRelationController : BaseController
    {

        [ActionDescription("_Page.BaseData.BaseSeibanCustomerRelation.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs.BaseSeibanCustomerRelationVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseSeibanCustomerRelation.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs.BaseSeibanCustomerRelationVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseSeibanCustomerRelation.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs.BaseSeibanCustomerRelationListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseSeibanCustomerRelation.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs.BaseSeibanCustomerRelationVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseSeibanCustomerRelation.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs.BaseSeibanCustomerRelationImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseSeibanCustomerRelation.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs.BaseSeibanCustomerRelationBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseSeibanCustomerRelation")]
        [HttpPost]
        public IActionResult SearchBaseSeibanCustomerRelation(WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs.BaseSeibanCustomerRelationSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs.BaseSeibanCustomerRelationListVM>(passInit: true);
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
        public IActionResult BaseSeibanCustomerRelationExportExcel(WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs.BaseSeibanCustomerRelationListVM vm)
        {
            return vm.GetExportData();
        }
    }
}


