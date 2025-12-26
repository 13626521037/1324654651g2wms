
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeTransferOutLineVMs
{
    public partial class KnifeTransferOutLineTemplateVM : BaseTemplateVM
    {
        
	    protected override void InitVM()
        {
            
        }

    }

    public class KnifeTransferOutLineImportVM : BaseImportVM<KnifeTransferOutLineTemplateVM, KnifeTransferOutLine>
    {
            //import

    }

}