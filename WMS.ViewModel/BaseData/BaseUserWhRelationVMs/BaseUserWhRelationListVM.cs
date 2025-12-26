using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseUserWhRelationVMs
{
    public partial class BaseUserWhRelationListVM : BasePagedListVM<BaseUserWhRelation_View, BaseUserWhRelationSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseUserWhRelation","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",600,800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseUserWhRelation","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",600,800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseUserWhRelation","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",600).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("BaseUserWhRelation", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("BaseUserWhRelation", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("BaseUserWhRelation","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("BaseUserWhRelation","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseUserWhRelation","BaseUserWhRelationExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseUserWhRelation_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseUserWhRelation_View>>{
                
                this.MakeGridHeader(x => x.BaseUserWhRelation_User, width: 120).SetTitle(@Localizer["_Admin.User"].Value),
                this.MakeGridHeader(x => x.BaseUserWhRelation_Org).SetTitle("组织"),
                this.MakeGridHeader(x => x.BaseUserWhRelation_Wh).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.BaseUserWhRelation_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseUserWhRelation_CreateTime, width: 145).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.BaseUserWhRelation_CreateBy, width: 120).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.BaseUserWhRelation_UpdateTime, width: 145).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.BaseUserWhRelation_UpdateBy, width: 120).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<BaseUserWhRelation_View> GetSearchQuery()
        {
            var query = DC.Set<BaseUserWhRelation>()
                
                .CheckEqual(Searcher.UserId, x=>x.UserId)
                .CheckEqual(Searcher.WhId, x=>x.WhId)
                .Select(x => new BaseUserWhRelation_View
                {
				    ID = x.ID,
                    BaseUserWhRelation_User = DC.Set<FrameworkUser>().Where(z0 => z0.ITCode == x.UserId).Select(y => y.Name).FirstOrDefault(),
                    BaseUserWhRelation_Wh = x.Wh.Code.CodeCombinName(x.Wh.Name),
                    BaseUserWhRelation_Org = x.Wh.Organization.Code.CodeCombinName(x.Wh.Organization.Name),
                    BaseUserWhRelation_Memo = x.Memo,
                    BaseUserWhRelation_CreateTime = x.CreateTime,
                    BaseUserWhRelation_CreateBy = x.CreateBy,
                    BaseUserWhRelation_UpdateTime = x.UpdateTime,
                    BaseUserWhRelation_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseUserWhRelation_View: BaseUserWhRelation
    {
        
        public string BaseUserWhRelation_User { get; set; }
        public string BaseUserWhRelation_Wh { get; set; }
        public string BaseUserWhRelation_Org { get; set; }
        public string BaseUserWhRelation_Memo { get; set; }
        public DateTime? BaseUserWhRelation_CreateTime { get; set; }
        public DateTime? BaseUserWhRelation_UpdateTime { get; set; }
        public string BaseUserWhRelation_CreateBy { get; set; }
        public string BaseUserWhRelation_UpdateBy { get; set; }

    }

}