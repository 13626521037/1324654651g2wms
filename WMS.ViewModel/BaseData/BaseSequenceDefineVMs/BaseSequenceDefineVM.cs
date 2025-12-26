using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseSequenceDefineLineVMs;
using WMS.Model.BaseData;
using WMS.Model;
using Elsa;
using WMS.ViewModel.BaseData.BaseSequenceRecordsVMs;
namespace WMS.ViewModel.BaseData.BaseSequenceDefineVMs
{
    public partial class BaseSequenceDefineVM : BaseCRUDVM<BaseSequenceDefine>
    {

        public List<string> BaseDataBaseSequenceDefineFTempSelected { get; set; }
        public BaseSequenceDefineLineSequenceDefineDetailListVM BaseSequenceDefineLineSequenceDefineList { get; set; }
        public BaseSequenceDefineLineSequenceDefineDetailListVM1 BaseSequenceDefineLineSequenceDefineList1 { get; set; }
        public BaseSequenceDefineLineSequenceDefineDetailListVM2 BaseSequenceDefineLineSequenceDefineList2 { get; set; }
        private Dictionary<string, string> Dict { get; set; }

        public BaseSequenceDefineVM()
        {

            BaseSequenceDefineLineSequenceDefineList = new BaseSequenceDefineLineSequenceDefineDetailListVM();
            BaseSequenceDefineLineSequenceDefineList.DetailGridPrix = "Entity.BaseSequenceDefineLine_SequenceDefine";
            BaseSequenceDefineLineSequenceDefineList1 = new BaseSequenceDefineLineSequenceDefineDetailListVM1();
            BaseSequenceDefineLineSequenceDefineList1.DetailGridPrix = "Entity.BaseSequenceDefineLine_SequenceDefine";
            BaseSequenceDefineLineSequenceDefineList2 = new BaseSequenceDefineLineSequenceDefineDetailListVM2();
            BaseSequenceDefineLineSequenceDefineList2.DetailGridPrix = "Entity.BaseSequenceDefineLine_SequenceDefine";
            Dict = new Dictionary<string, string>
            {
                {"ItemCategory", ""},
            };
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public BaseSequenceDefineVM SetProperty(string name, string value)
        {
            if (Dict.ContainsKey(name))
            {
                Dict[name] = value;
            }
            return this;
        }

        protected override void InitVM()
        {

            BaseSequenceDefineLineSequenceDefineList.CopyContext(this);
            BaseSequenceDefineLineSequenceDefineList1.CopyContext(this);
            BaseSequenceDefineLineSequenceDefineList2.CopyContext(this);

        }

        public override DuplicatedInfo<BaseSequenceDefine> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.Code));
            rv.AddGroup(SimpleField(x => x.Name));
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
            // 同一单据类型只能有一个有效的批次规则定义
            if (Entity.IsEffective == EffectiveEnum.Effective)
            {
                var exist = DC.Set<BaseSequenceDefine>().Where(x => x.DocType == Entity.DocType && x.ID != Entity.ID && x.IsEffective == EffectiveEnum.Effective).FirstOrDefault();
                if (exist != null)
                {
                    MSD.AddModelError("", "同一单据类型只能有一个有效的批次规则定义");
                }
            }
            if (Entity.BaseSequenceDefineLine_SequenceDefine.Count == 0)
            {
                MSD.AddModelError("", "请至少定义一行段规则");
                return;
            }
            // 流水号段有且只能有一个
            int SerialCount = Entity.BaseSequenceDefineLine_SequenceDefine.Count(x => x.SegmentType == SegmentTypeEnum.Serial);
            if (SerialCount > 1)
            {
                MSD.AddModelError("", "流水号段只能有一个");
                return;
            }
            else if (SerialCount == 0)
            {
                MSD.AddModelError("", "请至少定义一个流水号段");
                return;
            }
            Entity.BaseSequenceDefineLine_SequenceDefine = Entity.BaseSequenceDefineLine_SequenceDefine.OrderBy(x => x.SegmentOrder).ToList();
            // 流水号段必须放在最后一行
            int SerialIndex = Entity.BaseSequenceDefineLine_SequenceDefine.FindIndex(x => x.SegmentType == SegmentTypeEnum.Serial);
            if (SerialIndex != Entity.BaseSequenceDefineLine_SequenceDefine.Count - 1)
            {
                MSD.AddModelError("", "流水号段必须放在最后一行");
                return;
            }
            // 所有行数据校验（部分自动纠正）
            foreach (var line in Entity.BaseSequenceDefineLine_SequenceDefine)
            {
                if (line.SegmentType == SegmentTypeEnum.Constant)
                {
                    if (string.IsNullOrEmpty(line.SegmentValue))
                    {
                        MSD.AddModelError("", $"常量段段值不能为空");
                    }
                    line.SerialLength = null;
                    line.PadChar = null;
                    line.DateFormat = null;
                }
                else if (line.SegmentType == SegmentTypeEnum.OrganizationCode || line.SegmentType == SegmentTypeEnum.ItemCategory)
                {
                    line.SegmentValue = null;
                    line.SerialLength = null;
                    line.PadChar = null;
                    line.DateFormat = null;
                }
                else if (line.SegmentType == SegmentTypeEnum.CurrentDate)
                {
                    if (line.DateFormat == null)
                    {
                        MSD.AddModelError("", $"当前日期段日期格式不能为空");
                    }
                    line.SegmentValue = null;
                    line.SerialLength = null;
                    line.PadChar = null;
                }
                else if (line.SegmentType == SegmentTypeEnum.Serial)
                {
                    if (line.SerialLength == null)
                    {
                        MSD.AddModelError("", $"流水号段长度不能为空");
                    }
                    if (line.PadChar == null)
                    {
                        MSD.AddModelError("", $"流水号段填充字符不能为空");
                    }
                    line.SegmentValue = null;
                    line.DateFormat = null;
                }
            }
            base.Validate();
        }

        /// <summary>
        /// 获取一个批次号（或单据编号）
        /// </summary>
        /// <param name="sequenceDefineCode">定义编码</param>
        /// <returns></returns>
        public string GetSequence(string sequenceDefineCode, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = null)
        {
            var exist = DC.Set<BaseSequenceDefine>()
                .AsNoTracking()
                .Include(x => x.BaseSequenceDefineLine_SequenceDefine)
                .FirstOrDefault(x => x.Code == sequenceDefineCode);
            if (exist == null)
            {
                MSD.AddModelError("", "未找到批次规则定义");
                return null;
            }
            if (exist.IsEffective == EffectiveEnum.Ineffective)
            {
                MSD.AddModelError("", "批次规则定义已失效");
                return null;
            }
            return GetSequence(exist.ID, transaction);
        }

        /// <summary>
        /// 获取一个批次号（或单据编号）
        /// </summary>
        /// <param name="sequenceDefineId">批次规则定义ID</param>
        /// <param name="transaction">事务，外层如果有实物，请传进行</param>
        /// <returns></returns>
        public string GetSequence(Guid sequenceDefineId, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = null)
        {
            Entity = DC.Set<BaseSequenceDefine>().AsNoTracking().Include(x => x.BaseSequenceDefineLine_SequenceDefine).FirstOrDefault(x => x.ID == sequenceDefineId);
            if (Entity == null)
            {
                MSD.AddModelError("", "未找到批次规则定义");
                return null;
            }
            if (Entity.IsEffective == EffectiveEnum.Ineffective)
            {
                MSD.AddModelError("", "批次规则定义已失效");
                return null;
            }
            Guid whid = Guid.Empty;
            if (Entity.BaseSequenceDefineLine_SequenceDefine.Exists(x => x.SegmentType == SegmentTypeEnum.OrganizationCode))    // 需要组织编码时，登录信息中的仓库ID为必要条件
            {
                if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return null;
                }
                Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            }
            // 段标识（允许为空）
            string SegmentFlag = "";
            // 流水字段（不能为空）
            string SerialField = "";
            var tran = transaction ?? DC.Database.BeginTransaction();
            foreach (var line in Entity.BaseSequenceDefineLine_SequenceDefine.OrderBy(x => x.SegmentOrder))
            {
                if (line.SegmentType == SegmentTypeEnum.Constant)    // 常量
                {
                    SegmentFlag += line.SegmentValue;
                }
                else if (line.SegmentType == SegmentTypeEnum.OrganizationCode)  // 组织编码
                {
                    var org = DC.Set<BaseWareHouse>().Include(x => x.Organization).AsNoTracking().FirstOrDefault(x => x.ID == whid);
                    if (org == null)
                    {
                        return "";
                    }
                    SegmentFlag += org.Code;
                }
                else if (line.SegmentType == SegmentTypeEnum.ItemCategory)  // 料品大类
                {
                    if (string.IsNullOrEmpty(Dict["ItemCategory"]))
                    {
                        MSD.AddModelError("", "方法调用错误。此编码规则定义包含料品大类。请先调用“SetProperty”方法设置料品大类（ItemCategory）");
                        return "";
                    }
                    SegmentFlag += Dict["ItemCategory"];
                }
                else if (line.SegmentType == SegmentTypeEnum.CurrentDate)   // 当前日期
                {
                    if (line.DateFormat == null)
                    {
                        return "";
                    }
                    else if (line.DateFormat == DateFormatEnum.YYMM)
                    {
                        SegmentFlag += DateTime.Now.ToString("yyMM");
                    }
                    else if (line.DateFormat == DateFormatEnum.YYMMDD)
                    {
                        SegmentFlag += DateTime.Now.ToString("yyMMdd");
                    }
                    else if (line.DateFormat == DateFormatEnum.YYYYMM)
                    {
                        SegmentFlag += DateTime.Now.ToString("yyyyMM");
                    }
                    else if (line.DateFormat == DateFormatEnum.YYYYMMDD)
                    {
                        SegmentFlag += DateTime.Now.ToString("yyyyMMdd");
                    }
                }
                else if (line.SegmentType == SegmentTypeEnum.Serial)    // 流水号
                {
                    if (line.SerialLength == null || line.SerialLength < 1 || line.PadChar == null || line.PadChar.Length != 1)
                    {
                        MSD.AddModelError("", "流水段配置错误");
                        return "";
                    }
                    int serialValue = 0;
                    var recordVM = Wtm.CreateVM<BaseSequenceRecordsVM>();
                    BaseSequenceRecords record = DC.Set<BaseSequenceRecords>()
                        // .Include(x => x.BaseSequenceRecordsDetail_BaseSequenceRecords)   // 使用DC.UpdateEntity更新时需要注释掉include和AsNoTracking这两行
                        // .AsNoTracking() // 必须不追踪。否则DoEdit会报错
                        .FirstOrDefault(x => x.SegmentFlag == SegmentFlag && x.SequenceDefineId == sequenceDefineId);
                    if (record == null)
                    {
                        serialValue = 1;
                        SerialField = GetSerialNum(line.SerialLength.Value, serialValue, line.PadChar);
                        record = new BaseSequenceRecords();
                        record.SegmentFlag = SegmentFlag;
                        record.SequenceDefineId = sequenceDefineId;
                        record.SerialValue = serialValue;
                        //record.CreateBy = LoginUserInfo.ITCode;
                        //record.CreateTime = DateTime.Now;
                        record.BaseSequenceRecordsDetail_BaseSequenceRecords = new List<BaseSequenceRecordsDetail>()
                        {
                            new BaseSequenceRecordsDetail()
                            {
                                DocNo = SegmentFlag + SerialField,
                                CreateBy = LoginUserInfo.ITCode,
                                CreateTime = DateTime.Now
                            }
                        };
                        //DC.UpdateEntity(record);  // 用这个方法行会加不上
                        //DC.SaveChanges();
                        recordVM.Entity = record;
                        recordVM.DoAdd();
                    }
                    else
                    {
                        serialValue = (int)record.SerialValue + 1;
                        if (serialValue >= (int)Math.Pow(10, (double)line.SerialLength))
                        {
                            MSD.AddModelError("", "流水号段已用完");
                            return "";
                        }
                        SerialField = GetSerialNum(line.SerialLength.Value, serialValue, line.PadChar);
                        record.SerialValue = serialValue;
                        record.UpdateBy = LoginUserInfo.ITCode;
                        record.UpdateTime = DateTime.Now;
                        record.BaseSequenceRecordsDetail_BaseSequenceRecords = new List<BaseSequenceRecordsDetail>()
                        {
                            new BaseSequenceRecordsDetail()
                            {
                                DocNo = SegmentFlag + SerialField,
                                CreateBy = LoginUserInfo.ITCode,
                                CreateTime = DateTime.Now
                            }
                        };
                        DC.UpdateEntity(record);
                        DC.SaveChanges();

                        // 可以实现，担心Detail多了会慢
                        //record.BaseSequenceRecordsDetail_BaseSequenceRecords.Add(new BaseSequenceRecordsDetail()
                        //{
                        //    DocNo = SegmentFlag + SerialField
                        //});
                        //recordVM.Entity = record;
                        //recordVM.DoEdit(true);
                    }
                }
            }
            if (MSD.IsValid && transaction == null)
                tran.Commit();
            if (string.IsNullOrEmpty(SerialField))
            {
                MSD.AddModelError("", "流水号段配置错误");
                return "";
            }
            return SegmentFlag + SerialField;
        }

        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <param name="totalLength">流水总长度</param>
        /// <param name="serialNum">流水号</param>
        /// <param name="padChar">填充字符</param>
        /// <returns></returns>
        public string GetSerialNum(int totalLength, int serialNum, string padChar)
        {
            return serialNum.ToString().PadLeft(totalLength, padChar[0]);
        }
    }
}
