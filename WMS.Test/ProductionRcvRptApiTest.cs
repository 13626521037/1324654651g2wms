using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs;
using WMS.Model.ProductionManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class ProductionRcvRptApiTest
    {
        private ProductionRcvRptApiController _controller;
        private string _seed;

        public ProductionRcvRptApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<ProductionRcvRptApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new ProductionRcvRptApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            ProductionRcvRptApiVM vm = _controller.Wtm.CreateVM<ProductionRcvRptApiVM>();
            ProductionRcvRpt v = new ProductionRcvRpt();
            
            v.ErpID = "aYfueLNUJ9SCz3BIVcVA5nJa4c5B9tEF";
            v.OrganizationId = AddBaseOrganization();
            v.BusinessDate = DateTime.Parse("2026-07-02 10:31:58");
            v.DocNo = "iOBBwf8vVp";
            v.WhId = AddBaseWareHouse();
            v.OrderWhId = AddBaseWareHouse();
            v.Status = WMS.Model.ProductionRcvRptStatusEnum.AllReceive;
            v.Memo = "yYcJgkuNVMZ4ThJYxtd6F43AWGjGlZsSjMjnX3WtYJQHhM764W1T95USBfOT0NxgxaCe17L6N5jhqIfXyRa2sbUGcF71cDipVz0K0cUfqjqorsZ3Oqo8HxskMREgHmF2v8kVdhBXpj3wvlDXHXVyhzjB0uBfUqpHbsyUbZeVYOZiVrwYtiK5bKL7HJbJZRhzw5xnm9WSbyfqT6yb22j8400EvM1rl0Bow1LJDiJDyXxkD9lm4gCQOoGpYIMbQhI5Bx90Jfz7Hk7r";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ProductionRcvRpt>().Find(v.ID);
                
                Assert.AreEqual(data.ErpID, "aYfueLNUJ9SCz3BIVcVA5nJa4c5B9tEF");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2026-07-02 10:31:58"));
                Assert.AreEqual(data.DocNo, "iOBBwf8vVp");
                Assert.AreEqual(data.Status, WMS.Model.ProductionRcvRptStatusEnum.AllReceive);
                Assert.AreEqual(data.Memo, "yYcJgkuNVMZ4ThJYxtd6F43AWGjGlZsSjMjnX3WtYJQHhM764W1T95USBfOT0NxgxaCe17L6N5jhqIfXyRa2sbUGcF71cDipVz0K0cUfqjqorsZ3Oqo8HxskMREgHmF2v8kVdhBXpj3wvlDXHXVyhzjB0uBfUqpHbsyUbZeVYOZiVrwYtiK5bKL7HJbJZRhzw5xnm9WSbyfqT6yb22j8400EvM1rl0Bow1LJDiJDyXxkD9lm4gCQOoGpYIMbQhI5Bx90Jfz7Hk7r");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            ProductionRcvRpt v = new ProductionRcvRpt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ErpID = "aYfueLNUJ9SCz3BIVcVA5nJa4c5B9tEF";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2026-07-02 10:31:58");
                v.DocNo = "iOBBwf8vVp";
                v.WhId = AddBaseWareHouse();
                v.OrderWhId = AddBaseWareHouse();
                v.Status = WMS.Model.ProductionRcvRptStatusEnum.AllReceive;
                v.Memo = "yYcJgkuNVMZ4ThJYxtd6F43AWGjGlZsSjMjnX3WtYJQHhM764W1T95USBfOT0NxgxaCe17L6N5jhqIfXyRa2sbUGcF71cDipVz0K0cUfqjqorsZ3Oqo8HxskMREgHmF2v8kVdhBXpj3wvlDXHXVyhzjB0uBfUqpHbsyUbZeVYOZiVrwYtiK5bKL7HJbJZRhzw5xnm9WSbyfqT6yb22j8400EvM1rl0Bow1LJDiJDyXxkD9lm4gCQOoGpYIMbQhI5Bx90Jfz7Hk7r";
                context.Set<ProductionRcvRpt>().Add(v);
                context.SaveChanges();
            }

            ProductionRcvRptApiVM vm = _controller.Wtm.CreateVM<ProductionRcvRptApiVM>();
            var oldID = v.ID;
            v = new ProductionRcvRpt();
            v.ID = oldID;
       		
            v.ErpID = "82HJeWSptPlEqUIB3VXUfEZvFVfhH";
            v.BusinessDate = DateTime.Parse("2026-01-05 10:31:58");
            v.DocNo = "P6k98xtwj";
            v.Status = WMS.Model.ProductionRcvRptStatusEnum.Reported;
            v.Memo = "vgP0Jvbo0d7gFu3yQHeRISOxcMlItmYbZJRXltsdMMfnWOQcsFnOH0I0jjSilhb6o5ncyIb2G9jvf34SJvWkgZgCzc7x6xkoltEhggzHciJmZKwhcEg33WX86mJXORHzlKj74fkXlxUwmeFL5zM1XlyX";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ErpID", "");
            vm.FC.Add("Entity.OrganizationId", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.WhId", "");
            vm.FC.Add("Entity.OrderWhId", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ProductionRcvRpt>().Find(v.ID);
 				
                Assert.AreEqual(data.ErpID, "82HJeWSptPlEqUIB3VXUfEZvFVfhH");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2026-01-05 10:31:58"));
                Assert.AreEqual(data.DocNo, "P6k98xtwj");
                Assert.AreEqual(data.Status, WMS.Model.ProductionRcvRptStatusEnum.Reported);
                Assert.AreEqual(data.Memo, "vgP0Jvbo0d7gFu3yQHeRISOxcMlItmYbZJRXltsdMMfnWOQcsFnOH0I0jjSilhb6o5ncyIb2G9jvf34SJvWkgZgCzc7x6xkoltEhggzHciJmZKwhcEg33WX86mJXORHzlKj74fkXlxUwmeFL5zM1XlyX");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            ProductionRcvRpt v = new ProductionRcvRpt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ErpID = "aYfueLNUJ9SCz3BIVcVA5nJa4c5B9tEF";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2026-07-02 10:31:58");
                v.DocNo = "iOBBwf8vVp";
                v.WhId = AddBaseWareHouse();
                v.OrderWhId = AddBaseWareHouse();
                v.Status = WMS.Model.ProductionRcvRptStatusEnum.AllReceive;
                v.Memo = "yYcJgkuNVMZ4ThJYxtd6F43AWGjGlZsSjMjnX3WtYJQHhM764W1T95USBfOT0NxgxaCe17L6N5jhqIfXyRa2sbUGcF71cDipVz0K0cUfqjqorsZ3Oqo8HxskMREgHmF2v8kVdhBXpj3wvlDXHXVyhzjB0uBfUqpHbsyUbZeVYOZiVrwYtiK5bKL7HJbJZRhzw5xnm9WSbyfqT6yb22j8400EvM1rl0Bow1LJDiJDyXxkD9lm4gCQOoGpYIMbQhI5Bx90Jfz7Hk7r";
                context.Set<ProductionRcvRpt>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            ProductionRcvRpt v1 = new ProductionRcvRpt();
            ProductionRcvRpt v2 = new ProductionRcvRpt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ErpID = "aYfueLNUJ9SCz3BIVcVA5nJa4c5B9tEF";
                v1.OrganizationId = AddBaseOrganization();
                v1.BusinessDate = DateTime.Parse("2026-07-02 10:31:58");
                v1.DocNo = "iOBBwf8vVp";
                v1.WhId = AddBaseWareHouse();
                v1.OrderWhId = AddBaseWareHouse();
                v1.Status = WMS.Model.ProductionRcvRptStatusEnum.AllReceive;
                v1.Memo = "yYcJgkuNVMZ4ThJYxtd6F43AWGjGlZsSjMjnX3WtYJQHhM764W1T95USBfOT0NxgxaCe17L6N5jhqIfXyRa2sbUGcF71cDipVz0K0cUfqjqorsZ3Oqo8HxskMREgHmF2v8kVdhBXpj3wvlDXHXVyhzjB0uBfUqpHbsyUbZeVYOZiVrwYtiK5bKL7HJbJZRhzw5xnm9WSbyfqT6yb22j8400EvM1rl0Bow1LJDiJDyXxkD9lm4gCQOoGpYIMbQhI5Bx90Jfz7Hk7r";
                v2.ErpID = "82HJeWSptPlEqUIB3VXUfEZvFVfhH";
                v2.OrganizationId = v1.OrganizationId; 
                v2.BusinessDate = DateTime.Parse("2026-01-05 10:31:58");
                v2.DocNo = "P6k98xtwj";
                v2.WhId = v1.WhId; 
                v2.OrderWhId = v1.OrderWhId; 
                v2.Status = WMS.Model.ProductionRcvRptStatusEnum.Reported;
                v2.Memo = "vgP0Jvbo0d7gFu3yQHeRISOxcMlItmYbZJRXltsdMMfnWOQcsFnOH0I0jjSilhb6o5ncyIb2G9jvf34SJvWkgZgCzc7x6xkoltEhggzHciJmZKwhcEg33WX86mJXORHzlKj74fkXlxUwmeFL5zM1XlyX";
                context.Set<ProductionRcvRpt>().Add(v1);
                context.Set<ProductionRcvRpt>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<ProductionRcvRpt>().Find(v1.ID);
                var data2 = context.Set<ProductionRcvRpt>().Find(v2.ID);
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
                v.Memo = "IK8G1C78NaEBy5Tf0WbmnLGzBIjAmYLrOj51NxnETvLK1ZLFahkV9OAJANVJbMuvsMyX0NUGQwuEVo7nlVGXn6PpmathyVGAYgmGBekrGtRkYXJn9X05wZGr4iljXN6fCIVdxqxbL5rCxQUfpVILamwxlajpVsOcQEo5iFby4ZItSzLCFVjrL3QWSGeWoPO4U4ecDFKW5tMBUJ6ZpVt0l0dDX0ybPSHoxMd7kPhOuFOqy20hsOBoT4Y3SYdWAYsrX63brAwji9GEx6JIxME0b4DT9B98SDJ9VM81wc7soujHvbYIdYevyvO1Y8BWzdXCGJ6b6DU6Dw2sbsBWgFiNa0ylv5OLyELKoLt32DpNaRR1ieGVtCwItoxuPwThRIp9ezZeYvS2A9qYSDz53pjukFaZbis4fOPh9YajdDH7Q88Tn71cEmNiGxX6iJ";
                v.Code = "HFcwYOF";
                v.Name = "XFMeUo3W";
                v.SourceSystemId = "cEwr";
                v.LastUpdateTime = DateTime.Parse("2026-01-17 10:31:58");
                v.CodeAndName = "lHjNV";
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
                v.ShipType = WMS.Model.WhShipTypeEnum.SpotGoods;
                v.IsStacking = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "KrT2uOSMs0lPVih2RHKBrxu6eQVoIViuqrOlusEPxsXdtMG7EZQeToLeIfFYrshOW4lKOV0JWJG68M6kCPycyzcfJSJlzXdjiixc2u9sPf1k0ceGa7XHOOFC6KOsxXkhFM9epoxhSuNaWHHrSXsHtFxzPqABkB0wyz57mvUdXhShvDr4xjNoWEJwIUOeNxEK4ChYDMaC7Aj8qIYVoY7xnNz9DI9fNxgQCgPKz5sZjALrgO6U";
                v.Code = "Hd0fZepVMjJybqpHH1E0qGAfxNwQ0b";
                v.Name = "aNjyJTLtdRxglqSI6rrPpktSdKYY35mO5C";
                v.SourceSystemId = "Ro3oW4qpg2JDIkd3AyZakyGe2cGKEkZ6IrDJm1G9DS0tUWchc";
                v.LastUpdateTime = DateTime.Parse("2025-11-24 10:31:58");
                v.CodeAndName = "IFisysHP3m13dRad";
                context.Set<BaseWareHouse>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
