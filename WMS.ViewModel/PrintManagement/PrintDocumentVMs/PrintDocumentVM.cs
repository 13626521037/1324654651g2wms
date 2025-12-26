using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NetTopologySuite.Index.HPRtree;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.PrintManagement;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseBarCodeVMs;
namespace WMS.ViewModel.PrintManagement.PrintDocumentVMs
{
    public partial class PrintDocumentVM : BaseCRUDVM<PrintDocument>
    {

        public List<string> PrintManagementPrintDocumentFTempSelected { get; set; }

        public List<ComboSelectListItem> PrintModules { get; set; }

        [Display(Name = "打印模板")]
        public string SelectedPrintModule { get; set; }

        public PrintDocumentVM()
        {

        }

        protected override void InitVM()
        {

        }

        public override DuplicatedInfo<PrintDocument> SetDuplicatedCheck()
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
            string printBusinessName = Wtm.ConfigInfo.AppSettings["DocPrintBusinessName"];
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
            if (Entity == null || Entity.ID == Guid.Empty)
            {
                MSD.AddModelError("", "参数错误");
                return "";
            }
            var doc = DC.Set<PrintDocument>().Find(Entity.ID);
            if (doc == null)
            {
                MSD.AddModelError("", "数据不存在，请刷新后重试");
                return "";
            }
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
            // 限制打印数量为50张
            if (qtys.Count > 50)
            {
                MSD.AddModelError("", "单次打印数量不能超过50张");
                return "";
            }
            
            // 对每一张标签生成条码记录
            BaseBarCodeVM barCodeVM = Wtm.CreateVM<BaseBarCodeVM>();
            // string barcode = barCodeVM.CreateBarCode(false, doc.ItemCode, qty, doc.DocNo, doc.DocLineNo ?? 0, doc.OrgCode, entity: new BaseBarCode { ExtendedFields1 = doc.SupplierCode });
            List<(string, decimal)> barcodes = barCodeVM.CreateBarCodes(doc.ItemCode, qtys, doc.DocNo, doc.DocLineNo ?? 0, doc.OrgCode, sourceType: doc.DocNo.Contains("Rcv") ? 1 : 3, entity: new BaseBarCode { ExtendedFields1 = doc.SupplierCode }, seiban: doc.Seiban);
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
                    new CreatePrintDataSubLinePara { FieldName = "品名", FieldValue = doc.ItemName },
                    new CreatePrintDataSubLinePara { FieldName = "料号", FieldValue = doc.ItemCode },
                    new CreatePrintDataSubLinePara { FieldName = "规格", FieldValue = doc.SPECS },
                    new CreatePrintDataSubLinePara { FieldName = "番号", FieldValue = doc.Seiban },
                    new CreatePrintDataSubLinePara { FieldName = "数量", FieldValue = barcode.Item2.ToString() },
                    new CreatePrintDataSubLinePara { FieldName = "序列号", FieldValue = barcode.Item1 }
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
