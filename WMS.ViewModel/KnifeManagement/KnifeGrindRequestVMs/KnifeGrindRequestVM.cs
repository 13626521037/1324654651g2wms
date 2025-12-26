using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.Model.KnifeManagement;
using WMS.Model;
using Microsoft.EntityFrameworkCore.Storage;
using Elsa;
using WMS.Model.BaseData;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.ViewModel.KnifeManagement.KnifeCombineVMs;
using WMS.ViewModel.KnifeManagement.KnifeScrapVMs;
using WMS.Util.U9Para.Knife;
using WMS.Util;
using System.Xml.Linq;
using WMS.ViewModel.KnifeManagement.KnifeGrindRequestLineVMs;
using WMS.ViewModel.BaseData.BaseSysParaVMs;
namespace WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs
{
    public partial class KnifeGrindRequestVM : BaseCRUDVM<KnifeGrindRequest>
    {

        public List<string> KnifeManagementKnifeGrindRequestFTempSelected { get; set; }
        public KnifeGrindRequestLineKnifeGrindRequestDetailListVM KnifeGrindRequestLineKnifeGrindRequestList { get; set; }
        public KnifeGrindRequestLineKnifeGrindRequestDetailListVM1 KnifeGrindRequestLineKnifeGrindRequestList1 { get; set; }
        public KnifeGrindRequestLineKnifeGrindRequestDetailListVM2 KnifeGrindRequestLineKnifeGrindRequestList2 { get; set; }
        public KnifeGrindRequestVM()
        {
            SetInclude(x => x.HandledBy);
            KnifeGrindRequestLineKnifeGrindRequestList = new KnifeGrindRequestLineKnifeGrindRequestDetailListVM();
            KnifeGrindRequestLineKnifeGrindRequestList.DetailGridPrix = "Entity.KnifeGrindRequestLine_KnifeGrindRequest";
            KnifeGrindRequestLineKnifeGrindRequestList1 = new KnifeGrindRequestLineKnifeGrindRequestDetailListVM1();
            KnifeGrindRequestLineKnifeGrindRequestList1.DetailGridPrix = "Entity.KnifeGrindRequestLine_KnifeGrindRequest";
            KnifeGrindRequestLineKnifeGrindRequestList2 = new KnifeGrindRequestLineKnifeGrindRequestDetailListVM2();
            KnifeGrindRequestLineKnifeGrindRequestList2.DetailGridPrix = "Entity.KnifeGrindRequestLine_KnifeGrindRequest";
        }

        protected override void InitVM()
        {
            KnifeGrindRequestLineKnifeGrindRequestList.CopyContext(this);
            KnifeGrindRequestLineKnifeGrindRequestList1.CopyContext(this);
            KnifeGrindRequestLineKnifeGrindRequestList2.CopyContext(this);
        }

        public override DuplicatedInfo<KnifeGrindRequest> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.DocNo));
            return rv;

        }

        public override void Validate()
        {
            if (Entity.KnifeGrindRequestLine_KnifeGrindRequest.GroupBy(x => new { x.KnifeGrindRequestId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }
            if (Entity.KnifeGrindRequestLine_KnifeGrindRequest.Count == 0)
            {
                MSD.AddModelError("", "刀具列表为空");
                return;
            }
            base.Validate();
        }
        public override void DoAdd()
        {
            //新增归还单的时候 固定值写死
            //单号可以考虑在这里 
            //状态写死 新建就是开立 到也没必要 都在外面过一遍 不然有漏的不好找 doadd就不加默认值了
            //Entity.Status = KnifeOrderStatusEnum.Open;
            if (Entity.KnifeGrindRequestLine_KnifeGrindRequest.Count == 0)
            {
                MSD.AddModelError("", "请输入刀具");
                return;
            }
            if (Entity.KnifeGrindRequestLine_KnifeGrindRequest.GroupBy(x => new { x.KnifeGrindRequestId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }
            base.DoAdd();
        }

        public override async Task DoEditAsync(bool updateAllFields = false)
        {
            if (Entity.Status != KnifeOrderStatusEnum.Open)
            {
                MSD.AddModelError("", "只有开立状态的单据可以修改");
                return;
            }
            await base.DoEditAsync();

        }

        public override async Task DoDeleteAsync()
        {
            if (Entity.Status != KnifeOrderStatusEnum.Open)
            {
                MSD.AddModelError("", "只有开立状态的单据可以删除");
                return;
            }
            await base.DoDeleteAsync();
        }
        public override void DoEdit(bool updateAllFields = false)
        {
            if (Entity.Status != KnifeOrderStatusEnum.Open)
            {
                MSD.AddModelError("", "只有开立状态的单据可以修改");
                return;
            }
            base.DoEdit();

        }

        public override void DoDelete()
        {
            if (Entity.Status != KnifeOrderStatusEnum.Open)
            {
                MSD.AddModelError("", "只有开立状态的单据可以删除");
                return;
            }
            base.DoDelete();

        }
        /// <summary>
        /// 修磨申请单预处理
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        public void DoInitByInfo(KnifeGrindRequestInputInfo info, IDbContextTransaction tran, ReturnResult<List<BlueToothPrintDataLineReturn>> rr)
        {
            var currentVersion = DC.Set<BaseSysPara>().FirstOrDefault(x => x.Code == "KnifeGrindCodeVersion");
            if (currentVersion == null)
            {
                MSD.AddModelError("", "未检测到刀具修磨代码版本 请联系管理员");
                return;
            }
            if (currentVersion.Value == "0")//原来的
            {
                try
                {
                    if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                    {
                        MSD.AddModelError("", "登录信息已过期，请重新登录");
                        return;
                    }
                    Guid whid;//当前pda持有者(仓管员)的存储地点
                    Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                    var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                    var KnifeGrindRequestDocNo = DocNoVm.GetSequence("KnifeGrindRequestRule", tran);//生成归还单单号
                    var currentTime = DateTime.Now;//当前时间
                    var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                                                                                                                  //info 非空校验
                    if (info is null || info.Lines.Count == 0)
                    {
                        MSD.AddModelError("", "刀具列表为空 请检查");
                        return;
                    }
                    //刀具有效性校验
                    var knifeIds = info.Lines.Select(x => x.KnifeId).ToList();
                    var knifes = DC.Set<Knife>()
                    .Include(x => x.WhLocation)
                        .ThenInclude(x => x.WhArea)
                            .ThenInclude(x => x.WareHouse)
                        .CheckIDs(knifeIds).ToList();
                    foreach (var k in knifes)
                    {
                        if (k.WhLocation.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}不属于当前存储地点 不可修磨申请");
                            return;
                        }
                        if (k.Status != KnifeStatusEnum.InStock)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}不在库 无法修磨申请");
                            return;
                        }
                        if (k.WhLocation.Locked == true)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}所在的库位{k.WhLocation.Code}已锁定 不可用");
                            return;
                        }
                        if (k.WhLocation.IsEffective == EffectiveEnum.Ineffective || k.WhLocation.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"刀具{k.SerialNumber}所在库位{k.WhLocation.Code}无效");
                            return;
                        }
                    }
                    //校验通过 进行vm的初始化
                    Entity.DocNo = KnifeGrindRequestDocNo;
                    Entity.HandledBy = handledBy;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.Status = KnifeOrderStatusEnum.Open;
                    Entity.WareHouseId = whid;
                    Entity.KnifeGrindRequestLine_KnifeGrindRequest = new List<KnifeGrindRequestLine>();
                    foreach (var knife in knifes)
                    {
                        Entity.KnifeGrindRequestLine_KnifeGrindRequest.Add(new KnifeGrindRequestLine
                        {
                            KnifeId = knife.ID,
                            Status = KnifeOrderStatusEnum.Open,
                            FromWhLocationId = knife.WhLocationId,

                        });
                    }

                    #region 关闭涉及的组合单
                    var toCloseCombineLines = DC.Set<KnifeCombineLine>()
                        .Include(x => x.KnifeCombine)
                        .Where(x => x.KnifeCombine.Status == KnifeOrderStatusEnum.Approved)//组合单是已核准状态
                        .Where(x => knifes.Contains(x.Knife))//刀id和组合行的刀id匹配 
                        .ToList();
                    if (toCloseCombineLines != null) //涉及的组合单不一定有 有的话关闭 
                    {
                        var toCloseCombines = toCloseCombineLines.Select(x => x.KnifeCombine).Distinct().ToList();
                        foreach (var toCloseCombine in toCloseCombines)
                        {
                            toCloseCombine.Status = KnifeOrderStatusEnum.ApproveClose;
                            toCloseCombine.CloseTime = currentTime;
                        }
                    }
                    #endregion
                    DC.SaveChanges();
                }
                catch (Exception e)
                {
                    MSD.AddModelError("", " " + e.Message);
                    return;
                }
            }
            else if (currentVersion.Value == "1")//林琪伦的增加组合刀号的刀具修磨
            {
                try
                {
                    if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                    {
                        MSD.AddModelError("", "登录信息已过期，请重新登录");
                        return;
                    }
                    Guid whid;//当前pda持有者(仓管员)的存储地点
                    Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                    var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                    var KnifeGrindRequestDocNo = DocNoVm.GetSequence("KnifeGrindRequestRule", tran);//生成归还单单号
                    var currentTime = DateTime.Now;//当前时间
                    var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人

                    // 校验：允许组合条码与 Lines 同时传入
                    // 业务场景：
                    // 1. 只传 CombineCode：使用组合单中的所有刀具
                    // 2. 只传 Lines：使用指定的刀具列表
                    // 3. 同时传 CombineCode 和 Lines：使用组合单中的刀具，但根据Lines更新每把刀的修磨刀具号
                    // 注：删除了原有的“不允许混用”校验

                    // 识别并关联已有的组合单（如果传入了组合条码）
                    KnifeCombine existingCombine = null;
                    List<Knife> knifes = new List<Knife>();

                    if (!string.IsNullOrEmpty(info.CombineCode))
                    {
                        // 根据组合条码查找组合单（只查找已审核状态的，不包括已关闭的）
                        existingCombine = DC.Set<KnifeCombine>()
                            .Include(x => x.KnifeCombineLine_KnifeCombine)
                                .ThenInclude(x => x.Knife)
                                    .ThenInclude(x => x.WhLocation)
                                        .ThenInclude(x => x.WhArea)
                                            .ThenInclude(x => x.WareHouse)
                            .Where(x => x.DocNo == info.CombineCode || x.CombineKnifeNo == info.CombineCode)
                            .Where(x => x.Status == KnifeOrderStatusEnum.Approved)  // 只查找已审核状态的
                            .FirstOrDefault();

                        if (existingCombine == null)
                        {
                            MSD.AddModelError("", $"未找到可用的组合单: {info.CombineCode}（组合单可能已关闭或不存在）");
                            return;
                        }

                        // 使用组合单中的所有刀具
                        knifes = existingCombine.KnifeCombineLine_KnifeCombine
                            .Where(x => x.Knife != null)
                            .Select(x => x.Knife)
                            .ToList();

                        if (knifes.Count == 0)
                        {
                            MSD.AddModelError("", $"组合单 {info.CombineCode} 中没有有效的刀具");
                            return;
                        }

                        // 如果传入了Lines，则根据Lines中的数据更新特定刀具的修磨刀具号
                        // 允许传入空值来清空修磨刀具号
                        if (info.Lines != null && info.Lines.Count > 0)
                        {
                            foreach (var knife in knifes)
                            {
                                var inputLine = info.Lines.FirstOrDefault(x =>
                                    !string.IsNullOrEmpty(x.KnifeId) &&
                                    Guid.TryParse(x.KnifeId, out var knifeId) &&
                                    knifeId == knife.ID);

                                // 如果找到对应的行，则更新修磨刀具号（包括设置为空值）
                                if (inputLine != null)
                                {
                                    knife.GrindKnifeNO = string.IsNullOrWhiteSpace(inputLine.GrindKnifeNO)
                                        ? null
                                        : inputLine.GrindKnifeNO.Trim();
                                    knife.UpdateBy = LoginUserInfo.Name;
                                    knife.UpdateTime = currentTime;
                                }
                            }
                        }
                    }
                    else
                    {
                        // 没有传入组合条码，使用 Lines 中的刀具 ID
                        //info 非空校验
                        if (info is null || info.Lines == null || info.Lines.Count == 0)
                        {
                            MSD.AddModelError("", "刀具列表为空 请检查");
                            return;
                        }
                        //刀具有效性校验
                        var knifeIds = info.Lines.Select(x => x.KnifeId).ToList();
                        knifes = DC.Set<Knife>()
                            .Include(x => x.WhLocation)
                                .ThenInclude(x => x.WhArea)
                                    .ThenInclude(x => x.WareHouse)
                            .CheckIDs(knifeIds).ToList();

                        // 处理单独刀具的修磨刀具号
                        // 允许传入空值来清空修磨刀具号
                        foreach (var knife in knifes)
                        {
                            var inputLine = info.Lines.FirstOrDefault(x =>
                                !string.IsNullOrEmpty(x.KnifeId) &&
                                Guid.TryParse(x.KnifeId, out var knifeId) &&
                                knifeId == knife.ID);

                            // 如果找到对应的行，则更新修磨刀具号（包括设置为空值）
                            if (inputLine != null)
                            {
                                knife.GrindKnifeNO = string.IsNullOrWhiteSpace(inputLine.GrindKnifeNO)
                                    ? null
                                    : inputLine.GrindKnifeNO.Trim();
                                knife.UpdateBy = LoginUserInfo.Name;
                                knife.UpdateTime = currentTime;
                            }
                        }
                    }

                    // 刀具有效性校验（无论是从组合单还是 Lines 获取）
                    foreach (var k in knifes)
                    {
                        if (k.WhLocation.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}不属于当前存储地点 不可修磨申请");
                            return;
                        }
                        if (k.Status != KnifeStatusEnum.InStock)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}不在库 无法修磨申请");
                            return;
                        }
                        if (k.WhLocation.Locked == true)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}所在的库位{k.WhLocation.Code}已锁定 不可用");
                            return;
                        }
                        if (k.WhLocation.IsEffective == EffectiveEnum.Ineffective || k.WhLocation.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"刀具{k.SerialNumber}所在库位{k.WhLocation.Code}无效");
                            return;
                        }
                        if (k.WhLocation.IsEffective == EffectiveEnum.Ineffective || k.WhLocation.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"库存信息{k.SerialNumber}所在库位{k.WhLocation.Code}不可用");
                            return;
                        }
                    }

                    // 检查刀具是否已属于组合单（仅在未传入组合条码时）
                    if (string.IsNullOrEmpty(info.CombineCode))
                    {
                        var knifeIdsForCheck = knifes.Select(k => k.ID).ToList();
                        var existingCombineLines = DC.Set<KnifeCombineLine>()
                            .Include(x => x.KnifeCombine)
                            .Where(x => x.KnifeCombine != null)
                            .Where(x => x.KnifeId != null && knifeIdsForCheck.Contains(x.KnifeId.Value))
                            .ToList();

                        if (existingCombineLines.Count > 0)
                        {
                            // 如果刀具已属于组合单，关闭所有涉及的旧组合单
                            var oldCombines = existingCombineLines.Select(x => x.KnifeCombine).Distinct().ToList();
                            foreach (var oldCombine in oldCombines)
                            {
                                // 关闭旧组合单
                                if (oldCombine.Status != KnifeOrderStatusEnum.ApproveClose)
                                {
                                    oldCombine.Status = KnifeOrderStatusEnum.ApproveClose;
                                    oldCombine.CloseTime = currentTime;
                                }
                            }
                        }
                    }
                    //校验通过 进行vm的初始化
                    Entity.DocNo = KnifeGrindRequestDocNo;
                    Entity.HandledBy = handledBy;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.Status = KnifeOrderStatusEnum.Open;
                    Entity.WareHouseId = whid;

                    // 如果有已存在的组合单，直接关联
                    if (existingCombine != null)
                    {
                        Entity.KnifeCombineId = existingCombine.ID;
                        Entity.KnifeCombine = existingCombine;
                    }

                    Entity.KnifeGrindRequestLine_KnifeGrindRequest = new List<KnifeGrindRequestLine>();
                    foreach (var knife in knifes)
                    {
                        Entity.KnifeGrindRequestLine_KnifeGrindRequest.Add(new KnifeGrindRequestLine
                        {
                            KnifeId = knife.ID,
                            Status = KnifeOrderStatusEnum.Open,
                            FromWhLocationId = knife.WhLocationId,

                        });
                    }


                    
                    DC.SaveChanges();
                }
                catch (Exception e)
                {
                    MSD.AddModelError("", " " + e.Message);
                }
            }

        }
        /// <summary>
        /// 刀具修磨申请单审核
        /// </summary>
        /// <param name="tran"></param>
        public void DoApproved(KnifeGrindRequestInputInfo info,IDbContextTransaction tran, ReturnResult<List<BlueToothPrintDataLineReturn>> rr)
        {
            var currentVersion = DC.Set<BaseSysPara>().FirstOrDefault(x => x.Code == "KnifeGrindCodeVersion");
            if (currentVersion == null)
            {
                MSD.AddModelError("", "未检测到刀具修磨代码版本 请联系管理员");
                return;
            }
            if (currentVersion.Value == "0")//原来的
            {
                try
                {
                    //校验的必要性?请求从控制器进入 业务操作先初始化vm  初始化的时候已经校验了 到这一步有可能被修改? 没有吧?  结论:审核不做校验


                    // 重新加载 Entity 
                    Entity = DC.Set<KnifeGrindRequest>()
                        .Include(x => x.KnifeGrindRequestLine_KnifeGrindRequest)
                            .ThenInclude(line => line.Knife)
                                .ThenInclude(k => k.ItemMaster)
                        .FirstOrDefault(x => x.ID == Entity.ID);
                    var currentTime = DateTime.Now;
                    var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                    if (handledBy is null)
                    {
                        MSD.AddModelError("", $"未检测到有效的登录人");
                        return;
                    }
                    var handledBy_operator = DC.Set<BaseOperator>()
                       .Include(x => x.Department)
                           .ThenInclude(x => x.Organization)
                           .Where(x => x.IsValid == true)
                   .FirstOrDefault(x => x.Code.ToString() == LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");//领用人_业务员类型 必定属于刀具中心 修磨请购要在营销中心 2025-12-17  需求人和需求部门和需求组织要在刀具中心 为了收货
                    if (handledBy_operator is null)
                    {
                        MSD.AddModelError("", $"仓管员的U9的台邦电机工业集团有限中业务员未配置 操作失败");
                        return;
                    }
                    var knifes = Entity.KnifeGrindRequestLine_KnifeGrindRequest.Select(x => x.Knife).ToList();
                    var knifes_actualItem = new List<(Knife knife, BaseItemMaster itemMaster)>();
                    foreach (var k in knifes)
                    {
                        BaseItemMaster i = k.ItemMaster;
                        if (k.ActualItemCode != k.ItemMaster.Code)
                        {
                            i = DC.Set<BaseItemMaster>()
                                .Where(x => x.Code == k.ActualItemCode && x.Organization.Code == "0200")
                                .FirstOrDefault();
                            if (i == null)
                            {
                                MSD.AddModelError("", $"{k.SerialNumber}的实际料号{k.ActualItemCode}在台邦电机工业集团有限公司[0200]中不存在 无法生成请购单");
                                return;
                            }
                        }
                        knifes_actualItem.Add((k, i));
                    }
                    //审核修改对象:   修磨申请  刀具的详细信息  操作记录-修磨申请(U9请购)
                    //修磨申请单变化 状态 审核时间 经办人  修改时间 修改人 
                    Entity.Status = KnifeOrderStatusEnum.Approved;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.HandledBy = handledBy;
                    Entity.ApprovedTime = currentTime;
                    Entity.UpdateBy = LoginUserInfo.Name;
                    Entity.UpdateTime = currentTime;
                    //U9 创建并审核请购单
                    string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                    string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                    U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0200", LoginUserInfo.ITCode);//刀具修磨申请 固定组织 台邦电机工业集团
                    U9Return<PRInfoReturn> u9Return = apiHelper.CreateAndApprovedPR(handledBy_operator, knifes_actualItem, Entity.DocNo,info.Memo);
                    if (!u9Return.Success)
                    {
                        MSD.AddModelError("", "U9创建并审核请购单失败:" + u9Return.Msg);
                        return;
                    }
                    Entity.U9PRDocNo = u9Return.Entity.DocNo;//修磨申请要加上u9单号
                    foreach (var line in Entity.KnifeGrindRequestLine_KnifeGrindRequest)
                    {
                        line.Status = KnifeOrderStatusEnum.Approved;
                        var knife = line.Knife;
                        //修改 刀具
                        knife.Status = KnifeStatusEnum.GrindRequested;
                        knife.HandledById = Entity.HandledById;
                        knife.HandledBy = Entity.HandledBy;
                        knife.HandledByName = Entity.HandledBy.Name;

                        knife.LastOperationDate = currentTime;
                        knife.UpdateBy = LoginUserInfo.Name;
                        knife.UpdateTime = currentTime;
                        //新增 操作记录-归还
                        DC.Set<KnifeOperation>().Add(new KnifeOperation
                        {
                            KnifeId = knife.ID,
                            DocNo = Entity.DocNo,
                            OperationType = KnifeOperationTypeEnum.GrindRequest,
                            OperationTime = currentTime,
                            OperationBy = handledBy_operator,
                            OperationById = handledBy_operator.ID,
                            HandledBy = Entity.HandledBy,
                            HandledById = Entity.HandledById,
                            HandledByName = Entity.HandledBy.Name,

                            UsedDays = 0,
                            TotalUsedDays = line.Knife.TotalUsedDays,
                            RemainingDays = line.Knife.RemainingUsedDays,
                            CurrentLife = line.Knife.CurrentLife,
                            WhLocationId = line.Knife.WhLocationId,
                            WhLocation = line.Knife.WhLocation,
                            GrindNum = line.Knife.GrindCount,

                            U9SourceLineID = u9Return.Entity.Lines.Where(x => x.KnifeNO == knife.SerialNumber).Select(x => x.PrlineId).First().ToString(),
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime,
                        });
                    }



                    DC.SaveChanges();
                    if (!MSD.IsValid)
                    {
                        MSD.AddModelError("", "校验未通过:" + MSD.GetFirstError());
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MSD.AddModelError("", $"捕获异常: {ex.Message} ");
                    return;
                }

            }
            else if (currentVersion.Value == "1")//林琪伦的增加组合刀号的刀具修磨
            {
                try
                {
                    //校验的必要性?请求从控制器进入 业务操作先初始化vm  初始化的时候已经校验了 到这一步有可能被修改? 没有吧?  结论:审核不做校验


                    // 重新加载 Entity 
                    Entity = DC.Set<KnifeGrindRequest>()
                        .Include(x => x.KnifeCombine) // 【2025-12-18】包含组合单信息
                        .Include(x => x.KnifeGrindRequestLine_KnifeGrindRequest)
                            .ThenInclude(line => line.Knife)
                                .ThenInclude(k => k.ItemMaster)
                        .Include(x => x.KnifeGrindRequestLine_KnifeGrindRequest)
                            .ThenInclude(line => line.Knife)
                                .ThenInclude(k => k.WhLocation) // 包含库位信息
                        .FirstOrDefault(x => x.ID == Entity.ID);

                    if (Entity == null)
                    {
                        MSD.AddModelError("", "修磨申请单未找到");
                        return;
                    }
                    var currentTime = DateTime.Now;
                    var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                    if (handledBy is null)
                    {
                        MSD.AddModelError("", $"未检测到有效的登录人");
                        return;
                    }
                    var handledBy_operator = DC.Set<BaseOperator>()
                       .Include(x => x.Department)
                           .ThenInclude(x => x.Organization)
                           .Where(x => x.IsValid == true)
                       .FirstOrDefault(x => x.Code.ToString() == LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");//领用人_业务员类型 必定属于刀具中心 修磨请购要在营销中心
                    if (handledBy_operator is null)
                    {
                        MSD.AddModelError("", $"仓管员的U9的刀具中心业务员未配置 操作失败");
                        return;
                    }
                    var knifes = Entity.KnifeGrindRequestLine_KnifeGrindRequest.Select(x => x.Knife).ToList();
                    var knifes_actualItem = new List<(Knife knife, BaseItemMaster itemMaster)>();
                    foreach (var k in knifes)
                    {
                        BaseItemMaster i = k.ItemMaster;
                        if (k.ActualItemCode != k.ItemMaster.Code)
                        {
                            i = DC.Set<BaseItemMaster>()
                                .Where(x => x.Code == k.ActualItemCode && x.Organization.Code == "0200")
                                .FirstOrDefault();
                            if (i == null)
                            {
                                MSD.AddModelError("", $"{k.SerialNumber}的实际料号{k.ActualItemCode}在台邦电机工业集团有限公司[0200]中不存在 无法生成请购单");
                                return;
                            }
                        }
                        knifes_actualItem.Add((k, i));
                    }
                    //审核修改对象:   修磨申请  刀具的详细信息  操作记录-修磨申请(U9请购)
                    //修磨申请单变化 状态 审核时间 经办人  修改时间 修改人 
                    Entity.Status = KnifeOrderStatusEnum.Approved;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.HandledBy = handledBy;
                    Entity.ApprovedTime = currentTime;
                    Entity.UpdateBy = LoginUserInfo.Name;
                    Entity.UpdateTime = currentTime;
                    //U9 创建并审核请购单
                    string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                    string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                    U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0200", LoginUserInfo.ITCode);//刀具修磨申请 固定组织 台邦电机工业集团
                    U9Return<PRInfoReturn> u9Return = apiHelper.CreateAndApprovedPR(handledBy_operator, knifes_actualItem, Entity.DocNo,info.Memo);
                    if (!u9Return.Success)
                    {
                        MSD.AddModelError("", "U9创建并审核请购单失败:" + u9Return.Msg);
                        return;
                    }
                    // 处理组合单（当刀具数量大于1且未关联已有组合单时）
                    if (Entity.KnifeGrindRequestLine_KnifeGrindRequest.Count > 1 && Entity.KnifeCombineId == null)
                    {
                        var knifeIdsForCombine = knifes.Select(k => k.ID).ToList();

                        // 检查所有刀具是否来自同一个已审核的组合单
                        var existingCombineLines = DC.Set<KnifeCombineLine>()
                            .Include(x => x.KnifeCombine)
                            .Where(x => x.KnifeCombine != null && x.KnifeCombine.Status == KnifeOrderStatusEnum.Approved)
                            .Where(x => x.KnifeId != null && knifeIdsForCombine.Contains(x.KnifeId.Value))
                            .ToList();

                        // 按组合单分组，检查是否所有刀具都属于同一个组合单
                        var combineCounts = existingCombineLines
                            .GroupBy(x => x.KnifeCombineId)
                            .Select(g => new { CombineId = g.Key, Count = g.Count() })
                            .ToList();

                        // 如果找到一个组合单包含所有刀具，则直接关联该组合单
                        var matchingCombine = combineCounts.FirstOrDefault(x => x.Count == knifeIdsForCombine.Count);

                        if (matchingCombine != null)
                        {
                            // 所有刀具都来自同一个组合单，直接关联
                            var existingCombine = existingCombineLines
                                .First(x => x.KnifeCombineId == matchingCombine.CombineId)
                                .KnifeCombine;

                            Entity.KnifeCombine = existingCombine;
                            Entity.KnifeCombineId = existingCombine.ID;
                        }
                        else
                        {
                            // 刀具来自不同组合单或没有组合单，需要生成新的组合单
                            // 先关闭刀具关联的所有旧组合单
                            if (existingCombineLines != null && existingCombineLines.Count > 0)
                            {
                                var oldCombines = existingCombineLines.Select(x => x.KnifeCombine).Distinct().ToList();
                                foreach (var oldCombine in oldCombines)
                                {
                                    oldCombine.Status = KnifeOrderStatusEnum.ApproveClose;
                                    oldCombine.CloseTime = currentTime;
                                }
                            }

                            // 创建新的组合单
                            var baseSequenceDefineVM = Wtm.CreateVM<BaseSequenceDefineVM>();
                            var combineKnifeNo = baseSequenceDefineVM.SetProperty("ItemCategory", "ZHDH").GetSequence("KnifeNoRule", tran);
                            var knifeCombineVM = Wtm.CreateVM<KnifeCombineVM>();
                            knifeCombineVM.Entity = new KnifeCombine
                            {
                                DocNo = combineKnifeNo, // 使用组合刀号作为单据号，不再单独生成
                                HandledBy = Entity.HandledBy,
                                HandledById = Entity.HandledById,
                                Status = KnifeOrderStatusEnum.Approved,
                                ApprovedTime = currentTime,
                                CombineKnifeNo = combineKnifeNo,
                                WareHouseId = Entity.WareHouseId,
                                KnifeCombineLine_KnifeCombine = new List<KnifeCombineLine>(),
                            };

                            // 添加组合单明细
                            foreach (var line in Entity.KnifeGrindRequestLine_KnifeGrindRequest)
                            {
                                knifeCombineVM.Entity.KnifeCombineLine_KnifeCombine.Add(new KnifeCombineLine
                                {
                                    KnifeId = line.KnifeId,
                                    FromWhLocationId = line.FromWhLocationId,
                                });
                            }

                            // 保存组合单
                            knifeCombineVM.DoAdd();
                            if (!knifeCombineVM.MSD.IsValid)
                            {
                                MSD.AddModelError("", "生成组合单失败: " + knifeCombineVM.MSD.GetFirstError());
                                return;
                            }

                            // 关联组合单到修磨申请
                            Entity.KnifeCombine = knifeCombineVM.Entity;
                            Entity.KnifeCombineId = knifeCombineVM.Entity.ID;
                        }
                    }
                    Entity.U9PRDocNo = u9Return.Entity.DocNo;//修磨申请要加上u9单号

                    // 说明：采购单号由U9系统通过回写接口(UpdateKnifeGrindRequestByU9)异步写入
                    // 此处不再生成虚拟采购单号，等待U9采购单审核后回写

                    foreach (var line in Entity.KnifeGrindRequestLine_KnifeGrindRequest)
                    {
                        // 设置申请行状态为已审核
                        line.Status = KnifeOrderStatusEnum.Approved;

                        var knife = line.Knife;
                        //修改 刀具
                        knife.Status = KnifeStatusEnum.GrindRequested;
                        knife.HandledById = Entity.HandledById;
                        knife.HandledBy = Entity.HandledBy;
                        knife.HandledByName = Entity.HandledBy.Name;

                        knife.LastOperationDate = currentTime;
                        knife.UpdateBy = LoginUserInfo.Name;
                        knife.UpdateTime = currentTime;
                        //新增 操作记录-归还
                        DC.Set<KnifeOperation>().Add(new KnifeOperation
                        {
                            KnifeId = knife.ID,
                            DocNo = Entity.DocNo,
                            OperationType = KnifeOperationTypeEnum.GrindRequest,
                            OperationTime = currentTime,
                            OperationBy = handledBy_operator,
                            OperationById = handledBy_operator.ID,
                            HandledBy = Entity.HandledBy,
                            HandledById = Entity.HandledById,
                            HandledByName = Entity.HandledBy.Name,

                            UsedDays = 0,
                            TotalUsedDays = line.Knife.TotalUsedDays,
                            RemainingDays = line.Knife.RemainingUsedDays,
                            CurrentLife = line.Knife.CurrentLife,
                            WhLocationId = line.Knife.WhLocationId,
                            WhLocation = line.Knife.WhLocation,
                            GrindNum = line.Knife.GrindCount,

                            U9SourceLineID = (u9Return.Entity.Lines?.Where(x => x.KnifeNO == knife.SerialNumber)?.Select(x => x.PrlineId)?.FirstOrDefault() ?? 0).ToString(),
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime,
                        });
                    }

                    //组合单的返回要在这里
                    int knifeCount = Entity?.KnifeGrindRequestLine_KnifeGrindRequest?.Count ?? 0;
                    // 【2025-12-18 新增】构建蓝牙打印数据格式（vm.Entity在DoApproved中已重新加载包含KnifeCombine）
                    //组合刀信息仅多把散刀有组合单信息  组合刀号或者单刀情况没有新的组合单  不返回打印信息
                    if (string.IsNullOrEmpty(info.CombineCode) && info.Lines != null && info.Lines.Count > 1)
                    {
                        var combineKnifeNo = Entity?.KnifeCombine?.CombineKnifeNo ?? "";
                        string barCode = combineKnifeNo;

                        var printData = new BlueToothPrintDataLineReturn
                        {
                            IsKnife = false,
                            IsNew = true,
                            SerialNumber = "", // 
                            bar = barCode, // 组合刀号或申请单号
                            ItemCode = "", // 
                            ItemName = "", // 
                            Specs = "",
                            Qty = knifeCount.ToString(), // 刀具数量
                            CurrentDate = currentTime.ToString("yyyy-MM-dd"),
                            NotG = "not",
                        };
                        rr.Entity = new List<BlueToothPrintDataLineReturn>();
                        rr.Entity.Add(printData);
                    }


                    // 保存所有修改
                    DC.SaveChanges();

                    if (!MSD.IsValid)
                    {
                        MSD.AddModelError("", "校验未通过:" + MSD.GetFirstError());
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MSD.AddModelError("", $"捕获异常: {ex.Message} ");
                    return;
                }
            }




        }
        /// <summary>
        /// U9采购单审核/弃审回写修磨申请单
        /// </summary>
        /// <param name="info"></param>
        public void UpdateKnifeGrindRequestByU9(List<UpdateKnifeGrindRequestByU9Input> info)
        {
            try
            {
                //校验
                if (info == null || info.Any(x => string.IsNullOrEmpty(x.KnifeSerialNumber)))
                {
                    MSD.AddModelError("", "刀具序列号不可存在空值");
                    return;
                }
                var KnifeSerialNumbers = info.Select(x => x.KnifeSerialNumber).ToList();
                var Knifes = DC.Set<Knife>()
                    .Where(x => KnifeSerialNumbers.Contains(x.SerialNumber))
                    .ToList();
                if (Knifes.Count != info.Count)
                {
                    MSD.AddModelError("", "存在无效的刀具系列号");
                    return;
                }
                var KnifeGrindRequestLines = DC.Set<KnifeGrindRequestLine>()
                    .Include(x => x.Knife)
                    .Include(x => x.KnifeGrindRequest)
                    .Where(x => x.Status == KnifeOrderStatusEnum.Approved)
                    .Where(x => KnifeSerialNumbers.Contains(x.Knife.SerialNumber))
                    .ToList();
                if (KnifeGrindRequestLines.Count != Knifes.Count)
                {
                    MSD.AddModelError("", "存在异常的修磨申请行");
                    return;
                }
                //修改
                foreach (var line in KnifeGrindRequestLines)
                {
                    var inputLine = info.FirstOrDefault(x => x.KnifeSerialNumber == line.Knife.SerialNumber);
                    line.U9PODocNo = inputLine.PODocNo;
                    line.U9PODocLineNo = inputLine.PODocLineNo;
                    line.KnifeGrindRequest.LastU9PODocNo = inputLine.PODocNo;
                }

            }
            catch (Exception ex)
            {
                MSD.AddModelError("", $"捕获异常: {ex.Message} ");
                return;
            }
        }
    }
}
