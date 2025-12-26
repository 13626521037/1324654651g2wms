using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.KnifeManagement;
using WMS.Model;
using WMS.Model.BaseData;


namespace WMS.ViewModel.KnifeManagement.KnifeVMs
{
    public partial class KnifeApiListVM : BasePagedListVM<KnifeApi_View, KnifeApiSearcher>
    {

        protected override IEnumerable<IGridColumn<KnifeApi_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeApi_View>>{
                this.MakeGridHeader(x => x.CreatedDate),
                this.MakeGridHeader(x => x.SerialNumber),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.CurrentCheckOutBy),
                this.MakeGridHeader(x => x.LastOperationDate),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.GrindCount),
                this.MakeGridHeader(x => x.InitialLife),
                this.MakeGridHeader(x => x.CurrentLife),
                this.MakeGridHeader(x => x.TotalUsedDays),
                this.MakeGridHeader(x => x.RemainingUsedDays),
                
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<KnifeApi_View> GetSearchQuery()
        {
            var query = DC.Set<Knife>()
                .Select(x => new KnifeApi_View
                {
				    ID = x.ID,
                    CreatedDate = x.CreatedDate,
                    SerialNumber = x.SerialNumber,
                    Status = x.Status,
                    CurrentCheckOutBy = x.CurrentCheckOutBy,
                    LastOperationDate = x.LastOperationDate,
                    Name_view = x.WhLocation.Name,
                    GrindCount = x.GrindCount,
                    InitialLife = x.InitialLife,
                    CurrentLife = x.CurrentLife,
                    TotalUsedDays = x.TotalUsedDays,
                    RemainingUsedDays = x.RemainingUsedDays,
                    
                    Name_view2 = x.ItemMaster.Name,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class KnifeApi_View : Knife{
        [Display(Name = "_Model._BaseWhLocation._Name")]
        public String Name_view { get; set; }
        [Display(Name = "名称")]
        public String Name_view2 { get; set; }

    }
}
