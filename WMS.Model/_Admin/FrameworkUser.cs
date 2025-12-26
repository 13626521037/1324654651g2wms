using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WalkingTec.Mvvm.Core;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.KnifeManagement;
using WMS.Model.PurchaseManagement;

namespace WalkingTec.Mvvm.Core
{
    /// <summary>
    /// 用户
    /// </summary>
	[Table("FrameworkUsers")]
    [SoftKey(nameof(FrameworkUser.ITCode))]
    [Display(Name = "_Model.FrameworkUser")]
    public class FrameworkUser : FrameworkUserBase
    {
        [Display(Name = "_Model._FrameworkUser._Email")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("邮箱")]
        [RegularExpression("^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "Validate.{0}formaterror")]
        public string Email { get; set; }
        [Display(Name = "_Model._FrameworkUser._Gender")]
        [Comment("性别")]
        public GenderEnum? Gender { get; set; }
        [Display(Name = "_Model._FrameworkUser._CellPhone")]
        [Comment("手机")]
        [RegularExpression("^[1][3456789][0-9]{9}$", ErrorMessage = "Validate.{0}formaterror")]
        public string CellPhone { get; set; }
        [Display(Name = "_Model._FrameworkUser._HomePhone")]
        [StringLength(30, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("座机")]
        [RegularExpression("^[-0-9\\s]{8,30}$", ErrorMessage = "Validate.{0}formaterror")]
        public string HomePhone { get; set; }
        [Display(Name = "_Model._FrameworkUser._Address")]
        [StringLength(200, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("地址")]
        public string Address { get; set; }
        [Display(Name = "_Model._FrameworkUser._ZipCode")]
        [Comment("邮编")]
        [RegularExpression("^[0-9]{6,6}$", ErrorMessage = "Validate.{0}formaterror")]
        public string ZipCode { get; set; }
        [Display(Name = "最后登录仓库")]  // 因为前端显示不下，所以此处称为仓库（存储地点）
        public Guid? WarehouseId { get; set; }
        [Display(Name = "_Model._BaseUserWhRelation._User")]
        [InverseProperty("User")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.BaseData.BaseUserWhRelation.UserId))]
        public List<BaseUserWhRelation> BaseUserWhRelation_User { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._Inspector")]
        [InverseProperty("Inspector")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.PurchaseManagement.PurchaseReceivementLine.InspectorId))]
        public List<PurchaseReceivementLine> PurchaseReceivementLine_Inspector { get; set; }
        [Display(Name = "_Model._Knife._HandledBy")]
        [InverseProperty("HandledBy")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.KnifeManagement.Knife.HandledById))]
        public List<Knife> Knife_HandledBy { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._HandledBy")]
        [InverseProperty("HandledBy")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.KnifeManagement.KnifeCheckOut.HandledById))]
        public List<KnifeCheckOut> KnifeCheckOut_HandledBy { get; set; }
        [Display(Name = "_Model._KnifeOperation._HandledBy")]
        [InverseProperty("HandledBy")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.KnifeManagement.KnifeOperation.HandledById))]
        public List<KnifeOperation> KnifeOperation_HandledBy { get; set; }
        [Display(Name = "_Model._KnifeScrap._HandledBy")]
        [InverseProperty("HandledBy")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.KnifeManagement.KnifeScrap.HandledById))]
        public List<KnifeScrap> KnifeScrap_HandledBy { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._HandledBy")]
        [InverseProperty("HandledBy")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.KnifeManagement.KnifeCheckIn.HandledById))]
        public List<KnifeCheckIn> KnifeCheckIn_HandledBy { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._HandledBy")]
        [InverseProperty("HandledBy")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.KnifeManagement.KnifeGrindRequest.HandledById))]
        public List<KnifeGrindRequest> KnifeGrindRequest_HandledBy { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._HandledBy")]
        [InverseProperty("HandledBy")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.KnifeManagement.KnifeGrindOut.HandledById))]
        public List<KnifeGrindOut> KnifeGrindOut_HandledBy { get; set; }
        [Display(Name = "_Model._KnifeGrindIn._HandledBy")]
        [InverseProperty("HandledBy")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.KnifeManagement.KnifeGrindIn.HandledById))]
        public List<KnifeGrindIn> KnifeGrindIn_HandledBy { get; set; }
        [Display(Name = "_Model._KnifeCombine._HandledBy")]
        [InverseProperty("HandledBy")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.KnifeManagement.KnifeCombine.HandledById))]
        public List<KnifeCombine> KnifeCombine_HandledBy { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._HandledBy")]
        [InverseProperty("HandledBy")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.KnifeManagement.KnifeTransferIn.HandledById))]
        public List<KnifeTransferIn> KnifeTransferIn_HandledBy { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._HandledBy")]
        [InverseProperty("HandledBy")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.KnifeManagement.KnifeTransferOut.HandledById))]
        public List<KnifeTransferOut> KnifeTransferOut_HandledBy { get; set; }
        [Display(Name = "_Model._BaseShortcut._User")]
        [InverseProperty("User")]
        [NotMapped]
        [SoftFK(nameof(WMS.Model.BaseData.BaseShortcut.UserId))]
        public List<BaseShortcut> BaseShortcut_User { get; set; }

    }

}
