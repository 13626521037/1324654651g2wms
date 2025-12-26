using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.Model.KnifeManagement;
using WMS.Model;
using NetTopologySuite.Index.HPRtree;
using Elsa.Models;
using WMS.ViewModel.KnifeManagement.KnifeCombineLineVMs;
using WMS.Model.BaseData;
using WMS.Util;
namespace WMS.ViewModel.KnifeManagement.KnifeCombineVMs
{
    public partial class KnifeCombineVM : BaseCRUDVM<KnifeCombine>
    {
        
        public List<string> KnifeManagementKnifeCombineFTempSelected { get; set; }
        public KnifeCombineLineKnifeCombineDetailListVM KnifeCombineLineKnifeCombineList { get; set; }
        public List<ComboSelectListItem> PrintModules { get; set; }

        [Display(Name = "打印模板")]
        public string SelectedPrintModule { get; set; }

        public KnifeCombineVM()
        {

            SetInclude(x => x.HandledBy);
            KnifeCombineLineKnifeCombineList = new KnifeCombineLineKnifeCombineDetailListVM();
            KnifeCombineLineKnifeCombineList.DetailGridPrix = "Entity.KnifeCombineLine_KnifeCombine";

        }

        protected override void InitVM()
        {
            KnifeCombineLineKnifeCombineList.CopyContext(this);

        }

        public override DuplicatedInfo<KnifeCombine> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.DocNo));
            return rv;

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

        public override void Validate()
        {
            
            base.Validate();

        }
        public override void DoAdd()
        {
            if (Entity.KnifeCombineLine_KnifeCombine.GroupBy(x => new { x.KnifeCombineId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }
            if (Entity.KnifeCombineLine_KnifeCombine.Count == 0)
            {
                MSD.AddModelError("", "请输入刀具");
                return;
            }
            base.DoAdd();
        }
        public override void DoEdit(bool updateAllFields = false)
        {
            if (Entity.KnifeCombineLine_KnifeCombine.GroupBy(x => new { x.KnifeCombineId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }
            if (Entity.Status != KnifeOrderStatusEnum.Open)
            {
                MSD.AddModelError("", "只有开立状态的单据可以修改");
                return;
            }
            base.DoEdit();

        }

        public override void DoDelete()
        {
            if (Entity.Status != KnifeOrderStatusEnum.Open)
            {
                MSD.AddModelError("", "只有开立状态的单据可以删除");
                return;
            }
            base.DoDelete();

        }
        /// <summary>
        /// 刀具组合单审核
        /// </summary>
        internal void DoApproved()
        {
            if(Entity.Status != KnifeOrderStatusEnum.Open)
            {
                MSD.AddModelError("", "状态不是开立 无法审核");
                return;
            }
            Entity.Status = KnifeOrderStatusEnum.Approved;
            Entity.ApprovedTime = DateTime.Now;
            DC.SaveChanges();
        }
        /// <summary>
        /// 通过组合归还进行的组合单关闭 是单张的 之后大概率不需要了  
        /// </summary>
        /// <param name="knifeCheckInLines"></param>
        internal void DoClose(List<KnifeCheckInLine> knifeCheckInLines)
        {
            if (Entity.Status != KnifeOrderStatusEnum.Approved)
            {
                MSD.AddModelError("", "状态不是审核 无法关闭");
                return;
            }
            Entity.Status = KnifeOrderStatusEnum.ApproveClose;
            Entity.CloseTime = DateTime.Now;
            foreach (var line in Entity.KnifeCombineLine_KnifeCombine)
            {
                var knifeCheckInline = knifeCheckInLines.FirstOrDefault(x => x.KnifeId == line.KnifeId);
                line.ToWhLocationId = knifeCheckInline.ToWhLocationId;
            }
            DC.SaveChanges();
        }




        /// <summary>
        /// 初始化打印模板选项
        /// </summary>
        public void InitPrintModules()
        {
            // 获取打印模板（从打印服务器）
            string printServer = Wtm.ConfigInfo.AppSettings["PrintServer"];
            string printBusinessName = Wtm.ConfigInfo.AppSettings["KnifeDocNoPrintBusinessName"];
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
            Entity = DC.Set<KnifeCombine>().FirstOrDefault(x => x.ID == Entity.ID);
            if (Entity == null)
            {
                MSD.AddModelError("", "数据不存在，请刷新后重试");
                return "";
            }
            CreatePrintDataPara data = new CreatePrintDataPara();
            data.ModuleId = SelectedPrintModule;
            data.OperatorName = LoginUserInfo.Name;
            data.Records = new List<CreatePrintDataLinePara>();

            if (Entity.Status == KnifeOrderStatusEnum.ApproveClose)
            {
                MSD.AddModelError("", $"{Entity.CombineKnifeNo}该组合刀已关闭，无法打印！");
                return "";
            }
            if (Entity.Status == KnifeOrderStatusEnum.Open)
            {
                MSD.AddModelError("", $"{Entity.CombineKnifeNo}该组合刀未审核，无法打印！");
                return "";
            }


            CreatePrintDataLinePara line = new CreatePrintDataLinePara();
            line.Fields = [
                new CreatePrintDataSubLinePara { FieldName = "单号", FieldValue = Entity.CombineKnifeNo },


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
