using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseItemMasterVMs;
using WMS.ViewModel.BaseData.BaseWhLocationVMs;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Model.InventoryManagement;
using WMS.ViewModel.InventoryManagement.InventorySplitVMs;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseBarCodeVMs;
using Esprima;
namespace WMS.ViewModel.BaseData.BaseInventoryVMs
{
    public partial class BaseInventoryVM : BaseCRUDVM<BaseInventory>
    {

        public List<string> BaseDataBaseInventoryFTempSelected { get; set; }

        public List<ComboSelectListItem> PrintModules { get; set; }

        [Display(Name = "打印模板")]
        public string SelectedPrintModule { get; set; }

        [Display(Name = "拆分数量")]
        public decimal? SplitQty { get; set; }

        public BaseInventoryVM()
        {

            SetInclude(x => x.ItemMaster);
            SetInclude(x => x.WhLocation.WhArea.WareHouse);

        }

        protected override void InitVM()
        {


        }

        public override DuplicatedInfo<BaseInventory> SetDuplicatedCheck()
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
        /// 拆分库存
        /// </summary>
        public void Split()
        {
            MSD.Clear();
            if (Entity.ID == Guid.Empty)
            {
                MSD.AddModelError("", "数据不存在，请刷新后重试");
                return;
            }
            if (SplitQty == null || SplitQty <= 0)
            {
                MSD.AddModelError("", "拆分数量必须大于0");
                return;
            }
            BaseInventory oldEntity = DC.Set<BaseInventory>()
                .Include(x => x.WhLocation)
                .Where(x => x.ID == Entity.ID)
                .AsNoTracking()
                .FirstOrDefault();
            if (SplitQty >= oldEntity.Qty)
            {
                MSD.AddModelError("", $"拆分数量必须小于原库存数量({oldEntity.Qty})");
                return;
            }
            if(oldEntity.WhLocation.AreaType != WhLocationEnum.Normal)
            {
                MSD.AddModelError("", "只能拆分正常库位的库存");
                return;
            }

            InventorySplit splitDoc = new InventorySplit()
            {
                OldInvId = oldEntity.ID,
                OrigQty = oldEntity.Qty,
                SplitQty = SplitQty
            };
            MSD.Clear();
            var vm = Wtm.CreateVM<InventorySplitApiVM>();
            vm.Save(splitDoc, true);
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
            if (Entity == null || Entity.ID == Guid.Empty)
            {
                MSD.AddModelError("", "参数错误");
                return "";
            }
            Entity = DC.Set<BaseInventory>().Find(Entity.ID);
            if (Entity == null)
            {
                MSD.AddModelError("", "数据不存在，请刷新后重试");
                return "";
            }
            CreatePrintDataPara data = new CreatePrintDataPara();
            data.ModuleId = SelectedPrintModule;
            data.OperatorName = LoginUserInfo.Name;
            data.Records = new List<CreatePrintDataLinePara>();
            if (Entity.Qty <= 0)
            {
                MSD.AddModelError("", "库存数量必须大于0才可以打印");
                return "";
            }
            var qty = Entity.Qty;
            BaseItemMaster item = DC.Set<BaseItemMaster>().Include(x => x.Organization).Include(x => x.StockUnit).Where(x => x.ID == Entity.ItemMasterId).AsNoTracking().FirstOrDefault();
            // 获取条码记录
            BaseBarCode baseBarCode = DC.Set<BaseBarCode>().Where(x => x.Sn == Entity.SerialNumber).AsNoTracking().FirstOrDefault();

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
                new CreatePrintDataSubLinePara { FieldName = "序列号", FieldValue = Entity.SerialNumber },
                new CreatePrintDataSubLinePara { FieldName = "条码", FieldValue = $"{(int)Entity.ItemSourceType}@{item.Code}@{qty.TrimZero()}@{Entity.SerialNumber}" },
                new CreatePrintDataSubLinePara { FieldName = "数量", FieldValue = qty.ToString() },
                new CreatePrintDataSubLinePara { FieldName = "日期", FieldValue = baseBarCode?.CreateTime?.ToString("yyyy-MM-dd") },
                new CreatePrintDataSubLinePara { FieldName = "重量", FieldValue = "" },
                new CreatePrintDataSubLinePara { FieldName = "单号", FieldValue = baseBarCode?.DocNo },
                new CreatePrintDataSubLinePara { FieldName = "供应商编码", FieldValue = baseBarCode?.ExtendedFields1 },
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
    }
}
