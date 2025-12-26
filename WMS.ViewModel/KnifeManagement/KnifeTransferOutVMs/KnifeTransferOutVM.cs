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
using FluentStorage.Utils.Extensions;
using WMS.ViewModel.KnifeManagement.KnifeVMs;
using WMS.ViewModel.KnifeManagement.KnifeTransferOutLineVMs;
using WMS.Model.InventoryManagement;
namespace WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs
{
    public partial class KnifeTransferOutVM : BaseCRUDVM<KnifeTransferOut>
    {

        public List<string> KnifeManagementKnifeTransferOutFTempSelected { get; set; }
        public KnifeTransferOutLineKnifeTransferOutDetailListVM KnifeTransferOutLineKnifeTransferOutList { get; set; }
        public KnifeTransferOutLineKnifeTransferOutDetailListVM1 KnifeTransferOutLineKnifeTransferOutList1 { get; set; }
        public KnifeTransferOutLineKnifeTransferOutDetailListVM2 KnifeTransferOutLineKnifeTransferOutList2 { get; set; }

        public KnifeTransferOutVM()
        {

            SetInclude(x => x.HandledBy);
            SetInclude(x => x.ToWh);
            KnifeTransferOutLineKnifeTransferOutList = new KnifeTransferOutLineKnifeTransferOutDetailListVM();
            KnifeTransferOutLineKnifeTransferOutList.DetailGridPrix = "Entity.KnifeTransferOutLine_KnifeTransferOut";
            KnifeTransferOutLineKnifeTransferOutList1 = new KnifeTransferOutLineKnifeTransferOutDetailListVM1();
            KnifeTransferOutLineKnifeTransferOutList1.DetailGridPrix = "Entity.KnifeTransferOutLine_KnifeTransferOut";
            KnifeTransferOutLineKnifeTransferOutList2 = new KnifeTransferOutLineKnifeTransferOutDetailListVM2();
            KnifeTransferOutLineKnifeTransferOutList2.DetailGridPrix = "Entity.KnifeTransferOutLine_KnifeTransferOut";

        }

        protected override void InitVM()
        {

            KnifeTransferOutLineKnifeTransferOutList.CopyContext(this);
            KnifeTransferOutLineKnifeTransferOutList1.CopyContext(this);
            KnifeTransferOutLineKnifeTransferOutList2.CopyContext(this);

        }

        public override DuplicatedInfo<KnifeTransferOut> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.DocNo));
            return rv;

        }
        public override void Validate()
        {
            if (Entity.KnifeTransferOutLine_KnifeTransferOut.GroupBy(x => new { x.KnifeTransferOutId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }
            base.Validate();
        }
        public override void DoAdd()
        {
            if (Entity.KnifeTransferOutLine_KnifeTransferOut.Count == 0)
            {
                MSD.AddModelError("", "请输入刀具");
                return;
            }
            base.DoAdd();

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
        /// 刀具调出预处理
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        public void DoInitByInfo(KnifeTransferOutInputInfo info, IDbContextTransaction tran)
        {
            try
            {
                //校验
                //1.仓管员当前登录状态
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                //2.info非空检验
                if (string.IsNullOrEmpty(info.ToWhId) 
                    //|| string.IsNullOrEmpty(info.FromWhId) //泽宇周六不在没得改 调出单的调出存储地点先用系统里的
                    || info.Knifes is null || info.Knifes.Count == 0
                    )
                {
                    MSD.AddModelError("", "输入参数不足 请检查");
                    return;
                }
                //3.调出单的目标调入存储地点和和当前登录用户的所在存储地点不可一致    一致的属于移库操作 在一步式的初始化中实现 这里是单独的调出操作
                var ToWh = DC.Set<BaseWareHouse>().FirstOrDefault(x => x.ID.ToString() == info.ToWhId);
                if (ToWh.ID == whid)
                {
                    MSD.AddModelError("", "调出单的目标调入存储地点不可与当前登录的存储地点一致");
                    return;
                }
               /* var FromWh = DC.Set<BaseWareHouse>().FirstOrDefault(x => x.ID.ToString() == info.FromWhId);
                if (FromWh.ID != whid)
                {
                    MSD.AddModelError("", "调出单的调出存储地点只能是当前登录的存储地点");
                    return;
                }*/
                //4.调出单的刀具的存储地点与当前登录用户的存储地点一致 刀具都是在库的
                var knifeIds = info.Knifes.Select(x => x.KnifeId).ToList();
                var knifes = DC.Set<Knife>()
                    .Include(x=>x.WhLocation)
                        .ThenInclude(x=>x.WhArea)
                            .ThenInclude(x=>x.WareHouse)
                    .CheckIDs(knifeIds)
                    .ToList();
                foreach(var knife in knifes)
                {
                    if (knife.WhLocation.WhArea.WareHouse.ID != whid)
                    {
                        MSD.AddModelError("",$"{knife.SerialNumber}的存储地点与当前用户登录的不一致 无法进行调出操作");
                        return;
                    }
                    if (knife.Status != KnifeStatusEnum.InStock)
                    {
                        MSD.AddModelError("", $"{knife.SerialNumber}不在库 无法进行调出操作");
                        return;
                    }
                    if (knife.WhLocation.Locked == true)
                    {
                        MSD.AddModelError("", $"{knife.SerialNumber}所在的库位{knife.WhLocation.Code}已锁定 不可用");
                        return;
                    }
                    if (knife.WhLocation.IsEffective == EffectiveEnum.Ineffective || knife.WhLocation.AreaType != WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"刀具{knife.SerialNumber}所在库位{knife.WhLocation.Code}无效");
                        return ;
                    }
                }
                //给调出单vm赋值
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                var KnifeTransferOutDocNo = DocNoVm.GetSequence("KnifeTransferOutRule", tran);//生成调出单单号

                Entity.DocNo = KnifeTransferOutDocNo;
                Entity.Status = KnifeOrderStatusEnum.Open;
                Entity.HandledById = handledBy.ID.ToString();
                Entity.ToWhId =Guid.Parse(info.ToWhId);
                //Entity.FromWHId =Guid.Parse(info.FromWhId);
                Entity.FromWHId =whid;
                Entity.Memo =info.Memo;
                Entity.WareHouseId = whid;

                Entity.KnifeTransferOutLine_KnifeTransferOut = new List<KnifeTransferOutLine>();
                foreach( var line in info.Knifes)
                {
                    var knife = knifes.FirstOrDefault(x => x.ID == Guid.Parse(line.KnifeId));
                    Entity.KnifeTransferOutLine_KnifeTransferOut.Add( new KnifeTransferOutLine { 
                        KnifeId= Guid.Parse(line.KnifeId),
                        Status=KnifeOrderStatusEnum.Open ,
                        FromWhLocationId = knife.WhLocationId,
                    }) ;
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
                        toCloseCombine.CloseTime = DateTime.Now;
                    }
                }
                #endregion
                DC.SaveChanges();

            }
            catch (Exception e)
            {
                MSD.AddModelError("", "捕获到异常:"+ e.Message);
                return;
            }
        }
        /// <summary>
        /// 刀具调出审核
        /// </summary>
        public void DoApproved()
        {
            try
            {
                //校验
                //1.仓管员当前登录状态
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                //2.调出单非空检验
                if (string.IsNullOrEmpty(Entity.DocNo)
                    || string.IsNullOrEmpty(Entity.HandledById)
                    || string.IsNullOrEmpty(Entity.ToWhId.ToString())
                    || Entity.Status!=KnifeOrderStatusEnum.Open
                    || Entity.KnifeTransferOutLine_KnifeTransferOut is null || Entity.KnifeTransferOutLine_KnifeTransferOut.Count == 0
                    )
                {
                    MSD.AddModelError("", "调出单参数不足 请检查");
                    return;
                }
                //3.调出单的目标调入存储地点和和当前登录用户的所在存储地点不一致 这点不需要 这是两步式独有的  这类审核是通用的
               
                //4.调出单的刀具的存储地点与当前登录用户的存储地点一致 刀具都是在库的
                var knifeIds = Entity.KnifeTransferOutLine_KnifeTransferOut.Select(x => x.KnifeId.ToString()).ToList();
                var knifes = DC.Set<Knife>()
                    .Include(x => x.WhLocation)
                        .ThenInclude(x => x.WhArea)
                            .ThenInclude(x => x.WareHouse)
                    .CheckIDs(knifeIds)
                    .ToList();
                foreach (var knife in knifes)
                {
                    if (knife.WhLocation.WhArea.WareHouse.ID != whid)
                    {
                        MSD.AddModelError("", $"{knife.SerialNumber}的存储地点与当前用户登录的不一致 无法进行调出操作");
                        return;
                    }
                    if (knife.Status != KnifeStatusEnum.InStock)
                    {
                        MSD.AddModelError("", $"{knife.SerialNumber}不在库 无法进行调出操作");
                        return;
                    }
                }

                //审核阶段 自己 刀具 操作
                var currentTime = DateTime.Now;//当前时间
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//仓管员
                if (handledBy is null)
                {
                    MSD.AddModelError("", "未找到有效的仓管员");
                    return;
                }
                var handledBy_operator = DC.Set<BaseOperator>()
                    .Include(x => x.Department)
                        .ThenInclude(x => x.Organization)
                    .Where(x => x.IsValid == true)
                    .FirstOrDefault(x => x.Code.ToString() == LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");//仓管员_业务员类型
                if (handledBy_operator is null)
                {
                    MSD.AddModelError("", "请先配置U9仓管员的业务员身份并同步到wms");
                    return;
                }
                Entity.Status = KnifeOrderStatusEnum.Approved;
                Entity.HandledById = handledBy.ID.ToString();
                Entity.HandledBy = handledBy;
                Entity.ApprovedTime = currentTime;
                Entity.UpdateBy = handledBy.Name;
                Entity.UpdateTime = currentTime;
                foreach(var line in Entity.KnifeTransferOutLine_KnifeTransferOut)
                {
                    line.Status = KnifeOrderStatusEnum.Approved;
                }
                foreach(var knife in knifes)
                {
                    //刀变化
                    knife.Status = KnifeStatusEnum.Transferring;
                    knife.HandledById = Entity.HandledById;
                    knife.HandledBy = handledBy;
                    knife.HandledByName = handledBy.Name;
                    knife.LastOperationDate = currentTime;
                    knife.UpdateBy = handledBy.Name;
                    knife.UpdateTime = currentTime;
                    //生成操作
                    DC.Set<KnifeOperation>().Add(new KnifeOperation
                    {
                        KnifeId = knife.ID,
                        DocNo = Entity.DocNo,
                        OperationType = KnifeOperationTypeEnum.TransferOut,
                        OperationTime = currentTime,
                        OperationBy = handledBy_operator,
                        OperationById = handledBy_operator.ID,
                        HandledById = handledBy.ID.ToString(),
                        HandledBy = handledBy,
                        HandledByName = handledBy.Name,
                        UsedDays = 0,
                        TotalUsedDays = knife.TotalUsedDays,
                        RemainingDays = knife.RemainingUsedDays,
                        CurrentLife = knife.CurrentLife,
                        WhLocationId = knife.WhLocationId,
                        GrindNum = knife.GrindCount,
                        CreateBy = LoginUserInfo.Name,
                        CreateTime = currentTime
                    });
                }
                               
                DC.SaveChanges();

            }
            catch (Exception e)
            {
                MSD.AddModelError("捕获到异常:", e.Message);
                return;
            }
        }

        /// <summary>
        /// 调出单界面获取调入存储地点列表
        /// </summary>
        /// <returns></returns>
        public List<BaseWareHoustReturn> GetToWhList()
        {
            var result = new List<BaseWareHoustReturn>();
            try
            {
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                var Whs = DC.Set<BaseWareHouse>()
                    
                    .Where(x => x.IsEffective == EffectiveEnum.Effective
                                && x.Code.StartsWith("D") && x.Name.Contains("刀具")&&x.Organization.Code=="0410"
                                && !x.Name.Contains("失效") && !x.Name.Contains("报废") && !x.Name.Contains("维修") && !x.Name.Contains("事业")
                                && x.ID!= whid//不要当前仓库
                                )
                    .ToList();
                if(Whs is null || Whs.Count == 0)
                {
                    MSD.AddModelError("", "未找到有效的存储地点");
                    return null;
                }
                foreach(var wh in Whs)
                {
                    result.Add(new BaseWareHoustReturn
                    {
                        Name= wh.Name,
                        ID=wh.ID,
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "捕获到异常:"+e.Message);
                return null;
            }
        }
        /// <summary>
        /// 通过调出单获取刀具信息（包含推荐库位）
        /// </summary>
        /// <param name="docNo">已审核的调出单单号</param>
        /// <returns></returns>
        public List<KnifeTransferOutLineReturn> GetKnifesByTransferOutDocNo(string docNo)
        {

            try
            {
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return null;
                }
                var transferOut = DC.Set<KnifeTransferOut>()
                    .Include(x => x.KnifeTransferOutLine_KnifeTransferOut)
                        .ThenInclude(x => x.Knife)
                            .ThenInclude(x => x.ItemMaster) // 包含料号信息
                    .FirstOrDefault(x => x.DocNo == docNo && x.Status == KnifeOrderStatusEnum.Approved);
                if (transferOut is null)
                {
                    MSD.AddModelError("", "未找到有效的调出单");
                    return null;
                }
                if (transferOut.ToWhId != whid)
                {
                    MSD.AddModelError("", "当前存储地点获取调出单失败");
                    //MSD.AddModelError("", "调出单的调入存储地点与当前登录存储地点不符 调出单获取失败");
                    return null;
                }

                var result = new List<KnifeTransferOutLineReturn>();
                foreach (var line in transferOut.KnifeTransferOutLine_KnifeTransferOut)
                {
                    // 获取实际料号（优先使用实际料号）
                    var actualItemCode = line.Knife.ActualItemCode ?? line.Knife.ItemMaster?.Code;
                    // 获取推荐库位（传入刀具ID以查找原库位）
                    // 使用实际料号查找推荐库位，将itemMasterId设为null以强制使用料号编码匹配
                    var recommendLocations = GetRecommendLocationsByItem(
                        null,  // 不传入ItemMasterId，使用实际料号进行匹配
                        actualItemCode,
                        whid,
                        line.KnifeId);  // 传入刀具ID

                    result.Add(new KnifeTransferOutLineReturn
                    {
                        KnifeId = line.KnifeId.ToString(),
                        SerialNumber = line.Knife.SerialNumber,
                        IsClose = line.Status == KnifeOrderStatusEnum.ApproveClose,
                        SuggestLocs = recommendLocations,
                        
                    });
                }

                return result;
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "捕获到异常:" + e.Message);
                return null;
            }
        }
        /// <summary>
        /// 获取推荐库位（根据料号查找相同料号刀具所在库位）
        /// 推荐逻辑：
        /// 1. 优先推荐该刀具调出前的原库位（从调出单记录中获取最近一次调出操作的库位）
        /// 2. 如果没有找到原库位，查找相同料号刀具的最近出库库位
        /// 3. 查找有相同料号在库刀具的库位（按数量升序）
        /// 4. 如果没有相同料号在库，根据刀具分类查找相似刀具所在库位
        /// 5. 如果没有相似分类，则推荐空库位
        /// </summary>
        /// <param name="itemMasterId">料号ID</param>
        /// <param name="itemMasterCode">料号编码</param>
        /// <param name="whid">仓库ID</param>
        /// <param name="knifeId">刀具ID（可选，用于查找该刀具的原库位）</param>
        /// <returns>推荐库位列表</returns>
        private List<RecommendLocationReturn> GetRecommendLocationsByItem(Guid? itemMasterId, string itemMasterCode, Guid whid, Guid? knifeId = null)
        {
            try
            {
                // 第一步：如果提供了刀具ID，优先查找该刀具调出前的原库位
                if (knifeId.HasValue)
                {
                    var lastTransferOutOperation = DC.Set<KnifeOperation>()
                        .Include(x => x.WhLocation)
                            .ThenInclude(x => x.WhArea)
                        .Where(x => x.KnifeId == knifeId.Value)
                        .Where(x => x.OperationType == KnifeOperationTypeEnum.TransferOut)  // 调出操作
                        .Where(x => x.WhLocationId != null)  // 库位不为空
                        .Where(x => x.WhLocation.WhArea.WareHouseId == whid)  // 当前存储地点
                        .Where(x => x.WhLocation.IsEffective == EffectiveEnum.Effective)  // 库位有效
                        .Where(x => x.WhLocation.Locked != true)  // 库位未锁定
                        .Where(x => x.WhLocation.AreaType == WhLocationEnum.Normal)  // 正常库位
                        .Where(x => !x.WhLocation.Code.Contains("A9999999"))  // 排除不良退回库位
                        .OrderByDescending(x => x.OperationTime)  // 按操作时间降序，取最近一次
                        .FirstOrDefault();

                    if (lastTransferOutOperation != null)
                    {
                        // 统计该库位当前的刀具数量
                        var knifeCount = DC.Set<Knife>()
                            .Where(k => k.WhLocationId == lastTransferOutOperation.WhLocationId && k.Status == KnifeStatusEnum.InStock)
                            .Count();

                        var originalLocationReturn = new RecommendLocationReturn
                        {
                            AreaCode = lastTransferOutOperation.WhLocation.WhArea.Code,
                            LocCode = lastTransferOutOperation.WhLocation.Code,
                            Sn = null,
                            Batch = null,
                            Qty = knifeCount
                        };

                        // 补充一个相同料号的库位（优先）或空库位
                        // 查找相同料号的其他库位
                        var sameItemQueryStep1 = DC.Set<Knife>()
                            .Include(x => x.WhLocation)
                                .ThenInclude(x => x.WhArea)
                            .Include(x => x.ItemMaster)
                                .ThenInclude(x => x.Organization)
                            .Where(x => x.WhLocation.WhArea.WareHouseId == whid)
                            .Where(x => x.WhLocation.IsEffective == EffectiveEnum.Effective)
                            .Where(x => x.WhLocation.Locked != true)
                            .Where(x => x.WhLocation.AreaType == WhLocationEnum.Normal)
                            .Where(x => !x.WhLocation.Code.Contains("A9999999"))
                            .Where(x => x.Status == KnifeStatusEnum.InStock)
                            .Where(x => x.WhLocationId != lastTransferOutOperation.WhLocationId)  // 排除原库位
                            .Where(x => x.ItemMaster.Organization.Code == "0410")  // 限定组织0410
                            .AsQueryable();

                        // 根据料号筛选
                        if (itemMasterId.HasValue)
                        {
                            sameItemQueryStep1 = sameItemQueryStep1.Where(x => x.ItemMasterId == itemMasterId.Value);
                        }
                        else if (!string.IsNullOrEmpty(itemMasterCode))
                        {
                            sameItemQueryStep1 = sameItemQueryStep1.Where(x => x.ItemMaster.Code == itemMasterCode || x.ActualItemCode == itemMasterCode);
                        }

                        var additionalSameItemLocation = sameItemQueryStep1
                            .GroupBy(x => new
                            {
                                WhLocationId = x.WhLocationId,
                                LocCode = x.WhLocation.Code,
                                AreaCode = x.WhLocation.WhArea.Code
                            })
                            .Select(g => new RecommendLocationReturn
                            {
                                AreaCode = g.Key.AreaCode,
                                LocCode = g.Key.LocCode,
                                Sn = null,
                                Batch = null,
                                Qty = g.Count()
                            })
                            .OrderBy(x => x.Qty)  // 按数量升序，数量少的优先
                            .FirstOrDefault();

                        // 如果找到相同料号的库位，返回原库位+相同料号库位
                        if (additionalSameItemLocation != null)
                        {
                            return new List<RecommendLocationReturn>
                            {
                                originalLocationReturn,
                                additionalSameItemLocation
                            };
                        }

                        // 如果没有相同料号的库位，补充一个空库位
                        var emptyLocation = DC.Set<BaseWhLocation>()
                            .Include(x => x.WhArea)
                                .ThenInclude(x => x.WareHouse)
                            .Where(x => x.WhArea.WareHouseId == whid)
                            .Where(x => x.IsEffective == EffectiveEnum.Effective)
                            .Where(x => x.Locked != true)
                            .Where(x => x.AreaType == WhLocationEnum.Normal)
                            .Where(x => !x.Code.Contains("A9999999"))
                            .Where(x => x.ID != lastTransferOutOperation.WhLocationId)  // 排除原库位
                            .Where(x => !DC.Set<Knife>().Any(k => k.WhLocationId == x.ID && k.Status == KnifeStatusEnum.InStock))  // 只要空库位
                            .Select(loc => new RecommendLocationReturn
                            {
                                AreaCode = loc.WhArea.Code,
                                LocCode = loc.Code,
                                Sn = null,
                                Batch = null,
                                Qty = 0
                            })
                            .FirstOrDefault();

                        if (emptyLocation != null)
                        {
                            return new List<RecommendLocationReturn>
                            {
                                originalLocationReturn,
                                emptyLocation
                            };
                        }

                        // 如果连空库位都没有，只返回原库位
                        return new List<RecommendLocationReturn> { originalLocationReturn };
                    }
                }

                // 第二步：查找相同料号刀具的最近出库库位
                var recentCheckOutOperation = DC.Set<KnifeOperation>()
                    .Include(x => x.Knife)
                        .ThenInclude(x => x.ItemMaster)
                            .ThenInclude(x => x.Organization)
                    .Include(x => x.WhLocation)
                        .ThenInclude(x => x.WhArea)
                    .Where(x => x.WhLocation.WhArea.WareHouseId == whid)
                    .Where(x => x.OperationType == KnifeOperationTypeEnum.CheckOut)  // 出库操作
                    .Where(x => x.WhLocationId != null)
                    .Where(x => x.WhLocation.IsEffective == EffectiveEnum.Effective)
                    .Where(x => x.WhLocation.Locked != true)
                    .Where(x => x.WhLocation.AreaType == WhLocationEnum.Normal)
                    .Where(x => !x.WhLocation.Code.Contains("A9999999"))
                    .Where(x => x.Knife.ItemMaster.Organization.Code == "0410")  // 限定组织0410
                    .AsQueryable();

                // 根据料号筛选
                if (itemMasterId.HasValue)
                {
                    recentCheckOutOperation = recentCheckOutOperation.Where(x => x.Knife.ItemMasterId == itemMasterId.Value);
                }
                else if (!string.IsNullOrEmpty(itemMasterCode))
                {
                    recentCheckOutOperation = recentCheckOutOperation.Where(x => x.Knife.ItemMaster.Code == itemMasterCode || x.Knife.ActualItemCode == itemMasterCode);
                }

                var recentLocation = recentCheckOutOperation
                    .OrderByDescending(x => x.OperationTime)
                    .Select(x => new
                    {
                        x.WhLocation,
                        x.WhLocation.WhArea
                    })
                    .FirstOrDefault();

                if (recentLocation != null)
                {
                    // 统计该库位当前的刀具数量
                    var knifeCount = DC.Set<Knife>()
                        .Where(k => k.WhLocationId == recentLocation.WhLocation.ID && k.Status == KnifeStatusEnum.InStock)
                        .Count();

                    var recentLocationReturn = new RecommendLocationReturn
                    {
                        AreaCode = recentLocation.WhArea.Code,
                        LocCode = recentLocation.WhLocation.Code,
                        Sn = null,
                        Batch = null,
                        Qty = knifeCount
                    };

                    // 补充一个相同料号的库位或空库位
                    var sameItemQueryStep2 = DC.Set<Knife>()
                        .Include(x => x.WhLocation)
                            .ThenInclude(x => x.WhArea)
                        .Include(x => x.ItemMaster)
                            .ThenInclude(x => x.Organization)
                        .Where(x => x.WhLocation.WhArea.WareHouseId == whid)
                        .Where(x => x.WhLocation.IsEffective == EffectiveEnum.Effective)
                        .Where(x => x.WhLocation.Locked != true)
                        .Where(x => x.WhLocation.AreaType == WhLocationEnum.Normal)
                        .Where(x => !x.WhLocation.Code.Contains("A9999999"))
                        .Where(x => x.Status == KnifeStatusEnum.InStock)
                        .Where(x => x.WhLocationId != recentLocation.WhLocation.ID)
                        .Where(x => x.ItemMaster.Organization.Code == "0410")  // 限定组织0410
                        .AsQueryable();

                    if (itemMasterId.HasValue)
                    {
                        sameItemQueryStep2 = sameItemQueryStep2.Where(x => x.ItemMasterId == itemMasterId.Value);
                    }
                    else if (!string.IsNullOrEmpty(itemMasterCode))
                    {
                        sameItemQueryStep2 = sameItemQueryStep2.Where(x => x.ItemMaster.Code == itemMasterCode || x.ActualItemCode == itemMasterCode);
                    }

                    var additionalSameItemLocation = sameItemQueryStep2
                        .GroupBy(x => new
                        {
                            WhLocationId = x.WhLocationId,
                            LocCode = x.WhLocation.Code,
                            AreaCode = x.WhLocation.WhArea.Code
                        })
                        .Select(g => new RecommendLocationReturn
                        {
                            AreaCode = g.Key.AreaCode,
                            LocCode = g.Key.LocCode,
                            Sn = null,
                            Batch = null,
                            Qty = g.Count()
                        })
                        .OrderBy(x => x.Qty)
                        .FirstOrDefault();

                    if (additionalSameItemLocation != null)
                    {
                        return new List<RecommendLocationReturn>
                        {
                            recentLocationReturn,
                            additionalSameItemLocation
                        };
                    }

                    // 如果没有相同料号的库位，补充一个空库位
                    var emptyLocation = DC.Set<BaseWhLocation>()
                        .Include(x => x.WhArea)
                            .ThenInclude(x => x.WareHouse)
                        .Where(x => x.WhArea.WareHouseId == whid)
                        .Where(x => x.IsEffective == EffectiveEnum.Effective)
                        .Where(x => x.Locked != true)
                        .Where(x => x.AreaType == WhLocationEnum.Normal)
                        .Where(x => !x.Code.Contains("A9999999"))
                        .Where(x => x.ID != recentLocation.WhLocation.ID)
                        .Where(x => !DC.Set<Knife>().Any(k => k.WhLocationId == x.ID && k.Status == KnifeStatusEnum.InStock))
                        .Select(loc => new RecommendLocationReturn
                        {
                            AreaCode = loc.WhArea.Code,
                            LocCode = loc.Code,
                            Sn = null,
                            Batch = null,
                            Qty = 0
                        })
                        .FirstOrDefault();

                    if (emptyLocation != null)
                    {
                        return new List<RecommendLocationReturn>
                        {
                            recentLocationReturn,
                            emptyLocation
                        };
                    }

                    // 如果连空库位都没有，只返回最近出库的库位
                    return new List<RecommendLocationReturn> { recentLocationReturn };
                }

                // 获取当前料号的分类ID（用于后续步骤）
                Guid? itemCategoryId = null;

                // 如果料号ID有值，从数据库查询该料号的分类ID
                if (itemMasterId.HasValue)
                {
                    // 查询指定料号ID的分类ID
                    itemCategoryId = DC.Set<BaseItemMaster>()
                        .Where(x => x.ID == itemMasterId.Value)
                        .Select(x => x.ItemCategoryId)
                        .FirstOrDefault();
                }
                // 如果料号ID为空但料号编码不为空，通过料号编码查询分类ID
                else if (!string.IsNullOrEmpty(itemMasterCode))
                {
                    // 通过料号编码查询对应的分类ID
                    itemCategoryId = DC.Set<BaseItemMaster>()
                        .Where(x => x.Code == itemMasterCode)
                        .Select(x => x.ItemCategoryId)
                        .FirstOrDefault();
                }

                // 第三步：查找有相同料号在库刀具的库位
                // 创建一个查询，查找刀具信息，包含库位、库区、仓库和料号信息
                var sameItemQuery = DC.Set<Knife>()
                    .Include(x => x.WhLocation)        // 包含刀具所在的库位信息
                        .ThenInclude(x => x.WhArea)    // 包含库位所属的库区信息
                            .ThenInclude(x => x.WareHouse) // 包含库区所属的仓库信息
                    .Include(x => x.ItemMaster)        // 包含刀具对应的料号信息
                        .ThenInclude(x => x.Organization)  // 包含料号所属的组织信息
                    .Where(x => x.WhLocation.WhArea.WareHouseId == whid)  // 筛选条件：刀具所在仓库ID等于传入的仓库ID
                    .Where(x => x.WhLocation.IsEffective == EffectiveEnum.Effective)  // 筛选条件：库位有效
                    .Where(x => x.WhLocation.Locked != true)  // 筛选条件：库位未锁定
                    .Where(x => x.WhLocation.AreaType == WhLocationEnum.Normal)  // 筛选条件：库位类型为正常库位
                    .Where(x => !x.WhLocation.Code.Contains("A9999999"))  // 筛选条件：库位编码不包含A9999999（排除不良退回库位）
                    .Where(x => x.Status == KnifeStatusEnum.InStock)  // 筛选条件：刀具状态为在库
                    .Where(x => x.ItemMaster.Organization.Code == "0410")  // 限定组织0410
                    .AsQueryable(); // 将查询转换为可查询对象

                // 如果料号ID有值，添加料号ID匹配的筛选条件
                if (itemMasterId.HasValue)
                {
                    // 筛选条件：刀具料号ID等于传入的料号ID
                    sameItemQuery = sameItemQuery.Where(x => x.ItemMasterId == itemMasterId.Value);
                }
                // 如果料号ID为空但料号编码不为空，添加料号编码匹配的筛选条件
                else if (!string.IsNullOrEmpty(itemMasterCode))
                {
                    // 筛选条件：刀具料号编码等于传入的料号编码或实际料号编码等于传入的料号编码
                    sameItemQuery = sameItemQuery.Where(x => x.ItemMaster.Code == itemMasterCode || x.ActualItemCode == itemMasterCode);
                }
                // 如果料号ID和料号编码都为空，返回空的推荐库位列表
                else
                {
                    return new List<RecommendLocationReturn>();
                }

                // 按库位分组统计相同料号刀具数量，并按数量升序排列
                // 将查询结果按库位信息分组，计算每个库位的刀具数量
                var sameItemLocations = sameItemQuery
                    .GroupBy(x => new // 按以下字段分组
                    {
                        // 库位ID
                        WhLocationId = x.WhLocationId,
                        // 库位编码
                        LocCode = x.WhLocation.Code,
                        // 库区编码
                        AreaCode = x.WhLocation.WhArea.Code
                    })
                    .Select(g => new RecommendLocationReturn // 将分组结果转换为推荐库位返回对象
                    {
                        // 库区编码
                        AreaCode = g.Key.AreaCode,
                        // 库位编码
                        LocCode = g.Key.LocCode,
                        // 序列号
                        Sn = null,
                        // 批号
                        Batch = null,
                        // 该库位中相同料号的刀具数量
                        Qty = g.Count()
                    })
                    .OrderBy(x => x.Qty)  // 按刀具数量升序排列（数量少的优先推荐）
                    .Take(2)  // 只取前2个推荐库位
                    .ToList(); // 转换为列表

                // 如果找到有相同料号刀具的库位，优先返回，但如果不足2个则补充空库位
                if (sameItemLocations.Count > 0)
                {
                    // 如果已经有2个推荐库位，直接返回
                    if (sameItemLocations.Count >= 2)
                    {
                        return sameItemLocations;
                    }
                    // 否则，补充空库位
                    var needCount = 2 - sameItemLocations.Count;
                    var existingLocationCodes = sameItemLocations.Select(x => x.LocCode).ToList();

                    var additionalLocations = DC.Set<BaseWhLocation>()
                        .Include(x => x.WhArea)
                            .ThenInclude(x => x.WareHouse)
                        .Where(x => x.WhArea.WareHouseId == whid)
                        .Where(x => x.IsEffective == EffectiveEnum.Effective)
                        .Where(x => x.Locked != true)
                        .Where(x => x.AreaType == WhLocationEnum.Normal)
                        .Where(x => !x.Code.Contains("A9999999"))
                        .Where(x => !existingLocationCodes.Contains(x.Code))  // 排除已推荐的库位
                        .Where(x => !DC.Set<Knife>().Any(k => k.WhLocationId == x.ID && k.Status == KnifeStatusEnum.InStock))  // 只要空库位
                        .Take(needCount)
                        .Select(x => new RecommendLocationReturn
                        {
                            AreaCode = x.WhArea.Code,
                            LocCode = x.Code,
                            Sn = null,
                            Batch = null,
                            Qty = 0  // 空库位数量为0
                        })
                        .ToList();

                    sameItemLocations.AddRange(additionalLocations);
                    return sameItemLocations;
                }

                // 第四步：如果没有相同料号的库位，根据刀具分类查找相似刀具所在库位
                // 如果料号分类ID有值
                if (itemCategoryId.HasValue)
                {
                    // 查询相同分类的刀具所在库位
                    var sameCategoryLocations = DC.Set<Knife>()
                        .Include(x => x.WhLocation)        // 包含刀具所在的库位信息
                            .ThenInclude(x => x.WhArea)    // 包含库位所属的库区信息
                        .Include(x => x.ItemMaster)        // 包含刀具对应的料号信息
                            .ThenInclude(x => x.Organization)  // 包含料号所属的组织信息
                        .Where(x => x.WhLocation.WhArea.WareHouseId == whid) // 筛选条件：刀具所在仓库ID等于传入的仓库ID
                        .Where(x => x.WhLocation.IsEffective == EffectiveEnum.Effective) // 筛选条件：库位有效
                        .Where(x => x.WhLocation.Locked != true) // 筛选条件：库位未锁定
                        .Where(x => x.WhLocation.AreaType == WhLocationEnum.Normal) // 筛选条件：库位类型为正常库位
                        .Where(x => !x.WhLocation.Code.Contains("A9999999")) // 筛选条件：库位编码不包含A9999999
                        .Where(x => x.Status == KnifeStatusEnum.InStock) // 筛选条件：刀具状态为在库
                        .Where(x => x.ItemMaster.ItemCategoryId == itemCategoryId.Value)  // 筛选条件：刀具料号的分类ID等于传入的分类ID
                        .Where(x => x.ItemMaster.Organization.Code == "0410")  // 限定组织0410
                        .GroupBy(x => new // 按以下字段分组
                        {
                            // 库位ID
                            WhLocationId = x.WhLocationId,
                            // 库位编码
                            LocCode = x.WhLocation.Code,
                            // 库区编码
                            AreaCode = x.WhLocation.WhArea.Code
                        })
                        .Select(g => new RecommendLocationReturn // 将分组结果转换为推荐库位返回对象
                        {
                            // 库区编码
                            AreaCode = g.Key.AreaCode,
                            // 库位编码
                            LocCode = g.Key.LocCode,
                            // 序列号
                            Sn = null,
                            // 批号
                            Batch = null,
                            // 该库位中相同分类的刀具数量
                            Qty = g.Count()
                        })
                        .OrderBy(x => x.Qty) // 按刀具数量升序排列
                        .Take(2)  // 只取前2个推荐库位
                        .ToList(); // 转换为列表

                    // 如果找到相同分类的库位，返回，但如果不足2个则补充空库位
                    if (sameCategoryLocations.Count > 0)
                    {
                        // 如果已经有2个推荐库位，直接返回
                        if (sameCategoryLocations.Count >= 2)
                        {
                            return sameCategoryLocations;
                        }
                        // 否则，补充空库位
                        var needCount = 2 - sameCategoryLocations.Count;
                        var existingLocationCodes = sameCategoryLocations.Select(x => x.LocCode).ToList();

                        var additionalLocations = DC.Set<BaseWhLocation>()
                            .Include(x => x.WhArea)
                                .ThenInclude(x => x.WareHouse)
                            .Where(x => x.WhArea.WareHouseId == whid)
                            .Where(x => x.IsEffective == EffectiveEnum.Effective)
                            .Where(x => x.Locked != true)
                            .Where(x => x.AreaType == WhLocationEnum.Normal)
                            .Where(x => !x.Code.Contains("A9999999"))
                            .Where(x => !existingLocationCodes.Contains(x.Code))  // 排除已推荐的库位
                            .Where(x => !DC.Set<Knife>().Any(k => k.WhLocationId == x.ID && k.Status == KnifeStatusEnum.InStock))  // 只要空库位
                            .Take(needCount)
                            .Select(x => new RecommendLocationReturn
                            {
                                AreaCode = x.WhArea.Code,
                                LocCode = x.Code,
                                Sn = null,
                                Batch = null,
                                Qty = 0  // 空库位数量为0
                            })
                            .ToList();

                        sameCategoryLocations.AddRange(additionalLocations);
                        return sameCategoryLocations;
                    }
                }

                // 第五步：如果没有相同料号和相似分类的库位，只推荐空库位
                // 查询所有符合条件的空库位
                var emptyLocations = DC.Set<BaseWhLocation>() // 查询库位表
                    .Include(x => x.WhArea)        // 包含库位所属的库区信息
                        .ThenInclude(x => x.WareHouse) // 包含库区所属的仓库信息
                    .Where(x => x.WhArea.WareHouseId == whid) // 筛选条件：库区所属仓库ID等于传入的仓库ID
                    .Where(x => x.IsEffective == EffectiveEnum.Effective) // 筛选条件：库位有效
                    .Where(x => x.Locked != true) // 筛选条件：库位未锁定
                    .Where(x => x.AreaType == WhLocationEnum.Normal) // 筛选条件：库位类型为正常库位
                    .Where(x => !x.Code.Contains("A9999999")) // 筛选条件：库位编码不包含A9999999
                    .Where(x => !DC.Set<Knife>().Any(k => k.WhLocationId == x.ID && k.Status == KnifeStatusEnum.InStock)) // 只要空库位（没有在库刀具）
                    .Take(2) // 只取前2个库位
                    .Select(loc => new RecommendLocationReturn // 转换为推荐库位返回对象
                    {
                        // 库区编码
                        AreaCode = loc.WhArea.Code,
                        // 库位编码
                        LocCode = loc.Code,
                        // 序列号
                        Sn = null,
                        // 批号
                        Batch = null,
                        // 空库位数量为0
                        Qty = 0
                    })
                    .ToList(); // 转换为列表

                // 如果找到符合条件的空库位，返回这些库位信息
                if (emptyLocations.Count > 0)
                {
                    return emptyLocations;
                }

                // 如果以上所有步骤都没找到推荐库位，返回空列表
                return new List<RecommendLocationReturn>();
            }
            // 捕获异常，推荐库位获取失败不影响主流程
            catch (Exception)
            {
                // 推荐库位获取失败不影响主流程，返回空列表
                return new List<RecommendLocationReturn>();
            }
        }

    }
}
