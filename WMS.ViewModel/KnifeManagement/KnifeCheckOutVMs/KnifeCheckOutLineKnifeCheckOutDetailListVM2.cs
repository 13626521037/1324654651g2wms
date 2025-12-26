
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
using WMS.ViewModel.KnifeManagement.KnifeCheckOutLineVMs;
using WMS.Util;


namespace WMS.ViewModel.KnifeManagement.KnifeCheckOutLineVMs
{
    public partial class KnifeCheckOutLineKnifeCheckOutDetailListVM2 : BasePagedListVM<KnifeCheckOutLineDetail_View, KnifeCheckOutLineDetailSearcher2>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                  this.MakeAction("KnifeCheckOut","Print", "打印", "打印", GridActionParameterTypesEnum.NoId, "KnifeManagement", dialogHeight: 800, dialogWidth: 1000).SetShowInRow(false).SetHideOnToolBar(false).SetQueryString($"ID={Searcher.KnifeCheckOutId}"),
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeCheckOutLineDetail_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeCheckOutLineDetail_View>>{


                this.MakeGridHeader(x => x.SerialNumber).SetTitle("序列号").SetWidth(120),
                this.MakeGridHeader(x => x.BarCode).SetTitle("条码").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeCheckOutLine_FromWhLocation).SetTitle("领用库位").SetWidth(150),
            };
        }

        
        public override IOrderedQueryable<KnifeCheckOutLineDetail_View> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeCheckOutId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeCheckOutLineDetail_View>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeCheckOutLine>()
                .Where(x => id == x.KnifeCheckOutId)
                 .Select(x => new KnifeCheckOutLineDetail_View
                 {
                     SerialNumber = x.Knife.SerialNumber,
                     BarCode = Common.AddBarCodeDialog($"{x.Knife.BarCode}"),
                     KnifeCheckOutLine_FromWhLocation = x.FromWhLocation.Name,
                 })
                .OrderBy(x => x.SerialNumber);
            return query;
        }

    }

    public partial class KnifeCheckOutLineDetailSearcher2 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeCheckOutLine._KnifeCheckOut")]
        public string KnifeCheckOutId { get; set; }
    }
    public class KnifeCheckOutLineDetail_View : KnifeCheckOutLine
    {

        public string SerialNumber { get; set; }
        public string BarCode { get; set; }
        public string KnifeCheckOutLine_FromWhLocation { get; set; }

    }
}

