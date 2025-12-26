
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
using WMS.ViewModel.KnifeManagement.KnifeGrindInLineVMs;
using WMS.Util;


namespace WMS.ViewModel.KnifeManagement.KnifeGrindOutLineVMs
{
    public partial class KnifeGrindOutLineKnifeGrindOutDetailListVM2 : BasePagedListVM<KnifeGrindOutLineDetail_View, KnifeGrindOutLineDetailSearcher2>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeGrindOutLineDetail_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindOutLineDetail_View>>{

                this.MakeGridHeader(x => x.SerialNumber).SetTitle("序列号").SetWidth(120),
                this.MakeGridHeader(x => x.BarCode).SetTitle("条码").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeGrindOutLine_FromWhLocation).SetTitle("出库库位").SetWidth(120),
                this.MakeGridHeader(x => x.Status).SetTitle("行状态").SetWidth(80),
            };
        }

        
        public override IOrderedQueryable<KnifeGrindOutLineDetail_View> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeGrindOutId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeGrindOutLineDetail_View>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeGrindOutLine>()
                .Where(x => id == x.KnifeGrindOutId)
                .Select(x => new KnifeGrindOutLineDetail_View
                {
                    SerialNumber = x.Knife.SerialNumber,
                    BarCode = Common.AddBarCodeDialog($"{x.Knife.BarCode}"),
                    KnifeGrindOutLine_FromWhLocation = x.FromWhLocation.Code,
                    Status = x.Status
                })
               .OrderBy(x => x.SerialNumber);

            return query;
        }

    }

    public partial class KnifeGrindOutLineDetailSearcher2 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeGrindOutLine._KnifeGrindOut")]
        public string KnifeGrindOutId { get; set; }
    }
    public class KnifeGrindOutLineDetail_View : KnifeGrindOutLine
    {

        public string SerialNumber { get; set; }
        public string BarCode { get; set; }
        public string KnifeGrindOutLine_FromWhLocation { get; set; }

    }
}

