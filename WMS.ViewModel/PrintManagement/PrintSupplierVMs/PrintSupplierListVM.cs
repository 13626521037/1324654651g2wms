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
using Elsa.Models;
using NPOI.POIFS.FileSystem;

namespace WMS.ViewModel.PrintManagement.PrintSupplierVMs
{
    public partial class PrintSupplierListVM : BasePagedListVM<PrintSupplier_View, PrintSupplierSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("PrintSupplier","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("PrintSupplier","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                //this.MakeAction("PrintSupplier","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("PrintSupplier", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "PrintManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("PrintSupplier", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "PrintManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("PrintSupplier","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"PrintManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("PrintSupplier","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PrintManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                //this.MakeAction("PrintSupplier","PrintSupplierExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"PrintManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("PrintSupplier", "Print", "打印", "打印", GridActionParameterTypesEnum.SingleId, "PrintManagement", dialogHeight: 800, dialogWidth: 1000).SetShowInRow(true).SetHideOnToolBar(true),
                this.MakeAction("PrintSupplier", "CustomPrint", "自定义打印", "自定义打印", GridActionParameterTypesEnum.SingleId, "PrintManagement", dialogHeight: 800, dialogWidth: 1000).SetShowInRow(true).SetHideOnToolBar(true),
            };
        }


        protected override IEnumerable<IGridColumn<PrintSupplier_View>> InitGridHeader()
        {
            return new List<GridColumn<PrintSupplier_View>>{

                this.MakeGridHeader(x => x.DocNo, width: 120).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.DocLineNo, width: 80).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.ItemCode, width: 120),
                this.MakeGridHeader(x => x.ItemName, width: 140),
                this.MakeGridHeader(x => x.SPECS, width: 140),
                this.MakeGridHeader(x => x.Description),
                this.MakeGridHeader(x => x.UnitName, width: 80),
                this.MakeGridHeader(x => x.Qty, width: 90).SetTitle(@Localizer["Page.数量"].Value).SetFormat((a, b) =>
                {
                    return a.Qty.TrimZero();//?.ToString("0.#####");
                }),
                this.MakeGridHeader(x => x.ReceivedQty, width: 90).SetTitle(@Localizer["Page.已收数量"].Value).SetFormat((a, b) =>
                {
                    return a.ReceivedQty.TrimZero();//?.ToString("0.#####");
                }),
                this.MakeGridHeader(x => x.ValidQty, width: 90).SetTitle(@Localizer["Page.可操作数"].Value).SetFormat((a, b) =>
                {
                    return a.ValidQty.TrimZero();//?.ToString("0.#####");
                }),
                this.MakeGridHeader(x => x.BatchNumber, width: 110).SetTitle(@Localizer["Page.批号"].Value),
                this.MakeGridHeader(x => x.Seiban, width: 110).SetTitle(@Localizer["Page.番号"].Value),
                this.MakeGridHeader(x => x.SeibanRandom, width: 110).SetTitle(@Localizer["Page.番号随机码"].Value),
                this.MakeGridHeader(x => x.Weight, width: 90).SetTitle(@Localizer["Page.重量"].Value).SetFormat((a, b) =>
                {
                    return a.Weight.TrimZero();//?.ToString("0.#####");
                }),
                this.MakeGridHeader(x => x.SupplierName, width: 110),
                //this.MakeGridHeader(x => x.SyncSupplier).SetTitle(@Localizer["Page.供应商同步字段"].Value),
                //this.MakeGridHeader(x => x.SyncItem).SetTitle(@Localizer["Page.料品同步字段"].Value),
                //this.MakeGridHeader(x => x.CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 170)
            };
        }



        public override IOrderedQueryable<PrintSupplier_View> GetSearchQuery()
        {
            var query = DC.Set<PrintSupplier>()
                .CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy)
                .Select(x => new PrintSupplier_View
                {
                    ID = x.ID,
                    DocNo = x.DocNo,
                    DocLineNo = x.DocLineNo,
                    ItemCode = x.Item.Code,
                    ItemName = x.Item.Name,
                    SPECS = x.Item.SPECS,
                    Description = x.Item.Description,
                    UnitName = x.Item.StockUnit.Name,
                    //Item = x.Item.SourceSystemId,
                    Qty = x.Qty,
                    ReceivedQty = x.ReceivedQty,
                    ValidQty = x.ValidQty,
                    BatchNumber = x.BatchNumber,
                    Seiban = x.Seiban,
                    SeibanRandom = x.SeibanRandom,
                    Weight = x.Weight,
                    //Supplier = x.Supplier.Code,
                    SupplierName = x.Supplier.Name,
                    SyncSupplier = x.SyncSupplier,
                    SyncItem = x.SyncItem,
                    CreateTime = x.CreateTime,
                    UpdateTime = x.UpdateTime,
                    CreateBy = x.CreateBy,
                    UpdateBy = x.UpdateBy
                })
                .OrderBy(x => x.ID);
            return query;
        }

        /// <summary>
        /// 删除当前用户的搜索数据
        /// </summary>
        private void DeleteUserData()
        {
            string sql = $"delete from PrintSupplier where CreateBy = '{LoginUserInfo.ITCode}'";
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
                List<PrintSupplier> list = new List<PrintSupplier>();
                // 调用U9接口
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0300", Wtm.LoginUserInfo.Name);
                U9Return<List<PrintSupplier>> u9Return = apiHelper.GetPrintSupplier(Searcher.DocNo, Searcher.ItemCode);
                if (u9Return.Success)
                {
                    list = u9Return.Entity;
                    PrintSupplierVM vm = Wtm.CreateVM<PrintSupplierVM>();   // 只有vm才能进行id转换。listvm没定义转换方法
                    if (list != null && list.Count > 0)
                    {
                        foreach (var x in list)
                        {
                            x.ID = Guid.NewGuid();
                            x.CreateBy = LoginUserInfo.ITCode;
                            x.CreateTime = DateTime.Now;
                            x.BatchNumber = x.DocNo;    // 批号=单号
                            string r = vm.SSIdAttrToId(x, x.Item, "Item");    // 料号
                            if (!string.IsNullOrEmpty(r)) { MSD.AddModelError("", r); return; }
                            r = vm.SSIdAttrToId(x, x.Supplier, "Supplier");    // 供应商
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
            if (DC.Set<PrintSupplier>().CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy).Any(x => x.Item.Code != Searcher.ItemCode))
            {
                return true;
            }
            // 只有一条数据，则重新加载
            if (DC.Set<PrintSupplier>().CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy).Count() <= 1)
            {
                return true;
            }
            // 获取一条数据，判断料号一致，且数据创建时间在2分钟以内，则不需要重新加载
            var data = DC.Set<PrintSupplier>().CheckEqual(LoginUserInfo.ITCode, x => x.CreateBy).Where(x => x.Item.Code == Searcher.ItemCode).FirstOrDefault();
            if (data == null)    // 没有数据，则重新加载
                return true;
            if (data.CreateTime != null && DateTime.Now.Subtract((DateTime)data.CreateTime).TotalMinutes < 2)    // 数据创建时间在2分钟以内，则不需要重新加载
                return false;
            return true;
        }
    }
    public class PrintSupplier_View : PrintSupplier
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }

        [Display(Name = "品名")]
        public string ItemName { get; set; }

        [Display(Name = "规格")]
        public string SPECS { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        [Display(Name = "单位")]
        public string UnitName { get; set; }

        [Display(Name = "供应商")]
        public string SupplierName { get; set; }
    }

}