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
using Microsoft.Identity.Client;

namespace WMS.ViewModel.BaseData.BaseBarCodeVMs
{
    public partial class BaseBarCodeListVM : BasePagedListVM<BaseBarCode_View, BaseBarCodeSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseBarCode","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",500).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("BaseBarCode","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                //this.MakeAction("BaseBarCode","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseBarCode", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseBarCode", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseBarCode","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("BaseBarCode","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseBarCode","BaseBarCodeExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("BaseBarCode", "Print", "打印", "打印", GridActionParameterTypesEnum.SingleId, "BaseData", dialogHeight: 800, dialogWidth: 1000).SetShowInRow(true).SetHideOnToolBar(true),
                this.MakeAction("BaseBarCode", "BatchPrint", "批量打印", "批量打印", GridActionParameterTypesEnum.MultiIds, "BaseData", dialogHeight: 800, dialogWidth: 1400)
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseBarCode_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseBarCode_View>>{
                
                this.MakeGridHeader(x => x.ID, width: 250).SetHide(),
                this.MakeGridHeader(x => x.BaseBarCode_DocNo, width: 110).SetTitle(@Localizer["Page.来源单据号"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_Code, width: 250).SetTitle(@Localizer["Page.条码"].Value),
                this.MakeGridHeader(x => x.Sn, width: 120),
                this.MakeGridHeader(x => x.BaseBarCode_Item, width: 85).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_Qty, width: 65).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_CustomerCode, width: 110).SetTitle(@Localizer["Page.客户编码"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_CustomerName, width: 110).SetTitle(@Localizer["Page.客户名称"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_CustomerNameFirstLetter, width: 110).SetTitle(@Localizer["Page.客户首字母拼音"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_Seiban, width: 110).SetTitle(@Localizer["Page.番号"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_SeibanRandom, width: 110).SetTitle("番号随机码"),
                this.MakeGridHeader(x => x.BaseBarCode_BatchNumber, width: 110).SetTitle("批号"),
                this.MakeGridHeader(x => x.ExtendedFields1, width: 110),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields2, width: 110).SetTitle(@Localizer["Page.扩展字段2"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields3, width: 110).SetTitle(@Localizer["Page.扩展字段3"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields4, width: 110).SetTitle(@Localizer["Page.扩展字段4"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields5, width: 110).SetTitle(@Localizer["Page.扩展字段5"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields6, width: 110).SetTitle(@Localizer["Page.扩展字段6"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields7, width: 110).SetTitle(@Localizer["Page.扩展字段7"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields8, width: 110).SetTitle(@Localizer["Page.扩展字段8"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields9, width: 110).SetTitle(@Localizer["Page.扩展字段9"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields10, width: 110).SetTitle(@Localizer["Page.扩展字段10"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields11, width: 110).SetTitle(@Localizer["Page.扩展字段11"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields12, width: 110).SetTitle(@Localizer["Page.扩展字段12"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields13, width: 110).SetTitle(@Localizer["Page.扩展字段13"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields14, width: 110).SetTitle(@Localizer["Page.扩展字段14"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_ExtendedFields15, width: 110).SetTitle(@Localizer["Page.扩展字段15"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_CreateTime, width: 110).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.BaseBarCode_CreateBy, width: 110).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.BaseBarCode_UpdateTime, width: 110).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.BaseBarCode_UpdateBy, width: 110).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<BaseBarCode_View> GetSearchQuery()
        {
            var query = DC.Set<BaseBarCode>()
                
                .CheckEqual(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.Code, x=>x.Code)
                .CheckEqual(Searcher.ItemCode, x=>x.Item.Code)
                .CheckEqual(Searcher.Qty, x=>x.Qty)
                .CheckContain(Searcher.CustomerCode, x=>x.CustomerCode)
                .CheckContain(Searcher.CustomerName, x=>x.CustomerName)
                .CheckContain(Searcher.CustomerNameFirstLetter, x=>x.CustomerNameFirstLetter)
                .CheckEqual(Searcher.Seiban, x=>x.Seiban)
                .CheckEqual(Searcher.SeibanRandom, x=>x.SeibanRandom)
                .CheckEqual(Searcher.BatchNumber, x=>x.BatchNumber)
                .CheckEqual(Searcher.Sn, x=>x.Sn)
                //.CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                //.CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x => x.CreateBy)
                //.CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new BaseBarCode_View
                {
				    ID = x.ID,
                    BaseBarCode_DocNo = x.DocNo,
                    BaseBarCode_Code = Common.AddBarCodeDialog(x.Code),
                    Sn = x.Sn,
                    BaseBarCode_Item = x.Item.Code,
                    BaseBarCode_Qty = x.Qty.TrimZero(),
                    BaseBarCode_CustomerCode = x.CustomerCode,
                    BaseBarCode_CustomerName = x.CustomerName,
                    BaseBarCode_CustomerNameFirstLetter = x.CustomerNameFirstLetter,
                    BaseBarCode_Seiban = x.Seiban,
                    BaseBarCode_SeibanRandom = x.SeibanRandom,
                    BaseBarCode_BatchNumber = x.BatchNumber,
                    ExtendedFields1 = x.ExtendedFields1,
                    BaseBarCode_ExtendedFields2 = x.ExtendedFields2,
                    BaseBarCode_ExtendedFields3 = x.ExtendedFields3,
                    BaseBarCode_ExtendedFields4 = x.ExtendedFields4,
                    BaseBarCode_ExtendedFields5 = x.ExtendedFields5,
                    BaseBarCode_ExtendedFields6 = x.ExtendedFields6,
                    BaseBarCode_ExtendedFields7 = x.ExtendedFields7,
                    BaseBarCode_ExtendedFields8 = x.ExtendedFields8,
                    BaseBarCode_ExtendedFields9 = x.ExtendedFields9,
                    BaseBarCode_ExtendedFields10 = x.ExtendedFields10,
                    BaseBarCode_ExtendedFields11 = x.ExtendedFields11,
                    BaseBarCode_ExtendedFields12 = x.ExtendedFields12,
                    BaseBarCode_ExtendedFields13 = x.ExtendedFields13,
                    BaseBarCode_ExtendedFields14 = x.ExtendedFields14,
                    BaseBarCode_ExtendedFields15 = x.ExtendedFields15,
                    BaseBarCode_CreateTime = x.CreateTime,
                    BaseBarCode_UpdateTime = x.UpdateTime,
                    BaseBarCode_CreateBy = x.CreateBy,
                    BaseBarCode_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.BaseBarCode_CreateTime);
            return query;
        }

        
    }
    public class BaseBarCode_View: BaseBarCode
    {
        
        public string BaseBarCode_DocNo { get; set; }
        public string BaseBarCode_Code { get; set; }
        public string BaseBarCode_Item { get; set; }
        public decimal? BaseBarCode_Qty { get; set; }
        public string BaseBarCode_CustomerCode { get; set; }
        public string BaseBarCode_CustomerName { get; set; }
        public string BaseBarCode_CustomerNameFirstLetter { get; set; }
        public string BaseBarCode_Seiban { get; set; }
        public string BaseBarCode_SeibanRandom { get; set; }
        public string BaseBarCode_BatchNumber { get; set; }
        public string BaseBarCode_ExtendedFields1 { get; set; }
        public string BaseBarCode_ExtendedFields2 { get; set; }
        public string BaseBarCode_ExtendedFields3 { get; set; }
        public string BaseBarCode_ExtendedFields4 { get; set; }
        public string BaseBarCode_ExtendedFields5 { get; set; }
        public string BaseBarCode_ExtendedFields6 { get; set; }
        public string BaseBarCode_ExtendedFields7 { get; set; }
        public string BaseBarCode_ExtendedFields8 { get; set; }
        public string BaseBarCode_ExtendedFields9 { get; set; }
        public string BaseBarCode_ExtendedFields10 { get; set; }
        public string BaseBarCode_ExtendedFields11 { get; set; }
        public string BaseBarCode_ExtendedFields12 { get; set; }
        public string BaseBarCode_ExtendedFields13 { get; set; }
        public string BaseBarCode_ExtendedFields14 { get; set; }
        public string BaseBarCode_ExtendedFields15 { get; set; }
        public DateTime? BaseBarCode_CreateTime { get; set; }
        public DateTime? BaseBarCode_UpdateTime { get; set; }
        public string BaseBarCode_CreateBy { get; set; }
        public string BaseBarCode_UpdateBy { get; set; }

    }

}