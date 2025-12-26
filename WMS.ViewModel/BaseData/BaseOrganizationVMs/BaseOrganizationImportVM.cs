
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseOrganizationVMs
{
    public partial class BaseOrganizationTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseOrganization._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.Code);
        [Display(Name = "_Model._BaseOrganization._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.Name);
        [Display(Name = "_Model._BaseOrganization._IsProduction")]
        public ExcelPropety IsProduction_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.IsProduction);
        [Display(Name = "_Model._BaseOrganization._IsSale")]
        public ExcelPropety IsSale_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.IsSale);
        [Display(Name = "_Model._BaseOrganization._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.IsEffective);
        [Display(Name = "_Model._BaseOrganization._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.Memo);
        [Display(Name = "_Model._BaseOrganization._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.SourceSystemId);
        [Display(Name = "_Model._BaseOrganization._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._BaseOrganization._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseOrganization._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseOrganization._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.CreateBy);
        [Display(Name = "_Model._BaseOrganization._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.UpdateBy);
        [Display(Name = "_Model._BaseOrganization._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<BaseOrganization>(x => x.IsValid);

	    protected override void InitVM()
        {
            
        }

    }

    public class BaseOrganizationImportVM : BaseImportVM<BaseOrganizationTemplateVM, BaseOrganization>
    {
            //import

    }

}