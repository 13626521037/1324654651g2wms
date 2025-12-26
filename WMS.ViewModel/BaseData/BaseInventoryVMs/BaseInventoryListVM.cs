using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Util;

namespace WMS.ViewModel.BaseData.BaseInventoryVMs
{
    public partial class BaseInventoryListVM : BasePagedListVM<BaseInventory_View, BaseInventorySearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("BaseInventory","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("BaseInventory","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseInventory","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseInventory", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseInventory", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseInventory","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("BaseInventory","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseInventory","BaseInventoryExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                //this.MakeAction("BaseInventory", "QrCode", "二维码", "二维码", GridActionParameterTypesEnum.SingleId, "BaseData", 300).SetHideOnToolBar().SetShowInRow(),   // 统一显示在详情里了
                this.MakeAction("BaseInventory", "Split", "库存拆分", "库存拆分", GridActionParameterTypesEnum.SingleId, "BaseData", 300).SetHideOnToolBar().SetShowInRow().SetBindVisiableColName("CanSplit"),
                this.MakeAction("BaseInventory", "Print", "打印", "打印", GridActionParameterTypesEnum.SingleId, "BaseData", dialogHeight: 800, dialogWidth: 1000).SetShowInRow(true).SetHideOnToolBar(true),
                this.MakeAction("BaseInventory", "BatchPrint", "批量打印", "批量打印", GridActionParameterTypesEnum.MultiIds, "BaseData", dialogHeight: 800, dialogWidth: 1400)
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseInventory_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseInventory_View>>{
                this.MakeGridHeader(x => x.ID, width: 245).SetTitle("ID").SetHide(),
                this.MakeGridHeader(x => x.BaseInventory_ItemMaster, width: 90).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.BaseInventory_Wh, width: 220).SetTitle("存储地点"),
                this.MakeGridHeader(x => x.BaseInventory_WhArea, width: 220).SetTitle("库区"),
                this.MakeGridHeader(x => x.BaseInventory_WhLocation, width: 150).SetTitle(@Localizer["Page.库位"].Value),
                this.MakeGridHeader(x => x.BaseInventory_BatchNumber, width: 120).SetTitle(@Localizer["Page.批号"].Value),
                this.MakeGridHeader(x => x.BaseInventory_SerialNumber, width: 120).SetTitle(@Localizer["Page.序列号"].Value),
                this.MakeGridHeader(x => x.BaseInventory_Seiban, width: 120).SetTitle(@Localizer["Page.番号"].Value),
                this.MakeGridHeader(x => x.BaseInventory_SeibanRandom, width: 120).SetTitle("番号随机码"),
                this.MakeGridHeader(x => x.BaseInventory_Qty, width: 80).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.BarCode, width: 235),
                this.MakeGridHeader(x => x.BaseInventory_IsAbandoned, width: 90).SetTitle(@Localizer["Page.作废标记"].Value),
                this.MakeGridHeader(x => x.BaseInventory_ItemSourceType, width: 95).SetTitle(@Localizer["Page.料品来源类型"].Value),
                this.MakeGridHeader(x => x.BaseInventory_FrozenStatus, width: 90).SetTitle(@Localizer["Page.冻结状态"].Value),
                this.MakeGridHeader(x => "CanSplit").SetHide().SetFormat((a, b) =>
                {
                    if (a.BaseInventory_WhLocation_Type == WhLocationEnum.Normal)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 200).SetAlign(GridColumnAlignEnum.Left)
            };
        }

        

        public override IOrderedQueryable<BaseInventory_View> GetSearchQuery()
        {
            var query = DC.Set<BaseInventory>()
                
                //.CheckEqual(Searcher.ItemMasterId, x=>x.ItemMasterId)
                //.CheckEqual(Searcher.WhLocationId, x=>x.WhLocationId)
                .CheckContain(Searcher.BatchNumber, x=>x.BatchNumber)
                .CheckContain(Searcher.SerialNumber, x=>x.SerialNumber)
                .CheckContain(Searcher.ItemCode, x=>x.ItemMaster.Code)
                .CheckContain(Searcher.Wh, x=>x.WhLocation.WhArea.WareHouse.Code)
                .CheckContain(Searcher.Area, x=>x.WhLocation.WhArea.Code)
                .CheckContain(Searcher.Location, x=>x.WhLocation.Code)
                .CheckContain(Searcher.Seiban, x=>x.Seiban)
                .CheckContain(Searcher.SeibanRandom, x=>x.SeibanRandom)
                .CheckEqual(Searcher.Qty, x=>x.Qty)
                .CheckEqual(Searcher.IsAbandoned, x=>x.IsAbandoned)
                .Where(x => Searcher.HideZero == true ? x.Qty > 0 : true)
                .CheckEqual(Searcher.ItemSourceType, x=>x.ItemSourceType)
                .CheckEqual(Searcher.FrozenStatus, x=>x.FrozenStatus)
                .Select(x => new BaseInventory_View
                {
				    ID = x.ID,
                    
                    BaseInventory_ItemMaster = x.ItemMaster.Code,
                    BaseInventory_Wh = x.WhLocation.WhArea.WareHouse.Code.CodeCombinName(x.WhLocation.WhArea.WareHouse.Name),
                    BaseInventory_WhArea = x.WhLocation.WhArea.Code.CodeCombinName(x.WhLocation.WhArea.Name),
                    BaseInventory_WhLocation = Common.AddLocationDialog(x.WhLocation), //x.WhLocation.Code.CodeCombinName(x.WhLocation.Name),
                    BaseInventory_WhLocation_Type = x.WhLocation.AreaType,
                    BaseInventory_BatchNumber = x.BatchNumber,
                    BaseInventory_SerialNumber = x.SerialNumber,
                    BaseInventory_Seiban = x.Seiban,
                    BaseInventory_SeibanRandom = x.SeibanRandom,
                    BaseInventory_Qty = x.Qty.TrimZero(),
                    BaseInventory_IsAbandoned = x.IsAbandoned,
                    BaseInventory_ItemSourceType = x.ItemSourceType,
                    BaseInventory_FrozenStatus = x.FrozenStatus,
                    BarCode = Common.AddBarCodeDialog($"{(int)x.ItemSourceType}@{x.ItemMaster.Code}@{x.Qty.TrimZero()}@{x.SerialNumber}"),
                    CreateTime = x.CreateTime,
                })
                .OrderByDescending(x => x.CreateTime);
            return query;
        }

        
    }
    public class BaseInventory_View: BaseInventory
    {
        
        public string BaseInventory_ItemMaster { get; set; }
        public string BaseInventory_Wh { get; set; }
        public string BaseInventory_WhArea { get; set; }
        public string BaseInventory_WhLocation { get; set; }
        public WhLocationEnum? BaseInventory_WhLocation_Type { get; set; }
        public string BaseInventory_BatchNumber { get; set; }
        public string BaseInventory_SerialNumber { get; set; }
        public string BaseInventory_Seiban { get; set; }
        public string BaseInventory_SeibanRandom { get; set; }
        public decimal? BaseInventory_Qty { get; set; }
        public bool? BaseInventory_IsAbandoned { get; set; }
        public ItemSourceTypeEnum? BaseInventory_ItemSourceType { get; set; }
        public FrozenStatusEnum? BaseInventory_FrozenStatus { get; set; }

        [Display(Name = "条码")]
        public string BarCode { get; set; }
    }

}