using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryTransferInVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class InventoryTransferInApiTest
    {
        private InventoryTransferInApiController _controller;
        private string _seed;

        public InventoryTransferInApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryTransferInApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryTransferInApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryTransferInApiVM vm = _controller.Wtm.CreateVM<InventoryTransferInApiVM>();
            InventoryTransferIn v = new InventoryTransferIn();
            
            v.ErpID = "IU5IdXxAF3lwEApXD";
            v.BusinessDate = DateTime.Parse("2025-02-22 11:11:29");
            v.DocType = "nXBVnsln5nES";
            v.DocNo = "lDxNIgDwFhFzWwkI3qScLH25zPtCy";
            v.TransInOrganizationId = AddBaseOrganization();
            v.TransInWhId = AddBaseWareHouse();
            v.TransferOutType = WMS.Model.InventoryTransferOutTypeEnum.Manual;
            v.Status = WMS.Model.InventoryTransferInStatusEnum.NotReceive;
            v.Memo = "rIOTQJldJ";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryTransferIn>().Find(v.ID);
                
                Assert.AreEqual(data.ErpID, "IU5IdXxAF3lwEApXD");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2025-02-22 11:11:29"));
                Assert.AreEqual(data.DocType, "nXBVnsln5nES");
                Assert.AreEqual(data.DocNo, "lDxNIgDwFhFzWwkI3qScLH25zPtCy");
                Assert.AreEqual(data.TransferOutType, WMS.Model.InventoryTransferOutTypeEnum.Manual);
                Assert.AreEqual(data.Status, WMS.Model.InventoryTransferInStatusEnum.NotReceive);
                Assert.AreEqual(data.Memo, "rIOTQJldJ");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryTransferIn v = new InventoryTransferIn();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ErpID = "IU5IdXxAF3lwEApXD";
                v.BusinessDate = DateTime.Parse("2025-02-22 11:11:29");
                v.DocType = "nXBVnsln5nES";
                v.DocNo = "lDxNIgDwFhFzWwkI3qScLH25zPtCy";
                v.TransInOrganizationId = AddBaseOrganization();
                v.TransInWhId = AddBaseWareHouse();
                v.TransferOutType = WMS.Model.InventoryTransferOutTypeEnum.Manual;
                v.Status = WMS.Model.InventoryTransferInStatusEnum.NotReceive;
                v.Memo = "rIOTQJldJ";
                context.Set<InventoryTransferIn>().Add(v);
                context.SaveChanges();
            }

            InventoryTransferInApiVM vm = _controller.Wtm.CreateVM<InventoryTransferInApiVM>();
            var oldID = v.ID;
            v = new InventoryTransferIn();
            v.ID = oldID;
       		
            v.ErpID = "EaqBtWP8571U8p3Yg9HRSZooAE8t9xwm8TCghmQn8LYH1mLei";
            v.BusinessDate = DateTime.Parse("2025-02-19 11:11:29");
            v.DocType = "l79LFX6";
            v.DocNo = "hZ1DLwnG9YAdK1JWHagsuhEi4izJXP3qSrBNmOBbFr43";
            v.TransferOutType = WMS.Model.InventoryTransferOutTypeEnum.Manual;
            v.Status = WMS.Model.InventoryTransferInStatusEnum.PartReceive;
            v.Memo = "TpTJMbJFdK3Q7sstMU1XuUv6gDmdKowBUo6Hw93eUusJDaiN2UE1NY5nDQFS189vXQU4yTc2Nx5ZShKn1zrHTyVic6I4TiuuM9Au2bzvwPBwRG1ylQABJdv4YO87nDhV5nHEBlhTxc9G7RIidv1JYDUfdzbYogGjGkQLEAPVHrnPUTbecphnmomyTUd3H7TaQS374nNYyiTtnMQqHYvqUKeL4jY2K5jZCU79NjZSq7Een0Vn01hutCmxuWLjQNvBS0DlM13T8uyPT5sHvGhKMs3cBiQIKInBG8ua1V5jxIPfa39n1w7jgM988pKtqU4IpwpfSkb21188qrO0INvU9UUlkpc9GSoLuZCvaXQCWI9fvfFS9RmdYtVJ0xINixaisezfRQEubPlI1T27P";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ErpID", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.DocType", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.TransInOrganizationId", "");
            vm.FC.Add("Entity.TransInWhId", "");
            vm.FC.Add("Entity.TransferOutType", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryTransferIn>().Find(v.ID);
 				
                Assert.AreEqual(data.ErpID, "EaqBtWP8571U8p3Yg9HRSZooAE8t9xwm8TCghmQn8LYH1mLei");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2025-02-19 11:11:29"));
                Assert.AreEqual(data.DocType, "l79LFX6");
                Assert.AreEqual(data.DocNo, "hZ1DLwnG9YAdK1JWHagsuhEi4izJXP3qSrBNmOBbFr43");
                Assert.AreEqual(data.TransferOutType, WMS.Model.InventoryTransferOutTypeEnum.Manual);
                Assert.AreEqual(data.Status, WMS.Model.InventoryTransferInStatusEnum.PartReceive);
                Assert.AreEqual(data.Memo, "TpTJMbJFdK3Q7sstMU1XuUv6gDmdKowBUo6Hw93eUusJDaiN2UE1NY5nDQFS189vXQU4yTc2Nx5ZShKn1zrHTyVic6I4TiuuM9Au2bzvwPBwRG1ylQABJdv4YO87nDhV5nHEBlhTxc9G7RIidv1JYDUfdzbYogGjGkQLEAPVHrnPUTbecphnmomyTUd3H7TaQS374nNYyiTtnMQqHYvqUKeL4jY2K5jZCU79NjZSq7Een0Vn01hutCmxuWLjQNvBS0DlM13T8uyPT5sHvGhKMs3cBiQIKInBG8ua1V5jxIPfa39n1w7jgM988pKtqU4IpwpfSkb21188qrO0INvU9UUlkpc9GSoLuZCvaXQCWI9fvfFS9RmdYtVJ0xINixaisezfRQEubPlI1T27P");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryTransferIn v = new InventoryTransferIn();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ErpID = "IU5IdXxAF3lwEApXD";
                v.BusinessDate = DateTime.Parse("2025-02-22 11:11:29");
                v.DocType = "nXBVnsln5nES";
                v.DocNo = "lDxNIgDwFhFzWwkI3qScLH25zPtCy";
                v.TransInOrganizationId = AddBaseOrganization();
                v.TransInWhId = AddBaseWareHouse();
                v.TransferOutType = WMS.Model.InventoryTransferOutTypeEnum.Manual;
                v.Status = WMS.Model.InventoryTransferInStatusEnum.NotReceive;
                v.Memo = "rIOTQJldJ";
                context.Set<InventoryTransferIn>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryTransferIn v1 = new InventoryTransferIn();
            InventoryTransferIn v2 = new InventoryTransferIn();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ErpID = "IU5IdXxAF3lwEApXD";
                v1.BusinessDate = DateTime.Parse("2025-02-22 11:11:29");
                v1.DocType = "nXBVnsln5nES";
                v1.DocNo = "lDxNIgDwFhFzWwkI3qScLH25zPtCy";
                v1.TransInOrganizationId = AddBaseOrganization();
                v1.TransInWhId = AddBaseWareHouse();
                v1.TransferOutType = WMS.Model.InventoryTransferOutTypeEnum.Manual;
                v1.Status = WMS.Model.InventoryTransferInStatusEnum.NotReceive;
                v1.Memo = "rIOTQJldJ";
                v2.ErpID = "EaqBtWP8571U8p3Yg9HRSZooAE8t9xwm8TCghmQn8LYH1mLei";
                v2.BusinessDate = DateTime.Parse("2025-02-19 11:11:29");
                v2.DocType = "l79LFX6";
                v2.DocNo = "hZ1DLwnG9YAdK1JWHagsuhEi4izJXP3qSrBNmOBbFr43";
                v2.TransInOrganizationId = v1.TransInOrganizationId; 
                v2.TransInWhId = v1.TransInWhId; 
                v2.TransferOutType = WMS.Model.InventoryTransferOutTypeEnum.Manual;
                v2.Status = WMS.Model.InventoryTransferInStatusEnum.PartReceive;
                v2.Memo = "TpTJMbJFdK3Q7sstMU1XuUv6gDmdKowBUo6Hw93eUusJDaiN2UE1NY5nDQFS189vXQU4yTc2Nx5ZShKn1zrHTyVic6I4TiuuM9Au2bzvwPBwRG1ylQABJdv4YO87nDhV5nHEBlhTxc9G7RIidv1JYDUfdzbYogGjGkQLEAPVHrnPUTbecphnmomyTUd3H7TaQS374nNYyiTtnMQqHYvqUKeL4jY2K5jZCU79NjZSq7Een0Vn01hutCmxuWLjQNvBS0DlM13T8uyPT5sHvGhKMs3cBiQIKInBG8ua1V5jxIPfa39n1w7jgM988pKtqU4IpwpfSkb21188qrO0INvU9UUlkpc9GSoLuZCvaXQCWI9fvfFS9RmdYtVJ0xINixaisezfRQEubPlI1T27P";
                context.Set<InventoryTransferIn>().Add(v1);
                context.Set<InventoryTransferIn>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryTransferIn>().Find(v1.ID);
                var data2 = context.Set<InventoryTransferIn>().Find(v2.ID);
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
                v.IsSale = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "F7PYKntz53VlPbRc0QjzzgI7YNeU5TZ0z88Z03XLtSJBhbopzb652fNhXCMRCDk7ZTLw3OP43wCU1Vqklw8yIBrSYa6FTHlMkUBgzFM3RUvtQkk1CY6qopzxvToqIHiqB";
                v.Code = "VHmXxhvMSsH6QzIm8KW8sKWFMpiTW6d4PpuEWBZ";
                v.Name = "9yqQY38irP2a11CFd2Jpd2";
                v.SourceSystemId = "F6EHDueeV2PwTOimryTZK9WZyriGAF94G85icV";
                v.LastUpdateTime = DateTime.Parse("2024-07-23 11:11:29");
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
                v.ShipType = WMS.Model.WhShipTypeEnum.ToSaleCompany;
                v.IsStacking = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "OPWgqYI0QghAqz5kfhIvHj9z92vcLXm1XgDHRT2H8DBy3o48zcWeoBuONBEGJDp7nJt";
                v.Code = "CHW140IlkuLoPb";
                v.Name = "wOZRDRjPhd1Zs8xDfLwqcY";
                v.SourceSystemId = "sUQGnLHCsd6cAO7q7CpWf45HHOWQEcHnAVeFqOmqjGimj";
                v.LastUpdateTime = DateTime.Parse("2024-07-28 11:11:29");
                context.Set<BaseWareHouse>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
