using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PrintManagement;
using WMS.Model;
using WMS.Util;
using WMS.DataAccess;
using EFCore.BulkExtensions;

namespace WMS.ViewModel.PrintManagement.PrintDocumentVMs
{
    public partial class PrintDocumentListVM : BasePagedListVM<PrintDocument, PrintDocumentSearcher>
    {
        //public List<PrintDocument> lists = new List<PrintDocument>();

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("PrintDocument","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("PrintDocument","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                //this.MakeAction("PrintDocument","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("PrintDocument", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "PrintManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("PrintDocument", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "PrintManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("PrintDocument","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"PrintManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("PrintDocument","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                //this.MakeAction("PrintDocument","PrintDocumentExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"PrintManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("PrintDocument", "Print", "打印", "打印", GridActionParameterTypesEnum.SingleId, "PrintManagement", dialogHeight: 800, dialogWidth: 1000).SetShowInRow(true).SetHideOnToolBar(true),
            };
        }


        protected override IEnumerable<IGridColumn<PrintDocument>> InitGridHeader()
        {
            return new List<GridColumn<PrintDocument>>{

                this.MakeGridHeader(x => x.DocNo, width: 120).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.OrgCode, width: 110),
                this.MakeGridHeader(x => x.DocLineNo, width: 80).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.ItemID).SetTitle(@Localizer["Page.料品ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.ItemCode, width: 120).SetTitle(@Localizer["Page.料号"].Value),
                this.MakeGridHeader(x => x.ItemName, width: 140).SetTitle(@Localizer["Page.品名"].Value),
                this.MakeGridHeader(x => x.SPECS, width: 140).SetTitle(@Localizer["Page.规格"].Value),
                this.MakeGridHeader(x => x.Description).SetTitle(@Localizer["Page.描述"].Value),
                this.MakeGridHeader(x => x.UnitName, width: 80).SetTitle(@Localizer["Page.单位"].Value),
                this.MakeGridHeader(x => x.Qty, width: 90).SetTitle(@Localizer["Page.数量"].Value).SetFormat((a, b) =>
                {
                    return a.Qty.TrimZero();
                    //return decimal.Parse(Common.FormatDecimal(a.Qty));//?.ToString("0.#####");
                }),
                this.MakeGridHeader(x => x.ReceivedQty, width: 90).SetTitle(@Localizer["Page.已收数量"].Value).SetFormat((a, b) =>
                {
                    return a.ReceivedQty.TrimZero();
                    //return Common.FormatDecimal(a.ReceivedQty);
                }),
                this.MakeGridHeader(x => x.ValidQty, width: 90).SetTitle(@Localizer["Page.可操作数"].Value).SetFormat((a, b) =>
                {
                    return a.ValidQty.TrimZero();
                    //return Common.FormatDecimal(a.ValidQty);
                }),
                this.MakeGridHeader(x => x.Seiban, width: 110).SetTitle(@Localizer["Page.番号"].Value),
                this.MakeGridHeader(x => x.SeibanRandom, width: 110).SetTitle(@Localizer["Page.番号随机码"].Value),
                
                //this.MakeGridHeader(x => x.CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 100)
            };
        }

        public override IOrderedQueryable<PrintDocument> GetSearchQuery()
        {
            //var query = lists.AsQueryable().OrderBy(x => x.CreateTime);
            var query = DC.Set<PrintDocument>()

                //.CheckContain(Searcher.DocNo, x => x.DocNo)
                //.CheckEqual(Searcher.DocLineNo, x => x.DocLineNo)
                //.CheckEqual(Searcher.ItemID, x => x.ItemID)
                //.CheckContain(Searcher.ItemCode, x => x.ItemCode)
                //.CheckContain(Searcher.UnitName, x => x.UnitName)
                //.CheckEqual(Searcher.Qty, x => x.Qty)
                //.CheckEqual(Searcher.ReceivedQty, x => x.ReceivedQty)
                //.CheckEqual(Searcher.ValidQty, x => x.ValidQty)
                //.CheckContain(Searcher.Seiban, x => x.Seiban)
                //.CheckContain(Searcher.SeibanRandom, x => x.SeibanRandom)
                //.CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                //.CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy)
                .OrderBy(x => x.ID);
            return query;
        }

        /// <summary>
        /// 删除当前用户的搜索数据
        /// </summary>
        private void DeleteUserData()
        {
            string sql = $"delete from PrintDocument where CreateBy = '{LoginUserInfo.ITCode}'";
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
                List<PrintDocument> list = new List<PrintDocument>();
                // 调用U9接口
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0300", Wtm.LoginUserInfo.Name);
                U9Return<List<PrintDocument>> u9Return = apiHelper.GetPrintDocument(Searcher.DocNo, Searcher.ItemCode);
                if (u9Return.Success)
                {
                    list = u9Return.Entity;
                    if (list != null && list.Count > 0)
                    {
                        list.ForEach(x => 
                        { 
                            x.ID = Guid.NewGuid();
                            x.CreateBy = LoginUserInfo.ITCode;
                            x.CreateTime = DateTime.Now;
                        });
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
            if (DC.Set<PrintDocument>().CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy).Any(x => x.ItemCode != Searcher.ItemCode))
            {
                return true;
            }
            // 只有一条数据，则重新加载
            if (DC.Set<PrintDocument>().CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy).Count() <= 1)
            {
                return true;
            }
            // 获取一条数据，判断料号一致，且数据创建时间在2分钟以内，则不需要重新加载
            var data = DC.Set<PrintDocument>().CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy).Where(x => x.ItemCode == Searcher.ItemCode).FirstOrDefault();
            if (data == null)    // 没有数据，则重新加载
                return true;
            if (data.CreateTime != null && DateTime.Now.Subtract((DateTime)data.CreateTime).TotalMinutes < 2)    // 数据创建时间在2分钟以内，则不需要重新加载
                return false;
            return true;
        }
    }
    //public class PrintDocument_View: PrintDocument
    //{

    //    public string PrintDocument_DocNo { get; set; }
    //    public int? PrintDocument_DocLineNo { get; set; }
    //    public long? PrintDocument_ItemID { get; set; }
    //    public string PrintDocument_ItemCode { get; set; }
    //    public string PrintDocument_ItemName { get; set; }
    //    public string PrintDocument_SPECS { get; set; }
    //    public string PrintDocument_Description { get; set; }
    //    public string PrintDocument_UnitName { get; set; }
    //    public decimal? PrintDocument_Qty { get; set; }
    //    public decimal? PrintDocument_ReceivedQty { get; set; }
    //    public decimal? PrintDocument_ValidQty { get; set; }
    //    public string PrintDocument_Seiban { get; set; }
    //    public string PrintDocument_SeibanRandom { get; set; }
    //    public DateTime? PrintDocument_CreateTime { get; set; }
    //    public DateTime? PrintDocument_UpdateTime { get; set; }
    //    public string PrintDocument_CreateBy { get; set; }
    //    public string PrintDocument_UpdateBy { get; set; }

    //}

}