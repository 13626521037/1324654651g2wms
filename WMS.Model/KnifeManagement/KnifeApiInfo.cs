using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.KnifeManagement;
using WMS.Model.InventoryManagement;

namespace WMS.Model.KnifeManagement
{
    #region  传入参数
    //查询G2在库数量信息[含台账] 传入参数
    public class GetKnifeAndInventoryInStockQtyInputInfo
    {
        public string ItemCodePart { get; set; }

        public List<string> ItemCodes { get; set; }

        public GetKnifeAndInventoryInStockQtyInputInfo()
        {
            ItemCodes = new List<string>();
        }
    }
    //查询刀具/库存信息[pda] 传入参数
    public class GetKnifeAndInventoryInputInfo
    {
        public string? ItemCode { get; set; }
        public string? WhLocationCode { get; set; }
        public string? Seiban { get; set; }
        public string? Bar { get; set; }
    }
    //U9审核/弃审回写修磨申请单 传入参数
    public class UpdateKnifeGrindRequestByU9Input
    {
        public string KnifeSerialNumber { set; get; }
        public string PRDocNo { set; get; }
        public string PRDocLineNo { set; get; }
        public string PODocNo { set; get; }
        public string PODocLineNo { set; get; }
    }

    //折旧履历记录获取传入参数
    public class GetKnifeOperationsInputInfo
    {
        public string? KnifeNo { get; set; }
        public string? Begin { get; set; }
        public string? End { get; set; }
    }

    //刀具修磨入库传入参数
    public class KnifeGrindInInputInfo
    {
        public string WhLocationId { get; set; }
        public string CombineCode { get; set; }
        public string RcvDocNo { get; set; }
        public List<KnifeGrindInInputInfo_Line> Lines { get; set; }
    }
    //刀具修磨入库传入参数_行
    public class KnifeGrindInInputInfo_Line
    {
        public string KnifeId { get; set; }
        public string U9RcvLineId { get; set; }
        public string WhLocationId { get; set; }
        // 修磨刀具号
        public string GrindKnifeNO { get; set; }


    }
    //刀具修磨出库传入参数
    public class KnifeGrindOutInputInfo
    {
        public string CombineCode { get; set; }
        public string PODocNO { get; set; }
        public List<KnifeGrindOutInputInfo_Line> Lines { get; set; }
    }
    //刀具修磨出库传入参数_行
    public class KnifeGrindOutInputInfo_Line
    {
        public string KnifeId { get; set; }
        public string POShipLineId { get; set; }
        public string PRDocNo { get; set; }
        public string PRDocLineNo { get; set; }
        public string PODocNo { get; set; }
        public string PODocLineNo { get; set; }
        // 修磨刀具号
        public string GrindKnifeNO { get; set; }
    }

    //刀具修磨申请传入参数
    public class KnifeGrindRequestInputInfo
    {
        public string Memo { get; set; }
        public string CombineCode { get; set; }
        public List<KnifeGrindRequestInputInfo_Line> Lines { get; set; }
    }
    //刀具修磨申请传入参数_行
    public class KnifeGrindRequestInputInfo_Line
    {
        public string KnifeId { get; set; }
        // 修磨刀具号
        public string GrindKnifeNO { get; set; }
    }

    //刀具调入传入参数
    public class KnifeTransferInInputInfo
    {
        public string TransferOutDocNo { get; set; }
        public string Memo { get; set; }
        public List<KnifeTransferInInputInfo_Line> Lines { get; set; }
    }
    public class KnifeTransferInInputInfo_Line
    {
        public string KnifeId { get; set; }
        public string WhLocationId { get; set; }


    }

    //刀具移库 传入参数
    public class KnifeMoveInputInfo
    {
        public string Memo { get; set; }

        public List<KnifeMoveInputInfo_Line> Lines { get; set; }
    }
    public class KnifeMoveInputInfo_Line
    {
        public string KnifeId { get; set; }
        public string WhLocationId { get; set; }

    }

    //刀具调入修改传入参数

    public class KnifeTransferInInputInfoForUpdate
    {
        public string DocNo { get; set; }
        public string WhLocationId { get; set; }
        public List<KnifeTransferInInputInfo_Knife> Knifes { get; set; }
    }
    public class KnifeTransferInInputInfo_Knife
    {
        public string KnifeId { get; set; }

    }

    //刀具调出传入参数

    public class KnifeTransferOutInputInfo
    {
        public string ToWhId { get; set; }
        public string FromWhId { get; set; }
        public string Memo { get; set; }
        public List<KnifeTransferOutInputInfo_Line> Knifes { get; set; }
    }
    public class KnifeTransferOutInputInfo_Line
    {
        public string KnifeId { get; set; }

    }

    //组合刀具配件替换传入参数
    public class KnifeReplaceInputInfo
    {
        public string KnifeReplaceByID { get; set; }
        public string CombineKnifeNo { get; set; }
        public List<KnifeReplaceInputInfo_Line> Lines { get; set; }
    }
    public class KnifeReplaceInputInfo_Line
    {
        public string WhLocationId { get; set; }

        public string OldKnifeId { get; set; }
        public KnifeReplaceInputInfo_Knife NewKnifeInfo { get; set; }
    }
    public class KnifeReplaceInputInfo_Knife
    {
        public bool IsNew { get; set; }
        public string KnifeId { get; set; }
        public string SerialNumber { get; set; }
        public string bar { get; set; }

    }


    //刀具报废传入参数
    public class KnifeScrapInputInfo
    {
        public KnifeScrapTypeEnum ScrapType { get; set; }
        public List<KnifeScrapInputInfo_Knife> Knifes { get; set; }
    }
    //刀具报废传入参数_刀具
    public class KnifeScrapInputInfo_Knife
    {
        public string KnifeId { get; set; }
        public bool IsAccident { get; set; }
    }

    //OA刀具报废传入参数
    public class OAKnifeScrapInputInfo
    {
        public string ScrapDocNo { get; set; }
        public string OperatorCode { get; set; }
    }

    //刀具归还传入参数
    public class KnifeCheckInInputInfo
    {
        public string CheckInByID { get; set; }
        public KnifeCheckInTypeEnum CheckInType { get; set; }//0普通归还 1错领归还   4组合归还   
        public List<KnifeCheckInInputInfo_Line> Lines { get; set; }
        public List<KnifeCheckInCombineInputInfo_Line> CombineKnifeNoLines { get; set; }
        public int CheckInType2 { get; set; }//默认0普通归还 1维修归还 2报废归还
        public int CheckInTypeUnion { get; set; }//0普通归还 1错领归还 2维修归还 3报废归还 4组合归还 

    }
    //刀具归还传入参数_刀具
    public class KnifeCheckInInputInfo_Line
    {
        public string KnifeId { get; set; }
        public string WhLocationId { get; set; }

    }
    public class KnifeCheckInCombineInputInfo_Line
    {
        public string CombineKnifeNo { get; set; }
        public string WhLocationId { get; set; }

    }
    //期初刀具领用传入参数
    public class BeginningOfPeriodKnifeCheckOutInputInfo
    {
        public string HandledByName { get; set; }
        public string CheckOutByName { get; set; }
        public string BarCode { get; set; }
    }
    //期初刀具领用参数
    public class BeginningOfPeriodKnifeCheckOutInfo
    {
        public FrameworkUser HandledBy{ get; set; }
        public BaseOperator HandledBy_Operator { get; set; }
        public BaseOperator CheckOutBy { get; set; }
        public BaseInventory Inventory { get; set; }

    }
    //刀具领用传入参数
    public class KnifeCheckOutInputInfo
    {
        public string CheckOutByID { get; set; }
        public KnifeCheckOutTypeEnum CheckOutType { get; set; }
        public List<KnifeCheckOutInputInfo_Knife> Knifes { get; set; }

    }
    //刀具领用传入参数_刀具
    public class KnifeCheckOutInputInfo_Knife
    {
        public bool IsNew { get; set; }
        public string KnifeId { get; set; }
        public string SerialNumber { get; set; }
        public string bar { get; set; }

    }



    #endregion
    #region 返回参数

    //查询刀具/库存料号的在库数量信息 返回参数
    public class GetKnifeAndInventoryInStockQtyReturn
    {
        public string 料品 { get; set; }
        public decimal 在库数量 { get; set; }
        public decimal 领用数量 { get; set; }
        public decimal 调拨数量 { get; set; }
        public decimal 报废数量 { get; set; }
        public decimal 修磨申请数量 { get; set; }
        public decimal 修磨出库数量 { get; set; }
        public decimal 不良退回数量 { get; set; }
        public decimal 盘亏数量 { get; set; }
        public decimal 报废申请数量 { get; set; }
    }

    //查询刀具/库存信息 返回参数
    public class GetKnifeAndInventoryReturn
    {
        public bool IsKnife { get; set; }
        public string ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemSpecs { get; set; }
        public string ItemOrgID { get; set; }
        public string ItemOrgCode { get; set; }
        public string ItemOrgName { get; set; }
        public string Qty { get; set; }
        public string? WhLocationID { get; set; }
        public string? WhLocationCode { get; set; }
        public string? WhLocationName { get; set; }
        public string? Seiban { get; set; }
        public string? Bar { get; set; }
        public string? BatchNumber { get; set; }
    }


    //打印返回参数
    public class BlueToothPrintDataLineReturn
    {
        public bool IsKnife { get; set; }
        public bool IsNew { get; set; }

        public string bar { get; set; }//组合刀号或者刀具条码
        public string SerialNumber { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Specs { get; set; }
        public string Qty { get; set; }
        public string CurrentDate { get; set; }
        public string NotG { get; set; }
    }

    //林琪伦 推荐库位返回参数
    /// <summary>
    /// 推荐库位
    /// </summary>
    public class RecommendLocationReturn
    {
        /// <summary>
        /// 库区编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 库位编码
        /// </summary>
        public string LocCode { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string Batch { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }
    }

    //修磨入库扫收货单获取刀具列表信息返回参数
    public class KnifeGrindInLineReturn
    {
        public string KnifeId { get; set; }
        public string SerialNumber { get; set; }
        public string U9RcvLineId { get; set; }
        public string GrindKnifeNO { get; set; }
        /// <summary>
        /// 推荐库位列表（根据刀具料号查找相同料号刀具所在库位）
        /// </summary>
        public List<RecommendLocationReturn> SuggestLocs { get; set; }
    }
    //修磨出库扫采购单获取刀具列表信息返回参数
    public class KnifeGrindOutLineGetFromU9POReturn
    {
        public string KnifeId { get; set; }
        public string SerialNumber { get; set; }
        public bool IsClose { get; set; }
        public string POShipLineId { get; set; }
        public string PRDocNo { get; set; }
        public string PRDocLineNo { get; set; }
        public string PODocNo { get; set; }
        public string PODocLineNo { get; set; }
        public string GrindKnifeNO { get; set; }

    }

    //调入单扫码获取调出单单行信息返回参数
    public class KnifeTransferOutLineReturn
    {
        public string KnifeId { get; set; }
        public string SerialNumber { get; set; }
        public bool IsClose { get; set; }
        /// <summary>
        /// 推荐库位列表（三步策略）
        /// </summary>
        public List<RecommendLocationReturn> SuggestLocs { get; set; }


    }

    //刀具获取部门返回参数
    public class KnifeDeptReturn
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

    }
    //刀具获取业务员返回参数
    public class KnifeOperatorReturn
    {
        public string ID { get; set; }
        public string Name { get; set; }


    }
    //刀具获取刀返回参数
    public class KnifeItemMasterReturn
    {
        public bool IsNew { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string SPECS { get; set; }
        public decimal? Qty { get; set; }
        public string KnifeID { get; set; }
        public string SerialNumber { get; set; }
        public string bar { get; set; }

        public string CheckOutByName { get; set; }
        public string CheckOutByID { get; set; }
        public string DeptName { get; set; }
        public string CurrentWhLocationId { get; set; }
        public string CurrentWhLocationCode { get; set; }
        public string CurrentWhLocationName { get; set; }
        public string CombineKnifeNo { get; set; }
        public string CombineKnifeNo_bar { get; set; }
        public string GrindKnifeNO { get; set; }

    }

    //获取刀具履历信息返回参数
    public class KnifeOperationsReturn
    {
        public string Id { get; set; }

        public string KnifeNo { get; set; }
        public KnifeOperationTypeEnum OperationType { get; set; }
        public string OperationType_Trl { get; set; }

        public string OperationTime { get; set; }
        public string OperationBy { get; set; }
        public string HandledBy { get; set; }
        public decimal? UsedDays { get; set; }
        public decimal? TotalUsedDays { get; set; }
        public decimal? RemainingDays { get; set; }
        public decimal? CurrentLife { get; set; }
        public string WhLocation { get; set; }
        public string WareHouseId { get; set; }
        public string WareHouseCode { get; set; }
        public string WareHouseName { get; set; }

        public decimal GrindNum { get; set; }
        public string U9SourceLineID { get; set; }
        public string U9SourceLineType { get; set; }
        public string ItemId { get; set; }
        public string ItemCode { get; set; }
        public string DocNo { get; set; }
        public string OperationDeptId { get; set; }
        public string OperationDeptCode { get; set; }
        public string OperationDeptName { get; set; }
        public string MsicShipLineID { get; set; }
        public string Remake { get; set; }
        
    }
    //获取刀具料品寿命返回参数
    public class KnifeItemLivesReturn
    {
        public string ItemCode { get; set; }

        public string InitialLife { get; set; }
        public List<GrindLifePair> GrindLivePairs { get; set; }
        public KnifeItemLivesReturn()
        {
            GrindLivePairs = new List<GrindLifePair>();
        }
    }
    public class GrindLifePair
    {
        public string GrindNum { get; set; }  // 修磨次数（字符串形式）
        public decimal GrindLife { get; set; }     // 修磨寿命（整数）

        public GrindLifePair(string grindNum, decimal grindLife)
        {
            GrindNum = grindNum;
            GrindLife = grindLife;
        }
    }
    #endregion



}
