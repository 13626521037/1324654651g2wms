using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs;
using WMS.Model.SalesManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class SalesReturnReceivementApiTest
    {
        private SalesReturnReceivementApiController _controller;
        private string _seed;

        public SalesReturnReceivementApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<SalesReturnReceivementApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new SalesReturnReceivementApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            SalesReturnReceivementApiVM vm = _controller.Wtm.CreateVM<SalesReturnReceivementApiVM>();
            SalesReturnReceivement v = new SalesReturnReceivement();
            
            v.CreatePerson = "H5h3uziAW69TEiTk5oU";
            v.OrganizationId = AddBaseOrganization();
            v.BusinessDate = DateTime.Parse("2026-06-07 16:34:09");
            v.SubmitDate = DateTime.Parse("2024-12-02 16:34:09");
            v.DocNo = "PZHtNO4OI1pg1GZZ9QbaF4hTx46gLHT9CtgsrRYSF";
            v.DocType = "G3Zc3d3wlRkG90W9fDpZ4CxRTZTUz7J2Y8YGh";
            v.CustomerId = AddBaseCustomer();
            v.Status = WMS.Model.SalesReturnReceivementStatusEnum.NotReceive;
            v.Memo = "EVdCLJFzGZkBxfZspwdE12WgZPLJniqPi";
            v.SourceSystemId = "TAHE8lrYufV9nuNv5o";
            v.LastUpdateTime = DateTime.Parse("2025-05-26 16:34:09");
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<SalesReturnReceivement>().Find(v.ID);
                
                Assert.AreEqual(data.CreatePerson, "H5h3uziAW69TEiTk5oU");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2026-06-07 16:34:09"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2024-12-02 16:34:09"));
                Assert.AreEqual(data.DocNo, "PZHtNO4OI1pg1GZZ9QbaF4hTx46gLHT9CtgsrRYSF");
                Assert.AreEqual(data.DocType, "G3Zc3d3wlRkG90W9fDpZ4CxRTZTUz7J2Y8YGh");
                Assert.AreEqual(data.Status, WMS.Model.SalesReturnReceivementStatusEnum.NotReceive);
                Assert.AreEqual(data.Memo, "EVdCLJFzGZkBxfZspwdE12WgZPLJniqPi");
                Assert.AreEqual(data.SourceSystemId, "TAHE8lrYufV9nuNv5o");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2025-05-26 16:34:09"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            SalesReturnReceivement v = new SalesReturnReceivement();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.CreatePerson = "H5h3uziAW69TEiTk5oU";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2026-06-07 16:34:09");
                v.SubmitDate = DateTime.Parse("2024-12-02 16:34:09");
                v.DocNo = "PZHtNO4OI1pg1GZZ9QbaF4hTx46gLHT9CtgsrRYSF";
                v.DocType = "G3Zc3d3wlRkG90W9fDpZ4CxRTZTUz7J2Y8YGh";
                v.CustomerId = AddBaseCustomer();
                v.Status = WMS.Model.SalesReturnReceivementStatusEnum.NotReceive;
                v.Memo = "EVdCLJFzGZkBxfZspwdE12WgZPLJniqPi";
                v.SourceSystemId = "TAHE8lrYufV9nuNv5o";
                v.LastUpdateTime = DateTime.Parse("2025-05-26 16:34:09");
                context.Set<SalesReturnReceivement>().Add(v);
                context.SaveChanges();
            }

            SalesReturnReceivementApiVM vm = _controller.Wtm.CreateVM<SalesReturnReceivementApiVM>();
            var oldID = v.ID;
            v = new SalesReturnReceivement();
            v.ID = oldID;
       		
            v.CreatePerson = "lMDCQavMKJDHUWh0aD";
            v.BusinessDate = DateTime.Parse("2027-04-08 16:34:09");
            v.SubmitDate = DateTime.Parse("2025-06-03 16:34:09");
            v.DocNo = "7moNfcapUrbdyjMH";
            v.DocType = "dePJMEIYtnTwR";
            v.Status = WMS.Model.SalesReturnReceivementStatusEnum.AllReceive;
            v.Memo = "ulG6vrDwhyCtIfrWBGQUC2IarFdCkWz8h0po4PxSLxSuyvKnDOqWK2xiqEJWA7gRy9XHl4T2YPF9I9Rbiy5FGOzTHdljfrrwW6CeQaNio2iNuCIU5DJpg9792qpzxd6Tu8Yzs7IC8rWykC4vZtF37Fd7LzxZFElquZwGMjSYZiyEMP4HDVwrSjTRkJo5MC0ELmdP61KfI2XlK1z0wjU2ZEwwfGeIp4xDi32x0rzSL6sOqBv4Z";
            v.SourceSystemId = "k4A";
            v.LastUpdateTime = DateTime.Parse("2024-11-29 16:34:09");
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.CreatePerson", "");
            vm.FC.Add("Entity.OrganizationId", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.SubmitDate", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.DocType", "");
            vm.FC.Add("Entity.CustomerId", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.Memo", "");
            vm.FC.Add("Entity.SourceSystemId", "");
            vm.FC.Add("Entity.LastUpdateTime", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<SalesReturnReceivement>().Find(v.ID);
 				
                Assert.AreEqual(data.CreatePerson, "lMDCQavMKJDHUWh0aD");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2027-04-08 16:34:09"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2025-06-03 16:34:09"));
                Assert.AreEqual(data.DocNo, "7moNfcapUrbdyjMH");
                Assert.AreEqual(data.DocType, "dePJMEIYtnTwR");
                Assert.AreEqual(data.Status, WMS.Model.SalesReturnReceivementStatusEnum.AllReceive);
                Assert.AreEqual(data.Memo, "ulG6vrDwhyCtIfrWBGQUC2IarFdCkWz8h0po4PxSLxSuyvKnDOqWK2xiqEJWA7gRy9XHl4T2YPF9I9Rbiy5FGOzTHdljfrrwW6CeQaNio2iNuCIU5DJpg9792qpzxd6Tu8Yzs7IC8rWykC4vZtF37Fd7LzxZFElquZwGMjSYZiyEMP4HDVwrSjTRkJo5MC0ELmdP61KfI2XlK1z0wjU2ZEwwfGeIp4xDi32x0rzSL6sOqBv4Z");
                Assert.AreEqual(data.SourceSystemId, "k4A");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2024-11-29 16:34:09"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            SalesReturnReceivement v = new SalesReturnReceivement();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.CreatePerson = "H5h3uziAW69TEiTk5oU";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2026-06-07 16:34:09");
                v.SubmitDate = DateTime.Parse("2024-12-02 16:34:09");
                v.DocNo = "PZHtNO4OI1pg1GZZ9QbaF4hTx46gLHT9CtgsrRYSF";
                v.DocType = "G3Zc3d3wlRkG90W9fDpZ4CxRTZTUz7J2Y8YGh";
                v.CustomerId = AddBaseCustomer();
                v.Status = WMS.Model.SalesReturnReceivementStatusEnum.NotReceive;
                v.Memo = "EVdCLJFzGZkBxfZspwdE12WgZPLJniqPi";
                v.SourceSystemId = "TAHE8lrYufV9nuNv5o";
                v.LastUpdateTime = DateTime.Parse("2025-05-26 16:34:09");
                context.Set<SalesReturnReceivement>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            SalesReturnReceivement v1 = new SalesReturnReceivement();
            SalesReturnReceivement v2 = new SalesReturnReceivement();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.CreatePerson = "H5h3uziAW69TEiTk5oU";
                v1.OrganizationId = AddBaseOrganization();
                v1.BusinessDate = DateTime.Parse("2026-06-07 16:34:09");
                v1.SubmitDate = DateTime.Parse("2024-12-02 16:34:09");
                v1.DocNo = "PZHtNO4OI1pg1GZZ9QbaF4hTx46gLHT9CtgsrRYSF";
                v1.DocType = "G3Zc3d3wlRkG90W9fDpZ4CxRTZTUz7J2Y8YGh";
                v1.CustomerId = AddBaseCustomer();
                v1.Status = WMS.Model.SalesReturnReceivementStatusEnum.NotReceive;
                v1.Memo = "EVdCLJFzGZkBxfZspwdE12WgZPLJniqPi";
                v1.SourceSystemId = "TAHE8lrYufV9nuNv5o";
                v1.LastUpdateTime = DateTime.Parse("2025-05-26 16:34:09");
                v2.CreatePerson = "lMDCQavMKJDHUWh0aD";
                v2.OrganizationId = v1.OrganizationId; 
                v2.BusinessDate = DateTime.Parse("2027-04-08 16:34:09");
                v2.SubmitDate = DateTime.Parse("2025-06-03 16:34:09");
                v2.DocNo = "7moNfcapUrbdyjMH";
                v2.DocType = "dePJMEIYtnTwR";
                v2.CustomerId = v1.CustomerId; 
                v2.Status = WMS.Model.SalesReturnReceivementStatusEnum.AllReceive;
                v2.Memo = "ulG6vrDwhyCtIfrWBGQUC2IarFdCkWz8h0po4PxSLxSuyvKnDOqWK2xiqEJWA7gRy9XHl4T2YPF9I9Rbiy5FGOzTHdljfrrwW6CeQaNio2iNuCIU5DJpg9792qpzxd6Tu8Yzs7IC8rWykC4vZtF37Fd7LzxZFElquZwGMjSYZiyEMP4HDVwrSjTRkJo5MC0ELmdP61KfI2XlK1z0wjU2ZEwwfGeIp4xDi32x0rzSL6sOqBv4Z";
                v2.SourceSystemId = "k4A";
                v2.LastUpdateTime = DateTime.Parse("2024-11-29 16:34:09");
                context.Set<SalesReturnReceivement>().Add(v1);
                context.Set<SalesReturnReceivement>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<SalesReturnReceivement>().Find(v1.ID);
                var data2 = context.Set<SalesReturnReceivement>().Find(v2.ID);
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
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "5aOLASBIjkJlKh1nZXtCXgqs7RzLUPRQ8L1cZStDBBMeN7fCKEYneAm2DvSnLe6sMNLgN";
                v.Code = "T6S3WwpEvvHaKHLapTg7nt0Df5gkK4";
                v.Name = "axd4rAZm4uLxRHofDHYpea7jqmu";
                v.SourceSystemId = "tW8a3qcTHluaRSWQ7k4MNy4ZWrVhXfvmqyEAK5XCGBWj";
                v.LastUpdateTime = DateTime.Parse("2025-11-07 16:34:09");
                context.Set<BaseOrganization>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseCustomer()
        {
            BaseCustomer v = new BaseCustomer();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ShortName = "Qf4gOPLIKKO7cqCWrhizcb7XpZp2SjgEv";
                v.EnglishShortName = "Q8mJLzopwkltZsXjDC1kQhQBfE861hYuEDHW";
                v.OrganizationId = AddBaseOrganization();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "gFiVOclO2yR7nojP8qHbIyRladv1k9QaYl0mnO98JpuzMt9NtiOtafi593qKdu";
                v.Code = "v3Bdbz3PO7";
                v.Name = "aKFS7XS98zznuAZiEio3TC6dIOSo9HtqoXd9iUa";
                v.SourceSystemId = "7Qtlwj";
                v.LastUpdateTime = DateTime.Parse("2026-09-04 16:34:09");
                context.Set<BaseCustomer>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
