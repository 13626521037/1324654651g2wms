
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeCheckInLineVMs
{
    public partial class KnifeCheckInLineTemplateVM : BaseTemplateVM
    {
        
	    protected override void InitVM()
        {
            
        }

    }

    public class KnifeCheckInLineImportVM : BaseImportVM<KnifeCheckInLineTemplateVM, KnifeCheckInLine>
    {
            //import

    }

}