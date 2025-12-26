using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class InventoryTransferOutDirectApiTest
    {
        private InventoryTransferOutDirectApiController _controller;
        private string _seed;

        public InventoryTransferOutDirectApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryTransferOutDirectApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryTransferOutDirectApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryTransferOutDirectApiVM vm = _controller.Wtm.CreateVM<InventoryTransferOutDirectApiVM>();
            InventoryTransferOutDirect v = new InventoryTransferOutDirect();
            
            v.ErpID = "qNASlw6h06AcvPNUsWqkWGs4wv13Hn7Ksu";
            v.BusinessDate = DateTime.Parse("2026-06-30 13:27:57");
            v.DocTypeId = AddInventoryTransferOutDirectDocType();
            v.DocNo = "RCeotZGc4eFgNHVnUVCLgcr3mboXz";
            v.TransInOrganizationId = AddBaseOrganization();
            v.TransInWhId = AddBaseWareHouse();
            v.TransOutWhId = AddBaseWareHouse();
            v.TransOutOrganizationId = AddBaseOrganization();
            v.Memo = "7uz0fp84LaPagWTd1AchPvYkGRtwuSWCk7cqJPx1pZtlHQWxoQiN26T7ZylLENvUZJWlViUAOvp6VeeoHaedwQ1zxMIoBFFJ5MOkDf3I6aLjwn3HJCQkgzOv8e9QMoLgYjTAvkZWtzPAzs5CkSBeVxIsXg5hFpiwLs3sBBnUSLcmFAB0o8f7GPQ6IRIH74LARUIn5IX70MUu94v14645GmZZeefohwPThgSzut";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryTransferOutDirect>().Find(v.ID);
                
                Assert.AreEqual(data.ErpID, "qNASlw6h06AcvPNUsWqkWGs4wv13Hn7Ksu");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2026-06-30 13:27:57"));
                Assert.AreEqual(data.DocNo, "RCeotZGc4eFgNHVnUVCLgcr3mboXz");
                Assert.AreEqual(data.Memo, "7uz0fp84LaPagWTd1AchPvYkGRtwuSWCk7cqJPx1pZtlHQWxoQiN26T7ZylLENvUZJWlViUAOvp6VeeoHaedwQ1zxMIoBFFJ5MOkDf3I6aLjwn3HJCQkgzOv8e9QMoLgYjTAvkZWtzPAzs5CkSBeVxIsXg5hFpiwLs3sBBnUSLcmFAB0o8f7GPQ6IRIH74LARUIn5IX70MUu94v14645GmZZeefohwPThgSzut");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryTransferOutDirect v = new InventoryTransferOutDirect();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ErpID = "qNASlw6h06AcvPNUsWqkWGs4wv13Hn7Ksu";
                v.BusinessDate = DateTime.Parse("2026-06-30 13:27:57");
                v.DocTypeId = AddInventoryTransferOutDirectDocType();
                v.DocNo = "RCeotZGc4eFgNHVnUVCLgcr3mboXz";
                v.TransInOrganizationId = AddBaseOrganization();
                v.TransInWhId = AddBaseWareHouse();
                v.TransOutWhId = AddBaseWareHouse();
                v.TransOutOrganizationId = AddBaseOrganization();
                v.Memo = "7uz0fp84LaPagWTd1AchPvYkGRtwuSWCk7cqJPx1pZtlHQWxoQiN26T7ZylLENvUZJWlViUAOvp6VeeoHaedwQ1zxMIoBFFJ5MOkDf3I6aLjwn3HJCQkgzOv8e9QMoLgYjTAvkZWtzPAzs5CkSBeVxIsXg5hFpiwLs3sBBnUSLcmFAB0o8f7GPQ6IRIH74LARUIn5IX70MUu94v14645GmZZeefohwPThgSzut";
                context.Set<InventoryTransferOutDirect>().Add(v);
                context.SaveChanges();
            }

            InventoryTransferOutDirectApiVM vm = _controller.Wtm.CreateVM<InventoryTransferOutDirectApiVM>();
            var oldID = v.ID;
            v = new InventoryTransferOutDirect();
            v.ID = oldID;
       		
            v.ErpID = "iHgQfMH73yv51IDwnWDnw5EfEwJB05sziMD";
            v.BusinessDate = DateTime.Parse("2024-07-25 13:27:57");
            v.DocNo = "mf4scusUcANHAK6gRstnSuxfasAXCDVx1lHIE1SXT9";
            v.Memo = "NQ4BJoSNpN4JTZ1Jg8M9ts7xBXntjH7dJeFXHmWk4cWL9ctHVuSXrcgSCTBUzE5cUixd834pL4ReIqtgM9PY4HYeAIIN6d2uJu";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ErpID", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.DocTypeId", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.TransInOrganizationId", "");
            vm.FC.Add("Entity.TransInWhId", "");
            vm.FC.Add("Entity.TransOutWhId", "");
            vm.FC.Add("Entity.TransOutOrganizationId", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryTransferOutDirect>().Find(v.ID);
 				
                Assert.AreEqual(data.ErpID, "iHgQfMH73yv51IDwnWDnw5EfEwJB05sziMD");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-07-25 13:27:57"));
                Assert.AreEqual(data.DocNo, "mf4scusUcANHAK6gRstnSuxfasAXCDVx1lHIE1SXT9");
                Assert.AreEqual(data.Memo, "NQ4BJoSNpN4JTZ1Jg8M9ts7xBXntjH7dJeFXHmWk4cWL9ctHVuSXrcgSCTBUzE5cUixd834pL4ReIqtgM9PY4HYeAIIN6d2uJu");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryTransferOutDirect v = new InventoryTransferOutDirect();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ErpID = "qNASlw6h06AcvPNUsWqkWGs4wv13Hn7Ksu";
                v.BusinessDate = DateTime.Parse("2026-06-30 13:27:57");
                v.DocTypeId = AddInventoryTransferOutDirectDocType();
                v.DocNo = "RCeotZGc4eFgNHVnUVCLgcr3mboXz";
                v.TransInOrganizationId = AddBaseOrganization();
                v.TransInWhId = AddBaseWareHouse();
                v.TransOutWhId = AddBaseWareHouse();
                v.TransOutOrganizationId = AddBaseOrganization();
                v.Memo = "7uz0fp84LaPagWTd1AchPvYkGRtwuSWCk7cqJPx1pZtlHQWxoQiN26T7ZylLENvUZJWlViUAOvp6VeeoHaedwQ1zxMIoBFFJ5MOkDf3I6aLjwn3HJCQkgzOv8e9QMoLgYjTAvkZWtzPAzs5CkSBeVxIsXg5hFpiwLs3sBBnUSLcmFAB0o8f7GPQ6IRIH74LARUIn5IX70MUu94v14645GmZZeefohwPThgSzut";
                context.Set<InventoryTransferOutDirect>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryTransferOutDirect v1 = new InventoryTransferOutDirect();
            InventoryTransferOutDirect v2 = new InventoryTransferOutDirect();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ErpID = "qNASlw6h06AcvPNUsWqkWGs4wv13Hn7Ksu";
                v1.BusinessDate = DateTime.Parse("2026-06-30 13:27:57");
                v1.DocTypeId = AddInventoryTransferOutDirectDocType();
                v1.DocNo = "RCeotZGc4eFgNHVnUVCLgcr3mboXz";
                v1.TransInOrganizationId = AddBaseOrganization();
                v1.TransInWhId = AddBaseWareHouse();
                v1.TransOutWhId = AddBaseWareHouse();
                v1.TransOutOrganizationId = AddBaseOrganization();
                v1.Memo = "7uz0fp84LaPagWTd1AchPvYkGRtwuSWCk7cqJPx1pZtlHQWxoQiN26T7ZylLENvUZJWlViUAOvp6VeeoHaedwQ1zxMIoBFFJ5MOkDf3I6aLjwn3HJCQkgzOv8e9QMoLgYjTAvkZWtzPAzs5CkSBeVxIsXg5hFpiwLs3sBBnUSLcmFAB0o8f7GPQ6IRIH74LARUIn5IX70MUu94v14645GmZZeefohwPThgSzut";
                v2.ErpID = "iHgQfMH73yv51IDwnWDnw5EfEwJB05sziMD";
                v2.BusinessDate = DateTime.Parse("2024-07-25 13:27:57");
                v2.DocTypeId = v1.DocTypeId; 
                v2.DocNo = "mf4scusUcANHAK6gRstnSuxfasAXCDVx1lHIE1SXT9";
                v2.TransInOrganizationId = v1.TransInOrganizationId; 
                v2.TransInWhId = v1.TransInWhId; 
                v2.TransOutWhId = v1.TransOutWhId; 
                v2.TransOutOrganizationId = v1.TransOutOrganizationId; 
                v2.Memo = "NQ4BJoSNpN4JTZ1Jg8M9ts7xBXntjH7dJeFXHmWk4cWL9ctHVuSXrcgSCTBUzE5cUixd834pL4ReIqtgM9PY4HYeAIIN6d2uJu";
                context.Set<InventoryTransferOutDirect>().Add(v1);
                context.Set<InventoryTransferOutDirect>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryTransferOutDirect>().Find(v1.ID);
                var data2 = context.Set<InventoryTransferOutDirect>().Find(v2.ID);
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
                v.Memo = "ULOOu4z2UHFWv9wOxUpDaaVpmvs6kcrGkIgjcCqO6A7URNEeKOiagD6tb1mrgWrlqVKaqFxtDPJmCIxUQ7vr5IM3ni1vY6QRCECOA9feG4WdgsyOXQunnlEE6ucLvxN9T3sbJvSs1SvCM8oOMpccPwKsCYQRs3J2Vws5EiTQl40YqLCv6FpJgDRdFGYilbp4HaQ8jveUNrf7ptDJVboP49xGjKOo3Vfx1y3tryZyiatc9QzrCsAQPCNxgT01ezbhR02KpPYNbzZzgC2f7VYp7aLr1iXYZ1OwWvJAeYUOhkV4nDAblhGJdI8AfBiqfNgJsOumPNhTMXlYIS7glqwx4hWxyvjI0Yg0sd";
                v.Code = "Mo65MGiLuehx3w";
                v.Name = "lia8S1an9zOc7UpiVw74agnftmK1Y0kaS0qnR2Cj3";
                v.SourceSystemId = "GAA9uFqd";
                v.LastUpdateTime = DateTime.Parse("2025-02-11 13:27:57");
                context.Set<BaseOrganization>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddInventoryTransferOutDirectDocType()
        {
            InventoryTransferOutDirectDocType v = new InventoryTransferOutDirectDocType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.CreatePerson = "Wd";
                v.OrganizationId = AddBaseOrganization();
                v.Memo = "lQwgrJmJ66YKwbqmRMssqQGxSnUKFaAwxq38k4sd2CWMPFOglasHUAz7AJaZDscSBPKDVPtOKRlMF7Ny9OUdKTt9d12UMGI52zdLDweQEwBaJZrowaDySF7sdxfeXH";
                v.Code = "mvFiec4lYj9ZoQ5wSwHbLpA";
                v.Name = "nCsJB1MBV";
                v.SourceSystemId = "sAQsst0sf";
                v.LastUpdateTime = DateTime.Parse("2024-07-26 13:27:57");
                context.Set<InventoryTransferOutDirectDocType>().Add(v);
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
                v.IsProduct = true;
                v.ShipType = WMS.Model.WhShipTypeEnum.ToSaleCompany;
                v.IsStacking = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "MkDdVwIPualMqLeFdiHH3oTbtfi9vcmA7wkgjaNO7hcekYetVXLOY5y4cYfOQxl6OlYb5W3IlSfJbqKsR9uyvspAmgFHLn2LUKctgaWtEfGufMHWbEUY3IGv64Kr71Lb20zZPFJygDFIs09g5CgUIQvbgjdHyA6SBoEqwleDOfFZNA3XWeEfLpjPdw739oUrmqagFHKB5RtGFegCL6ptdF3YMXmwRqVCAdrgSzu2Usx1Ocd3frUGXmMU6Z9BkK6Trm219MQ7NLWHSKvwvSU0GPO51fn8TurQgEGq0h7laRwZAOTfz6Z8GH07WvR31MtL1PiVPV9fHMxsQFP9SVkELkfT6qyiRywuLVUkHCAnwiQpLfF6CG29aYPIljp6HqmVFGrfkQZ7YaCiAXNprwO4iCVreHaHnXyEjSC9tVPqhb5W8Kah3tGLWhH392Adjf5Inke5I";
                v.Code = "QNSJdCPdOWE";
                v.Name = "8H1aoxx0sg";
                v.SourceSystemId = "X2ofhiN1";
                v.LastUpdateTime = DateTime.Parse("2024-08-08 13:27:57");
                context.Set<BaseWareHouse>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
