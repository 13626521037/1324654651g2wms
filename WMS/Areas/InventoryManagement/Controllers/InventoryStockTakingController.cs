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
using WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs;
using WMS.ViewModel.InventoryManagement.InventoryStockTakingLocationsVMs;
using Elsa.Models;
using WMS.ViewModel.InventoryManagement.InventoryStockTakingErpDiffLineVMs;
using Newtonsoft.Json;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryStockTakingController : BaseController
    {
        /// <summary>
        /// 创建功能兼容修改功能（为了前端只做一次，方便修改维护）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionDescription("_Page.InventoryManagement.InventoryStockTaking.Create")]
        public ActionResult Create(string id)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs.InventoryStockTakingVM>();
            if (!string.IsNullOrEmpty(id))
            {
                vm.SetEntityById(id);
                vm.InventoryStockTakingLocationsStockTakingList.Searcher.StockTakingId = id;
            }
            else
            {
                vm.Entity.Dimension = InventoryStockTakingDimensionEnum.Location;   // 此字段已取消。一律设置为按库位进行盘点。
                vm.Entity.Status = InventoryStockTakingStatusEnum.Opened;
                vm.Entity.Mode = InventoryStockTakingModeEnum.ErpWms;
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryStockTaking.Edit")]
        public ActionResult Edit(string id)
        {
            return RedirectToAction("Create", new { id = id });
        }

        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingVM>(id);
            vm.InventoryStockTakingLineStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingLocationsStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingErpDiffLineStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingLocationsStockTakingList.HideDeleteButton = true;
            return PartialView(vm);
        }

        [ActionDescription("_Page.InventoryManagement.InventoryStockTaking.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs.InventoryStockTakingListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryStockTaking.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs.InventoryStockTakingVM>(id);
            vm.InventoryStockTakingLineStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingLocationsStockTakingList.HideDeleteButton = true;
            vm.InventoryStockTakingLocationsStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingErpDiffLineStockTakingList.Searcher.StockTakingId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryStockTaking.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs.InventoryStockTakingImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryStockTaking.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs.InventoryStockTakingBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryStockTaking")]
        [HttpPost]
        public IActionResult SearchInventoryStockTaking(WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs.InventoryStockTakingSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs.InventoryStockTakingListVM>(passInit: true);
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

        [ActionDescription("盘点明细行查询")]
        [HttpPost]
        public IActionResult SearchInventoryStockTakingLine(ViewModel.InventoryManagement.InventoryStockTakingLineVMs.InventoryStockTakingLineDetailSearcher searcher)
        {
            var vm = Wtm.CreateVM<ViewModel.InventoryManagement.InventoryStockTakingLineVMs.InventoryStockTakingLineStockTakingDetailListVM>(passInit: true);
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

        [ActionDescription("盘点库位明细查询")]
        [HttpPost]
        public IActionResult SearchInventoryStockTakingLocations(InventoryStockTakingLocationsDetailSearcher searcher)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingLocationsStockTakingDetailListVM>(passInit: true);
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

        [ActionDescription("盘点前ERP差异明细查询")]
        [HttpPost]
        public IActionResult SearchInventoryStockTakingErpDiffLine(InventoryStockTakingErpDiffLineDetailSearcher searcher)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingErpDiffLineStockTakingDetailListVM>(passInit: true);
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
        public IActionResult InventoryStockTakingExportExcel(WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs.InventoryStockTakingListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("导出盘点明细")]
        [HttpPost]
        public IActionResult ExportDetail(ViewModel.InventoryManagement.InventoryStockTakingLineVMs.InventoryStockTakingLineStockTakingDetailListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("选择盘点库位")]
        public IActionResult SelectLocation(string id)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingLocationsSelectListVM>();
            if (string.IsNullOrEmpty(id))
            {
                return FFResult().Message("请先保存盘点单再添加库位！");
            }
            if (Guid.TryParse(id, out Guid result))
                vm.Searcher.StockTakingId = result;
            return PartialView(vm);
        }

        [ActionDescription("选择盘点库位")]
        [HttpPost]
        public IActionResult SearchSelectLocation(InventoryStockTakingLocationsSelectSearcher searcher)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingLocationsSelectListVM>(passInit: true);
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

        //public IActionResult GetAllLocations(string id)
        //{
        //    var vm = Wtm.CreateVM<InventoryStockTakingVM>(id);
        //    return Ok(vm);
        //}

        [ActionDescription("添加选中库位")]
        public IActionResult AddSelectedLocations(string[] IDs, string docId)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingVM>();
            vm.AddSelectedLocations(IDs, docId);
            if (ModelState.IsValid)
            {
                return FFResult().RefreshGrid().AddCustomScript("ff.RefreshGrid('InventoryStockTaking_Create_Locations',0);");  // 刷新本页面的Grid和父页面的Grid
            }
            else
            {
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
            }
        }

        [ActionDescription("删除选中库位")]
        public IActionResult DeleteSelectedLocations(string[] IDs)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingVM>();
            vm.DeleteSelectedLocations(IDs);
            if (ModelState.IsValid)
            {
                return FFResult().RefreshGrid();
            }
            else
            {
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
            }
        }

        /// <summary>
        /// 前端框架内表格调用专用
        /// </summary>
        /// <returns></returns>
        [ActionDescription("获取判定单的库位列表")]
        [HttpPost]
        public IActionResult GetStockTakingLocations(InventoryStockTakingLocationsDetailSearcher searcher)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingLocationsStockTakingDetailListVM>(passInit: true);
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

        [ActionDescription("提交")]
        public IActionResult Submit(string id)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingVM>(id);
            vm.InventoryStockTakingLineStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingLocationsStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingErpDiffLineStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingLocationsStockTakingList.HideDeleteButton = true;
            return PartialView(vm);
        }

        [ActionDescription("审核")]
        public IActionResult Approve(string id)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingVM>(id);
            vm.InventoryStockTakingLineStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingLocationsStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingErpDiffLineStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingLocationsStockTakingList.HideDeleteButton = true;
            return PartialView(vm);
        }

        [ActionDescription("终止关闭")]
        public IActionResult ForceClose(string id)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingVM>(id);
            vm.InventoryStockTakingLineStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingLocationsStockTakingList.Searcher.StockTakingId = id;
            vm.InventoryStockTakingErpDiffLineStockTakingList.Searcher.StockTakingId = id;
            return PartialView(vm);
        }

        [ActionDescription("帮助")]
        [AllRights]
        public IActionResult Help()
        {
            List<Help> data = new List<Help>
            {
                new Help{
                    Title = "创建",
                    Contents = new List<HelpContent>
                    {
                        new HelpContent{ Text = "选择盘点单的存储地点、库位。" },
                        new HelpContent{ Text = "存储地点选定后，无法修改。如果存储地点选择有误，请删除盘点单后重新创建。" },
                        new HelpContent{ Text = "选择的库位将被“盘点锁定”。" },
                        new HelpContent{ Text = "操作后盘点单状态：开立" },
                    }
                },
                new Help{
                    Title = "修改",
                    Contents = new List<HelpContent>
                    {
                        new HelpContent{ Text = "维护盘点的库位（增、删）" },
                        new HelpContent{ Text = "操作后盘点单状态：开立" },
                    }
                },
                new Help{
                    Title = "提交",
                    Contents = new List<HelpContent>
                    {
                        new HelpContent{ Text = "根据选择的库位，生成盘点库存明细表。" },
                        new HelpContent{ Text = "提交后，盘点单无法删除。如需取消，请进行“终止”操作。" },
                        new HelpContent{ Text = "操作后盘点单状态：核准中" },
                        new HelpContent {Text = "提交操作不可逆，请谨慎操作。" },
                    }
                },
                new Help{
                    Title = "盘点（PDA端）",
                    Contents = new List<HelpContent>
                    {
                        new HelpContent{ Text = "“核准中”状态的盘点单，可以进行盘点操作。" },
                        new HelpContent{ Text = "可多人同时对一张盘点单进行盘点" },
                        new HelpContent{ Text = "操作后盘点单状态：核准中" },
                    }
                },
                new Help{
                    Title = "审核",
                    Contents = new List<HelpContent>
                    {
                        new HelpContent{ Text = "操作后盘点单状态：已审核" },
                        new HelpContent {Text = "审核操作不可逆，请谨慎操作。" },
                    }
                },
                new Help{
                    Title = "删除",
                    Contents = new List<HelpContent>
                    {
                        new HelpContent {Text = "“开立”状态的盘点单，可以进行删除操作。"},
                        new HelpContent {Text = "任何人可以删除任意开立状态的盘点单。"},
                        new HelpContent {Text = "删除操作会将盘点范围内的库位进行“解除锁定”" },
                        new HelpContent {Text = "删除操作不可逆，请谨慎操作。" },
                    }
                },
                new Help{
                    Title = "终止",
                    Contents = new List<HelpContent>
                    {
                        new HelpContent {Text = "只有创建人可以终止对应的盘点单"},
                        new HelpContent {Text = "终止操作会将盘点范围内的库位进行“解除锁定”" },
                        new HelpContent {Text = "提交后，但未审核的盘点单，可以终止，终止后，盘点单将无法进行任何操作，只可查看。" },
                        new HelpContent {Text = "终止操作不可逆，请谨慎操作。" },
                    }
                },
                new Help
                {
                    Title = "注意事项",
                    Contents = new List<HelpContent>
                    {
                        new HelpContent{ Text = "盘点模式默认为“ERP+WMS”。仓管员无法修改。如果需要改为“仅WMS”模式，请联系信息部进行评估后修改。" },
                        new HelpContent{ Text = "盘点单状态为“关闭”，为何ERP单号为空。以下三种情况无需过账ERP：1. 盘点模式为“仅WMS”。2. 盘点单全部盘平。3. 盘点单按料号和番号统计后，盘亏+盘盈=0" },
                    }
                }
            };
            TempData["help"] = JsonConvert.SerializeObject(data);    // 存入TempData，用于传递参数。下面的RedirectToAction无法传递复杂对象
            return RedirectToAction("Help", "Home", new { Area = "" });
            //return PartialView("/Home/Help");
        }
    }
}


