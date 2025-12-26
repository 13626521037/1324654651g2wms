
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

using WMS.ViewModel.KnifeManagement.KnifeCombineVMs;
using Microsoft.EntityFrameworkCore;
using WMS.Util;


namespace WMS.ViewModel.KnifeManagement.KnifeCombineLineVMs
{
    public partial class KnifeCombineLineKnifeCombineDetailListVM : BasePagedListVM<KnifeCombineLineDetail_View, KnifeCombineLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeCombineLineDetail_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeCombineLineDetail_View>>{
                
                this.MakeGridHeader(x => x.KnifeCombineLine_SerialNumber).SetTitle("序列号").SetWidth(120),
                this.MakeGridHeader(x => x.BarCode).SetTitle("条码").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeCombineLine_KnifeStatus).SetTitle("刀具状态").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeCombineLine_FromWhLocation).SetTitle("领用库位").SetWidth(250),
                this.MakeGridHeader(x => x.KnifeCombineLine_ToWhLocation).SetTitle("归还库位").SetWidth(250),
            };
        }

        
        public override IOrderedQueryable<KnifeCombineLineDetail_View> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeCombineId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeCombineLineDetail_View>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeCombineLine>()
                .Include(x=>x.Knife).ThenInclude(x=>x.ItemMaster)
                .Where(x => id == x.KnifeCombineId)
                .Select(x => new KnifeCombineLineDetail_View
                {
                    
                    KnifeCombineLine_SerialNumber = x.Knife.SerialNumber,
                    BarCode = Common.AddBarCodeDialog($"{x.Knife.BarCode}"),
                    KnifeCombineLine_KnifeStatus = x.Knife.Status,
                    KnifeCombineLine_FromWhLocation = x.FromWhLocation.Code,
                    KnifeCombineLine_ToWhLocation = x.ToWhLocation.Code,
                })
                .OrderBy(x => x.KnifeCombineLine_SerialNumber)
                ;
            return query;
        }

    }

    public partial class KnifeCombineLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeCombineLine._KnifeCombine")]
        public string KnifeCombineId { get; set; }

    }
    public class KnifeCombineLineDetail_View : KnifeCombineLine
    {

        public string KnifeCombineLine_SerialNumber { get; set; }
        public KnifeStatusEnum? KnifeCombineLine_KnifeStatus { get; set; }
        public string KnifeCombineLine_FromWhLocation { get; set; }
        public string KnifeCombineLine_ToWhLocation { get; set; }
        public string BarCode { get; set; }

    }
}

