using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.DataAccess;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.ProductionManagement;
using WMS.Util;


namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs
{
    public partial class ProductionRcvRptApiVM : BaseCRUDVM<ProductionRcvRpt>
    {

        public ProductionRcvRptApiVM()
        {
            SetInclude(x => x.Organization);
            SetInclude(x => x.Wh);
            SetInclude(x => x.OrderWh);
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
        /// 扫描获取MO信息
        /// </summary>
        /// <param name="moDocNo">MO单号</param>
        /// <returns></returns>
        public ProductionRcvRptMO GetRcvRptMo(string moDocNo)
        {
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<ProductionRcvRptMO> u9Return = apiHelper.GetRcvRptMo(moDocNo);
            if (u9Return.Success)
            {
                // 转换料品、存储地点
                BaseItemMaster item = this.GetEntityBySourceSystemId<ProductionRcvRpt, BaseItemMaster>(u9Return.Entity.SyncItem);
                if (item == null)
                {
                    MSD.AddModelError("", $"料品ID[{u9Return.Entity.SyncItem}]在系统中不存在，请先同步料品资料");
                    return null;
                }
                BaseWareHouse wh = this.GetEntityBySourceSystemId<ProductionRcvRpt, BaseWareHouse>(u9Return.Entity.SyncWh);
                if (wh == null)
                {
                    MSD.AddModelError("", $"完工存储地点ID[{u9Return.Entity.SyncWh}]在系统中不存在，请先同步存储地点资料");
                    return null;
                }
                BaseWareHouse orderWh = null;
                if (u9Return.Entity.SyncOrderWh != null)
                {
                    orderWh = this.GetEntityBySourceSystemId<ProductionRcvRpt, BaseWareHouse>(u9Return.Entity.SyncOrderWh);
                    if (orderWh == null)
                    {
                        MSD.AddModelError("", $"订单存储地点ID[{u9Return.Entity.SyncOrderWh}]在系统中不存在，请先同步存储地点资料");
                        return null;
                    }
                }
                u9Return.Entity.ItemId = item.ID;
                u9Return.Entity.ItemCode = item.Code;
                u9Return.Entity.ItemName = item.Name;
                u9Return.Entity.ItemSPECS = item.SPECS;
                u9Return.Entity.WhId = wh.ID;
                u9Return.Entity.WhCode = wh.Code;
                u9Return.Entity.WhName = wh.Name;
                u9Return.Entity.OrderWhId = orderWh?.ID;
                u9Return.Entity.OrderWhCode = orderWh?.Code;
                u9Return.Entity.OrderWhName = orderWh?.Name;
                return u9Return.Entity;
            }
            else
            {
                MSD.AddModelError("", u9Return.Msg);
                return null;
            }
        }

        /// <summary>
        /// 创建单据（G2创建）
        /// </summary>
        /// <param name="entity"></param>
        public void CreateRcvRpt(ProductionRcvRpt entity)
        {
            MSD.Clear();    // 看下是否需要判断
            using (var trans = DC.BeginTransaction())
            {
                BaseWareHouse wh = DC.Set<BaseWareHouse>().Include(x => x.Organization).AsNoTracking().Where(x => x.ID == entity.WhId).FirstOrDefault();
                if (wh == null)
                {
                    MSD.AddModelError("", $"存储地点{entity.WhId}不存在");
                    return;
                }
                int docLineNo = 1;
                if (entity.ProductionRcvRptLine_ProductionRcvRpt == null || entity.ProductionRcvRptLine_ProductionRcvRpt.Count == 0)
                {
                    MSD.AddModelError("", $"报工明细行不能为空");
                    return;
                }
                // 获取当前存储地点的线边库位
                BaseWhLocation location = DC.Set<BaseWhLocation>()
                    .Where(x => x.WhArea.WareHouseId == entity.WhId && x.AreaType == Model.WhLocationEnum.LineEdge && x.IsEffective == EffectiveEnum.Effective)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (entity.ShipType != WhShipTypeEnum.SpotGoods)    // 009-现货，G2WMS不管控（不上架）
                {
                    if (location == null)
                    {
                        MSD.AddModelError("", $"当前存储地点{wh.Code}未配置线边库位，无法进行收货操作");
                        return;
                    }
                    if (location.Locked == true)
                    {
                        MSD.AddModelError("", "当前库位已被盘点锁定，无法进行收货操作");
                        return;
                    }
                }
                // 新增的库存信息集合（后面统一插入数据库）
                List<BaseInventory> newInventoryList = new List<BaseInventory>();
                Dictionary<string, (decimal, decimal)> barcodeOccupyQtyDict = new Dictionary<string, (decimal, decimal)>();  // 条码库存占用（第一个数字是库存数量，第二个数字是本次占用数量）
                List<(Guid, Guid, decimal)> lineIdToInvIdDict = new List<(Guid, Guid, decimal)>();  // 行ID与库存ID的对应关系（第一个Guid是行ID，第二个Guid是库存ID，第三个数字是本次收货数量）
                List<CreateRcvRptDocLineDTO> lineDtos = new List<CreateRcvRptDocLineDTO>(); // U9接口行参数
                bool? isParts = null;
                foreach (var line in entity.ProductionRcvRptLine_ProductionRcvRpt)
                {
                    if (line.Qty <= 0)
                    {
                        MSD.AddModelError("", $"报工数量必须大于0，生产订单[{line.MODocNo}]");
                        return;
                    }
                    BaseItemMaster item = DC.Set<BaseItemMaster>().Where(x => x.ID == line.ItemMasterId).AsNoTracking().FirstOrDefault();
                    if (item == null)
                    {
                        MSD.AddModelError("", $"行“{line.DocLineNo}”，物料ID[{line.ItemMasterId}]不存在");
                        return;
                    }
                    if (isParts == null)
                    {
                        isParts = !item.Code.StartsWith("10");
                    }
                    else
                    {
                        if (item.Code.StartsWith("10") && isParts == true)
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”，物料为成品，不能与零件一起收货");
                            return;
                        }
                        if (!item.Code.StartsWith("10") && isParts == false)
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”，物料为零件，不能与成品一起收货");
                            return;
                        }
                    }
                    lineDtos.Add(new CreateRcvRptDocLineDTO
                    {
                        MO_DocNo = line.MODocNo,
                        RcvQtyByWhUOM = (decimal)line.Qty,
                        Wh_Code = wh.Code
                    });
                    line.ID = new Guid();
                    line.Status = Model.ProductionRcvRptLineStatusEnum.Reported;
                    line.ToBeReceiveQty = line.Qty;
                    line.ToBeInWhQty = 0;
                    line.ReceiveQty = 0;
                    line.InWhQty = 0;
                    line.DocLineNo = 10 * docLineNo;    // 此处的10不要随意修改，与U9参数设置中的“单据行号步长”一致
                    docLineNo++;
                    if (entity.ShipType != WhShipTypeEnum.SpotGoods)    // 009-现货G2WMS不管控
                    {
                        if (line.ReceivingSubLine == null || line.ReceivingSubLine.Count == 0)
                        {
                            MSD.AddModelError("", "扫码明细行不能为空");
                            return;
                        }
                        decimal receiveQty = 0; // 本次收货数量
                        foreach (var dataSubLine in line.ReceivingSubLine)
                        {
                            BaseBarCode barcode = DC.Set<BaseBarCode>().Include(x => x.Item).Where(x => x.ID == dataSubLine.BarcodeID).AsNoTracking().FirstOrDefault();

                            if (barcode == null)
                            {
                                MSD.AddModelError("", $"行“{line.DocLineNo}”，子行“{dataSubLine.BarcodeID}”条码不存在");
                                return;
                            }
                            if (barcode.Item.Code != item.Code)
                            {
                                MSD.AddModelError("", $"行“{line.DocLineNo}”，子行“{barcode.Code}”条码与行物料不匹配");
                                return;
                            }
                            if (barcode.DocNo != line.MODocNo)
                            {
                                MSD.AddModelError("", $"行“{line.DocLineNo}”，子行“{barcode.Code}”条码与单据不匹配");
                                return;
                            }
                            // 使用字典对条码库存占用进行累加
                            if (barcodeOccupyQtyDict.ContainsKey(barcode.Code))
                            {
                                if (barcodeOccupyQtyDict.TryGetValue(barcode.Code, out (decimal, decimal) v))
                                {
                                    barcodeOccupyQtyDict[barcode.Code] = (v.Item1, v.Item2 + dataSubLine.BarcodeOccupyQty);
                                }
                            }
                            else
                            {
                                // 判断条码是否已被使用
                                if (!this.IsSNNotUsed(barcode.Sn))
                                {
                                    MSD.AddModelError("", $"条码“{barcode.Code}”已被使用，请使用新条码");
                                    return;
                                }

                                barcodeOccupyQtyDict.Add(barcode.Code, ((decimal)barcode.Qty, dataSubLine.BarcodeOccupyQty));
                            }
                            if (barcode.Qty < barcodeOccupyQtyDict[barcode.Code].Item2)
                            {
                                MSD.AddModelError("", $"行“{line.DocLineNo}”，“{barcode.Code}”条码数量“{barcode.Qty}”不能满足截止当前行{line.DocLineNo}的总需求数量“{barcodeOccupyQtyDict[barcode.Code].Item2}”");
                                return;
                            }
                            receiveQty += dataSubLine.BarcodeOccupyQty;
                            // 线边库位增加库存
                            if (newInventoryList.Any(x => x.SerialNumber == barcode.Sn))
                            {
                                var inventory = newInventoryList.Find(x => x.SerialNumber == barcode.Sn);
                                inventory.Qty += dataSubLine.BarcodeOccupyQty;
                                lineIdToInvIdDict.Add((line.ID, inventory.ID, dataSubLine.BarcodeOccupyQty));
                            }
                            else
                            {
                                newInventoryList.Add(new BaseInventory
                                {
                                    ID = Guid.NewGuid(),
                                    ItemMasterId = line.ItemMasterId,
                                    WhLocationId = location?.ID,
                                    BatchNumber = line.MODocNo,
                                    SerialNumber = barcode.Sn,
                                    Seiban = barcode.Seiban,
                                    SeibanRandom = barcode.SeibanRandom,
                                    Qty = dataSubLine.BarcodeOccupyQty,
                                    IsAbandoned = false,
                                    ItemSourceType = ItemSourceTypeEnum.Buy,
                                    FrozenStatus = FrozenStatusEnum.Normal,
                                    CreateBy = LoginUserInfo.ITCode,
                                    CreateTime = DateTime.Now
                                });
                                lineIdToInvIdDict.Add((line.ID, newInventoryList.Last().ID, dataSubLine.BarcodeOccupyQty));
                            }
                        }
                        // 更新行已收货数量和待收货数量
                        if (receiveQty > line.ToBeReceiveQty)   // 本次总收货数量大于待收货数量
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”，总收货数量“{receiveQty}”大于待收货数量“{line.ToBeReceiveQty}”");
                            return;
                        }
                        if (receiveQty < line.ToBeReceiveQty)   // 报工单的报工数量与扫码数量不一致（前端构造错误才会出现的bug）
                        {
                            MSD.AddModelError("", $"行“{line.DocLineNo}”，收货数与扫码数量不匹配，前端参数构造错误");
                            return;
                        }
                    }
                }
                // 将单据保存到本系统中
                if (entity.ShipType != WhShipTypeEnum.SpotGoods)    // 009-现货G2WMS不管控
                {
                    // 验证所有条码均被完全使用
                    foreach (var item in barcodeOccupyQtyDict)
                    {
                        if (item.Value.Item1 < item.Value.Item2)
                        {
                            MSD.AddModelError("", $"“{item.Key}”条码数量“{item.Value.Item1}”不能满足总需求数量“{item.Value.Item2}”");
                            return;
                        }
                        else if (item.Value.Item1 > item.Value.Item2)
                        {
                            MSD.AddModelError("", $"“{item.Key}”条码数量“{item.Value.Item1}”，总需求数量“{item.Value.Item2}”，条码数量必须被完全收货");
                            return;
                        }
                    }
                    Entity = entity;
                    Entity.OrganizationId = wh.OrganizationId;
                    Entity.IsParts = (bool)isParts;
                    Entity.BusinessDate = DateTime.Now.Date;
                    Entity.Status = Model.ProductionRcvRptStatusEnum.Reported;
                    DoAdd();
                    ((DataContext)DC).BulkInsert(newInventoryList);
                }
                // 调用U9接口创建单据
                // 调用U9接口
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, wh.Organization.Code, LoginUserInfo.Name);
                CreateRcvRptDocPara para = new CreateRcvRptDocPara
                {
                    rcvRptDocDTOList = new CreateRcvRptDocDTO
                    {
                        OrgCode = wh.Organization.Code,
                        ActualRcvTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        RcvRptDocLineDTOList = lineDtos
                    }
                };
                U9Return<CreateRcvRptDocResult> u9Return = apiHelper.CreateRcvRptDoc(para);
                if (!u9Return.Success)
                {
                    MSD.AddModelError("", u9Return.Msg);
                    return;
                }

                if (entity.ShipType != WhShipTypeEnum.SpotGoods)    // 009-现货G2WMS不管控
                {
                    entity = DC.Set<ProductionRcvRpt>().Include(x => x.ProductionRcvRptLine_ProductionRcvRpt).Where(x => x.ID == Entity.ID).FirstOrDefault();
                    if (entity.ProductionRcvRptLine_ProductionRcvRpt.Count != u9Return.Entity.Lines.Count)  // 不会出现
                    {
                        MSD.AddModelError("", "U9单据已成功创建，但系统中单据行数与U9单据行数不一致，请联系管理员处理");
                        return;
                    }
                    // 修改单号、ID、行ID等U9返回的信息
                    entity.ErpID = u9Return.Entity.ID;
                    entity.DocNo = u9Return.Entity.DocNo;
                    entity.ProductionRcvRptLine_ProductionRcvRpt = entity.ProductionRcvRptLine_ProductionRcvRpt.OrderBy(x => x.DocLineNo).ToList(); // 按行号排序
                    for (int i = 0; i < entity.ProductionRcvRptLine_ProductionRcvRpt.Count; i++)
                    {
                        entity.ProductionRcvRptLine_ProductionRcvRpt[i].ErpID = u9Return.Entity.Lines[i];
                    }
                    DC.SaveChanges();

                    foreach (var inventory in newInventoryList)
                    {
                        // 创建库存流水
                        if (!this.CreateInvLog(OperationTypeEnum.ProductionRcvRptReport, entity.DocNo, null, inventory.ID, null, inventory.Qty))
                        {
                            return;
                        }
                    }
                    foreach (var item in lineIdToInvIdDict)
                    {
                        // 创建单据库存关联关系
                        if (!this.CreateInvRelation(DocTypeEnum.ProductionRcvRpt, item.Item2, entity.ID, item.Item1, item.Item3))
                        {
                            return;
                        }
                    }
                    trans.Commit();
                }
            }
        }
    }
}
