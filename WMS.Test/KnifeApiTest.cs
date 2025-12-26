using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.KnifeManagement.KnifeVMs;
using WMS.Model.KnifeManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class KnifeApiTest
    {
        private KnifeApiController _controller;
        private string _seed;

        public KnifeApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<KnifeApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new KnifeApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            KnifeApiVM vm = _controller.Wtm.CreateVM<KnifeApiVM>();
            Knife v = new Knife();
            
            v.CreatedDate = DateTime.Parse("2025-07-09 16:06:51");
            v.SerialNumber = "Yh2viFN0LMVMHpcVcIdsAuC0KXT8C52iVVevf0wPFc5v31Lh";
            v.Status = WMS.Model.KnifeStatusEnum.CheckOut;
            v.CurrentCheckOutBy = "AO7LCqiikpk07";
            v.HandledById = AddFrameworkUser();
            v.LastOperationDate = DateTime.Parse("2025-07-11 16:06:51");
            v.WhLocationId = AddBaseWhLocation();
            v.GrindCount = 4;
            v.InitialLife = 73;
            v.CurrentLife = 19;
            v.TotalUsedDays = 14;
            v.RemainingUsedDays = 5;
            v.InitialAmount = 92;
            v.CurrentAmount = 26;
            v.TotalUsedAmount = 33;
            v.RemainingUsedAmount = 35;
            v.ResidualTranferAmount = 20;
            v.ItemMasterId = AddBaseItemMaster();
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Knife>().Find(v.ID);
                
                Assert.AreEqual(data.CreatedDate, DateTime.Parse("2025-07-09 16:06:51"));
                Assert.AreEqual(data.SerialNumber, "Yh2viFN0LMVMHpcVcIdsAuC0KXT8C52iVVevf0wPFc5v31Lh");
                Assert.AreEqual(data.Status, WMS.Model.KnifeStatusEnum.CheckOut);
                Assert.AreEqual(data.CurrentCheckOutBy, "AO7LCqiikpk07");
                Assert.AreEqual(data.LastOperationDate, DateTime.Parse("2025-07-11 16:06:51"));
                Assert.AreEqual(data.GrindCount, 4);
                Assert.AreEqual(data.InitialLife, 73);
                Assert.AreEqual(data.CurrentLife, 19);
                Assert.AreEqual(data.TotalUsedDays, 14);
                Assert.AreEqual(data.RemainingUsedDays, 5);
                Assert.AreEqual(data.InitialAmount, 92);
                Assert.AreEqual(data.CurrentAmount, 26);
                Assert.AreEqual(data.TotalUsedAmount, 33);
                Assert.AreEqual(data.RemainingUsedAmount, 35);
                Assert.AreEqual(data.ResidualTranferAmount, 20);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            Knife v = new Knife();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.CreatedDate = DateTime.Parse("2025-07-09 16:06:51");
                v.SerialNumber = "Yh2viFN0LMVMHpcVcIdsAuC0KXT8C52iVVevf0wPFc5v31Lh";
                v.Status = WMS.Model.KnifeStatusEnum.CheckOut;
                v.CurrentCheckOutBy = "AO7LCqiikpk07";
                v.HandledById = AddFrameworkUser();
                v.LastOperationDate = DateTime.Parse("2025-07-11 16:06:51");
                v.WhLocationId = AddBaseWhLocation();
                v.GrindCount = 4;
                v.InitialLife = 73;
                v.CurrentLife = 19;
                v.TotalUsedDays = 14;
                v.RemainingUsedDays = 5;
                v.InitialAmount = 92;
                v.CurrentAmount = 26;
                v.TotalUsedAmount = 33;
                v.RemainingUsedAmount = 35;
                v.ResidualTranferAmount = 20;
                v.ItemMasterId = AddBaseItemMaster();
                context.Set<Knife>().Add(v);
                context.SaveChanges();
            }

            KnifeApiVM vm = _controller.Wtm.CreateVM<KnifeApiVM>();
            var oldID = v.ID;
            v = new Knife();
            v.ID = oldID;
       		
            v.CreatedDate = DateTime.Parse("2025-02-27 16:06:51");
            v.SerialNumber = "H4827MlQBEyNqZ41LCTfLOJrRsqQaeC0I3pM";
            v.Status = WMS.Model.KnifeStatusEnum.Scrapped;
            v.CurrentCheckOutBy = "mPMrBZkisxjZRTT94A";
            v.LastOperationDate = DateTime.Parse("2026-01-29 16:06:51");
            v.GrindCount = 55;
            v.InitialLife = 66;
            v.CurrentLife = 63;
            v.TotalUsedDays = 6;
            v.RemainingUsedDays = 86;
            v.InitialAmount = 38;
            v.CurrentAmount = 70;
            v.TotalUsedAmount = 79;
            v.RemainingUsedAmount = 52;
            v.ResidualTranferAmount = 13;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.CreatedDate", "");
            vm.FC.Add("Entity.KnifeNo", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.CurrentCheckOutBy", "");
            vm.FC.Add("Entity.HandledById", "");
            vm.FC.Add("Entity.LastOperationDate", "");
            vm.FC.Add("Entity.WhLocationId", "");
            vm.FC.Add("Entity.GrindCount", "");
            vm.FC.Add("Entity.InitialLife", "");
            vm.FC.Add("Entity.CurrentLife", "");
            vm.FC.Add("Entity.TotalUsedDays", "");
            vm.FC.Add("Entity.RemainingUsedDays", "");
            vm.FC.Add("Entity.InitialAmount", "");
            vm.FC.Add("Entity.CurrentAmount", "");
            vm.FC.Add("Entity.TotalUsedAmount", "");
            vm.FC.Add("Entity.RemainingUsedAmount", "");
            vm.FC.Add("Entity.ItemMasterId", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Knife>().Find(v.ID);
 				
                Assert.AreEqual(data.CreatedDate, DateTime.Parse("2025-02-27 16:06:51"));
                Assert.AreEqual(data.SerialNumber, "H4827MlQBEyNqZ41LCTfLOJrRsqQaeC0I3pM");
                Assert.AreEqual(data.Status, WMS.Model.KnifeStatusEnum.Scrapped);
                Assert.AreEqual(data.CurrentCheckOutBy, "mPMrBZkisxjZRTT94A");
                Assert.AreEqual(data.LastOperationDate, DateTime.Parse("2026-01-29 16:06:51"));
                Assert.AreEqual(data.GrindCount, 55);
                Assert.AreEqual(data.InitialLife, 66);
                Assert.AreEqual(data.CurrentLife, 63);
                Assert.AreEqual(data.TotalUsedDays, 6);
                Assert.AreEqual(data.RemainingUsedDays, 86);
                Assert.AreEqual(data.InitialAmount, 38);
                Assert.AreEqual(data.CurrentAmount, 70);
                Assert.AreEqual(data.TotalUsedAmount, 79);
                Assert.AreEqual(data.RemainingUsedAmount, 52);
                Assert.AreEqual(data.ResidualTranferAmount, 13);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            Knife v = new Knife();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.CreatedDate = DateTime.Parse("2025-07-09 16:06:51");
                v.SerialNumber = "Yh2viFN0LMVMHpcVcIdsAuC0KXT8C52iVVevf0wPFc5v31Lh";
                v.Status = WMS.Model.KnifeStatusEnum.CheckOut;
                v.CurrentCheckOutBy = "AO7LCqiikpk07";
                v.HandledById = AddFrameworkUser();
                v.LastOperationDate = DateTime.Parse("2025-07-11 16:06:51");
                v.WhLocationId = AddBaseWhLocation();
                v.GrindCount = 4;
                v.InitialLife = 73;
                v.CurrentLife = 19;
                v.TotalUsedDays = 14;
                v.RemainingUsedDays = 5;
                v.InitialAmount = 92;
                v.CurrentAmount = 26;
                v.TotalUsedAmount = 33;
                v.RemainingUsedAmount = 35;
                v.ResidualTranferAmount = 20;
                v.ItemMasterId = AddBaseItemMaster();
                context.Set<Knife>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Knife v1 = new Knife();
            Knife v2 = new Knife();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.CreatedDate = DateTime.Parse("2025-07-09 16:06:51");
                v1.SerialNumber = "Yh2viFN0LMVMHpcVcIdsAuC0KXT8C52iVVevf0wPFc5v31Lh";
                v1.Status = WMS.Model.KnifeStatusEnum.CheckOut;
                v1.CurrentCheckOutBy = "AO7LCqiikpk07";
                v1.HandledById = AddFrameworkUser();
                v1.LastOperationDate = DateTime.Parse("2025-07-11 16:06:51");
                v1.WhLocationId = AddBaseWhLocation();
                v1.GrindCount = 4;
                v1.InitialLife = 73;
                v1.CurrentLife = 19;
                v1.TotalUsedDays = 14;
                v1.RemainingUsedDays = 5;
                v1.InitialAmount = 92;
                v1.CurrentAmount = 26;
                v1.TotalUsedAmount = 33;
                v1.RemainingUsedAmount = 35;
                v1.ResidualTranferAmount = 20;
                v1.ItemMasterId = AddBaseItemMaster();
                v2.CreatedDate = DateTime.Parse("2025-02-27 16:06:51");
                v2.SerialNumber = "H4827MlQBEyNqZ41LCTfLOJrRsqQaeC0I3pM";
                v2.Status = WMS.Model.KnifeStatusEnum.Scrapped;
                v2.CurrentCheckOutBy = "mPMrBZkisxjZRTT94A";
                v2.HandledById = v1.HandledById; 
                v2.LastOperationDate = DateTime.Parse("2026-01-29 16:06:51");
                v2.WhLocationId = v1.WhLocationId; 
                v2.GrindCount = 55;
                v2.InitialLife = 66;
                v2.CurrentLife = 63;
                v2.TotalUsedDays = 6;
                v2.RemainingUsedDays = 86;
                v2.InitialAmount = 38;
                v2.CurrentAmount = 70;
                v2.TotalUsedAmount = 79;
                v2.RemainingUsedAmount = 52;
                v2.ResidualTranferAmount = 13;
                v2.ItemMasterId = v1.ItemMasterId; 
                context.Set<Knife>().Add(v1);
                context.Set<Knife>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Knife>().Find(v1.ID);
                var data2 = context.Set<Knife>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "4";
                v.FileExt = "ayqw";
                v.Path = "gMbsaB";
                v.Length = 23;
                v.UploadTime = DateTime.Parse("2025-12-05 16:06:51");
                v.SaveMode = "MCF0YROgzaUqnDuKkro";
                v.ExtraInfo = "MImeNq2cx3ZVMYRpZe";
                v.HandlerInfo = "7YIFa9NhtBDLszyO";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddFrameworkUser()
        {
            FrameworkUser v = new FrameworkUser();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Email = "lE7Dgc5NaLzf";
                v.Gender = WalkingTec.Mvvm.Core.GenderEnum.Male;
                v.CellPhone = "VS3TDSeTV2";
                v.HomePhone = "4Mn3Ehphzt7TbIOU2cET";
                v.Address = "vUBq0jjYiSjeKpbX5resAI8h0Qj05r8klchKBgSFfdpOleGvObB26zvr3wSAggQBQnWVbaVzCzEDDllviI7ZmfJKtaLNdgbsoqRdxOdKDXvTQMOLbDUmwgAoqITnKfwjjaQD2giYFaoN5UJSQrFvP9";
                v.ZipCode = "ukVEAZ5pKm";
                v.ITCode = "xaz47E6ieRMfpsh4Ia9HT5C";
                v.Password = "wiHmiPIu3";
                v.Name = "cmQieiTSfpQFI3GXI0vDR8Xhrek8K9";
                v.IsValid = false;
                v.PhotoId = AddFileAttachment();
                context.Set<FrameworkUser>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseOrganization()
        {
            BaseOrganization v = new BaseOrganization();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.IsProduction = false;
                v.IsSale = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "8h3aRW57S7vpc6xmMOg6W7CKOWDD23CsHjKJj9iz2mzzQOSXun5hgKFcJLTy2WSQj3KV8T09267eHBeahktxg1a2InIKPySutsZYsUuV5rze5ngSj6JQUoh8Q1tTLrCYhUXuEUeqKxijlcW2HoOj1ceXV9sdwvFNPi6";
                v.Code = "a4qz";
                v.Name = "IAB5p9NtbjBiTnHUPftgRMfv0AsWWcMFESdYG80";
                v.SourceSystemId = "iRfwpgEGlne0tXyWF7A0MDawDDJ3JS";
                v.LastUpdateTime = DateTime.Parse("2025-10-29 16:06:51");
                context.Set<BaseOrganization>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseWareHouse()
        {
            BaseWareHouse v = new BaseWareHouse();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.OrganizationId = AddBaseOrganization();
                v.IsProduct = null;
                v.ShipType = WMS.Model.WhShipTypeEnum.SpotGoods;
                v.IsStacking = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "r72oJMijqGl9X9b91amxtpw9MhbpCntfwXBlEuac1U2Y6b9Np39xF57EhYhKY3tBUOMmRUX6SVkmE2gflTSVvhFVeMDAmaIb2Awp14yk3PRLQZPgy3nCRCutEFIqXOyoBWbP2QyY5Ir9bAgC85VMHxCzxJkhVRRaAaFGpfdhPZx3tHur3npMMHRJW2khjFwxjzkE1dSKzmeisec749f17DX7NVAcKoynR4VmmaPObit6ElYADtv0Jkrw1RJSR8yEzHKvwha9yL83y1P9upJpf47Kulm3eds867qfrTMlb8ttHD8riJMa4PS1hINEEUuawPq51EXr2cftWuYYRNUbURGD80H6RNRlAbV2dZ9VrwYWpOW3I71xuTuEKlq2kkbT3HYMeVC7tB0l9xlp";
                v.Code = "Wvw6bN5pCAanoL7GgqIWAXC77RSsRU6CJuDkY3Uf82t";
                v.Name = "Wl6rian9KsG7nBJ0H7cddJCjBuht";
                v.SourceSystemId = "dCdJEX";
                v.LastUpdateTime = DateTime.Parse("2024-11-11 16:06:51");
                context.Set<BaseWareHouse>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseWhArea()
        {
            BaseWhArea v = new BaseWhArea();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "0FtQvkWrORpUrfQ8SL84bEeoDwwfsVrN";
                v.Name = "zwGTe4C3UZ34";
                v.WareHouseId = AddBaseWareHouse();
                v.AreaType = WMS.Model.WhAreaEnum.Reinspect;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "RenYevKr1kOKcG8C3TijZXDiL8EZT4q49B6j1oDNlDgaDc1j6Yu1PsnF8slwzY53uhpZMR9cWXS7rxzn4lBCLj0ETDwl8Kr78x0WBVzIWYFEJ0KzChIwCawHzVtO3SD17rTjQg46laG7AXo4x0vtnbNfuQzqn2Qefoa6l7oMnaPRh0KkCSaFa6XBbgAEGXgI1oTPLK2qtsbaWMSMfdcv6til3iQQy7woFnoh5be9TdyyMGEipcMNqkc5x09yzVArrx501SFJ4slgwrPpTI9oqtRfeVkzwzaxQ3cuEpzJZWR8ANAmX5WlGTXhHzTRgJ8mDhCC1cAL5M3e8Fh45o0a96hgl8ptqZmxBrdRH49yS6myo03UtwJbLBvg7S5lWJSZSyRRizqrqL2ayjIbvc3BEHOKtbu68zajjo26tOXjuQjbZS9e3HFzqq6yhkvfqXykjTZHyrEp8tNus5XVrzWpO0oAX";
                context.Set<BaseWhArea>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseWhLocation()
        {
            BaseWhLocation v = new BaseWhLocation();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "VQkrv0X6TRPQE74GM6cmVykwDBemBhZdSV8E1sKwjEKrfs";
                v.Name = "i6fqCvdL2LmgGPwITdGRX3Z3Je";
                v.WhAreaId = AddBaseWhArea();
                v.AreaType = WMS.Model.WhLocationEnum.Rejected;
                v.Locked = false;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "8WDShAh2ec7oc1w907YMvTKs5IdFdTu5qSV3kgzBBblEhveEY8QVvywfJOsCL25tPNC8WoS2hXeWfUKI0fAfXxQ7siiScNcaZUnlekrYCRzDWnYrxmW1IlayGDGWMjfNYDkhPQeVUkD52ZunKq5naBSpltuxY9NllYDKQltszDmrOE6nBnASbAIuRhYNe85gkVS5PYvo8Vl3Fz5acF8im6D0GW36ARHlZBJszzwwqNOmwYG4NMtOz9n5pyj8c1poJWr4Lm0fHBkIaF5V1BPYR6ov9Ejxc0JzUWbwOwN5FtSNxce0zMmimHb2UwcGjRPgWwOajEqLTc4mqUKKe0Oe0RaqcFdVlXT1S4kmAbFx5kttGyT26HtxrQgIgGK0lW5MVuTPXMM2iMsDKDVg40w1NwQn9lsbaFK33p9IlWBU7xiAxLGQOEMGgdhP7s5ldvMILHXsjzMQq0tMtkKo0u9tdZrIS0MWUWqVmaqfNqqraTOIei";
                context.Set<BaseWhLocation>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseAnalysisType()
        {
            BaseAnalysisType v = new BaseAnalysisType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "6lj";
                v.Name = "VMcgNQs4VCEINhhwTa2wZpOJF0i8PcrPytw70jSfQbTrE";
                v.SourceSystemId = "TMZXWREtFYHgBwSVmOxors";
                v.LastUpdateTime = DateTime.Parse("2024-05-25 16:06:51");
                context.Set<BaseAnalysisType>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseDepartment()
        {
            BaseDepartment v = new BaseDepartment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.OrganizationId = AddBaseOrganization();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "ErJoBDQgHs70jDrMx6LZasMVTbDjkYL9Q3t3GbIBtq8iWAjVhAY22HbQmM3nmyUJM7mRhS4mIw3Y8hyCSZVuTqjpJEW7JsRTgUXA8fcavN8sCloOKypOT0nLMfdtxBv23FVja98BU6scODVxFmGUaARcACuRVwNzK90vl6B8a";
                v.Code = "V74O2BBBEgzZdd7SE4";
                v.Name = "7BlIF1YF0z6wflsZXud4IH4CrnBwlhEUlTl1yeCjOtK";
                v.SourceSystemId = "znd2";
                v.LastUpdateTime = DateTime.Parse("2025-08-23 16:06:51");
                context.Set<BaseDepartment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseItemCategory()
        {
            BaseItemCategory v = new BaseItemCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.OrganizationId = AddBaseOrganization();
                v.AnalysisTypeId = AddBaseAnalysisType();
                v.DepartmentId = AddBaseDepartment();
                v.Code = "f9wLKJxzqBChKinh38XNWd81chM7Z5lKIO7yogBk";
                v.Name = "sDdC27PLeVcZDAE2I";
                v.SourceSystemId = "vVkcTlaLbUNtO6K1egaujKAaLsVWoNmPyvUV3ap3TLBAt85";
                v.LastUpdateTime = DateTime.Parse("2025-05-12 16:06:51");
                context.Set<BaseItemCategory>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseUnit()
        {
            BaseUnit v = new BaseUnit();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "bCDg4ztglpKaRNWXmFL7eeXc6Mo98AeAVJ5rcK8genXn8be50cCemOzCGw89Zs0MwM1F3Y0Z3Yhgc15vmSWpsNy26dIyaCfo6WuqfQpHBCVp0fQG52NskEdXe2LFdzRZ4URdFOoPeDPRzbFK7Qqpoc3bms6xTAijkP";
                v.Code = "zlRsfIbGNQyBvTRweJY8bkJmqAVCHDUH9fuentBATUhF4IB";
                v.Name = "TBQhGhVuTAhifV5ZhCKoYqQPNROfaC2";
                v.SourceSystemId = "UyHukIxOPDPhv0lzNvdtnejISb8F";
                v.LastUpdateTime = DateTime.Parse("2024-05-20 16:06:51");
                context.Set<BaseUnit>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseItemMaster()
        {
            BaseItemMaster v = new BaseItemMaster();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.OrganizationId = AddBaseOrganization();
                v.ItemCategoryId = AddBaseItemCategory();
                v.SPECS = "PS9nb2ZnK0QCMWpuyNd2WMCH4eCcI7WuekJAPntoqXs5r8renUb5cPhUavDRnsEAhv8kidJtLRVHczbSihXAGq5LIpMdmd7PjOHaBHloEoc0A02iIpZIi5MjB9buiaraQ5isAeDizQW6omtY1lonstEvwYlS60SKTEINz58LRAiYzLcc1HyKTofnv3X21wEMETh0sQ3t0K9t6dIIa78j4lC4hoTmWLpwBgME5Mmt3SpPMFYSaDxm1f9zWaDPyYXnWU321RdK33lYnJJSdcrxb1sdWi7NGZMhYoqoQ2rpOK7erwfM98K58iCU16HrhUtqvU95QBbuU6k2SeRtSiozATd4VFOrx7vYHaKolMwXZkpsJw1rtwnSB69gLsOcPq57u04uUaCcxcwPOXUFiNRtXLRawAyEp593jRW3DZeJH90iqEqWXDxeJPR0xLMSXCtxzG0RQyGee3adpa3wkm75llSRvaKJIPteUGX58EsIkS";
                v.MateriaModel = "i4ikUsw";
                v.Description = "BBnoiLL";
                v.StockUnitId = AddBaseUnit();
                v.ProductionOrgId = AddBaseOrganization();
                v.ProductionDeptId = AddBaseDepartment();
                v.WhId = AddBaseWareHouse();
                v.FormAttribute = WMS.Model.ItemFormAttributeEnum.SubcontractPart;
                v.MateriaAttribute = "YmjcF0";
                v.GearRatio = 81;
                v.Power = 68;
                v.SafetyStockQty = 10;
                v.FixedLT = 21;
                v.BuildBatch = 28;
                v.NotAnalysisQty = 54;
                v.AnalysisTypeId = AddBaseAnalysisType();
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Code = "nrAc4P9p79YmhSFnEe";
                v.Name = "BcwjmKUUClXsNA7a9tVPTscv4";
                v.SourceSystemId = "WAzzHzFBvgq5fkDFubZB4V6l2Jqd";
                v.LastUpdateTime = DateTime.Parse("2024-12-17 16:06:51");
                context.Set<BaseItemMaster>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
