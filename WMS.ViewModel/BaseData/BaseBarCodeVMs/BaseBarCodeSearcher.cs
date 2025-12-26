
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
namespace WMS.ViewModel.BaseData.BaseBarCodeVMs
{
    public partial class BaseBarCodeSearcher : BaseSearcher
    {
        
        public List<string> BaseDataBaseBarCodeSTempSelected { get; set; }
        [Display(Name = "_Model._BaseBarCode._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._BaseBarCode._Code")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseBarCode._Item")]
        public string ItemCode { get; set; }
        public List<ComboSelectListItem> AllItems { get; set; }
        [Display(Name = "_Model._BaseBarCode._Qty")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._BaseBarCode._CustomerCode")]
        public string CustomerCode { get; set; }
        [Display(Name = "_Model._BaseBarCode._CustomerName")]
        public string CustomerName { get; set; }
        [Display(Name = "_Model._BaseBarCode._CustomerNameFirstLetter")]
        public string CustomerNameFirstLetter { get; set; }
        [Display(Name = "_Model._BaseBarCode._Seiban")]
        public string Seiban { get; set; }
        [Display(Name = "番号随机码")]
        public string SeibanRandom { get; set; }
        [Display(Name = "批号")]
        public string BatchNumber { get; set; }
        [Display(Name = "序列号")]
        public string Sn { get; set; }
        [Display(Name = "_Model._BaseBarCode._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._BaseBarCode._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._BaseBarCode._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._BaseBarCode._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}