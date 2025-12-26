using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class InventorySplitSingleApiTest
    {
        private InventorySplitSingleApiController _controller;
        private string _seed;

        public InventorySplitSingleApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventorySplitSingleApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventorySplitSingleApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventorySplitSingleApiVM vm = _controller.Wtm.CreateVM<InventorySplitSingleApiVM>();
            InventorySplitSingle v = new InventorySplitSingle();
            
            v.DocNo = "6K9T16";
            v.OriginalInvId = AddBaseInventory();
            v.OriginalQty = 9;
            v.Memo = "MJt9TajEjKyGSHvsGUTDSvt9E4wVANer0n1ysWzRgxMVrTzmJkvZfssjyVyh4b2KXqa8sbRKeTuXb4Eonv2FM1SVwsf0EzEdFKRqXf8tEb058va4nz9n0w5GaKccPwyUchPlB5VrMTLJ4sVvzt0iRvrRy4vZoQ83qGcqodP8wPVywuh";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventorySplitSingle>().Find(v.ID);
                
                Assert.AreEqual(data.DocNo, "6K9T16");
                Assert.AreEqual(data.OriginalQty, 9);
                Assert.AreEqual(data.Memo, "MJt9TajEjKyGSHvsGUTDSvt9E4wVANer0n1ysWzRgxMVrTzmJkvZfssjyVyh4b2KXqa8sbRKeTuXb4Eonv2FM1SVwsf0EzEdFKRqXf8tEb058va4nz9n0w5GaKccPwyUchPlB5VrMTLJ4sVvzt0iRvrRy4vZoQ83qGcqodP8wPVywuh");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventorySplitSingle v = new InventorySplitSingle();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DocNo = "6K9T16";
                v.OriginalInvId = AddBaseInventory();
                v.OriginalQty = 9;
                v.Memo = "MJt9TajEjKyGSHvsGUTDSvt9E4wVANer0n1ysWzRgxMVrTzmJkvZfssjyVyh4b2KXqa8sbRKeTuXb4Eonv2FM1SVwsf0EzEdFKRqXf8tEb058va4nz9n0w5GaKccPwyUchPlB5VrMTLJ4sVvzt0iRvrRy4vZoQ83qGcqodP8wPVywuh";
                context.Set<InventorySplitSingle>().Add(v);
                context.SaveChanges();
            }

            InventorySplitSingleApiVM vm = _controller.Wtm.CreateVM<InventorySplitSingleApiVM>();
            var oldID = v.ID;
            v = new InventorySplitSingle();
            v.ID = oldID;
       		
            v.DocNo = "cC9qjrmXgSY3FtM1jjKyQSxq8z0neJAA";
            v.OriginalQty = 36;
            v.Memo = "vQw9ThL4q9w8k68ftM4aqYtQQT6Samnf1krkn7izgyEoY4CDy7Vt6UmIqNfWL0Uq13pdLBWL5jQu8v6pw9Ad6CK9cyUngNTw8QsjYJZrpVc1lGb50fvSD5nG3s9E7suN8Ao30AfX6RWY2UW0D3yHyKbA657leYSXT9S4pDPh17LirNX8kuMPPqywE0n3bdTDqEB3UKqzAUncdbLn34VTQ9h5whxPw4kP95zLhYnnnKaKHBn3URhJ7pSLMWV3qZ38pOWQJs0k12bvL1RmjwIjbakiZ82FpOlDKa2qTqB1SoUSE9NpTzf8UNKSebgV6J3vObOn1WMTXULSrHmgha8Bp1H4t3WDz8P4QodPh8Vcjonsygcepyls9Ss";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.OriginalInvId", "");
            vm.FC.Add("Entity.OriginalQty", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventorySplitSingle>().Find(v.ID);
 				
                Assert.AreEqual(data.DocNo, "cC9qjrmXgSY3FtM1jjKyQSxq8z0neJAA");
                Assert.AreEqual(data.OriginalQty, 36);
                Assert.AreEqual(data.Memo, "vQw9ThL4q9w8k68ftM4aqYtQQT6Samnf1krkn7izgyEoY4CDy7Vt6UmIqNfWL0Uq13pdLBWL5jQu8v6pw9Ad6CK9cyUngNTw8QsjYJZrpVc1lGb50fvSD5nG3s9E7suN8Ao30AfX6RWY2UW0D3yHyKbA657leYSXT9S4pDPh17LirNX8kuMPPqywE0n3bdTDqEB3UKqzAUncdbLn34VTQ9h5whxPw4kP95zLhYnnnKaKHBn3URhJ7pSLMWV3qZ38pOWQJs0k12bvL1RmjwIjbakiZ82FpOlDKa2qTqB1SoUSE9NpTzf8UNKSebgV6J3vObOn1WMTXULSrHmgha8Bp1H4t3WDz8P4QodPh8Vcjonsygcepyls9Ss");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventorySplitSingle v = new InventorySplitSingle();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DocNo = "6K9T16";
                v.OriginalInvId = AddBaseInventory();
                v.OriginalQty = 9;
                v.Memo = "MJt9TajEjKyGSHvsGUTDSvt9E4wVANer0n1ysWzRgxMVrTzmJkvZfssjyVyh4b2KXqa8sbRKeTuXb4Eonv2FM1SVwsf0EzEdFKRqXf8tEb058va4nz9n0w5GaKccPwyUchPlB5VrMTLJ4sVvzt0iRvrRy4vZoQ83qGcqodP8wPVywuh";
                context.Set<InventorySplitSingle>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventorySplitSingle v1 = new InventorySplitSingle();
            InventorySplitSingle v2 = new InventorySplitSingle();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DocNo = "6K9T16";
                v1.OriginalInvId = AddBaseInventory();
                v1.OriginalQty = 9;
                v1.Memo = "MJt9TajEjKyGSHvsGUTDSvt9E4wVANer0n1ysWzRgxMVrTzmJkvZfssjyVyh4b2KXqa8sbRKeTuXb4Eonv2FM1SVwsf0EzEdFKRqXf8tEb058va4nz9n0w5GaKccPwyUchPlB5VrMTLJ4sVvzt0iRvrRy4vZoQ83qGcqodP8wPVywuh";
                v2.DocNo = "cC9qjrmXgSY3FtM1jjKyQSxq8z0neJAA";
                v2.OriginalInvId = v1.OriginalInvId; 
                v2.OriginalQty = 36;
                v2.Memo = "vQw9ThL4q9w8k68ftM4aqYtQQT6Samnf1krkn7izgyEoY4CDy7Vt6UmIqNfWL0Uq13pdLBWL5jQu8v6pw9Ad6CK9cyUngNTw8QsjYJZrpVc1lGb50fvSD5nG3s9E7suN8Ao30AfX6RWY2UW0D3yHyKbA657leYSXT9S4pDPh17LirNX8kuMPPqywE0n3bdTDqEB3UKqzAUncdbLn34VTQ9h5whxPw4kP95zLhYnnnKaKHBn3URhJ7pSLMWV3qZ38pOWQJs0k12bvL1RmjwIjbakiZ82FpOlDKa2qTqB1SoUSE9NpTzf8UNKSebgV6J3vObOn1WMTXULSrHmgha8Bp1H4t3WDz8P4QodPh8Vcjonsygcepyls9Ss";
                context.Set<InventorySplitSingle>().Add(v1);
                context.Set<InventorySplitSingle>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventorySplitSingle>().Find(v1.ID);
                var data2 = context.Set<InventorySplitSingle>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }

        private Guid AddBaseOrganization()
        {
            BaseOrganization v = new BaseOrganization();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.IsProduction = null;
                v.IsSale = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "gmGJ3N5HuvjPPhzgmITeRTjqI00ZzfGGxMii2aZfEAX5lincS2H10RYFQR3HQhHkcgP283f8W8Z3sUaT0Eq7qgDwmN5H1K1bcl0qiAvbe1nmA0UBC54dAIu2qThNgotNJ0jxZWjIQ0tOMYiRlGtz";
                v.Code = "OFWWXfCqL6FOAASOj5";
                v.Name = "y7QIlKkoNfP";
                v.SourceSystemId = "EAGq9Ip";
                v.LastUpdateTime = DateTime.Parse("2025-04-20 15:14:03");
                context.Set<BaseOrganization>().Add(v);
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

                v.Code = "9m4HMQk8LN2QE2QfOmoL2CDDqxbNle";
                v.Name = "TYZPtlZwPtWEAyqlhKgEMkGYFtoGAHJ8Y6EE2";
                v.SourceSystemId = "5Pih";
                v.LastUpdateTime = DateTime.Parse("2026-05-24 15:14:03");
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
                v.IsMFG = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "Kdgvxt0VwvncsJAcCc8dwsMxa6uLOvRRCyk11xiPciHX7aoTGhWm7ugXGsB2ZP0jTeREArtVgl7aZd0l6XGZ3qQ7eoJp9O3P94CSaMY6d3fMGYfK2r3QrQjvHr2oEaLtWYFV6WXRVbTWHqugv9vQC6ugragim5qUo8z07xc6RXI93iLEpEavV6B5pIWKsJVSmBCdwikKTjLdEJqAhJCbG6BaHuzLGK021UCuj9llSDSCGWVBGc7b8xp91iDCov7eWjwO4ICiiKa8sI1z7c27GBtAbS5f7KXahSzXbp0z82NXkHVE9IO66wmGWGG4H6edipWPgxK5SjByaGEt1cgiouJdHaVduOvmXkvlygFYpgWUUDq40F7HSfkF7f";
                v.Code = "XatzD0Lkw267oYi5cr7G7TyzWpQLFrsp2mXcqk1p8eWFM";
                v.Name = "8WvkhUc3idlTowcKQo7XbFsqIBInp6PrOxMJI4yw9F9k";
                v.SourceSystemId = "JfY71EyNjAiXFzSjpnk9fmkYqcrjwscHSG";
                v.LastUpdateTime = DateTime.Parse("2025-08-01 15:14:03");
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
                v.Code = "wL6VYIJI60XJeeZzwVj5l";
                v.Name = "pisBd5pB1z11y3zzAD0d5GinVSgvjR2Kaq1";
                v.SourceSystemId = "PXFXhh5MX0g1pfhMBMfNJxn6XZ80Su5fsKtePurrnfto";
                v.LastUpdateTime = DateTime.Parse("2025-10-14 15:14:03");
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
                v.Memo = "41RMab19oy34ljrpc74lNTAYpeBQqOxktiFm4OtVNpPkQHXUE23m6Ig7b8vAn";
                v.Code = "boIXZo0Pwk4";
                v.Name = "zcq51b1q3cPnTf18ebzBonMHMkr4oS7DTIG";
                v.SourceSystemId = "Zlld";
                v.LastUpdateTime = DateTime.Parse("2024-07-01 15:14:03");
                context.Set<BaseUnit>().Add(v);
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
                v.ShipType = WMS.Model.WhShipTypeEnum.ToCustomer;
                v.IsStacking = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "t3jPUM0QPKB4ePycWxStIohd4Kc84zsJHTT2CsWBoeQjPDwTrDJWCWwlTquSP0LWRr3WdfTJv4H608kHn";
                v.Code = "OmzIUdpbfr6QplqyDnjwpYv0dCzjnfaak3";
                v.Name = "k5vg3f5N";
                v.SourceSystemId = "s";
                v.LastUpdateTime = DateTime.Parse("2025-11-26 15:14:03");
                context.Set<BaseWareHouse>().Add(v);
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
                v.SPECS = "tPcbLpvXKoYEX3dp7aK6d33XQ9XdjcuxWzdyM7fH2IVr7BFX7nqxpa0otbpQ4ynvC2ILMSRMapTC3vEbEf2vugvPFW1eAirXA3wcGgibsO2IIxTGGtJENb60cvgeZ9oguRUBnGOT2eNsLbr06c5Ql9WR7swVqcdexk3i67RhTyX0iPNMquDIwOfYiNSgPkxhS7lhxD1SdQKrUWnPpOnxuUivluTd9PX";
                v.MateriaModel = "bvOLr7MOpWMKgIBDjye2dlAldG0hhARAW0P2zyjIKN7Y1mvQHYPnzGRunBLb8Q3t9UfL8X3xDLfrkdOcwNWVInU8LhieHfFc43xAi2oE4ERHXJWSt52HJ57xgZVWLpDo3ptXwLEcac9Jq0CRdl0dhuRINnlNyvmNYOtWi0eO0RadEg1BbkCalJ3CtxMM9tL8cZrE3ZBIbyjWjaPPsBD93BajFWg46Sy3h8Q79hf9MIB4NaUc1BQWKc9kRMcP79eqYfpVbe9k2q1ezHTvPCIKsvqp3faqf8T3KlhcTX9mxQ9aXM5ZMk8maGyfBXntG1OElh98idMrtqgPtkpMXPxPiGzMmCEiG4j6Mg0ji8s4AqhEaxvepZvWIPocMe8nDqidYSpJmHZPycXkxB4uanTUAiOYDucH7ZRJZQ9A66xWxY7GA5TaV62IT2XNvy0wW9889ernyxS6";
                v.Description = "Ucc7ziRsvSfaK";
                v.StockUnitId = AddBaseUnit();
                v.ProductionOrgId = AddBaseOrganization();
                v.ProductionDeptId = AddBaseDepartment();
                v.WhId = AddBaseWareHouse();
                v.FormAttribute = WMS.Model.ItemFormAttributeEnum.PurchasePart;
                v.MateriaAttribute = "qE7TBJWITxGwr3mSM";
                v.GearRatio = 36;
                v.Power = 89;
                v.SafetyStockQty = 34;
                v.FixedLT = 47;
                v.BuildBatch = 23;
                v.NotAnalysisQty = 50;
                v.AnalysisTypeId = AddBaseAnalysisType();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Code = "vT";
                v.Name = "vQseQ8WZNAELkE7GLs7lOgYMrePPTio6uR8urgcSR8kBX3";
                v.SourceSystemId = "MJMDM";
                v.LastUpdateTime = DateTime.Parse("2025-05-31 15:14:03");
                context.Set<BaseItemMaster>().Add(v);
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

                v.Code = "KAOTiQmVqTgP6CJtf4dkK";
                v.Name = "XLIrxRtGaEV7qVw88t";
                v.WareHouseId = AddBaseWareHouse();
                v.AreaType = WMS.Model.WhAreaEnum.Normal;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "H9UCIpJTmhi6qIYLwWHLGRtJr2P3C2Qo6rqzgRK18vzedRc4rAJreeAX88S9wiL10cZHuzS2YLuIqB5RkvFmNFKDcC7ILTO8OFH89pwPqkeBen1Jrd86l5XbaT1vw2AzkgKryB9k75mLKu3Qjf3OZrkcoaLiV3WxPiaDGyXnDljOBI2jYVlxnjNQDWLtprEdsmLwMwEKOpIBrGazH2YQiSmQTVaFSXJfPzIKjCwKV5KXLoFNBbiI1hkwE9Ag4mi8ZW81z9mV5PzN1mnvEdAN7h3N4m6UsNsVP1PYcgVQt3YH42AyLbCdLENXOcsX3VZNUnoLsKdWadScwAhJFb67mQ01F9sxnymeK10Mxe96OZErbTQdnMH62OVW6vHcv5B76FrEchhgTvdi";
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

                v.Code = "inFC0OrA";
                v.Name = "TGzTbnFx9SpZfd3Y8mXIc3IBsgPgOrnzpycBZkmKhx";
                v.WhAreaId = AddBaseWhArea();
                v.AreaType = WMS.Model.WhLocationEnum.Inspected;
                v.Locked = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "UcVWNj6OWe9RIaMR6qrdOGkqEwF7cWB1kV1ddt0PIBCKWhHsseVvQSv6TKwqWAJYvlHn2BlBvEIcljH3eHNJ5y2C9VWQFgNDosrUUwGBISuDyQaLzjmXQaU5RsvKEZuUY5JqNJhSfdWgxlSx9PCx5aLb56cMduS7St1A1LCkZ6wKUZiNKfyjSwkMYJT8RFDzEtM23NX6BuFBtIsc3uQGncNmXFcvJanqM";
                context.Set<BaseWhLocation>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseInventory()
        {
            BaseInventory v = new BaseInventory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ItemMasterId = AddBaseItemMaster();
                v.WhLocationId = AddBaseWhLocation();
                v.BatchNumber = "XqvsdaOWgnmlnN0edkIKLHQj20FzBkA5v";
                v.SerialNumber = "JU";
                v.Seiban = "tHuel3Mj0vrlZ8wG5sLHf6PQtwzMRvS971c3gOom";
                v.SeibanRandom = "FuDWIqNwSnP110nMfnik";
                v.Qty = 86;
                v.IsAbandoned = true;
                v.ItemSourceType = WMS.Model.ItemSourceTypeEnum.Buy;
                v.FrozenStatus = WMS.Model.FrozenStatusEnum.Freezed;
                context.Set<BaseInventory>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
