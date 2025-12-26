using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;


namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs
{
    public partial class InventoryStockTakingApiListVM : BasePagedListVM<InventoryStockTakingApi_View, InventoryStockTakingApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryStockTakingApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryStockTakingApi_View>>{
                this.MakeGridHeader(x => x.ErpID),
                this.MakeGridHeader(x => x.ErpDocNo),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.Dimension),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.SubmitTime),
                this.MakeGridHeader(x => x.SubmitUser),
                this.MakeGridHeader(x => x.ApproveTime),
                this.MakeGridHeader(x => x.ApproveUser),
                this.MakeGridHeader(x => x.CloseTime),
                this.MakeGridHeader(x => x.CloseUser),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryStockTakingApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryStockTaking>()
                .Select(x => new InventoryStockTakingApi_View
                {
				    ID = x.ID,
                    ErpID = x.ErpID,
                    ErpDocNo = x.ErpDocNo,
                    DocNo = x.DocNo,
                    Dimension = x.Dimension,
                    Name_view = x.Wh.Name,
                    SubmitTime = x.SubmitTime,
                    SubmitUser = x.SubmitUser,
                    ApproveTime = x.ApproveTime,
                    ApproveUser = x.ApproveUser,
                    CloseTime = x.CloseTime,
                    CloseUser = x.CloseUser,
                    Status = x.Status,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventoryStockTakingApi_View : InventoryStockTaking{
        [Display(Name = "名称")]
        public String Name_view { get; set; }

    }
}
