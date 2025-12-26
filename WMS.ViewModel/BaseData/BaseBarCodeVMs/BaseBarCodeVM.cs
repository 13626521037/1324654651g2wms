using EFCore.BulkExtensions;
using Elsa.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Quic;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.DataAccess;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseItemMasterVMs;
namespace WMS.ViewModel.BaseData.BaseBarCodeVMs
{
    public partial class BaseBarCodeVM : BaseCRUDVM<BaseBarCode>
    {

        public List<string> BaseDataBaseBarCodeFTempSelected { get; set; }

        public List<ComboSelectListItem> PrintModules { get; set; }

        [Display(Name = "打印模板")]
        public string SelectedPrintModule { get; set; }

        public BaseBarCodeVM()
        {

            SetInclude(x => x.Item);

        }

        protected override void InitVM()
        {


        }

        public override DuplicatedInfo<BaseBarCode> SetDuplicatedCheck()
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
        /// 创建一个条码
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="qty"></param>
        /// <param name="docNo"></param>
        /// <param name="docLineNo"></param>
        /// <param name="orgCode"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public string CreateBarCode(bool onlyCode, string itemCode, decimal qty, string docNo = "", int docLineNo = 0, string orgCode = "", int sourceType = 3, string barCode = "", BaseBarCode entity = null, string batchNumber = "", string seiban = "")
        {
            MSD.Clear();
            if (sourceType != 3 && sourceType != 1)
            {
                MSD.AddModelError("", "来源类型参数错误");
                return "";
            }
            if (string.IsNullOrEmpty(orgCode))
            {
                MSD.AddModelError("", "组织编码参数不能为空");
                return "";
            }
            if (qty <= 0)
            {
                MSD.AddModelError("", "数量必须大于0");
                return "";
            }
            if (string.IsNullOrEmpty(itemCode))
            {
                MSD.AddModelError("", "料号不能为空");
                return "";
            }

            // 生成一个条码
            if (string.IsNullOrEmpty(barCode))
            {
                barCode = Common.GetRandom13();
                while (DC.Set<BaseBarCode>().Any(x => x.Sn == barCode))   // 重复条码判定
                {
                    barCode = Common.GetRandom13();
                }
            }

            BaseItemMaster itemMaster = DC.Set<BaseItemMaster>().AsNoTracking().Where(x => x.Code == itemCode && x.Organization.Code == orgCode).FirstOrDefault();
            if (itemMaster == null)
            {
                MSD.AddModelError("", $"未找到料号{itemCode}");
                return "";
            }
            // 构建条码信息
            Entity = new BaseBarCode
            {
                DocNo = docNo,
                Code = $"{sourceType}@{itemCode}@{qty.TrimZero()}@{barCode}",
                Sn = barCode,
                Qty = qty,
                ItemId = itemMaster.ID,
                BatchNumber = batchNumber,
                Seiban = seiban,
                ExtendedFields1 = entity?.ExtendedFields1,
                ExtendedFields2 = entity?.ExtendedFields2,
                ExtendedFields3 = entity?.ExtendedFields3,
                ExtendedFields4 = entity?.ExtendedFields4,
                ExtendedFields5 = entity?.ExtendedFields5,
                ExtendedFields6 = entity?.ExtendedFields6,
                ExtendedFields7 = entity?.ExtendedFields7,
                ExtendedFields8 = entity?.ExtendedFields8,
                ExtendedFields9 = entity?.ExtendedFields9,
                ExtendedFields10 = entity?.ExtendedFields10,
            };

            // 保存条码信息
            DoAdd();

            // 返回条码
            if (MSD.IsValid)
            {
                return onlyCode ? Entity.Sn : Entity.Code;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 批量创建条码
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="qtys"></param>
        /// <param name="docNo"></param>
        /// <param name="docLineNo"></param>
        /// <param name="orgCode"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public List<(string, decimal)> CreateBarCodes(string itemCode, List<decimal> qtys, string docNo = "", int docLineNo = 0, string orgCode = "", int sourceType = 3, BaseBarCode entity = null, string batchNumber = "", string seiban = "")
        {
            MSD.Clear();
            if (sourceType != 3 && sourceType != 1)
            {
                MSD.AddModelError("", "来源类型参数错误");
                return null;
            }
            if (string.IsNullOrEmpty(orgCode))
            {
                MSD.AddModelError("", "组织编码参数不能为空");
                return null;
            }
            if (qtys == null || qtys.Count == 0)
            {
                MSD.AddModelError("", "数量不能为空");
                return null;
            }
            foreach (var qty in qtys)
            {
                if (qty <= 0)
                {
                    MSD.AddModelError("", "数量必须大于0");
                    return null;
                }
            }
            if (string.IsNullOrEmpty(itemCode))
            {
                MSD.AddModelError("", "料号不能为空");
                return null;
            }

            BaseItemMaster itemMaster = DC.Set<BaseItemMaster>().AsNoTracking().Where(x => x.Code == itemCode && x.Organization.Code == orgCode).FirstOrDefault();
            if (itemMaster == null)
            {
                MSD.AddModelError("", $"未找到料号{itemCode}");
                return null;
            }

            List<BaseBarCode> barCodes = new List<BaseBarCode>();
            List<(string, decimal)> result = new List<(string, decimal)>();
            foreach (var qty in qtys)
            {
                // 生成一个条码
                string barCode = Common.GetRandom13();
                while (DC.Set<BaseBarCode>().Any(x => x.Sn == barCode))   // 重复条码判定
                {
                    barCode = Common.GetRandom13();
                }
                string code = $"{sourceType}@{itemCode}@{qty.TrimZero()}@{barCode}";
                // 构建条码信息
                barCodes.Add(new BaseBarCode
                {
                    ID = Guid.NewGuid(),
                    DocNo = docNo,
                    Code = code,
                    Sn = barCode,
                    Qty = qty,
                    ItemId = itemMaster.ID,
                    BatchNumber = batchNumber,
                    Seiban = seiban,
                    ExtendedFields1 = entity?.ExtendedFields1,
                    ExtendedFields2 = entity?.ExtendedFields2,
                    ExtendedFields3 = entity?.ExtendedFields3,
                    ExtendedFields4 = entity?.ExtendedFields4,
                    ExtendedFields5 = entity?.ExtendedFields5,
                    ExtendedFields6 = entity?.ExtendedFields6,
                    ExtendedFields7 = entity?.ExtendedFields7,
                    ExtendedFields8 = entity?.ExtendedFields8,
                    ExtendedFields9 = entity?.ExtendedFields9,
                    ExtendedFields10 = entity?.ExtendedFields10,
                    CreateBy = LoginUserInfo.ITCode,
                    CreateTime = DateTime.Now,
                });
                result.Add((code, qty));
            }
            ((DataContext)DC).BulkInsert(barCodes);

            // 返回条码
            if (MSD.IsValid)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取一个未使用但已存在的条码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public BaseBarCode GetUnusedExistingBarcode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                MSD.AddModelError("", "条码不能为空");
                return null;
            }
            string[] arr = code.Split('@');
            if (arr.Length != 4)
            {
                MSD.AddModelError("", "条码格式错误");
                return null;
            }
            // 判断条码是否已被使用
            if (DC.Set<BaseInventory>().Any(x => x.SerialNumber == arr[3]))
            {
                // 如果此条码的最后一个库存流水为“取消收货”，则允许重复使用。（如果为取消收货，则意味着此条码尚未被其它业务使用过）
                var log = DC.Set<BaseInventoryLog>().Where(x => x.SourceInventory.SerialNumber == arr[3] || x.TargetInventory.SerialNumber == arr[3])
                    .OrderByDescending(x => x.CreateTime).FirstOrDefault();
                if (log == null || log.OperationType != OperationTypeEnum.PurchaseReceivementReceiveCancel)
                {
                    MSD.AddModelError("", "此条码已被使用");
                    return null;
                }
            }
            // 判断条码是否已存在
            BaseBarCode barCode = DC.Set<BaseBarCode>().Include(x => x.Item).AsNoTracking().Where(x => x.Code == code).FirstOrDefault();
            if (barCode == null)
            {
                MSD.AddModelError("", "条码不存在");
                return null;
            }
            return barCode;
        }

        /// <summary>
        /// 调入单扫码获取条码信息（兼容杂收单获取条码信息）
        /// </summary>
        /// <param name="code">完整的条码</param>
        /// <returns></returns>
        public BaseBarCode GetBarcodeForTransferIn(string code)
        {
            /**
             * 处理流程：
             * 1. 从库存流水中获取条码信息（最后一次库存流水为调出单则满足条件。如果存在，单不满足条件，则返回错误信息）（此步骤暂时不做，从第二步开始做。以后可以考虑下是否需要）
             * 2. 从本系统的条码表中获取条码信息
             * 3. 从链溯系统的条码表中获取条码信息
             * 
             * 与GetUnusedExistingBarcode方法的区别：
             * 1. GetUnusedExistingBarcode方法要求条码从来未被使用过。且只从本系统的条码表中获取条码信息。
             * 2. GetBarcodeForTransferIn允许曾经使用过，但现在系统中没有正在使用此条码的库存信息（即已使用过此条码的库存信息均已作废即可）
             **/

            if (string.IsNullOrEmpty(code))
            {
                MSD.AddModelError("", "条码不能为空");
                return null;
            }
            string[] arr = code.Split('@');
            if (arr.Length != 4)
            {
                MSD.AddModelError("", "条码格式错误");
                return null;
            }
            // 判断条码是否已被使用
            if (DC.Set<BaseInventory>().Any(x => x.SerialNumber == arr[3] && x.IsAbandoned == false))
            {
                MSD.AddModelError("", "条码正在使用中，无法再次入库");
                return null;
            }
            // 2. 从本系统的条码表中获取条码信息
            BaseBarCode barCode = DC.Set<BaseBarCode>().Include(x => x.Item).AsNoTracking().Where(x => x.Sn == arr[3]).FirstOrDefault();    // 如果已使用，未曾从新打码，则数量会不一致。所以只能匹配
            if (barCode != null)
            {
                return barCode;
            }
            // 3. 从链溯系统的条码表中获取条码信息
            var result = LsWmsHelper.GetBarCode(arr[3]);
            if (result == null)
            {
                MSD.AddModelError("", "条码不存在");
                return null;
            }
            arr = result.BarCode.Split('@');
            if (arr.Length != 4)
            {
                MSD.AddModelError("", "本系统不存在此条码。链溯WMS中的条码格式错误");
                return null;
            }
            decimal qty = 0;
            decimal.TryParse(arr[2], out qty);
            if (qty <= 0)
            {
                MSD.AddModelError("", "数量信息错误或条码格式错误");
                return null;
            }
            string itemCode = arr[1];
            if (string.IsNullOrEmpty(itemCode))
            {
                MSD.AddModelError("", "料号信息错误或条码格式错误");
                return null;
            }
            BaseOrganization org = DC.Set<BaseOrganization>().AsNoTracking().Where(x => x.Code == result.OrgCode).FirstOrDefault();
            if (org == null)
            {
                MSD.AddModelError("", $"组织编码“{result.OrgCode}”不存在");
                return null;
            }
            if (org.IsEffective == EffectiveEnum.Ineffective || org.IsValid == false)
            {
                MSD.AddModelError("", $"组织“{org.Name}”已失效");
                return null;
            }
            BaseItemMaster item = DC.Set<BaseItemMaster>().AsNoTracking().Where(x => x.Code == itemCode && x.Organization.Code == org.Code).FirstOrDefault();
            if (item == null)
            {
                MSD.AddModelError("", $"料号“{itemCode}”不存在");
                return null;
            }
            if (item.IsEffective == EffectiveEnum.Ineffective || item.IsValid == false)
            {
                MSD.AddModelError("", $"料号“{item.Name}”已失效");
                return null;
            }
            // 构建条码信息
            Entity = new BaseBarCode
            {
                DocNo = result.DocNo,
                Code = result.BarCode,
                Sn = arr[3],
                Qty = qty,
                ItemId = item.ID,
                // 从链溯系统返回的各种字段信息.
            };
            DoAdd();
            barCode = DC.Set<BaseBarCode>().Include(x => x.Item).AsNoTracking().Where(x => x.Sn == arr[3]).FirstOrDefault();
            if (barCode == null)
            {
                MSD.AddModelError("", "从链溯获取的条码信息保存失败");
                return null;
            }
            return barCode;
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
            if (Entity == null || Entity.ID == Guid.Empty)
            {
                MSD.AddModelError("", "参数错误");
                return "";
            }
            Entity = DC.Set<BaseBarCode>().Find(Entity.ID);
            if (Entity == null)
            {
                MSD.AddModelError("", "数据不存在，请刷新后重试");
                return "";
            }
            CreatePrintDataPara data = new CreatePrintDataPara();
            data.ModuleId = SelectedPrintModule;
            data.OperatorName = LoginUserInfo.Name;
            data.Records = new List<CreatePrintDataLinePara>();
            //if (Entity.Qty <= 0)
            //{
            //    MSD.AddModelError("", "库存数量必须大于0才可以打印");
            //    return "";
            //}
            var qty = Entity.Qty;
            BaseItemMaster item = DC.Set<BaseItemMaster>().Include(x => x.Organization).Include(x => x.StockUnit).Where(x => x.ID == Entity.ItemId).AsNoTracking().FirstOrDefault();

            CreatePrintDataLinePara line = new CreatePrintDataLinePara();
            line.Fields = [
                new CreatePrintDataSubLinePara { FieldName = "料品描述", FieldValue = item.Description },
                new CreatePrintDataSubLinePara { FieldName = "料号", FieldValue = item.Code },
                new CreatePrintDataSubLinePara { FieldName = "规格", FieldValue = item.SPECS },
                new CreatePrintDataSubLinePara { FieldName = "批号", FieldValue = Entity.BatchNumber },
                new CreatePrintDataSubLinePara { FieldName = "番号", FieldValue = Entity.Seiban },
                new CreatePrintDataSubLinePara { FieldName = "番号随机码", FieldValue = Entity.SeibanRandom },
                new CreatePrintDataSubLinePara { FieldName = "品名", FieldValue = item.Name },
                new CreatePrintDataSubLinePara { FieldName = "单位", FieldValue = item.StockUnit.Name },
                new CreatePrintDataSubLinePara { FieldName = "序列号", FieldValue = Entity.Sn },
                new CreatePrintDataSubLinePara { FieldName = "条码", FieldValue = Entity.Code },
                new CreatePrintDataSubLinePara { FieldName = "数量", FieldValue = qty.ToString() },
                new CreatePrintDataSubLinePara { FieldName = "日期", FieldValue = Entity?.CreateTime?.ToString("yyyy-MM-dd") },
                new CreatePrintDataSubLinePara { FieldName = "重量", FieldValue = "" },
                new CreatePrintDataSubLinePara { FieldName = "单号", FieldValue = Entity?.DocNo },
                new CreatePrintDataSubLinePara { FieldName = "供应商编码", FieldValue = Entity?.ExtendedFields1 },
            ];
            data.Records.Add(line);

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

        /// <summary>
        /// SRM批量创建条码
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="qtys"></param>
        /// <param name="docNo"></param>
        /// <param name="docLineNo"></param>
        /// <param name="orgCode"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public List<SRMCreateBarCodesReturnInfo> SRMCreateBarCodes(string itemCode, List<decimal> qtys, string docNo = "", int docLineNo = 0, string orgCode = "", int sourceType = 3, BaseBarCode entity = null, string batchNumber = "", string seiban = "", string srmUserName = "SRM_User")
        {
            MSD.Clear();
            if (sourceType != 3 && sourceType != 1)
            {
                MSD.AddModelError("", "来源类型参数错误");
                return null;
            }
            if (string.IsNullOrEmpty(orgCode))
            {
                MSD.AddModelError("", "组织编码参数不能为空");
                return null;
            }
            if (qtys == null || qtys.Count == 0)
            {
                MSD.AddModelError("", "数量不能为空");
                return null;
            }
            foreach (var qty in qtys)
            {
                if (qty <= 0)
                {
                    MSD.AddModelError("", "数量必须大于0");
                    return null;
                }
            }
            if (string.IsNullOrEmpty(itemCode))
            {
                MSD.AddModelError("", "料号不能为空");
                return null;
            }

            BaseItemMaster itemMaster = DC.Set<BaseItemMaster>().AsNoTracking().Where(x => x.Code == itemCode && x.Organization.Code == orgCode).FirstOrDefault();
            if (itemMaster == null)
            {
                MSD.AddModelError("", $"未找到料号{itemCode}");
                return null;
            }

            List<BaseBarCode> barCodes = new List<BaseBarCode>();
            List<SRMCreateBarCodesReturnInfo> result = new List<SRMCreateBarCodesReturnInfo>();
            foreach (var qty in qtys)
            {
                // 生成一个条码
                string barCode = Common.GetRandom13();
                while (DC.Set<BaseBarCode>().Any(x => x.Sn == barCode))   // 重复条码判定
                {
                    barCode = Common.GetRandom13();
                }
                string code = $"{sourceType}@{itemCode}@{qty.TrimZero()}@{barCode}";
                // 构建条码信息
                barCodes.Add(new BaseBarCode
                {
                    ID = Guid.NewGuid(),
                    DocNo = docNo,
                    Code = code,
                    Sn = barCode,
                    Qty = qty,
                    ItemId = itemMaster.ID,
                    BatchNumber = batchNumber,
                    Seiban = seiban,
                    ExtendedFields1 = entity?.ExtendedFields1,
                    ExtendedFields2 = entity?.ExtendedFields2,
                    ExtendedFields3 = entity?.ExtendedFields3,
                    ExtendedFields4 = entity?.ExtendedFields4,
                    ExtendedFields5 = entity?.ExtendedFields5,
                    ExtendedFields6 = entity?.ExtendedFields6,
                    ExtendedFields7 = entity?.ExtendedFields7,
                    ExtendedFields8 = entity?.ExtendedFields8,
                    ExtendedFields9 = entity?.ExtendedFields9,
                    ExtendedFields10 = entity?.ExtendedFields10,
                    CreateBy = srmUserName ,
                    CreateTime = DateTime.Now,
                });
                result.Add(new SRMCreateBarCodesReturnInfo() {
                    barCode = code,
                    qty = qty
                });
            }
            ((DataContext)DC).BulkInsert(barCodes);

            // 返回条码
            if (MSD.IsValid)
            {
                return result;
            }
            else
            {
                return null;
            }
        }



    }


    public class SRMCreateBarCodesReturnInfo
    {
        public string barCode { set; get; }
        public decimal qty { set; get; }
    }
}
