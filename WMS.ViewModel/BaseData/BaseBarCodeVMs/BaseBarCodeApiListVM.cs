using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.BaseData;


namespace WMS.ViewModel.BaseData.BaseBarCodeVMs
{
    public partial class BaseBarCodeApiListVM : BasePagedListVM<BaseBarCodeApi_View, BaseBarCodeApiSearcher>
    {

        protected override IEnumerable<IGridColumn<BaseBarCodeApi_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseBarCodeApi_View>>{
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.Code),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Qty),
                this.MakeGridHeader(x => x.CustomerCode),
                this.MakeGridHeader(x => x.CustomerName),
                this.MakeGridHeader(x => x.CustomerNameFirstLetter),
                this.MakeGridHeader(x => x.Seiban),
                this.MakeGridHeader(x => x.ExtendedFields1),
                this.MakeGridHeader(x => x.ExtendedFields2),
                this.MakeGridHeader(x => x.ExtendedFields3),
                this.MakeGridHeader(x => x.ExtendedFields4),
                this.MakeGridHeader(x => x.ExtendedFields5),
                this.MakeGridHeader(x => x.ExtendedFields6),
                this.MakeGridHeader(x => x.ExtendedFields7),
                this.MakeGridHeader(x => x.ExtendedFields8),
                this.MakeGridHeader(x => x.ExtendedFields9),
                this.MakeGridHeader(x => x.ExtendedFields10),
                this.MakeGridHeader(x => x.ExtendedFields11),
                this.MakeGridHeader(x => x.ExtendedFields12),
                this.MakeGridHeader(x => x.ExtendedFields13),
                this.MakeGridHeader(x => x.ExtendedFields14),
                this.MakeGridHeader(x => x.ExtendedFields15),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BaseBarCodeApi_View> GetSearchQuery()
        {
            var query = DC.Set<BaseBarCode>()
                .Select(x => new BaseBarCodeApi_View
                {
				    ID = x.ID,
                    DocNo = x.DocNo,
                    Code = x.Code,
                    Name_view = x.Item.Name,
                    Qty = x.Qty,
                    CustomerCode = x.CustomerCode,
                    CustomerName = x.CustomerName,
                    CustomerNameFirstLetter = x.CustomerNameFirstLetter,
                    Seiban = x.Seiban,
                    ExtendedFields1 = x.ExtendedFields1,
                    ExtendedFields2 = x.ExtendedFields2,
                    ExtendedFields3 = x.ExtendedFields3,
                    ExtendedFields4 = x.ExtendedFields4,
                    ExtendedFields5 = x.ExtendedFields5,
                    ExtendedFields6 = x.ExtendedFields6,
                    ExtendedFields7 = x.ExtendedFields7,
                    ExtendedFields8 = x.ExtendedFields8,
                    ExtendedFields9 = x.ExtendedFields9,
                    ExtendedFields10 = x.ExtendedFields10,
                    ExtendedFields11 = x.ExtendedFields11,
                    ExtendedFields12 = x.ExtendedFields12,
                    ExtendedFields13 = x.ExtendedFields13,
                    ExtendedFields14 = x.ExtendedFields14,
                    ExtendedFields15 = x.ExtendedFields15,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BaseBarCodeApi_View : BaseBarCode{
        [Display(Name = "名称")]
        public String Name_view { get; set; }

    }
}
