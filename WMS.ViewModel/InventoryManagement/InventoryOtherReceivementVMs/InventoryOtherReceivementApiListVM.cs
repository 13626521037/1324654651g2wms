using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs
{
    public partial class InventoryOtherReceivementApiListVM : BasePagedListVM<InventoryOtherReceivementApi_View, InventoryOtherReceivementApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryOtherReceivementApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryOtherReceivementApi_View>>{
                this.MakeGridHeader(x => x.ErpID),
                this.MakeGridHeader(x => x.BusinessDate),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.IsScrap),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryOtherReceivementApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryOtherReceivement>()
                .Select(x => new InventoryOtherReceivementApi_View
                {
				    ID = x.ID,
                    ErpID = x.ErpID,
                    BusinessDate = x.BusinessDate,
                    DocNo = x.DocNo,
                    IsScrap = x.IsScrap,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventoryOtherReceivementApi_View : InventoryOtherReceivement{

    }
}
