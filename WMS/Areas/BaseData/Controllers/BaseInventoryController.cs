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
using WMS.ViewModel.BaseData.BaseInventoryVMs;
using NPOI.SS.Formula.Functions;
using WMS.Util;

namespace WMS.BaseData.Controllers
{
    public partial class BaseInventoryController : BaseController
    {

        [ActionDescription("_Page.BaseData.BaseInventory.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryVMs.BaseInventoryVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseInventory.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryVMs.BaseInventoryVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseInventory.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryVMs.BaseInventoryListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseInventory.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryVMs.BaseInventoryVM>(id);
            string code = $"{(int)vm.Entity.ItemSourceType}@{vm.Entity.ItemMaster.Code}@{vm.Entity.Qty.TrimZero()}@{vm.Entity.SerialNumber}";
            ViewBag.code = code;
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseInventory.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryVMs.BaseInventoryImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseInventory.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryVMs.BaseInventoryBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseInventory")]
        [HttpPost]
        public IActionResult SearchBaseInventory(WMS.ViewModel.BaseData.BaseInventoryVMs.BaseInventorySearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryVMs.BaseInventoryListVM>(passInit: true);
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
        public IActionResult BaseInventoryExportExcel(WMS.ViewModel.BaseData.BaseInventoryVMs.BaseInventoryListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("二维码")]
        public IActionResult QrCode(string id)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryVMs.BaseInventoryVM>(id);
            if (vm.Entity != null)
            {
                string code = $"{(int)vm.Entity.ItemSourceType}@{vm.Entity.ItemMaster.Code}@{vm.Entity.Qty.TrimZero()}@{vm.Entity.SerialNumber}";
                ViewBag.code = code;
                return PartialView();
            }
            else
            {
                ViewBag.code = "";
                return PartialView();
            }
        }

        [ActionDescription("库存拆分")]
        public IActionResult Split(string id)
        {
            var vm = Wtm.CreateVM<BaseInventoryVM>(id);

            return PartialView(vm);
        }

        [ActionDescription("库存拆分")]
        [HttpPost]
        public IActionResult DoSplit(BaseInventoryVM vm, IFormCollection nouse)
        {
            vm.Split();
            if (!ModelState.IsValid)
            {
                return PartialView(vm.FromView, vm);
            }
            return FFResult().CloseDialog().RefreshGrid();
        }

        [ActionDescription("打印")]
        public IActionResult Print(string ID)
        {
            BaseInventoryVM vm = Wtm.CreateVM<BaseInventoryVM>(ID);
            if (vm.Entity.IsAbandoned == true)
            {
                return FFResult().Message("该库存已经作废，无法打印！");
            }
            if (vm.Entity.Qty == 0)
            {
                return FFResult().Message("该库存数量为0，无法打印！");
            }
            vm.InitPrintModules();
            if (!ModelState.IsValid)
            {
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            return PartialView(vm);
        }

        [ActionDescription("打印")]
        [HttpPost]
        public IActionResult DoPrint(BaseInventoryVM vm, IFormCollection nouse)
        {
            ModelState.Clear();
            if (string.IsNullOrEmpty(vm.SelectedPrintModule))
            {
                vm.SetEntityById(vm.Entity.ID);
                vm.InitPrintModules();
                ModelState.AddModelError("", "请选择打印模板");
                return PartialView(vm.FromView, vm);
            }
            // 将数据写入打印服务的数据库（接口）
            string id = vm.CreatePrintData();
            if (!ModelState.IsValid)
            {
                vm.SetEntityById(vm.Entity.ID);
                vm.InitPrintModules();
                return PartialView(vm.FromView, vm);
            }
            // 按照返回的ID，访问打印服务的地址，进行页面展示
            string url = Wtm.ConfigInfo.AppSettings["PrintServer"] + "/_Admin/CPrintModule/PrintByDataId?dataId=" + id;
            string str = @"
                <div style='text-align:center;margin: 30px;'>
                    请在新窗口中查看。 如无新窗口弹出，请检查是否禁用了弹出窗口
                </div>
                <script>
                    window.open('" + url + @"', '_blank');
                    $(function(){layer.closeAll();});
                </script>";
            return Content(str, "text/html");
        }


        [ActionDescription("批量打印")]
        public IActionResult BatchPrint(string[] IDs)
        {
            var vm = Wtm.CreateVM<BaseInventoryBatchVM>(Ids: IDs);
            vm.InitPrintModules();
            if (!ModelState.IsValid)
            {
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            return PartialView(vm);
        }

        [ActionDescription("批量打印")]
        [HttpPost]
        public ActionResult DoBatchPrint(BaseInventoryBatchVM vm, IFormCollection nouse)
        {
            ModelState.Clear();
            if (string.IsNullOrEmpty(vm.SelectedPrintModule))
            {
                vm.InitPrintModules();
                ModelState.AddModelError("", "请选择打印模板");
                return PartialView(vm.FromView, vm);
            }
            // 将数据写入打印服务的数据库（接口）
            string id = vm.CreatePrintData();
            if (!ModelState.IsValid)
            {
                vm.InitPrintModules();
                return PartialView(vm.FromView, vm);
            }
            // 按照返回的ID，访问打印服务的地址，进行页面展示
            string url = Wtm.ConfigInfo.AppSettings["PrintServer"] + "/_Admin/CPrintModule/PrintByDataId?dataId=" + id;
            string str = @"
                <div style='text-align:center;margin: 30px;'>
                    请在新窗口中查看。 如无新窗口弹出，请检查是否禁用了弹出窗口
                </div>
                <script>
                    window.open('" + url + @"', '_blank');
                    $(function(){layer.closeAll();});
                </script>";
            return Content(str, "text/html");
        }
    }
}


