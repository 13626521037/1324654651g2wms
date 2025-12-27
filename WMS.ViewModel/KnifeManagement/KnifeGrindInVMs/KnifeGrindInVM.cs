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
using WMS.Util;
using Microsoft.EntityFrameworkCore.Storage;
using Elsa;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.ViewModel.KnifeManagement.KnifeVMs;
using WMS.ViewModel.KnifeManagement.KnifeGrindInLineVMs;
using WMS.Model.BaseData;
using WMS.Util.U9Para.Knife;
namespace WMS.ViewModel.KnifeManagement.KnifeGrindInVMs
{
    public partial class KnifeGrindInVM : BaseCRUDVM<KnifeGrindIn>
    {

        public List<string> KnifeManagementKnifeGrindInFTempSelected { get; set; }
        public KnifeGrindInLineKnifeGrindInDetailListVM KnifeGrindInLineKnifeGrindInList { get; set; }
        public KnifeGrindInLineKnifeGrindInDetailListVM1 KnifeGrindInLineKnifeGrindInList1 { get; set; }
        public KnifeGrindInLineKnifeGrindInDetailListVM2 KnifeGrindInLineKnifeGrindInList2 { get; set; }
        public KnifeGrindInVM()
        {
            SetInclude(x => x.HandledBy);
            KnifeGrindInLineKnifeGrindInList = new KnifeGrindInLineKnifeGrindInDetailListVM();
            KnifeGrindInLineKnifeGrindInList.DetailGridPrix = "Entity.KnifeGrindInLine_KnifeGrindIn";
            KnifeGrindInLineKnifeGrindInList1 = new KnifeGrindInLineKnifeGrindInDetailListVM1();
            KnifeGrindInLineKnifeGrindInList1.DetailGridPrix = "Entity.KnifeGrindInLine_KnifeGrindIn";
            KnifeGrindInLineKnifeGrindInList2 = new KnifeGrindInLineKnifeGrindInDetailListVM2();
            KnifeGrindInLineKnifeGrindInList2.DetailGridPrix = "Entity.KnifeGrindInLine_KnifeGrindIn";
        }

        protected override void InitVM()
        {
            KnifeGrindInLineKnifeGrindInList.CopyContext(this);
            KnifeGrindInLineKnifeGrindInList1.CopyContext(this);
            KnifeGrindInLineKnifeGrindInList2.CopyContext(this);


        }

        public override DuplicatedInfo<KnifeGrindIn> SetDuplicatedCheck()
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
            if (Entity.KnifeGrindInLine_KnifeGrindIn.Count == 0)
            {
                MSD.AddModelError("", "请输入刀具");
                return;
            }
            if (Entity.KnifeGrindInLine_KnifeGrindIn.GroupBy(x => new { x.KnifeGrindInId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }

            base.Validate();
        }

        public override void DoAdd()
        {
            if (Entity.KnifeGrindInLine_KnifeGrindIn.Count == 0)
            {
                MSD.AddModelError("", "请输入刀具");
                return;
            }
            if (Entity.KnifeGrindInLine_KnifeGrindIn.GroupBy(x => new { x.KnifeGrindInId, x.KnifeId }).Any(x => x.Count() > 1))
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
        /// 修磨入库时通过收货单获取刀具信息
        /// </summary>
        /// <param name="RcvDocNo"></param>
        /// <returns></returns>
        public List<KnifeGrindInLineReturn> GetKnifesByRcvDocNo(string RcvDocNo)
        {
            var result = new List<KnifeGrindInLineReturn>();
            try
            {
                // 校验登录状态
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return null;
                }
                Guid whid;
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);

                //调u9接口  根据单号 获取请购行list
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);// 固定组织
                U9Return<List<GetKnifeInfosByRcvDocNoResult>> u9Return = (U9Return<List<GetKnifeInfosByRcvDocNoResult>>)apiHelper.GetKnifeInfosByRcvDocNo(RcvDocNo);
                if (!u9Return.Success)
                {
                    MSD.AddModelError("", "" + u9Return.Msg);
                    return null;
                }
                var knifeSerialNumbers = u9Return.Entity.Select(x => x.KnifeNO).ToList();
                var knifes = DC.Set<Knife>()
                    .Include(x => x.ItemMaster)
                    .Where(x => knifeSerialNumbers.Contains(x.SerialNumber))
                    .ToList();
                if (knifes.Count != u9Return.Entity.Count)
                {
                    MSD.AddModelError("", "收货单中存在无效刀具行 ");
                    return null;
                }
                if (knifes.Any(x => x.Status != KnifeStatusEnum.GrindingOut))
                {
                    MSD.AddModelError("", "存在刀具状态不为修磨出库 ");
                    return null;
                }

                foreach (var line in u9Return.Entity)
                {
                    var knife = knifes.FirstOrDefault(x => x.SerialNumber == line.KnifeNO);

                    // 每把刀具都按实际料号单独推荐库位（不区分组合单和单刀）
                    var recommendLocations = GetRecommendLocationsByItem(knife.ActualItemCode ?? knife.ItemMaster?.Code, whid, knife.ID);

                    result.Add(new KnifeGrindInLineReturn
                    {
                        KnifeId = knife.ID.ToString(),
                        SerialNumber = knife.SerialNumber,
                        U9RcvLineId = line.RcvlineId,
                        GrindKnifeNO = knife.GrindKnifeNO,
                        SuggestLocs = recommendLocations
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "" + e.Message);
                return null;
            }
        }
        /// <summary>
        /// 修磨入库单预处理
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        public void DoInitByInfo(KnifeGrindInInputInfo info, IDbContextTransaction tran)
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
                    //校验:传入参数非空
                    if (info is null || info.Lines is null || info.Lines.Count == 0 || string.IsNullOrEmpty(info.RcvDocNo))
                    {
                        MSD.AddModelError("", "修磨入库参数不足");
                        return;
                    }
                    //校验:传入参数行非空
                    foreach (var line in info.Lines)
                    {
                        if (string.IsNullOrEmpty(line.WhLocationId))
                        {
                            MSD.AddModelError("", "修磨入库参数不足");
                            return;
                        }
                        if (string.IsNullOrEmpty(line.U9RcvLineId))
                        {
                            MSD.AddModelError("", "修磨入库参数不足");
                            return;
                        }
                        if (string.IsNullOrEmpty(line.KnifeId))
                        {
                            MSD.AddModelError("", "修磨入库参数不足");
                            return;
                        }
                    }
                    //校验:有效性 刀具状态校验 
                    var knifeIds_string = info.Lines.Select(x => x.KnifeId).ToList();
                    var knifes = DC.Set<Knife>()
                        .Include(x => x.WhLocation)
                            .ThenInclude(x => x.WhArea)
                                .ThenInclude(x => x.WareHouse)
                        .CheckIDs(knifeIds_string)
                        .ToList();
                    if (knifes.Count != info.Lines.Count)
                    {
                        MSD.AddModelError("", $"刀具信息有误 存在失效或重复 无法进行操作");
                        return;
                    }
                    foreach (var k in knifes)
                    {
                        if (k.WhLocation.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}不属于当前登录存储地点 无法进行操作");
                            return;
                        }
                        if (k.Status != KnifeStatusEnum.GrindingOut)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}未修磨出库 无法进行修磨入库");
                            return;
                        }
                        //修磨入库检查原来的库位?可以考虑不检查 只检查归还的库位  原库位没有意义
                        /*if (k.WhLocation.Locked == true)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}所在的库位{k.WhLocation.Code}已锁定 不可用");
                            return;
                        }
                        if (k.WhLocation.IsEffective == EffectiveEnum.Ineffective || k.WhLocation.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"刀具{k.SerialNumber}所在库位{k.WhLocation.Code}无效");
                            return ;
                        }*/
                    }
                    //校验:有效性 存储地点 
                    var whlocationIds = info.Lines.Select(x => Guid.Parse(x.WhLocationId)).ToList();
                    var whlocations = DC.Set<BaseWhLocation>()
                            .Include(x => x.WhArea)
                                .ThenInclude(x => x.WareHouse)
                        .Where(x => whlocationIds.Contains(x.ID)).ToList();
                    foreach (var whlocation in whlocations)
                    {
                        if (whlocation.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", $"{whlocation.Name}不属于当前登录存储地点 无法进行操作");
                        }
                        if (whlocation.Locked == true)
                        {
                            MSD.AddModelError("", $"库位{whlocation.Code}已锁定 不可用");
                            return;
                        }
                        if (whlocation.IsEffective == EffectiveEnum.Ineffective || whlocation.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"库位{whlocation.Code}无效");
                            return;
                        }
                    }
                    //生成修磨入库单
                    var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                    var KnifeGrindInDocNo = DocNoVm.GetSequence("KnifeGrindInRule", tran);//生成归还单单号
                    var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人

                    Entity.DocNo = KnifeGrindInDocNo;
                    Entity.HandledBy = handledBy;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.Status = KnifeOrderStatusEnum.Open;
                    Entity.U9RcvDocNo = info.RcvDocNo;
                    Entity.WareHouseId = whid;
                    Entity.KnifeGrindInLine_KnifeGrindIn = new List<KnifeGrindInLine>();
                    foreach (var line in info.Lines)
                    {
                        var knife = knifes.FirstOrDefault(x => x.ID == Guid.Parse(line.KnifeId));
                        Entity.KnifeGrindInLine_KnifeGrindIn.Add(new KnifeGrindInLine
                        {
                            KnifeId = Guid.Parse(line.KnifeId),
                            ToWhLocationId = Guid.Parse(line.WhLocationId),
                            FromWhLocationId = knife.WhLocationId,
                        }
                        );
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

                    //校验:收货单号非空
                    if (info is null || string.IsNullOrEmpty(info.RcvDocNo))
                    {
                        MSD.AddModelError("", "修磨入库参数不足 请填写收货单号");
                        return;
                    }

                    // 获取刀具列表：优先使用组合条码，其次使用Lines
                    List<Knife> knifes = new List<Knife>();
                    Guid? unifiedWhLocationId = null; // 统一库位

                    // 【修改】优先尝试从WhLocationId字段获取统一库位
                    if (!string.IsNullOrEmpty(info.WhLocationId))
                    {
                        Guid tempId;
                        if (Guid.TryParse(info.WhLocationId, out tempId))
                        {
                            unifiedWhLocationId = tempId;
                        }
                        else
                        {
                            // 尝试通过库位编码查询
                            var whlocation = DC.Set<BaseWhLocation>()
                                .FirstOrDefault(x => x.Code == info.WhLocationId || x.Name == info.WhLocationId);
                            if (whlocation != null)
                            {
                                unifiedWhLocationId = whlocation.ID;
                            }
                        }
                    }
                    // 如果WhLocationId字段为空，再尝试从Lines中获取统一库位（兼容旧格式）
                    else if (info.Lines != null && info.Lines.Count > 0)
                    {
                        var distinctWhLocationIds = info.Lines
                            .Where(x => !string.IsNullOrEmpty(x.WhLocationId))
                            .Select(x => x.WhLocationId)
                            .Distinct()
                            .ToList();

                        // 如果只有一个库位，则设置为统一库位
                        if (distinctWhLocationIds.Count == 1)
                        {
                            Guid tempId;
                            if (Guid.TryParse(distinctWhLocationIds[0], out tempId))
                            {
                                unifiedWhLocationId = tempId;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(info.CombineCode))
                    {
                        // 使用组合条码查询刀具，支持多种匹配方式
                        var knifeCombine = DC.Set<KnifeCombine>()
                            .Include(x => x.KnifeCombineLine_KnifeCombine)
                                .ThenInclude(x => x.Knife)
                                    .ThenInclude(x => x.WhLocation)
                                        .ThenInclude(x => x.WhArea)
                                            .ThenInclude(x => x.WareHouse)
                            .Where(x => x.DocNo == info.CombineCode ||
                                       x.CombineKnifeNo == info.CombineCode ||
                                       (x.CombineKnifeNo != null && x.CombineKnifeNo.Contains(info.CombineCode)) ||
                                       (x.CombineKnifeNo != null && x.CombineKnifeNo.EndsWith(info.CombineCode)))
                            .FirstOrDefault();

                        if (knifeCombine == null)
                        {
                            MSD.AddModelError("", $"未找到组合单: {info.CombineCode}");
                            return;
                        }

                        // 确保组合单行不为空
                        if (knifeCombine.KnifeCombineLine_KnifeCombine == null || knifeCombine.KnifeCombineLine_KnifeCombine.Count == 0)
                        {
                            MSD.AddModelError("", $"组合单{info.CombineCode}中没有刀具");
                            return;
                        }

                        // 获取组合单中所有"修磨中"状态的刀具
                        var combineKnifes = knifeCombine.KnifeCombineLine_KnifeCombine
                            .Where(x => x != null && x.Knife != null)
                            .Where(x => x.Knife.Status == KnifeStatusEnum.GrindingOut)
                            .Select(x => x.Knife)
                            .ToList();

                        if (combineKnifes.Count == 0)
                        {
                            MSD.AddModelError("", $"组合单{info.CombineCode}中没有可入库的刀具（所有刀具可能已入库）");
                            return;
                        }

                        // 使用组合单中的所有修磨中刀具
                        knifes = combineKnifes;

                        // 使用组合条码时，必须有统一库位（优先使用WhLocationId字段，其次从Lines中提取）
                        if (unifiedWhLocationId == null)
                        {
                            MSD.AddModelError("", "使用组合条码时，必须传入统一库位ID (WhLocationId)");
                            return;
                        }
                    }
                    else if (info.Lines != null && info.Lines.Count > 0)
                    {
                        // 使用Lines查询刀具
                        foreach (var line in info.Lines)
                        {
                            // 如果没有统一库位，则每行必须有库位
                            if (unifiedWhLocationId == null && string.IsNullOrEmpty(line.WhLocationId))
                            {
                                MSD.AddModelError("", "修磨入库参数不足 请传入库位ID");
                                return;
                            }
                            if (string.IsNullOrEmpty(line.KnifeId))
                            {
                                MSD.AddModelError("", "修磨入库参数不足 请传入刀具ID");
                                return;
                            }
                        }

                        var knifeIds_string = info.Lines.Select(x => x.KnifeId).ToList();
                        knifes = DC.Set<Knife>()
                            .Include(x => x.WhLocation)
                                .ThenInclude(x => x.WhArea)
                                    .ThenInclude(x => x.WareHouse)
                            .CheckIDs(knifeIds_string)
                            .ToList();
                    }
                    else
                    {
                        MSD.AddModelError("", "修磨入库参数不足 请传入组合条码或刀具信息");
                        return;
                    }

                    if (knifes.Count == 0)
                    {
                        MSD.AddModelError("", "未找到有效的刀具");
                        return;
                    }

                    // 校验刀具状态
                    foreach (var k in knifes)
                    {
                        if (k.WhLocation == null || k.WhLocation.WhArea == null)
                        {
                            MSD.AddModelError("", $"刀具{k.SerialNumber}的库位信息不存在");
                            return;
                        }
                        if (k.WhLocation.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}不属于当前登录存储地点 无法进行操作");
                            return;
                        }
                        if (k.Status != KnifeStatusEnum.GrindingOut)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}未修磨出库 无法进行修磨入库");
                            return;
                        }
                    }

                    // 校验库位
                    List<Guid> whlocationIds = new List<Guid>();
                    Dictionary<string, Guid> whLocationCodeToIdMap = new Dictionary<string, Guid>(); // 库位编码到ID的映射

                    if (unifiedWhLocationId != null)
                    {
                        whlocationIds.Add(unifiedWhLocationId.Value);
                    }
                    else if (info.Lines != null)
                    {
                        // 处理Lines中的库位ID（支持GUID或库位编码）
                        foreach (var line in info.Lines)
                        {
                            if (string.IsNullOrEmpty(line.WhLocationId)) continue;

                            if (Guid.TryParse(line.WhLocationId, out Guid parsedId))
                            {
                                if (!whlocationIds.Contains(parsedId))
                                {
                                    whlocationIds.Add(parsedId);
                                }
                                whLocationCodeToIdMap[line.WhLocationId] = parsedId;
                            }
                            else
                            {
                                // 尝试通过库位编码查询
                                var whlocation = DC.Set<BaseWhLocation>()
                                    .FirstOrDefault(x => x.Code == line.WhLocationId || x.Name == line.WhLocationId);
                                if (whlocation == null)
                                {
                                    MSD.AddModelError("", $"未找到库位: {line.WhLocationId}");
                                    return;
                                }
                                if (!whlocationIds.Contains(whlocation.ID))
                                {
                                    whlocationIds.Add(whlocation.ID);
                                }
                                whLocationCodeToIdMap[line.WhLocationId] = whlocation.ID;
                            }
                        }
                    }

                    var whlocations = DC.Set<BaseWhLocation>()
                        .Include(x => x.WhArea)
                            .ThenInclude(x => x.WareHouse)
                        .Where(x => whlocationIds.Contains(x.ID)).ToList();

                    foreach (var whlocation in whlocations)
                    {
                        if (whlocation.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", $"{whlocation.Name}不属于当前登录存储地点 无法进行操作");
                            return;
                        }
                        if (whlocation.Locked == true)
                        {
                            MSD.AddModelError("", $"库位{whlocation.Code}已锁定 不可用");
                            return;
                        }
                        if (whlocation.IsEffective == EffectiveEnum.Ineffective || whlocation.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"库位{whlocation.Code}无效");
                            return;
                        }

                    }

                    //生成修磨入库单
                    var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                    var KnifeGrindInDocNo = DocNoVm.GetSequence("KnifeGrindInRule", tran);//生成归还单单号
                    var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人

                    Entity.DocNo = KnifeGrindInDocNo;
                    Entity.HandledBy = handledBy;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.Status = KnifeOrderStatusEnum.Open;
                    Entity.U9RcvDocNo = info.RcvDocNo;
                    Entity.WareHouseId = whid;
                    Entity.KnifeGrindInLine_KnifeGrindIn = new List<KnifeGrindInLine>();
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
                        // 确定入库库位
                        Guid toWhLocationId;
                        if (unifiedWhLocationId != null)
                        {
                            toWhLocationId = unifiedWhLocationId.Value;
                        }
                        else
                        {
                            // 确保 info.Lines 不为 null
                            if (info.Lines == null || info.Lines.Count == 0)
                            {
                                MSD.AddModelError("", $"刀具{knife.SerialNumber}未指定入库库位");
                                return;
                            }

                            var line = info.Lines.FirstOrDefault(x => x.KnifeId == knife.ID.ToString());
                            if (line == null || string.IsNullOrEmpty(line.WhLocationId))
                            {
                                MSD.AddModelError("", $"刀具{knife.SerialNumber}未指定入库库位");
                                return;
                            }
                            // 使用映射字典获取库位ID
                            if (whLocationCodeToIdMap.ContainsKey(line.WhLocationId))
                            {
                                toWhLocationId = whLocationCodeToIdMap[line.WhLocationId];
                            }
                            else if (Guid.TryParse(line.WhLocationId, out Guid parsedId))
                            {
                                toWhLocationId = parsedId;
                            }
                            else
                            {
                                MSD.AddModelError("", $"库位{line.WhLocationId}无效");
                                return;
                            }
                        }

                        Entity.KnifeGrindInLine_KnifeGrindIn.Add(new KnifeGrindInLine
                        {
                            KnifeId = knife.ID,
                            ToWhLocationId = toWhLocationId,
                            FromWhLocationId = knife.WhLocationId,
                        });
                    }

                    // 修磨入库后不关闭组合单，也不从组合单中移除刀具
                    // 组合单允许包含不同状态的刀具（部分已入库、部分修磨中）
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
        /// 修磨入库单审核
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        public void DoApproved(KnifeGrindInInputInfo info, IDbContextTransaction tran)
        {
            var currentVersion = DC.Set<BaseSysPara>().FirstOrDefault(x => x.Code == "KnifeGrindCodeVersion");
            if (currentVersion == null)
            {
                MSD.AddModelError("", "未检测到刀具修磨代码版本 请联系管理员");
                return;
            }
            if (currentVersion.Value == "0")//原来的
            {
                //审核 该自己  改刀具  加操作(收货行)  审核收货单  更新刀具平均寿命表(并不)
                try
                {
                    Entity = DC.Set<KnifeGrindIn>()
                        .Include(x => x.KnifeGrindInLine_KnifeGrindIn).ThenInclude(x => x.Knife).ThenInclude(x => x.ItemMaster)
                        .Where(x => x.ID == Entity.ID).FirstOrDefault();
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
                    Entity.Status = KnifeOrderStatusEnum.Approved;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.ApprovedTime = currentTime;
                    Entity.UpdateBy = LoginUserInfo.Name;
                    Entity.UpdateTime = currentTime;

                    //U9访问寿命接口 取寿命表 一会给刀具的修磨寿命赋值 一次访问所有料号
                    var itemCodes = Entity.KnifeGrindInLine_KnifeGrindIn.Select(x => x.Knife.ItemMaster.Code).Distinct().ToList();
                    //var itemLives = GetKnifesLivesSV.GetKnifesLives_1(itemCodes);
                    var itemLivesReturn = Wtm.CreateVM<KnifeVM>().GetKnifesLives_2(itemCodes);
                    if (itemLivesReturn is null)
                    {
                        MSD.AddModelError("", "获取U9刀具寿命失败");
                        return;
                    }
                    if (itemLivesReturn.Success == false)
                    {
                        MSD.AddModelError("", "" + itemLivesReturn.Message);
                        return;
                    }

                    foreach (var line in Entity.KnifeGrindInLine_KnifeGrindIn)
                    {
                        var knife = line.Knife;
                        int currentLife = 0;


                        //修改 刀具
                        knife.Status = KnifeStatusEnum.InStock;
                        knife.HandledBy = Entity.HandledBy;
                        knife.HandledById = Entity.HandledById;
                        knife.LastOperationDate = currentTime;
                        knife.WhLocationId = line.ToWhLocationId;
                        knife.GrindCount += 1;
                        switch (knife.GrindCount)
                        {
                            case 1:
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.FirstDepreciationLife).First();
                                break;
                            case 2:
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.SecondDepreciationLife).First();
                                break;
                            case 3:
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.ThirdDepreciationLife).First();
                                break;
                            case 4:
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.FourthDepreciationLife).First();
                                break;
                            case 5:
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.FifthDepreciationLife).First();
                                break;
                            default://5次以及之后都使用第五次的数据
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.FifthDepreciationLife).First();
                                break;
                        }

                        knife.CurrentLife = currentLife;
                        knife.RemainingUsedDays = currentLife;

                        knife.UpdateBy = LoginUserInfo.Name;
                        knife.UpdateTime = currentTime;
                        //新增 操作记录-
                        DC.Set<KnifeOperation>().Add(new KnifeOperation
                        {
                            KnifeId = knife.ID,
                            DocNo = Entity.DocNo,
                            OperationType = KnifeOperationTypeEnum.GrindingIn,
                            OperationTime = currentTime,
                            OperationBy = handledBy_operator,
                            OperationById = handledBy_operator.ID,
                            HandledBy = Entity.HandledBy,
                            HandledById = Entity.HandledById,
                            UsedDays = 0,
                            TotalUsedDays = knife.TotalUsedDays,
                            RemainingDays = knife.RemainingUsedDays,
                            CurrentLife = knife.CurrentLife,
                            WhLocationId = line.ToWhLocationId,
                            GrindNum = knife.GrindCount,
                            U9SourceLineID = info.Lines.Where(x => Guid.Parse(x.KnifeId) == knife.ID).Select(x => x.U9RcvLineId).FirstOrDefault(),
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime,
                        });

                    }
                    //回写修磨出库行  行和头的关闭逻辑
                    //修磨入库单的刀
                    var knifes = Entity.KnifeGrindInLine_KnifeGrindIn.Select(x => x.Knife).ToList();
                    var grindOutLines = DC.Set<KnifeGrindOutLine>()
                       .Include(x => x.Knife)
                       .Include(x => x.KnifeGrindOut).ThenInclude(x => x.KnifeGrindOutLine_KnifeGrindOut).ThenInclude(x => x.Knife)
                       .Where(x => knifes.Contains(x.Knife) && x.Status == KnifeOrderStatusEnum.Approved)//已审核未关闭的
                       .ToList();
                    if (knifes.Count != grindOutLines.Count)
                    {
                        MSD.AddModelError("", "修磨入库的刀无法与未关闭的修磨出库行的刀一一对应 修磨入库失败");
                        return;
                    }
                    //修磨出库行关闭
                    foreach (var line in grindOutLines)
                    {
                        line.Status = KnifeOrderStatusEnum.ApproveClose;
                    }
                    //如果所有行都已被回写 关闭单
                    var grindOuts = grindOutLines
                       .Select(x => x.KnifeGrindOut)
                       .Distinct()
                       .ToList();
                    foreach (var grindOut in grindOuts)
                    {
                        var unCloseLines = grindOut.KnifeGrindOutLine_KnifeGrindOut.Where(x => x.Status != KnifeOrderStatusEnum.ApproveClose).ToList();
                        if (unCloseLines.Count == 0)
                        {
                            grindOut.Status = KnifeOrderStatusEnum.ApproveClose;
                        }
                    }

                    //调用U9接口  审核收货单
                    string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                    string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                    U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);
                    U9Return u9Return = apiHelper.ApproveRcv(info.RcvDocNo);
                    if (!u9Return.Success)
                    {
                        MSD.AddModelError("", u9Return.Msg);
                        return;
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

                //审核 该自己  改刀具  加操作(收货行)  审核收货单  更新刀具平均寿命表(并不)
                try
                {
                    Entity = DC.Set<KnifeGrindIn>()
                        .Include(x => x.KnifeGrindInLine_KnifeGrindIn).ThenInclude(x => x.Knife).ThenInclude(x => x.ItemMaster)
                        .Where(x => x.ID == Entity.ID).FirstOrDefault();
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
                    Entity.Status = KnifeOrderStatusEnum.Approved;
                    Entity.HandledById = handledBy.ID.ToString();
                    Entity.ApprovedTime = currentTime;
                    Entity.UpdateBy = LoginUserInfo.Name;
                    Entity.UpdateTime = currentTime;

                    //U9访问寿命接口 取寿命表 一会给刀具的修磨寿命赋值 一次访问所有料号
                    var itemCodes = Entity.KnifeGrindInLine_KnifeGrindIn.Select(x => x.Knife.ItemMaster.Code).Distinct().ToList();
                    //var itemLives = GetKnifesLivesSV.GetKnifesLives_1(itemCodes);
                    var itemLivesReturn = Wtm.CreateVM<KnifeVM>().GetKnifesLives_2(itemCodes);
                    if (itemLivesReturn is null)
                    {
                        MSD.AddModelError("", "获取U9刀具寿命失败");
                        return;
                    }
                    if (itemLivesReturn.Success == false)
                    {
                        MSD.AddModelError("", "" + itemLivesReturn.Message);
                        return;
                    }

                    foreach (var line in Entity.KnifeGrindInLine_KnifeGrindIn)
                    {
                        var knife = line.Knife;
                        int currentLife = 0;


                        //修改 刀具
                        knife.Status = KnifeStatusEnum.InStock;
                        knife.HandledBy = Entity.HandledBy;
                        knife.HandledById = Entity.HandledById;
                        knife.LastOperationDate = currentTime;
                        knife.WhLocationId = line.ToWhLocationId;
                        knife.GrindCount += 1;
                        switch (knife.GrindCount)
                        {
                            case 1:
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.FirstDepreciationLife).First();
                                break;
                            case 2:
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.SecondDepreciationLife).First();
                                break;
                            case 3:
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.ThirdDepreciationLife).First();
                                break;
                            case 4:
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.FourthDepreciationLife).First();
                                break;
                            case 5:
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.FifthDepreciationLife).First();
                                break;
                            default://5次以及之后都使用第五次的数据
                                currentLife = (int)itemLivesReturn.Data.Where(x => x.ItemMaster == line.Knife.ItemMaster.Code).Select(x => x.FifthDepreciationLife).First();
                                break;
                        }

                        knife.CurrentLife = currentLife;
                        knife.RemainingUsedDays = currentLife;

                        knife.UpdateBy = LoginUserInfo.Name;
                        knife.UpdateTime = currentTime;
                        //新增 操作记录-
                        // 获取U9收货行ID：优先从Lines获取，如果没有Lines则为null
                        string u9SourceLineId = null;
                        if (info.Lines != null && info.Lines.Count > 0)
                        {
                            u9SourceLineId = info.Lines
                                .Where(x => !string.IsNullOrEmpty(x.KnifeId) && Guid.TryParse(x.KnifeId, out var knifeId) && knifeId == knife.ID)
                                .Select(x => x.U9RcvLineId)
                                .FirstOrDefault();
                        }

                        DC.Set<KnifeOperation>().Add(new KnifeOperation
                        {
                            KnifeId = knife.ID,
                            DocNo = Entity.DocNo,
                            OperationType = KnifeOperationTypeEnum.GrindingIn,
                            OperationTime = currentTime,
                            OperationBy = handledBy_operator,
                            OperationById = handledBy_operator.ID,
                            HandledBy = Entity.HandledBy,
                            HandledById = Entity.HandledById,
                            UsedDays = 0,
                            TotalUsedDays = knife.TotalUsedDays,
                            RemainingDays = knife.RemainingUsedDays,
                            CurrentLife = knife.CurrentLife,
                            WhLocationId = line.ToWhLocationId,
                            GrindNum = knife.GrindCount,
                            U9SourceLineID = u9SourceLineId,
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime,
                        });

                    }
                    //回写修磨出库行  行和头的关闭逻辑
                    //修磨入库单的刀
                    var knifes = Entity.KnifeGrindInLine_KnifeGrindIn.Select(x => x.Knife).ToList();
                    var grindOutLines = DC.Set<KnifeGrindOutLine>()
                       .Include(x => x.Knife)
                       .Include(x => x.KnifeGrindOut).ThenInclude(x => x.KnifeGrindOutLine_KnifeGrindOut).ThenInclude(x => x.Knife)
                       .Where(x => knifes.Contains(x.Knife) && x.Status == KnifeOrderStatusEnum.Approved)//已审核未关闭的
                       .ToList();
                    if (knifes.Count != grindOutLines.Count)
                    {
                        MSD.AddModelError("", "修磨入库的刀无法与未关闭的修磨出库行的刀一一对应 修磨入库失败");
                        return;
                    }
                    //修磨出库行关闭
                    foreach (var line in grindOutLines)
                    {
                        line.Status = KnifeOrderStatusEnum.ApproveClose;
                    }
                    //如果所有行都已被回写 关闭单
                    var grindOuts = grindOutLines
                       .Select(x => x.KnifeGrindOut)
                       .Distinct()
                       .ToList();
                    foreach (var grindOut in grindOuts)
                    {
                        var unCloseLines = grindOut.KnifeGrindOutLine_KnifeGrindOut.Where(x => x.Status != KnifeOrderStatusEnum.ApproveClose).ToList();
                        if (unCloseLines.Count == 0)
                        {
                            grindOut.Status = KnifeOrderStatusEnum.ApproveClose;
                        }
                    }

                    #region 关闭组合单（如果不是全部刀具一起入库）
                    // 查询本次入库涉及的组合单
                    var inKnifeIds = Entity.KnifeGrindInLine_KnifeGrindIn.Select(x => x.KnifeId).ToList();
                    var combineLines = DC.Set<KnifeCombineLine>()
                        .Include(x => x.KnifeCombine).ThenInclude(x => x.KnifeCombineLine_KnifeCombine)
                        .Where(x => inKnifeIds.Contains(x.KnifeId.Value))
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

                        // 本次入库该组合单的刀具数量
                        var inCount = group.Count();

                        // 如果本次入库数量少于组合单总刀具数，关闭组合单
                        if (inCount < totalKnifesInCombine)
                        {
                            knifeCombine.Status = KnifeOrderStatusEnum.ApproveClose;
                            knifeCombine.CloseTime = currentTime; // 【修复】2025-12-18 设置组合单关闭时间
                        }
                    }
                    #endregion

                    //调用U9接口  审核收货单
                    string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                    string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                    U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);

                    // 检查是否为虚假收货单号，如果是则跳过U9调用
                    if (!Entity.U9RcvDocNo.StartsWith("RCV"))
                    {
                        U9Return u9Return = apiHelper.ApproveRcv(info.RcvDocNo);
                        if (!u9Return.Success)
                        {
                            MSD.AddModelError("", u9Return.Msg);
                            return;
                        }
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

        }


        /// <summary>
        /// 获取推荐库位（修磨入库专用推荐逻辑）
        /// 推荐逻辑：
        /// 1. 优先推荐该刀具修磨出库前的原库位（从操作记录中获取最近一次修磨出库操作的库位）
        /// 2. 如果没有找到原库位，查找相同实际料号刀具的最近出库库位
        /// 3. 查找有相同实际料号在库刀具的库位（按数量升序）
        /// 4. 如果没有相同实际料号在库，根据刀具分类查找相似刀具所在库位
        /// 5. 如果没有相似分类，则推荐空库位
        /// </summary>
        /// <param name="actualItemCode">实际料号编码</param>
        /// <param name="whid">仓库ID</param>
        /// <param name="knifeId">刀具ID（可选，用于查找该刀具的原库位）</param>
        /// <returns>推荐库位列表</returns>
        private List<RecommendLocationReturn> GetRecommendLocationsByItem(string actualItemCode, Guid whid, Guid? knifeId = null)
        {
            try
            {
                // 实际料号为空时直接返回空列表
                if (string.IsNullOrEmpty(actualItemCode))
                {
                    return new List<RecommendLocationReturn>();
                }

                // 第一步：如果提供了刀具ID，优先查找该刀具修磨出库前的原库位
                if (knifeId.HasValue)
                {
                    var lastGrindOutOperation = DC.Set<KnifeOperation>()
                        .Include(x => x.WhLocation)
                            .ThenInclude(x => x.WhArea)
                        .Where(x => x.KnifeId == knifeId.Value)
                        .Where(x => x.OperationType == KnifeOperationTypeEnum.GrindOut)  // 修磨出库操作
                        .Where(x => x.WhLocationId != null)  // 库位不为空
                        .Where(x => x.WhLocation.WhArea.WareHouseId == whid)  // 当前存储地点
                        .Where(x => x.WhLocation.IsEffective == EffectiveEnum.Effective)  // 库位有效
                        .Where(x => x.WhLocation.Locked != true)  // 库位未锁定
                        .Where(x => x.WhLocation.AreaType == WhLocationEnum.Normal)  // 正常库位
                        .Where(x => !x.WhLocation.Code.Contains("A9999999"))  // 排除不良退回库位
                        .OrderByDescending(x => x.OperationTime)  // 按操作时间降序，取最近一次
                        .FirstOrDefault();

                    if (lastGrindOutOperation != null)
                    {
                        // 统计该库位当前的刀具数量
                        var knifeCount = DC.Set<Knife>()
                            .Where(k => k.WhLocationId == lastGrindOutOperation.WhLocationId && k.Status == KnifeStatusEnum.InStock)
                            .Count();

                        var originalLocationReturn = new RecommendLocationReturn
                        {
                            AreaCode = lastGrindOutOperation.WhLocation.WhArea.Code,
                            LocCode = lastGrindOutOperation.WhLocation.Code,
                            Qty = knifeCount
                        };

                        // 补充一个相同实际料号的库位（优先）或空库位
                        // 查找相同实际料号的其他库位
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
                            .Where(x => x.WhLocationId != lastGrindOutOperation.WhLocationId)  // 排除原库位
                            .Where(x => x.ActualItemCode == actualItemCode)  // 按实际料号筛选
                            .Where(x => x.ItemMaster.Organization.Code == "0410")  // 限定组织0410
                            .AsQueryable();

                        var additionalSameItemLocation = sameItemQueryStep1
                            .GroupBy(x => new
                            {
                                WhLocationId = x.WhLocationId,
                                LocCode = x.WhLocation.Code,
                                AreaCode = x.WhLocation.WhArea.Code
                            })
                            .Select(g => new 
                            {
                                g.Key.WhLocationId,
                                g.Key.AreaCode,
                                g.Key.LocCode,
                                Qty = g.Count()
                            })
                            .OrderBy(x => x.Qty)  // 按数量升序，数量少的优先
                            .FirstOrDefault();

                        // 如果找到相同实际料号的库位，返回原库位+相同实际料号库位
                        if (additionalSameItemLocation != null)
                        {
                            return new List<RecommendLocationReturn>
                            {
                                originalLocationReturn,
                                new RecommendLocationReturn
                                {
                                    AreaCode = additionalSameItemLocation.AreaCode,
                                    LocCode = additionalSameItemLocation.LocCode,
                                    Qty = additionalSameItemLocation.Qty
                                }
                            };
                        }

                        // 如果没有相同实际料号的库位，补充一个空库位
                        var emptyLocationData = DC.Set<BaseWhLocation>()
                            .Include(x => x.WhArea)
                                .ThenInclude(x => x.WareHouse)
                            .Where(x => x.WhArea.WareHouseId == whid)
                            .Where(x => x.IsEffective == EffectiveEnum.Effective)
                            .Where(x => x.Locked != true)
                            .Where(x => x.AreaType == WhLocationEnum.Normal)
                            .Where(x => !x.Code.Contains("A9999999"))
                            .Where(x => x.ID != lastGrindOutOperation.WhLocationId)  // 排除原库位
                            .Where(x => !DC.Set<Knife>().Any(k => k.WhLocationId == x.ID && k.Status == KnifeStatusEnum.InStock))  // 只要空库位
                            .Select(loc => new { loc.ID, loc.WhArea.Code, LocCode = loc.Code })
                            .FirstOrDefault();

                        if (emptyLocationData != null)
                        {
                            return new List<RecommendLocationReturn>
                            {
                                originalLocationReturn,
                                new RecommendLocationReturn
                                {
                                    AreaCode = emptyLocationData.Code,
                                    LocCode = emptyLocationData.LocCode,
                                    Qty = 0
                                }
                            };
                        }

                        // 如果连空库位都没有，只返回原库位
                        return new List<RecommendLocationReturn> { originalLocationReturn };
                    }
                }

                // 第二步：查找相同实际料号刀具的最近出库库位
                var recentCheckOutQuery = DC.Set<KnifeOperation>()
                    .Include(x => x.Knife)
                        .ThenInclude(x => x.ItemMaster)
                            .ThenInclude(x => x.Organization)
                    .Include(x => x.WhLocation)
                        .ThenInclude(x => x.WhArea)
                    .Where(x => x.WhLocation.WhArea.WareHouseId == whid)  // 当前仓库
                    .Where(x => x.OperationType == KnifeOperationTypeEnum.CheckOut || x.OperationType == KnifeOperationTypeEnum.GrindOut)  // 出库或修磨出库操作
                    .Where(x => x.WhLocationId != null)  // 库位不为空
                    .Where(x => x.WhLocation.IsEffective == EffectiveEnum.Effective)  // 库位有效
                    .Where(x => x.WhLocation.Locked != true)  // 库位未锁定
                    .Where(x => x.WhLocation.AreaType == WhLocationEnum.Normal)  // 正常库位
                    .Where(x => !x.WhLocation.Code.Contains("A9999999"))  // 排除不良退回库位
                    .Where(x => x.Knife.ActualItemCode == actualItemCode)  // 按实际料号筛选
                    .Where(x => x.Knife.ItemMaster.Organization.Code == "0410")  // 限定组织0410
                    .AsQueryable();

                var recentLocation = recentCheckOutQuery
                    .OrderByDescending(x => x.OperationTime)  // 按操作时间降序
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
                        Qty = knifeCount
                    };

                    // 补充一个相同实际料号的库位或空库位
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
                        .Where(x => x.ActualItemCode == actualItemCode)  // 按实际料号筛选
                        .Where(x => x.ItemMaster.Organization.Code == "0410")  // 限定组织0410
                        .AsQueryable();

                    var additionalSameItemLocation = sameItemQueryStep2
                        .GroupBy(x => new
                        {
                            WhLocationId = x.WhLocationId,
                            LocCode = x.WhLocation.Code,
                            AreaCode = x.WhLocation.WhArea.Code
                        })
                        .Select(g => new 
                        {
                            g.Key.WhLocationId,
                            g.Key.AreaCode,
                            g.Key.LocCode,
                            Qty = g.Count()
                        })
                        .OrderBy(x => x.Qty)
                        .FirstOrDefault();

                    if (additionalSameItemLocation != null)
                    {
                        return new List<RecommendLocationReturn>
                        {
                            recentLocationReturn,
                            new RecommendLocationReturn
                            {
                                AreaCode = additionalSameItemLocation.AreaCode,
                                LocCode = additionalSameItemLocation.LocCode,
                                Qty = additionalSameItemLocation.Qty
                            }
                        };
                    }

                    // 如果没有相同实际料号的库位，补充一个空库位
                    var emptyLocationData2 = DC.Set<BaseWhLocation>()
                        .Include(x => x.WhArea)
                            .ThenInclude(x => x.WareHouse)
                        .Where(x => x.WhArea.WareHouseId == whid)
                        .Where(x => x.IsEffective == EffectiveEnum.Effective)
                        .Where(x => x.Locked != true)
                        .Where(x => x.AreaType == WhLocationEnum.Normal)
                        .Where(x => !x.Code.Contains("A9999999"))
                        .Where(x => x.ID != recentLocation.WhLocation.ID)
                        .Where(x => !DC.Set<Knife>().Any(k => k.WhLocationId == x.ID && k.Status == KnifeStatusEnum.InStock))
                        .Select(loc => new { loc.ID, loc.WhArea.Code, LocCode = loc.Code })
                        .FirstOrDefault();

                    if (emptyLocationData2 != null)
                    {
                        return new List<RecommendLocationReturn>
                        {
                            recentLocationReturn,
                            new RecommendLocationReturn
                            {
                                AreaCode = emptyLocationData2.Code,
                                LocCode = emptyLocationData2.LocCode,
                                Qty = 0
                            }
                        };
                    }

                    // 如果连空库位都没有，只返回最近出库的库位
                    return new List<RecommendLocationReturn> { recentLocationReturn };
                }

                // 获取当前实际料号的分类ID（用于后续步骤）
                Guid? itemCategoryId = DC.Set<BaseItemMaster>()
                    .Where(x => x.Code == actualItemCode)
                    .Select(x => x.ItemCategoryId)
                    .FirstOrDefault();

                // 第三步：查找有相同实际料号在库刀具的库位
                var sameItemQuery = DC.Set<Knife>()
                    .Include(x => x.WhLocation)
                        .ThenInclude(x => x.WhArea)
                            .ThenInclude(x => x.WareHouse)
                    .Include(x => x.ItemMaster)
                        .ThenInclude(x => x.Organization)
                    .Where(x => x.WhLocation.WhArea.WareHouseId == whid)  // 当前存储地点
                    .Where(x => x.WhLocation.IsEffective == EffectiveEnum.Effective)  // 库位有效
                    .Where(x => x.WhLocation.Locked != true)  // 库位未锁定
                    .Where(x => x.WhLocation.AreaType == WhLocationEnum.Normal)  // 正常库位
                    .Where(x => !x.WhLocation.Code.Contains("A9999999"))  // 排除不良退回库位
                    .Where(x => x.Status == KnifeStatusEnum.InStock)  // 在库状态的刀具
                    .Where(x => x.ActualItemCode == actualItemCode)  // 按实际料号筛选
                    .Where(x => x.ItemMaster.Organization.Code == "0410")  // 限定组织0410
                    .AsQueryable();

                // 按库位分组统计相同实际料号刀具数量，并按数量升序排列
                var sameItemLocationsData = sameItemQuery
                    .GroupBy(x => new
                    {
                        WhLocationId = x.WhLocationId,
                        LocCode = x.WhLocation.Code,
                        AreaCode = x.WhLocation.WhArea.Code
                    })
                    .Select(g => new 
                    {
                        g.Key.WhLocationId,
                        g.Key.AreaCode,
                        g.Key.LocCode,
                        Qty = g.Count()
                    })
                    .OrderBy(x => x.Qty)  // 相同料号的库位按数量升序（数量少的优先）
                    .Take(2)  // 限制返回前2个推荐库位
                    .ToList();

                // 转换为RecommendLocationReturn
                var sameItemLocations = sameItemLocationsData.Select(loc => new RecommendLocationReturn
                {
                    AreaCode = loc.AreaCode,
                    LocCode = loc.LocCode,
                    Qty = loc.Qty
                }).ToList();

                // 如果找到相同料号在库的库位，优先返回，但如果不足2个则补充空库位
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

                    var additionalLocationsData = DC.Set<BaseWhLocation>()
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
                        .Select(x => new { x.ID, x.WhArea.Code, LocCode = x.Code })
                        .ToList();

                    var additionalLocations = additionalLocationsData.Select(loc => new RecommendLocationReturn
                    {
                        AreaCode = loc.Code,
                        LocCode = loc.LocCode,
                        Qty = 0
                    }).ToList();

                    sameItemLocations.AddRange(additionalLocations);
                    return sameItemLocations;
                }

                // 第四步：没有相同料号在库时，根据刀具分类查找相似刀具所在库位
                if (itemCategoryId.HasValue)
                {
                    var sameCategoryLocationsData = DC.Set<Knife>()
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
                        .Where(x => x.ItemMaster.ItemCategoryId == itemCategoryId.Value)  // 相同分类
                        .Where(x => x.ItemMaster.Organization.Code == "0410")  // 限定组织0410
                        .GroupBy(x => new
                        {
                            WhLocationId = x.WhLocationId,
                            LocCode = x.WhLocation.Code,
                            AreaCode = x.WhLocation.WhArea.Code
                        })
                        .Select(g => new 
                        {
                            g.Key.WhLocationId,
                            g.Key.AreaCode,
                            g.Key.LocCode,
                            Qty = g.Count()
                        })
                        .OrderBy(x => x.Qty)
                        .Take(2)  // 限制返回前2个推荐库位
                        .ToList();

                    var sameCategoryLocations = sameCategoryLocationsData.Select(loc => new RecommendLocationReturn
                    {
                        AreaCode = loc.AreaCode,
                        LocCode = loc.LocCode,
                        Qty = loc.Qty
                    }).ToList();

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

                        var additionalLocationsData = DC.Set<BaseWhLocation>()
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
                            .Select(x => new { x.ID, x.WhArea.Code, LocCode = x.Code })
                            .ToList();

                        var additionalLocations = additionalLocationsData.Select(loc => new RecommendLocationReturn
                        {
                            AreaCode = loc.Code,
                            LocCode = loc.LocCode,
                            Qty = 0
                        }).ToList();

                        sameCategoryLocations.AddRange(additionalLocations);
                        return sameCategoryLocations;
                    }
                }

                // 第五步：没有相同料号和相似分类时，只推荐空库位
                // 查询所有符合条件的空库位
                var emptyLocations = DC.Set<BaseWhLocation>()
                    .Include(x => x.WhArea)
                        .ThenInclude(x => x.WareHouse)
                    .Where(x => x.WhArea.WareHouseId == whid)  // 当前存储地点
                    .Where(x => x.IsEffective == EffectiveEnum.Effective)  // 库位有效
                    .Where(x => x.Locked != true)  // 库位未锁定
                    .Where(x => x.AreaType == WhLocationEnum.Normal)  // 正常库位
                    .Where(x => !x.Code.Contains("A9999999"))  // 排除不良退回库位
                    .Where(x => !DC.Set<Knife>().Any(k => k.WhLocationId == x.ID && k.Status == KnifeStatusEnum.InStock))  // 只要空库位（没有在库刀具）
                    .Take(2)  // 限制返回前2个推荐库位
                    .Select(loc => new RecommendLocationReturn
                    {
                        AreaCode = loc.WhArea.Code,
                        LocCode = loc.Code,
                        Sn = null,
                        Batch = null,
                        Qty = 0  // 空库位数量为0
                    })
                    .ToList();

                if (emptyLocations.Count > 0)
                {
                    return emptyLocations;
                }

                // 如果以上所有步骤都没找到推荐库位，返回空列表
                return new List<RecommendLocationReturn>();
            }
            catch (Exception ex)
            {
                // 推荐库位获取失败不影响主流程，返回空列表
                return new List<RecommendLocationReturn>();
            }
        }


    }
}
