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
using WMS.ViewModel.BaseData.BaseSequenceRecordsVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseSequenceRecordsController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseSequenceRecords.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceRecordsVMs.BaseSequenceRecordsVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSequenceRecords.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceRecordsVMs.BaseSequenceRecordsVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSequenceRecords.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceRecordsVMs.BaseSequenceRecordsListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSequenceRecords.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceRecordsVMs.BaseSequenceRecordsVM>(id);
            vm.BaseSequenceRecordsDetailBaseSequenceRecordsList.Searcher.BaseSequenceRecordsId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSequenceRecords.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceRecordsVMs.BaseSequenceRecordsImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSequenceRecords.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceRecordsVMs.BaseSequenceRecordsBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseSequenceRecords")]
        [HttpPost]
        public IActionResult SearchBaseSequenceRecords(WMS.ViewModel.BaseData.BaseSequenceRecordsVMs.BaseSequenceRecordsSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceRecordsVMs.BaseSequenceRecordsListVM>(passInit: true);
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
        public IActionResult BaseSequenceRecordsExportExcel(WMS.ViewModel.BaseData.BaseSequenceRecordsVMs.BaseSequenceRecordsListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


