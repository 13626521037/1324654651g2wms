using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs
{
    public partial class BaseSeibanCustomerRelationListVM : BasePagedListVM<BaseSeibanCustomerRelation_View, BaseSeibanCustomerRelationSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseSeibanCustomerRelation","SyncData","同步数据","同步数据",GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                //this.MakeAction("BaseSeibanCustomerRelation","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("BaseSeibanCustomerRelation","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseSeibanCustomerRelation","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseSeibanCustomerRelation", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseSeibanCustomerRelation", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseSeibanCustomerRelation","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("BaseSeibanCustomerRelation","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseSeibanCustomerRelation","BaseSeibanCustomerRelationExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseSeibanCustomerRelation_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseSeibanCustomerRelation_View>>{
                
                this.MakeGridHeader(x => x.BaseSeibanCustomerRelation_Code, width: 160).SetTitle(@Localizer["Page.番号"].Value),
                this.MakeGridHeader(x => x.BaseSeibanCustomerRelation_Name, width: 160).SetTitle("客户编码"),
                this.MakeGridHeader(x => x.BaseSeibanCustomerRelation_Customer).SetTitle(@Localizer["Page.客户"].Value),
                this.MakeGridHeader(x => x.BaseSeibanCustomerRelation_RandomCode, width: 110).SetTitle(@Localizer["Page.随机码"].Value),
                //this.MakeGridHeader(x => x.BaseSeibanCustomerRelation_Memo, width: 110).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseSeibanCustomerRelation_LastUpdateTime, width: 160).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => x.BaseSeibanCustomerRelation_IsValid, width: 80).SetTitle(@Localizer["_Admin.IsValid"].Value),
                //this.MakeGridHeader(x => x.BaseSeibanCustomerRelation_CreateTime, width: 160).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.BaseSeibanCustomerRelation_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.BaseSeibanCustomerRelation_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value).SetHide(true),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<BaseSeibanCustomerRelation_View> GetSearchQuery()
        {
            var query = DC.Set<BaseSeibanCustomerRelation>()
                
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckContain(Searcher.RandomCode, x=>x.RandomCode)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .Select(x => new BaseSeibanCustomerRelation_View
                {
				    ID = x.ID,
                    
                    BaseSeibanCustomerRelation_Customer = x.Customer.Name,
                    BaseSeibanCustomerRelation_RandomCode = x.RandomCode,
                    BaseSeibanCustomerRelation_Memo = x.Memo,
                    BaseSeibanCustomerRelation_SourceSystemId = x.SourceSystemId,
                    BaseSeibanCustomerRelation_LastUpdateTime = x.LastUpdateTime,
                    BaseSeibanCustomerRelation_Code = x.Code,
                    BaseSeibanCustomerRelation_Name = x.Name,
                    BaseSeibanCustomerRelation_CreateTime = x.CreateTime,
                    BaseSeibanCustomerRelation_CreateBy = x.CreateBy,
                    BaseSeibanCustomerRelation_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseSeibanCustomerRelation_View: BaseSeibanCustomerRelation
    {
        
        public string BaseSeibanCustomerRelation_Customer { get; set; }
        public string BaseSeibanCustomerRelation_RandomCode { get; set; }
        public string BaseSeibanCustomerRelation_Memo { get; set; }
        public string BaseSeibanCustomerRelation_SourceSystemId { get; set; }
        public DateTime? BaseSeibanCustomerRelation_LastUpdateTime { get; set; }
        public string BaseSeibanCustomerRelation_Code { get; set; }
        public string BaseSeibanCustomerRelation_Name { get; set; }
        public DateTime? BaseSeibanCustomerRelation_CreateTime { get; set; }
        public string BaseSeibanCustomerRelation_CreateBy { get; set; }
        public bool? BaseSeibanCustomerRelation_IsValid { get; set; }

    }

}