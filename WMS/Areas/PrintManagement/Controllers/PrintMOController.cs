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
using WMS.ViewModel.PrintManagement.PrintMOVMs;
using WMS.Util;

namespace WMS.PrintManagement.Controllers
{
    public partial class PrintMOController : BaseController
    {
        
        [ActionDescription("_Page.PrintManagement.PrintMO.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintMOVMs.PrintMOVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PrintManagement.PrintMO.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintMOVMs.PrintMOVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PrintManagement.PrintMO.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintMOVMs.PrintMOListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PrintManagement.PrintMO.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintMOVMs.PrintMOVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PrintManagement.PrintMO.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintMOVMs.PrintMOImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PrintManagement.PrintMO.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintMOVMs.PrintMOBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchPrintMO")]
        [HttpPost]
        public IActionResult SearchPrintMO(WMS.ViewModel.PrintManagement.PrintMOVMs.PrintMOSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintMOVMs.PrintMOListVM>(passInit: true);
            if (ModelState.IsValid)
            {
                vm.Searcher = searcher;
                if(vm.IsNeedReload())
                    vm.SearchErpData();
                if (!ModelState.IsValid)
                {
                    return Content(vm.GetError());
                }
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
        public IActionResult PrintMOExportExcel(WMS.ViewModel.PrintManagement.PrintMOVMs.PrintMOListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("打印")]
        public IActionResult Print(string ID)
        {
            PrintMOVM vm = Wtm.CreateVM<PrintMOVM>(ID);
            vm.Entity.PrintQty = vm.Entity.Qty.TrimZero();
            vm.Entity.PackingQty = vm.Entity.Qty.TrimZero();
            vm.InitPrintModules();
            if (!ModelState.IsValid)
            {
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            return PartialView(vm);
        }

        [ActionDescription("打印")]
        [HttpPost]
        public IActionResult DoPrint(PrintMOVM vm, IFormCollection nouse)
        {
            ModelState.Clear();
            if (vm.Entity.PrintQty <= 0)
            {
                ModelState.AddModelError("", "打印总数必须大于0");
                vm.InitPrintModules();
                return PartialView(vm.FromView, vm);
            }
            if (vm.Entity.PackingQty <= 0)
            {
                ModelState.AddModelError("", "装箱数必须大于0");
                vm.InitPrintModules();
                return PartialView(vm.FromView, vm);
            }
            if (vm.Entity.Qty < vm.Entity.PrintQty)
            {
                ModelState.AddModelError("", "打印总数不能大于可操作数");
                vm.InitPrintModules();
                return PartialView(vm.FromView, vm);
            }
            if (string.IsNullOrEmpty(vm.SelectedPrintModule))
            {
                ModelState.AddModelError("", "请选择打印模板");
                vm.InitPrintModules();
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


