
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
using Microsoft.EntityFrameworkCore;


namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingLocationsVMs
{
    /// <summary>
    /// 盘点单选择库位列表
    /// </summary>
    public partial class InventoryStockTakingLocationsSelectListVM : BasePagedListVM<BaseWhLocation_DetailView, InventoryStockTakingLocationsSelectSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryStockTaking", "AddSelectedLocations", "添加选中库位", "添加选中库位", GridActionParameterTypesEnum.MultiIds, "InventoryManagement").SetQueryString($"docId={Searcher.StockTakingId}"),
            };
        }

        protected override IEnumerable<IGridColumn<BaseWhLocation_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<BaseWhLocation_DetailView>>{

                //this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value),
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle("库位ID").SetHide(),
                this.MakeGridHeader(x => x.WhCode, width: 130).SetEditType(EditTypeEnum.Text).SetTitle("存储地点编码"),
                this.MakeGridHeader(x => x.WhName).SetEditType(EditTypeEnum.Text).SetTitle("存储地点名称"),
                this.MakeGridHeader(x => x.AreaCode, width: 130).SetEditType(EditTypeEnum.Text).SetTitle("库区编码"),
                this.MakeGridHeader(x => x.AreaName).SetEditType(EditTypeEnum.Text).SetTitle("库区名称"),
                this.MakeGridHeader(x => x.LocationCode, width: 130).SetEditType(EditTypeEnum.Text).SetTitle("库位编码"),
                this.MakeGridHeader(x => x.LocationName).SetEditType(EditTypeEnum.Text).SetTitle("库位名称"),
            };
        }


        public override IOrderedQueryable<BaseWhLocation_DetailView> GetSearchQuery()
        {
            InventoryStockTaking stockTaking = DC.Set<InventoryStockTaking>()
                .Where(x => x.ID == Searcher.StockTakingId).Include(x => x.InventoryStockTakingLocations_StockTaking).FirstOrDefault();
            if (stockTaking == null)
                return new List<BaseWhLocation_DetailView>().AsQueryable().OrderBy(x => x.ID);
            List<Guid?> locationIds = stockTaking.InventoryStockTakingLocations_StockTaking.Select(x => x.LocationId).ToList();
            var query = DC.Set<BaseWhLocation>()
                .CheckContain(Searcher.AreaCode, x => x.WhArea.Code)
                .CheckContain(Searcher.LocationCode, x => x.Code)
                .Where(x => x.WhArea.WareHouse.ID == stockTaking.WhId 
                    && x.AreaType == WhLocationEnum.Normal
                    && x.Locked == false && x.IsEffective == EffectiveEnum.Effective)   // 过滤已经选择的库位 不能加：!locationIds.Contains(x.ID)，量大会卡死。只通过Locked判断
                .Select(x => new BaseWhLocation_DetailView
                {
                    ID = x.ID,
                    LocationCode = x.Code,
                    LocationName = x.Name,
                    AreaCode = x.WhArea.Code,
                    AreaName = x.WhArea.Name,
                    WhCode = x.WhArea.WareHouse.Code,
                    WhName = x.WhArea.WareHouse.Name
                })
                .OrderBy(x => x.LocationCode);
            return query;
        }

    }

    public partial class InventoryStockTakingLocationsSelectSearcher : BaseSearcher
    {

        [Display(Name = "库区编码")]
        public string AreaCode { get; set; }

        [Display(Name = "库位编码")]
        public string LocationCode { get; set; }

        public Guid StockTakingId { get; set; }
    }

    public class BaseWhLocation_DetailView : BaseWhLocation
    {
        public string WhCode { get; set; }

        public string WhName { get; set; }

        public string AreaCode { get; set; }

        public string AreaName { get; set; }

        public Guid LocationId { get; set; }

        public string LocationCode { get; set; }

        public string LocationName { get; set; }
    }
}

