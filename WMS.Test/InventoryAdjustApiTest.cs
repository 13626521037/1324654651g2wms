using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryAdjustVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;


namespace WMS.Test
{
    [TestClass]
    public class InventoryAdjustApiTest
    {
        private InventoryAdjustApiController _controller;
        private string _seed;

        public InventoryAdjustApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryAdjustApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryAdjustApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryAdjustApiVM vm = _controller.Wtm.CreateVM<InventoryAdjustApiVM>();
            InventoryAdjust v = new InventoryAdjust();
            
            v.StockTakingId = AddInventoryStockTaking();
            v.DocNo = "sIFnOUXrnHjwiqmwpT8rUhSG9aNn3XFCKLAvA";
            v.Memo = "6uXRXB9gxTMs4dskMQbISeRMh9gTcJI2zfVSQ1vUYP2fF6KNx5P6SWJytkp2TXewEqNtsubGwYgBsaOibdmoP8ED1E2LEvF5jLQWzt02E5rYpLNnEDOTgrxNPMyP5we14QTzy8RKvphx9yyS9nSYIE0GHEWJDxUcqbQK00OBvUsPkmgSXoxbZRN8b4QooFqTfSwXx9EYiNPN5j46uLknYvEK99R0lIYEyQhYJXpVXObmlQ54NrTdw5fnGOkIGg1L7TonuqPSlFeM1c03tOK5voo8HRKv729MDGHrM04T7ZXFMbOfAZMF7I4";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryAdjust>().Find(v.ID);
                
                Assert.AreEqual(data.DocNo, "sIFnOUXrnHjwiqmwpT8rUhSG9aNn3XFCKLAvA");
                Assert.AreEqual(data.Memo, "6uXRXB9gxTMs4dskMQbISeRMh9gTcJI2zfVSQ1vUYP2fF6KNx5P6SWJytkp2TXewEqNtsubGwYgBsaOibdmoP8ED1E2LEvF5jLQWzt02E5rYpLNnEDOTgrxNPMyP5we14QTzy8RKvphx9yyS9nSYIE0GHEWJDxUcqbQK00OBvUsPkmgSXoxbZRN8b4QooFqTfSwXx9EYiNPN5j46uLknYvEK99R0lIYEyQhYJXpVXObmlQ54NrTdw5fnGOkIGg1L7TonuqPSlFeM1c03tOK5voo8HRKv729MDGHrM04T7ZXFMbOfAZMF7I4");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryAdjust v = new InventoryAdjust();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.StockTakingId = AddInventoryStockTaking();
                v.DocNo = "sIFnOUXrnHjwiqmwpT8rUhSG9aNn3XFCKLAvA";
                v.Memo = "6uXRXB9gxTMs4dskMQbISeRMh9gTcJI2zfVSQ1vUYP2fF6KNx5P6SWJytkp2TXewEqNtsubGwYgBsaOibdmoP8ED1E2LEvF5jLQWzt02E5rYpLNnEDOTgrxNPMyP5we14QTzy8RKvphx9yyS9nSYIE0GHEWJDxUcqbQK00OBvUsPkmgSXoxbZRN8b4QooFqTfSwXx9EYiNPN5j46uLknYvEK99R0lIYEyQhYJXpVXObmlQ54NrTdw5fnGOkIGg1L7TonuqPSlFeM1c03tOK5voo8HRKv729MDGHrM04T7ZXFMbOfAZMF7I4";
                context.Set<InventoryAdjust>().Add(v);
                context.SaveChanges();
            }

            InventoryAdjustApiVM vm = _controller.Wtm.CreateVM<InventoryAdjustApiVM>();
            var oldID = v.ID;
            v = new InventoryAdjust();
            v.ID = oldID;
       		
            v.DocNo = "IqScZzyvVK2Ieva8daCMqGqa8i6L8U";
            v.Memo = "IKpC2YOCEb8MZQzrHSgykZabQyh6L795ZORe9zk3GTkdiQY7CQSJBiGbG5MwqVsD0FCQBwzAAZ7RePcELlmUoLLoAqXmIGSgjFrpQWB88fHoajGu4bcVrLSNOL1V2OTAkXvC8Ax4wrRJbNw4gw8C7HChz7cByplFOAOJvwh3ztRZe4oE6NWkpfgkCd9s8j5kQ9t3bJo9qU06UN4sAIz3VznQDouCeHTzpRNxlJKjNAwO2Xtw6A3480qv5mzw10zfgnrm92l1mRvO4trUOVJmPKB9BovFzu79R76vFxitK3X6BrdFz8L17JJiw7DYjOp7GzfEQ0HESM0ZzQJdny7R9nsutq1Rab5R4O";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.StockTakingId", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryAdjust>().Find(v.ID);
 				
                Assert.AreEqual(data.DocNo, "IqScZzyvVK2Ieva8daCMqGqa8i6L8U");
                Assert.AreEqual(data.Memo, "IKpC2YOCEb8MZQzrHSgykZabQyh6L795ZORe9zk3GTkdiQY7CQSJBiGbG5MwqVsD0FCQBwzAAZ7RePcELlmUoLLoAqXmIGSgjFrpQWB88fHoajGu4bcVrLSNOL1V2OTAkXvC8Ax4wrRJbNw4gw8C7HChz7cByplFOAOJvwh3ztRZe4oE6NWkpfgkCd9s8j5kQ9t3bJo9qU06UN4sAIz3VznQDouCeHTzpRNxlJKjNAwO2Xtw6A3480qv5mzw10zfgnrm92l1mRvO4trUOVJmPKB9BovFzu79R76vFxitK3X6BrdFz8L17JJiw7DYjOp7GzfEQ0HESM0ZzQJdny7R9nsutq1Rab5R4O");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryAdjust v = new InventoryAdjust();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.StockTakingId = AddInventoryStockTaking();
                v.DocNo = "sIFnOUXrnHjwiqmwpT8rUhSG9aNn3XFCKLAvA";
                v.Memo = "6uXRXB9gxTMs4dskMQbISeRMh9gTcJI2zfVSQ1vUYP2fF6KNx5P6SWJytkp2TXewEqNtsubGwYgBsaOibdmoP8ED1E2LEvF5jLQWzt02E5rYpLNnEDOTgrxNPMyP5we14QTzy8RKvphx9yyS9nSYIE0GHEWJDxUcqbQK00OBvUsPkmgSXoxbZRN8b4QooFqTfSwXx9EYiNPN5j46uLknYvEK99R0lIYEyQhYJXpVXObmlQ54NrTdw5fnGOkIGg1L7TonuqPSlFeM1c03tOK5voo8HRKv729MDGHrM04T7ZXFMbOfAZMF7I4";
                context.Set<InventoryAdjust>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryAdjust v1 = new InventoryAdjust();
            InventoryAdjust v2 = new InventoryAdjust();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.StockTakingId = AddInventoryStockTaking();
                v1.DocNo = "sIFnOUXrnHjwiqmwpT8rUhSG9aNn3XFCKLAvA";
                v1.Memo = "6uXRXB9gxTMs4dskMQbISeRMh9gTcJI2zfVSQ1vUYP2fF6KNx5P6SWJytkp2TXewEqNtsubGwYgBsaOibdmoP8ED1E2LEvF5jLQWzt02E5rYpLNnEDOTgrxNPMyP5we14QTzy8RKvphx9yyS9nSYIE0GHEWJDxUcqbQK00OBvUsPkmgSXoxbZRN8b4QooFqTfSwXx9EYiNPN5j46uLknYvEK99R0lIYEyQhYJXpVXObmlQ54NrTdw5fnGOkIGg1L7TonuqPSlFeM1c03tOK5voo8HRKv729MDGHrM04T7ZXFMbOfAZMF7I4";
                v2.StockTakingId = v1.StockTakingId; 
                v2.DocNo = "IqScZzyvVK2Ieva8daCMqGqa8i6L8U";
                v2.Memo = "IKpC2YOCEb8MZQzrHSgykZabQyh6L795ZORe9zk3GTkdiQY7CQSJBiGbG5MwqVsD0FCQBwzAAZ7RePcELlmUoLLoAqXmIGSgjFrpQWB88fHoajGu4bcVrLSNOL1V2OTAkXvC8Ax4wrRJbNw4gw8C7HChz7cByplFOAOJvwh3ztRZe4oE6NWkpfgkCd9s8j5kQ9t3bJo9qU06UN4sAIz3VznQDouCeHTzpRNxlJKjNAwO2Xtw6A3480qv5mzw10zfgnrm92l1mRvO4trUOVJmPKB9BovFzu79R76vFxitK3X6BrdFz8L17JJiw7DYjOp7GzfEQ0HESM0ZzQJdny7R9nsutq1Rab5R4O";
                context.Set<InventoryAdjust>().Add(v1);
                context.Set<InventoryAdjust>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryAdjust>().Find(v1.ID);
                var data2 = context.Set<InventoryAdjust>().Find(v2.ID);
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
                v.IsSale = false;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "2cFuM34qe6FaazNgqx5vS4TmbTEQSO2KO66FZKgwJT3IDvvkhnNnCQ2s7rPLuM1Ev9XEmYl9vkFTatfvBi87Tz7AEb5lCgfVxddwFxqXJIUav5sL4uLOqguUSQRNv1eT49zJzujPxsnLaP1BjyZHBjmtIKR5cEH9UTgy2WGPZft71E2l67dLHLqXTxdqzMY0qObgs5pnpoLA4tu8xgF0JsmTkRCplvS34walamH1YliHrfXghUkhFJlBJZ82jKRprzhZimFVT9ZLG9NuA6m4VbDzKOgyMpFIHLL9DmZZ1rPv60en7lnVTzG3Jmt75FaX9ikr3fqMASz1aJGtwuNB1Dz81neLsTLvtqTcsMLXO8dGhB14OsNG2jqMHayllUGWx1pIubuZzEgZe3wpGXSQFPR5ACTIQA4jEeNTG8c02XGnV461v2GdU9on4MX1OChFOn";
                v.Code = "MNdeFpXjgYS5Aiz";
                v.Name = "uhydywiORmIVkBQIHxmEPsPyr510dy";
                v.SourceSystemId = "Bh6nKZBKtnyjmOqHRDbwUMIsO";
                v.LastUpdateTime = DateTime.Parse("2024-05-02 10:28:20");
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
                v.IsProduct = false;
                v.ShipType = WMS.Model.WhShipTypeEnum.ToCustomer;
                v.IsStacking = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "Cxv5VmEFNuglwvpOcmey3cAfeOkZrNGv6OESGj4NHPEjcnEF3XfEcoJYNBvLUGa8cpm3U3aByiYzX6ay44Y7cfWReU4pQpqKnklKw2jLFT8ecVPQKvnvI0HxPMwfxiQJCQvLjdNKQ2x24mOMDaWKoz3bUWrRzMOQGEJP221LgmCzhAeyhFib6qy";
                v.Code = "wCwusx2nPm6p8p";
                v.Name = "l2zJrGnh3WrqfOlP9i6BoYS10qobNBi";
                v.SourceSystemId = "LZYuMZy3";
                v.LastUpdateTime = DateTime.Parse("2026-03-01 10:28:20");
                context.Set<BaseWareHouse>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddInventoryStockTaking()
        {
            InventoryStockTaking v = new InventoryStockTaking();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ErpID = "EBHZMp4uNl0hj2vNM9HyH";
                v.ErpDocNo = 91;
                v.DocNo = "a03Jcd1D7E9UH7p9AEdcUAb24FGwZ2MtSTns";
                v.Dimension = WMS.Model.InventoryStockTakingDimensionEnum.Area;
                v.WhId = AddBaseWareHouse();
                v.SubmitTime = DateTime.Parse("2026-10-29 10:28:20");
                v.SubmitUser = "rUitdwGVp6nLj8oZO";
                v.ApproveTime = DateTime.Parse("2025-05-01 10:28:20");
                v.ApproveUser = "PbOcJu0FXNAID49aLGYOMrAIsb";
                v.CloseTime = DateTime.Parse("2026-09-04 10:28:20");
                v.CloseUser = "vsZUe2R5MDb";
                v.Status = WMS.Model.InventoryStockTakingStatusEnum.Closed;
                v.Memo = "Cmp8rpFxh3tOcW9b9pEfn0x5vcNHaUQmo8fbNemJRpITZLaB2QpPAKf5OXh5nHw6Lj160vEOyHdIsLqLoA8mxTZ7HRPP7QYYLxNwQnQp6HhSQAJBBW1GcVw5lMGQApuAHwp5v8IOvzo3kQMdvhwXW3LwLRvwW5fNIptv8tay4K77tVSzRcuUN3B1rs1P6K42y2yFwR6F2ikKCbYspn9f5NlHV4k75qax1JL2jlxct94k60NDga04K2nEQyflvjeuy4FDppSkSXAvW2IRbTwDZCVhV1fSlGlw5Q2PGbVwDoKSaQ8gBUIQpCHFp7gjWmJT2I4JT0yqpOouJ7PQ7RQrFhhp3hJl85KXasOoAPiTuij35phmLYpRSLWe8d9Y49rQp7blsbnjqknEylccAo1Bp3zwQzralNJ7Dv4XHp5Fd992EXg0SbM3uxNAWXFvBWaOCzglgDWtn4UoMI6Fjpx1TveaaZeH";
                v.Mode = WMS.Model.InventoryStockTakingModeEnum.Wms;
                context.Set<InventoryStockTaking>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
