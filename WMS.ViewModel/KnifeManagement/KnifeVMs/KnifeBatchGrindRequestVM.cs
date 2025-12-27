using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.KnifeManagement;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.Util;
using WMS.Util.U9Para.Knife;
using WMS.ViewModel.KnifeManagement.KnifeCombineVMs;

namespace WMS.ViewModel.KnifeManagement.KnifeVMs
{
    /// <summary>
    /// 批量修磨申请ViewModel
    /// </summary>
    public partial class KnifeBatchGrindRequestVM : BaseBatchVM<Knife, Knife_BatchGrindRequest>
    {
        public KnifeBatchGrindRequestVM()
        {
            ListVM = new KnifeBatchGrindRequestListVM();
            LinkedVM = new Knife_BatchGrindRequest();
        }

        protected override void InitVM()
        {
            base.InitVM();
        }

        /// <summary>
        /// 批量修磨申请操作
        /// </summary>
        /// <returns>返回错误信息，成功时返回null</returns>
        public string DoBatchGrindRequest()
        {
            try
            {
                // 获取选中的刀具IDs
                if (Ids == null || Ids.Length == 0)
                {
                    return "请选择要申请修磨的刀具";
                }

                // 获取登录人信息
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);
                if (handledBy == null)
                {
                    return "登录人信息无效，请重新登录";
                }

                // 获取业务员信息
                var handledBy_operator = DC.Set<BaseOperator>()
                    .Include(x => x.Department)
                        .ThenInclude(x => x.Organization)
                    .Where(x => x.IsValid == true)
                    .FirstOrDefault(x => x.Code == LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");
                if (handledBy_operator == null)
                {
                    return "请先配置U9仓管员的业务员身份并同步到WMS";
                }

                var currentTime = DateTime.Now;
                var knifeIds = Ids.Select(x => Guid.Parse(x)).ToList();
                var knifes = DC.Set<Knife>()
                    .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                    .Include(x => x.ItemMaster)
                    .Where(x => knifeIds.Contains(x.ID))
                    .ToList();

                if (knifes == null || knifes.Count != Ids.Length)
                {
                    return "部分刀具不存在，请检查";
                }

                // 获取存储地点（从刀具所在库位获取）
                var firstKnife = knifes.First();
                var whid = firstKnife.WhLocation.WhArea.WareHouse.ID;

                // 校验刀具状态
                foreach (var knife in knifes)
                {
                    if (knife.Status != KnifeStatusEnum.InStock)
                    {
                        return $"刀具 {knife.SerialNumber} 的状态不是在库，无法申请修磨";
                    }
                    if (knife.WhLocation.Locked == true)
                    {
                        return $"刀具 {knife.SerialNumber} 所在的库位{knife.WhLocation.Code}已锁定，不可用";
                    }
                    if (knife.WhLocation.IsEffective == EffectiveEnum.Ineffective || knife.WhLocation.AreaType != WhLocationEnum.Normal)
                    {
                        return $"刀具 {knife.SerialNumber} 所在库位{knife.WhLocation.Code}无效";
                    }
                }

                // 生成修磨申请单号
                var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();
                var tran = DC.Database.BeginTransaction();
                try
                {
                    var KnifeGrindRequestDocNo = DocNoVm.GetSequence("KnifeGrindRequestRule", tran);

                    // 创建修磨申请单（直接设为已审核状态）
                    var knifeGrindRequest = new KnifeGrindRequest
                    {
                        DocNo = KnifeGrindRequestDocNo,
                        Status = KnifeOrderStatusEnum.Approved,
                        HandledBy = handledBy,
                        HandledById = handledBy.ID.ToString(),
                        WareHouseId = whid,
                        ApprovedTime = currentTime,
                        CreateBy = LoginUserInfo.Name,
                        CreateTime = currentTime,
                        UpdateBy = LoginUserInfo.Name,
                        UpdateTime = currentTime,
                        KnifeGrindRequestLine_KnifeGrindRequest = new List<KnifeGrindRequestLine>()
                    };

                    // 准备U9请购单数据（与单个修磨申请相同）
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
                                tran.Rollback();
                                return $"{k.SerialNumber}的实际料号{k.ActualItemCode}在台邦电机工业集团有限公司[0200]中不存在 无法生成请购单";
                            }
                        }
                        knifes_actualItem.Add((k, i));
                    }

                    // 创建U9请购单（与单个修磨申请相同）
                    string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                    string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                    U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0200", LoginUserInfo.ITCode);
                    U9Return<PRInfoReturn> u9Return = apiHelper.CreateAndApprovedPR(handledBy_operator, knifes_actualItem, KnifeGrindRequestDocNo, LinkedVM.Memo);
                    if (!u9Return.Success)
                    {
                        tran.Rollback();
                        return "U9创建并审核请购单失败:" + u9Return.Msg;
                    }
                    knifeGrindRequest.U9PRDocNo = u9Return.Entity.DocNo;

                    // 如果是多把刀具，生成组合单（与单个修磨申请相同）
                    if (knifes.Count > 1)
                    {
                        // 检查刀具是否已存在组合单
                        var existingCombineLines = DC.Set<KnifeCombineLine>()
                            .Include(x => x.KnifeCombine)
                            .Where(x => x.KnifeCombine != null)
                            .Where(x => x.KnifeId != null && knifeIds.Contains(x.KnifeId.Value))
                            .ToList();

                        // 检查是否所有刀具都来自同一个组合单
                        var matchingCombine = existingCombineLines
                            .GroupBy(x => x.KnifeCombineId)
                            .Where(g => g.Count() == knifes.Count)
                            .Select(g => new { CombineId = g.Key, Count = g.Count() })
                            .FirstOrDefault();

                        if (matchingCombine != null)
                        {
                            // 所有刀具都来自同一个组合单，直接关联
                            var existingCombine = existingCombineLines
                                .First(x => x.KnifeCombineId == matchingCombine.CombineId)
                                .KnifeCombine;

                            knifeGrindRequest.KnifeCombine = existingCombine;
                            knifeGrindRequest.KnifeCombineId = existingCombine.ID;
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

                            // 创建新的组合单（使用KnifeCombineVM保持与其他地方一致）
                            var baseSequenceDefineVM = Wtm.CreateVM<BaseSequenceDefineVM>();
                            var combineKnifeNo = baseSequenceDefineVM.SetProperty("ItemCategory", "ZHDH").GetSequence("KnifeNoRule", tran);
                            
                            var knifeCombineVM = Wtm.CreateVM<KnifeCombineVM>();
                            knifeCombineVM.Entity = new KnifeCombine
                            {
                                DocNo = combineKnifeNo,
                                HandledBy = handledBy,
                                HandledById = handledBy.ID.ToString(),
                                Status = KnifeOrderStatusEnum.Approved,
                                ApprovedTime = currentTime,
                                CombineKnifeNo = combineKnifeNo,
                                WareHouseId = whid,
                                KnifeCombineLine_KnifeCombine = new List<KnifeCombineLine>(),
                            };

                            // 添加组合单明细
                            foreach (var knife in knifes)
                            {
                                knifeCombineVM.Entity.KnifeCombineLine_KnifeCombine.Add(new KnifeCombineLine
                                {
                                    KnifeId = knife.ID,
                                    FromWhLocationId = knife.WhLocationId,
                                });
                            }

                            // 保存组合单
                            knifeCombineVM.DoAdd();
                            if (!knifeCombineVM.MSD.IsValid)
                            {
                                tran.Rollback();
                                return "生成组合单失败: " + knifeCombineVM.MSD.GetFirstError();
                            }

                            // 关联组合单到修磨申请
                            knifeGrindRequest.KnifeCombine = knifeCombineVM.Entity;
                            knifeGrindRequest.KnifeCombineId = knifeCombineVM.Entity.ID;
                        }
                    }

                    // 创建修磨申请行并更新刀具状态
                    foreach (var knife in knifes)
                    {
                        // 添加修磨申请行（已审核状态）
                        knifeGrindRequest.KnifeGrindRequestLine_KnifeGrindRequest.Add(new KnifeGrindRequestLine
                        {
                            KnifeId = knife.ID,
                            Status = KnifeOrderStatusEnum.Approved,
                            FromWhLocationId = knife.WhLocationId,
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime
                        });

                        // 直接更新刀具状态为已申请修磨
                        knife.Status = KnifeStatusEnum.GrindRequested;
                        knife.HandledById = handledBy.ID.ToString();
                        knife.HandledBy = handledBy;
                        knife.HandledByName = handledBy.Name;
                        knife.LastOperationDate = currentTime;
                        knife.UpdateBy = LoginUserInfo.Name;
                        knife.UpdateTime = currentTime;

                        // 新增操作记录（与单个修磨申请相同）
                        DC.Set<KnifeOperation>().Add(new KnifeOperation
                        {
                            KnifeId = knife.ID,
                            DocNo = KnifeGrindRequestDocNo,
                            OperationType = KnifeOperationTypeEnum.GrindRequest,
                            OperationTime = currentTime,
                            OperationBy = handledBy_operator,
                            OperationById = handledBy_operator.ID,
                            HandledBy = handledBy,
                            HandledById = handledBy.ID.ToString(),
                            HandledByName = handledBy.Name,
                            UsedDays = 0,
                            TotalUsedDays = knife.TotalUsedDays,
                            RemainingDays = knife.RemainingUsedDays,
                            CurrentLife = knife.CurrentLife,
                            WhLocationId = knife.WhLocationId,
                            GrindNum = knife.GrindCount,
                            U9SourceLineID = u9Return.Entity.Lines.Where(x => x.KnifeNO == knife.SerialNumber).Select(x => x.PrlineId).First().ToString(),
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime
                        });
                    }

                    DC.Set<KnifeGrindRequest>().Add(knifeGrindRequest);
                    DC.SaveChanges();
                    tran.Commit();
                    return null; // 成功
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return "创建修磨申请单失败：" + ex.Message;
                }
            }
            catch (Exception ex)
            {
                return "批量修磨申请操作失败：" + ex.Message;
            }
        }
    }

    /// <summary>
    /// 批量修磨申请编辑字段
    /// </summary>
    public class Knife_BatchGrindRequest : BaseVM
    {
        [Display(Name = "备注")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
        }
    }

    /// <summary>
    /// 批量修磨申请刀具列表
    /// </summary>
    public partial class KnifeBatchGrindRequestListVM : BasePagedListVM<Knife_View, KnifeSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>();
        }

        protected override IEnumerable<IGridColumn<Knife_View>> InitGridHeader()
        {
            return new List<GridColumn<Knife_View>>{
                this.MakeGridHeader(x => x.Knife_SerialNumber).SetTitle("序列号").SetWidth(120),
                this.MakeGridHeader(x => x.Knife_Status).SetTitle("状态").SetWidth(80),
                this.MakeGridHeader(x => x.Knife_ItemMaster).SetTitle("料品").SetWidth(100),
                this.MakeGridHeader(x => x.Knife_WareHouse).SetTitle("存储地点").SetWidth(120),
                this.MakeGridHeader(x => x.Knife_WhLocation).SetTitle("库位").SetWidth(80),
                this.MakeGridHeader(x => x.Knife_CurrentLife).SetTitle("当前寿命").SetWidth(60),
                this.MakeGridHeader(x => x.Knife_GrindCount_Int).SetTitle("修磨次数").SetWidth(60),
            };
        }

        public override IOrderedQueryable<Knife_View> GetSearchQuery()
        {
            var query = DC.Set<Knife>()
                .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                .Include(x => x.ItemMaster)
                .CheckIDs(Ids)
                .Select(x => new Knife_View
                {
                    ID = x.ID,
                    Knife_SerialNumber = x.SerialNumber,
                    Knife_Status = x.Status,
                    Knife_ItemMaster = x.ItemMaster.Code,
                    Knife_WareHouse = x.WhLocation.WhArea.WareHouse.Name,
                    Knife_WhLocation = x.WhLocation.Code,
                    Knife_CurrentLife = x.CurrentLife,
                    Knife_GrindCount = x.GrindCount,
                })
                .OrderByDescending(x => x.Knife_SerialNumber);
            return query;
        }
    }
}
