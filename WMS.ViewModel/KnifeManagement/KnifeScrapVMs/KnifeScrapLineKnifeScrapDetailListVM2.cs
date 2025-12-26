
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


namespace WMS.ViewModel.KnifeManagement.KnifeScrapLineVMs
{
    public partial class KnifeScrapLineKnifeScrapDetailListVM2 : BasePagedListVM<KnifeScrapLineDetail_View, KnifeScrapLineDetailSearcher2>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeScrapLineDetail_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeScrapLineDetail_View>>{

                this.MakeGridHeader(x => x.SerialNumber).SetTitle("序列号"),
                this.MakeGridHeader(x => x.BarCode).SetTitle("条码").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeScrapLineDetail_IsAccident).SetTitle("意外"),
                this.MakeGridHeader(x => x.KnifeScrapLine_FromWhLocation).SetTitle("报废前库位"),
                this.MakeGridHeader(x => x.KnifeScrapLine_ToWhLocation).SetTitle("报废后库位"),
            };
        }

        
        public override IOrderedQueryable<KnifeScrapLineDetail_View> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeScrapId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeScrapLineDetail_View>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeScrapLine>()
                .Where(x => id == x.KnifeScrapId)
                 .Select(x => new KnifeScrapLineDetail_View
                 {
                     SerialNumber = x.Knife.SerialNumber,
                     BarCode = Common.AddBarCodeDialog($"{x.Knife.BarCode}"),
                     KnifeScrapLineDetail_IsAccident = (bool)x.IsAccident,
                     KnifeScrapLine_FromWhLocation = x.FromWhLocation.Code,
                     KnifeScrapLine_ToWhLocation = x.ToWhLocation.Code,
                 })
                .OrderBy(x => x.SerialNumber);
            return query;
        }

    }

    public partial class KnifeScrapLineDetailSearcher2 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeScrapLine._KnifeScrap")]
        public string KnifeScrapId { get; set; }
    }
    public class KnifeScrapLineDetail_View : KnifeScrapLine
    {

        public string SerialNumber { get; set; }
        public string BarCode { get; set; }
        public string KnifeScrapLine_FromWhLocation { get; set; }
        public string KnifeScrapLine_ToWhLocation { get; set; }
        public bool KnifeScrapLineDetail_IsAccident { get; set; }

    }
}

