using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class InventoryPalletVirtualApiTest
    {
        private InventoryPalletVirtualApiController _controller;
        private string _seed;

        public InventoryPalletVirtualApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryPalletVirtualApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryPalletVirtualApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryPalletVirtualApiVM vm = _controller.Wtm.CreateVM<InventoryPalletVirtualApiVM>();
            InventoryPalletVirtual v = new InventoryPalletVirtual();
            
            v.Code = "N9fENCWda7yn5Xa9";
            v.Status = WMS.Model.FrozenStatusEnum.Freezed;
            v.LocationId = AddBaseWhLocation();
            v.SysVersion = 37;
            v.Memo = "F6Y8jLjKKV0eGs9FMLvDRurckVnasA5fP8A5RUxJpZw4pMVzAkShu9yNh0yzjUNLYUkvxQjFsf1fzWyhmQn6k7uXGibr9ka1nzdMaRJeUYglZuLTyhnlNNcJLL6iVTYXrvEPxCV1ywhIkOe46wMJusYaal0ze4Ctt0f6rXBnWBAWrH5Ow9YM2UIHHQZ0T3jkyTkkTV8VoZ3JL8GsJnbyB1byfzMFuWyTd0Ksz9bZZjxLNMzKykE2exy1oQEeoCmEmxIVz5qvfm4SmNvcTBNAbiVPA0NNamgHRR021Q2DyzMtAOgOxYBSCVM4DP04CzYRPnwpO4rIla4kTaqCUXKrkAl5fc2LwT1hWiWQMDgSXJkQEqIDdQd3zYBbtYZjy4HjMhP5nKAyTkMGWgWOdNxOTLhvZ7YzzNKtvZYs8fCrJ3J5pWA57rijajJHvFt1UxSOUCVdC1nViazhtK";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryPalletVirtual>().Find(v.ID);
                
                Assert.AreEqual(data.Code, "N9fENCWda7yn5Xa9");
                Assert.AreEqual(data.Status, WMS.Model.FrozenStatusEnum.Freezed);
                Assert.AreEqual(data.SysVersion, 37);
                Assert.AreEqual(data.Memo, "F6Y8jLjKKV0eGs9FMLvDRurckVnasA5fP8A5RUxJpZw4pMVzAkShu9yNh0yzjUNLYUkvxQjFsf1fzWyhmQn6k7uXGibr9ka1nzdMaRJeUYglZuLTyhnlNNcJLL6iVTYXrvEPxCV1ywhIkOe46wMJusYaal0ze4Ctt0f6rXBnWBAWrH5Ow9YM2UIHHQZ0T3jkyTkkTV8VoZ3JL8GsJnbyB1byfzMFuWyTd0Ksz9bZZjxLNMzKykE2exy1oQEeoCmEmxIVz5qvfm4SmNvcTBNAbiVPA0NNamgHRR021Q2DyzMtAOgOxYBSCVM4DP04CzYRPnwpO4rIla4kTaqCUXKrkAl5fc2LwT1hWiWQMDgSXJkQEqIDdQd3zYBbtYZjy4HjMhP5nKAyTkMGWgWOdNxOTLhvZ7YzzNKtvZYs8fCrJ3J5pWA57rijajJHvFt1UxSOUCVdC1nViazhtK");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryPalletVirtual v = new InventoryPalletVirtual();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Code = "N9fENCWda7yn5Xa9";
                v.Status = WMS.Model.FrozenStatusEnum.Freezed;
                v.LocationId = AddBaseWhLocation();
                v.SysVersion = 37;
                v.Memo = "F6Y8jLjKKV0eGs9FMLvDRurckVnasA5fP8A5RUxJpZw4pMVzAkShu9yNh0yzjUNLYUkvxQjFsf1fzWyhmQn6k7uXGibr9ka1nzdMaRJeUYglZuLTyhnlNNcJLL6iVTYXrvEPxCV1ywhIkOe46wMJusYaal0ze4Ctt0f6rXBnWBAWrH5Ow9YM2UIHHQZ0T3jkyTkkTV8VoZ3JL8GsJnbyB1byfzMFuWyTd0Ksz9bZZjxLNMzKykE2exy1oQEeoCmEmxIVz5qvfm4SmNvcTBNAbiVPA0NNamgHRR021Q2DyzMtAOgOxYBSCVM4DP04CzYRPnwpO4rIla4kTaqCUXKrkAl5fc2LwT1hWiWQMDgSXJkQEqIDdQd3zYBbtYZjy4HjMhP5nKAyTkMGWgWOdNxOTLhvZ7YzzNKtvZYs8fCrJ3J5pWA57rijajJHvFt1UxSOUCVdC1nViazhtK";
                context.Set<InventoryPalletVirtual>().Add(v);
                context.SaveChanges();
            }

            InventoryPalletVirtualApiVM vm = _controller.Wtm.CreateVM<InventoryPalletVirtualApiVM>();
            var oldID = v.ID;
            v = new InventoryPalletVirtual();
            v.ID = oldID;
       		
            v.Code = "PWw2i2ktRJ8lvEve";
            v.Status = WMS.Model.FrozenStatusEnum.Freezed;
            v.SysVersion = 4;
            v.Memo = "bOrlCYa9yqrqlHpbWzmodxuLkgyVPAsC0r1duajduo6a2pmf5FnyO6Jx7b3hhGX3AX5zKyUC0RWMnW2RW0TvHhHAOwJRZJB3wCp7cYUgeD03nusEEgJacvrcy7f1p1ypvfvgFah1gYgi8rpljTVamiH8UJC1AINjKEO0RPXRrYQEj3loHdY2zrXT9JhOne0PBp3g63QWvOsMsR4Q3siav5ObLZYCBABkhjgErbEmoiq1BOAtxKSZiiU3lO2SADi7SUhp5ZnjtNyKyWGp5YlKNcvki805AK3UsyAJUzqW3olqNXOT9j90726WjMjrRS27U6mAdevMX3D6tF2U1PgRkjMuqP4xKYuvsPegdqTrAxqkDLaBYmDf4EVBXB3iW4jneMATDC2oDeXHLJ7NWO9qM5xqXk7hkDzEJP5dphdTxrPdRf5QGPe8EIA0Emciu5vUoxXMFkWQ";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.LocationId", "");
            vm.FC.Add("Entity.SysVersion", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryPalletVirtual>().Find(v.ID);
 				
                Assert.AreEqual(data.Code, "PWw2i2ktRJ8lvEve");
                Assert.AreEqual(data.Status, WMS.Model.FrozenStatusEnum.Freezed);
                Assert.AreEqual(data.SysVersion, 4);
                Assert.AreEqual(data.Memo, "bOrlCYa9yqrqlHpbWzmodxuLkgyVPAsC0r1duajduo6a2pmf5FnyO6Jx7b3hhGX3AX5zKyUC0RWMnW2RW0TvHhHAOwJRZJB3wCp7cYUgeD03nusEEgJacvrcy7f1p1ypvfvgFah1gYgi8rpljTVamiH8UJC1AINjKEO0RPXRrYQEj3loHdY2zrXT9JhOne0PBp3g63QWvOsMsR4Q3siav5ObLZYCBABkhjgErbEmoiq1BOAtxKSZiiU3lO2SADi7SUhp5ZnjtNyKyWGp5YlKNcvki805AK3UsyAJUzqW3olqNXOT9j90726WjMjrRS27U6mAdevMX3D6tF2U1PgRkjMuqP4xKYuvsPegdqTrAxqkDLaBYmDf4EVBXB3iW4jneMATDC2oDeXHLJ7NWO9qM5xqXk7hkDzEJP5dphdTxrPdRf5QGPe8EIA0Emciu5vUoxXMFkWQ");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryPalletVirtual v = new InventoryPalletVirtual();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Code = "N9fENCWda7yn5Xa9";
                v.Status = WMS.Model.FrozenStatusEnum.Freezed;
                v.LocationId = AddBaseWhLocation();
                v.SysVersion = 37;
                v.Memo = "F6Y8jLjKKV0eGs9FMLvDRurckVnasA5fP8A5RUxJpZw4pMVzAkShu9yNh0yzjUNLYUkvxQjFsf1fzWyhmQn6k7uXGibr9ka1nzdMaRJeUYglZuLTyhnlNNcJLL6iVTYXrvEPxCV1ywhIkOe46wMJusYaal0ze4Ctt0f6rXBnWBAWrH5Ow9YM2UIHHQZ0T3jkyTkkTV8VoZ3JL8GsJnbyB1byfzMFuWyTd0Ksz9bZZjxLNMzKykE2exy1oQEeoCmEmxIVz5qvfm4SmNvcTBNAbiVPA0NNamgHRR021Q2DyzMtAOgOxYBSCVM4DP04CzYRPnwpO4rIla4kTaqCUXKrkAl5fc2LwT1hWiWQMDgSXJkQEqIDdQd3zYBbtYZjy4HjMhP5nKAyTkMGWgWOdNxOTLhvZ7YzzNKtvZYs8fCrJ3J5pWA57rijajJHvFt1UxSOUCVdC1nViazhtK";
                context.Set<InventoryPalletVirtual>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryPalletVirtual v1 = new InventoryPalletVirtual();
            InventoryPalletVirtual v2 = new InventoryPalletVirtual();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Code = "N9fENCWda7yn5Xa9";
                v1.Status = WMS.Model.FrozenStatusEnum.Freezed;
                v1.LocationId = AddBaseWhLocation();
                v1.SysVersion = 37;
                v1.Memo = "F6Y8jLjKKV0eGs9FMLvDRurckVnasA5fP8A5RUxJpZw4pMVzAkShu9yNh0yzjUNLYUkvxQjFsf1fzWyhmQn6k7uXGibr9ka1nzdMaRJeUYglZuLTyhnlNNcJLL6iVTYXrvEPxCV1ywhIkOe46wMJusYaal0ze4Ctt0f6rXBnWBAWrH5Ow9YM2UIHHQZ0T3jkyTkkTV8VoZ3JL8GsJnbyB1byfzMFuWyTd0Ksz9bZZjxLNMzKykE2exy1oQEeoCmEmxIVz5qvfm4SmNvcTBNAbiVPA0NNamgHRR021Q2DyzMtAOgOxYBSCVM4DP04CzYRPnwpO4rIla4kTaqCUXKrkAl5fc2LwT1hWiWQMDgSXJkQEqIDdQd3zYBbtYZjy4HjMhP5nKAyTkMGWgWOdNxOTLhvZ7YzzNKtvZYs8fCrJ3J5pWA57rijajJHvFt1UxSOUCVdC1nViazhtK";
                v2.Code = "PWw2i2ktRJ8lvEve";
                v2.Status = WMS.Model.FrozenStatusEnum.Freezed;
                v2.LocationId = v1.LocationId; 
                v2.SysVersion = 4;
                v2.Memo = "bOrlCYa9yqrqlHpbWzmodxuLkgyVPAsC0r1duajduo6a2pmf5FnyO6Jx7b3hhGX3AX5zKyUC0RWMnW2RW0TvHhHAOwJRZJB3wCp7cYUgeD03nusEEgJacvrcy7f1p1ypvfvgFah1gYgi8rpljTVamiH8UJC1AINjKEO0RPXRrYQEj3loHdY2zrXT9JhOne0PBp3g63QWvOsMsR4Q3siav5ObLZYCBABkhjgErbEmoiq1BOAtxKSZiiU3lO2SADi7SUhp5ZnjtNyKyWGp5YlKNcvki805AK3UsyAJUzqW3olqNXOT9j90726WjMjrRS27U6mAdevMX3D6tF2U1PgRkjMuqP4xKYuvsPegdqTrAxqkDLaBYmDf4EVBXB3iW4jneMATDC2oDeXHLJ7NWO9qM5xqXk7hkDzEJP5dphdTxrPdRf5QGPe8EIA0Emciu5vUoxXMFkWQ";
                context.Set<InventoryPalletVirtual>().Add(v1);
                context.Set<InventoryPalletVirtual>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryPalletVirtual>().Find(v1.ID);
                var data2 = context.Set<InventoryPalletVirtual>().Find(v2.ID);
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

                v.IsProduction = false;
                v.IsSale = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "DI7zU2hk5DBgMgBz5X6tKuxkc4xFToZEhiw26lToYWEMtGdNipjhQgBFt85X1myOf0FFs5zeb0XWf9F904bhvij7BLo39hieOGOIKPfomq0CdayGdiadCWJskHafsoyMgvbl7e5vzlmBo9X64APqwapevxrGStJe3GEr7sUizswfX51LBYRzr46dky19wFtCOh4Y0JbHjtZtUfCVgqwzvDNWuARDkNbIKHNMpx1kO2U7iIvUu8VaxjS35Fv33E6JmcVqT6KjsSPPob93Irxxc6sTpidwqr0BOqtvX0L7X8XaLPPxKFY9JavueJHK83IkJtVLqXf5fMe9vUQYdPHpMaETUz";
                v.Code = "jHE";
                v.Name = "q4X4uTuWoyVta368Sx";
                v.SourceSystemId = "G";
                v.LastUpdateTime = DateTime.Parse("2023-12-16 16:21:15");
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
                v.ShipType = WMS.Model.WhShipTypeEnum.ToSaleCompany;
                v.IsStacking = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "Wlf76YzSBmdBiubBo3lClOtHUO45djC8NA404aD0CiWxhnWIoRNHtFj7ZL9LzootFDGdShyHDEx0q1TPnWBArjAyeZL4JDhSb82TC4VFxYmDbyXc9mRQ7Dk3KkZdPFwmIekuTW9YvEB7AsxD5xce7XbJ8vAi5XhpJM55Sv6D9oFN4i85b9JlZTnjRxlPRyiLsw96DSQuGzcsfh";
                v.Code = "t8BnayQrhT0Xlcx4vRjzD14fdfOhWQ6yHqyXsydHNGE5KBH";
                v.Name = "zibOkDt8LExkJRqhqHdbv1MXwv3";
                v.SourceSystemId = "2K7qlzqBx31pwDzPOzlY8QkfcqntkZnJGHMJi3h9nB3";
                v.LastUpdateTime = DateTime.Parse("2026-05-22 16:21:15");
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

                v.Code = "JQvTuYA4yocTZWCyxLrdrdErQpZhEh2VM6VXFrERtUePRFMi";
                v.Name = "iztGhHEyDJupOECC";
                v.WareHouseId = AddBaseWareHouse();
                v.AreaType = WMS.Model.WhAreaEnum.Rejected;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "cMDokKLU5TZ4vmcARrtmTZ9t7Z9ERLZhsZpoIC82vz7aIEWmzZstmA3f24TyELgo27MS9oZg8DlA0LfdwpPjtYnv7aXHei986kxZsP5kHzC7UdN2Mxj54cN5F9XOJKgSA381MGGOFvlARGxGpZiCPQNv1w1t7sKkz8Mt2qEIUxXwZStDff9pd4WjO0hWk35qooJ3s7QoGLw1fWL3W2m0oy6e48KA6WvenFN9d5Qee0VDh4WSiE0ReEMkvFTkXBYAtDffhj8IhkoL0S3I5KhXRDqIwZFMO92sBqcjTCShjVBTS0wbFM4jYP95n0nZOjX3ixuTdxv3UOK0qmgIbrKKr0er3hX4arvpX";
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

                v.Code = "xNg4xw3Bmjsz2OicqBM";
                v.Name = "20tNS4RxC";
                v.WhAreaId = AddBaseWhArea();
                v.AreaType = WMS.Model.WhLocationEnum.Normal;
                v.Locked = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "SlgffwYe03ijsEEzfszWKJu4sUqDXnDCX9sWFdFtUqnQGDivMCLHYuWhWcRLHiO1XwIrKGJ9BOqCSNfHCNh04j5f9xq47qhQRiCpHXAh052XTQUeKKuVjRwLqpLXnaqKZUOKxwnik4w8Pt8ifLW3QCTYiXRxjt5o0Zt4HUD5dnFPklyg1rp9pO1H9rbFR2157XRj7G6BTTByPO6TOsiudxDjmMcLHKhMLaHT0fJWqFmZMKUpkqYEBEt7kuSZ5IpVM5CJYFeDKxAErxPnOa7X2DPWYQ1PE20AilCmS9Ey4R65co7Web03NrQ";
                context.Set<BaseWhLocation>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
