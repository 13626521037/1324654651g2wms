using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseOperatorVMs
{
    public partial class BaseOperatorListVM : BasePagedListVM<BaseOperator_View, BaseOperatorSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseOperator","SyncData",@Localizer["同步数据"].Value,@Localizer["同步数据"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                //this.MakeAction("BaseOperator","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseOperator","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                //this.MakeAction("BaseOperator","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseOperator", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseOperator", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseOperator","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("BaseOperator","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseOperator","BaseOperatorExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseOperator_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseOperator_View>>{
                
                this.MakeGridHeader(x => x.BaseOperator_Code, width: 85).SetTitle("编码"),
                this.MakeGridHeader(x => x.BaseOperator_Name, width: 200).SetTitle("名称"),
                this.MakeGridHeader(x => x.BaseOperator_JobID, width: 85).SetTitle(@Localizer["Page.工号"].Value),
                this.MakeGridHeader(x => x.BaseOperator_Department, width: 220).SetTitle(@Localizer["Page.部门"].Value),
                this.MakeGridHeader(x => x.BaseOperator_OAAccount, width: 85).SetTitle(@Localizer["Page.OA账号"].Value),
                this.MakeGridHeader(x => x.BaseOperator_IDCard, width: 100).SetTitle(@Localizer["Page.磁卡信息"].Value),
                this.MakeGridHeader(x => x.BaseOperator_TempAuthCode, width: 100).SetTitle(@Localizer["Page.临时授权码"].Value),
                this.MakeGridHeader(x => x.BaseOperator_TACExpired, width: 135).SetTitle(@Localizer["Page.授权码失效时间"].Value),
                this.MakeGridHeader(x => x.BaseOperator_IsEffective, width: 75).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.BaseOperator_Phone, width: 125).SetTitle(@Localizer["Page.联系方式"].Value),
                this.MakeGridHeader(x => x.BaseOperator_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseOperator_SourceSystemId, width: 125).SetTitle(@Localizer["Page.来源系统主键"].Value),
                this.MakeGridHeader(x => x.BaseOperator_LastUpdateTime, width: 135).SetTitle(@Localizer["Page.最后修改时间"].Value),
                //this.MakeGridHeader(x => x.BaseOperator_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.BaseOperator_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.BaseOperator_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.BaseOperator_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => x.BaseOperator_IsValid, width: 75).SetTitle(@Localizer["_Admin.IsValid"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<BaseOperator_View> GetSearchQuery()
        {
            var query = DC.Set<BaseOperator>()
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckContain(Searcher.JobID, x=>x.JobID)
                .CheckContain(Searcher.OAAccount, x=>x.OAAccount)
                .CheckContain(Searcher.IDCard, x=>x.IDCard)
                .CheckContain(Searcher.TempAuthCode, x=>x.TempAuthCode)
                .CheckBetween(Searcher.TACExpired?.GetStartTime(), Searcher.TACExpired?.GetEndTime(), x => x.TACExpired, includeMax: false)
                //.CheckEqual(Searcher.DepartmentId, x=>x.DepartmentId)
                .Where(x => string.IsNullOrEmpty(Searcher.Department) || x.Department.Code.Contains(Searcher.Department) || x.Department.Name.Contains(Searcher.Department))
                .CheckEqual(Searcher.IsEffective, x=>x.IsEffective)
                .CheckContain(Searcher.Phone, x=>x.Phone)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new BaseOperator_View
                {
				    ID = x.ID,
                    BaseOperator_Code = x.Code,
                    BaseOperator_Name = x.Name,
                    BaseOperator_JobID = x.JobID,
                    BaseOperator_OAAccount = x.OAAccount,
                    BaseOperator_IDCard = x.IDCard,
                    BaseOperator_TempAuthCode = x.TempAuthCode,
                    BaseOperator_TACExpired = x.TACExpired,
                    BaseOperator_Department = x.Department.Code.CodeCombinName(x.Department.Name),
                    BaseOperator_IsEffective = x.IsEffective,
                    BaseOperator_Phone = x.Phone,
                    BaseOperator_Memo = x.Memo,
                    BaseOperator_SourceSystemId = x.SourceSystemId,
                    BaseOperator_LastUpdateTime = x.LastUpdateTime,
                    BaseOperator_CreateTime = x.CreateTime,
                    BaseOperator_UpdateTime = x.UpdateTime,
                    BaseOperator_CreateBy = x.CreateBy,
                    BaseOperator_UpdateBy = x.UpdateBy,
                    BaseOperator_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseOperator_View: BaseOperator
    {
        
        public string BaseOperator_Code { get; set; }
        public string BaseOperator_Name { get; set; }
        public string BaseOperator_JobID { get; set; }
        public string BaseOperator_OAAccount { get; set; }
        public string BaseOperator_IDCard { get; set; }
        public string BaseOperator_TempAuthCode { get; set; }
        public DateTime? BaseOperator_TACExpired { get; set; }
        public string BaseOperator_Department { get; set; }
        public EffectiveEnum? BaseOperator_IsEffective { get; set; }
        public string BaseOperator_Phone { get; set; }
        public string BaseOperator_Memo { get; set; }
        public string BaseOperator_SourceSystemId { get; set; }
        public DateTime? BaseOperator_LastUpdateTime { get; set; }
        public DateTime? BaseOperator_CreateTime { get; set; }
        public DateTime? BaseOperator_UpdateTime { get; set; }
        public string BaseOperator_CreateBy { get; set; }
        public string BaseOperator_UpdateBy { get; set; }
        public bool? BaseOperator_IsValid { get; set; }

    }

}