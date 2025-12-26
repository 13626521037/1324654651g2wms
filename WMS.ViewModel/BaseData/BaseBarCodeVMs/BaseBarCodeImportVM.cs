
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.ViewModel.BaseData.BaseInventoryVMs;
using WMS.Util;
using Esprima;

namespace WMS.ViewModel.BaseData.BaseBarCodeVMs
{
    public partial class BaseBarCodeTemplateVM : BaseTemplateVM
    {

        [Display(Name = "_Model._BaseBarCode._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.DocNo);
        //[Display(Name = "_Model._BaseBarCode._Code")]
        //public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.Code);
        [Display(Name = "_Model._BaseBarCode._Item")]
        public ExcelPropety Item_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ItemMasterCode_Import);
        [Display(Name = "组织编码")]
        public ExcelPropety OrgCode_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.OrgCode_Import);
        [Display(Name = "料品来源类型")]
        public ExcelPropety ItemSourceType_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ItemSourceType_Import);
        [Display(Name = "条码（不传则自动生成。只有期初开账可以传）")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.Code);
        [Display(Name = "_Model._BaseBarCode._Qty")]
        public ExcelPropety Qty_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.Qty);
        [Display(Name = "_Model._BaseBarCode._CustomerCode")]
        public ExcelPropety CustomerCode_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.CustomerCode);
        [Display(Name = "_Model._BaseBarCode._CustomerName")]
        public ExcelPropety CustomerName_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.CustomerName);
        [Display(Name = "_Model._BaseBarCode._CustomerNameFirstLetter")]
        public ExcelPropety CustomerNameFirstLetter_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.CustomerNameFirstLetter);
        [Display(Name = "_Model._BaseBarCode._Seiban")]
        public ExcelPropety Seiban_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.Seiban);
        [Display(Name = "_Model._BaseBarCode._SeibanRandom")]
        public ExcelPropety SeibanRandom_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.SeibanRandom);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields1")]
        public ExcelPropety ExtendedFields1_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields1);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields2")]
        public ExcelPropety ExtendedFields2_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields2);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields3")]
        public ExcelPropety ExtendedFields3_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields3);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields4")]
        public ExcelPropety ExtendedFields4_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields4);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields5")]
        public ExcelPropety ExtendedFields5_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields5);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields6")]
        public ExcelPropety ExtendedFields6_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields6);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields7")]
        public ExcelPropety ExtendedFields7_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields7);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields8")]
        public ExcelPropety ExtendedFields8_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields8);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields9")]
        public ExcelPropety ExtendedFields9_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields9);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields10")]
        public ExcelPropety ExtendedFields10_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields10);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields11")]
        public ExcelPropety ExtendedFields11_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields11);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields12")]
        public ExcelPropety ExtendedFields12_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields12);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields13")]
        public ExcelPropety ExtendedFields13_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields13);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields14")]
        public ExcelPropety ExtendedFields14_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields14);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields15")]
        public ExcelPropety ExtendedFields15_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields15);
        [Display(Name = "_Model._BaseBarCode._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseBarCode._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseBarCode._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.CreateBy);
        [Display(Name = "_Model._BaseBarCode._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.UpdateBy);

        protected override void InitVM()
        {

            //Item_Excel.DataType = ColumnDataType.ComboBox;
            //Item_Excel.ListItems = DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class BaseBarCodeImportVM : BaseImportVM<BaseBarCodeTemplateVM, BaseBarCode>
    {
        //import
        public override void SetEntityList()
        {
            base.SetEntityList();
            List<BaseInventory_Import_Item> allItems = DC.Set<BaseItemMaster>().Select(x => new BaseInventory_Import_Item
            {
                ID = x.ID,
                Code = x.Code,
                OrganizationId = x.OrganizationId
            }).ToList();
            List<Base_Import> allOrgs = DC.Set<BaseOrganization>().Select(x => new Base_Import
            {
                ID = x.ID,
                Code = x.Code,
            }).ToList();
            foreach (var entity in EntityList)
            {
                if (string.IsNullOrEmpty(entity.OrgCode_Import))
                {
                    MSD.AddModelError("", $"组织不能为空");
                    break;
                }
                var org = allOrgs.Where(x => x.Code == entity.OrgCode_Import).FirstOrDefault();
                if (org == null)
                {
                    MSD.AddModelError("", $"未找到组织:{entity.OrgCode_Import} 请先同步");
                    break;
                }
                var item = allItems.Where(x => x.OrganizationId == org.ID && x.Code.Equals(entity.ItemMasterCode_Import)).FirstOrDefault();
                if (item == null)
                {
                    MSD.AddModelError("", $"料号不存在:{entity.ItemMasterCode_Import} 请先同步");
                    break;
                }
                entity.ItemId = item.ID;
                // 使用导入的条码
                if (!string.IsNullOrEmpty(entity.Code))
                {
                    var strs = entity.Code.Split('@');
                    // 验证每一个字段
                    if (strs.Length != 4)
                    {
                        MSD.AddModelError("", $"条码格式不正确:{entity.Code}，正确格式为 料品来源类型@料号@数量@序列号");
                        break;
                    }
                    if (!strs[0].Equals("1") && !strs[0].Equals("2") && !strs[0].Equals("3"))
                    {
                        MSD.AddModelError("", $"条码格式不正确:{entity.Code}，料品来源类型只能为1、2、3");
                        break;
                    }
                    if (!strs[1].Equals(item.Code))
                    {
                        MSD.AddModelError("", $"条码格式不正确:{entity.Code}，料号段与导入的料号不匹配");
                        break;
                    }
                    if (!decimal.TryParse(strs[2], out decimal qty))
                    {
                        MSD.AddModelError("", $"条码格式不正确:{entity.Code}，数量段必须为数字");
                        break;
                    }
                    entity.Sn = strs[3];
                    if (DC.Set<BaseBarCode>().Any(x => x.Sn == entity.Sn))   // 重复条码判定（查重只查条码表。库存表不查，因为正规操作下库存表中的数据一定是来源于条码表）
                    {
                        MSD.AddModelError("", $"条码:{entity.Sn} 已存在");
                        break;
                    }
                }
                else
                {
                    // 新生成条码
                    if (entity.ItemSourceType_Import == null)
                    {
                        MSD.AddModelError("", $"请选择料品来源类型");
                        break;
                    }
                    //var location = DC.Set<BaseWhLocation>().Include(x => x.WhArea.WareHouse).AsNoTracking().Where(x => x.Code == entity.LocationCode_Import).FirstOrDefault();

                    // 创建序列号
                    entity.Sn = Common.GetRandom13();
                    while (DC.Set<BaseBarCode>().Any(x => x.Sn == entity.Sn))   // 重复条码判定
                    {
                        entity.Sn = Common.GetRandom13();
                    }
                    // 构造条码
                    entity.Code = $"{(int)entity.ItemSourceType_Import}@{item.Code}@{entity.Qty.TrimZero()}@{entity.Sn}";
                }
            }
        }
    }

    public class Base_Import
    {
        public Guid ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}