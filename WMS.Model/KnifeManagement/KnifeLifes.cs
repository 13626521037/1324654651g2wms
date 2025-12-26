using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.Model.KnifeManagement
{
    /// <summary>
    /// 刀具寿命
    /// </summary>
	[Table("KnifeLifess")]

    [Display(Name = "_Model.KnifeLifes")]
    public class KnifeLifes : BasePoco
    {
        [Display(Name = "_Model._KnifeLifes._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._KnifeLifes._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalNum0")]
        [Comment("初始寿命统计数量")]
        public int? TotalNum0 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalDays0")]
        [Comment("初始寿命统计天数")]
        public int? TotalDays0 { get; set; }
        [Display(Name = "_Model._KnifeLifes._AvgLife0")]
        [Comment("初始寿命平均值")]
        [Precision(18,2)]
        public decimal? AvgLife0 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalNum1")]
        [Comment("修磨寿命1统计数量")]
        public int? TotalNum1 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalDays1")]
        [Comment("修磨寿命1统计天数")]
        public int? TotalDays1 { get; set; }
        [Display(Name = "_Model._KnifeLifes._AvgLife1")]
        [Comment("修磨寿命1平均值")]
        [Precision(18,2)]
        public decimal? AvgLife1 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalNum2")]
        [Comment("修磨寿命2统计数量")]
        public int? TotalNum2 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalDays2")]
        [Comment("修磨寿命2统计天数")]
        public int? TotalDays2 { get; set; }
        [Display(Name = "_Model._KnifeLifes._AvgLife2")]
        [Comment("修磨寿命2平均值")]
        [Precision(18,2)]
        public decimal? AvgLife2 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalNum3")]
        [Comment("修磨寿命3统计数量")]
        public int? TotalNum3 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalDays3")]
        [Comment("修磨寿命3统计天数")]
        public int? TotalDays3 { get; set; }
        [Display(Name = "_Model._KnifeLifes._AvgLife3")]
        [Comment("修磨寿命3平均值")]
        [Precision(18,2)]
        public decimal? AvgLife3 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalNum4")]
        [Comment("修磨寿命4统计数量")]
        public int? TotalNum4 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalDays4")]
        [Comment("修磨寿命4统计天数")]
        public int? TotalDays4 { get; set; }
        [Display(Name = "_Model._KnifeLifes._AvgLife4")]
        [Comment("修磨寿命4平均值")]
        [Precision(18,2)]
        public decimal? AvgLife4 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalNum5")]
        [Comment("修磨寿命5统计数量")]
        public int? TotalNum5 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalDays5")]
        [Comment("修磨寿命5统计天数")]
        public int? TotalDays5 { get; set; }
        [Display(Name = "_Model._KnifeLifes._AvgLife5")]
        [Comment("修磨寿命5平均值")]
        [Precision(18,2)]
        public decimal? AvgLife5 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalNum6")]
        [Comment("修磨寿命6统计数量")]
        public int? TotalNum6 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalDays6")]
        [Comment("修磨寿命6统计天数")]
        public int? TotalDays6 { get; set; }
        [Display(Name = "_Model._KnifeLifes._AvgLife6")]
        [Comment("修磨寿命6平均值")]
        [Precision(18,2)]
        public decimal? AvgLife6 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalNum7")]
        [Comment("修磨寿命7统计数量")]
        public int? TotalNum7 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalDays7")]
        [Comment("修磨寿命7统计天数")]
        public int? TotalDays7 { get; set; }
        [Display(Name = "_Model._KnifeLifes._AvgLife7")]
        [Comment("修磨寿命7平均值")]
        [Precision(18,2)]
        public decimal? AvgLife7 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalNum8")]
        [Comment("修磨寿命8统计数量")]
        public int? TotalNum8 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalDays8")]
        [Comment("修磨寿命8统计天数")]
        public int? TotalDays8 { get; set; }
        [Display(Name = "_Model._KnifeLifes._AvgLife8")]
        [Comment("修磨寿命8平均值")]
        [Precision(18,2)]
        public decimal? AvgLife8 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalNum9")]
        [Comment("修磨寿命9统计数量")]
        public int? TotalNum9 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalDays9")]
        [Comment("修磨寿命9统计天数")]
        public int? TotalDays9 { get; set; }
        [Display(Name = "_Model._KnifeLifes._AvgLife9")]
        [Comment("修磨寿命9平均值")]
        [Precision(18,2)]
        public decimal? AvgLife9 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalNum10")]
        [Comment("修磨寿命10统计数量")]
        public int? TotalNum10 { get; set; }
        [Display(Name = "_Model._KnifeLifes._TotalDays10")]
        [Comment("修磨寿命10统计天数")]
        public int? TotalDays10 { get; set; }
        [Display(Name = "_Model._KnifeLifes._AvgLife10")]
        [Comment("修磨寿命10平均值")]
        [Precision(18,2)]
        public decimal? AvgLife10 { get; set; }
        [Display(Name = "_Model._KnifeLifes._IsUpdating")]
        [Comment("自动更新")]
        public bool? IsUpdating { get; set; }

	}

}
