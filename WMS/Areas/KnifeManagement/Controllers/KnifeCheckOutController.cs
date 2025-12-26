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
using WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs;
using WMS.ViewModel.KnifeManagement.KnifeVMs;

namespace WMS.KnifeManagement.Controllers
{
    public partial class KnifeCheckOutController : BaseController
    {
        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckOut.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs.KnifeCheckOutVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckOut.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs.KnifeCheckOutVM>(id);
            vm.KnifeCheckOutLineKnifeCheckOutList1.Searcher.KnifeCheckOutId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckOut.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs.KnifeCheckOutListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckOut.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs.KnifeCheckOutVM>(id);
            vm.KnifeCheckOutLineKnifeCheckOutList2.Searcher.KnifeCheckOutId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckOut.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs.KnifeCheckOutImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.KnifeManagement.KnifeCheckOut.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs.KnifeCheckOutBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchKnifeCheckOut")]
        [HttpPost]
        public IActionResult SearchKnifeCheckOut(WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs.KnifeCheckOutSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs.KnifeCheckOutListVM>(passInit: true);
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
        public IActionResult KnifeCheckOutExportExcel(WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs.KnifeCheckOutListVM vm)
        {
            return vm.GetExportData();
        }

        #region 领用打印 仅打印行上属于条码的记录
        [ActionDescription("打印")]
        public IActionResult Print(string ID)
        {
            KnifeCheckOutVM vm = Wtm.CreateVM<KnifeCheckOutVM>(ID);

            // 准备打印数据
            vm.InitPrintModules();
            if (!ModelState.IsValid)
            {
                return FFResult().Message($"失败:" + ModelState.GetErrorJson().GetFirstError());
            }

            // 将数据写入打印服务的数据库（接口）
            string id = vm.CreatePrintData();
            if (!ModelState.IsValid)
            {
                return FFResult().Message($"打印失败:"+ ModelState.GetErrorJson().GetFirstError());
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
        #endregion
    }
}


