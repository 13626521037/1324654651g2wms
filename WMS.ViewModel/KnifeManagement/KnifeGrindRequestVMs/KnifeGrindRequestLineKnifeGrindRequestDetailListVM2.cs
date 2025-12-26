
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
using WMS.ViewModel.KnifeManagement.KnifeGrindOutLineVMs;
using WMS.Util;


namespace WMS.ViewModel.KnifeManagement.KnifeGrindRequestLineVMs
{
    public partial class KnifeGrindRequestLineKnifeGrindRequestDetailListVM2 : BasePagedListVM<KnifeGrindRequestLineDetail_View, KnifeGrindRequestLineDetailSearcher2>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeGrindRequestLineDetail_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindRequestLineDetail_View>>{

                this.MakeGridHeader(x => x.SerialNumber).SetTitle("序列号").SetWidth(120),
                this.MakeGridHeader(x => x.BarCode).SetTitle("条码").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeGrindRequestLine_FromWhLocation).SetTitle("库位").SetWidth(100),
                this.MakeGridHeader(x => x.Status).SetTitle("行状态").SetWidth(60),
                this.MakeGridHeader(x => x.U9PODocNo).SetTitle("采购单号").SetWidth(120),
                this.MakeGridHeader(x => x.U9PODocLineNo).SetTitle("采购行号").SetWidth(80),
            };
        }

        
        public override IOrderedQueryable<KnifeGrindRequestLineDetail_View> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeGrindRequestId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeGrindRequestLineDetail_View>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeGrindRequestLine>()
                .Where(x => id == x.KnifeGrindRequestId)
                 .Select(x => new KnifeGrindRequestLineDetail_View
                 {
                     ID=x.ID,
                     SerialNumber = x.Knife.SerialNumber,
                     BarCode = Common.AddBarCodeDialog($"{x.Knife.BarCode}"),
                     KnifeGrindRequestLine_FromWhLocation = x.FromWhLocation.Code,
                     Status =x.Status,
                     U9PODocNo = Common.AddBarCodeDialog(x.U9PODocNo),
                     U9PODocLineNo = x.U9PODocLineNo,
                 })
               .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class KnifeGrindRequestLineDetailSearcher2 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeGrindRequestLine._KnifeGrindRequest")]
        public string KnifeGrindRequestId { get; set; }
    }
    public class KnifeGrindRequestLineDetail_View : KnifeGrindRequestLine
    {

        public string SerialNumber { get; set; }
        public string BarCode { get; set; }
        public string KnifeGrindRequestLine_FromWhLocation { get; set; }

    }
}

