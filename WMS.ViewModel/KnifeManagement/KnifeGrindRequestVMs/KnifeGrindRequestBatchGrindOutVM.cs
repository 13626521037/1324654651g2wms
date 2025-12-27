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
using WMS.Util;
using WMS.Util.U9Para.Knife;

namespace WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs
{
    /// <summary>
    /// 批量修磨出库ViewModel
    /// </summary>
    public partial class KnifeGrindRequestBatchGrindOutVM : BaseBatchVM<KnifeGrindRequest, KnifeGrindRequest_BatchGrindOut>
    {
        public KnifeGrindRequestBatchGrindOutVM()
        {
            ListVM = new KnifeGrindRequestBatchGrindOutListVM();
            LinkedVM = new KnifeGrindRequest_BatchGrindOut();
        }

        protected override void InitVM()
        {
            base.InitVM();
        }

        /// <summary>
        /// 批量修磨出库操作
        /// </summary>
        /// <returns>返回错误信息，成功时返回null</returns>
        public string DoBatchGrindOut()
        {
            try
            {
                // 获取选中的修磨申请单IDs
                if (Ids == null || Ids.Length == 0)
                {
                    return "请选择要进行修磨出库的申请单";
                }

                var requestIds = Ids.Select(x => Guid.Parse(x)).ToList();
                var grindRequests = DC.Set<KnifeGrindRequest>()
                    .Include(x => x.KnifeGrindRequestLine_KnifeGrindRequest)
                        .ThenInclude(x => x.Knife)
                            .ThenInclude(x => x.WhLocation)
                                .ThenInclude(x => x.WhArea)
                                    .ThenInclude(x => x.WareHouse)
                    .Include(x => x.KnifeGrindRequestLine_KnifeGrindRequest)
                        .ThenInclude(x => x.Knife)
                            .ThenInclude(x => x.ItemMaster)
                    .Where(x => requestIds.Contains(x.ID))
                    .ToList();

                if (grindRequests == null || grindRequests.Count != Ids.Length)
                {
                    return "部分修磨申请单不存在，请检查";
                }

                // 校验：只能选择有U9采购单号的申请单
                var requestsWithoutPO = grindRequests.Where(x => string.IsNullOrEmpty(x.LastU9PODocNo)).ToList();
                if (requestsWithoutPO.Any())
                {
                    var docNos = string.Join("、", requestsWithoutPO.Select(x => x.DocNo));
                    return $"以下申请单没有U9采购单号，无法进行批量出库：{docNos}";
                }

                // 校验：U9采购单状态（支持多个不同的采购单号）
                var distinctPODocNos = grindRequests.Select(x => x.LastU9PODocNo).Distinct().ToList();
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);
                
                var closedPODocNos = new List<string>();
                foreach (var poDocNo in distinctPODocNos)
                {
                    U9Return<List<GetKnifeNosByPODocNoResult>> u9Return = apiHelper.GetKnifesByPODocNo(poDocNo);
                    if (!u9Return.Success)
                    {
                        return $"U9采购单{poDocNo}校验失败：{u9Return.Msg}";
                    }
                    if (u9Return.Entity == null || u9Return.Entity.Count == 0)
                    {
                        closedPODocNos.Add(poDocNo);
                    }
                }
                
                if (closedPODocNos.Any())
                {
                    return $"以下采购单在U9中所有行均已关闭，无法进行修磨出库：{string.Join("、", closedPODocNos)}";
                }

                // 校验：申请单必须是已审核状态
                var invalidStatusRequests = grindRequests.Where(x => x.Status != KnifeOrderStatusEnum.Approved).ToList();
                if (invalidStatusRequests.Any())
                {
                    var docNos = string.Join("、", invalidStatusRequests.Select(x => x.DocNo));
                    return $"以下申请单状态不是已审核，无法进行批量出库：{docNos}";
                }

                // 校验：申请单中的刀具必须都是已申请修磨状态
                var allKnives = grindRequests.SelectMany(x => x.KnifeGrindRequestLine_KnifeGrindRequest)
                    .Select(x => x.Knife)
                    .Where(x => x != null)
                    .ToList();

                var invalidKnives = allKnives.Where(x => x.Status != KnifeStatusEnum.GrindRequested).ToList();
                if (invalidKnives.Any())
                {
                    var serialNumbers = string.Join("、", invalidKnives.Select(x => x.SerialNumber));
                    return $"以下刀具状态不是已申请修磨，无法进行批量出库：{serialNumbers}";
                }

                // 执行批量修磨出库（逐个处理）
                var successCount = 0;
                var failedRequests = new List<string>();
                var tran = DC.Database.BeginTransaction();

                try
                {
                    foreach (var request in grindRequests)
                    {
                        try
                        {
                            // 构建修磨出库输入参数
                            var grindOutInfo = new KnifeGrindOutInputInfo
                            {
                                PODocNO = request.LastU9PODocNo,
                                Lines = request.KnifeGrindRequestLine_KnifeGrindRequest
                                    .Where(x => x.Status == KnifeOrderStatusEnum.Approved)
                                    .Select(x => new KnifeGrindOutInputInfo_Line
                                    {
                                        KnifeId = x.KnifeId.ToString(),
                                        GrindKnifeNO = x.Knife.GrindKnifeNO
                                    }).ToList()
                            };

                            // 创建修磨出库VM
                            var grindOutVM = Wtm.CreateVM<KnifeGrindOutVMs.KnifeGrindOutVM>();
                            grindOutVM.DoInitByInfo(grindOutInfo, tran);
                            if (!grindOutVM.MSD.IsValid)
                            {
                                failedRequests.Add($"{request.DocNo}（{grindOutVM.MSD.GetFirstError()}）");
                                continue;
                            }

                            grindOutVM.DoAdd();
                            if (!grindOutVM.MSD.IsValid)
                            {
                                failedRequests.Add($"{request.DocNo}（{grindOutVM.MSD.GetFirstError()}）");
                                continue;
                            }

                            grindOutVM.DoApproved(grindOutInfo, tran);
                            if (!grindOutVM.MSD.IsValid)
                            {
                                failedRequests.Add($"{request.DocNo}（{grindOutVM.MSD.GetFirstError()}）");
                                continue;
                            }

                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            failedRequests.Add($"{request.DocNo}（{ex.Message}）");
                        }
                    }

                    if (failedRequests.Any())
                    {
                        tran.Rollback();
                        return $"部分申请单出库失败：{string.Join("、", failedRequests)}。成功：{successCount}个";
                    }

                    DC.SaveChanges();
                    tran.Commit();
                    return null; // 全部成功
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return "批量修磨出库操作失败：" + ex.Message;
                }
            }
            catch (Exception ex)
            {
                return "批量修磨出库操作失败：" + ex.Message;
            }
        }
    }

    /// <summary>
    /// 批量修磨出库编辑字段
    /// </summary>
    public class KnifeGrindRequest_BatchGrindOut : BaseVM
    {
        [Display(Name = "备注")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
        }
    }

    /// <summary>
    /// 批量修磨出库申请单列表
    /// </summary>
    public partial class KnifeGrindRequestBatchGrindOutListVM : BasePagedListVM<KnifeGrindRequest_View, KnifeGrindRequestSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>();
        }

        protected override IEnumerable<IGridColumn<KnifeGrindRequest_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindRequest_View>>{
                this.MakeGridHeader(x => x.KnifeGrindRequest_DocNo).SetTitle("申请单号").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeGrindRequest_Status).SetTitle("状态").SetWidth(80),
                this.MakeGridHeader(x => x.KnifeGrindRequest_OrderNum).SetTitle("数量").SetWidth(50),
                this.MakeGridHeader(x => x.KnifeGrindRequest_LastU9PODocNo).SetTitle("U9采购单号").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeGrindRequest_HandledBy).SetTitle("经办人").SetWidth(80),
                this.MakeGridHeader(x => x.KnifeGrindRequest_ApprovedTime).SetTitle("审核时间").SetWidth(140),
            };
        }

        public override IOrderedQueryable<KnifeGrindRequest_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeGrindRequest>()
                .CheckIDs(Ids)
                .Select(x => new KnifeGrindRequest_View
                {
                    ID = x.ID,
                    KnifeGrindRequest_DocNo = x.DocNo,
                    KnifeGrindRequest_Status = x.Status,
                    KnifeGrindRequest_OrderNum = x.KnifeGrindRequestLine_KnifeGrindRequest.Count.ToString(),
                    KnifeGrindRequest_LastU9PODocNo = x.LastU9PODocNo,
                    KnifeGrindRequest_HandledBy = DC.Set<FrameworkUser>().Where(z => z.ID.ToString() == x.HandledById).Select(y => y.Name).FirstOrDefault(),
                    KnifeGrindRequest_ApprovedTime = x.ApprovedTime,
                })
                .OrderByDescending(x => x.KnifeGrindRequest_ApprovedTime);
            return query;
        }
    }
}
