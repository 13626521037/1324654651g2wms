
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
using WMS.Model.BaseData;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Operation.Overlay.Validate;

namespace WMS.ViewModel.KnifeManagement.KnifeVMs
{
    public partial class KnifeTemplateVM : BaseTemplateVM
    {

        [Display(Name = "_Model._Knife._CreatedDate")]
        public ExcelPropety CreatedDate_Excel = ExcelPropety.CreateProperty<Knife>(x => x.CreatedDate, true);
        [Display(Name = "_Model._Knife._KnifeNo")]
        public ExcelPropety KnifeNo_Excel = ExcelPropety.CreateProperty<Knife>(x => x.SerialNumber);
        [Display(Name = "_Model._Knife._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<Knife>(x => x.Status);
        [Display(Name = "_Model._Knife._CurrentCheckOutBy")]
        public ExcelPropety CurrentCheckOutBy_Excel = ExcelPropety.CreateProperty<Knife>(x => x.CurrentCheckOutById);
        [Display(Name = "_Model._Knife._HandledBy")]
        public ExcelPropety HandledBy_Excel = ExcelPropety.CreateProperty<Knife>(x => x.HandledById);
        [Display(Name = "_Model._Knife._LastOperationDate")]
        public ExcelPropety LastOperationDate_Excel = ExcelPropety.CreateProperty<Knife>(x => x.LastOperationDate, true);
        [Display(Name = "_Model._Knife._WhLocation")]
        public ExcelPropety WhLocation_Excel = ExcelPropety.CreateProperty<Knife>(x => x.WhLocationId);
        [Display(Name = "_Model._Knife._GrindCount")]
        public ExcelPropety GrindCount_Excel = ExcelPropety.CreateProperty<Knife>(x => x.GrindCount);
        [Display(Name = "_Model._Knife._InitialLife")]
        public ExcelPropety InitialLife_Excel = ExcelPropety.CreateProperty<Knife>(x => x.InitialLife);
        [Display(Name = "_Model._Knife._CurrentLife")]
        public ExcelPropety CurrentLife_Excel = ExcelPropety.CreateProperty<Knife>(x => x.CurrentLife);
        [Display(Name = "_Model._Knife._TotalUsedDays")]
        public ExcelPropety TotalUsedDays_Excel = ExcelPropety.CreateProperty<Knife>(x => x.TotalUsedDays);
        [Display(Name = "_Model._Knife._RemainingUsedDays")]
        public ExcelPropety RemainingUsedDays_Excel = ExcelPropety.CreateProperty<Knife>(x => x.RemainingUsedDays);
        [Display(Name = "_Model._Knife._ItemMaster")]
        public ExcelPropety ItemMaster_Excel = ExcelPropety.CreateProperty<Knife>(x => x.ItemMasterCode_Import);
        [Display(Name = "_Model._Knife._MiscShipLineID")]
        public ExcelPropety MiscShipLineID_Excel = ExcelPropety.CreateProperty<Knife>(x => x.MiscShipLineID);
        [Display(Name = "_Model._Knife._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<Knife>(x => x.CreateTime, true);
        [Display(Name = "_Model._Knife._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<Knife>(x => x.UpdateTime, true);
        [Display(Name = "_Model._Knife._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<Knife>(x => x.CreateBy);
        [Display(Name = "_Model._Knife._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<Knife>(x => x.UpdateBy);

        protected override void InitVM()
        {

            CurrentCheckOutBy_Excel.DataType = ColumnDataType.ComboBox;
            CurrentCheckOutBy_Excel.ListItems = DC.Set<BaseOperator>().GetSelectListItems(Wtm, y => y.Name);
            WhLocation_Excel.DataType = ColumnDataType.ComboBox;
            WhLocation_Excel.ListItems = DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, y => y.Code.ToString());
            HandledBy_Excel.DataType = ColumnDataType.ComboBox;
            HandledBy_Excel.ListItems = DC.Set<FrameworkUser>().GetSelectListItems(Wtm, y => y.Name.ToString());
            /*ItemMaster_Excel.DataType = ColumnDataType.ComboBox;
            ItemMaster_Excel.ListItems = DC.Set<BaseItemMaster>()
                .Include(x => x.ItemCategory)
                .Include(x=>x.Organization)
                
                .Where(x=>x.ItemCategory.Code.StartsWith("17")&&x.Organization.Code=="0410")
                .GetSelectListItems(Wtm, y =>  y.Code.ToString());*/
            //ItemMaster_Excel.ListItems = DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class KnifeImportVM : BaseImportVM<KnifeTemplateVM, Knife>
    {
        //import
        public override bool BatchSaveData()
        {
            
            return base.BatchSaveData();
        }
        public override void SetEntityList()
        {
            base.SetEntityList();
            foreach (var entity in EntityList)
            {
                var item = DC.Set<BaseItemMaster>()
                    .Include(x => x.Organization)
                    .Include(x=>x.ItemCategory)
                    .Where(x => x.Organization.Code == "0410" && x.Code.Equals(entity.ItemMasterCode_Import)&& x.ItemCategory.Code.StartsWith("17")).FirstOrDefault();
                if (item != null)
                {
                    entity.ItemMasterId = item.ID;
                }
                else
                {
                    MSD.AddModelError("", $"未找到组织[0410]下的刀具料号:{entity.ItemMasterCode_Import} 请先同步");
                }
            }
        }

    }

}