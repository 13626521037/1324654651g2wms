
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSequenceRecordsDetailVMs
{
    public partial class BaseSequenceRecordsDetailTemplateVM : BaseTemplateVM
    {
        
	    protected override void InitVM()
        {
            
        }

    }

    public class BaseSequenceRecordsDetailImportVM : BaseImportVM<BaseSequenceRecordsDetailTemplateVM, BaseSequenceRecordsDetail>
    {
            //import

    }

}