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

namespace WMS.ViewModel.BaseData.BaseWhLocationVMs
{
    public partial class BaseWhLocationListVM : BasePagedListVM<BaseWhLocation_View, BaseWhLocationSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseWhLocation","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",600,650).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseWhLocation","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",600,650).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseWhLocation","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("BaseWhLocation", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("BaseWhLocation", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("BaseWhLocation","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("BaseWhLocation","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseWhLocation","BaseWhLocationExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseWhLocation_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseWhLocation_View>>{
                this.MakeGridHeader(x => x.ID, width: 245).SetTitle("ID").SetHide(),
                this.MakeGridHeader(x => x.BaseWhLocation_Code, width: 150).SetTitle(@Localizer["Page.编码"].Value),
                this.MakeGridHeader(x => x.BaseWhLocation_Name, width: 220).SetTitle(@Localizer["Page.名称"].Value),
                this.MakeGridHeader(x => x.BaseWhLocation_WhArea, width: 220).SetTitle(@Localizer["Page.库区"].Value),
                this.MakeGridHeader(x => x.BaseWhLocation_Warehouse, width: 250).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.BaseWhLocation_Org, width: 200).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.BaseWhLocation_AreaType, width:80).SetTitle(@Localizer["Page.库位类型"].Value),
                this.MakeGridHeader(x => x.BaseWhLocation_Locked, width:80).SetTitle(@Localizer["Page.盘点锁定"].Value),
                this.MakeGridHeader(x => x.BaseWhLocation_IsEffective, width:80).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.BaseWhLocation_Memo, width: 200).SetTitle(@Localizer["Page.描述"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<BaseWhLocation_View> GetSearchQuery()
        {
            var query = DC.Set<BaseWhLocation>()
                
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.WhAreaId, x=>x.WhAreaId)
                .CheckEqual(Searcher.WareHouseId, x=>x.WhArea.WareHouseId)
                .CheckEqual(Searcher.AreaType, x=>x.AreaType)
                .CheckEqual(Searcher.Locked, x=>x.Locked)
                .CheckEqual(Searcher.IsEffective, x=>x.IsEffective)
                .Select(x => new BaseWhLocation_View
                {
				    ID = x.ID,
                    
                    BaseWhLocation_Code = Common.AddBarCodeDialog(x.Code),
                    BaseWhLocation_Name = x.Name,
                    BaseWhLocation_WhArea = x.WhArea.Code.CodeCombinName(x.WhArea.Name),
                    BaseWhLocation_Org = x.WhArea.WareHouse.Organization.Name,
                    BaseWhLocation_AreaType = x.AreaType,
                    BaseWhLocation_Locked = x.Locked,
                    BaseWhLocation_IsEffective = x.IsEffective,
                    BaseWhLocation_Memo = x.Memo,
                    BaseWhLocation_Warehouse = x.WhArea.WareHouse.Code.CodeCombinName(x.WhArea.WareHouse.Name),
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseWhLocation_View: BaseWhLocation
    {
        
        public string BaseWhLocation_Code { get; set; }
        public string BaseWhLocation_Name { get; set; }
        public string BaseWhLocation_WhArea { get; set; }
        public string BaseWhLocation_Warehouse { get; set; }
        public string BaseWhLocation_Org { get; set; }
        public WhLocationEnum? BaseWhLocation_AreaType { get; set; }
        public bool? BaseWhLocation_Locked { get; set; }
        public EffectiveEnum? BaseWhLocation_IsEffective { get; set; }
        public string BaseWhLocation_Memo { get; set; }

    }

}