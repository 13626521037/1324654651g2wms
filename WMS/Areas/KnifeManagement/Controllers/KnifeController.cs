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
using WMS.ViewModel.KnifeManagement.KnifeVMs;
using WMS.ViewModel.BaseData.BaseInventoryVMs;

namespace WMS.KnifeManagement.Controllers
{
    public partial class KnifeController : BaseController
    {
        
        [ActionDescription("_Page.KnifeManagement.Knife.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeVMs.KnifeListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.Knife.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeVMs.KnifeVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("修改")]
        public ActionResult EditInStockStatus(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeVMs.KnifeVM>(id);
            vm.Entity.HandledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ID.ToString() == vm.Entity.HandledById);
            return PartialView(vm);
        }
        [ActionDescription("修改刀具状态")]
        public ActionResult EditKnifeStatus(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeVMs.KnifeVM>(id);
            vm.Entity.HandledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ID.ToString() == vm.Entity.HandledById);
            return PartialView(vm);
        }
        [ActionDescription("_Page.KnifeManagement.Knife.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeVMs.KnifeImportVM>();
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchKnife")]
        [HttpPost]
        public IActionResult SearchKnife(WMS.ViewModel.KnifeManagement.KnifeVMs.KnifeSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeVMs.KnifeListVM>(passInit: true);
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
        public IActionResult KnifeExportExcel(WMS.ViewModel.KnifeManagement.KnifeVMs.KnifeListVM vm)
        {
            return vm.GetExportData();
        }


        [ActionDescription("打印")]
        public IActionResult Print(string ID)
        {
            KnifeVM vm = Wtm.CreateVM<KnifeVM>(ID);
            if (vm.Entity.Status == KnifeStatusEnum.Scrapped)
            {
                return FFResult().Message("该刀具已报废，无法打印！");
            }
            if (vm.Entity.Status == KnifeStatusEnum.DefectiveReturned)
            {
                return FFResult().Message("该刀具已不良退回，无法打印！");
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
        public IActionResult DoPrint(KnifeVM vm, IFormCollection nouse)
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

        #region 批量报废
        [ActionDescription("批量报废")]
        [HttpPost]
        public ActionResult BatchScrap(string[] IDs)
        {
            var vm = Wtm.CreateVM<KnifeBatchScrapVM>(Ids: IDs);
            return PartialView(vm);
        }

        [ActionDescription("执行批量报废")]
        [HttpPost]
        public ActionResult DoBatchScrap(KnifeBatchScrapVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid)
            {
                return FFResult().Alert("请选择报废类型");
            }
            
            var errorMsg = vm.DoBatchScrap();
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return FFResult().Alert(errorMsg);
            }
            
            return FFResult().CloseDialog().RefreshGrid().Alert("批量报废成功，报废单已创建");
        }
        #endregion

        #region 批量修磨申请
        [ActionDescription("批量修磨申请")]
        [HttpPost]
        public ActionResult BatchGrindRequest(string[] IDs)
        {
            var vm = Wtm.CreateVM<KnifeBatchGrindRequestVM>(Ids: IDs);
            return PartialView(vm);
        }

        [ActionDescription("执行批量修磨申请")]
        [HttpPost]
        public ActionResult DoBatchGrindRequest(KnifeBatchGrindRequestVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid)
            {
                return FFResult().Alert("参数验证失败");
            }
            
            var errorMsg = vm.DoBatchGrindRequest();
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return FFResult().Alert(errorMsg);
            }
            
            return FFResult().CloseDialog().RefreshGrid().Alert("批量修磨申请成功");
        }
        #endregion
    }
}


