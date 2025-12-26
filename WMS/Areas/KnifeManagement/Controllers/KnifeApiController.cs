using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.KnifeManagement.KnifeVMs;
using WMS.Model.KnifeManagement;
using WMS.Model;
using WMS.Model.BaseData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs;
using NPOI.SS.Formula.Functions;
using WMS.Model.PurchaseManagement;
using WMS.Util;
using Newtonsoft.Json;
using Elsa;
using WMS.ViewModel.KnifeManagement.KnifeCheckInVMs;
using WMS.ViewModel.KnifeManagement.KnifeCombineVMs;
using WMS.ViewModel.KnifeManagement.KnifeScrapVMs;
using WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs;
using static NPOI.HSSF.Util.HSSFColor;
using WMS.ViewModel.KnifeManagement.KnifeTransferInVMs;
using WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs;
using WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs;
using WMS.ViewModel.KnifeManagement.KnifeGrindInVMs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WMS.ViewModel.KnifeManagement.KnifeOperationVMs;


namespace WMS.Controllers
{
    [Area("KnifeManagement")]
    [AuthorizeJwt]
    [ActionDescription("刀具管理")]
    [ApiController]
    [Route("api/Knife")]
	public partial class KnifeApiController : BaseApiController
    {
        #region 系统原生接口
        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
		public IActionResult Search(KnifeApiSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<KnifeApiListVM>(passInit: true);
                vm.Searcher = searcher;
                return Content(vm.GetJson());
            }
            else
            {
                return BadRequest(ModelState.GetErrorJson());
            }
        }

        [ActionDescription("Sys.Get")]
        [HttpGet("{id}")]
        public KnifeApiVM Get(string id)
        {
            var vm = Wtm.CreateVM<KnifeApiVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(KnifeApiVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }

        }

        [ActionDescription("Sys.Edit")]
        [HttpPut("Edit")]
        public IActionResult Edit(KnifeApiVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoEdit(false);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }
        }

		[HttpPost("BatchDelete")]
        [ActionDescription("Sys.Delete")]
        public IActionResult BatchDelete(string[] ids)
        {
            var vm = Wtm.CreateVM<KnifeApiBatchVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = ids;
            }
            else
            {
                return Ok();
            }
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                return Ok(ids.Count());
            }
        }


        [ActionDescription("Sys.Export")]
        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel(KnifeApiSearcher searcher)
        {
            var vm = Wtm.CreateVM<KnifeApiListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<KnifeApiListVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = new List<string>(ids);
                vm.SearcherMode = ListVMSearchModeEnum.CheckExport;
            }
            return vm.GetExportData();
        }

        [ActionDescription("Sys.DownloadTemplate")]
        [HttpGet("GetExcelTemplate")]
        public IActionResult GetExcelTemplate()
        {
            var vm = Wtm.CreateVM<KnifeApiImportVM>();
            var qs = new Dictionary<string, string>();
            foreach (var item in Request.Query.Keys)
            {
                qs.Add(item, Request.Query[item]);
            }
            vm.SetParms(qs);
            var data = vm.GenerateTemplate(out string fileName);
            return File(data, "application/vnd.ms-excel", fileName);
        }

        [ActionDescription("Sys.Import")]
        [HttpPost("Import")]
        public ActionResult Import(KnifeApiImportVM vm)
        {
            if (vm!=null && (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData()))
            {
                return BadRequest(vm.GetErrorJson());
            }
            else
            {
                return Ok(vm?.EntityList?.Count ?? 0);
            }
        }


        [HttpGet("GetBaseWhLocations")]
        public ActionResult GetBaseWhLocations()
        {
            return Ok(DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, x => x.Name));
        }

        [HttpGet("GetBaseItemMasters")]
        public ActionResult GetBaseItemMasters()
        {
            return Ok(DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, x => x.Name));
        }
        #endregion

        #region 刀具业务接口

        #region 通用接口
        [ActionDescription("查询库存/刀具")]
        [HttpPost("GetKnifeAndInventory")]
        [Public]
        public ActionResult GetKnifeAndInventory(GetKnifeAndInventoryInputInfo info)
        {
            ReturnResult<List<GetKnifeAndInventoryReturn>> rr = new ReturnResult<List<GetKnifeAndInventoryReturn>>();
            try
            {
                var knifeVM = Wtm.CreateVM<KnifeVM>();
                rr.Entity = knifeVM.GetKnifeAndInventory(info);
                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail("查询失败:" + ModelState.GetErrorJson().GetFirstError());
                }
                return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            }
            catch (Exception e)
            {
                rr.Entity = null;
                rr.SetFail("捕获到异常:" + e.Message);
                return Ok(rr);
            }
        }


        [ActionDescription("获取刀具履历记录")]
        [HttpPost("GetKnifeOperations")]
        [Public]
        public ActionResult GetKnifeOperations(GetKnifeOperationsInputInfo info)
        {
            ReturnResult<List<KnifeOperationsReturn>> rr = new ReturnResult<List<KnifeOperationsReturn>>();
            var vm = Wtm.CreateVM<KnifeOperationVM>();
            rr.Entity = vm.getKnifeOperations(info);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);//

        }
        [ActionDescription("计算履历表中刀具料品寿命")]
        [HttpPost("GetKnifeItemLives")]
        [Public]
        public ActionResult GetKnifeItemLives()
        {
            var rr = new ReturnResult<List<KnifeItemLivesReturn>>();
            var vm = Wtm.CreateVM<KnifeOperationVM>();
            rr.Entity = vm.GetKnifeItemLives();
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);//

        }





        [ActionDescription("获取刀具中心下的所有生产部门")]
        [HttpPost("GetProductDept")]
        public ActionResult GetProductDept()
        {
            ReturnResult<List<KnifeDeptReturn>> rr = new ReturnResult<List<KnifeDeptReturn>>();
            rr.Entity = DC.Set<BaseDepartment>().AsNoTracking()
                .Include(x=>x.Organization)
                .Where(x => x.IsEffective == EffectiveEnum.Effective && x.IsMFG == true&&x.Organization.Name.Contains("刀具"))
                .Select(x => new KnifeDeptReturn{ ID=x.ID.ToString(), Name=x.Name, Code = x.Code })
                .ToList();
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }

            if (rr.Entity != null && rr.Entity.Count > 0)   // 2025-09-16 zhouxin 增加过滤，只显示末级的部门
            {
                rr.Entity = rr.Entity.Where(
                    x => !rr.Entity.Any(y => y.Code != x.Code 
                    && y.Code.StartsWith(x.Code) 
                    && y.Code.Length == x.Code.Length + 2)).ToList();
            }
            //return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));//json序列化时省去值为null的键值对
            return Ok(rr);//
        }

        [ActionDescription("获取指定部门下的业务员")]
        [HttpPost("GetOperatorsByDept")]
        public ActionResult GetOperatorsByDept(string deptID)
        {
            ReturnResult<List<KnifeOperatorReturn>> rr = new ReturnResult<List<KnifeOperatorReturn>>();
            rr.Entity = DC.Set<BaseOperator>().AsNoTracking()
                .Where(x => x.Department.ID.ToString() == deptID)
                .Select(x => new KnifeOperatorReturn { ID = x.ID.ToString(), Name = x.Name })
.               ToList();
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
        //2025-08-08 删除老的两个分开的获取新老刀信息接口
        /*删除[ActionDescription("领用时通过条码获取新老刀刀具信息")]
        [HttpPost("GetNewAndOldKnifeInfoByBar")]
        public ActionResult GetNewAndOldKnifeInfoByBar(bool isNew, string bar)
        {
            ReturnResult<KnifeItemMasterReturn> rr = new ReturnResult<KnifeItemMasterReturn>();
            var knifeVM = Wtm.CreateVM<KnifeVM>();
            if (isNew)
            {
                rr.Entity=knifeVM.GetNewKnifeInfo(bar,0);
                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail("条码刀获取信息失败:"+ModelState.GetErrorJson().GetFirstError());
                }
            }
            else
            {
                rr.Entity=knifeVM.GetOldKnifeInfo(bar,0);//0领用  1归还
                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail("台账刀获取信息失败:" + ModelState.GetErrorJson().GetFirstError());
                }
            }
            return Ok(rr);
        }
        [ActionDescription("通过条码获取老刀刀具信息")]
        [HttpPost("GetOldKnifeInfoByBar")]
        public ActionResult GetOldKnifeInfoByBar(string KnifeNo ,int type)//  1归还 2报废
        {
            ReturnResult<KnifeItemMasterReturn> rr = new ReturnResult<KnifeItemMasterReturn>();
            var knifeVM = Wtm.CreateVM<KnifeVM>();
            //归还不存在新刀 只有老刀                                                      

            rr.Entity = knifeVM.GetOldKnifeInfo(KnifeNo, type);//0领用  1归还 2报废  0可用但不会用到0
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail("归还时老刀获取信息失败:" + ModelState.GetErrorJson().GetFirstError());
            }

            return Ok(rr);
        }*/

        //2025.12.4 删除此接口 组合刀号获取信息的方式已经合并了
        /*[ActionDescription("通过组合刀号获取刀具信息列表")]//还有归还人信息
        [HttpPost("GetInfoByCombineknifeNo")]
        public ActionResult GetInfoByCombineknifeNo(string CombineKnifeNo)
        {
            ReturnResult<List<KnifeItemMasterReturn>> rr = new ReturnResult<List<KnifeItemMasterReturn>>();//扫描组合刀号返回信息 是多条刀的信息
            var knifeCombineVM = Wtm.CreateVM<KnifeCombineVM>();

            rr.Entity = knifeCombineVM.GetInfoByCombineknifeNo(CombineKnifeNo);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail("扫描组合刀号获取信息失败:" + ModelState.GetErrorJson().GetFirstError());
            }

            return Ok(rr);

        }*/


        [ActionDescription("获取刀具信息")]
        [HttpPost("GetKnifeInfoByBar")]
        public ActionResult GetKnifeInfoByBar(string bar ,int type)//0领用  1归还 2报废 3替换 4调出 5调入 6移库 7修磨申请 8修磨出库 9修磨入库
        {
            ReturnResult<List<KnifeItemMasterReturn>> rr = new ReturnResult<List<KnifeItemMasterReturn>>();
            try
            {
                var knifeVM = Wtm.CreateVM<KnifeVM>();
                rr.Entity = knifeVM.GeBarInfo(bar, type);

                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail("刀具获取信息失败:" + ModelState.GetErrorJson().GetFirstError());
                }
                return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            }
            catch (Exception e)
            {
                rr.Entity = null;
                rr.SetFail("捕获到异常:" + e.Message);
                return Ok(rr);
            }
        }
        





        #endregion
        #region 刀具领用
        [ActionDescription("[PDA] 刀具领用")]
        [HttpPost("KnifeCheckOut")]
        public  ActionResult KnifeCheckOut(KnifeCheckOutInputInfo info)
        {
            ReturnResult<List<BlueToothPrintDataLineReturn>> rr = new ReturnResult<List<BlueToothPrintDataLineReturn>>();
            rr.Entity = new List<BlueToothPrintDataLineReturn>();
            //先根据传上来的领用单参数  将其中的新刀做入台账操作
            //然后构建vm的entity  领用单的创建与审核
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeCheckOutVM vm = Wtm.CreateVM<KnifeCheckOutVM>();
                vm.DoInitByInfo(info,tran,rr);//检验在初始化的时候一起进行
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"领用失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                if (vm.Entity.KnifeCheckOutLine_KnifeCheckOut.Count != 0)//只领低价值刀具就不生成领用单
                {
                    vm.DoAdd();
                }
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"领用失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                if (vm.Entity.KnifeCheckOutLine_KnifeCheckOut.Count != 0)
                {
                    vm.DoApproved(tran, rr);
                }
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"领用失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess($"成功领用,涉及高价值刀具数:{vm.Entity.KnifeCheckOutLine_KnifeCheckOut.Count}把");
                return Ok(rr);
                }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("领用失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }

        [ActionDescription("期初刀具领用")]
        [HttpPost("BeginningOfPeriodKnifeCheckOut")]
        [Public]
        public ActionResult BeginningOfPeriodKnifeCheckOut(List<BeginningOfPeriodKnifeCheckOutInputInfo> info)
        {
            ReturnResult<List<BeginningOfPeriodKnifeCheckOutInputInfo>> rr = new ReturnResult<List<BeginningOfPeriodKnifeCheckOutInputInfo>>();
            rr.Entity = new List<BeginningOfPeriodKnifeCheckOutInputInfo>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeCheckOutVM vm = Wtm.CreateVM<KnifeCheckOutVM>();
                vm.BeginningOfPeriodKnifeCheckOut(info,tran);//检验在初始化的时候一起进行
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"期初刀具领用失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                rr.Entity = info;
                rr.SetSuccess($"期初刀具领用成功");
                tran.Commit();
                return Ok(rr);
            }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail($"期初刀具领用失败:{ModelState.GetErrorJson().GetFirstError()}");
                return Ok(rr);
            }
        }


        #endregion
        #region 刀具归还
        [ActionDescription("[PDA] 刀具归还")]
        [HttpPost("KnifeCheckIn")]
        public ActionResult KnifeCheckIn(KnifeCheckInInputInfo info)
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                #region 归还类型二次划分//对于前端改动更大 不用了   CheckInTypeUnion //0普通归还 1错领归还 2维修归还 3报废归还 4组合归还 
                //CheckInType //0普通归还 1错领归还   4组合归还
                //CheckInType2 //0普通归还 1维修归还 2报废归还
                /*switch (info.CheckInTypeUnion)
                {
                    case 0:
                        info.CheckInType = KnifeCheckInTypeEnum.NormalCheckIn;
                        info.CheckInType2 = 0;
                        break;
                    case 1:
                        info.CheckInType = KnifeCheckInTypeEnum.WrongPickupCheckIn;
                        info.CheckInType2 = 0;
                        break;
                    case 2:
                        info.CheckInType = KnifeCheckInTypeEnum.NormalCheckIn;
                        info.CheckInType2 = 1;
                        break;
                    case 3:
                        info.CheckInType = KnifeCheckInTypeEnum.NormalCheckIn;
                        info.CheckInType2 = 2;
                        break;
                    case 4:
                        info.CheckInType = KnifeCheckInTypeEnum.CombineCheckIn;
                        info.CheckInType2 = 0;
                        break;
                    default:
                        tran.Rollback();
                        rr.Entity = null;
                        rr.SetFail($"归还失败:未知的归还类型");
                        return Ok(rr);

                }*/
                #endregion
                KnifeCheckInVM vm = Wtm.CreateVM<KnifeCheckInVM>();
                vm.DoInitByInfo(info, tran);//检验在初始化的时候一起进行 
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"归还失败 :{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"归还失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoApproved(tran);
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"归还失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess( $"成功归还,涉及刀具数:{vm.Entity.KnifeCheckInLine_KnifeCheckIn.Count}把");
                return Ok(rr);
            }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("归还失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }
        #endregion
        #region 修磨申请
        [ActionDescription("[PDA] 刀具修磨申请")]
        [HttpPost("KnifeGrindRequest")]
        public ActionResult KnifeGrindRequest(KnifeGrindRequestInputInfo info)
        {
            // 【2025-12-18 修改】返回蓝牙打印数据格式
            ReturnResult<List<BlueToothPrintDataLineReturn>> rr = new ReturnResult<List<BlueToothPrintDataLineReturn>>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeGrindRequestVM vm = Wtm.CreateVM<KnifeGrindRequestVM>();
                vm.DoInitByInfo(info, tran,rr);//检验在初始化的时候一起进行 
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"失败 :{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoApproved(info,tran,rr);
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess ($"修磨申请成功,涉及刀具数:{vm.Entity.KnifeGrindRequestLine_KnifeGrindRequest.Count}把");
                return Ok(rr);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("修磨申请失败: " + ex.Message);
                return Ok(rr);
            }
        }
        [ActionDescription("U9采购单审核/弃审回写修磨申请单")]
        [HttpPost("UpdateKnifeGrindOutByU9")]
        [Public]//u9不登录就可以访问此接口
        public ActionResult UpdateKnifeGrindOutByU9(List<UpdateKnifeGrindRequestByU9Input> info)
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeGrindRequestVM vm = Wtm.CreateVM<KnifeGrindRequestVM>();
                vm.UpdateKnifeGrindRequestByU9(info);
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);
                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess($"采购申请/弃审修改修磨申请单信息成功");
                return Ok(rr);
            }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }

        #endregion
        #region 修磨出库
        [ActionDescription("[PDA] 刀具修磨出库")]
        [HttpPost("KnifeGrindOut")]
        public ActionResult KnifeGrindOut(KnifeGrindOutInputInfo info)
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeGrindOutVM vm = Wtm.CreateVM<KnifeGrindOutVM>();
                vm.DoInitByInfo(info, tran);//检验在初始化的时候一起进行 
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"失败 :{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoApproved(info,tran);
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess ($"修磨出库成功,涉及刀具数:{vm.Entity.KnifeGrindOutLine_KnifeGrindOut.Count}把");
                return Ok(rr);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("修磨出库失败: " + ex.Message);
                return Ok(rr);
            }
        }

        [ActionDescription("修磨出库扫描采购单获取刀具列表信息")]
        [HttpPost("GetKnifesByPODocNo")]
        public ActionResult GetKnifesByPODocNo(string PODocNo)
        {
            ReturnResult<List<KnifeGrindOutLineGetFromU9POReturn>> rr = new ReturnResult<List<KnifeGrindOutLineGetFromU9POReturn>>();
            try
            {
                KnifeGrindOutVM vm = Wtm.CreateVM<KnifeGrindOutVM>();
                rr.Entity = vm.GetKnifesByPODocNo(PODocNo);
                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail($"获取刀具信息失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);
                }

                return Ok(rr);
            }
            catch(Exception e)
            {
                rr.Entity = null;
                rr.SetFail("异常:" + e.Message);
                return Ok(rr);
            }
        }






        #endregion
        #region 修磨入库
        [ActionDescription("[PDA] 刀具修磨入库")]
        [HttpPost("KnifeGrindIn")]
        public ActionResult KnifeGrindIn(KnifeGrindInInputInfo info)//直接收货单号吧?1对1关系的
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeGrindInVM vm = Wtm.CreateVM<KnifeGrindInVM>();
                vm.DoInitByInfo(info, tran);//检验在初始化的时候一起进行 
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"失败 :{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoApproved(info ,tran);
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess ($"修磨入库成功,涉及刀具数:{vm.Entity.KnifeGrindInLine_KnifeGrindIn.Count}把");
                return Ok(rr);
            }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("修磨入库失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }

        [ActionDescription("修磨入库扫描收货单获取刀具列表信息")]
        [HttpPost("GetKnifesByRcvDocNo")]
        public ActionResult GetKnifesByRcvDocNo(string RcvDocNo)
        {
            ReturnResult<List<KnifeGrindInLineReturn>> rr = new ReturnResult<List<KnifeGrindInLineReturn>>();
            try
            {
                KnifeGrindInVM vm = Wtm.CreateVM<KnifeGrindInVM>();
                rr.Entity = vm.GetKnifesByRcvDocNo(RcvDocNo);
                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail($"获取刀具信息失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);
                }

                return Ok(rr);
            }
            catch(Exception e)
            {
                rr.Entity = null;
                rr.SetFail("异常:"+e.Message);
                return Ok(rr);
            }
        }
        #endregion
        #region 刀具报废
        [ActionDescription("[PDA] 刀具报废")]//其实是报废申请 要OA回写的时候才是真报废
        [HttpPost("KnifeScrap")]
        public ActionResult KnifeScrap(KnifeScrapInputInfo info)
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeScrapVM vm = Wtm.CreateVM<KnifeScrapVM>();
                vm.DoInitByInfo(info, tran);//检验在初始化的时候一起进行
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"报废失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"报废失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoApproved();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"报废失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess ($"报废申请提交成功,涉及刀具数:{vm.Entity.KnifeScrapLine_KnifeScrap.Count}把");
                return Ok(rr);
            }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("报废失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }

        [ActionDescription("刀具报废OA通过")]//
        [HttpPost("OADoApproved")]
        [Public]
        public ActionResult OADoApproved(OAKnifeScrapInputInfo info)
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeScrapVM vm = Wtm.CreateVM<KnifeScrapVM>();
                vm.OADoApproved(info);
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess($"报废成功,涉及刀具数:{vm.Entity.KnifeScrapLine_KnifeScrap.Count}把");
                return Ok(rr);
            }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("报废失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }
        #endregion
        #region 刀具调入
        #region 刀具调入 思路有误 这段废弃
        /*
        //新增
        [ActionDescription("刀具调入单_新增")]
        [HttpPost("KnifeTransferIn_Add")]
        public ActionResult KnifeTransferIn_Add()
        {
            ReturnResult<KnifeTransferIn> rr = new ReturnResult<KnifeTransferIn>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeTransferInVM vm = Wtm.CreateVM<KnifeTransferInVM>();
                vm.DoInitForAdd(tran);//调入单的新增初始化
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"新增领用单操作失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.Entity = vm.Entity;
                return Ok(rr);
            }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("新建调入单失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }
        //获取开立状态列表
        [ActionDescription("刀具调入单_获取列表")]
        [HttpPost("KnifeTransferIn_GetList")]
        public ActionResult KnifeTransferIn_GetList()
        {
            ReturnResult<List<KnifeTransferIn>> rr = new ReturnResult<List<KnifeTransferIn>>();
            try
            {
                KnifeTransferInVM vm = Wtm.CreateVM<KnifeTransferInVM>();
                rr.Entity = vm.GetList();
                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail($"{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);
                }
                return Ok(rr);
            }
            catch
            {
                rr.Entity = null;
                rr.SetFail("获取调入单列表失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }
        //获取开立状态单个
        [ActionDescription("刀具调入单_获取单个")]
        [HttpPost("KnifeTransferIn_GetOne")]
        public ActionResult KnifeTransferIn_GetOne(string DocNo)
        {
            ReturnResult<KnifeTransferIn> rr = new ReturnResult<KnifeTransferIn>();
            try
            {
                KnifeTransferInVM vm = Wtm.CreateVM<KnifeTransferInVM>();
                rr.Entity = vm.GetOne(DocNo);
                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail($"{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);
                }
                return Ok(rr);
            }
            catch
            {
                rr.Entity = null;
                rr.SetFail("获取刀具调入单失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }
        //修改
        [ActionDescription("刀具调入单_修改")]
        [HttpPost("KnifeTransferIn_Update")]
        public ActionResult KnifeTransferIn_Update(KnifeTransferInInputInfoForUpdate info)
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeTransferInVM vm = Wtm.CreateVM<KnifeTransferInVM>();
                vm.DoInitForUpdate(info);//调入单的修改操作的初始化vm 只在两步调入时会用到这里的方法
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);
                }
                DC.SaveChanges();
                tran.Commit();
                rr.Entity = "修改调入单成功";
                return Ok(rr);
            }
            catch (Exception e)
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("修改调入单失败:" + e.Message);
                return Ok(rr);
            }
        }
        //删除
        [ActionDescription("刀具调入单_删除")]
        [HttpPost("KnifeTransferIn_Delete")]
        public ActionResult KnifeTransferIn_Delete(string DocNo)
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            try
            {
                KnifeTransferInVM vm = Wtm.CreateVM<KnifeTransferInVM>();
                vm.Delete(DocNo);
                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail($"{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);
                }
                rr.Entity = $"删除{DocNo}成功";
                return Ok(rr);
            }
            catch
            {
                rr.Entity = null;
                rr.SetFail("删除刀具调入单失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }
        //提交/审核
        [ActionDescription("刀具调入单_审核")]
        [HttpPost("KnifeTransferIn_Approve")]
        public ActionResult KnifeTransferIn_Approve(string DocNo)
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeTransferInVM vm = Wtm.CreateVM<KnifeTransferInVM>();
                vm.DoInitForApproved(DocNo);
                if (!ModelState.IsValid)
                {
                    rr.Entity = $"审核{DocNo}失败";
                    rr.SetFail($"{ModelState.GetErrorJson().GetFirstError()}");
                    tran.Rollback();
                    return Ok(rr);
                }
                vm.DoApproved();
                if (!ModelState.IsValid)
                {
                    rr.Entity = $"审核{DocNo}失败";
                    rr.SetFail($"{ModelState.GetErrorJson().GetFirstError()}");
                    tran.Rollback();
                    return Ok(rr);
                }
                rr.Entity = $"审核{DocNo}成功";
                return Ok(rr);
            }
            catch
            {
                rr.Entity = null;
                rr.SetFail("审核刀具调入单失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }*/
        #endregion
        [ActionDescription("刀具调入时扫描调出单获取刀具列表信息")]
        [HttpPost("GetKnifesByTransferOutDocNo")]
        public ActionResult GetKnifesByTransferOutDocNo(string DocNo)
        {
            ReturnResult<List<KnifeTransferOutLineReturn>> rr = new ReturnResult<List<KnifeTransferOutLineReturn>>();
            try
            {
                KnifeTransferOutVM vm = Wtm.CreateVM<KnifeTransferOutVM>();
                rr.Entity = vm.GetKnifesByTransferOutDocNo(DocNo);
                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail($"获取调出单刀具信息失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);
                }
              
                return Ok(rr);
            }
            catch
            {
                rr.Entity = null;
                rr.SetFail("无法获取调出单刀具信息");
                return Ok(rr);
            }
        }
        [ActionDescription("[PDA] 刀具调入")]
        [HttpPost("KnifeTransferIn")]
        public ActionResult KnifeTransferIn(KnifeTransferInInputInfo info)
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeTransferInVM vm = Wtm.CreateVM<KnifeTransferInVM>();
                vm.DoInitByInfo(info,tran);//调入单的新增初始化
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"调入失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoApproved();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"调入失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess ($"刀具成功调入涉及刀具数:{vm.Entity.KnifeTransferInLine_KnifeTransferIn.Count}把");
                return Ok(rr);
            }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("调入失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }
        #endregion
        #region 刀具调出
        [ActionDescription("[PDA] 刀具调出")]
        [HttpPost("KnifeTransferOut")]
        public ActionResult KnifeTransferOut(KnifeTransferOutInputInfo info)
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeTransferOutVM vm = Wtm.CreateVM<KnifeTransferOutVM>();
                vm.DoInitByInfo(info, tran);//赋值
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"刀具调出失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"刀具调出失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm.DoApproved();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"刀具调出失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess ($"刀具成功调出 涉及刀具数:{vm.Entity.KnifeTransferOutLine_KnifeTransferOut.Count}把");
                return Ok(rr);
            }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("刀具调出失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }
        [ActionDescription("获取调出单的调入存储地点列表")]
        [HttpPost("GetToWhList")]
        public ActionResult GetToWhList()
        {
            ReturnResult<List<BaseWareHoustReturn>> rr = new ReturnResult<List<BaseWareHoustReturn>>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeTransferOutVM vm = Wtm.CreateVM<KnifeTransferOutVM>();

                rr.Entity = vm.GetToWhList();
                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail("获取调入存储地点列表失败:" + ModelState.GetErrorJson().GetFirstError());
                }

                return Ok(rr);
            }
            catch (Exception e )
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("获取调入存储地点列表失败:" +e.Message);
                return Ok(rr);
            }
        }

        #endregion
        #region 刀具移库 同存储地点的调入调出 一次性完成
        [ActionDescription("[PDA] 刀具移库")]
        [HttpPost("KnifeMove")]
        public ActionResult KnifeMove(KnifeMoveInputInfo info)
        {
            //调出然后调入
            ReturnResult<string> rr = new ReturnResult<string>();
            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeVM vm_knife = Wtm.CreateVM<KnifeVM>();
                KnifeTransferOutVM vm_transferOut = Wtm.CreateVM<KnifeTransferOutVM>();
                KnifeTransferInVM vm_transferIn = Wtm.CreateVM<KnifeTransferInVM>();
                vm_knife.KnifeMoveInitByInfo(info, vm_transferOut, vm_transferIn, tran);//刀具移库初始化 单号需要事务
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"刀具移库失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm_transferOut.DoAdd();
                vm_transferIn.DoAdd();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"刀具移库失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                vm_transferOut.DoApproved();
                vm_transferIn.DoApproved();
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"刀具移库失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess( $"移库成功");
                return Ok(rr);
            }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("刀具移库失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }



        #endregion

        #region 配件替换
        [ActionDescription("[PDA] 组合刀具配件替换")]
        [HttpPost("KnifeReplace")]
        public ActionResult KnifeReplace(KnifeReplaceInputInfo info)
        {
            //配件替换操作:组合归还+组合领用 结束
            ReturnResult<List<BlueToothPrintDataLineReturn>> rr = new ReturnResult<List<BlueToothPrintDataLineReturn>>();
            rr.Entity = new List<BlueToothPrintDataLineReturn>();

            var tran = DC.Database.BeginTransaction();
            try
            {
                KnifeVM vm_knife = Wtm.CreateVM<KnifeVM>();
                KnifeCheckOutVM vm_checkOut = Wtm.CreateVM<KnifeCheckOutVM>();
                KnifeCheckInVM vm_checkIn = Wtm.CreateVM<KnifeCheckInVM>();
                vm_knife.KnifeReplaceInitByInfo(info, vm_checkOut,vm_checkIn, tran,rr);//组合刀具配件替换初始化 单号需要事务
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"组合配件替换失败:{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                if (vm_checkIn.Entity.KnifeCheckInLine_KnifeCheckIn!=null&& vm_checkIn.Entity.KnifeCheckInLine_KnifeCheckIn.Count != 0)
                {
                    vm_checkIn.DoAdd();
                }
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"组合配件替换失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                if (vm_checkOut.Entity.KnifeCheckOutLine_KnifeCheckOut!=null&& vm_checkOut.Entity.KnifeCheckOutLine_KnifeCheckOut.Count != 0)
                {
                    vm_checkOut.DoAdd();
                }
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"组合配件替换失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                if (vm_checkIn.Entity.KnifeCheckInLine_KnifeCheckIn != null && vm_checkIn.Entity.KnifeCheckInLine_KnifeCheckIn.Count != 0)
                {
                    vm_checkIn.DoApproved(tran);
                }
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"组合配件替换失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                if (vm_checkOut.Entity.KnifeCheckOutLine_KnifeCheckOut != null && vm_checkOut.Entity.KnifeCheckOutLine_KnifeCheckOut.Count != 0)
                {
                    vm_checkOut.DoApproved(tran, rr);
                }
                if (!ModelState.IsValid)
                {
                    tran.Rollback();
                    rr.Entity = null;
                    rr.SetFail($"组合配件替换失败{ModelState.GetErrorJson().GetFirstError()}");
                    return Ok(rr);

                }
                DC.SaveChanges();
                tran.Commit();
                rr.SetSuccess($"组合配件替换成功") ;
                return Ok(rr);
            }
            catch
            {
                tran.Rollback();
                rr.Entity = null;
                rr.SetFail("组合配件替换失败:" + ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }
        #endregion
        #endregion


        #region 刀具其他接口
        [ActionDescription("查询库存/刀具在库数量")]
        [HttpPost("GetKnifeAndInventoryInStockQty")]
        [Public]
        public ActionResult GetKnifeAndInventoryInStockQty(GetKnifeAndInventoryInStockQtyInputInfo input)
        {
            ReturnResult<List<GetKnifeAndInventoryInStockQtyReturn>> rr = new ReturnResult<List<GetKnifeAndInventoryInStockQtyReturn>>();
            try
            {
                var knifeVM = Wtm.CreateVM<KnifeVM>();
                rr.Entity = knifeVM.GetKnifeAndInventoryInStockQty(input);
                if (!ModelState.IsValid)
                {
                    rr.Entity = null;
                    rr.SetFail("查询失败:" + ModelState.GetErrorJson().GetFirstError());
                }
                return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            }
            catch (Exception e)
            {
                rr.Entity = null;
                rr.SetFail("捕获到异常:" + e.Message);
                return Ok(rr);
            }
        }
        #endregion



    }
}
