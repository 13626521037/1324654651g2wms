using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class InventoryTransferOutManualApiTest
    {
        private InventoryTransferOutManualApiController _controller;
        private string _seed;

        public InventoryTransferOutManualApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryTransferOutManualApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryTransferOutManualApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryTransferOutManualApiVM vm = _controller.Wtm.CreateVM<InventoryTransferOutManualApiVM>();
            InventoryTransferOutManual v = new InventoryTransferOutManual();
            
            v.CreatePerson = "UA99r";
            v.OrganizationId = AddBaseOrganization();
            v.BusinessDate = DateTime.Parse("2024-11-30 13:13:33");
            v.SubmitDate = DateTime.Parse("2025-07-20 13:13:33");
            v.DocNo = "Lozbw02meEFZVxNpaBFDjR1gZ1oZBUhR7NtognUjOtdgtpGw";
            v.DocType = "z8XEfxxhUGsjFKYhEe2Q7Ycj9tLXXfJzyo0FUasi4HUkP43Sv";
            v.TransInOrganizationId = AddBaseOrganization();
            v.TranInWhId = AddBaseWareHouse();
            v.TransOutOrganizationId = AddBaseOrganization();
            v.TransOutWhId = AddBaseWareHouse();
            v.Status = WMS.Model.InventoryTransferOutManualStatusEnum.PartShipped;
            v.Memo = "COmPpxCexOiTBI5Tg2pATRItibkt3RsJXD2ayt30iaZ43decNqUPtO1x0eZLzyC3EM63YVSGzz1StEfybLRr49UX0iWzqgfcqG1G8subLnVcvuWCG8KNvGQEwiQjeQlMsPiLA3JIgS";
            v.SourceSystemId = "78jRQaDRR2HRLuz4C7p7Xh1c1vAEhyCKLCP";
            v.LastUpdateTime = DateTime.Parse("2025-02-19 13:13:33");
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryTransferOutManual>().Find(v.ID);
                
                Assert.AreEqual(data.CreatePerson, "UA99r");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-11-30 13:13:33"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2025-07-20 13:13:33"));
                Assert.AreEqual(data.DocNo, "Lozbw02meEFZVxNpaBFDjR1gZ1oZBUhR7NtognUjOtdgtpGw");
                Assert.AreEqual(data.DocType, "z8XEfxxhUGsjFKYhEe2Q7Ycj9tLXXfJzyo0FUasi4HUkP43Sv");
                Assert.AreEqual(data.Status, WMS.Model.InventoryTransferOutManualStatusEnum.PartShipped);
                Assert.AreEqual(data.Memo, "COmPpxCexOiTBI5Tg2pATRItibkt3RsJXD2ayt30iaZ43decNqUPtO1x0eZLzyC3EM63YVSGzz1StEfybLRr49UX0iWzqgfcqG1G8subLnVcvuWCG8KNvGQEwiQjeQlMsPiLA3JIgS");
                Assert.AreEqual(data.SourceSystemId, "78jRQaDRR2HRLuz4C7p7Xh1c1vAEhyCKLCP");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2025-02-19 13:13:33"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryTransferOutManual v = new InventoryTransferOutManual();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.CreatePerson = "UA99r";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2024-11-30 13:13:33");
                v.SubmitDate = DateTime.Parse("2025-07-20 13:13:33");
                v.DocNo = "Lozbw02meEFZVxNpaBFDjR1gZ1oZBUhR7NtognUjOtdgtpGw";
                v.DocType = "z8XEfxxhUGsjFKYhEe2Q7Ycj9tLXXfJzyo0FUasi4HUkP43Sv";
                v.TransInOrganizationId = AddBaseOrganization();
                v.TranInWhId = AddBaseWareHouse();
                v.TransOutOrganizationId = AddBaseOrganization();
                v.TransOutWhId = AddBaseWareHouse();
                v.Status = WMS.Model.InventoryTransferOutManualStatusEnum.PartShipped;
                v.Memo = "COmPpxCexOiTBI5Tg2pATRItibkt3RsJXD2ayt30iaZ43decNqUPtO1x0eZLzyC3EM63YVSGzz1StEfybLRr49UX0iWzqgfcqG1G8subLnVcvuWCG8KNvGQEwiQjeQlMsPiLA3JIgS";
                v.SourceSystemId = "78jRQaDRR2HRLuz4C7p7Xh1c1vAEhyCKLCP";
                v.LastUpdateTime = DateTime.Parse("2025-02-19 13:13:33");
                context.Set<InventoryTransferOutManual>().Add(v);
                context.SaveChanges();
            }

            InventoryTransferOutManualApiVM vm = _controller.Wtm.CreateVM<InventoryTransferOutManualApiVM>();
            var oldID = v.ID;
            v = new InventoryTransferOutManual();
            v.ID = oldID;
       		
            v.CreatePerson = "gRd1yEZvN";
            v.BusinessDate = DateTime.Parse("2024-06-04 13:13:33");
            v.SubmitDate = DateTime.Parse("2026-06-13 13:13:33");
            v.DocNo = "mDH8LxnIcgkVrw4elceVWxNrWWO52NWVDpSnhcyJe";
            v.DocType = "dGksJ";
            v.Status = WMS.Model.InventoryTransferOutManualStatusEnum.PartOff;
            v.Memo = "9B90sh7GnfQyDJUz9BqoNIO78W7QjlLswws7nFdGHQ0VNdeuOUSFu5JKhTc7Wb6PUlSFaTkljw8AfxhJJKyP53inVVmiJX8n19MlBvREF49KFLpukTo8scZvBDfvCWqbgDasQNC";
            v.SourceSystemId = "YZk";
            v.LastUpdateTime = DateTime.Parse("2024-03-26 13:13:33");
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.CreatePerson", "");
            vm.FC.Add("Entity.OrganizationId", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.SubmitDate", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.DocType", "");
            vm.FC.Add("Entity.TransInOrganizationId", "");
            vm.FC.Add("Entity.TranInWhId", "");
            vm.FC.Add("Entity.TransOutOrganizationId", "");
            vm.FC.Add("Entity.TransOutWhId", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.Memo", "");
            vm.FC.Add("Entity.SourceSystemId", "");
            vm.FC.Add("Entity.LastUpdateTime", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryTransferOutManual>().Find(v.ID);
 				
                Assert.AreEqual(data.CreatePerson, "gRd1yEZvN");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-06-04 13:13:33"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2026-06-13 13:13:33"));
                Assert.AreEqual(data.DocNo, "mDH8LxnIcgkVrw4elceVWxNrWWO52NWVDpSnhcyJe");
                Assert.AreEqual(data.DocType, "dGksJ");
                Assert.AreEqual(data.Status, WMS.Model.InventoryTransferOutManualStatusEnum.PartOff);
                Assert.AreEqual(data.Memo, "9B90sh7GnfQyDJUz9BqoNIO78W7QjlLswws7nFdGHQ0VNdeuOUSFu5JKhTc7Wb6PUlSFaTkljw8AfxhJJKyP53inVVmiJX8n19MlBvREF49KFLpukTo8scZvBDfvCWqbgDasQNC");
                Assert.AreEqual(data.SourceSystemId, "YZk");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2024-03-26 13:13:33"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryTransferOutManual v = new InventoryTransferOutManual();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.CreatePerson = "UA99r";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2024-11-30 13:13:33");
                v.SubmitDate = DateTime.Parse("2025-07-20 13:13:33");
                v.DocNo = "Lozbw02meEFZVxNpaBFDjR1gZ1oZBUhR7NtognUjOtdgtpGw";
                v.DocType = "z8XEfxxhUGsjFKYhEe2Q7Ycj9tLXXfJzyo0FUasi4HUkP43Sv";
                v.TransInOrganizationId = AddBaseOrganization();
                v.TranInWhId = AddBaseWareHouse();
                v.TransOutOrganizationId = AddBaseOrganization();
                v.TransOutWhId = AddBaseWareHouse();
                v.Status = WMS.Model.InventoryTransferOutManualStatusEnum.PartShipped;
                v.Memo = "COmPpxCexOiTBI5Tg2pATRItibkt3RsJXD2ayt30iaZ43decNqUPtO1x0eZLzyC3EM63YVSGzz1StEfybLRr49UX0iWzqgfcqG1G8subLnVcvuWCG8KNvGQEwiQjeQlMsPiLA3JIgS";
                v.SourceSystemId = "78jRQaDRR2HRLuz4C7p7Xh1c1vAEhyCKLCP";
                v.LastUpdateTime = DateTime.Parse("2025-02-19 13:13:33");
                context.Set<InventoryTransferOutManual>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryTransferOutManual v1 = new InventoryTransferOutManual();
            InventoryTransferOutManual v2 = new InventoryTransferOutManual();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.CreatePerson = "UA99r";
                v1.OrganizationId = AddBaseOrganization();
                v1.BusinessDate = DateTime.Parse("2024-11-30 13:13:33");
                v1.SubmitDate = DateTime.Parse("2025-07-20 13:13:33");
                v1.DocNo = "Lozbw02meEFZVxNpaBFDjR1gZ1oZBUhR7NtognUjOtdgtpGw";
                v1.DocType = "z8XEfxxhUGsjFKYhEe2Q7Ycj9tLXXfJzyo0FUasi4HUkP43Sv";
                v1.TransInOrganizationId = AddBaseOrganization();
                v1.TranInWhId = AddBaseWareHouse();
                v1.TransOutOrganizationId = AddBaseOrganization();
                v1.TransOutWhId = AddBaseWareHouse();
                v1.Status = WMS.Model.InventoryTransferOutManualStatusEnum.PartShipped;
                v1.Memo = "COmPpxCexOiTBI5Tg2pATRItibkt3RsJXD2ayt30iaZ43decNqUPtO1x0eZLzyC3EM63YVSGzz1StEfybLRr49UX0iWzqgfcqG1G8subLnVcvuWCG8KNvGQEwiQjeQlMsPiLA3JIgS";
                v1.SourceSystemId = "78jRQaDRR2HRLuz4C7p7Xh1c1vAEhyCKLCP";
                v1.LastUpdateTime = DateTime.Parse("2025-02-19 13:13:33");
                v2.CreatePerson = "gRd1yEZvN";
                v2.OrganizationId = v1.OrganizationId; 
                v2.BusinessDate = DateTime.Parse("2024-06-04 13:13:33");
                v2.SubmitDate = DateTime.Parse("2026-06-13 13:13:33");
                v2.DocNo = "mDH8LxnIcgkVrw4elceVWxNrWWO52NWVDpSnhcyJe";
                v2.DocType = "dGksJ";
                v2.TransInOrganizationId = v1.TransInOrganizationId; 
                v2.TranInWhId = v1.TranInWhId; 
                v2.TransOutOrganizationId = v1.TransOutOrganizationId; 
                v2.TransOutWhId = v1.TransOutWhId; 
                v2.Status = WMS.Model.InventoryTransferOutManualStatusEnum.PartOff;
                v2.Memo = "9B90sh7GnfQyDJUz9BqoNIO78W7QjlLswws7nFdGHQ0VNdeuOUSFu5JKhTc7Wb6PUlSFaTkljw8AfxhJJKyP53inVVmiJX8n19MlBvREF49KFLpukTo8scZvBDfvCWqbgDasQNC";
                v2.SourceSystemId = "YZk";
                v2.LastUpdateTime = DateTime.Parse("2024-03-26 13:13:33");
                context.Set<InventoryTransferOutManual>().Add(v1);
                context.Set<InventoryTransferOutManual>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryTransferOutManual>().Find(v1.ID);
                var data2 = context.Set<InventoryTransferOutManual>().Find(v2.ID);
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
                v.IsSale = false;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "ziOaaYko9gsMxVd74hs8WcOZBVxVqeqY0zkmjyuZxuv5FILm5f35ydXti4xmzXwbollpOfNgcBQeuvpMcHnIWU2Wyy5pEEVsQ8s8RwnwMDs5cdktYxZtLn2zaiHkwhvUBpK1GxDoPsjBGKhE2ccSj1";
                v.Code = "1kSuePdDsVTyQR3k8EZ6r";
                v.Name = "k";
                v.SourceSystemId = "cItAFKKQq4MGFNZwFoK6kfNXG1erVMlIvXoIWjWo5veOYS";
                v.LastUpdateTime = DateTime.Parse("2026-08-03 13:13:33");
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
                v.IsProduct = true;
                v.ShipType = WMS.Model.WhShipTypeEnum.WaitToShip;
                v.IsStacking = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "y2F8O5VEyDKLUEALC16ZENWL6WgwYFfXlvKqWpCwMxXOh1dVovICjpWLzXDK8YkzS8Y4os1GOLprD01QcYfRcibL6uN798veKp1DCkygLB2vQzENCO2SiJA78nE83jWbCSw9wC1NBZZ5saz3Aq07ybnwuwD1nb2YoNQjBnAweXp5mgn";
                v.Code = "wbZUBD1";
                v.Name = "R5FQC0HJWeSnXSWn5";
                v.SourceSystemId = "H9gLxmexDpBAD06WdEFJhrQPBwxyGElt04Z";
                v.LastUpdateTime = DateTime.Parse("2026-05-28 13:13:33");
                context.Set<BaseWareHouse>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
