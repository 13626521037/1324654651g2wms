using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseOrganizationVMs
{
    public partial class BaseOrganizationListVM : BasePagedListVM<BaseOrganization_View, BaseOrganizationSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseOrganization","SyncData","同步数据","同步数据",GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                // this.MakeAction("BaseOrganization","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseOrganization","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseOrganization","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                // this.MakeStandardAction("BaseOrganization", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                // this.MakeStandardAction("BaseOrganization", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                // this.MakeAction("BaseOrganization","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                // this.MakeAction("BaseOrganization","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseOrganization","BaseOrganizationExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }


        protected override IEnumerable<IGridColumn<BaseOrganization_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseOrganization_View>>{

                this.MakeGridHeader(x => x.ID, width: 240).SetHide(),
                this.MakeGridHeader(x => x.BaseOrganization_Code).SetTitle(@Localizer["Page.组织编码"].Value),
                this.MakeGridHeader(x => x.BaseOrganization_Name).SetTitle(@Localizer["Page.组织名称"].Value),
                this.MakeGridHeader(x => x.BaseOrganization_IsProduction).SetTitle(@Localizer["Page.是否生产组织"].Value),
                this.MakeGridHeader(x => x.BaseOrganization_IsSale).SetTitle(@Localizer["Page.是否销售组织"].Value),
                this.MakeGridHeader(x => x.BaseOrganization_IsEffective).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.BaseOrganization_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseOrganization_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value),
                this.MakeGridHeader(x => x.BaseOrganization_LastUpdateTime).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => x.BaseOrganization_IsValid).SetTitle(@Localizer["_Admin.IsValid"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }



        public override IOrderedQueryable<BaseOrganization_View> GetSearchQuery()
        {
            var query = DC.Set<BaseOrganization>()

                .CheckContain(Searcher.Code, x => x.Code)
                .CheckContain(Searcher.Name, x => x.Name)
                .CheckEqual(Searcher.IsProduction, x => x.IsProduction)
                .CheckEqual(Searcher.IsSale, x => x.IsSale)
                .CheckEqual(Searcher.IsEffective, x => x.IsEffective)
                .CheckContain(Searcher.SourceSystemId, x => x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .Select(x => new BaseOrganization_View
                {
                    ID = x.ID,

                    BaseOrganization_Code = x.Code,
                    BaseOrganization_Name = x.Name,
                    BaseOrganization_IsProduction = x.IsProduction,
                    BaseOrganization_IsSale = x.IsSale,
                    BaseOrganization_IsEffective = x.IsEffective,
                    BaseOrganization_Memo = x.Memo,
                    BaseOrganization_SourceSystemId = x.SourceSystemId,
                    BaseOrganization_LastUpdateTime = x.LastUpdateTime,
                    BaseOrganization_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }


    }
    public class BaseOrganization_View : BaseOrganization
    {

        public string BaseOrganization_Code { get; set; }
        public string BaseOrganization_Name { get; set; }
        public bool? BaseOrganization_IsProduction { get; set; }
        public bool? BaseOrganization_IsSale { get; set; }
        public EffectiveEnum? BaseOrganization_IsEffective { get; set; }
        public string BaseOrganization_Memo { get; set; }
        public string BaseOrganization_SourceSystemId { get; set; }
        public DateTime? BaseOrganization_LastUpdateTime { get; set; }
        public bool? BaseOrganization_IsValid { get; set; }

    }

}