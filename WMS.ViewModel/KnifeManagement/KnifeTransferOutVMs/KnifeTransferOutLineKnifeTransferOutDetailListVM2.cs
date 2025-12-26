
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


namespace WMS.ViewModel.KnifeManagement.KnifeTransferOutLineVMs
{
    public partial class KnifeTransferOutLineKnifeTransferOutDetailListVM2 : BasePagedListVM<KnifeTransferOutLineDetail_View, KnifeTransferOutLineDetailSearcher2>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeTransferOutLineDetail_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeTransferOutLineDetail_View>>{

                this.MakeGridHeader(x => x.SerialNumber).SetTitle("序列号"),
                this.MakeGridHeader(x => x.BarCode).SetTitle("条码").SetWidth(250),
                this.MakeGridHeader(x => x.LineStatus).SetTitle("行状态"),
                this.MakeGridHeader(x => x.KnifeTransferOutLine_FromWhLocation).SetTitle("调出库位"),

            };
        }

        
        public override IOrderedQueryable<KnifeTransferOutLineDetail_View> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeTransferOutId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeTransferOutLineDetail_View>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeTransferOutLine>()
                .Where(x => id == x.KnifeTransferOutId)
                 .Select(x => new KnifeTransferOutLineDetail_View
                 {
                     SerialNumber = x.Knife.SerialNumber,
                     LineStatus = (KnifeOrderStatusEnum)x.Status,
                     BarCode = Common.AddBarCodeDialog($"{x.Knife.BarCode}"),
                     KnifeTransferOutLine_FromWhLocation = x.FromWhLocation.Code,
                 })
                .OrderBy(x => x.SerialNumber);

            return query;
        }

    }

    public partial class KnifeTransferOutLineDetailSearcher2 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeTransferOutLine._KnifeTransferOut")]
        public string KnifeTransferOutId { get; set; }
    }
    public class KnifeTransferOutLineDetail_View : KnifeTransferOutLine
    {

        public string SerialNumber { get; set; }
        public string BarCode { get; set; }
        public string KnifeTransferOutLine_FromWhLocation { get; set; }
        public KnifeOrderStatusEnum LineStatus { get; set; }
    }
}

