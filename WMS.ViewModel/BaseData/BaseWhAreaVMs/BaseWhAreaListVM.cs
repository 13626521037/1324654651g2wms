using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseWhAreaVMs
{
    public partial class BaseWhAreaListVM : BasePagedListVM<BaseWhArea_View, BaseWhAreaSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseWhArea","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",600,650).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseWhArea","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",600,650).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseWhArea","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",600).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("BaseWhArea", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("BaseWhArea", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("BaseWhArea","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("BaseWhArea","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseWhArea","BaseWhAreaExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }


        protected override IEnumerable<IGridColumn<BaseWhArea_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseWhArea_View>>{
                this.MakeGridHeader(x => x.ID, width: 250).SetTitle("ID").SetHide(),
                this.MakeGridHeader(x => x.BaseWhArea_Code, width:150).SetTitle(@Localizer["Page.编码"].Value),
                this.MakeGridHeader(x => x.BaseWhArea_Name, width:200).SetTitle(@Localizer["Page.名称"].Value),
                this.MakeGridHeader(x => x.BaseWhArea_WareHouse, width:250).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.BaseWhArea_Org, width:200).SetTitle("组织"),
                this.MakeGridHeader(x => x.BaseWhArea_AreaType, width:100).SetTitle(@Localizer["Page.库区类型"].Value),
                this.MakeGridHeader(x => x.BaseWhArea_IsEffective, width:100).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.BaseWhArea_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }



        public override IOrderedQueryable<BaseWhArea_View> GetSearchQuery()
        {
            var query = DC.Set<BaseWhArea>()

                .CheckContain(Searcher.Code, x => x.Code)
                .CheckContain(Searcher.Name, x => x.Name)
                .CheckEqual(Searcher.WareHouseId, x => x.WareHouseId)
                .CheckEqual(Searcher.AreaType, x => x.AreaType)
                .CheckEqual(Searcher.IsEffective, x => x.IsEffective)
                .Select(x => new BaseWhArea_View
                {
                    ID = x.ID,

                    BaseWhArea_Code = x.Code,
                    BaseWhArea_Name = x.Name,
                    BaseWhArea_WareHouse = x.WareHouse.Code.CodeCombinName(x.WareHouse.Name),
                    BaseWhArea_Org = x.WareHouse.Organization.Name,
                    //BaseWhArea_Org = x.WareHouse.Organization.Code.CodeCombinName(x.WareHouse.Organization.Name),
                    BaseWhArea_AreaType = x.AreaType,
                    BaseWhArea_IsEffective = x.IsEffective,
                    BaseWhArea_Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }


    }
    public class BaseWhArea_View : BaseWhArea
    {

        public string BaseWhArea_Code { get; set; }
        public string BaseWhArea_Name { get; set; }
        public string BaseWhArea_WareHouse { get; set; }
        public string BaseWhArea_Org { get; set; }
        public WhAreaEnum? BaseWhArea_AreaType { get; set; }
        public EffectiveEnum? BaseWhArea_IsEffective { get; set; }
        public string BaseWhArea_Memo { get; set; }

    }

}