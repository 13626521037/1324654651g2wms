
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
using WMS.ViewModel.KnifeManagement.KnifeCombineLineVMs;
using WMS.Util;


namespace WMS.ViewModel.KnifeManagement.KnifeGrindInLineVMs
{
    public partial class KnifeGrindInLineKnifeGrindInDetailListVM2 : BasePagedListVM<KnifeGrindInLineDetail_View, KnifeGrindInLineDetailSearcher2>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeGrindInLineDetail_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindInLineDetail_View>>{
                
                this.MakeGridHeader(x => x.SerialNumber).SetTitle("序列号").SetWidth(120),
                this.MakeGridHeader(x => x.BarCode).SetTitle("条码").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeGrindInLine_FromWhLocation).SetTitle("出库库位").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeGrindInLine_ToWhLocation).SetTitle("入库库位").SetWidth(250),

            };
        }

        
        public override IOrderedQueryable<KnifeGrindInLineDetail_View> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeGrindInId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeGrindInLineDetail_View>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeGrindInLine>()
                .Where(x => id == x.KnifeGrindInId)
                .Select(x => new KnifeGrindInLineDetail_View
                {
                     SerialNumber = x.Knife.SerialNumber,
                     BarCode = Common.AddBarCodeDialog($"{x.Knife.BarCode}"),
                     KnifeGrindInLine_FromWhLocation = x.FromWhLocation.Code,
                     KnifeGrindInLine_ToWhLocation = x.ToWhLocation.Code,
                })
               .OrderBy(x => x.SerialNumber);
            return query;
        }

    }

    public partial class KnifeGrindInLineDetailSearcher2 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeGrindInLine._KnifeGrindIn")]
        public string KnifeGrindInId { get; set; }
    }
    public class KnifeGrindInLineDetail_View : KnifeGrindInLine
    {

        public string SerialNumber { get; set; }
        public string BarCode { get; set; }
        public string KnifeGrindInLine_FromWhLocation { get; set; }
        public string KnifeGrindInLine_ToWhLocation { get; set; }

    }
}

