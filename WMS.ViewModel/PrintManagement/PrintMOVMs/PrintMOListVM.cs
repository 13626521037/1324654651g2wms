using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PrintManagement;
using WMS.Model;
using WMS.DataAccess;
using WMS.Util;
using EFCore.BulkExtensions;

namespace WMS.ViewModel.PrintManagement.PrintMOVMs
{
    public partial class PrintMOListVM : BasePagedListVM<PrintMO_View, PrintMOSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("PrintMO","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("PrintMO","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                //this.MakeAction("PrintMO","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("PrintMO", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "PrintManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("PrintMO", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "PrintManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("PrintMO","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"PrintManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("PrintMO","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                //this.MakeAction("PrintMO","PrintMOExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"PrintManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("PrintMO", "Print", "打印", "打印", GridActionParameterTypesEnum.SingleId, "PrintManagement", dialogHeight: 800, dialogWidth: 1000).SetShowInRow(true).SetHideOnToolBar(true),
            };
        }
 

        protected override IEnumerable<IGridColumn<PrintMO_View>> InitGridHeader()
        {
            return new List<GridColumn<PrintMO_View>>{
                
                this.MakeGridHeader(x => x.PrintMO_DocNo, width: 130).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.PrintMO_ItemCode, width: 100),
                this.MakeGridHeader(x => x.PrintMO_CustomerSPECS).SetTitle(@Localizer["Page.客户规格型号"].Value),
                this.MakeGridHeader(x => x.PrintMO_Qty, width: 90).SetTitle(@Localizer["Page.数量"].Value).SetFormat((a, b) =>
                {
                    return a.PrintMO_Qty.TrimZero();
                }),
                this.MakeGridHeader(x => x.PrintMO_CustomerCode, width: 80).SetTitle(@Localizer["Page.客户编码"].Value),
                this.MakeGridHeader(x => x.PrintMO_Seiban, width: 110).SetTitle(@Localizer["Page.番号"].Value),
                this.MakeGridHeader(x => x.PrintMO_SeibanRandom, width: 110).SetTitle(@Localizer["Page.番号随机码"].Value),
                this.MakeGridHeader(x => x.PrintMO_BatchNumber, width: 110).SetTitle(@Localizer["Page.批号"].Value),
                this.MakeGridHeader(x => x.PrintMO_DocNoChange, width: 120).SetTitle(@Localizer["Page.单号转换"].Value),
                this.MakeGridHeader(x => x.PrintMO_OrderWhName, width: 120).SetTitle(@Localizer["Page.订单存储地点"].Value),
                this.MakeGridHeader(x => x.PrintMO_LocationName, width: 80).SetTitle(@Localizer["Page.地点"].Value),
                //this.MakeGridHeader(x => x.PrintMO_Org).SetTitle(@Localizer["Page.组织"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<PrintMO_View> GetSearchQuery()
        {
            var query = DC.Set<PrintMO>()
                .CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy)
                .Select(x => new PrintMO_View
                {
				    ID = x.ID,
                    
                    PrintMO_CustomerCode = x.CustomerCode,
                    PrintMO_OrderWhName = x.OrderWhName,
                    PrintMO_CustomerSPECS = x.CustomerSPECS,
                    PrintMO_Item = x.Item.SourceSystemId,
                    PrintMO_ItemCode = x.Item.Code,
                    PrintMO_ItemName = x.Item.Name,
                    PrintMO_Seiban = x.Seiban,
                    PrintMO_SeibanRandom = x.SeibanRandom,
                    PrintMO_BatchNumber = x.BatchNumber,
                    PrintMO_Qty = x.Qty,
                    PrintMO_DocNo = x.DocNo,
                    PrintMO_DocNoChange = x.DocNoChange,
                    PrintMO_LocationName = x.LocationName,
                    PrintMO_Org = x.Org.SourceSystemId,
                    PrintMO_SyncItem = x.SyncItem,
                    PrintMO_SyncOrg = x.SyncOrg,
                    PrintMO_CreateTime = x.CreateTime,
                    PrintMO_UpdateTime = x.UpdateTime,
                    PrintMO_CreateBy = x.CreateBy,
                    PrintMO_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        /// <summary>
        /// 删除当前用户的搜索数据
        /// </summary>
        private void DeleteUserData()
        {
            string sql = $"delete from PrintMO where CreateBy = '{LoginUserInfo.ITCode}'";
            DC.RunSQL(sql);
        }

        /// <summary>
        /// 从ERP获取单据数据列表
        /// </summary>
        public void SearchErpData()
        {
            if (!MSD.IsValid)
            {
                return;
            }
            DeleteUserData();
            if (!MSD.IsValid)
            {
                return;
            }
            if (!string.IsNullOrEmpty(Searcher.DocNo) || !string.IsNullOrEmpty(Searcher.ItemCode))
            {
                List<PrintMO> list = new List<PrintMO>();
                // 调用U9接口
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0300", Wtm.LoginUserInfo.Name);
                U9Return<List<PrintMO>> u9Return = apiHelper.GetPrintMO(Searcher.DocNo, Searcher.ItemCode);
                if (u9Return.Success)
                {
                    list = u9Return.Entity;
                    PrintMOVM vm = Wtm.CreateVM<PrintMOVM>();   // 只有vm才能进行id转换。listvm没定义转换方法
                    if (list != null && list.Count > 0)
                    {
                        foreach (var x in list)
                        {
                            x.ID = Guid.NewGuid();
                            x.CreateBy = LoginUserInfo.ITCode;
                            x.CreateTime = DateTime.Now;
                            string r = vm.SSIdAttrToId(x, x.Item, "Item");    // 料号
                            if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
                            r = vm.SSIdAttrToId(x, x.Org, "Org");    // 组织
                            if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
                        }
                    }
                    ((DataContext)DC).BulkInsert(list);
                }
                else
                {
                    MSD.AddModelError("", u9Return.Msg);
                }
            }
        }

        /// <summary>
        /// 是否需要重新加载数据
        /// </summary>
        /// <returns></returns>
        public bool IsNeedReload()
        {
            if (!string.IsNullOrEmpty(Searcher.DocNo))  // 单号非空，则重新加载
                return true;
            if (string.IsNullOrEmpty(Searcher.ItemCode))    // 料号为空，则不需要重新加载
                return true;
            // 存在不同的料号，则重新加载
            if (DC.Set<PrintMO>().CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy).Any(x => x.Item.Code != Searcher.ItemCode))
            {
                return true;
            }
            // 只有一条数据，则重新加载
            if(DC.Set<PrintMO>().CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy).Count() <= 1)
            {
                return true;
            }
            // 获取一条数据，判断料号一致，且数据创建时间在2分钟以内，则不需要重新加载
            var data = DC.Set<PrintMO>().CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy).Where(x => x.Item.Code == Searcher.ItemCode).FirstOrDefault();
            if (data == null)    // 没有数据，则重新加载
                return true;
            if (data.CreateTime != null && DateTime.Now.Subtract((DateTime)data.CreateTime).TotalMinutes < 2)    // 数据创建时间在2分钟以内，则不需要重新加载
                return false;
            return true;
        }
    }
    public class PrintMO_View: PrintMO
    {
        
        public string PrintMO_CustomerCode { get; set; }
        public string PrintMO_OrderWhName { get; set; }
        public string PrintMO_CustomerSPECS { get; set; }
        public string PrintMO_Item { get; set; }
        [Display(Name = "料号")]
        public string PrintMO_ItemCode { get; set; }
        [Display(Name = "品名")]
        public string PrintMO_ItemName { get; set; }
        public string PrintMO_Seiban { get; set; }
        public string PrintMO_SeibanRandom { get; set; }
        public string PrintMO_BatchNumber { get; set; }
        public decimal? PrintMO_Qty { get; set; }
        public string PrintMO_DocNo { get; set; }
        public string PrintMO_DocNoChange { get; set; }
        public string PrintMO_LocationName { get; set; }
        public string PrintMO_Org { get; set; }
        public string PrintMO_SyncItem { get; set; }
        public string PrintMO_SyncOrg { get; set; }
        public DateTime? PrintMO_CreateTime { get; set; }
        public DateTime? PrintMO_UpdateTime { get; set; }
        public string PrintMO_CreateBy { get; set; }
        public string PrintMO_UpdateBy { get; set; }

    }

}