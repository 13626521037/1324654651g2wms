using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseItemMasterVMs;
using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.Model.PrintManagement;
using WMS.Model;
using WMS.Util;
using WMS.Model.BaseData;
using WMS.ViewModel.BaseData.BaseBarCodeVMs;
namespace WMS.ViewModel.PrintManagement.PrintMOVMs
{
    public partial class PrintMOVM : BaseCRUDVM<PrintMO>
    {

        public List<string> PrintManagementPrintMOFTempSelected { get; set; }

        public List<ComboSelectListItem> PrintModules { get; set; }

        [Display(Name = "打印模板")]
        public string SelectedPrintModule { get; set; }

        public PrintMOVM()
        {

            SetInclude(x => x.Item.StockUnit);
            SetInclude(x => x.Org);

        }

        protected override void InitVM()
        {


        }

        public override DuplicatedInfo<PrintMO> SetDuplicatedCheck()
        {
            return null;

        }

        public override async Task DoAddAsync()
        {

            await base.DoAddAsync();

        }

        public override async Task DoEditAsync(bool updateAllFields = false)
        {

            await base.DoEditAsync();

        }

        public override async Task DoDeleteAsync()
        {
            await base.DoDeleteAsync();

        }

        /// <summary>
        /// 初始化打印模板选项
        /// </summary>
        public void InitPrintModules()
        {
            // 获取打印模板（从打印服务器）
            string printServer = Wtm.ConfigInfo.AppSettings["PrintServer"];
            string printBusinessName = Wtm.ConfigInfo.AppSettings["MOPrintBusinessName"];
            PrintApiHelper apiHelper = new PrintApiHelper(printServer);
            ReturnResult<List<GetPrintModuleResult>> rr = apiHelper.GetPrintModule(printBusinessName);
            if (rr.Success)
            {
                if (rr.Entity == null)
                {
                    MSD.AddModelError("", "未找到打印模板");
                }
                else
                {
                    PrintModules = new List<ComboSelectListItem>();
                    foreach (var item in rr.Entity)
                    {
                        PrintModules.Add(new ComboSelectListItem { Text = item.ModuleName, Value = item.ID });
                    }
                    if (PrintModules.Count > 0)
                    {
                        SelectedPrintModule = PrintModules[0].Value;
                    }
                }
            }
            else
            {
                MSD.AddModelError("", rr.Msg);
            }
        }

        /// <summary>
        /// 生成打印数据
        /// </summary>
        public string CreatePrintData()
        {
            CreatePrintDataPara data = new CreatePrintDataPara();
            data.ModuleId = SelectedPrintModule;
            data.OperatorName = LoginUserInfo.Name;
            data.Records = new List<CreatePrintDataLinePara>();
            // 打印张数判定
            List<decimal> qtys = new List<decimal>();
            decimal printQty = (decimal)Entity.PrintQty;
            decimal packingQty = (decimal)Entity.PackingQty;
            while (printQty > packingQty)
            {
                qtys.Add(packingQty);
                printQty -= packingQty;
            }
            if (printQty > 0)
            {
                qtys.Add(printQty);
            }
            //var reloadEntity = DC.Set<PrintDocument>().Where(x => x.ID == Entity.ID).AsNoTracking().FirstOrDefault();
            // 限制打印数量为50张
            if (qtys.Count > 50)
            {
                MSD.AddModelError("", "单次打印数量不能超过50张");
                return "";
            }
            BaseItemMaster item = DC.Set<BaseItemMaster>().Include(x => x.Organization).Include(x => x.StockUnit).Where(x => x.ID == Entity.ItemId).AsNoTracking().FirstOrDefault();
            
            // 对每一张标签生成条码记录
            BaseBarCodeVM barCodeVM = Wtm.CreateVM<BaseBarCodeVM>();
            //string barcode = barCodeVM.CreateBarCode(false, item.Code, qty, Entity.DocNo, 0, item.Organization.Code);
            List<(string, decimal)> barcodes = barCodeVM.CreateBarCodes(item.Code, qtys, Entity.DocNo, 0, item.Organization.Code, seiban: Entity.Seiban);
            if (!MSD.IsValid)
            {
                return "";
            }
            if (barcodes == null || barcodes.Count == 0)
            {
                MSD.AddModelError("", "生成条码失败");
                return "";
            }
            foreach (var barcode in barcodes)
            {
                CreatePrintDataLinePara line = new CreatePrintDataLinePara();
                line.Fields = [
                    new CreatePrintDataSubLinePara { FieldName = "客户编码", FieldValue = Entity.CustomerCode },
                    new CreatePrintDataSubLinePara { FieldName = "订单存储地点", FieldValue = Entity.OrderWhName },
                    new CreatePrintDataSubLinePara { FieldName = "客户规格型号", FieldValue = Entity.CustomerSPECS },
                    new CreatePrintDataSubLinePara { FieldName = "料号", FieldValue = item.Code },
                    new CreatePrintDataSubLinePara { FieldName = "品名", FieldValue = item.Name },
                    new CreatePrintDataSubLinePara { FieldName = "规格", FieldValue = item.SPECS },
                    new CreatePrintDataSubLinePara { FieldName = "番号", FieldValue = Entity.Seiban },
                    new CreatePrintDataSubLinePara { FieldName = "番号随机码", FieldValue = Entity.SeibanRandom },
                    new CreatePrintDataSubLinePara { FieldName = "批号", FieldValue = Entity.BatchNumber },
                    new CreatePrintDataSubLinePara { FieldName = "数量", FieldValue = barcode.Item2.ToString() },
                    new CreatePrintDataSubLinePara { FieldName = "单号", FieldValue = Entity.DocNo },
                    new CreatePrintDataSubLinePara { FieldName = "单号转换", FieldValue = Entity.DocNoChange },
                    new CreatePrintDataSubLinePara { FieldName = "地点", FieldValue = Entity.LocationName },
                    new CreatePrintDataSubLinePara { FieldName = "日期", FieldValue = DateTime.Now.Date.ToString("yyyy-MM-dd") },
                    new CreatePrintDataSubLinePara { FieldName = "序列号", FieldValue = barCodeVM.Entity.Sn },
                    new CreatePrintDataSubLinePara { FieldName = "条码", FieldValue = barcode.Item1 }
                ];
                data.Records.Add(line);
            }

            string printServer = Wtm.ConfigInfo.AppSettings["PrintServer"];
            PrintApiHelper apiHelper = new PrintApiHelper(printServer);
            ReturnResult rr = apiHelper.CreatePrintData(data);
            if (rr.Success)
            {
                if (string.IsNullOrEmpty(rr.Msg))
                {
                    MSD.AddModelError("", "生成打印数据失败");
                }
                return rr.Msg;
            }
            else
            {
                MSD.AddModelError("", rr.Msg);
                return "";
            }
        }
    }
}
