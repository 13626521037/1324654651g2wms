
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

using WMS.Model.BaseData;


namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingLocationsVMs
{
    public partial class InventoryStockTakingLocationsStockTakingDetailListVM : BasePagedListVM<InventoryStockTakingLocations_DetailView, InventoryStockTakingLocationsDetailSearcher>
    {
        public bool HideDeleteButton { get; set; }
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryStockTaking", "SelectLocation", "添加盘点库位", "添加盘点库位", GridActionParameterTypesEnum.NoId, "InventoryManagement", dialogWidth:1200, dialogHeight: 600).SetHideOnToolBar(HideDeleteButton).SetQueryString($"id={Searcher.StockTakingId}"),
                this.MakeAction("InventoryStockTaking", "DeleteSelectedLocations", "删除盘点库位", "删除盘点库位", GridActionParameterTypesEnum.MultiIds, "InventoryManagement").SetHideOnToolBar(HideDeleteButton),
            };
        }

        protected override IEnumerable<IGridColumn<InventoryStockTakingLocations_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<InventoryStockTakingLocations_DetailView>>{

                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                //this.MakeGridHeader(x => x.LocationId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.库位"].Value),
                this.MakeGridHeader(x => x.WhCode).SetEditType(EditTypeEnum.Text).SetTitle("存储地点编码"),
                this.MakeGridHeader(x => x.WhName).SetEditType(EditTypeEnum.Text).SetTitle("存储地点名称"),
                this.MakeGridHeader(x => x.AreaCode).SetEditType(EditTypeEnum.Text).SetTitle("库区编码"),
                this.MakeGridHeader(x => x.AreaName).SetEditType(EditTypeEnum.Text).SetTitle("库区名称"),
                this.MakeGridHeader(x => x.LocationCode).SetEditType(EditTypeEnum.Text).SetTitle("库位编码"),
                this.MakeGridHeader(x => x.LocationName).SetEditType(EditTypeEnum.Text).SetTitle("库位名称"),
            };
        }


        public override IOrderedQueryable<InventoryStockTakingLocations_DetailView> GetSearchQuery()
        {

            var id = (Guid?)Searcher.StockTakingId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryStockTakingLocations_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryStockTakingLocations>()
                .Where(x => id == x.StockTakingId)
                .Select(x => new InventoryStockTakingLocations_DetailView
                {
                    LineNum = x.LineNum,
                    ID = x.ID,
                    LocationId = x.LocationId,
                    LocationCode = x.Location.Code,
                    LocationName = x.Location.Name,
                    AreaCode = x.Location.WhArea.Code,
                    AreaName = x.Location.WhArea.Name,
                    WhCode = x.Location.WhArea.WareHouse.Code,
                    WhName = x.Location.WhArea.WareHouse.Name,
                    CreateTime = x.CreateTime,
                })
                .OrderBy(x => x.CreateTime);
            return query;
        }

    }

    public partial class InventoryStockTakingLocationsDetailSearcher : BaseSearcher
    {

        [Display(Name = "_Model._InventoryStockTakingLocations._StockTaking")]
        public string StockTakingId { get; set; }
    }

    public class InventoryStockTakingLocations_DetailView: InventoryStockTakingLocations
    {
        public string WhCode { get; set; }

        public string WhName { get; set; }

        public string AreaCode { get; set; }

        public string AreaName { get; set; }

        public string LocationCode { get; set; }

        public string LocationName { get; set; }
    }
}

