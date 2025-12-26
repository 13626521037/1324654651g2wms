using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WMS.Model
{
    public enum ItemSourceTypeEnum
    {
        [Display(Name = "外购")]
        Buy = 1,

        [Display(Name = "原WMS")]
        OldWms = 2,

        [Display(Name = "自制")]
        Make = 3,
    }
    public enum EffectiveEnum
    {
        [Display(Name = "_Enum._EffectiveEnum._Effective")]
        Effective = 1,
        [Display(Name = "_Enum._EffectiveEnum._Ineffective")]
        Ineffective = 0
    }
    /// <summary>
    /// 存储地点发货属性。注意：发货属性在ERP中增加时，在此处也需要增加。响应的接口也需要增加。（否则新值无法正确同步）
    /// </summary>
    public enum WhShipTypeEnum
    {
        [Display(Name = "_Enum._WhShipTypeEnum._ToCustomer")]
        ToCustomer = 1,
        [Display(Name = "_Enum._WhShipTypeEnum._ToSaleCompany")]
        ToSaleCompany = 6,
        [Display(Name = "_Enum._WhShipTypeEnum._WaitToShip")]
        WaitToShip = 8,
        [Display(Name = "_Enum._WhShipTypeEnum._SpotGoods")]
        SpotGoods = 9
    }
    public enum WhAreaEnum
    {
        [Display(Name = "_Enum._WhAreaEnum._Normal")]
        Normal = 1,
        [Display(Name = "_Enum._WhAreaEnum._WaitForInspect")]
        WaitForInspect = 2,
        [Display(Name = "_Enum._WhAreaEnum._WaitForReceive")]
        WaitForReceive = 3,
        [Display(Name = "_Enum._WhAreaEnum._WaitForDeliver")]
        WaitForDeliver = 4,
        [Display(Name = "_Enum._WhAreaEnum._UnQualified")]
        UnQualified = 5,
        [Display(Name = "_Enum._WhAreaEnum._Rejected")]
        Rejected = 6,
        [Display(Name = "_Enum._WhAreaEnum._Reinspect")]
        Reinspect = 7,
        [Display(Name = "_Enum._WhAreaEnum._LineEdge")]
        LineEdge = 8,
        [Display(Name = "_Enum._WhAreaEnum._Inspected")]
        Inspected = 9
    }
    public enum WhLocationEnum
    {
        [Display(Name = "_Enum._WhLocationEnum._Normal")]
        Normal = 1,
        [Display(Name = "_Enum._WhLocationEnum._WaitForInspect")]
        WaitForInspect = 2,
        [Display(Name = "_Enum._WhLocationEnum._WaitForReceive")]
        WaitForReceive = 3,
        [Display(Name = "_Enum._WhLocationEnum._WaitForDeliver")]
        WaitForDeliver = 4,
        [Display(Name = "_Enum._WhLocationEnum._UnQualified")]
        UnQualified = 5,
        [Display(Name = "_Enum._WhLocationEnum._Rejected")]
        Rejected = 6,
        [Display(Name = "_Enum._WhLocationEnum._Reinspect")]
        Reinspect = 7,
        [Display(Name = "_Enum._WhLocationEnum._LineEdge")]
        LineEdge = 8,
        [Display(Name = "_Enum._WhLocationEnum._Inspected")]
        Inspected = 9
    }
    public enum ItemFormAttributeEnum
    {
        [Display(Name = "_Enum._ItemFormAttributeEnum._MakePart")]
        MakePart = 10,
        [Display(Name = "_Enum._ItemFormAttributeEnum._PurchasePart")]
        PurchasePart = 9,
        [Display(Name = "_Enum._ItemFormAttributeEnum._SubcontractPart")]
        SubcontractPart = 4
    }
    public enum FrozenStatusEnum
    {
        [Display(Name = "_Enum._FrozenStatusEnum._Normal")]
        Normal = 0,
        [Display(Name = "_Enum._FrozenStatusEnum._Freezed")]
        Freezed = 1
    }
    public enum DocTypeEnum
    {
        [Display(Name = "_Enum._DocTypeEnum._ShipPlan")]
        ShipPlan = 0,
        [Display(Name = "_Enum._DocTypeEnum._Ship")]
        Ship = 1,
        [Display(Name = "_Enum._DocTypeEnum._RMA")]
        RMA = 2,
        [Display(Name = "_Enum._DocTypeEnum._ReturnReceivement")]
        ReturnReceivement = 3,
        [Display(Name = "_Enum._DocTypeEnum._Batch")]
        Batch = 4,
        [Display(Name = "_Enum._DocTypeEnum._PurchaseDelivery")]
        PurchaseDelivery = 5,
        [Display(Name = "_Enum._DocTypeEnum._PurchaseReceivement")]
        PurchaseReceivement = 6,
        [Display(Name = "_Enum._DocTypeEnum._PurchaseReturn")]
        PurchaseReturn = 7,
        [Display(Name = "_Enum._DocTypeEnum._OutsourcingIssue")]
        OutsourcingIssue = 8,
        [Display(Name = "_Enum._DocTypeEnum._OutsourcingReturn")]
        OutsourcingReturn = 9,
        [Display(Name = "_Enum._DocTypeEnum._ProductionPreIssue")]
        ProductionPreIssue = 10,
        [Display(Name = "_Enum._DocTypeEnum._ProductionIssue")]
        ProductionIssue = 11,
        [Display(Name = "_Enum._DocTypeEnum._ProductionReturnIssue")]
        ProductionReturnIssue = 12,
        [Display(Name = "_Enum._DocTypeEnum._ProductionRcvRpt")]
        ProductionRcvRpt = 13,
        [Display(Name = "_Enum._DocTypeEnum._InventoryOtherShip")]
        InventoryOtherShip = 14,
        [Display(Name = "_Enum._DocTypeEnum._InventoryOtherReceivement")]
        InventoryOtherReceivement = 15,
        [Display(Name = "_Enum._DocTypeEnum._InventoryTransferOutDirect")]
        InventoryTransferOutDirect = 16,
        [Display(Name = "_Enum._DocTypeEnum._InventoryTransferOutManual")]
        InventoryTransferOutManual = 17,
        [Display(Name = "_Enum._DocTypeEnum._InventoryTransferIn")]
        InventoryTransferIn = 18,
        [Display(Name = "_Enum._DocTypeEnum._InventoryPalletVirtual")]
        InventoryPalletVirtual = 19,
        [Display(Name = "_Enum._DocTypeEnum._InventoryFreeze")]
        InventoryFreeze = 20,
        [Display(Name = "_Enum._DocTypeEnum._InventoryUnfreeze")]
        InventoryUnfreeze = 21,
        [Display(Name = "_Enum._DocTypeEnum._InventoryStockTaking")]
        InventoryStockTaking = 22,
        [Display(Name = "_Enum._DocTypeEnum._InventoryMoveLocation")]
        InventoryMoveLocation = 23,
        [Display(Name = "_Enum._DocTypeEnum._InventoryLendTrans")]
        InventoryLendTrans = 24,
        [Display(Name = "_Enum._DocTypeEnum._InventoryLendBackTrans")]
        InventoryLendBackTrans = 25,
        [Display(Name = "_Enum._DocTypeEnum._InventoryTransfer")]
        InventoryTransfer = 26,
        [Display(Name = "_Enum._DocTypeEnum._InventorySplit")]
        InventorySplit = 27,
        [Display(Name = "_Enum._DocTypeEnum._InventoryAdjustDirect")]
        InventoryAdjustDirect = 28,
        [Display(Name = "刀具领用")]
        KnifeCheckOut = 29,
        [Display(Name = "刀具归还")]
        KnifeCheckIn = 30,
        [Display(Name = "刀具报废")]
        KnifeScrap = 31,
        [Display(Name = "刀具组合")]
        KnifeCombine = 32,
        [Display(Name = "刀具修磨申请")]
        KnifeGrindRequest = 33,
        [Display(Name = "刀具修磨出库")]
        KnifeGrindOut = 34,
        [Display(Name = "刀具修磨入库")]
        KnifeGrindIn = 35,
        [Display(Name = "刀具调出")]
        KnifeTransferOut = 36,
        [Display(Name = "刀具调入")]
        KnifeTransferIn = 37,
        [Display(Name = "刀号")]
        KnifeNo = 38,
        [Display(Name = "库存拆零")]
        InventorySplitSingle = 39,
        [Display(Name = "库存调整单")]
        InventoryAdjust = 40

    }
    public enum SegmentTypeEnum
    {
        [Display(Name = "_Enum._SegmentTypeEnum._Constant")]
        Constant = 0,
        [Display(Name = "_Enum._SegmentTypeEnum._OrganizationCode")]
        OrganizationCode = 1,
        [Display(Name = "_Enum._SegmentTypeEnum._CurrentDate")]
        CurrentDate = 2,
        [Display(Name = "_Enum._SegmentTypeEnum._Serial")]
        Serial = 3,
        [Display(Name = "料品大类")]
        //[Display(Name = "_Enum._SegmentTypeEnum._ItemCategory")]
        ItemCategory = 4,
    }
    public enum DateFormatEnum
    {
        [Display(Name = "_Enum._DateFormatEnum._YYMMDD")]
        YYMMDD = 0,
        [Display(Name = "_Enum._DateFormatEnum._YYMM")]
        YYMM = 1,
        [Display(Name = "_Enum._DateFormatEnum._YYYYMMDD")]
        YYYYMMDD = 2,
        [Display(Name = "_Enum._DateFormatEnum._YYYYMM")]
        YYYYMM = 3
    }

    public enum OperationTypeEnum
    {
        [Display(Name = "_Enum._OperationTypeEnum._ShipPlanOff")]
        ShipPlanOff = 0,
        [Display(Name = "_Enum._OperationTypeEnum._ShipPlanOffCancel")]
        ShipPlanOffCancel = 1,
        [Display(Name = "_Enum._OperationTypeEnum._ShipPlanApprove")]
        ShipPlanApprove = 2,
        [Display(Name = "_Enum._OperationTypeEnum._ShipOff")]
        ShipOff = 3,
        [Display(Name = "_Enum._OperationTypeEnum._ShipOffCancel")]
        ShipOffCancel = 4,
        [Display(Name = "_Enum._OperationTypeEnum._ShipApprove")]
        ShipApprove = 5,
        [Display(Name = "_Enum._OperationTypeEnum._RMAReceive")]
        RMAReceive = 6,
        [Display(Name = "_Enum._OperationTypeEnum._RMAReceiveCancel")]
        RMAReceiveCancel = 7,
        [Display(Name = "_Enum._OperationTypeEnum._RMAReceiveApprove")]
        RMAReceiveApprove = 8,
        [Display(Name = "_Enum._OperationTypeEnum._ReturnReceivementReceive")]
        ReturnReceivementReceive = 9,
        [Display(Name = "_Enum._OperationTypeEnum._ReturnReceivementReceiveCancel")]
        ReturnReceivementReceiveCancel = 10,
        [Display(Name = "_Enum._OperationTypeEnum._ReturnReceivementApprove")]
        ReturnReceivementApprove = 11,
        [Display(Name = "_Enum._OperationTypeEnum._PurchaseReceivementInspect")]
        PurchaseReceivementInspect = 12,
        [Display(Name = "_Enum._OperationTypeEnum._PurchaseReceivementInspectCancel")]
        PurchaseReceivementInspectCancel = 13,
        [Display(Name = "_Enum._OperationTypeEnum._PurchaseReceivementReceive")]
        PurchaseReceivementReceive = 14,
        [Display(Name = "_Enum._OperationTypeEnum._PurchaseReceivementReceiveCancel")]
        PurchaseReceivementReceiveCancel = 15,
        [Display(Name = "_Enum._OperationTypeEnum._PurchaseReceivementApprove")]
        PurchaseReceivementApprove = 16,
        [Display(Name = "_Enum._OperationTypeEnum._PurchaseReturnOff")]
        PurchaseReturnOff = 17,
        [Display(Name = "_Enum._OperationTypeEnum._PurchaseReturnOffCancel")]
        PurchaseReturnOffCancel = 18,
        [Display(Name = "_Enum._OperationTypeEnum._PurchaseReturnApprove")]
        PurchaseReturnApprove = 19,
        [Display(Name = "_Enum._OperationTypeEnum._OutsourcingIssueOff")]
        OutsourcingIssueOff = 20,
        [Display(Name = "_Enum._OperationTypeEnum._OutsourcingIssueOffCancel")]
        OutsourcingIssueOffCancel = 21,
        [Display(Name = "_Enum._OperationTypeEnum._OutsourcingIssueApprove")]
        OutsourcingIssueApprove = 22,
        [Display(Name = "_Enum._OperationTypeEnum._OutsourcingReturnReceive")]
        OutsourcingReturnReceive = 23,
        [Display(Name = "_Enum._OperationTypeEnum._OutsourcingReturnReceiveCancel")]
        OutsourcingReturnReceiveCancel = 24,
        [Display(Name = "_Enum._OperationTypeEnum._OutsourcingReturnApprove")]
        OutsourcingReturnApprove = 25,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionPreIssueOff")]
        ProductionPreIssueOff = 26,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionPreIssueOffCancel")]
        ProductionPreIssueOffCancel = 27,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionPreIssueApprove")]
        ProductionPreIssueApprove = 28,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionIssueOff")]
        ProductionIssueOff = 29,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionIssueOffCancel")]
        ProductionIssueOffCancel = 30,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionIssueApprove")]
        ProductionIssueApprove = 31,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionReturnIssueReceive")]
        ProductionReturnIssueReceive = 32,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionReturnIssueReceiveCancel")]
        ProductionReturnIssueReceiveCancel = 33,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionRcvRptReport")]
        ProductionRcvRptReport = 34,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionRcvRptReportCancel")]
        ProductionRcvRptReportCancel = 35,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionRcvRptReceive")]
        ProductionRcvRptReceive = 36,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionRcvRptReceiveCancel")]
        ProductionRcvRptReceiveCancel = 37,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionRcvRptApprove")]
        ProductionRcvRptApprove = 38,
        [Display(Name = "_Enum._OperationTypeEnum._ProductionReturnIssueApprove")]
        ProductionReturnIssueApprove = 39,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryOtherShipCreate")]
        InventoryOtherShipCreate = 40,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryOtherReceivementCreate")]
        InventoryOtherReceivementCreate = 41,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryTransferOutDirectCreate")]
        InventoryTransferOutDirectCreate = 42,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryTransferOutManualOff")]
        InventoryTransferOutManualOff = 43,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryTransferOutManualOffCancel")]
        InventoryTransferOutManualOffCancel = 44,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryTransferOutManualApprove")]
        InventoryTransferOutManualApprove = 45,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryTransferInCreate")]
        InventoryTransferInCreate = 46,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryTransferInApprove")]
        InventoryTransferInApprove = 47,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryStockTakingApprove")]
        InventoryStockTakingApprove = 48,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryTransfer")]
        InventoryTransfer = 49,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryLendTransCreate")]
        InventoryLendTransCreate = 50,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryLendBackTransCreate")]
        InventoryLendBackTransCreate = 51,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryTransferOff")]
        InventoryTransferOff = 52,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryTransferOffCancel")]
        InventoryTransferOffCancel = 53,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryTransferApprove")]
        InventoryTransferApprove = 54,
        [Display(Name = "_Enum._OperationTypeEnum._InventorySplitCreate")]
        InventorySplitCreate = 55,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryAdjustDirectCreate")]
        InventoryAdjustDirectCreate = 56,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryPalletVirtualCreate")]
        InventoryPalletVirtualCreate = 57,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryFreezeCreate")]
        InventoryFreezeCreate = 58,
        [Display(Name = "_Enum._OperationTypeEnum._InventoryUnfreezeCreate")]
        InventoryUnfreezeCreate = 59,
        [Display(Name = "调入单删除")]
        InventoryTransferInDelete = 60,
        [Display(Name = "移库")]
        InventoryMoveLocation = 61,
        [Display(Name = "库存拆零单创建")]
        InventorySplitSingleCreate = 62,
        [Display(Name = "库存调整创建")]
        InventoryAdjustCreate = 63
    }

    public enum PurchaseReceivementInspectStatusEnum
    {
        [Display(Name = "_Enum._PurchaseReceivementInspectStatusEnum._NotInspect")]
        NotInspect = 0,
        [Display(Name = "_Enum._PurchaseReceivementInspectStatusEnum._PartInspected")]
        PartInspected = 1,
        [Display(Name = "_Enum._PurchaseReceivementInspectStatusEnum._AllInspected")]
        AllInspected = 2
    }
    public enum PurchaseReceivementLineStatusEnum
    {
        [Display(Name = "_Enum._PurchaseReceivementLineStatusEnum._NotReceive")]
        NotReceive = 0,
        [Display(Name = "_Enum._PurchaseReceivementLineStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._PurchaseReceivementLineStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._PurchaseReceivementLineStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._PurchaseReceivementLineStatusEnum._AllInWh")]
        AllInWh = 4
    }
    public enum PurchaseReceivementLineInspectStatusEnum
    {
        [Display(Name = "_Enum._PurchaseReceivementLineInspectStatusEnum._NotInspect")]
        NotInspect = 0,
        [Display(Name = "_Enum._PurchaseReceivementLineInspectStatusEnum._PartInspected")]
        PartInspected = 1,
        [Display(Name = "_Enum._PurchaseReceivementLineInspectStatusEnum._AllInspected")]
        AllInspected = 2
    }
    public enum BizTypeEnum
    {
        [Display(Name = "_Enum._BizTypeEnum._PM005")]
        PM005 = 316,
        [Display(Name = "_Enum._BizTypeEnum._PM055")]
        PM055 = 326
    }
    public enum PurchaseReceivementStatusEnum
    {
        [Display(Name = "_Enum._PurchaseReceivementStatusEnum._NotReceive")]
        NotReceive = 0,
        [Display(Name = "_Enum._PurchaseReceivementStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._PurchaseReceivementStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._PurchaseReceivementStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._PurchaseReceivementStatusEnum._AllInWh")]
        AllInWh = 4
    }
    public enum QCRejectDealEnum
    {
        [Display(Name = "_Enum._QCRejectDealEnum._Rework")]
        Rework = 1,
        [Display(Name = "_Enum._QCRejectDealEnum._ItemWaste")]
        ItemWaste = 2,
        [Display(Name = "_Enum._QCRejectDealEnum._PreProcessWasteByUs")]
        PreProcessWasteByUs = 3,
        [Display(Name = "_Enum._QCRejectDealEnum._ThisProcessWaste")]
        ThisProcessWaste = 4,
        [Display(Name = "_Enum._QCRejectDealEnum._PreProcessWasteBySupplier")]
        PreProcessWasteBySupplier = 5
    }
    public enum PurchaseReturnStatusEnum
    {
        [Display(Name = "_Enum._PurchaseReturnStatusEnum._InWh")]
        InWh,
        [Display(Name = "_Enum._PurchaseReturnStatusEnum._PartOff")]
        PartOff,
        [Display(Name = "_Enum._PurchaseReturnStatusEnum._AllOff")]
        AllOff,
        [Display(Name = "_Enum._PurchaseReturnStatusEnum._PartShipped")]
        PartShipped,
        [Display(Name = "_Enum._PurchaseReturnStatusEnum._AllShipped")]
        AllShipped
    }
    public enum PurchaseReturnLineStatusEnum
    {
        [Display(Name = "_Enum._PurchaseReturnLineStatusEnum._InWh")]
        InWh,
        [Display(Name = "_Enum._PurchaseReturnLineStatusEnum._PartOff")]
        PartOff,
        [Display(Name = "_Enum._PurchaseReturnLineStatusEnum._AllOff")]
        AllOff,
        [Display(Name = "_Enum._PurchaseReturnLineStatusEnum._PartShipped")]
        PartShipped,
        [Display(Name = "_Enum._PurchaseReturnLineStatusEnum._AllShipped")]
        AllShipped
    }

    public enum InventoryTransferOutManualStatusEnum
    {
        [Display(Name = "_Enum._InventoryTransferOutManualStatusEnum._InWh")]
        InWh,
        [Display(Name = "_Enum._InventoryTransferOutManualStatusEnum._PartOff")]
        PartOff,
        [Display(Name = "_Enum._InventoryTransferOutManualStatusEnum._AllOff")]
        AllOff,
        [Display(Name = "_Enum._InventoryTransferOutManualStatusEnum._PartShipped")]
        PartShipped,
        [Display(Name = "_Enum._InventoryTransferOutManualStatusEnum._AllShipped")]
        AllShipped
    }
    public enum InventoryTransferOutManualLineStatusEnum
    {
        [Display(Name = "_Enum._InventoryTransferOutManualLineStatusEnum._InWh")]
        InWh,
        [Display(Name = "_Enum._InventoryTransferOutManualLineStatusEnum._PartOff")]
        PartOff,
        [Display(Name = "_Enum._InventoryTransferOutManualLineStatusEnum._AllOff")]
        AllOff,
        [Display(Name = "_Enum._InventoryTransferOutManualLineStatusEnum._PartShipped")]
        PartShipped,
        [Display(Name = "_Enum._InventoryTransferOutManualLineStatusEnum._AllShipped")]
        AllShipped
    }
    public enum InventoryTransferOutTypeEnum
    {
        [Display(Name = "_Enum._InventoryTransferOutTypeEnum._Direct")]
        Direct,
        [Display(Name = "_Enum._InventoryTransferOutTypeEnum._Manual")]
        Manual
    }
    public enum InventoryTransferInStatusEnum
    {
        [Display(Name = "_Enum._InventoryTransferInStatusEnum._NotReceive")]
        NotReceive,
        [Display(Name = "_Enum._InventoryTransferInStatusEnum._PartReceive")]
        PartReceive,
        [Display(Name = "_Enum._InventoryTransferInStatusEnum._AllReceive")]
        AllReceive,
        [Display(Name = "_Enum._InventoryTransferInStatusEnum._PartInWh")]
        PartInWh,
        [Display(Name = "_Enum._InventoryTransferInStatusEnum._AllInWh")]
        AllInWh
    }
    public enum InventoryTransferInLineStatusEnum
    {
        [Display(Name = "_Enum._InventoryTransferInLineStatusEnum._NotReceive")]
        NotReceive,
        [Display(Name = "_Enum._InventoryTransferInLineStatusEnum._PartReceive")]
        PartReceive,
        [Display(Name = "_Enum._InventoryTransferInLineStatusEnum._AllReceive")]
        AllReceive,
        [Display(Name = "_Enum._InventoryTransferInLineStatusEnum._PartInWh")]
        PartInWh,
        [Display(Name = "_Enum._InventoryTransferInLineStatusEnum._AllInWh")]
        AllInWh
    }

    public enum InventoryStockTakingDimensionEnum
    {
        [Display(Name = "_Enum._InventoryStockTakingDimensionEnum._Warehouse")]
        Warehouse,
        [Display(Name = "_Enum._InventoryStockTakingDimensionEnum._Area")]
        Area,
        [Display(Name = "_Enum._InventoryStockTakingDimensionEnum._Location")]
        Location
    }
    public enum InventoryStockTakingStatusEnum
    {
        [Display(Name = "_Enum._InventoryStockTakingStatusEnum._Opened")]
        Opened = 0,
        [Display(Name = "_Enum._InventoryStockTakingStatusEnum._Approving")]
        Approving = 1,
        [Display(Name = "盘点锁定")]
        Locked = 2,
        [Display(Name = "_Enum._InventoryStockTakingStatusEnum._Approved")]
        Approved = 3,
        [Display(Name = "_Enum._InventoryStockTakingStatusEnum._Closed")]
        Closed = 4,
        [Display(Name = "终止关闭")]
        ForceClosed = 5,
    }
    public enum GainLossStatusEnum
    {
        [Display(Name = "_Enum._GainLossStatusEnum._NotStart")]
        NotStart = 0,
        [Display(Name = "_Enum._GainLossStatusEnum._Equal")]
        Equal = 1,
        [Display(Name = "_Enum._GainLossStatusEnum._Loss")]
        Loss = 2,
        [Display(Name = "_Enum._GainLossStatusEnum._Gain")]
        Gain = 3
    }

    public enum InventoryStockTakingModeEnum
    {
        [Display(Name = "_Enum._InventoryStockTakingModeEnum._ErpWms")]
        ErpWms = 0,
        [Display(Name = "_Enum._InventoryStockTakingModeEnum._Wms")]
        Wms = 1
    }

    public enum PurchaseOutsourcingIssueStatusEnum
    {
        [Display(Name = "_Enum._PurchaseOutsourcingIssueStatusEnum._InWh")]
        InWh = 0,
        [Display(Name = "_Enum._PurchaseOutsourcingIssueStatusEnum._PartOff")]
        PartOff = 1,
        [Display(Name = "_Enum._PurchaseOutsourcingIssueStatusEnum._AllOff")]
        AllOff = 2,
        [Display(Name = "_Enum._PurchaseOutsourcingIssueStatusEnum._PartShipped")]
        PartShipped = 3,
        [Display(Name = "_Enum._PurchaseOutsourcingIssueStatusEnum._AllShipped")]
        AllShipped = 4
    }
    public enum PurchaseOutsourcingIssueLineStatusEnum
    {
        [Display(Name = "_Enum._PurchaseOutsourcingIssueLineStatusEnum._InWh")]
        InWh = 0,
        [Display(Name = "_Enum._PurchaseOutsourcingIssueLineStatusEnum._PartOff")]
        PartOff = 1,
        [Display(Name = "_Enum._PurchaseOutsourcingIssueLineStatusEnum._AllOff")]
        AllOff = 2,
        [Display(Name = "_Enum._PurchaseOutsourcingIssueLineStatusEnum._PartShipped")]
        PartShipped = 3,
        [Display(Name = "_Enum._PurchaseOutsourcingIssueLineStatusEnum._AllShipped")]
        AllShipped = 4
    }
    public enum PurchaseOutsourcingReturnStatusEnum
    {
        [Display(Name = "_Enum._PurchaseOutsourcingReturnStatusEnum._NotReceive")]
        NotReceive = 0,
        [Display(Name = "_Enum._PurchaseOutsourcingReturnStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._PurchaseOutsourcingReturnStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._PurchaseOutsourcingReturnStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._PurchaseOutsourcingReturnStatusEnum._AllInWh")]
        AllInWh = 4
    }
    public enum PurchaseOutsourcingReturnLineStatusEnum
    {
        [Display(Name = "_Enum._PurchaseOutsourcingReturnLineStatusEnum._NotReceive")]
        NotReceive = 0,
        [Display(Name = "_Enum._PurchaseOutsourcingReturnLineStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._PurchaseOutsourcingReturnLineStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._PurchaseOutsourcingReturnLineStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._PurchaseOutsourcingReturnLineStatusEnum._AllInWh")]
        AllInWh = 4
    }

    public enum SalesShipStatusEnum
    {
        [Display(Name = "_Enum._SalesShipStatusEnum._InWh")]
        InWh = 0,
        [Display(Name = "_Enum._SalesShipStatusEnum._PartOff")]
        PartOff = 1,
        [Display(Name = "_Enum._SalesShipStatusEnum._AllOff")]
        AllOff = 2,
        [Display(Name = "_Enum._SalesShipStatusEnum._PartShipped")]
        PartShipped = 3,
        [Display(Name = "_Enum._SalesShipStatusEnum._AllShipped")]
        AllShipped = 4
    }
    public enum SalesShipLineStatusEnum
    {
        [Display(Name = "_Enum._SalesShipLineStatusEnum._InWh")]
        InWh = 0,
        [Display(Name = "_Enum._SalesShipLineStatusEnum._PartOff")]
        PartOff = 1,
        [Display(Name = "_Enum._SalesShipLineStatusEnum._AllOff")]
        AllOff = 2,
        [Display(Name = "_Enum._SalesShipLineStatusEnum._PartShipped")]
        PartShipped = 3,
        [Display(Name = "_Enum._SalesShipLineStatusEnum._AllShipped")]
        AllShipped = 4
    }

    public enum SalesRMAStatusEnum
    {
        [Display(Name = "_Enum._SalesRMAStatusEnum._NotReceive")]
        NotReceive = 0,
        [Display(Name = "_Enum._SalesRMAStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._SalesRMAStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._SalesRMAStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._SalesRMAStatusEnum._AllInWh")]
        AllInWh = 4
    }
    public enum SalesRMALineStatusEnum
    {
        [Display(Name = "_Enum._SalesRMALineStatusEnum._NotReceive")]
        NotReceive = 0,
        [Display(Name = "_Enum._SalesRMALineStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._SalesRMALineStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._SalesRMALineStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._SalesRMALineStatusEnum._AllInWh")]
        AllInWh = 4
    }
    public enum SalesReturnReceivementStatusEnum
    {
        [Display(Name = "_Enum._SalesReturnReceivementStatusEnum._NotReceive")]
        NotReceive = 0,
        [Display(Name = "_Enum._SalesReturnReceivementStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._SalesReturnReceivementStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._SalesReturnReceivementStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._SalesReturnReceivementStatusEnum._AllInWh")]
        AllInWh = 4
    }
    public enum SalesReturnReceivementLineStatusEnum
    {
        [Display(Name = "_Enum._SalesReturnReceivementLineStatusEnum._NotReceive")]
        NotReceive = 0,
        [Display(Name = "_Enum._SalesReturnReceivementLineStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._SalesReturnReceivementLineStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._SalesReturnReceivementLineStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._SalesReturnReceivementLineStatusEnum._AllInWh")]
        AllInWh = 4
    }
    public enum ProductionIssueStatusEnum
    {
        [Display(Name = "_Enum._ProductionIssueStatusEnum._InWh")]
        InWh = 0,
        [Display(Name = "_Enum._ProductionIssueStatusEnum._PartOff")]
        PartOff = 1,
        [Display(Name = "_Enum._ProductionIssueStatusEnum._AllOff")]
        AllOff = 2,
        [Display(Name = "_Enum._ProductionIssueStatusEnum._PartShipped")]
        PartShipped = 3,
        [Display(Name = "_Enum._ProductionIssueStatusEnum._AllShipped")]
        AllShipped = 4
    }
    public enum ProductionIssueLineStatusEnum
    {
        [Display(Name = "_Enum._ProductionIssueLineStatusEnum._InWh")]
        InWh = 0,
        [Display(Name = "_Enum._ProductionIssueLineStatusEnum._PartOff")]
        PartOff = 1,
        [Display(Name = "_Enum._ProductionIssueLineStatusEnum._AllOff")]
        AllOff = 2,
        [Display(Name = "_Enum._ProductionIssueLineStatusEnum._PartShipped")]
        PartShipped = 3,
        [Display(Name = "_Enum._ProductionIssueLineStatusEnum._AllShipped")]
        AllShipped = 4
    }
    public enum ProductionReturnIssueStatusEnum
    {
        [Display(Name = "_Enum._ProductionReturnIssueStatusEnum._NotReceive")]
        NotReceive = 0,
        [Display(Name = "_Enum._ProductionReturnIssueStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._ProductionReturnIssueStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._ProductionReturnIssueStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._ProductionReturnIssueStatusEnum._AllInWh")]
        AllInWh = 4
    }
    public enum ProductionReturnIssueLineStatusEnum
    {
        [Display(Name = "_Enum._ProductionReturnIssueLineStatusEnum._NotReceive")]
        NotReceive = 0,
        [Display(Name = "_Enum._ProductionReturnIssueLineStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._ProductionReturnIssueLineStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._ProductionReturnIssueLineStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._ProductionReturnIssueLineStatusEnum._AllInWh")]
        AllInWh = 4
    }
    public enum ProductionRcvRptStatusEnum
    {
        [Display(Name = "_Enum._ProductionRcvRptStatusEnum._Reported")]
        Reported = 0,
        [Display(Name = "_Enum._ProductionRcvRptStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._ProductionRcvRptStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._ProductionRcvRptStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._ProductionRcvRptStatusEnum._AllInWh")]
        AllInWh = 4
    }
    public enum ProductionRcvRptLineStatusEnum
    {
        [Display(Name = "_Enum._ProductionRcvRptLineStatusEnum._Reported")]
        Reported = 0,
        [Display(Name = "_Enum._ProductionRcvRptLineStatusEnum._PartReceive")]
        PartReceive = 1,
        [Display(Name = "_Enum._ProductionRcvRptLineStatusEnum._AllReceive")]
        AllReceive = 2,
        [Display(Name = "_Enum._ProductionRcvRptLineStatusEnum._PartInWh")]
        PartInWh = 3,
        [Display(Name = "_Enum._ProductionRcvRptLineStatusEnum._AllInWh")]
        AllInWh = 4
    }
    public class RefDicNameAttribute : Attribute
    {
        public string Name { get; set; }
    }

    #region 刀具枚举
    public enum KnifeStatusEnum
    {
        [Display(Name = "_Enum._KnifeStatusEnum._InStock")]
        InStock,
        [Display(Name = "_Enum._KnifeStatusEnum._CheckOut")]
        CheckOut,
        [Display(Name = "_Enum._KnifeStatusEnum._Transferring")]
        Transferring,
        [Display(Name = "_Enum._KnifeStatusEnum._Scrapped")]
        Scrapped,
        [Display(Name = "_Enum._KnifeStatusEnum._GrindRequested")]
        GrindRequested,
        [Display(Name = "_Enum._KnifeStatusEnum._GrindingOut")]
        GrindingOut,
        [Display(Name = "不良退回")]
        DefectiveReturned,
        [Display(Name = "盘亏")]
        inventoryLoss,
        [Display(Name = "报废申请")]
        ScrapRequested,
    }
    public enum KnifeInStockStatusEnum
    {
        [Display(Name = "在库")]
        InStock,
        [Display(Name = "待维修")]
        ToGrind,
        [Display(Name = "待报废")]
        ToScrap,
        
    }
    public enum KnifeScrapTypeEnum
    {
        [Display(Name = "_Enum._KnifeScrapTypeEnum._NormalScrap")]
        NormalScrap,
        [Display(Name = "_Enum._KnifeScrapTypeEnum._DefectiveScrap")]
        DefectiveScrap
    }
    public enum KnifeOrderStatusEnum
    {
        [Display(Name = "_Enum._KnifeOrderStatusEnum._Open")]
        Open,
        [Display(Name = "_Enum._KnifeOrderStatusEnum._Approved")]
        Approved,
        [Display(Name = "审核关闭")]
        ApproveClose,
        [Display(Name = "终止关闭")]
        SuspendClose
    }
    public enum KnifeOperationTypeEnum
    {
        [Display(Name = "_Enum._KnifeOperationTypeEnum._Created")]
        Created,
        [Display(Name = "_Enum._KnifeOperationTypeEnum._CheckOut")]
        CheckOut,
        [Display(Name = "_Enum._KnifeOperationTypeEnum._CheckIn")]
        CheckIn,
        [Display(Name = "_Enum._KnifeOperationTypeEnum._Scrap")]
        Scrap,
        [Display(Name = "_Enum._KnifeOperationTypeEnum._TransferOut")]
        TransferOut,
        [Display(Name = "_Enum._KnifeOperationTypeEnum._TransferIn")]
        TransferIn,
        [Display(Name = "_Enum._KnifeOperationTypeEnum._GrindRequest")]
        GrindRequest,
        [Display(Name = "_Enum._KnifeOperationTypeEnum._GrindOut")]
        GrindOut,
        [Display(Name = "_Enum._KnifeOperationTypeEnum._GrindingIn")]
        GrindingIn,
        [Display(Name = "盘亏")]
        inventoryLoss,
        [Display(Name = "报废申请")]
        ScrapRequested,
        [Display(Name = "月初归还")]
        MonthlyCheckIn,
        [Display(Name = "月初领用")]
        MonthlyCheckOut,
        [Display(Name = "终止关闭")]
        SuspendClose,
        [Display(Name = "强制修改")]
        ForcedModify,
        [Display(Name = "盘盈")]
        inventorySurplus

    }
    public enum KnifeCheckOutTypeEnum
    {
        [Display(Name = "_Enum._KnifeCheckOutTypeEnum._NormalCheckOut")]
        NormalCheckOut,
        [Display(Name = "_Enum._KnifeCheckOutTypeEnum._CombinedCheckOut")]
        CombinedCheckOut,
        [Display(Name = "_Enum._KnifeCheckOutTypeEnum._CombinePartCheckOut")]
        CombinePartCheckOut
    }
    public enum KnifeCheckInTypeEnum
    {
        [Display(Name = "_Enum._KnifeCheckInTypeEnum._NormalCheckIn")]
        NormalCheckIn,
        [Display(Name = "_Enum._KnifeCheckInTypeEnum._WrongPickupCheckIn")]
        WrongPickupCheckIn,
        [Display(Name = "_Enum._KnifeCheckInTypeEnum._ScrapCheckIn")]
        ScrapCheckIn,
        [Display(Name = "_Enum._KnifeCheckInTypeEnum._DefectiveCheckIn")]
        DefectiveCheckIn,
        [Display(Name = "_Enum._KnifeCheckInTypeEnum._CombineCheckIn")]
        CombineCheckIn,
        [Display(Name = "_Enum._KnifeCheckInTypeEnum._CombinePartCheckIn")]
        CombinePartCheckIn
    }
    #endregion


}