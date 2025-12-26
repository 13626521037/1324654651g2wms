using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class InventoryStockTakingApiTest
    {
        private InventoryStockTakingApiController _controller;
        private string _seed;

        public InventoryStockTakingApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryStockTakingApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryStockTakingApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryStockTakingApiVM vm = _controller.Wtm.CreateVM<InventoryStockTakingApiVM>();
            InventoryStockTaking v = new InventoryStockTaking();
            
            v.ErpID = "AMro";
            v.ErpDocNo = 90;
            v.DocNo = "nIA8Mf87L1o3e87VnZygdoNLKoaaZa675hRWKv";
            v.Dimension = WMS.Model.InventoryStockTakingDimensionEnum.Area;
            v.WhId = AddBaseWareHouse();
            v.SubmitTime = DateTime.Parse("2026-05-02 09:44:46");
            v.SubmitUser = "NO061ffPxSVNZty4Qsg9CSPVRSxQNu64AQhupc";
            v.ApproveTime = DateTime.Parse("2025-11-13 09:44:46");
            v.ApproveUser = "bOxxZ4b8MBMTXu";
            v.CloseTime = DateTime.Parse("2025-01-24 09:44:46");
            v.CloseUser = "vVNgDz6pM7ynvFzqilnapjCESLnKE01dXXzfgKXcUa4fA";
            v.Status = WMS.Model.InventoryStockTakingStatusEnum.Approving;
            v.Memo = "TzZcidx1OehMD6XwSVGk6gMDdLUJOiEW4WoMAcIjk2TNEZTqkgUxs9FAMPuCIuXwrIhiu2AwnT3CAWlvS81X4O12nXPZtCfLr6OX1VAxWTCUU2f7bJlNVHktPpC0DH7b";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryStockTaking>().Find(v.ID);
                
                Assert.AreEqual(data.ErpID, "AMro");
                Assert.AreEqual(data.ErpDocNo, 90);
                Assert.AreEqual(data.DocNo, "nIA8Mf87L1o3e87VnZygdoNLKoaaZa675hRWKv");
                Assert.AreEqual(data.Dimension, WMS.Model.InventoryStockTakingDimensionEnum.Area);
                Assert.AreEqual(data.SubmitTime, DateTime.Parse("2026-05-02 09:44:46"));
                Assert.AreEqual(data.SubmitUser, "NO061ffPxSVNZty4Qsg9CSPVRSxQNu64AQhupc");
                Assert.AreEqual(data.ApproveTime, DateTime.Parse("2025-11-13 09:44:46"));
                Assert.AreEqual(data.ApproveUser, "bOxxZ4b8MBMTXu");
                Assert.AreEqual(data.CloseTime, DateTime.Parse("2025-01-24 09:44:46"));
                Assert.AreEqual(data.CloseUser, "vVNgDz6pM7ynvFzqilnapjCESLnKE01dXXzfgKXcUa4fA");
                Assert.AreEqual(data.Status, WMS.Model.InventoryStockTakingStatusEnum.Approving);
                Assert.AreEqual(data.Memo, "TzZcidx1OehMD6XwSVGk6gMDdLUJOiEW4WoMAcIjk2TNEZTqkgUxs9FAMPuCIuXwrIhiu2AwnT3CAWlvS81X4O12nXPZtCfLr6OX1VAxWTCUU2f7bJlNVHktPpC0DH7b");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryStockTaking v = new InventoryStockTaking();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ErpID = "AMro";
                v.ErpDocNo = 90;
                v.DocNo = "nIA8Mf87L1o3e87VnZygdoNLKoaaZa675hRWKv";
                v.Dimension = WMS.Model.InventoryStockTakingDimensionEnum.Area;
                v.WhId = AddBaseWareHouse();
                v.SubmitTime = DateTime.Parse("2026-05-02 09:44:46");
                v.SubmitUser = "NO061ffPxSVNZty4Qsg9CSPVRSxQNu64AQhupc";
                v.ApproveTime = DateTime.Parse("2025-11-13 09:44:46");
                v.ApproveUser = "bOxxZ4b8MBMTXu";
                v.CloseTime = DateTime.Parse("2025-01-24 09:44:46");
                v.CloseUser = "vVNgDz6pM7ynvFzqilnapjCESLnKE01dXXzfgKXcUa4fA";
                v.Status = WMS.Model.InventoryStockTakingStatusEnum.Approving;
                v.Memo = "TzZcidx1OehMD6XwSVGk6gMDdLUJOiEW4WoMAcIjk2TNEZTqkgUxs9FAMPuCIuXwrIhiu2AwnT3CAWlvS81X4O12nXPZtCfLr6OX1VAxWTCUU2f7bJlNVHktPpC0DH7b";
                context.Set<InventoryStockTaking>().Add(v);
                context.SaveChanges();
            }

            InventoryStockTakingApiVM vm = _controller.Wtm.CreateVM<InventoryStockTakingApiVM>();
            var oldID = v.ID;
            v = new InventoryStockTaking();
            v.ID = oldID;
       		
            v.ErpID = "0UOAzFQBsTEK6yZzDxFP2895J4BIJUj03kIwK9BpCNeu9KX";
            v.ErpDocNo = 73;
            v.DocNo = "AUX5uQknVkRQ9k6jVB0qHx5SBRPUjXAKQILf4TFKFQL1Xq";
            v.Dimension = WMS.Model.InventoryStockTakingDimensionEnum.Area;
            v.SubmitTime = DateTime.Parse("2026-01-02 09:44:46");
            v.SubmitUser = "I79BPTlxzTCpQqKb1JnrSGW5XDVGjJLp4GVWsakqehSh52";
            v.ApproveTime = DateTime.Parse("2024-08-19 09:44:46");
            v.ApproveUser = "9VXAaWGQdx6p46G9aob0zjSqVTMi3UlV";
            v.CloseTime = DateTime.Parse("2026-02-07 09:44:46");
            v.CloseUser = "yF13yP9kWqMq8hsCVRtvfX6l8M0";
            v.Status = WMS.Model.InventoryStockTakingStatusEnum.Approved;
            v.Memo = "Qwi3oYsgamUH8eI2vIKfFkq6rC1JaDfcYpQra616ZV7f4sP8yieJJYY37CNQMJYKWxPqdoyfvsLIySYWpYCXmcck3nADegzoyInzIX91eWUmgg9jyIDWYEXwcVzUlHUEHE08poE14SSLZTPQhiVVpVmgGVK6tD0CQHgwlU0N1b7Devzj3jQ3ZK98a7VTGM2xRtRVwHsrt3wmAUE7VFD9p9XP14qNAYuiQRe6pUsugNMRXdBtkXIah0Qt2aqcvVCwr53vzj9x3Tx1z1wkxzxnTiumKtlWs2SQkkvcteXNoYXZNAA8brcLgB2NkIxUXfMRHfVKkS0Z8SJL";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ErpID", "");
            vm.FC.Add("Entity.ErpDocNo", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.Dimension", "");
            vm.FC.Add("Entity.WhId", "");
            vm.FC.Add("Entity.SubmitTime", "");
            vm.FC.Add("Entity.SubmitUser", "");
            vm.FC.Add("Entity.ApproveTime", "");
            vm.FC.Add("Entity.ApproveUser", "");
            vm.FC.Add("Entity.CloseTime", "");
            vm.FC.Add("Entity.CloseUser", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryStockTaking>().Find(v.ID);
 				
                Assert.AreEqual(data.ErpID, "0UOAzFQBsTEK6yZzDxFP2895J4BIJUj03kIwK9BpCNeu9KX");
                Assert.AreEqual(data.ErpDocNo, 73);
                Assert.AreEqual(data.DocNo, "AUX5uQknVkRQ9k6jVB0qHx5SBRPUjXAKQILf4TFKFQL1Xq");
                Assert.AreEqual(data.Dimension, WMS.Model.InventoryStockTakingDimensionEnum.Area);
                Assert.AreEqual(data.SubmitTime, DateTime.Parse("2026-01-02 09:44:46"));
                Assert.AreEqual(data.SubmitUser, "I79BPTlxzTCpQqKb1JnrSGW5XDVGjJLp4GVWsakqehSh52");
                Assert.AreEqual(data.ApproveTime, DateTime.Parse("2024-08-19 09:44:46"));
                Assert.AreEqual(data.ApproveUser, "9VXAaWGQdx6p46G9aob0zjSqVTMi3UlV");
                Assert.AreEqual(data.CloseTime, DateTime.Parse("2026-02-07 09:44:46"));
                Assert.AreEqual(data.CloseUser, "yF13yP9kWqMq8hsCVRtvfX6l8M0");
                Assert.AreEqual(data.Status, WMS.Model.InventoryStockTakingStatusEnum.Approved);
                Assert.AreEqual(data.Memo, "Qwi3oYsgamUH8eI2vIKfFkq6rC1JaDfcYpQra616ZV7f4sP8yieJJYY37CNQMJYKWxPqdoyfvsLIySYWpYCXmcck3nADegzoyInzIX91eWUmgg9jyIDWYEXwcVzUlHUEHE08poE14SSLZTPQhiVVpVmgGVK6tD0CQHgwlU0N1b7Devzj3jQ3ZK98a7VTGM2xRtRVwHsrt3wmAUE7VFD9p9XP14qNAYuiQRe6pUsugNMRXdBtkXIah0Qt2aqcvVCwr53vzj9x3Tx1z1wkxzxnTiumKtlWs2SQkkvcteXNoYXZNAA8brcLgB2NkIxUXfMRHfVKkS0Z8SJL");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryStockTaking v = new InventoryStockTaking();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ErpID = "AMro";
                v.ErpDocNo = 90;
                v.DocNo = "nIA8Mf87L1o3e87VnZygdoNLKoaaZa675hRWKv";
                v.Dimension = WMS.Model.InventoryStockTakingDimensionEnum.Area;
                v.WhId = AddBaseWareHouse();
                v.SubmitTime = DateTime.Parse("2026-05-02 09:44:46");
                v.SubmitUser = "NO061ffPxSVNZty4Qsg9CSPVRSxQNu64AQhupc";
                v.ApproveTime = DateTime.Parse("2025-11-13 09:44:46");
                v.ApproveUser = "bOxxZ4b8MBMTXu";
                v.CloseTime = DateTime.Parse("2025-01-24 09:44:46");
                v.CloseUser = "vVNgDz6pM7ynvFzqilnapjCESLnKE01dXXzfgKXcUa4fA";
                v.Status = WMS.Model.InventoryStockTakingStatusEnum.Approving;
                v.Memo = "TzZcidx1OehMD6XwSVGk6gMDdLUJOiEW4WoMAcIjk2TNEZTqkgUxs9FAMPuCIuXwrIhiu2AwnT3CAWlvS81X4O12nXPZtCfLr6OX1VAxWTCUU2f7bJlNVHktPpC0DH7b";
                context.Set<InventoryStockTaking>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryStockTaking v1 = new InventoryStockTaking();
            InventoryStockTaking v2 = new InventoryStockTaking();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ErpID = "AMro";
                v1.ErpDocNo = 90;
                v1.DocNo = "nIA8Mf87L1o3e87VnZygdoNLKoaaZa675hRWKv";
                v1.Dimension = WMS.Model.InventoryStockTakingDimensionEnum.Area;
                v1.WhId = AddBaseWareHouse();
                v1.SubmitTime = DateTime.Parse("2026-05-02 09:44:46");
                v1.SubmitUser = "NO061ffPxSVNZty4Qsg9CSPVRSxQNu64AQhupc";
                v1.ApproveTime = DateTime.Parse("2025-11-13 09:44:46");
                v1.ApproveUser = "bOxxZ4b8MBMTXu";
                v1.CloseTime = DateTime.Parse("2025-01-24 09:44:46");
                v1.CloseUser = "vVNgDz6pM7ynvFzqilnapjCESLnKE01dXXzfgKXcUa4fA";
                v1.Status = WMS.Model.InventoryStockTakingStatusEnum.Approving;
                v1.Memo = "TzZcidx1OehMD6XwSVGk6gMDdLUJOiEW4WoMAcIjk2TNEZTqkgUxs9FAMPuCIuXwrIhiu2AwnT3CAWlvS81X4O12nXPZtCfLr6OX1VAxWTCUU2f7bJlNVHktPpC0DH7b";
                v2.ErpID = "0UOAzFQBsTEK6yZzDxFP2895J4BIJUj03kIwK9BpCNeu9KX";
                v2.ErpDocNo = 73;
                v2.DocNo = "AUX5uQknVkRQ9k6jVB0qHx5SBRPUjXAKQILf4TFKFQL1Xq";
                v2.Dimension = WMS.Model.InventoryStockTakingDimensionEnum.Area;
                v2.WhId = v1.WhId; 
                v2.SubmitTime = DateTime.Parse("2026-01-02 09:44:46");
                v2.SubmitUser = "I79BPTlxzTCpQqKb1JnrSGW5XDVGjJLp4GVWsakqehSh52";
                v2.ApproveTime = DateTime.Parse("2024-08-19 09:44:46");
                v2.ApproveUser = "9VXAaWGQdx6p46G9aob0zjSqVTMi3UlV";
                v2.CloseTime = DateTime.Parse("2026-02-07 09:44:46");
                v2.CloseUser = "yF13yP9kWqMq8hsCVRtvfX6l8M0";
                v2.Status = WMS.Model.InventoryStockTakingStatusEnum.Approved;
                v2.Memo = "Qwi3oYsgamUH8eI2vIKfFkq6rC1JaDfcYpQra616ZV7f4sP8yieJJYY37CNQMJYKWxPqdoyfvsLIySYWpYCXmcck3nADegzoyInzIX91eWUmgg9jyIDWYEXwcVzUlHUEHE08poE14SSLZTPQhiVVpVmgGVK6tD0CQHgwlU0N1b7Devzj3jQ3ZK98a7VTGM2xRtRVwHsrt3wmAUE7VFD9p9XP14qNAYuiQRe6pUsugNMRXdBtkXIah0Qt2aqcvVCwr53vzj9x3Tx1z1wkxzxnTiumKtlWs2SQkkvcteXNoYXZNAA8brcLgB2NkIxUXfMRHfVKkS0Z8SJL";
                context.Set<InventoryStockTaking>().Add(v1);
                context.Set<InventoryStockTaking>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryStockTaking>().Find(v1.ID);
                var data2 = context.Set<InventoryStockTaking>().Find(v2.ID);
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
                v.IsSale = false;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "I1NRZ7Y2AExm9CUKw0Ms9sjMKW1r3LgClJoJPsr0Co0lMiSeFXm42gtQSuCTsnhldr29z3uG2LWTNwjSPYxhx3BR2K6XnES6vrNopkKgGzNtBROFVkumwBHVYHNk6crguwaJtgynl5VJzCYGOInvFiNj83EXhvQsjRoSG2NuhjXGW4rLCewk5vs29ADJ0opupWRJTFy14hbcVNuH1eVkGCImL1dAvTU8rzFf6nEt9R6J9dKkAJtgyRuBxYc2FXp5OSpLqMEDOOFPAp4OnPmbq7E0FhOD6aXrgtA6oiwgnmaLNLPgkDLjXVXcf8WZ8IP5sS6QDKtE71WkyheAMmcfgzWeEaA4zx3eq";
                v.Code = "2ZNj4vm5APX6zxBOoXPptujPKmqxKACDWm";
                v.Name = "JSJSLC7FUt23rgMJjhYbvUyj7qlNJPqyBY4ta0JIBDlTVt";
                v.SourceSystemId = "8VdTMAV1R1zj3DG";
                v.LastUpdateTime = DateTime.Parse("2024-07-17 09:44:46");
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
                v.ShipType = WMS.Model.WhShipTypeEnum.WaitToShip;
                v.IsStacking = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "3FMHhO0wyzo1uvMrfsVf0TnJT7qwfW6WDJuHoX3AqFUTwwuJuzqqo3StilcfmOp8ylJiSJ5JDTutzD5JHJsMZnO832inZpD8W37tfpzBceL1RrOrTh2vFctiYUemNGHIhznnCHVwrAU5BFnyT2N1WVcBiKS6oyJUovaDD6jfzXqtkHoff56jLOM8VF";
                v.Code = "VBGodczcaGRikdd1O2gzxnBzGggPhEjXJghxVYdUcEsaXI";
                v.Name = "yobWctA";
                v.SourceSystemId = "H";
                v.LastUpdateTime = DateTime.Parse("2026-09-16 09:44:46");
                context.Set<BaseWareHouse>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
