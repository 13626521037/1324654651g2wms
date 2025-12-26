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
using WMS.ViewModel.KnifeManagement.KnifeTransferInLineVMs;
using Elsa.Models;
using WMS.ViewModel.KnifeManagement.KnifeVMs;
namespace WMS.ViewModel.KnifeManagement.KnifeTransferInVMs
{
    public partial class KnifeTransferInVM : BaseCRUDVM<KnifeTransferIn>
    {

        public List<string> KnifeManagementKnifeTransferInFTempSelected { get; set; }
        public KnifeTransferInLineKnifeTransferInDetailListVM KnifeTransferInLineKnifeTransferInList { get; set; }
        public KnifeTransferInLineKnifeTransferInDetailListVM1 KnifeTransferInLineKnifeTransferInList1 { get; set; }
        public KnifeTransferInLineKnifeTransferInDetailListVM2 KnifeTransferInLineKnifeTransferInList2 { get; set; }

        public KnifeTransferInVM()
        {
            SetInclude(x => x.HandledBy);
            KnifeTransferInLineKnifeTransferInList = new KnifeTransferInLineKnifeTransferInDetailListVM();
            KnifeTransferInLineKnifeTransferInList.DetailGridPrix = "Entity.KnifeTransferInLine_KnifeTransferIn";
            KnifeTransferInLineKnifeTransferInList1 = new KnifeTransferInLineKnifeTransferInDetailListVM1();
            KnifeTransferInLineKnifeTransferInList1.DetailGridPrix = "Entity.KnifeTransferInLine_KnifeTransferIn";
            KnifeTransferInLineKnifeTransferInList2 = new KnifeTransferInLineKnifeTransferInDetailListVM2();
            KnifeTransferInLineKnifeTransferInList2.DetailGridPrix = "Entity.KnifeTransferInLine_KnifeTransferIn";

        }

        protected override void InitVM()
        {
            KnifeTransferInLineKnifeTransferInList.CopyContext(this);
            KnifeTransferInLineKnifeTransferInList1.CopyContext(this);
            KnifeTransferInLineKnifeTransferInList2.CopyContext(this);
        }

        public override DuplicatedInfo<KnifeTransferIn> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.DocNo));
            return rv;

        }

        public override void DoAdd()
        {
            if (Entity.KnifeTransferInLine_KnifeTransferIn.GroupBy(x => new { x.KnifeTransferInId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }
            if (Entity.KnifeTransferInLine_KnifeTransferIn.Count == 0)
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
            //刀具校验
            if (Entity.KnifeTransferInLine_KnifeTransferIn.Count == 0)
            {
                MSD.AddModelError("", "请输入刀具");
                return;
            }
            if (Entity.KnifeTransferInLine_KnifeTransferIn.GroupBy(x => new { x.KnifeTransferInId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }

            base.DoEdit(updateAllFields);
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
        /// 刀具调入单新建初始化
        /// </summary>
        /// <param name="tran"></param>
        public void DoInitForAdd(IDbContextTransaction tran)//新建用的初始化
        {
            try
            {
                //新建  直接调入单赋值
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                var KnifeTransferInDocNo = DocNoVm.GetSequence("KnifeTransferInRule", tran);//生成调出单单号

                Entity.DocNo = KnifeTransferInDocNo;
                Entity.Status = KnifeOrderStatusEnum.Open;
                Entity.HandledById = handledBy.ID.ToString();
                Entity.KnifeTransferInLine_KnifeTransferIn = new List<KnifeTransferInLine>();
                Entity.CreateBy = handledBy.Name;
                Entity.CreateTime = DateTime.Now;

                DC.SaveChanges();

            }
            catch (Exception e)
            {
                MSD.AddModelError("", "捕获到异常:" + e.Message);
                return;
            }
        }

        /// <summary>
        /// 刀具调入单获取列表
        /// </summary>
        /// <returns></returns>
        public List<KnifeTransferIn> GetList()
        {
            try
            {
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                var result = DC.Set<KnifeTransferIn>()
                    .Where(x => x.Status == KnifeOrderStatusEnum.Open && x.HandledById == handledBy.ID.ToString())
                    .ToList();
                if (result == null&&result.Count==0)
                {
                    MSD.AddModelError("", "列表为空");
                    return null;
                }
                return result;
            }
            catch(Exception e)
            {
                MSD.AddModelError("", "未知错误:"+e.Message);
                return null;
            }
            
        }

        /// <summary>
        /// 刀具调入单获取单个
        /// </summary>
        /// <param name="DocNo"></param>
        /// <returns></returns>
        public KnifeTransferIn GetOne(string DocNo)
        {
            try
            {
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                var result = DC.Set<KnifeTransferIn>()
                    .Where(x => x.Status == KnifeOrderStatusEnum.Open && x.HandledById == handledBy.ID.ToString()&&x.DocNo==DocNo)
                    .FirstOrDefault();
                if (result == null)
                {
                    MSD.AddModelError("", "未查询到此单号");
                    return null;
                }
                return result;
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "未知错误:" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 刀具调入单更新初始化
        /// </summary>
        /// <param name="info"></param>
        public void DoInitForUpdate(KnifeTransferInInputInfoForUpdate info)
        {
            try
            {
                //存储地点校验
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                //非空检验
                if (string.IsNullOrEmpty(info.DocNo)|| info.Knifes is null || info.Knifes.Count == 0&& string.IsNullOrEmpty(info.WhLocationId))
                {
                    MSD.AddModelError("", "缺少参数 请检查");
                    return;
                }
                //有效性校验 调入单有效   库位有效  刀具有效
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                Entity = DC.Set<KnifeTransferIn>()
                    .Where(x => x.DocNo == info.DocNo && x.Status == KnifeOrderStatusEnum.Open&&x.HandledById ==handledBy.ID.ToString())
                    .FirstOrDefault();
                if(Entity is null)
                {
                    MSD.AddModelError("", "未查询到此单号");
                    return ;
                }
                var whLocation = DC.Set<BaseWhLocation>().Where(x => x.ID.ToString() == info.WhLocationId&&x.WhArea.WareHouseId==whid).FirstOrDefault();
                if (whLocation is null)
                {
                    MSD.AddModelError("", "未查询到此库位");
                    return;
                }
                var knifeIds = info.Knifes.Select(x => x.KnifeId).ToList();
                var knifes = DC.Set<Knife>()
                    .Include(x=>x.WhLocation) 
                        .ThenInclude(x=>x.WhArea)
                            .ThenInclude(x=>x.WareHouse)
                    .CheckIDs(knifeIds).ToList();
                foreach(var knife in knifes)
                {
                    if(knife.Status != KnifeStatusEnum.Transferring )
                    {
                        MSD.AddModelError("", $"{knife.SerialNumber}的状态不为调拨中 无法被调入");
                        return;
                    }
                    if (knife.WhLocation.WhArea.WareHouseId == whid)
                    {
                        MSD.AddModelError("", $"{knife.SerialNumber}已在当前登录存储地点 无法调入");
                        return;
                    }
                }



                //校验通过 先放上原来的值  再修改entity信息  修改时原有的刀具的信息要删除掉然后重新新建进去  不能直接覆盖掉  
                var knifeTransferInlineVM = Wtm.CreateVM<KnifeTransferInLineVM>();
                if (Entity.KnifeTransferInLine_KnifeTransferIn != null)
                {
                    foreach (var line in Entity.KnifeTransferInLine_KnifeTransferIn)
                    {
                        knifeTransferInlineVM.Entity = line;
                        knifeTransferInlineVM.DoDelete();
                    }
                }
                var lines = new List<KnifeTransferInLine>();
                foreach (var knife in info.Knifes)
                {
                    var line = new KnifeTransferInLine { KnifeId = Guid.Parse(knife.KnifeId), KnifeTransferInId = Entity.ID };
                    knifeTransferInlineVM.Entity = line;
                    knifeTransferInlineVM.DoAdd();//这里可以vm doadd新增 也可以组成list给entity save就新增了 .
                    lines.Add(line);
                }
                Entity.KnifeTransferInLine_KnifeTransferIn = lines;
                /*var lines = new List<KnifeTransferInLine>();
                foreach (var knife in info.Knifes)
                {
                    var line = new KnifeTransferInLine { KnifeId = Guid.Parse(knife.KnifeId) };// 
                    lines.Add(line); 
                }
                Entity.KnifeTransferInLine_KnifeTransferIn = lines;*///直接放line也会新增 但原有的不会被删除

                Guid whLocationId_guid = Guid.Parse(info.WhLocationId);
                //Entity.WhLocationId = whLocationId_guid;

                Entity.UpdateBy = handledBy.Name;
                Entity.UpdateTime  = DateTime.Now;

                DC.SaveChanges();

            }
            catch (Exception e)
            {
                MSD.AddModelError("", "捕获到异常:" + e.Message);
                return;
            }
        }

        public void Delete(string DocNo)
        {
            try
            {
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                var knifeTransferIn = DC.Set<KnifeTransferIn>()
                    .Where(x => x.Status == KnifeOrderStatusEnum.Open && x.HandledById == handledBy.ID.ToString() && x.DocNo == DocNo)
                    .First();
                if (knifeTransferIn == null)
                {
                    MSD.AddModelError("", "单号不存在或已审核 无法删除");
                    return ;
                }
                DoDelete();
                return ;
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "未知错误:" + e.Message);
                return ;
            }
        }
        public void DoInitForApproved(string docNo)
        {
            try
            {
                //存储地点校验
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                //非空检验
                if (string.IsNullOrEmpty(docNo))
                {
                    MSD.AddModelError("", "缺少参数 请检查");
                    return;
                }
                //有效性校验 调入单有效   库位有效  刀具有效
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                var knifeTransferIn = DC.Set<KnifeTransferIn>()
                               /* .Include(x => x.WhLocation)
                                    .ThenInclude(x => x.WhArea)
                                        .ThenInclude(x => x.WareHouse)*/
                                .Include(x => x.KnifeTransferInLine_KnifeTransferIn)
                                .Where(x => x.DocNo == docNo && x.Status == KnifeOrderStatusEnum.Open && x.HandledById == handledBy.ID.ToString())
                                .FirstOrDefault();
                if (knifeTransferIn is null)
                {
                    MSD.AddModelError("", "未查询到此单号");
                    return;
                }




                //校验通过 把查到的调入单给当前
                Entity = knifeTransferIn;

                DC.SaveChanges();

            }
            catch (Exception e)
            {
                MSD.AddModelError("", "捕获到异常:" + e.Message);
                return;
            }
        }

        /// <summary>
        /// 刀具调出单审核
        /// </summary>
        public void DoApproved()
        {
            try
            {
                //重新加载Entity
                Entity = DC.Set<KnifeTransferIn>()
                    .Include(x => x.KnifeTransferInLine_KnifeTransferIn)
                        .ThenInclude(x => x.Knife)
                    .Where(x => x.ID == Entity.ID)
                    .FirstOrDefault();
                //校验  非空  有效  刀的对应的调出单的调入地点与当前用户相符  
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
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
                }//1.仓管员当前登录状态
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                //根据单号获取当前用户的开立状态的调入单 并且重新加载   加入库位信息 1.单号有效检验

                //2.非空校验
                if (string.IsNullOrEmpty(Entity.DocNo)
                    || Entity.Status!=KnifeOrderStatusEnum.Open
                    || string.IsNullOrEmpty(Entity.HandledById)
                    || string.IsNullOrEmpty(Entity.TransferOutDocNo)
                    || Entity.KnifeTransferInLine_KnifeTransferIn is null ||Entity.KnifeTransferInLine_KnifeTransferIn.Count ==0
                    )
                {
                    MSD.AddModelError("", "调入单审核缺少参数 请检查");
                    return;
                }
                //3.调入单有效性校验
                var transferOut = DC.Set<KnifeTransferOut>()
                    .Include(x => x.KnifeTransferOutLine_KnifeTransferOut)
                        .ThenInclude(x => x.Knife)
                    .FirstOrDefault(x => x.DocNo == Entity.TransferOutDocNo && x.Status == KnifeOrderStatusEnum.Approved && x.ToWhId == whid);//对应的调出单
                if (transferOut is null)
                {
                    MSD.AddModelError("", "未检测有效的调出单 已被使用或目标调入存储地点与当前登录地点不同");
                    return;
                }
                //4.遍历行 刀和库位的有效性校验
                var unCloseKnifes = transferOut.KnifeTransferOutLine_KnifeTransferOut
                    .Where(x => x.Status == KnifeOrderStatusEnum.Approved && x.Knife.Status == KnifeStatusEnum.Transferring)
                    .Select(x => x.Knife).ToList();//获取未关闭且状态为调拨中的的刀
                var whLocationIds = DC.Set<BaseWhLocation>().Where(x => x.WhArea.WareHouseId == whid).Select(x => x.ID).ToList();//本存储地点的库位
                foreach (var line in Entity.KnifeTransferInLine_KnifeTransferIn)
                {
                    //刀
                    var knife = unCloseKnifes.FirstOrDefault(x => x.ID == line.KnifeId);
                    if (knife is null)
                    {
                        MSD.AddModelError("", "检测到无效的调出刀具 调入失败");
                        return;
                    }
                    //库位
                    if (!whLocationIds.Contains((Guid)line.ToWhLocationId))
                    {
                        MSD.AddModelError("", "检测无效的调入库位 调入失败");
                        return;
                    }
                }
                //审核操作 调入单  刀具 操作
                var currentTime = DateTime.Now;
                Entity.Status = KnifeOrderStatusEnum.Approved;
                Entity.ApprovedTime = currentTime;
                Entity.HandledById = handledBy.ID.ToString();
                Entity.HandledBy = handledBy;
                Entity.UpdateBy = handledBy.Name;
                Entity.UpdateTime = currentTime;
                foreach(var inLine in Entity.KnifeTransferInLine_KnifeTransferIn)
                {
                    var knife = inLine.Knife;
                    inLine.Knife.Status = KnifeStatusEnum.InStock;
                    inLine.Knife.HandledById = handledBy.ID.ToString();
                    inLine.Knife.HandledBy = handledBy;
                    inLine.Knife.HandledByName = handledBy.Name;
                    inLine.Knife.LastOperationDate = currentTime;
                    inLine.Knife.WhLocationId = inLine.ToWhLocationId;
                    DC.Set<KnifeOperation>().Add(new KnifeOperation
                    {
                        KnifeId = inLine.Knife.ID,
                        DocNo = Entity.DocNo,
                        OperationType = KnifeOperationTypeEnum.TransferIn,
                        OperationTime = currentTime,
                        OperationBy = handledBy_operator,
                        OperationById = handledBy_operator.ID,
                        HandledById = handledBy.ID.ToString(),
                        HandledBy = handledBy,
                        HandledByName = handledBy.Name,
                        UsedDays = 0,
                        TotalUsedDays = knife.TotalUsedDays,//累计使用天数不变
                        RemainingDays = knife.RemainingUsedDays,
                        CurrentLife = knife.CurrentLife,
                        WhLocationId = inLine.ToWhLocationId,
                        GrindNum = knife.GrindCount,

                        CreateBy = handledBy.Name,
                        CreateTime = currentTime,
                    });
                }
                //回写调出单的的行  如果所有行都已被回写   关闭调出单
                var needToCloseLines = transferOut.KnifeTransferOutLine_KnifeTransferOut
                    .Where(x => x.Status == KnifeOrderStatusEnum.Approved && x.Knife.Status == KnifeStatusEnum.InStock)
                    .ToList();//获取未关闭且刀状态为在库(已完成调入,需要关闭)的的调出行 关闭他们
                foreach (var line in needToCloseLines)
                {
                    line.Status = KnifeOrderStatusEnum.ApproveClose;
                }
                var unCloseLines = transferOut.KnifeTransferOutLine_KnifeTransferOut
                    .Where(x => x.Status == KnifeOrderStatusEnum.Approved && x.Knife.Status == KnifeStatusEnum.Transferring)
                    .ToList();//获取未关闭且刀状态为调拨中的的调出行 看看是否已经整单完成
                if(unCloseLines is null||unCloseLines .Count == 0)
                {
                    transferOut.Status = KnifeOrderStatusEnum.ApproveClose;
                }

                DC.SaveChanges();
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "未知错误:" + e.Message);
                return;
            }
        }
        /// <summary>
        /// 刀具调出单预处理
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        public void DoInitByInfo(KnifeTransferInInputInfo info, IDbContextTransaction tran)
        {
            try
            {
                //校验info的信息
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                Guid whid;
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);//当前pda持有者(仓管员)的存储地点
                var transferOut = DC.Set<KnifeTransferOut>()
                    .Include(x => x.KnifeTransferOutLine_KnifeTransferOut)
                        .ThenInclude(x => x.Knife).ThenInclude(x=>x.WhLocation).ThenInclude(x=>x.WhArea).ThenInclude(x=>x.WareHouse)
                    .FirstOrDefault(x => x.DocNo == info.TransferOutDocNo && x.Status == KnifeOrderStatusEnum.Approved && x.ToWhId == whid);

                //1.仓管员当前登录状态
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                //2.info参数非空校验
                if (string.IsNullOrEmpty(info.TransferOutDocNo) || info.Lines is null||info.Lines.Count==0)
                {
                    MSD.AddModelError("", "输入参数不足 请检查");
                    return;
                }
                //3.调入单有效性校验
                if (transferOut is null)
                {
                    MSD.AddModelError("", "未检测有效的调出单 已被使用或目标调入存储地点与当前登录地点不同");
                    return;
                }
                //4.调入的刀的lines展开校验
                var unCloseKnifes = transferOut.KnifeTransferOutLine_KnifeTransferOut
                    .Where(x => x.Status == KnifeOrderStatusEnum.Approved&&x.Knife.Status==KnifeStatusEnum.Transferring)
                    .Select(x => x.Knife).ToList();//获取未关闭的刀
                var effectWhLocationIds = DC.Set<BaseWhLocation>().Where(x => x.WhArea.WareHouseId == whid).Select(x => x.ID).ToList();
                foreach (var line in info.Lines)
                {
                    //刀
                    var knife = unCloseKnifes.FirstOrDefault(x=>x.ID.ToString().ToUpper()==line.KnifeId.ToUpper());
                    if(knife is null)
                    {
                        MSD.AddModelError("", $"检测到无效的调出刀具 调入失败");
                        return;
                    }
                    //库位
                    if (!effectWhLocationIds.Contains(Guid.Parse(line.WhLocationId)))
                    {
                        MSD.AddModelError("", $"检测无效的库位 调入失败");
                        return;
                    }
                    
                }
                //5.调入库位盘点锁定判断
                var whLocationIds = info.Lines.Select(x => Guid.Parse(x.WhLocationId)).ToList();
                var whLocations = DC.Set<BaseWhLocation>().Where(x => whLocationIds.Contains(x.ID)).ToList();
                foreach(var location in whLocations)
                {
                    if (location.Locked == true)
                    {
                        MSD.AddModelError("", $"库位{location.Code}已锁定 不可用");
                        return;
                    }
                    
                    if (location.IsEffective == EffectiveEnum.Ineffective || location.AreaType != WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"库位{location.Code}无效");
                        return;
                    }
                }


                //校验无误就初始化vm信息
                var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                var KnifeTransferInDocNo = DocNoVm.GetSequence("KnifeTransferInRule", tran);//生成调入单单号
                Entity.DocNo = KnifeTransferInDocNo;
                Entity.Status = KnifeOrderStatusEnum.Open;
                Entity.HandledById = handledBy.ID.ToString();
                Entity.HandledBy = handledBy;
                Entity.TransferOutDocNo = info.TransferOutDocNo;
                Entity.FromWHId = transferOut.FromWHId;
                Entity.ToWHId = transferOut.ToWhId;
                Entity.WareHouseId = whid;
                Entity.KnifeTransferInLine_KnifeTransferIn = new List<KnifeTransferInLine>();
                foreach(var line in info.Lines)
                {
                    var knife = unCloseKnifes.FirstOrDefault(x => x.ID == Guid.Parse(line.KnifeId));
                    Entity.KnifeTransferInLine_KnifeTransferIn.Add(new KnifeTransferInLine
                    {
                        KnifeId = Guid.Parse(line.KnifeId),
                        ToWhLocationId = Guid.Parse(line.WhLocationId),
                        FromWhLocationId = knife.WhLocationId,

                    });
                }
                #region 关闭涉及的组合单
                var toCloseKnifeIds = info.Lines.Select(x => Guid.Parse(x.KnifeId)).ToList();
                var toCloseCombineLines = DC.Set<KnifeCombineLine>()
                    .Include(x => x.KnifeCombine)
                    .Where(x => x.KnifeCombine.Status == KnifeOrderStatusEnum.Approved)//组合单是已核准状态
                    .Where(x => toCloseKnifeIds.Contains(x.Knife.ID))//刀id和组合行的刀id匹配 
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
                MSD.AddModelError("", "未知错误:" + e.Message);
                return;
            }
        }
    }
}
