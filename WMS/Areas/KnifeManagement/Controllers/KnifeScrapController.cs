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
using WMS.ViewModel.KnifeManagement.KnifeScrapVMs;
using System.Threading.Tasks;

namespace WMS.KnifeManagement.Controllers
{
    public partial class KnifeScrapController : BaseController
    {
        
        [ActionDescription("_Page.KnifeManagement.KnifeScrap.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeScrapVMs.KnifeScrapVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeScrap.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeScrapVMs.KnifeScrapVM>(id);
            vm.KnifeScrapLineKnifeScrapList1.Searcher.KnifeScrapId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeScrap.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeScrapVMs.KnifeScrapListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeScrap.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeScrapVMs.KnifeScrapVM>(id);
            vm.KnifeScrapLineKnifeScrapList2.Searcher.KnifeScrapId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeScrap.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeScrapVMs.KnifeScrapImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeScrap.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeScrapVMs.KnifeScrapBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchKnifeScrap")]
        [HttpPost]
        public IActionResult SearchKnifeScrap(WMS.ViewModel.KnifeManagement.KnifeScrapVMs.KnifeScrapSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeScrapVMs.KnifeScrapListVM>(passInit: true);
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
        public IActionResult KnifeScrapExportExcel(WMS.ViewModel.KnifeManagement.KnifeScrapVMs.KnifeScrapListVM vm)
        {
            return vm.GetExportData();
        }



        [ActionDescription("报废单关闭")]
        public IActionResult  KnifeScrapClose(string id)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeScrapVMs.KnifeScrapVM>(id);
            var tran = DC.Database.BeginTransaction();
            try
            {


                vm.DoClose(id);
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
                }
                DC.SaveChanges();
                tran.Commit();
                return FFResult().RefreshGrid();
            }
            catch
            {
                tran.Rollback();
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
            }
        }

    }
}


