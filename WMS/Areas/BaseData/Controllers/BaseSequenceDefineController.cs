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
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.ViewModel;

namespace WMS.BaseData.Controllers
{
    public partial class BaseSequenceDefineController : BaseController
    {

        [ActionDescription("_Page.BaseData.BaseSequenceDefine.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceDefineVMs.BaseSequenceDefineVM>();
            vm.Entity.IsEffective = EffectiveEnum.Effective;
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseSequenceDefine.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceDefineVMs.BaseSequenceDefineVM>(id);
            vm.BaseSequenceDefineLineSequenceDefineList1.Searcher.SequenceDefineId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseSequenceDefine.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceDefineVMs.BaseSequenceDefineListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseSequenceDefine.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceDefineVMs.BaseSequenceDefineVM>(id);
            vm.BaseSequenceDefineLineSequenceDefineList2.Searcher.SequenceDefineId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseSequenceDefine.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceDefineVMs.BaseSequenceDefineImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseSequenceDefine.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceDefineVMs.BaseSequenceDefineBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseSequenceDefine")]
        [HttpPost]
        public IActionResult SearchBaseSequenceDefine(WMS.ViewModel.BaseData.BaseSequenceDefineVMs.BaseSequenceDefineSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSequenceDefineVMs.BaseSequenceDefineListVM>(passInit: true);
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
        public IActionResult BaseSequenceDefineExportExcel(WMS.ViewModel.BaseData.BaseSequenceDefineVMs.BaseSequenceDefineListVM vm)
        {
            return vm.GetExportData();
        }

        // 获取下一个序列号
        [ActionDescription("测试获取一个序列号")]
        public ActionResult GetSequence(Guid id)
        {
            var vm = Wtm.CreateVM<BaseSequenceDefineVM>();
            string data = vm.SetProperty("ItemCategory", "100001").GetSequence(id);
            if (ModelState.IsValid)
            {
                return FFResult().Alert(data);
            }
            else
            {
                return FFResult().AlertAllErrors(ModelState);
            }
        }
    }
}


