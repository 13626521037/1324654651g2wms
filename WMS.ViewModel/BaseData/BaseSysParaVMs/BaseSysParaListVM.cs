using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.BaseData;


namespace WMS.ViewModel.BaseData.BaseSysParaVMs
{
    public partial class BaseSysParaListVM : BasePagedListVM<BaseSysPara_View, BaseSysParaSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("BaseSysPara", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"BaseData", dialogWidth: 800),
                this.MakeStandardAction("BaseSysPara", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "BaseData", dialogWidth: 800),
                //this.MakeStandardAction("BaseSysPara", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "BaseData", dialogWidth: 800),
                this.MakeStandardAction("BaseSysPara", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "BaseData", dialogWidth: 800),
                //this.MakeStandardAction("BaseSysPara", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "BaseData", dialogWidth: 800),
                //this.MakeStandardAction("BaseSysPara", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "BaseData", dialogWidth: 800),
                //this.MakeStandardAction("BaseSysPara", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "BaseData", dialogWidth: 800),
                this.MakeStandardAction("BaseSysPara", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "BaseData"),
            };
        }


        protected override IEnumerable<IGridColumn<BaseSysPara_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseSysPara_View>>{
                this.MakeGridHeader(x => x.Code),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Value),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BaseSysPara_View> GetSearchQuery()
        {
            var query = DC.Set<BaseSysPara>()
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckContain(Searcher.Memo, x=>x.Memo)
                .Select(x => new BaseSysPara_View
                {
				    ID = x.ID,
                    Code = x.Code,
                    Name = x.Name,
                    Value = x.Value,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BaseSysPara_View : BaseSysPara{

    }
}
