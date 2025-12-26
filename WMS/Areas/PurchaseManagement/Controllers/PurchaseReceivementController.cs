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
using WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs;
using WMS.Util;
using Newtonsoft.Json;

namespace WMS.PurchaseManagement.Controllers
{
    public partial class PurchaseReceivementController : BaseController
    {
        
        [ActionDescription("_Page.PurchaseManagement.PurchaseReceivement.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs.PurchaseReceivementVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseReceivement.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs.PurchaseReceivementVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<PurchaseReceivementVM>(id);
            vm.PurchaseReceivementLinePurchaseReceivementList.Searcher.PurchaseReceivementId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.PurchaseManagement.PurchaseReceivement.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs.PurchaseReceivementListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseReceivement.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs.PurchaseReceivementVM>(id);
            vm.PurchaseReceivementLinePurchaseReceivementList.Searcher.PurchaseReceivementId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseReceivement.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs.PurchaseReceivementImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseReceivement.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs.PurchaseReceivementBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchPurchaseReceivement")]
        [HttpPost]
        public IActionResult SearchPurchaseReceivement(WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs.PurchaseReceivementSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs.PurchaseReceivementListVM>(passInit: true);
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
        public IActionResult PurchaseReceivementExportExcel(WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs.PurchaseReceivementListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("取消收货操作")]
        public IActionResult CancelReceive(string id)
        {
            PurchaseReceivementApiVM vm = Wtm.CreateVM<PurchaseReceivementApiVM>();
            vm.CancelReceive(Guid.Parse(id));
            if (!ModelState.IsValid)
            {
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                return FFResult().RefreshGridRow(CurrentWindowId);
            }
        }

        [ActionDescription("帮助")]
        [AllRights]
        public IActionResult Help()
        {
            List<Help> data = new List<Help>
            {
                new Help
                {
                    Title = "检验操作",
                    Contents = new List<HelpContent>
                    {
                        new HelpContent{ Text = "在PDA端进行检验操作" },
                        new HelpContent{ Text = "允许只对部分行进行检验操作" },
                        new HelpContent{ Text = "单行必须一次性检验完毕，不允许分多次检验" },
                        new HelpContent{ Text = "所有行全部检验完成后，U9单据会同步完成检验" },
                        new HelpContent
                        {
                            Text = "检验操作不可逆（U9检验后无法逆向操作）。请根据实际情况，按下面的步骤进行逆向操作",
                            Contents = new List<HelpContent>
                            {
                                new HelpContent {Text = "如果全部行均已检验完成（U9已过账），请删除WMS系统单据后，再将U9收货单删除。并在U9端重新创建“收货单”"},
                                new HelpContent {Text = "如果只是部分行已检验完成（U9未过账），请删除WMS系统单据后，重新扫描收货单二维码进行操作" },
                            }
                        }
                    },
                },
                new Help
                {
                    Title = "收货操作",
                    Contents = new List<HelpContent>
                    {
                        new HelpContent{ Text = "在PDA端进行收货操作" },
                        new HelpContent{ Text = "所有行全部检验完成后，才允许进行收货操作" },
                        new HelpContent{ Text = "允许只对部分行进行收货操作" },
                        new HelpContent{ Text = "允许对行进行部分收货操作" },
                        new HelpContent{ Text = "扫描的条码必须全部收货，不允许对条码进行部分收货" },
                        new HelpContent{ Text = "如果多行存在相同的料号，收货扫条码时会按行号顺序自动进行关联" },
                    }
                },
                new Help
                {
                    Title = "取消收货操作",
                    Contents = new List<HelpContent>
                    {
                        new HelpContent{ Text = "在网页端进行取消收货操作" },
                        new HelpContent{ Text = "操作条件：本单据未做审核操作（即本单所有关联的条码均未上架）" }
                    }
                }
            };
            TempData["help"] = JsonConvert.SerializeObject(data);    // 存入TempData，用于传递参数。下面的RedirectToAction无法传递复杂对象
            return RedirectToAction("Help", "Home", new { Area = "" });
            //return PartialView("/Home/Help");
        }
    }
}


