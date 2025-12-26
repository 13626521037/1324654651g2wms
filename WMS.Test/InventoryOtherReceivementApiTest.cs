using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;


namespace WMS.Test
{
    [TestClass]
    public class InventoryOtherReceivementApiTest
    {
        private InventoryOtherReceivementApiController _controller;
        private string _seed;

        public InventoryOtherReceivementApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryOtherReceivementApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryOtherReceivementApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryOtherReceivementApiVM vm = _controller.Wtm.CreateVM<InventoryOtherReceivementApiVM>();
            InventoryOtherReceivement v = new InventoryOtherReceivement();
            
            v.ErpID = "fXhO9bIOIO3RK6yuaQYY3JLXNJkC25UaIRfXtdUFaWJm7OG";
            v.BusinessDate = DateTime.Parse("2025-11-21 14:04:55");
            v.DocNo = "j1TYOOHluxxTcJSDHcVOxVfCpkZ7jwzCTakSZmY2TKlRQ4HtC";
            v.IsScrap = true;
            v.Memo = "sXLzxC8sVn0bvmeF3iC3NvVE2qMbEGtlh6ZPqLjPPwmWPXPRyIY576AVCPfGLtB6DYHZiHcXDUQBbicNIOiz5F15zZ8Y4MpOqMJapXSXyOLyMC0J9cGmd2SD81BuBX3iz02ZRmpL1641E7NXxKO5gwBtS3kKixBsszIUL9QUlTT3uR8TNjb9lpKvMjayTYg8HyyJbjJ5D2fwU11T6Fqo7qTxSFRsJgLnko9YAHVZiTIhqqbwVTcbv1jEWMyMVvSXKTMNsPzdBelGY2ZChFrrcK51NE1eGuESi06LlIxVtfOD7cNlfQGiOnDzvkajsFxdX8vAWVFTiEIdztRk8CnxdVvVuvgE";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryOtherReceivement>().Find(v.ID);
                
                Assert.AreEqual(data.ErpID, "fXhO9bIOIO3RK6yuaQYY3JLXNJkC25UaIRfXtdUFaWJm7OG");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2025-11-21 14:04:55"));
                Assert.AreEqual(data.DocNo, "j1TYOOHluxxTcJSDHcVOxVfCpkZ7jwzCTakSZmY2TKlRQ4HtC");
                Assert.AreEqual(data.IsScrap, true);
                Assert.AreEqual(data.Memo, "sXLzxC8sVn0bvmeF3iC3NvVE2qMbEGtlh6ZPqLjPPwmWPXPRyIY576AVCPfGLtB6DYHZiHcXDUQBbicNIOiz5F15zZ8Y4MpOqMJapXSXyOLyMC0J9cGmd2SD81BuBX3iz02ZRmpL1641E7NXxKO5gwBtS3kKixBsszIUL9QUlTT3uR8TNjb9lpKvMjayTYg8HyyJbjJ5D2fwU11T6Fqo7qTxSFRsJgLnko9YAHVZiTIhqqbwVTcbv1jEWMyMVvSXKTMNsPzdBelGY2ZChFrrcK51NE1eGuESi06LlIxVtfOD7cNlfQGiOnDzvkajsFxdX8vAWVFTiEIdztRk8CnxdVvVuvgE");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryOtherReceivement v = new InventoryOtherReceivement();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ErpID = "fXhO9bIOIO3RK6yuaQYY3JLXNJkC25UaIRfXtdUFaWJm7OG";
                v.BusinessDate = DateTime.Parse("2025-11-21 14:04:55");
                v.DocNo = "j1TYOOHluxxTcJSDHcVOxVfCpkZ7jwzCTakSZmY2TKlRQ4HtC";
                v.IsScrap = true;
                v.Memo = "sXLzxC8sVn0bvmeF3iC3NvVE2qMbEGtlh6ZPqLjPPwmWPXPRyIY576AVCPfGLtB6DYHZiHcXDUQBbicNIOiz5F15zZ8Y4MpOqMJapXSXyOLyMC0J9cGmd2SD81BuBX3iz02ZRmpL1641E7NXxKO5gwBtS3kKixBsszIUL9QUlTT3uR8TNjb9lpKvMjayTYg8HyyJbjJ5D2fwU11T6Fqo7qTxSFRsJgLnko9YAHVZiTIhqqbwVTcbv1jEWMyMVvSXKTMNsPzdBelGY2ZChFrrcK51NE1eGuESi06LlIxVtfOD7cNlfQGiOnDzvkajsFxdX8vAWVFTiEIdztRk8CnxdVvVuvgE";
                context.Set<InventoryOtherReceivement>().Add(v);
                context.SaveChanges();
            }

            InventoryOtherReceivementApiVM vm = _controller.Wtm.CreateVM<InventoryOtherReceivementApiVM>();
            var oldID = v.ID;
            v = new InventoryOtherReceivement();
            v.ID = oldID;
       		
            v.ErpID = "rVFodsw9vms";
            v.BusinessDate = DateTime.Parse("2024-12-31 14:04:55");
            v.DocNo = "AGf89hc4O";
            v.IsScrap = true;
            v.Memo = "NRq4iUMcbOYCKLsaEiqhoDyu3RaKiJoUCdgDzcZq7IrDC92ZyVy7vNuZdJhriJuKzVRlnOdOAZ4L6PIXHkL4W9kceQvvrBIspTKwsoZ895DRcMl6OJVQ";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ErpID", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.IsScrap", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryOtherReceivement>().Find(v.ID);
 				
                Assert.AreEqual(data.ErpID, "rVFodsw9vms");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-12-31 14:04:55"));
                Assert.AreEqual(data.DocNo, "AGf89hc4O");
                Assert.AreEqual(data.IsScrap, true);
                Assert.AreEqual(data.Memo, "NRq4iUMcbOYCKLsaEiqhoDyu3RaKiJoUCdgDzcZq7IrDC92ZyVy7vNuZdJhriJuKzVRlnOdOAZ4L6PIXHkL4W9kceQvvrBIspTKwsoZ895DRcMl6OJVQ");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryOtherReceivement v = new InventoryOtherReceivement();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ErpID = "fXhO9bIOIO3RK6yuaQYY3JLXNJkC25UaIRfXtdUFaWJm7OG";
                v.BusinessDate = DateTime.Parse("2025-11-21 14:04:55");
                v.DocNo = "j1TYOOHluxxTcJSDHcVOxVfCpkZ7jwzCTakSZmY2TKlRQ4HtC";
                v.IsScrap = true;
                v.Memo = "sXLzxC8sVn0bvmeF3iC3NvVE2qMbEGtlh6ZPqLjPPwmWPXPRyIY576AVCPfGLtB6DYHZiHcXDUQBbicNIOiz5F15zZ8Y4MpOqMJapXSXyOLyMC0J9cGmd2SD81BuBX3iz02ZRmpL1641E7NXxKO5gwBtS3kKixBsszIUL9QUlTT3uR8TNjb9lpKvMjayTYg8HyyJbjJ5D2fwU11T6Fqo7qTxSFRsJgLnko9YAHVZiTIhqqbwVTcbv1jEWMyMVvSXKTMNsPzdBelGY2ZChFrrcK51NE1eGuESi06LlIxVtfOD7cNlfQGiOnDzvkajsFxdX8vAWVFTiEIdztRk8CnxdVvVuvgE";
                context.Set<InventoryOtherReceivement>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryOtherReceivement v1 = new InventoryOtherReceivement();
            InventoryOtherReceivement v2 = new InventoryOtherReceivement();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ErpID = "fXhO9bIOIO3RK6yuaQYY3JLXNJkC25UaIRfXtdUFaWJm7OG";
                v1.BusinessDate = DateTime.Parse("2025-11-21 14:04:55");
                v1.DocNo = "j1TYOOHluxxTcJSDHcVOxVfCpkZ7jwzCTakSZmY2TKlRQ4HtC";
                v1.IsScrap = true;
                v1.Memo = "sXLzxC8sVn0bvmeF3iC3NvVE2qMbEGtlh6ZPqLjPPwmWPXPRyIY576AVCPfGLtB6DYHZiHcXDUQBbicNIOiz5F15zZ8Y4MpOqMJapXSXyOLyMC0J9cGmd2SD81BuBX3iz02ZRmpL1641E7NXxKO5gwBtS3kKixBsszIUL9QUlTT3uR8TNjb9lpKvMjayTYg8HyyJbjJ5D2fwU11T6Fqo7qTxSFRsJgLnko9YAHVZiTIhqqbwVTcbv1jEWMyMVvSXKTMNsPzdBelGY2ZChFrrcK51NE1eGuESi06LlIxVtfOD7cNlfQGiOnDzvkajsFxdX8vAWVFTiEIdztRk8CnxdVvVuvgE";
                v2.ErpID = "rVFodsw9vms";
                v2.BusinessDate = DateTime.Parse("2024-12-31 14:04:55");
                v2.DocNo = "AGf89hc4O";
                v2.IsScrap = true;
                v2.Memo = "NRq4iUMcbOYCKLsaEiqhoDyu3RaKiJoUCdgDzcZq7IrDC92ZyVy7vNuZdJhriJuKzVRlnOdOAZ4L6PIXHkL4W9kceQvvrBIspTKwsoZ895DRcMl6OJVQ";
                context.Set<InventoryOtherReceivement>().Add(v1);
                context.Set<InventoryOtherReceivement>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryOtherReceivement>().Find(v1.ID);
                var data2 = context.Set<InventoryOtherReceivement>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
