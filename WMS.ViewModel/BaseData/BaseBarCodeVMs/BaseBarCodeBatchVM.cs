
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Util;
using Elsa.Models;

namespace WMS.ViewModel.BaseData.BaseBarCodeVMs
{
    public partial class BaseBarCodeBatchVM : BaseBatchVM<BaseBarCode, BaseBarCode_BatchEdit>
    {
        public List<ComboSelectListItem> PrintModules { get; set; }

        [Display(Name = "打印模板")]
        public string SelectedPrintModule { get; set; }

        public BaseBarCodeBatchVM()
        {
            ListVM = new BaseBarCodeListVM();
            LinkedVM = new BaseBarCode_BatchEdit();
        }

        public override bool DoBatchEdit()
        {

            return base.DoBatchEdit();
        }

        /// <summary>
        /// 初始化打印模板选项
        /// </summary>
        public void InitPrintModules()
        {
            // 获取打印模板（从打印服务器）
            string printServer = Wtm.ConfigInfo.AppSettings["PrintServer"];
            string printBusinessName = Wtm.ConfigInfo.AppSettings["BarCodePrintBusinessName"];
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
            if(Ids == null || Ids.Length == 0)
            {
                MSD.AddModelError("", "请选择要打印的数据");
                return "";
            }
            CreatePrintDataPara data = new CreatePrintDataPara();
            data.ModuleId = SelectedPrintModule;
            data.OperatorName = LoginUserInfo.Name;
            data.Records = new List<CreatePrintDataLinePara>();

            foreach (var id in Ids)
            {
                var entity = DC.Set<BaseBarCode>().Find(Guid.Parse(id));
                if (entity == null)
                {
                    MSD.AddModelError("", "数据不存在，请刷新后重试");
                    return "";
                }
                var qty = entity.Qty;
                BaseItemMaster item = DC.Set<BaseItemMaster>().Include(x => x.Organization).Include(x => x.StockUnit).Where(x => x.ID == entity.ItemId).AsNoTracking().FirstOrDefault();

                CreatePrintDataLinePara line = new CreatePrintDataLinePara();
                line.Fields = [
                    new CreatePrintDataSubLinePara { FieldName = "料品描述", FieldValue = item.Description },
                    new CreatePrintDataSubLinePara { FieldName = "料号", FieldValue = item.Code },
                    new CreatePrintDataSubLinePara { FieldName = "规格", FieldValue = item.SPECS },
                    new CreatePrintDataSubLinePara { FieldName = "批号", FieldValue = entity.BatchNumber },
                    new CreatePrintDataSubLinePara { FieldName = "番号", FieldValue = entity.Seiban },
                    new CreatePrintDataSubLinePara { FieldName = "番号随机码", FieldValue = entity.SeibanRandom },
                    new CreatePrintDataSubLinePara { FieldName = "品名", FieldValue = item.Name },
                    new CreatePrintDataSubLinePara { FieldName = "单位", FieldValue = item.StockUnit.Name },
                    new CreatePrintDataSubLinePara { FieldName = "序列号", FieldValue = entity.Sn },
                    new CreatePrintDataSubLinePara { FieldName = "条码", FieldValue = entity.Code },
                    new CreatePrintDataSubLinePara { FieldName = "数量", FieldValue = qty.ToString() },
                    new CreatePrintDataSubLinePara { FieldName = "日期", FieldValue = entity?.CreateTime?.ToString("yyyy-MM-dd") },
                    new CreatePrintDataSubLinePara { FieldName = "重量", FieldValue = "" },
                    new CreatePrintDataSubLinePara { FieldName = "单号", FieldValue = entity?.DocNo },
                    new CreatePrintDataSubLinePara { FieldName = "供应商编码", FieldValue = entity?.ExtendedFields1 },
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

    /// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BaseBarCode_BatchEdit : BaseVM
    {


        public List<string> BaseDataBaseBarCodeBTempSelected { get; set; }
        [Display(Name = "_Model._BaseBarCode._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._BaseBarCode._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseBarCode._Item")]
        public Guid? ItemId { get; set; }
        [Display(Name = "_Model._BaseBarCode._Qty")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._BaseBarCode._CustomerCode")]
        public string CustomerCode { get; set; }
        [Display(Name = "_Model._BaseBarCode._CustomerName")]
        public string CustomerName { get; set; }
        [Display(Name = "_Model._BaseBarCode._CustomerNameFirstLetter")]
        public string CustomerNameFirstLetter { get; set; }
        [Display(Name = "_Model._BaseBarCode._Seiban")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields1")]
        public string ExtendedFields1 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields2")]
        public string ExtendedFields2 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields3")]
        public string ExtendedFields3 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields4")]
        public string ExtendedFields4 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields5")]
        public string ExtendedFields5 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields6")]
        public string ExtendedFields6 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields7")]
        public string ExtendedFields7 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields8")]
        public string ExtendedFields8 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields9")]
        public string ExtendedFields9 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields10")]
        public string ExtendedFields10 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields11")]
        public string ExtendedFields11 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields12")]
        public string ExtendedFields12 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields13")]
        public string ExtendedFields13 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields14")]
        public string ExtendedFields14 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields15")]
        public string ExtendedFields15 { get; set; }

        protected override void InitVM()
        {

        }
    }

}