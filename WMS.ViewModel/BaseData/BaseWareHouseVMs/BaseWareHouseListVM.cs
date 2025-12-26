using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseWareHouseVMs
{
    public partial class BaseWareHouseListVM : BasePagedListVM<BaseWareHouse_View, BaseWareHouseSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseWareHouse","SyncData","同步数据","同步数据",GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                //this.MakeAction("BaseWareHouse","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseWareHouse","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseWareHouse","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseWareHouse", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseWareHouse", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseWareHouse","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("BaseWareHouse","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseWareHouse","BaseWareHouseExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseWareHouse_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseWareHouse_View>>{
                this.MakeGridHeader(x => x.ID, width: 250).SetTitle("ID").SetHide(),
                this.MakeGridHeader(x => x.BaseWareHouse_Code, width: 85).SetTitle(@Localizer["Page.编码"].Value),
                this.MakeGridHeader(x => x.BaseWareHouse_Name).SetTitle(@Localizer["Page.名称"].Value),
                this.MakeGridHeader(x => x.BaseWareHouse_Organization, width : 210).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.BaseWareHouse_IsProduct,width: 116).SetTitle(@Localizer["Page.是否成品仓库"].Value),
                this.MakeGridHeader(x => x.BaseWareHouse_ShipType,width: 116).SetTitle(@Localizer["Page.发货属性"].Value),
                this.MakeGridHeader(x => x.BaseWareHouse_IsStacking,width: 90).SetTitle(@Localizer["Page.是否上架"].Value),
                this.MakeGridHeader(x => x.BaseWareHouse_IsEffective,width: 90).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.BaseWareHouse_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseWareHouse_SourceSystemId,width: 153).SetTitle(@Localizer["Page.来源系统主键"].Value),
                this.MakeGridHeader(x => x.BaseWareHouse_LastUpdateTime,width: 158).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => x.BaseWareHouse_IsValid,width: 90).SetTitle(@Localizer["_Admin.IsValid"].Value),
                this.MakeGridHeaderAction(width: 118)
            };
        }

        

        public override IOrderedQueryable<BaseWareHouse_View> GetSearchQuery()
        {
            var query = DC.Set<BaseWareHouse>()
                
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckEqual(Searcher.IsProduct, x=>x.IsProduct)
                .CheckEqual(Searcher.ShipType, x=>x.ShipType)
                .CheckEqual(Searcher.IsStacking, x=>x.IsStacking)
                .CheckEqual(Searcher.IsEffective, x=>x.IsEffective)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .Select(x => new BaseWareHouse_View
                {
				    ID = x.ID,
                    
                    BaseWareHouse_Code = x.Code,
                    BaseWareHouse_Name = x.Name,
                    BaseWareHouse_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name).SetFontSmallSize(),
                    BaseWareHouse_IsProduct = x.IsProduct,
                    BaseWareHouse_ShipType = x.ShipType,
                    BaseWareHouse_IsStacking = x.IsStacking,
                    BaseWareHouse_IsEffective = x.IsEffective,
                    BaseWareHouse_Memo = x.Memo,
                    BaseWareHouse_SourceSystemId = x.SourceSystemId,
                    BaseWareHouse_LastUpdateTime = x.LastUpdateTime,
                    BaseWareHouse_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseWareHouse_View: BaseWareHouse
    {
        
        public string BaseWareHouse_Code { get; set; }
        public string BaseWareHouse_Name { get; set; }
        public string BaseWareHouse_Organization { get; set; }
        public bool? BaseWareHouse_IsProduct { get; set; }
        public WhShipTypeEnum? BaseWareHouse_ShipType { get; set; }
        public bool? BaseWareHouse_IsStacking { get; set; }
        public EffectiveEnum? BaseWareHouse_IsEffective { get; set; }
        public string BaseWareHouse_Memo { get; set; }
        public string BaseWareHouse_SourceSystemId { get; set; }
        public DateTime? BaseWareHouse_LastUpdateTime { get; set; }
        public bool? BaseWareHouse_IsValid { get; set; }

    }

}