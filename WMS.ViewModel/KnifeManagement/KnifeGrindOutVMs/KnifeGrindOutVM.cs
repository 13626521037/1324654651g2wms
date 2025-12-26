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
using WMS.Util.U9Para.Knife;
using WMS.Util;
using Microsoft.EntityFrameworkCore.Storage;
using Elsa;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.ViewModel.KnifeManagement.KnifeVMs;
using WMS.ViewModel.KnifeManagement.KnifeGrindOutLineVMs;
using WMS.Model.BaseData;
namespace WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs
{
    public partial class KnifeGrindOutVM : BaseCRUDVM<KnifeGrindOut>
    {

        public List<string> KnifeManagementKnifeGrindOutFTempSelected { get; set; }
        public KnifeGrindOutLineKnifeGrindOutDetailListVM KnifeGrindOutLineKnifeGrindOutList { get; set; }
        public KnifeGrindOutLineKnifeGrindOutDetailListVM1 KnifeGrindOutLineKnifeGrindOutList1 { get; set; }
        public KnifeGrindOutLineKnifeGrindOutDetailListVM2 KnifeGrindOutLineKnifeGrindOutList2 { get; set; }
        public KnifeGrindOutVM()
        {
            SetInclude(x => x.HandledBy);
            KnifeGrindOutLineKnifeGrindOutList = new KnifeGrindOutLineKnifeGrindOutDetailListVM();
            KnifeGrindOutLineKnifeGrindOutList.DetailGridPrix = "Entity.KnifeGrindOutLine_KnifeGrindOut";
            KnifeGrindOutLineKnifeGrindOutList1 = new KnifeGrindOutLineKnifeGrindOutDetailListVM1();
            KnifeGrindOutLineKnifeGrindOutList1.DetailGridPrix = "Entity.KnifeGrindOutLine_KnifeGrindOut";
            KnifeGrindOutLineKnifeGrindOutList2 = new KnifeGrindOutLineKnifeGrindOutDetailListVM2();
            KnifeGrindOutLineKnifeGrindOutList2.DetailGridPrix = "Entity.KnifeGrindOutLine_KnifeGrindOut";

        }

        protected override void InitVM()
        {
            KnifeGrindOutLineKnifeGrindOutList.CopyContext(this);
            KnifeGrindOutLineKnifeGrindOutList1.CopyContext(this);
            KnifeGrindOutLineKnifeGrindOutList2.CopyContext(this);
        }

        public override DuplicatedInfo<KnifeGrindOut> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.DocNo));
            return rv;

        }

        public override async Task DoAddAsync()
        {

            await base.DoAddAsync();

        }

        public override async Task DoEditAsync(bool updateAllFields = false)
        {

            await base.DoEditAsync();

        }

        public override async Task DoDeleteAsync()
        {
            await base.DoDeleteAsync();

        }
        public override void Validate()
        {
            if (Entity.KnifeGrindOutLine_KnifeGrindOut.Count == 0)
            {
                MSD.AddModelError("", "请输入刀具");
                return;
            }
            if (Entity.KnifeGrindOutLine_KnifeGrindOut.GroupBy(x => new { x.KnifeGrindOutId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }

            base.Validate();
        }

        public override void DoAdd()
        {
            if (Entity.KnifeGrindOutLine_KnifeGrindOut.Count == 0)
            {
                MSD.AddModelError("", "请输入刀具");
                return;
            }
            if (Entity.KnifeGrindOutLine_KnifeGrindOut.GroupBy(x => new { x.KnifeGrindOutId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
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
        /// 修磨出库时根据po单号获取刀具列表
        /// </summary>
        /// <param name="PODocNo"></param>
        /// <returns></returns>
        public List<KnifeGrindOutLineGetFromU9POReturn> GetKnifesByPODocNo(string PODocNo)//
        {
            var result = new List<KnifeGrindOutLineGetFromU9POReturn>();
            try
            {
                //调u9接口  根据单号 获取采购单-采购行list -请购行list
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);// 固定组织
                U9Return<List<GetKnifeNosByPODocNoResult>> u9Return = (U9Return<List<GetKnifeNosByPODocNoResult>>)apiHelper.GetKnifesByPODocNo(PODocNo);
                if (!u9Return.Success)
                {
                    MSD.AddModelError("", "" + u9Return.Msg);
                    return null;
                }
                if (u9Return.Entity.Count==0)
                {
                    MSD.AddModelError("", $"{PODocNo}采购单所有行均已关闭");
                    return null;
                }
                // 获取到的数据加以处理后传给前端 
                var knifeSerialNumbers = u9Return.Entity.Select(x => x.KnifeSerialNumber).ToList();
                var knifes = DC.Set<Knife>().Where(x => knifeSerialNumbers.Contains(x.SerialNumber)).ToList();
                var grindRequestLines = DC.Set<KnifeGrindRequestLine>()
                    .Include(x=>x.Knife)
                    .Where(x => knifes.Contains(x.Knife)&&x.Status == KnifeOrderStatusEnum.Approved)//已审核未关闭的
                    .ToList();
                foreach (var k in knifes)
                {
                    var grindRequestLine = grindRequestLines.FirstOrDefault(x => x.Knife.ID == k.ID);
                    GetKnifeNosByPODocNoResult u9Line = u9Return.Entity.FirstOrDefault(x => x.KnifeSerialNumber == k.SerialNumber);
                    result.Add(new KnifeGrindOutLineGetFromU9POReturn()
                    {
                        KnifeId=k.ID.ToString(),
                        SerialNumber=k.SerialNumber,
                        IsClose=false,//是否已修磨出库的判断在U9的采购子行上了  不在这边  只要获取到就不是关闭
                        POShipLineId = u9Line.POShipLineId,
                        PRDocNo = u9Line.PRDocNo,
                        PRDocLineNo = u9Line.PRDocLineNo,
                        PODocNo = u9Line.PODocNo,
                        PODocLineNo = u9Line.PODocLineNo,
                        GrindKnifeNO = k.GrindKnifeNO,
                    });
                }

                return result;
            }
            catch (Exception e)
            {
                MSD.AddModelError("", ""+e.Message);
                return null;
            }
        }
        /// <summary>
        /// 修磨出库单预处理
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        public void DoInitByInfo(KnifeGrindOutInputInfo info, IDbContextTransaction tran)
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
                    //登录状态校验
                    if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                    {
                        MSD.AddModelError("", "登录信息已过期，请重新登录");
                        return;
                    }
                    Guid whid;//当前pda持有者(仓管员)的存储地点
                    Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                    //校验:非空
                    if (info is null || info.Lines is null || info.Lines.Count == 0 || string.IsNullOrEmpty(info.PODocNO))
                    {
                        MSD.AddModelError("", "修磨出库参数不足 请填写刀具信息");
                        return;
                    }
                    //校验:有效性 刀具状态校验 
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
                            MSD.AddModelError("", $"{k.SerialNumber}不属于当前登录存储地点 无法进行操作");

                        }
                        if (k.Status != KnifeStatusEnum.GrindRequested)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}的状态不为修磨申请中 无法进行修磨出库");
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
                    //生成修磨出库单
                    var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                    var KnifeGrindOutDocNo = DocNoVm.GetSequence("KnifeGrindOutRule", tran);//生成归还单单号
                    var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人

                    Entity.DocNo = KnifeGrindOutDocNo;
                    Entity.HandledBy = handledBy;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.Status = KnifeOrderStatusEnum.Open;
                    Entity.U9PODocNo = info.PODocNO;
                    Entity.WareHouseId = whid;
                    Entity.KnifeGrindOutLine_KnifeGrindOut = new List<KnifeGrindOutLine>();
                    foreach (var knife in knifes)
                    {
                        Entity.KnifeGrindOutLine_KnifeGrindOut.Add(new KnifeGrindOutLine
                        {
                            KnifeId = knife.ID,
                            Knife = knife,
                            FromWhLocationId = knife.WhLocationId,
                            Status = KnifeOrderStatusEnum.Open,
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
                            toCloseCombine.CloseTime = DateTime.Now;
                        }
                    }
                    #endregion
                    DC.SaveChanges();

                    return;
                }
                catch (Exception e)
                {
                    MSD.AddModelError("", "" + e.Message);
                    return;
                }

            }
            else if (currentVersion.Value == "1")//林琪伦的增加组合刀号的刀具修磨
            {
                try
                {
                    //登录状态校验
                    if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                    {
                        MSD.AddModelError("", "登录信息已过期，请重新登录");
                        return;
                    }
                    Guid whid;//当前pda持有者(仓管员)的存储地点
                    Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);

                    //校验:非空 - 支持两种方式：1. 组合条码 2. Lines数组
                    if (info is null || string.IsNullOrEmpty(info.PODocNO))
                    {
                        MSD.AddModelError("", "修磨出库参数不足，请传入采购单号");
                        return;
                    }

                    // 如果既没有组合条码也没有Lines，则报错
                    if (string.IsNullOrEmpty(info.CombineCode) && (info.Lines == null || info.Lines.Count == 0))
                    {
                        MSD.AddModelError("", "请传入组合条码或刀具列表");
                        return;
                    }

                    // 获取刀具列表
                    List<Knife> knifes = new List<Knife>();

                    // 【新增】支持组合条码方式
                    if (!string.IsNullOrEmpty(info.CombineCode))
                    {
                        // 根据组合条码查找组合单
                        var knifeCombine = DC.Set<KnifeCombine>()
                            .Include(x => x.KnifeCombineLine_KnifeCombine)
                                .ThenInclude(x => x.Knife)
                                    .ThenInclude(x => x.WhLocation)
                                        .ThenInclude(x => x.WhArea)
                                            .ThenInclude(x => x.WareHouse)
                            .Where(x => x.DocNo == info.CombineCode || x.CombineKnifeNo == info.CombineCode)
                            .Where(x => x.Status == KnifeOrderStatusEnum.Approved)
                            .FirstOrDefault();

                        if (knifeCombine == null)
                        {
                            MSD.AddModelError("", $"未找到可用的组合单: {info.CombineCode}");
                            return;
                        }

                        // 使用组合单中的所有刀具
                        knifes = knifeCombine.KnifeCombineLine_KnifeCombine
                            .Where(x => x.Knife != null)
                            .Select(x => x.Knife)
                            .ToList();

                        if (knifes.Count == 0)
                        {
                            MSD.AddModelError("", $"组合单 {info.CombineCode} 中没有有效的刀具");
                            return;
                        }
                    }
                    else
                    {
                        // 使用Lines查询刀具
                        var knifeIds = info.Lines.Select(x => x.KnifeId).ToList();
                        knifes = DC.Set<Knife>()
                            .Include(x => x.WhLocation)
                                .ThenInclude(x => x.WhArea)
                                    .ThenInclude(x => x.WareHouse)
                            .CheckIDs(knifeIds).ToList();

                        if (knifes.Count == 0)
                        {
                            MSD.AddModelError("", "未找到有效的刀具信息");
                            return;
                        }

                        if (knifes.Count != info.Lines.Count)
                        {
                            MSD.AddModelError("", $"部分刀具信息无效，传入{info.Lines.Count}把，实际找到{knifes.Count}把");
                            return;
                        }
                    }

                    // 【2025-12-18 新增】校验：确保刀具属于指定的采购单
                    // 查询这些刀具对应的修磨申请行，验证U9PODocNo是否匹配
                    var knifeIdsForValidation = knifes.Select(k => k.ID).ToList();
                    var grindRequestLines = DC.Set<KnifeGrindRequestLine>()
                        .Where(x => x.KnifeId.HasValue && knifeIdsForValidation.Contains(x.KnifeId.Value))
                        .Where(x => x.Status == KnifeOrderStatusEnum.Approved)
                        .ToList();

                    // 检查是否所有刀具都有对应的修磨申请行
                    var knivesWithoutRequest = knifes
                        .Where(k => !grindRequestLines.Any(line => line.KnifeId == k.ID))
                        .ToList();

                    if (knivesWithoutRequest.Count > 0)
                    {
                        var serialNumbers = string.Join(", ", knivesWithoutRequest.Select(k => k.SerialNumber));
                        MSD.AddModelError("", $"以下刀具没有有效的修磨申请记录：{serialNumbers}");
                        return;
                    }

                    // 检查刀具对应的采购单号是否与传入的PODocNo匹配
                    var mismatchCount = grindRequestLines
                        .Where(line => line.U9PODocNo != info.PODocNO)
                        .Count();

                    if (mismatchCount > 0)
                    {
                        MSD.AddModelError("", $"采购单号不匹配：传入的采购单号为{info.PODocNO}，但部分刀具不属于该采购单");
                        return;
                    }
                    foreach (var k in knifes)
                    {
                        // 空值检查
                        if (k.WhLocation == null)
                        {
                            MSD.AddModelError("", $"刀具{k.SerialNumber}的库位信息不存在");
                            return;
                        }
                        if (k.WhLocation.WhArea == null)
                        {
                            MSD.AddModelError("", $"刀具{k.SerialNumber}的库区信息不存在");
                            return;
                        }

                        if (k.WhLocation.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}不属于当前登录存储地点 无法进行操作");
                            return;
                        }
                        if (k.Status != KnifeStatusEnum.GrindRequested)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}的状态不为修磨申请中 无法进行修磨出库");
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
                    //生成修磨出库单
                    var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                    var KnifeGrindOutDocNo = DocNoVm.GetSequence("KnifeGrindOutRule", tran);//生成归还单单号
                    var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人

                    Entity.DocNo = KnifeGrindOutDocNo;
                    Entity.HandledBy = handledBy;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.Status = KnifeOrderStatusEnum.Open;
                    Entity.U9PODocNo = info.PODocNO;
                    Entity.WareHouseId = whid;
                    Entity.KnifeGrindOutLine_KnifeGrindOut = new List<KnifeGrindOutLine>();
                    var currentTime = DateTime.Now;

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

                    foreach (var knife in knifes)
                    {
                        Entity.KnifeGrindOutLine_KnifeGrindOut.Add(new KnifeGrindOutLine
                        {
                            KnifeId = knife.ID,
                            Knife = knife,
                            FromWhLocationId = knife.WhLocationId,
                            Status = KnifeOrderStatusEnum.Open,
                        });
                    }

                    // 修磨出库后不关闭组合单，也不从组合单中移除刀具
                    // 组合单允许包含不同状态的刀具（部分修磨中、部分待出库）
                    // 组合单的完整性由组合单本身维护，不受单个刀具状态影响

                    DC.SaveChanges();

                    return;
                }
                catch (Exception e)
                {
                    MSD.AddModelError("", "" + e.Message);
                    return;
                }
            }

            
        }
        /// <summary>
        /// 修磨出库单审核
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        public void DoApproved(KnifeGrindOutInputInfo info, IDbContextTransaction tran)
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
                    Entity = DC.Set<KnifeGrindOut>()
                           .Include(x => x.KnifeGrindOutLine_KnifeGrindOut).ThenInclude(x => x.Knife)
                           //.Include(x=>x.HandledBy)
                           .First(x => x.ID == Entity.ID);
                    var currentTime = DateTime.Now;
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
                    //修改对象:   归还单自己  刀具的详细信息  操作记录-修磨出库
                    Entity.Status = KnifeOrderStatusEnum.Approved;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.HandledBy = handledBy;
                    Entity.ApprovedTime = currentTime;
                    Entity.UpdateBy = LoginUserInfo.Name;
                    Entity.UpdateTime = currentTime;

                    foreach (var line in Entity.KnifeGrindOutLine_KnifeGrindOut)
                    {
                        line.Status = KnifeOrderStatusEnum.Approved;//行状态开立变为已审核
                        var knife = line.Knife;
                        var usedays = knife.RemainingUsedDays;
                        //修改 刀具
                        knife.Status = KnifeStatusEnum.GrindingOut;
                        knife.HandledBy = Entity.HandledBy;
                        knife.HandledById = Entity.HandledById;
                        knife.HandledByName = Entity.HandledBy.Name;
                        knife.LastOperationDate = currentTime;
                        //刀具剩余天数归零
                        knife.RemainingUsedDays = 0;
                        knife.UpdateBy = LoginUserInfo.Name;
                        knife.UpdateTime = currentTime;
                        //新增 操作记录-
                        DC.Set<KnifeOperation>().Add(new KnifeOperation
                        {
                            KnifeId = knife.ID,
                            DocNo = Entity.DocNo,
                            OperationType = KnifeOperationTypeEnum.GrindOut,
                            OperationTime = currentTime,
                            OperationBy = handledBy_operator,
                            OperationById = handledBy_operator.ID,
                            HandledBy = Entity.HandledBy,
                            HandledById = Entity.HandledById,
                            HandledByName = Entity.HandledBy.Name,
                            UsedDays = usedays,
                            TotalUsedDays = line.Knife.TotalUsedDays,
                            RemainingDays = 0,
                            CurrentLife = line.Knife.CurrentLife,
                            WhLocationId = line.Knife.WhLocationId,
                            WhLocation = line.Knife.WhLocation,
                            GrindNum = line.Knife.GrindCount,
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime

                        });
                    }
                    #region 回写修磨申请
                    //修磨出库单涉及的刀
                    var knifes = Entity.KnifeGrindOutLine_KnifeGrindOut.Select(x => x.Knife).ToList();
                    // 修磨出库单涉及的刀对应的未关闭的修磨申请行
                    var grindRequestLines = DC.Set<KnifeGrindRequestLine>()
                       .Include(x => x.Knife)
                       .Include(x => x.KnifeGrindRequest).ThenInclude(x => x.KnifeGrindRequestLine_KnifeGrindRequest).ThenInclude(x => x.Knife)
                       .Where(x => knifes.Contains(x.Knife) && x.Status == KnifeOrderStatusEnum.Approved)//已审核未关闭的
                       .ToList();
                    if (knifes.Count != grindRequestLines.Count)
                    {
                        MSD.AddModelError("", "修磨出库的刀无法与未关闭的修磨申请行的刀一一对应 修磨出库失败");
                        return;
                    }
                    //修磨申请行关闭
                    foreach (var line in grindRequestLines)
                    {
                        line.Status = KnifeOrderStatusEnum.ApproveClose;
                    }
                    //如果所有行都已被回写 关闭单
                    var grindRequests = grindRequestLines
                       .Select(x => x.KnifeGrindRequest)
                       .Distinct()
                       .ToList();
                    foreach (var grindRequest in grindRequests)
                    {
                        var unCloseLines = grindRequest.KnifeGrindRequestLine_KnifeGrindRequest.Where(x => x.Status != KnifeOrderStatusEnum.ApproveClose).ToList();
                        if (unCloseLines.Count == 0)
                        {
                            grindRequest.Status = KnifeOrderStatusEnum.ApproveClose;
                        }
                    }
                    #endregion
                    #region 回写U9采购行子行 已修磨出库字段
                    //U9接口  采购行子行id
                    List<Int64> POShipLineIds = info.Lines.Select(x => Int64.Parse(x.POShipLineId)).ToList();
                    string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                    string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                    U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);//新刀领用固定组织
                    U9Return<List<string>> u9Return = apiHelper.POShipLineGrindOutDone(POShipLineIds);
                    if (!u9Return.Success)
                    {
                        MSD.AddModelError("", "修磨出库回写U9采购子行失败:" + u9Return.Msg);
                        return;
                    }
                    #endregion

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
                    Entity = DC.Set<KnifeGrindOut>()
                           .Include(x => x.KnifeGrindOutLine_KnifeGrindOut).ThenInclude(x => x.Knife)
                           //.Include(x=>x.HandledBy)
                           .First(x => x.ID == Entity.ID);
                    var currentTime = DateTime.Now;
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
                    //修改对象:   归还单自己  刀具的详细信息  操作记录-修磨出库
                    Entity.Status = KnifeOrderStatusEnum.Approved;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.HandledBy = handledBy;
                    Entity.ApprovedTime = currentTime;
                    Entity.UpdateBy = LoginUserInfo.Name;
                    Entity.UpdateTime = currentTime;

                    foreach (var line in Entity.KnifeGrindOutLine_KnifeGrindOut)
                    {
                        line.Status = KnifeOrderStatusEnum.Approved;//行状态开立变为已审核
                        var knife = line.Knife;
                        var usedays = knife.RemainingUsedDays;
                        //修改 刀具
                        knife.Status = KnifeStatusEnum.GrindingOut;
                        knife.HandledBy = Entity.HandledBy;
                        knife.HandledById = Entity.HandledById;
                        knife.HandledByName = Entity.HandledBy.Name;
                        knife.LastOperationDate = currentTime;
                        //刀具剩余天数归零
                        knife.RemainingUsedDays = 0;
                        knife.UpdateBy = LoginUserInfo.Name;
                        knife.UpdateTime = currentTime;
                        //新增 操作记录-
                        DC.Set<KnifeOperation>().Add(new KnifeOperation
                        {
                            KnifeId = knife.ID,
                            DocNo = Entity.DocNo,
                            OperationType = KnifeOperationTypeEnum.GrindOut,
                            OperationTime = currentTime,
                            OperationBy = handledBy_operator,
                            OperationById = handledBy_operator.ID,
                            HandledBy = Entity.HandledBy,
                            HandledById = Entity.HandledById,
                            HandledByName = Entity.HandledBy.Name,
                            UsedDays = usedays,
                            TotalUsedDays = line.Knife.TotalUsedDays,
                            RemainingDays = 0,
                            CurrentLife = line.Knife.CurrentLife,
                            WhLocationId = line.Knife.WhLocationId,
                            WhLocation = line.Knife.WhLocation,
                            GrindNum = line.Knife.GrindCount,
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime

                        });
                    }
                    #region 回写修磨申请
                    //修磨出库单涉及的刀
                    var knifes = Entity.KnifeGrindOutLine_KnifeGrindOut.Select(x => x.Knife).ToList();
                    var knifeIds = knifes.Select(x => x.ID).ToList();
                    // 修磨出库单涉及的刀对应的未关闭的修磨申请行
                    var grindRequestLines = DC.Set<KnifeGrindRequestLine>()
                       .Include(x => x.Knife)
                       .Include(x => x.KnifeGrindRequest).ThenInclude(x => x.KnifeGrindRequestLine_KnifeGrindRequest).ThenInclude(x => x.Knife)
                       .Where(x => knifeIds.Contains(x.KnifeId.Value) && x.Status == KnifeOrderStatusEnum.Approved)//已审核未关闭的
                       .ToList();
                    if (knifes.Count != grindRequestLines.Count)
                    {
                        // 检查是否每把刀具都有对应的修磨申请行
                        var foundKnifeIds = grindRequestLines.Select(x => x.KnifeId.Value).Distinct().ToList();
                        var missingKnifeIds = knifeIds.Where(id => !foundKnifeIds.Contains(id)).ToList();

                        if (missingKnifeIds.Count > 0)
                        {
                            // 有刀具缺少修磨申请行
                            var missingKnifes = knifes.Where(k => missingKnifeIds.Contains(k.ID)).ToList();
                            var missingSerialNumbers = string.Join(", ", missingKnifes.Select(k => k.SerialNumber));
                            MSD.AddModelError("", $"以下刀具没有对应的修磨申请行: {missingSerialNumbers}");
                            return;
                        }
                        // 如果每把刀具都有对应的申请行，只是数量不等（同一刀具有多行申请），则只关闭对应的行
                        // 重新筛选，每把刀具只取一行
                        grindRequestLines = knifeIds
                            .Select(knifeId => grindRequestLines.FirstOrDefault(x => x.KnifeId == knifeId))
                            .Where(x => x != null)
                            .ToList();
                    }
                    //修磨申请行关闭
                    // 记录本次出库涉及的修磨申请行ID
                    var closedLineIds = grindRequestLines.Select(x => x.ID).ToList();
                    foreach (var line in grindRequestLines)
                    {
                        line.Status = KnifeOrderStatusEnum.ApproveClose;
                    }
                    //如果所有行都已被回写 关闭单
                    // 获取本次出库涉及的修磨申请单ID列表
                    var grindRequestIds = grindRequestLines
                       .Select(x => x.KnifeGrindRequestId)
                       .Where(id => id.HasValue)
                       .Select(id => id.Value)
                       .Distinct()
                       .ToList();

                    // 直接从数据库查询修磨申请单（确保被EF Core跟踪）
                    var grindRequests = DC.Set<KnifeGrindRequest>()
                       .Where(x => grindRequestIds.Contains(x.ID))
                       .ToList();

                    foreach (var grindRequest in grindRequests)
                    {
                        // 从数据库查询该修磨申请单的所有已审核行，排除本次出库已关闭的行
                        // （因为本次修改还没有SaveChanges，数据库中的状态还是旧的）
                        var unCloseLinesCount = DC.Set<KnifeGrindRequestLine>()
                            .Where(x => x.KnifeGrindRequestId == grindRequest.ID &&
                                       x.Status == KnifeOrderStatusEnum.Approved &&
                                       !closedLineIds.Contains(x.ID)) // 排除本次已关闭的行
                            .Count();

                        if (unCloseLinesCount == 0)
                        {
                            grindRequest.Status = KnifeOrderStatusEnum.ApproveClose;
                        }
                    }
                    #endregion
                    #region 关闭组合单（如果不是全部刀具一起出库）
                    // 查询本次出库涉及的组合单
                    var outKnifeIds = Entity.KnifeGrindOutLine_KnifeGrindOut.Select(x => x.KnifeId).ToList();
                    var combineLines = DC.Set<KnifeCombineLine>()
                        .Include(x => x.KnifeCombine).ThenInclude(x => x.KnifeCombineLine_KnifeCombine)
                        .Where(x => outKnifeIds.Contains(x.KnifeId.Value))
                        .ToList();

                    // 按组合单分组
                    var combineGroups = combineLines.GroupBy(x => x.KnifeCombineId).ToList();
                    foreach (var group in combineGroups)
                    {
                        var knifeCombine = group.First().KnifeCombine;
                        if (knifeCombine == null || knifeCombine.Status != KnifeOrderStatusEnum.Approved)
                            continue;

                        // 获取该组合单中的刀具总数(所有刀具，不限状态)
                        var totalKnifesInCombine = DC.Set<KnifeCombineLine>()
                            .Where(x => x.KnifeCombineId == knifeCombine.ID)
                            .Count();

                        // 本次出库该组合单的刀具数量
                        var outCount = group.Count();

                        // 如果本次出库数量少于组合单总刀具数，关闭组合单
                        if (outCount < totalKnifesInCombine)
                        {
                            knifeCombine.Status = KnifeOrderStatusEnum.ApproveClose;
                            knifeCombine.CloseTime = currentTime; // 【修复】2025-12-18 设置组合单关闭时间
                        }
                    }
                    #endregion
                    #region 回写U9采购行子行 已修磨出库字段

                    // 只有传入Lines时才调用U9接口
                    if (info.Lines != null && info.Lines.Count > 0)
                    {
                        //U9接口  采购行子行id
                        List<Int64> POShipLineIds = info.Lines
                            .Where(x => !string.IsNullOrEmpty(x.POShipLineId))
                            .Select(x => Int64.Parse(x.POShipLineId))
                            .ToList();

                        if (POShipLineIds.Count > 0)
                        {
                            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);//新刀领用固定组织

                            U9Return<List<string>> u9Return = apiHelper.POShipLineGrindOutDone(POShipLineIds);
                            if (!u9Return.Success)
                            {
                                MSD.AddModelError("", "修磨出库回写U9采购子行失败:" + u9Return.Msg);
                                return;
                            }
                        }
                    }

                    #endregion

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
    }
}