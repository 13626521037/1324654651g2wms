
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
using WMS.ViewModel.BaseData.BaseBarCodeVMs;

namespace WMS.ViewModel.BaseData.BaseInventoryVMs
{
    public partial class BaseInventoryBatchVM : BaseBatchVM<BaseInventory, BaseInventory_BatchEdit>
    {
        public List<ComboSelectListItem> PrintModules { get; set; }

        [Display(Name = "打印模板")]
        public string SelectedPrintModule { get; set; }

        public BaseInventoryBatchVM()
        {
            ListVM = new BaseInventoryListVM();
            LinkedVM = new BaseInventory_BatchEdit();
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
            string printBusinessName = Wtm.ConfigInfo.AppSettings["StockPrintBusinessName"];
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
            if (Ids == null || Ids.Length == 0)
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
                var entity = DC.Set<BaseInventory>().Find(Guid.Parse(id));
                if (entity == null)
                {
                    MSD.AddModelError("", "数据不存在，请刷新后重试");
                    return "";
                }
                var qty = entity.Qty;
                BaseItemMaster item = DC.Set<BaseItemMaster>().Include(x => x.Organization).Include(x => x.StockUnit).Where(x => x.ID == entity.ItemMasterId).AsNoTracking().FirstOrDefault();
                // 获取条码记录
                BaseBarCode baseBarCode = DC.Set<BaseBarCode>().Where(x => x.Sn == entity.SerialNumber).AsNoTracking().FirstOrDefault();

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
                    new CreatePrintDataSubLinePara { FieldName = "序列号", FieldValue = entity.SerialNumber },
                    new CreatePrintDataSubLinePara { FieldName = "条码", FieldValue = $"{entity.ItemSourceType}@{item.Code}@{qty.TrimZero()}@{entity.SerialNumber}" },
                    new CreatePrintDataSubLinePara { FieldName = "数量", FieldValue = qty.ToString() },
                    new CreatePrintDataSubLinePara { FieldName = "日期", FieldValue = baseBarCode?.CreateTime?.ToString("yyyy-MM-dd") },
                    new CreatePrintDataSubLinePara { FieldName = "重量", FieldValue = "" },
                    new CreatePrintDataSubLinePara { FieldName = "单号", FieldValue = baseBarCode?.DocNo },
                    new CreatePrintDataSubLinePara { FieldName = "供应商编码", FieldValue = baseBarCode?.ExtendedFields1 },
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
    public class BaseInventory_BatchEdit : BaseVM
    {


        public List<string> BaseDataBaseInventoryBTempSelected { get; set; }
        [Display(Name = "_Model._BaseInventory._ItemMaster")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._BaseInventory._WhLocation")]
        public Guid? WhLocationId { get; set; }
        [Display(Name = "_Model._BaseInventory._BatchNumber")]
        public string BatchNumber { get; set; }
        [Display(Name = "_Model._BaseInventory._SerialNumber")]
        public string SerialNumber { get; set; }
        [Display(Name = "_Model._BaseInventory._Seiban")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._BaseInventory._Qty")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._BaseInventory._IsAbandoned")]
        public bool? IsAbandoned { get; set; }
        [Display(Name = "_Model._BaseInventory._ItemSourceType")]
        public int? ItemSourceType { get; set; }
        [Display(Name = "_Model._BaseInventory._FrozenStatus")]
        public FrozenStatusEnum? FrozenStatus { get; set; }

        protected override void InitVM()
        {

        }
    }

}