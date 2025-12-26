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
using WMS.ViewModel.KnifeManagement.KnifeCombineVMs;
using WMS.ViewModel.KnifeManagement.KnifeVMs;

namespace WMS.KnifeManagement.Controllers
{
    public partial class KnifeCombineController : BaseController
    {
        
        [ActionDescription("_Page.KnifeManagement.KnifeCombine.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCombineVMs.KnifeCombineListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCombine.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCombineVMs.KnifeCombineVM>(id);
            vm.KnifeCombineLineKnifeCombineList.Searcher.KnifeCombineId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCombine.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCombineVMs.KnifeCombineImportVM>();
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchKnifeCombine")]
        [HttpPost]
        public IActionResult SearchKnifeCombine(WMS.ViewModel.KnifeManagement.KnifeCombineVMs.KnifeCombineSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCombineVMs.KnifeCombineListVM>(passInit: true);
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
        public IActionResult KnifeCombineExportExcel(WMS.ViewModel.KnifeManagement.KnifeCombineVMs.KnifeCombineListVM vm)
        {
            return vm.GetExportData();
        }



        [ActionDescription("打印")]
        public IActionResult Print(string ID)
        {
            KnifeCombineVM vm = Wtm.CreateVM<KnifeCombineVM>(ID);
            //vm.KnifeCombineLineKnifeCombineList.Searcher.KnifeCombineId = ID;

            if (vm.Entity.Status == KnifeOrderStatusEnum.ApproveClose)
            {
                return FFResult().Message("该组合刀已关闭，无法打印！");
            }
            if (vm.Entity.Status == KnifeOrderStatusEnum.Open)
            {
                return FFResult().Message("该组合刀未审核，无法打印！");
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
        public IActionResult DoPrint(KnifeCombineVM vm, IFormCollection nouse)
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
    }
}


