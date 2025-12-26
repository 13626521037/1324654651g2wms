
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
using Microsoft.EntityFrameworkCore;

namespace WMS.ViewModel.BaseData.BaseInventoryVMs
{
    public partial class BaseInventoryTemplateVM : BaseTemplateVM
    {

        [Display(Name = "_Model._BaseInventory._ItemMaster")]
        public ExcelPropety ItemMaster_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.ItemMasterCode_Import);
        [Display(Name = "_Model._BaseInventory._WhLocation")]
        public ExcelPropety WhLocation_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.LocationCode_Import);
        [Display(Name = "_Model._BaseInventory._BatchNumber")]
        public ExcelPropety BatchNumber_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.BatchNumber);
        [Display(Name = "_Model._BaseInventory._SerialNumber")]
        public ExcelPropety SerialNumber_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.SerialNumber);
        [Display(Name = "_Model._BaseInventory._Seiban")]
        public ExcelPropety Seiban_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.Seiban);
        [Display(Name = "_Model._BaseInventory._SeibanRandom")]
        public ExcelPropety SeibanRandom_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.SeibanRandom);
        [Display(Name = "_Model._BaseInventory._Qty")]
        public ExcelPropety Qty_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.Qty);
        [Display(Name = "_Model._BaseInventory._IsAbandoned")]
        public ExcelPropety IsAbandoned_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.IsAbandoned);
        [Display(Name = "_Model._BaseInventory._ItemSourceType")]
        public ExcelPropety ItemSourceType_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.ItemSourceType);
        [Display(Name = "_Model._BaseInventory._FrozenStatus")]
        public ExcelPropety FrozenStatus_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.FrozenStatus);
        [Display(Name = "_Model._BaseInventory._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseInventory._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseInventory._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.CreateBy);
        [Display(Name = "_Model._BaseInventory._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.UpdateBy);

        protected override void InitVM()
        {

            //ItemMaster_Excel.DataType = ColumnDataType.ComboBox;
            //ItemMaster_Excel.ListItems = DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            //WhLocation_Excel.DataType = ColumnDataType.ComboBox;
            //WhLocation_Excel.ListItems = DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, y => y.Code.ToString());

        }

    }

    public class BaseInventoryImportVM : BaseImportVM<BaseInventoryTemplateVM, BaseInventory>
    {
        //import
        public override bool BatchSaveData()
        {
            return base.BatchSaveData();
        }

        public override void SetEntityList()
        {
            base.SetEntityList();
            // 一次性全部加载，避免多次查询
            List<BaseInventory_Import_Item> allItems = DC.Set<BaseItemMaster>().Select(x => new BaseInventory_Import_Item
            {
                ID = x.ID,
                Code = x.Code,
                OrganizationId = x.OrganizationId
            }).ToList();
            List<BaseInventory_Import_Location> allLocations = DC.Set<BaseWhLocation>().Select(x => new BaseInventory_Import_Location
            {
                ID = x.ID,
                Code = x.Code,
                OrganizationId = x.WhArea.WareHouse.OrganizationId
            }).ToList();
            foreach (var entity in EntityList)
            {
                //var location = DC.Set<BaseWhLocation>().Include(x => x.WhArea.WareHouse).AsNoTracking().Where(x => x.Code == entity.LocationCode_Import).FirstOrDefault();
                var location = allLocations.Where(x => x.Code == entity.LocationCode_Import).FirstOrDefault();
                if (location != null)
                {
                    entity.WhLocationId = location.ID;
                    //var item = DC.Set<BaseItemMaster>()
                    //    .AsNoTracking()
                    //    .Where(x => x.OrganizationId == location.WhArea.WareHouse.OrganizationId && x.Code.Equals(entity.ItemMasterCode_Import)).FirstOrDefault();
                    var item = allItems.Where(x => x.OrganizationId == location.OrganizationId && x.Code.Equals(entity.ItemMasterCode_Import)).FirstOrDefault();
                    if (item != null)
                    {
                        entity.ItemMasterId = item.ID;
                    }
                    else
                    {
                        MSD.AddModelError("", $"料号不存在:{entity.ItemMasterCode_Import} 请先同步");
                    }
                }
                else
                {
                    MSD.AddModelError("", $"未找到库位:{entity.LocationCode_Import} 请先同步");
                }
            }
        }
    }

    public class BaseInventory_Import_Item
    {
        public Guid ID { get; set; }

        public string Code { get; set; }

        public Guid? OrganizationId { get; set; }
    }

    public class BaseInventory_Import_Location
    {
        public Guid ID { get; set; }

        public string Code { get; set; }

        public Guid? OrganizationId { get; set; }
    }
}