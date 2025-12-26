using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
using WMS.Util;
using Microsoft.EntityFrameworkCore;

namespace WMS.ViewModel.KnifeManagement.KnifeVMs
{
    public partial class KnifeListVM : BasePagedListVM<Knife_View, KnifeSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("Knife","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeAction("Knife","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("Knife","KnifeExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"KnifeManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("Knife","Print", "打印", "打印", GridActionParameterTypesEnum.SingleId, "KnifeManagement", dialogHeight: 800, dialogWidth: 1000).SetShowInRow(true).SetHideOnToolBar(true),
                this.MakeAction("Knife","EditInStockStatus", "修改", "修改", GridActionParameterTypesEnum.SingleId, "KnifeManagement", dialogHeight: 800, dialogWidth: 1000).SetShowInRow(true).SetHideOnToolBar(true),
                this.MakeAction("Knife","EditKnifeStatus", "修改刀具状态", "修改刀具状态", GridActionParameterTypesEnum.SingleId, "KnifeManagement", dialogHeight: 800, dialogWidth: 1000).SetShowInRow(true).SetHideOnToolBar(true),
            };
        }


        protected override IEnumerable<IGridColumn<Knife_View>> InitGridHeader()
        {
            return new List<GridColumn<Knife_View>>{
                this.MakeGridHeader(x => x.ID).SetTitle("ID").SetWidth(180).SetHide(true),
                this.MakeGridHeader(x => x.Knife_SerialNumber).SetTitle("序列号").SetWidth(120),
                this.MakeGridHeader(x => x.Knife_Status).SetTitle("状态").SetWidth(80),
                this.MakeGridHeader(x => x.Knife_CurrentCheckOutBy).SetTitle("领用人").SetWidth(60).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.Knife_CurrentCheckOutBy_Dept).SetTitle("领用部门").SetWidth(150).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.Knife_HandledByName).SetTitle("经办人").SetWidth(80).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.Knife_LastOperationDate).SetTitle("最近操作日期").SetWidth(140),
                this.MakeGridHeader(x => x.Knife_WareHouse).SetTitle("存储地点").SetWidth(120),
                this.MakeGridHeader(x => x.Knife_WhLocation).SetTitle("库位").SetWidth(80),
                this.MakeGridHeader(x => x.Knife_GrindCount_Int).SetTitle("修磨次数").SetWidth(60),
                this.MakeGridHeader(x => x.Knife_InitialLife).SetTitle("初始寿命").SetWidth(60),
                this.MakeGridHeader(x => x.Knife_CurrentLife).SetTitle("当前寿命").SetWidth(60),
                this.MakeGridHeader(x => x.Knife_TotalUsedDays).SetTitle("累计使用天数").SetWidth(100),
                this.MakeGridHeader(x => x.Knife_RemainingUsedDays).SetTitle("剩余使用天数").SetWidth(100),
                this.MakeGridHeader(x => x.Knife_ItemMaster).SetTitle("料品").SetWidth(100).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.Knife_CreatedDate).SetTitle("建档时间").SetWidth(120),
                this.MakeGridHeader(x => x.Knife_InStockStatus).SetTitle("在库状态").SetWidth(80),
                this.MakeGridHeader(x => x.Knife_ActualItemCode).SetTitle("实际料号").SetWidth(80),
                this.MakeGridHeader(x => x.Knife_GrindKnifeNO).SetTitle("修磨刀具号").SetWidth(80),
                this.MakeGridHeader(x => x.BarCode, width: 235),


                this.MakeGridHeaderAction(width:380)
            };
        }



        public override IOrderedQueryable<Knife_View> GetSearchQuery()
        {
            var query = DC.Set<Knife>()
                .Include(x=>x.WhLocation).ThenInclude(x=>x.WhArea).ThenInclude(x=>x.WareHouse)
                .Include(x=>x.CurrentCheckOutBy).ThenInclude(x=>x.Department)
                .Include(x=>x.ItemMaster)
                .CheckBetween(Searcher.CreatedDate?.GetStartTime(), Searcher.CreatedDate?.GetEndTime(), x => x.CreatedDate, includeMax: false)
                .CheckContain(Searcher.SerialNumber, x => x.SerialNumber)
                .CheckEqual(Searcher.Status, x => x.Status)
                .CheckEqual(Searcher.CurrentCheckOutById, x => x.CurrentCheckOutById)
                .CheckEqual(Searcher.HandledById, x => x.HandledById)
                .CheckBetween(Searcher.LastOperationDate?.GetStartTime(), Searcher.LastOperationDate?.GetEndTime(), x => x.LastOperationDate, includeMax: false)
                .CheckEqual(Searcher.WareHouseId, x => x.WhLocation.WhArea.WareHouseId)
                .CheckEqual(Searcher.WhLocationId, x => x.WhLocationId)
                .CheckEqual(Searcher.GrindCount, x => x.GrindCount)
                .CheckEqual(Searcher.InitialLife, x => x.InitialLife)
                .CheckEqual(Searcher.CurrentLife, x => x.CurrentLife)
                .CheckEqual(Searcher.TotalUsedDays, x => x.TotalUsedDays)
                .CheckEqual(Searcher.RemainingUsedDays, x => x.RemainingUsedDays)
                .CheckEqual(Searcher.ItemMasterCode, x => x.ItemMaster.Code)
                .CheckContain(Searcher.MiscShipLineID, x => x.MiscShipLineID)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x => x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x => x.UpdateBy)
                .CheckEqual(Searcher.InStockStatus, x => x.InStockStatus)
                .CheckEqual(Searcher.ActualItemCode, x => x.ActualItemCode)
                .CheckContain(Searcher.GrindKnifeNO, x => x.GrindKnifeNO)
                .Select(x => new Knife_View
                {
                    ID = x.ID,

                    Knife_CreatedDate = x.CreatedDate,
                    Knife_SerialNumber = x.SerialNumber,
                    Knife_Status = x.Status,
                    Knife_CurrentCheckOutBy = x.CurrentCheckOutBy.Name,
                    Knife_CurrentCheckOutBy_Dept = x.CurrentCheckOutBy.Department.Name ,
                    Knife_HandledByName = x.HandledByName,
                    Knife_LastOperationDate = x.LastOperationDate,
                    Knife_WareHouse =x.WhLocation.WhArea.WareHouse.Name,
                    Knife_WhLocation = x.WhLocation.Code,
                    Knife_GrindCount = x.GrindCount,
                    Knife_InitialLife = x.InitialLife,
                    Knife_CurrentLife = x.CurrentLife,
                    Knife_TotalUsedDays = x.TotalUsedDays,
                    Knife_RemainingUsedDays = x.RemainingUsedDays,
                    Knife_ItemMaster = x.ItemMaster.Code,
                    Knife_MiscShipLineID = x.MiscShipLineID,
                    Knife_CreateTime = x.CreateTime,
                    Knife_UpdateTime = x.UpdateTime,
                    Knife_CreateBy = x.CreateBy,
                    Knife_UpdateBy = x.UpdateBy,
                    Knife_InStockStatus = x.InStockStatus,
                    Knife_ActualItemCode= x.ActualItemCode,
                    Knife_GrindKnifeNO = x.GrindKnifeNO,
                    BarCode = Common.AddBarCodeDialog($"{x.BarCode}"),

                })
                .OrderByDescending(x => x.Knife_LastOperationDate);
            return query;
        }


    }
    public class Knife_View : Knife
    {

        public DateTime? Knife_CreatedDate { get; set; }
        public string Knife_SerialNumber { get; set; }
        public KnifeStatusEnum? Knife_Status { get; set; }
        public string Knife_CurrentCheckOutBy { get; set; }
        public string Knife_CurrentCheckOutBy_Dept { get; set; }
        public string Knife_HandledByName { get; set; }
        public DateTime? Knife_LastOperationDate { get; set; }
        public string Knife_WareHouse { get; set; }
        public string Knife_WhLocation { get; set; }
        public decimal? Knife_GrindCount { get; set; }
        public int Knife_GrindCount_Int => (int)Knife_GrindCount;
        public decimal? Knife_InitialLife { get; set; }
        public decimal? Knife_CurrentLife { get; set; }
        public decimal? Knife_TotalUsedDays { get; set; }
        public decimal? Knife_RemainingUsedDays { get; set; }
        public string Knife_ItemMaster { get; set; }
        public string Knife_MiscShipLineID { get; set; }
        public string Knife_ActualItemCode { get; set; }
        public string Knife_GrindKnifeNO { get; set; }
        public DateTime? Knife_CreateTime { get; set; }
        public DateTime? Knife_UpdateTime { get; set; }
        public string Knife_CreateBy { get; set; }
        public string Knife_UpdateBy { get; set; }

        public KnifeInStockStatusEnum Knife_InStockStatus { get; set; }
    }

}