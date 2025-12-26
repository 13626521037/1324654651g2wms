using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.BaseData.BaseInventoryVMs;
using WMS.Model.BaseData;
using WMS.DataAccess;


namespace WMS.Test
{
    [TestClass]
    public class BaseInventoryApiTest
    {
        private BaseInventoryApiController _controller;
        private string _seed;

        public BaseInventoryApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<BaseInventoryApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new BaseInventoryApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            BaseInventoryApiVM vm = _controller.Wtm.CreateVM<BaseInventoryApiVM>();
            BaseInventory v = new BaseInventory();
            
            v.ItemMasterId = AddBaseItemMaster();
            v.WhLocationId = AddBaseWhLocation();
            v.BatchNumber = "hDcMhChVvtwA9GzhGS0uHIWKfXnTmpE36sxrxSMXN0";
            v.SerialNumber = "NXwpTb";
            v.Seiban = "8Y7";
            v.Qty = 98;
            v.IsAbandoned = false;
            v.ItemSourceType = WMS.Model.ItemSourceTypeEnum.Make;
            v.FrozenStatus = WMS.Model.FrozenStatusEnum.Freezed;
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BaseInventory>().Find(v.ID);
                
                Assert.AreEqual(data.BatchNumber, "hDcMhChVvtwA9GzhGS0uHIWKfXnTmpE36sxrxSMXN0");
                Assert.AreEqual(data.SerialNumber, "NXwpTb");
                Assert.AreEqual(data.Seiban, "8Y7");
                Assert.AreEqual(data.Qty, 98);
                Assert.AreEqual(data.IsAbandoned, false);
                Assert.AreEqual(data.ItemSourceType, WMS.Model.ItemSourceTypeEnum.Make);
                Assert.AreEqual(data.FrozenStatus, WMS.Model.FrozenStatusEnum.Freezed);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            BaseInventory v = new BaseInventory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ItemMasterId = AddBaseItemMaster();
                v.WhLocationId = AddBaseWhLocation();
                v.BatchNumber = "hDcMhChVvtwA9GzhGS0uHIWKfXnTmpE36sxrxSMXN0";
                v.SerialNumber = "NXwpTb";
                v.Seiban = "8Y7";
                v.Qty = 98;
                v.IsAbandoned = false;
                v.ItemSourceType = WMS.Model.ItemSourceTypeEnum.Make;
                v.FrozenStatus = WMS.Model.FrozenStatusEnum.Freezed;
                context.Set<BaseInventory>().Add(v);
                context.SaveChanges();
            }

            BaseInventoryApiVM vm = _controller.Wtm.CreateVM<BaseInventoryApiVM>();
            var oldID = v.ID;
            v = new BaseInventory();
            v.ID = oldID;
       		
            v.BatchNumber = "nOnazfIIBqsZse7TsX6tno";
            v.SerialNumber = "uvqjZsJgn";
            v.Seiban = "Fb69S";
            v.Qty = 34;
            v.IsAbandoned = false;
            v.ItemSourceType = WMS.Model.ItemSourceTypeEnum.Buy;
            v.FrozenStatus = WMS.Model.FrozenStatusEnum.Freezed;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ItemMasterId", "");
            vm.FC.Add("Entity.WhLocationId", "");
            vm.FC.Add("Entity.BatchNumber", "");
            vm.FC.Add("Entity.SerialNumber", "");
            vm.FC.Add("Entity.Seiban", "");
            vm.FC.Add("Entity.Qty", "");
            vm.FC.Add("Entity.IsAbandoned", "");
            vm.FC.Add("Entity.ItemSourceType", "");
            vm.FC.Add("Entity.FrozenStatus", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BaseInventory>().Find(v.ID);
 				
                Assert.AreEqual(data.BatchNumber, "nOnazfIIBqsZse7TsX6tno");
                Assert.AreEqual(data.SerialNumber, "uvqjZsJgn");
                Assert.AreEqual(data.Seiban, "Fb69S");
                Assert.AreEqual(data.Qty, 34);
                Assert.AreEqual(data.IsAbandoned, false);
                Assert.AreEqual(data.ItemSourceType, WMS.Model.ItemSourceTypeEnum.Buy);
                Assert.AreEqual(data.FrozenStatus, WMS.Model.FrozenStatusEnum.Freezed);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            BaseInventory v = new BaseInventory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ItemMasterId = AddBaseItemMaster();
                v.WhLocationId = AddBaseWhLocation();
                v.BatchNumber = "hDcMhChVvtwA9GzhGS0uHIWKfXnTmpE36sxrxSMXN0";
                v.SerialNumber = "NXwpTb";
                v.Seiban = "8Y7";
                v.Qty = 98;
                v.IsAbandoned = false;
                v.ItemSourceType = WMS.Model.ItemSourceTypeEnum.Make;
                v.FrozenStatus = WMS.Model.FrozenStatusEnum.Freezed;
                context.Set<BaseInventory>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            BaseInventory v1 = new BaseInventory();
            BaseInventory v2 = new BaseInventory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ItemMasterId = AddBaseItemMaster();
                v1.WhLocationId = AddBaseWhLocation();
                v1.BatchNumber = "hDcMhChVvtwA9GzhGS0uHIWKfXnTmpE36sxrxSMXN0";
                v1.SerialNumber = "NXwpTb";
                v1.Seiban = "8Y7";
                v1.Qty = 98;
                v1.IsAbandoned = false;
                v1.ItemSourceType = WMS.Model.ItemSourceTypeEnum.Make;
                v1.FrozenStatus = WMS.Model.FrozenStatusEnum.Freezed;
                v2.ItemMasterId = v1.ItemMasterId; 
                v2.WhLocationId = v1.WhLocationId; 
                v2.BatchNumber = "nOnazfIIBqsZse7TsX6tno";
                v2.SerialNumber = "uvqjZsJgn";
                v2.Seiban = "Fb69S";
                v2.Qty = 34;
                v2.IsAbandoned = false;
                v2.ItemSourceType = WMS.Model.ItemSourceTypeEnum.Buy;
                v2.FrozenStatus = WMS.Model.FrozenStatusEnum.Freezed;
                context.Set<BaseInventory>().Add(v1);
                context.Set<BaseInventory>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BaseInventory>().Find(v1.ID);
                var data2 = context.Set<BaseInventory>().Find(v2.ID);
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

                v.IsProduction = true;
                v.IsSale = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "Pk7kuzjX4gnP8jUNR3Hd1d9bqmHnMmsSInEGNArPyP81WqG4rOFlk0pX9wwNSamkResAxyix3OUhfjpo4";
                v.Code = "Hc1T1LQG80CcdvNA0rbAbyzyifLhPM8LnpUMtMOZvK2a9";
                v.Name = "fcah56k5MxkfYcFSz7JjypzgJnUhnq24oEB4O1k";
                v.SourceSystemId = "IwuzmTyDE811X5UHm";
                v.LastUpdateTime = DateTime.Parse("2024-03-31 16:50:12");
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

                v.Code = "3cjJGYHcSsRdnpUnb9sB97GjiegS6v1MPwrE38P";
                v.Name = "rG3GV6W0duLQKvFHqCbo9SXwV48gZGJqKR6T3DBKzF5";
                v.SourceSystemId = "sjUMu95OnlD4cHWlzHMF4pNXVpHTXeVEn66";
                v.LastUpdateTime = DateTime.Parse("2026-08-01 16:50:12");
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
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "PUM7kqDWostgqzTnLQx3qLS9tK2QnrB4M8WOWmLUbLQJUZZaVD6hvCtg42W3THbSBINGH1DyMS04VYhEp5SiFVuqCpJTiqfKZa6nD2TSjmVZ7QBmjoEtMLimUTWlMKoZd0M4XVX99uwbB93CcMuPiMezpKuW0b7kqna2PpxzQhZYHWdqOTTUDTzD8hMlioQkMcKZIdYDRYPxCIOusxfDb7kBGnF7xJX0lg0I6ZigXN2vY27HZZOPwMTy";
                v.Code = "hf2eOt7MyXskGHvLnCCP1EHR8l4Q8";
                v.Name = "6vqaQ1KPrAfaaqfjENgUdub0lAdHN3oa";
                v.SourceSystemId = "Mdu6xR9S1VlhpX05y68tmqWG";
                v.LastUpdateTime = DateTime.Parse("2025-07-21 16:50:12");
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
                v.Code = "94oKM1GW6pL";
                v.Name = "JTu2Klmq0sgMZVY81htmr4tRHxCysjVwl5y";
                v.SourceSystemId = "Yy2O8gRB75SNZtDFO63UoPD0iDK6rhSMlSkerP1rvZk5PCR8";
                v.LastUpdateTime = DateTime.Parse("2024-11-18 16:50:12");
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

                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "RJERvPHxsgaVxST0Vd86UMVOuFd0B4ke5FDqm8SJZ6ELVhyxIiCsYdNYF1MiqdENoAdJr7vC6yNYby3oyAVht05IJiKtM4JuMrWO03VnDjoDDozIaco9GHR7XtShRWpyNgezAumafSK9GceApvabALM18eKBBlRbzhXDmmvQdlFQFg446ZVBo5027589g7PLidTE57iAxK2Eei95SOoOnSWMUKuccIbu3HfbZJjcq1lIaqAiAdVuYkKd6zHzb2R7yNVCjMplVOY0CiP3HTRhOfbuoHGvcnqmRS6tH0JcFDW9fS8QJx83IVH6wGYibp0SnbvUQpKVYfUEqCXPpwbmhTbT9xOvYE5PsjFx6WRdgpS2m14fn6tw7mmesRv6Hj5aOaRK2i2HgoSW7WwOOVYftQUyKvJDRu3QQrx5Dz0LrjBAaXie2KRHga7hJogL967W3ah952Ph1i9Dc324K99DtxLBFBFzg7LSytltnztNLT4e8AFx";
                v.Code = "Pl5Qn3rJ";
                v.Name = "x5AJ1NdXeDat2FIVnABJ4Fu18hq";
                v.SourceSystemId = "hi3Eb3o2dDceCBSaOHA";
                v.LastUpdateTime = DateTime.Parse("2025-01-31 16:50:12");
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
                v.IsProduct = false;
                v.ShipType = WMS.Model.WhShipTypeEnum.WaitToShip;
                v.IsStacking = false;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "GxtT3dfH9eFxTRWmWnEby9NSCZbssGQgu48nzYUe0CnVQQoPDCn3RgYp55R9PKJZJZg8OlA1788UINzIeXgL1VMVCUsGiy598jOWHgdbVzqlAm3HSQKBmUfhfuEcYTL9zdCYd3jcnyVT1r5FNekpKY8Tvru4lWhBQxHzNofDizqhLNs7EcgMDoteT3vTd18N0LHC5Ay9C8TwsRtuEhuLE7yZBnGqf3JJIDDTdK0nyiyWakRDGaPgmhT0lnJlYxerlnZfwrqemiYvBHHYkrOrWixrIFUHcc";
                v.Code = "Gh47cd45dpKJ3HppjVUMAW2c";
                v.Name = "FPf66a50We";
                v.SourceSystemId = "iegV2sknytKwH2NARW7";
                v.LastUpdateTime = DateTime.Parse("2024-11-23 16:50:12");
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
                v.SPECS = "oe5791xZh0dLrGv9HJUHRuANJUJT2VIQpyn1JVbxTh7uWfThSucHlAd4I6sUpWXKRprcl0UQqXUlY2RPoIeICRYasUvtQXfHajeYSDSL56hHallz98ZYiNnyaV3N0Oq2ECEtRprQTeohivicVyEit58ZWIrwlfnilzyqjpM1Z7hCIDwROAv8Su2EIDjowAmWymJAu3zPTBYkQAlhareUn0F1Oy4zCgLNwGfqBO9IosC";
                v.MateriaModel = "HcSh21rKMpPyzu9OU5Y2GtIV8dWhKlalWNcXwNjC5rWG1PejaBnR2muGFy4vOe1hpfQb9pBmxtUVlr4KeVctJSKQ9aAx4eUaCASp9MIvuNBQgBIdblUsK5KBO34o7czOyib21PyvUIc0qHUUXMK1Fq9Dr39oeWnsMiYxu3IpqzXc5ZMKPaX5KzuC6RSTqxjiCSybxVWdrzkOIvWMLvmGHsetEcvY5PDnvq0sf3sX4oOlmvmjsdVZZeyb7lOFGkAc1pkkebscdijeppk8Hdsb0MbXtopakKPlg5eXx8uHJaXLldCPdeWjAHSVa5253GetI7ZtFqxevH7VA8GlbzGFZr51KLcVjwKI63F2VFbIH";
                v.Description = "N0Zsz9YwaoFK7FaO0mo";
                v.StockUnitId = AddBaseUnit();
                v.ProductionOrgId = AddBaseOrganization();
                v.ProductionDeptId = AddBaseDepartment();
                v.WhId = AddBaseWareHouse();
                v.FormAttribute = WMS.Model.ItemFormAttributeEnum.SubcontractPart;
                v.MateriaAttribute = "ondkSpg4Xx0xtEk2I";
                v.GearRatio = 86;
                v.Power = 57;
                v.SafetyStockQty = 70;
                v.FixedLT = 9;
                v.BuildBatch = 19;
                v.NotAnalysisQty = 73;
                v.AnalysisTypeId = AddBaseAnalysisType();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Code = "SzvNjiTYaEw1OE9Q46LD20Qph7G13CcxOrAZKf139hKmqt";
                v.Name = "2gjQG84Q0Giv1dK4Vt8fYoqd4SussKbPXHnwOl47";
                v.SourceSystemId = "8GtvfWnbQXvWNV37gIv3Stz1jwQIu9Y";
                v.LastUpdateTime = DateTime.Parse("2025-03-04 16:50:12");
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

                v.Code = "n4dkUB3VIJiPSYEunXkwvXrsuiD3RfAOEXuTvKu";
                v.Name = "0iiNAXfiRNTTT222Vm1BywjFMNNKIrLg";
                v.WareHouseId = AddBaseWareHouse();
                v.AreaType = WMS.Model.WhAreaEnum.Rejected;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "DKlpJ0KCUvpZGm55RA4jzssHagj8EFMlhVE94d3BpP3YmNaL9iVZeuC52j4IubpM5ZQxNdOOsn1V8SURseyExXnnqr4ULNDzP1MQ68IBgyzXmnM2y0fm7X5dTgvhL79LDhWpDYWFGF4KUPnwYnkOa1AUp2sPUvypxkn4br3Wz";
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

                v.Code = "gcQcE0huSE8brd6wdpP93za6og";
                v.Name = "iEqMmttzyT42RTKmSOI20XbJpxhg9";
                v.WhAreaId = AddBaseWhArea();
                v.AreaType = WMS.Model.WhLocationEnum.WaitForReceive;
                v.Locked = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "FgArAFAk55FmhlQfGAoVx6b5O";
                context.Set<BaseWhLocation>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
