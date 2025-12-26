using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class InventoryMoveLocationApiTest
    {
        private InventoryMoveLocationApiController _controller;
        private string _seed;

        public InventoryMoveLocationApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryMoveLocationApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryMoveLocationApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryMoveLocationApiVM vm = _controller.Wtm.CreateVM<InventoryMoveLocationApiVM>();
            InventoryMoveLocation v = new InventoryMoveLocation();
            
            v.DocNo = "FodspWT9n7uhswmPEM0t6J9qSIyEXPmQaZLO3kv";
            v.InWhLocationId = AddBaseWhLocation();
            v.Memo = "rjUefYTcgMtr0H3esuxAWnztPRCu7o7xHO7QakxAYzkGJ9oj67CWMXin3ePJqAK4zKkp6MUq5Wan1KcSVBulUfjXKvWglC1sO85WI7ozkN19YWZQ7vRfCWn8xxhccYfFLOoE53WUdTqCx2jQ2OJNxzQul40mUwqBBl7NgvATd4OHwJUkyRfmRJTGTgUm765JpKn6lyRwYJSXwIZtOGgBtGsVQhsF3pp";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryMoveLocation>().Find(v.ID);
                
                Assert.AreEqual(data.DocNo, "FodspWT9n7uhswmPEM0t6J9qSIyEXPmQaZLO3kv");
                Assert.AreEqual(data.Memo, "rjUefYTcgMtr0H3esuxAWnztPRCu7o7xHO7QakxAYzkGJ9oj67CWMXin3ePJqAK4zKkp6MUq5Wan1KcSVBulUfjXKvWglC1sO85WI7ozkN19YWZQ7vRfCWn8xxhccYfFLOoE53WUdTqCx2jQ2OJNxzQul40mUwqBBl7NgvATd4OHwJUkyRfmRJTGTgUm765JpKn6lyRwYJSXwIZtOGgBtGsVQhsF3pp");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryMoveLocation v = new InventoryMoveLocation();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DocNo = "FodspWT9n7uhswmPEM0t6J9qSIyEXPmQaZLO3kv";
                v.InWhLocationId = AddBaseWhLocation();
                v.Memo = "rjUefYTcgMtr0H3esuxAWnztPRCu7o7xHO7QakxAYzkGJ9oj67CWMXin3ePJqAK4zKkp6MUq5Wan1KcSVBulUfjXKvWglC1sO85WI7ozkN19YWZQ7vRfCWn8xxhccYfFLOoE53WUdTqCx2jQ2OJNxzQul40mUwqBBl7NgvATd4OHwJUkyRfmRJTGTgUm765JpKn6lyRwYJSXwIZtOGgBtGsVQhsF3pp";
                context.Set<InventoryMoveLocation>().Add(v);
                context.SaveChanges();
            }

            InventoryMoveLocationApiVM vm = _controller.Wtm.CreateVM<InventoryMoveLocationApiVM>();
            var oldID = v.ID;
            v = new InventoryMoveLocation();
            v.ID = oldID;
       		
            v.DocNo = "zEyMh";
            v.Memo = "gKcJWrEjh";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.InWhLocationId", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryMoveLocation>().Find(v.ID);
 				
                Assert.AreEqual(data.DocNo, "zEyMh");
                Assert.AreEqual(data.Memo, "gKcJWrEjh");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryMoveLocation v = new InventoryMoveLocation();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DocNo = "FodspWT9n7uhswmPEM0t6J9qSIyEXPmQaZLO3kv";
                v.InWhLocationId = AddBaseWhLocation();
                v.Memo = "rjUefYTcgMtr0H3esuxAWnztPRCu7o7xHO7QakxAYzkGJ9oj67CWMXin3ePJqAK4zKkp6MUq5Wan1KcSVBulUfjXKvWglC1sO85WI7ozkN19YWZQ7vRfCWn8xxhccYfFLOoE53WUdTqCx2jQ2OJNxzQul40mUwqBBl7NgvATd4OHwJUkyRfmRJTGTgUm765JpKn6lyRwYJSXwIZtOGgBtGsVQhsF3pp";
                context.Set<InventoryMoveLocation>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryMoveLocation v1 = new InventoryMoveLocation();
            InventoryMoveLocation v2 = new InventoryMoveLocation();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DocNo = "FodspWT9n7uhswmPEM0t6J9qSIyEXPmQaZLO3kv";
                v1.InWhLocationId = AddBaseWhLocation();
                v1.Memo = "rjUefYTcgMtr0H3esuxAWnztPRCu7o7xHO7QakxAYzkGJ9oj67CWMXin3ePJqAK4zKkp6MUq5Wan1KcSVBulUfjXKvWglC1sO85WI7ozkN19YWZQ7vRfCWn8xxhccYfFLOoE53WUdTqCx2jQ2OJNxzQul40mUwqBBl7NgvATd4OHwJUkyRfmRJTGTgUm765JpKn6lyRwYJSXwIZtOGgBtGsVQhsF3pp";
                v2.DocNo = "zEyMh";
                v2.InWhLocationId = v1.InWhLocationId; 
                v2.Memo = "gKcJWrEjh";
                context.Set<InventoryMoveLocation>().Add(v1);
                context.Set<InventoryMoveLocation>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryMoveLocation>().Find(v1.ID);
                var data2 = context.Set<InventoryMoveLocation>().Find(v2.ID);
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
                v.IsSale = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "EtcBYeW0eMROxvzgI51pjVA5WBNLf7J1AZv8iZ6XF1boJdLMEAIA4cfExCXskYv5Gplv";
                v.Code = "sKX";
                v.Name = "tLgNc4Zij5KdNCajMDM1MYakzQb8ntjK";
                v.SourceSystemId = "McmQuzpSuX1lTQoJe2ObAnZserVxbJpb7J8rrBqw06i";
                v.LastUpdateTime = DateTime.Parse("2025-06-18 09:12:21");
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
                v.IsStacking = false;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "Flmwmo8vvtU6IlRUY1mMeY5dqjlkcPW25kcWyE64v2TKU5UxN9ExmBQb7P4aidiZtRF7YuSig901AX7ObowSA5vPc9LihetZD6OdLQel7f7NlmFKkhhby6sa5maV567sNadHpvAf8n4GvTZTY4tCYXEX0nHLUky7f5tm7cvJyv1x98vgt8C1XcUsKigI4Nvu";
                v.Code = "Vbpi1vLkBTo9lA0n4WCFEuoW0X1RbPen";
                v.Name = "rtOMDVD6M9fB6lnV";
                v.SourceSystemId = "mvFP8mYNHg5COOD";
                v.LastUpdateTime = DateTime.Parse("2026-07-20 09:12:21");
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

                v.Code = "qQlw9V";
                v.Name = "Ag98QuThQV5U2UIIIukhUnz1fSl6kF4qhWFaku";
                v.WareHouseId = AddBaseWareHouse();
                v.AreaType = WMS.Model.WhAreaEnum.UnQualified;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "9apiLlBIL6JggxpEwS2g65zSA219hXAAfLQskn9mn8h0L7QXognxy7pvuZhX8uFdEMcynelQUnRCldtKqebxIduCmv2myghrVKG7AObUKGY29xQYHx3oymAzz2k7lCFdKSmnTGvn3ngSXkNfWUrYMdMiABeORNN";
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

                v.Code = "tkypQPAYsKVE0ClWiM0LG2iH0JLjWa9OujU4N";
                v.Name = "W8UIkrB0iWp7ELPT9cDEu4QM8uYX";
                v.WhAreaId = AddBaseWhArea();
                v.AreaType = WMS.Model.WhLocationEnum.Rejected;
                v.Locked = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "aNUrUajHTNeHPOBsVK81Sm9l5wFo5STRhtS7X9ympCX0XxkGN0deIcnvWzS6u1OsOEFwGTtifkhlEO5tybLsPvvyy5MudOyNgOB1VVoADjDiuqRpoBfHdGD4FNdbN87FLdABNupzPzmgUKTGknFa1KX3ZfjGTyHf4ZZ8xRMJ0H1EqrroC3GYybXWBfwtEghiAQhYSlnrGpYopywVmhbwKexVKMNFZ47gAOQs6OkKfQV9q34qfbAbK1klQKkSzcyHBtuYzL9O3iPMiP0ia04uJVuTJPfcHXtefJvOvqQSfhIWrdMU5sOjp5yTQhs78jllUc8FTQSoyR2Zntmj3tWZrLvFPugezoS8Uxm3hFNcf4isQGOt3aB87MBRvlLHbEJZtkZKjfhJSfjTRZH1D526EGcvT35JkZV5gmmLXonf5LDigMbAbPiSAIC1WxSwzzfytcsSD";
                context.Set<BaseWhLocation>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
