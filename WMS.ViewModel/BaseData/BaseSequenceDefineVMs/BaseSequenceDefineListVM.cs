using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSequenceDefineVMs
{
    public partial class BaseSequenceDefineListVM : BasePagedListVM<BaseSequenceDefine_View, BaseSequenceDefineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseSequenceDefine","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",1200).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseSequenceDefine","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",1200).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseSequenceDefine","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",1200).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("BaseSequenceDefine", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("BaseSequenceDefine", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("BaseSequenceDefine","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("BaseSequenceDefine","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseSequenceDefine","BaseSequenceDefineExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("BaseSequenceDefine","GetSequence","测试获取","测试获取",GridActionParameterTypesEnum.SingleId,"BaseData").SetShowInRow(true).SetShowDialog(false).SetHideOnToolBar(true),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseSequenceDefine_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseSequenceDefine_View>>{
                
                this.MakeGridHeader(x => x.BaseSequenceDefine_Code).SetTitle(@Localizer["Page.编码"].Value),
                this.MakeGridHeader(x => x.BaseSequenceDefine_Name).SetTitle(@Localizer["Page.名称"].Value),
                this.MakeGridHeader(x => x.BaseSequenceDefine_DocType).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.BaseSequenceDefine_IsEffective).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.BaseSequenceDefine_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseSequenceDefine_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.BaseSequenceDefine_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.BaseSequenceDefine_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.BaseSequenceDefine_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 300)
            };
        }

        

        public override IOrderedQueryable<BaseSequenceDefine_View> GetSearchQuery()
        {
            var query = DC.Set<BaseSequenceDefine>()
                
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.DocType, x=>x.DocType)
                .CheckEqual(Searcher.IsEffective, x=>x.IsEffective)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new BaseSequenceDefine_View
                {
				    ID = x.ID,
                    
                    BaseSequenceDefine_Code = x.Code,
                    BaseSequenceDefine_Name = x.Name,
                    BaseSequenceDefine_DocType = x.DocType,
                    BaseSequenceDefine_IsEffective = x.IsEffective,
                    BaseSequenceDefine_Memo = x.Memo,
                    BaseSequenceDefine_CreateTime = x.CreateTime,
                    BaseSequenceDefine_UpdateTime = x.UpdateTime,
                    BaseSequenceDefine_CreateBy = x.CreateBy,
                    BaseSequenceDefine_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseSequenceDefine_View: BaseSequenceDefine
    {
        
        public string BaseSequenceDefine_Code { get; set; }
        public string BaseSequenceDefine_Name { get; set; }
        public DocTypeEnum? BaseSequenceDefine_DocType { get; set; }
        public EffectiveEnum? BaseSequenceDefine_IsEffective { get; set; }
        public string BaseSequenceDefine_Memo { get; set; }
        public DateTime? BaseSequenceDefine_CreateTime { get; set; }
        public DateTime? BaseSequenceDefine_UpdateTime { get; set; }
        public string BaseSequenceDefine_CreateBy { get; set; }
        public string BaseSequenceDefine_UpdateBy { get; set; }

    }

}