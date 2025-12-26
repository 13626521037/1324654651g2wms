
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

using WMS.Model.KnifeManagement;
using WMS.ViewModel.KnifeManagement.KnifeGrindRequestLineVMs;
using WMS.Util;


namespace WMS.ViewModel.KnifeManagement.KnifeCheckInLineVMs
{
    public partial class KnifeCheckInLineKnifeCheckInDetailListVM2 : BasePagedListVM<KnifeCheckInLineDetail_View, KnifeCheckInLineDetailSearcher2>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeCheckInLineDetail_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeCheckInLineDetail_View>>{

                this.MakeGridHeader(x => x.SerialNumber).SetTitle("序列号").SetWidth(120),
                this.MakeGridHeader(x => x.BarCode).SetTitle("条码").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeCheckInLine_FromWhLocation).SetTitle("领用库位").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeCheckInLine_ToWhLocation).SetTitle("归还库位").SetWidth(250),
            };
        }

        
        public override IOrderedQueryable<KnifeCheckInLineDetail_View> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeCheckInId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeCheckInLineDetail_View>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeCheckInLine>()
                .Where(x => id == x.KnifeCheckInId)

                 .Select(x => new KnifeCheckInLineDetail_View
                 {
                     SerialNumber = x.Knife.SerialNumber,
                     BarCode = Common.AddBarCodeDialog($"{x.Knife.BarCode}"),
                     KnifeCheckInLine_FromWhLocation = x.FromWhLocation.Code,
                     KnifeCheckInLine_ToWhLocation = x.ToWhLocation.Code,
                 })
                .OrderBy(x => x.SerialNumber);
            return query;
        }

    }

    public partial class KnifeCheckInLineDetailSearcher2 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeCheckInLine._KnifeCheckIn")]
        public string KnifeCheckInId { get; set; }
    }
    public class KnifeCheckInLineDetail_View : KnifeCheckInLine
    {

        public string SerialNumber { get; set; }
        public string BarCode { get; set; }
        public string KnifeCheckInLine_ToWhLocation { get; set; }
        public string KnifeCheckInLine_FromWhLocation { get; set; }

    }
}

