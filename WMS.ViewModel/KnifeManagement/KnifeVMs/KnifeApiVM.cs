using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model.BaseData;


namespace WMS.ViewModel.KnifeManagement.KnifeVMs
{
    public partial class KnifeApiVM : BaseCRUDVM<Knife>
    {

        public KnifeApiVM()
        {
            SetInclude(x => x.WhLocation);
            SetInclude(x => x.ItemMaster);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
