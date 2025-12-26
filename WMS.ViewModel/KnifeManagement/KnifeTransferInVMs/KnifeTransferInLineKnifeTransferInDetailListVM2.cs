
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
using WMS.Model.BaseData;
using WMS.ViewModel.KnifeManagement.KnifeGrindRequestLineVMs;
using WMS.Util;


namespace WMS.ViewModel.KnifeManagement.KnifeTransferInLineVMs
{
    public partial class KnifeTransferInLineKnifeTransferInDetailListVM2 : BasePagedListVM<KnifeTransferInLineDetail_View, KnifeTransferInLineDetailSearcher2>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeTransferInLineDetail_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeTransferInLineDetail_View>>{
                
                this.MakeGridHeader(x => x.SerialNumber).SetTitle("序列号"),
                this.MakeGridHeader(x => x.BarCode).SetTitle("条码").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeTransferInLine_FromWhLocationCode).SetTitle("调出库位"),
                this.MakeGridHeader(x => x.KnifeTransferInLine_ToWhLocationCode).SetTitle("调入库位"),

            };
        }

        
        public override IOrderedQueryable<KnifeTransferInLineDetail_View> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeTransferInId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeTransferInLineDetail_View>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeTransferInLine>()
                .Where(x => id == x.KnifeTransferInId)
                 .Select(x => new KnifeTransferInLineDetail_View
                 {
                     SerialNumber = x.Knife.SerialNumber,
                     KnifeTransferInLine_FromWhLocationCode = x.FromWhLocation.Code,
                     KnifeTransferInLine_ToWhLocationCode = x.ToWhLocation.Code,
                     BarCode = Common.AddBarCodeDialog($"{x.Knife.BarCode}"),
                 })
                .OrderBy(x => x.SerialNumber);

            return query;
        }

    }

    public partial class KnifeTransferInLineDetailSearcher2 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeTransferInLine._KnifeTransferIn")]
        public string KnifeTransferInId { get; set; }
    }
    public class KnifeTransferInLineDetail_View : KnifeTransferInLine
    {

        public string SerialNumber { get; set; }
        public string BarCode { get; set; }
        public string KnifeTransferInLine_FromWhLocationCode { get; set; }
        public string KnifeTransferInLine_ToWhLocationCode { get; set; }
    }
}

