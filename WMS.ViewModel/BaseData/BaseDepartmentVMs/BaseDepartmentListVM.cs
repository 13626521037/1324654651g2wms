using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseDepartmentVMs
{
    public partial class BaseDepartmentListVM : BasePagedListVM<BaseDepartment_View, BaseDepartmentSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseDepartment","SyncData","同步数据","同步数据",GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                // this.MakeAction("BaseDepartment","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseDepartment","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseDepartment","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                // this.MakeStandardAction("BaseDepartment", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                // this.MakeStandardAction("BaseDepartment", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                // this.MakeAction("BaseDepartment","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                // this.MakeAction("BaseDepartment","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseDepartment","BaseDepartmentExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }


        protected override IEnumerable<IGridColumn<BaseDepartment_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseDepartment_View>>{

                this.MakeGridHeader(x => x.BaseDepartment_Code).SetTitle(@Localizer["Page.部门编码"].Value),
                this.MakeGridHeader(x => x.BaseDepartment_Name).SetTitle(@Localizer["Page.部门名称"].Value),
                this.MakeGridHeader(x => x.BaseDepartment_Organization).SetTitle(@Localizer["Page.组织"].Value).SetWidth(280),
                this.MakeGridHeader(x => x.BaseDepartment_IsMFG).SetTitle("生产相关"),
                this.MakeGridHeader(x => x.BaseDepartment_IsEffective).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.BaseDepartment_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseDepartment_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value),
                this.MakeGridHeader(x => x.BaseDepartment_LastUpdateTime).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => x.BaseDepartment_IsValid).SetTitle(@Localizer["_Admin.IsValid"].Value).SetWidth(90),

                this.MakeGridHeaderAction(width: 200)
            };
        }



        public override IOrderedQueryable<BaseDepartment_View> GetSearchQuery()
        {
            var query = DC.Set<BaseDepartment>()

                .CheckContain(Searcher.Code, x => x.Code)
                .CheckContain(Searcher.Name, x => x.Name)
                .CheckEqual(Searcher.IsMFG, x => x.IsMFG)
                .Where(x => x.Organization.Code.Contains(Searcher.Organization) || x.Organization.Name.Contains(Searcher.Organization))
                .CheckEqual(Searcher.IsEffective, x => x.IsEffective)
                .CheckContain(Searcher.SourceSystemId, x => x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .Select(x => new BaseDepartment_View
                {
                    ID = x.ID,

                    BaseDepartment_Code = x.Code,
                    BaseDepartment_Name = x.Name,
                    BaseDepartment_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name),
                    BaseDepartment_IsMFG = x.IsMFG,
                    BaseDepartment_IsEffective = x.IsEffective,
                    BaseDepartment_Memo = x.Memo,
                    BaseDepartment_SourceSystemId = x.SourceSystemId,
                    BaseDepartment_LastUpdateTime = x.LastUpdateTime,
                    BaseDepartment_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }


    }
    public class BaseDepartment_View : BaseDepartment
    {

        public string BaseDepartment_Code { get; set; }
        public string BaseDepartment_Name { get; set; }
        public bool? BaseDepartment_IsMFG { get; set; }
        public string BaseDepartment_Organization { get; set; }
        public EffectiveEnum? BaseDepartment_IsEffective { get; set; }
        public string BaseDepartment_Memo { get; set; }
        public string BaseDepartment_SourceSystemId { get; set; }
        public DateTime? BaseDepartment_LastUpdateTime { get; set; }
        public bool? BaseDepartment_IsValid { get; set; }

    }

}