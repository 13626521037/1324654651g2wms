using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using Elsa;
using Microsoft.EntityFrameworkCore;
using WMS.Model;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseBarCodeVMs;


namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs
{
    public partial class InventoryOtherShipApiVM : BaseCRUDVM<InventoryOtherShip>
    {

        public InventoryOtherShipApiVM()
        {
            SetInclude(x => x.DocType);
            SetInclude(x => x.BenefitOrganization);
            SetInclude(x => x.BenefitDepartment);
            SetInclude(x => x.BenefitPerson);
            SetInclude(x => x.Wh);
            SetInclude(x => x.OwnerOrganization);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }

        /// <summary>
        /// 创建单据
        /// </summary>
        /// <param name="para">前端创建单据的参数</param>
        public void CreateDoc(InventoryOtherShipCreatePara para)
        {
            MSD.Clear();

            #region 表头数据有效性验证、获取对应实体

            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            if (whid == Guid.Empty)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录2");
                return;
            }
            BaseWareHouse outWh = DC.Set<BaseWareHouse>().Include(x => x.Organization).AsNoTracking().Where(x => x.ID == whid).FirstOrDefault();
            if (outWh == null)
            {
                MSD.AddModelError("", "当前登录存储地点不存在");
                return;
            }

            if (para == null)
            {
                MSD.AddModelError("", "参数不能为空");
                return;
            }
            if (para.BenefitOrganizationId == null || para.BenefitOrganizationId == Guid.Empty)
            {
                MSD.AddModelError("", "受益组织不能为空");
                return;
            }
            if (para.BenefitDepartmentId == null || para.BenefitDepartmentId == Guid.Empty)
            {
                MSD.AddModelError("", "受益部门不能为空");
                return;
            }
            if (para.BenefitPersonId == null || para.BenefitPersonId == Guid.Empty)
            {
                MSD.AddModelError("", "受益人员不能为空");
                return;
            }
            if (para.DocTypeId == null || para.DocTypeId == Guid.Empty)
            {
                MSD.AddModelError("", "单据类型不能为空");
                return;
            }
            if (para.Lines == null || para.Lines.Count == 0)
            {
                MSD.AddModelError("", "行数据不能为空");
                return;
            }
            //InventoryTransferOutDirectDocType docType = DC.Set<InventoryTransferOutDirectDocType>().AsNoTracking().Where(x => x.ID == para.DocTypeId).FirstOrDefault();
            //if (docType == null)
            //{
            //    MSD.AddModelError("", "调出类型不存在");
            //    return;
            //}

            #endregion

            InventoryOtherShip entity = new InventoryOtherShip
            {
                // ID = Guid.NewGuid(),
                BusinessDate = DateTime.Now.Date,
                DocTypeId = para.DocTypeId,
                BenefitOrganizationId = para.BenefitOrganizationId,
                BenefitDepartmentId = para.BenefitDepartmentId,
                BenefitPersonId = para.BenefitPersonId,
                WhId = whid,
                OwnerOrganizationId = outWh.OrganizationId,
                Memo = para.Memo,
                InventoryOtherShipLine_InventoryOtherShip = new List<InventoryOtherShipLine>(),
            };

            #region 行数据有效性验证

            List<BaseInventory> invs = new List<BaseInventory>();
            Dictionary<BaseInventory, decimal> invsQty = new Dictionary<BaseInventory, decimal>();  // 库存ID和数量的字典（记录库存流水时用）
            List<(Guid, Guid, decimal)> lineIdToInvIdDict = new List<(Guid, Guid, decimal)>();  // 行ID与库存ID的对应关系（第一个Guid是行ID，第二个Guid是库存ID，第三个数字是本次下架数量）
            foreach (var paraLine in para.Lines)
            {
                if (paraLine.InventoryId == null || paraLine.InventoryId == Guid.Empty)
                {
                    MSD.AddModelError("", "行数据中的库存ID不能为空");
                    return;
                }
                BaseInventory inv = invs.Find(x => x.ID == paraLine.InventoryId);
                if (inv != null)
                {
                    MSD.AddModelError("", $"同一条码({inv.SerialNumber})被多次扫描");
                    return;
                }
                if (paraLine.Qty <= 0)
                {
                    MSD.AddModelError("", "行数据中的数量必须大于0");
                    return;
                }

                inv = DC.Set<BaseInventory>().Include(x => x.ItemMaster.StockUnit).Include(x => x.WhLocation.WhArea)
                    .Where(x => x.ID == paraLine.InventoryId && x.IsAbandoned == false).FirstOrDefault();
                if (inv == null)
                {
                    MSD.AddModelError("", "库存不存在");
                    return;
                }
                if (inv.ItemMaster.Code.Substring(0, 2) == "17")
                {
                    MSD.AddModelError("", "17开头料号不允许使用“杂发”功能，请通过“刀具领用”功能操作");
                    return;
                }
                invsQty.Add(inv, paraLine.Qty);
                if (inv.FrozenStatus == FrozenStatusEnum.Freezed)
                {
                    MSD.AddModelError("", $"“{inv.ItemMaster.Code}({inv.SerialNumber}”)的库存已冻结，无法操作");
                    return;
                }
                //var loc = DC.Set<BaseWhLocation>()
                //    .Include(x => x.WhArea)
                //    .Where(x => x.ID == inv.WhLocationId)
                //    .AsNoTracking()
                //    .FirstOrDefault();
                var loc = inv.WhLocation;
                if (loc != null)
                {
                    if (loc.AreaType != WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"只允许从正常库位进行下架操作。下架明细中“{inv.SerialNumber}”所在的库位类型为“{loc.AreaType.GetEnumDisplayName()}”，无法进行下架操作");
                        return;
                    }
                    if (loc.Locked == true)
                    {
                        MSD.AddModelError("", $"下架明细中“{inv.SerialNumber}”所在的库位已被盘点锁定，无法进行下架操作");
                        return;
                    }
                    if (loc.WhArea.WareHouseId != whid)
                    {
                        MSD.AddModelError("", $"下架明细中“{inv.SerialNumber}”所在的库位不属于当前登录的存储地点，无法进行下架操作");
                        return;
                    }
                }
                else
                {
                    MSD.AddModelError("", $"“{inv.ItemMaster.Code}({inv.SerialNumber}”)的库位信息丢失");
                    return;
                }
                invs.Add(inv);
                if (inv.Qty < paraLine.Qty)
                {
                    MSD.AddModelError("", $"库存数量不足，库存数量为{inv.Qty}，需要{paraLine.Qty}");
                    return;
                }
                inv.Qty -= paraLine.Qty;    // 库存信息数量减少
                InventoryOtherShipLine line = new InventoryOtherShipLine
                {
                    // ID = Guid.NewGuid(),
                    //Inventory = inv,
                    InventoryId = inv.ID,
                    Qty = paraLine.Qty,
                    RcvQty = 0,
                };
                lineIdToInvIdDict.Add((line.ID, inv.ID, paraLine.Qty));
                entity.InventoryOtherShipLine_InventoryOtherShip.Add(line);
            }

            // 判断所有库存信息是否都已下架完。下机操作不允许库存有剩余（所有下架动作都需要进行这个判定）
            foreach (var inv in invs)
            {
                // 前端已做过校验。正常不会报这个错误
                if (inv.Qty > 0)
                {
                    MSD.AddModelError("", $"序列号：{inv.SerialNumber}对应的条码数量未全部下架。请检查数据");
                    return;
                }
                inv.IsAbandoned = true;
            }

            #endregion

            #region 保存数据

            // 保存以后，所有的关联实体要跟踪一下，是否会被设为空
            using (var tran = DC.Database.BeginTransaction())
            {
                // 新增WMS端的单据
                Entity = entity;
                DoAdd();
                DC.SaveChanges();
                if (!MSD.IsValid)
                {
                    return;
                }
                // 重新查找一遍，进行跟踪。后面要修改
                entity = DC.Set<InventoryOtherShip>()
                    .Include(x => x.DocType)
                    .Include(x => x.BenefitOrganization)
                    .Include(x => x.BenefitDepartment)
                    .Include(x => x.BenefitPerson)
                    .Include(x => x.Wh)
                    .Include(x => x.OwnerOrganization)
                    .Include(x => x.InventoryOtherShipLine_InventoryOtherShip)
                    .ThenInclude(x => x.Inventory)
                    .ThenInclude(x => x.ItemMaster)
                    .Where(x => x.ID == Entity.ID)
                    .FirstOrDefault();
                //// 库存信息重新匹配
                //foreach (var line in entity.InventoryOtherShipLine_InventoryOtherShip)
                //{
                //    var inv = invs.Find(x => x.SerialNumber == line.SerialNumber);
                //    if (inv != null)
                //    {
                //        line.Inventory = inv;
                //    }
                //    else
                //    {
                //        MSD.AddModelError("", $"库存信息不匹配，无法完成操作");   // 正常不会出现
                //        return;
                //    }
                //}
                // 调用U9接口过账
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, outWh.Organization.Code, LoginUserInfo.Name);
                var u9Return = apiHelper.CreateMiscShipment(entity);
                if (u9Return.Success)
                {
                    entity.DocNo = u9Return.Entity.DocNo;
                    entity.ErpID = u9Return.Entity.ID.ToString();
                    if (u9Return.Entity.Lines != null
                        && entity.InventoryOtherShipLine_InventoryOtherShip != null
                        && entity.InventoryOtherShipLine_InventoryOtherShip.Count == u9Return.Entity.Lines.Count)
                    {
                        for (int i = 0; i < u9Return.Entity.Lines.Count; i++)
                        {
                            entity.InventoryOtherShipLine_InventoryOtherShip[i].ErpID = u9Return.Entity.Lines[i].ID.ToString();
                            entity.InventoryOtherShipLine_InventoryOtherShip[i].DocLineNo = u9Return.Entity.Lines[i].DocLineNo;
                        }
                    }

                    // 处理条码（做杂收单时会用到）
                    BaseBarCodeVM barCodeVM = Wtm.CreateVM<BaseBarCodeVM>();
                    List<LsWmsBarCodePara> paras = new List<LsWmsBarCodePara>();
                    foreach (var inv in invs)
                    {
                        // 判断条码中是否存在此序列号，如果不存在，则新增。存在，则更新番号、批号
                        BaseBarCode barCode = DC.Set<BaseBarCode>().Where(x => x.Sn == inv.SerialNumber).FirstOrDefault();
                        if (barCode == null)
                        {
                            barCodeVM.CreateBarCode(true, inv.ItemMaster.Code, invsQty[inv], docNo: entity.DocNo, orgCode: entity.BenefitOrganization.Code, sourceType: (int)inv.ItemSourceType, barCode: inv.SerialNumber, seiban: inv.Seiban);
                        }
                        else
                        {
                            barCode.Seiban = inv.Seiban;
                            barCode.SeibanRandom = inv.SeibanRandom;
                            barCode.BatchNumber = inv.BatchNumber;
                            barCode.Qty = invsQty[inv];
                        }
                        // 这里直接传杂发出的存储地点。不知是否妥当
                        paras.Add(new LsWmsBarCodePara(entity.DocNo, inv.ItemMaster.Code, inv.ItemMaster.Name, inv.ItemMaster.SPECS, invsQty[inv], inv.SerialNumber
                            , entity.BenefitOrganization.Code, entity.BenefitOrganization.Name, inv.ItemMaster.StockUnit.Name, entity.Wh.Code, entity.Wh.Name, inv.Seiban, inv.SeibanRandom));
                    }
                    // 将条码传链溯WMS系统
                    LsWmsHelper.WriteBarCode(paras);

                    DC.SaveChanges();
                    tran.Commit();
                }
                else
                {
                    MSD.AddModelError("", u9Return.Msg);
                    return;
                }
            }

            // 记录库存流水（遍历字典）——必须放在U9接口过账之后，不然没有单号
            foreach (var inv in invsQty)
            {
                this.CreateInvLog(OperationTypeEnum.InventoryOtherShipCreate, entity.DocNo, inv.Key.ID, null, -inv.Value, null);
            }

            // 单据库存关联关系要做。复制的调出调入的逻辑
            foreach (var item in lineIdToInvIdDict)
            {
                // 创建单据库存关联关系
                if (!this.CreateInvRelation(DocTypeEnum.InventoryOtherShip, item.Item2, entity.ID, item.Item1, item.Item3))
                {
                    return;
                }
            }

            #endregion

        }
    }
}
