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
using WMS.ViewModel.BaseData.BaseBarCodeVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseBarCodeController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseBarCode.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseBarCodeVMs.BaseBarCodeVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseBarCode.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseBarCodeVMs.BaseBarCodeVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseBarCode.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseBarCodeVMs.BaseBarCodeListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseBarCode.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseBarCodeVMs.BaseBarCodeVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseBarCode.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseBarCodeVMs.BaseBarCodeImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseBarCode.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseBarCodeVMs.BaseBarCodeBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseBarCode")]
        [HttpPost]
        public IActionResult SearchBaseBarCode(WMS.ViewModel.BaseData.BaseBarCodeVMs.BaseBarCodeSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseBarCodeVMs.BaseBarCodeListVM>(passInit: true);
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
        public IActionResult BaseBarCodeExportExcel(WMS.ViewModel.BaseData.BaseBarCodeVMs.BaseBarCodeListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("打印")]
        public IActionResult Print(string ID)
        {
            BaseBarCodeVM vm = Wtm.CreateVM<BaseBarCodeVM>(ID);
            vm.InitPrintModules();
            if (!ModelState.IsValid)
            {
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            return PartialView(vm);
        }

        [ActionDescription("打印")]
        [HttpPost]
        public IActionResult DoPrint(BaseBarCodeVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<BaseBarCodeBatchVM>(Ids: IDs);
            vm.InitPrintModules();
            if (!ModelState.IsValid)
            {
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            return PartialView(vm);
        }

        [ActionDescription("批量打印")]
        [HttpPost]
        public ActionResult DoBatchPrint(BaseBarCodeBatchVM vm, IFormCollection nouse)
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


