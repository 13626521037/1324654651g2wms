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
using WMS.ViewModel.PrintManagement.PrintSupplierVMs;
using WMS.Util;

namespace WMS.PrintManagement.Controllers
{
    public partial class PrintSupplierController : BaseController
    {
        
        [ActionDescription("_Page.PrintManagement.PrintSupplier.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintSupplierVMs.PrintSupplierVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PrintManagement.PrintSupplier.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintSupplierVMs.PrintSupplierVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PrintManagement.PrintSupplier.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintSupplierVMs.PrintSupplierListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PrintManagement.PrintSupplier.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintSupplierVMs.PrintSupplierVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PrintManagement.PrintSupplier.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintSupplierVMs.PrintSupplierImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PrintManagement.PrintSupplier.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintSupplierVMs.PrintSupplierBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchPrintSupplier")]
        [HttpPost]
        public IActionResult SearchPrintSupplier(WMS.ViewModel.PrintManagement.PrintSupplierVMs.PrintSupplierSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.PrintManagement.PrintSupplierVMs.PrintSupplierListVM>(passInit: true);
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
        public IActionResult PrintSupplierExportExcel(WMS.ViewModel.PrintManagement.PrintSupplierVMs.PrintSupplierListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("打印")]
        public IActionResult Print(string ID)
        {
            PrintSupplierVM vm = Wtm.CreateVM<PrintSupplierVM>(ID);
            vm.Entity.PrintQty = vm.Entity.ValidQty.TrimZero();
            vm.Entity.PackingQty = vm.Entity.ValidQty.TrimZero();
            vm.InitPrintModules();
            if (!ModelState.IsValid)
            {
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            return PartialView(vm);
        }

        [ActionDescription("打印")]
        [HttpPost]
        public IActionResult DoPrint(PrintSupplierVM vm, IFormCollection nouse, int type)
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
            if (vm.Entity.ValidQty < vm.Entity.PrintQty)
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
            if (type == 2)
            {
                url = Wtm.ConfigInfo.AppSettings["PrintServer"] + "/_Admin/CPrintModule/PrintByDataIdPDF?dataId=" + id;
            }
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

        [ActionDescription("自定义打印")]
        public IActionResult CustomPrint(string ID)
        {
            PrintSupplierVM vm = Wtm.CreateVM<PrintSupplierVM>(ID);
            vm.InitPrintModules();
            if (!ModelState.IsValid)
            {
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            return PartialView(vm);
        }

        [ActionDescription("自定义打印")]
        [HttpPost]
        public IActionResult DoCustomPrint(PrintSupplierVM vm, IFormCollection nouse, int type)
        {
            ModelState.Clear();
            if (string.IsNullOrEmpty(vm.Entity.CustomQty))
            {
                ModelState.AddModelError("", "未正确输入打印数量");
                vm.InitPrintModules();
                return PartialView(vm.FromView, vm);
            }
            var qtyStrArray = vm.Entity.CustomQty.Split('/');
            List<decimal> qtys = new List<decimal>();
            decimal totalQty = 0;
            foreach (var item in qtyStrArray)
            {
                if (decimal.TryParse(item, out decimal num) == false)
                {
                    ModelState.AddModelError("", "未正确输入打印数量");
                    vm.InitPrintModules();
                    return PartialView(vm.FromView, vm);
                }
                qtys.Add(num);
                totalQty += num;
            }
            if (vm.Entity.ValidQty < totalQty)
            {
                ModelState.AddModelError("", $"打印总数{totalQty}不能大于可操作数");
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
            string id = vm.CreateCustomPrintData(qtys);
            if (!ModelState.IsValid)
            {
                vm.InitPrintModules();
                return PartialView(vm.FromView, vm);
            }
            // 按照返回的ID，访问打印服务的地址，进行页面展示
            string url = Wtm.ConfigInfo.AppSettings["PrintServer"] + "/_Admin/CPrintModule/PrintByDataId?dataId=" + id;
            if (type == 2)
            {
                url = Wtm.ConfigInfo.AppSettings["PrintServer"] + "/_Admin/CPrintModule/PrintByDataIdPDF?dataId=" + id;
            }
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


