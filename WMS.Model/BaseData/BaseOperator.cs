using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model._Admin;
using WMS.Model.BaseData;
using WMS.Model.KnifeManagement;
using WMS.Model.InventoryManagement;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 业务员
    /// </summary>
	[Table("BaseOperator")]

    [Display(Name = "_Model.BaseOperator")]
    public class BaseOperator : BaseExternal
    {
        [Display(Name = "_Model._BaseOperator._JobID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("工号")]
        public string JobID { get; set; }
        [Display(Name = "_Model._BaseOperator._OAAccount")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("OA账号")]
        public string OAAccount { get; set; }
        [Display(Name = "_Model._BaseOperator._IDCard")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("磁卡信息")]
        public string IDCard { get; set; }
        [Display(Name = "_Model._BaseOperator._TempAuthCode")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("临时授权码")]
        public string TempAuthCode { get; set; }
        [Display(Name = "_Model._BaseOperator._TACExpired")]
        [Comment("授权码失效时间")]
        public DateTime? TACExpired { get; set; }
        [Display(Name = "_Model._BaseOperator._Department")]
        [Comment("部门")]
        public BaseDepartment Department { get; set; }
        [Display(Name = "_Model._BaseOperator._Department")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("部门")]
        public Guid? DepartmentId { get; set; }
        [Display(Name = "_Model._BaseOperator._IsEffective")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseOperator._Phone")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("联系方式")]
        public string Phone { get; set; }
        [Display(Name = "_Model._BaseOperator._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._Knife._CurrentCheckOutBy")]
        [InverseProperty("CurrentCheckOutBy")]
        public List<Knife> Knife_CurrentCheckOutBy { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._CheckOutBy")]
        [InverseProperty("CheckOutBy")]
        public List<KnifeCheckOut> KnifeCheckOut_CheckOutBy { get; set; }
        [Display(Name = "_Model._KnifeOperation._OperationBy")]
        [InverseProperty("OperationBy")]
        public List<KnifeOperation> KnifeOperation_OperationBy { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._CheckInBy")]
        [InverseProperty("CheckInBy")]
        public List<KnifeCheckIn> KnifeCheckIn_CheckInBy { get; set; }
        [Display(Name = "_Model._KnifeCombine._CheckOutBy")]
        [InverseProperty("CheckOutBy")]
        public List<KnifeCombine> KnifeCombine_CheckOutBy { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitPerson")]
        [InverseProperty("BenefitPerson")]
        public List<InventoryOtherShip> InventoryOtherShip_BenefitPerson { get; set; }

    }

}
