
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model;
using WMS.Model.KnifeManagement;
using WMS.Util;


namespace WMS.ViewModel.KnifeManagement.KnifeGrindOutLineVMs
{
    public partial class KnifeGrindOutLineBatchCreateListVM : BasePagedListVM<KnifeGrindOutLineBatchCreate_View, KnifeGrindOutLineBatchCreateSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeGrindOutLineBatchCreate_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindOutLineBatchCreate_View>>{

                /*this.MakeGridHeader(x => x.SerialNumber).SetTitle("序列号").SetWidth(120),
                this.MakeGridHeader(x => x.BarCode).SetTitle("条码").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeGrindOutLine_FromWhLocation).SetTitle("出库库位").SetWidth(250),
                this.MakeGridHeader(x => x.Status).SetTitle("行状态").SetWidth(250),*/
            };
        }

        
        public override IOrderedQueryable<KnifeGrindOutLineBatchCreate_View> GetSearchQuery()
        {
                
            /*var id = (Guid?)Searcher.KnifeGrindOutId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeGrindOutLineBatchCreate_View>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeGrindOutLine>()
                .Where(x => id == x.KnifeGrindOutId)
                .Select(x => new KnifeGrindOutLineDetail_View
                {
                    SerialNumber = x.Knife.SerialNumber,
                    BarCode = Common.AddBarCodeDialog($"{x.Knife.BarCode}"),
                    KnifeGrindOutLine_FromWhLocation = x.FromWhLocation.Code,
                    Status = x.Status
                })
               .OrderBy(x => x.SerialNumber);*/
            var a = new List<KnifeGrindOutLineBatchCreate_View>();
            return  a.AsQueryable().OrderBy(x => x);
        }

    }

    public partial class KnifeGrindOutLineBatchCreateSearcher : BaseSearcher
    {
        
        
    }
    public class KnifeGrindOutLineBatchCreate_View : KnifeGrindOutLine
    {


    }
}

