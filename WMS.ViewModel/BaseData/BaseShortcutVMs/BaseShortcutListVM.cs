using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseShortcutVMs
{
    public partial class BaseShortcutListVM : BasePagedListVM<BaseShortcut_View, BaseShortcutSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseShortcut","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseShortcut","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseShortcut","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("BaseShortcut", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("BaseShortcut", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("BaseShortcut","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("BaseShortcut","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseShortcut","BaseShortcutExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseShortcut_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseShortcut_View>>{
                
                this.MakeGridHeader(x => x.BaseShortcut_Menu).SetTitle(@Localizer["Sys.Menu"].Value),
                this.MakeGridHeader(x => x.BaseShortcut_User).SetTitle(@Localizer["_Admin.User"].Value),
                this.MakeGridHeader(x => x.BaseShortcut_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.BaseShortcut_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.BaseShortcut_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.BaseShortcut_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<BaseShortcut_View> GetSearchQuery()
        {
            var query = DC.Set<BaseShortcut>()
                
                .CheckEqual(Searcher.MenuId, x=>x.MenuId)
                .CheckEqual(Searcher.UserId, x=>x.UserId)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new BaseShortcut_View
                {
				    ID = x.ID,
                    
                    BaseShortcut_Menu = x.Menu.PageName,
                    BaseShortcut_User = DC.Set<FrameworkUser>().Where(z0 => z0.ITCode == x.UserId).Select(y => y.Name).FirstOrDefault(),
                    BaseShortcut_CreateTime = x.CreateTime,
                    BaseShortcut_UpdateTime = x.UpdateTime,
                    BaseShortcut_CreateBy = x.CreateBy,
                    BaseShortcut_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseShortcut_View: BaseShortcut
    {
        
        public string BaseShortcut_Menu { get; set; }
        public string BaseShortcut_User { get; set; }
        public DateTime? BaseShortcut_CreateTime { get; set; }
        public DateTime? BaseShortcut_UpdateTime { get; set; }
        public string BaseShortcut_CreateBy { get; set; }
        public string BaseShortcut_UpdateBy { get; set; }

    }

}