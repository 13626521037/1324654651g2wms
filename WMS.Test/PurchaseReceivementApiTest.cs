using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs;
using WMS.Model.PurchaseManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class PurchaseReceivementApiTest
    {
        private PurchaseReceivementApiController _controller;
        private string _seed;

        public PurchaseReceivementApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<PurchaseReceivementApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new PurchaseReceivementApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            PurchaseReceivementApiVM vm = _controller.Wtm.CreateVM<PurchaseReceivementApiVM>();
            PurchaseReceivement v = new PurchaseReceivement();
            
            v.CreatePerson = "av0GlVa5cxZM0k0dA90ofmYYo4fGmrZL4n";
            v.OrganizationId = AddBaseOrganization();
            v.BusinessDate = DateTime.Parse("2023-12-14 10:38:06");
            v.SubmitDate = DateTime.Parse("2026-05-25 10:38:06");
            v.DocNo = "zijlTwgikaveqWIuonxgDoQrwGdJyP4xs9ppWQaEYFiOO3";
            v.DocType = "RoccjnfhCLeo7YaWFNWmEdYoDhJ8y4qtkWx0BONJya";
            v.SupplierId = AddBaseSupplier();
            v.BizType = WMS.Model.BizTypeEnum.PM005;
            v.InspectStatus = WMS.Model.PurchaseReceivementInspectStatusEnum.ParInspected;
            v.Status = WMS.Model.PurchaseReceivementStatusEnum.NotReceive;
            v.Memo = "BEIzeV8rSbkWqdAveQRTghUI6cbpT2VVyjtymb9Vo7C8SR0epQIarzl69kYTxhswPy5H159UYsSCCJ1EQ3ZI3ylYEbCWaXF";
            v.SourceSystemId = "Bg4tsJSyC0GuExi8UnG";
            v.LastUpdateTime = DateTime.Parse("2024-03-15 10:38:06");
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<PurchaseReceivement>().Find(v.ID);
                
                Assert.AreEqual(data.CreatePerson, "av0GlVa5cxZM0k0dA90ofmYYo4fGmrZL4n");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2023-12-14 10:38:06"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2026-05-25 10:38:06"));
                Assert.AreEqual(data.DocNo, "zijlTwgikaveqWIuonxgDoQrwGdJyP4xs9ppWQaEYFiOO3");
                Assert.AreEqual(data.DocType, "RoccjnfhCLeo7YaWFNWmEdYoDhJ8y4qtkWx0BONJya");
                Assert.AreEqual(data.BizType, WMS.Model.BizTypeEnum.PM005);
                Assert.AreEqual(data.InspectStatus, WMS.Model.PurchaseReceivementInspectStatusEnum.ParInspected);
                Assert.AreEqual(data.Status, WMS.Model.PurchaseReceivementStatusEnum.NotReceive);
                Assert.AreEqual(data.Memo, "BEIzeV8rSbkWqdAveQRTghUI6cbpT2VVyjtymb9Vo7C8SR0epQIarzl69kYTxhswPy5H159UYsSCCJ1EQ3ZI3ylYEbCWaXF");
                Assert.AreEqual(data.SourceSystemId, "Bg4tsJSyC0GuExi8UnG");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2024-03-15 10:38:06"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            PurchaseReceivement v = new PurchaseReceivement();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.CreatePerson = "av0GlVa5cxZM0k0dA90ofmYYo4fGmrZL4n";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2023-12-14 10:38:06");
                v.SubmitDate = DateTime.Parse("2026-05-25 10:38:06");
                v.DocNo = "zijlTwgikaveqWIuonxgDoQrwGdJyP4xs9ppWQaEYFiOO3";
                v.DocType = "RoccjnfhCLeo7YaWFNWmEdYoDhJ8y4qtkWx0BONJya";
                v.SupplierId = AddBaseSupplier();
                v.BizType = WMS.Model.BizTypeEnum.PM005;
                v.InspectStatus = WMS.Model.PurchaseReceivementInspectStatusEnum.ParInspected;
                v.Status = WMS.Model.PurchaseReceivementStatusEnum.NotReceive;
                v.Memo = "BEIzeV8rSbkWqdAveQRTghUI6cbpT2VVyjtymb9Vo7C8SR0epQIarzl69kYTxhswPy5H159UYsSCCJ1EQ3ZI3ylYEbCWaXF";
                v.SourceSystemId = "Bg4tsJSyC0GuExi8UnG";
                v.LastUpdateTime = DateTime.Parse("2024-03-15 10:38:06");
                context.Set<PurchaseReceivement>().Add(v);
                context.SaveChanges();
            }

            PurchaseReceivementApiVM vm = _controller.Wtm.CreateVM<PurchaseReceivementApiVM>();
            var oldID = v.ID;
            v = new PurchaseReceivement();
            v.ID = oldID;
       		
            v.CreatePerson = "tVnVBkqAGlkAEMHiL5oF19tRATI3X9j0NHetTCIzvtqAqBqVD";
            v.BusinessDate = DateTime.Parse("2026-05-01 10:38:06");
            v.SubmitDate = DateTime.Parse("2024-08-29 10:38:06");
            v.DocNo = "rVe7ADUwvBtp";
            v.DocType = "G0g6ghPJc7lHhJyMI5pWXWBilOrZckPEsSOOAhfznk";
            v.BizType = WMS.Model.BizTypeEnum.PM005;
            v.InspectStatus = WMS.Model.PurchaseReceivementInspectStatusEnum.AllInspected;
            v.Status = WMS.Model.PurchaseReceivementStatusEnum.AllReceive;
            v.Memo = "ZWVM";
            v.SourceSystemId = "Syg4qWJw9TTHG3a4Spwgm8M8znvTx5tu5zX3L";
            v.LastUpdateTime = DateTime.Parse("2024-08-16 10:38:06");
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.CreatePerson", "");
            vm.FC.Add("Entity.OrganizationId", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.SubmitDate", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.DocType", "");
            vm.FC.Add("Entity.SupplierId", "");
            vm.FC.Add("Entity.BizType", "");
            vm.FC.Add("Entity.InspectStatus", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.Memo", "");
            vm.FC.Add("Entity.SourceSystemId", "");
            vm.FC.Add("Entity.LastUpdateTime", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<PurchaseReceivement>().Find(v.ID);
 				
                Assert.AreEqual(data.CreatePerson, "tVnVBkqAGlkAEMHiL5oF19tRATI3X9j0NHetTCIzvtqAqBqVD");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2026-05-01 10:38:06"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2024-08-29 10:38:06"));
                Assert.AreEqual(data.DocNo, "rVe7ADUwvBtp");
                Assert.AreEqual(data.DocType, "G0g6ghPJc7lHhJyMI5pWXWBilOrZckPEsSOOAhfznk");
                Assert.AreEqual(data.BizType, WMS.Model.BizTypeEnum.PM005);
                Assert.AreEqual(data.InspectStatus, WMS.Model.PurchaseReceivementInspectStatusEnum.AllInspected);
                Assert.AreEqual(data.Status, WMS.Model.PurchaseReceivementStatusEnum.AllReceive);
                Assert.AreEqual(data.Memo, "ZWVM");
                Assert.AreEqual(data.SourceSystemId, "Syg4qWJw9TTHG3a4Spwgm8M8znvTx5tu5zX3L");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2024-08-16 10:38:06"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            PurchaseReceivement v = new PurchaseReceivement();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.CreatePerson = "av0GlVa5cxZM0k0dA90ofmYYo4fGmrZL4n";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2023-12-14 10:38:06");
                v.SubmitDate = DateTime.Parse("2026-05-25 10:38:06");
                v.DocNo = "zijlTwgikaveqWIuonxgDoQrwGdJyP4xs9ppWQaEYFiOO3";
                v.DocType = "RoccjnfhCLeo7YaWFNWmEdYoDhJ8y4qtkWx0BONJya";
                v.SupplierId = AddBaseSupplier();
                v.BizType = WMS.Model.BizTypeEnum.PM005;
                v.InspectStatus = WMS.Model.PurchaseReceivementInspectStatusEnum.ParInspected;
                v.Status = WMS.Model.PurchaseReceivementStatusEnum.NotReceive;
                v.Memo = "BEIzeV8rSbkWqdAveQRTghUI6cbpT2VVyjtymb9Vo7C8SR0epQIarzl69kYTxhswPy5H159UYsSCCJ1EQ3ZI3ylYEbCWaXF";
                v.SourceSystemId = "Bg4tsJSyC0GuExi8UnG";
                v.LastUpdateTime = DateTime.Parse("2024-03-15 10:38:06");
                context.Set<PurchaseReceivement>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            PurchaseReceivement v1 = new PurchaseReceivement();
            PurchaseReceivement v2 = new PurchaseReceivement();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.CreatePerson = "av0GlVa5cxZM0k0dA90ofmYYo4fGmrZL4n";
                v1.OrganizationId = AddBaseOrganization();
                v1.BusinessDate = DateTime.Parse("2023-12-14 10:38:06");
                v1.SubmitDate = DateTime.Parse("2026-05-25 10:38:06");
                v1.DocNo = "zijlTwgikaveqWIuonxgDoQrwGdJyP4xs9ppWQaEYFiOO3";
                v1.DocType = "RoccjnfhCLeo7YaWFNWmEdYoDhJ8y4qtkWx0BONJya";
                v1.SupplierId = AddBaseSupplier();
                v1.BizType = WMS.Model.BizTypeEnum.PM005;
                v1.InspectStatus = WMS.Model.PurchaseReceivementInspectStatusEnum.ParInspected;
                v1.Status = WMS.Model.PurchaseReceivementStatusEnum.NotReceive;
                v1.Memo = "BEIzeV8rSbkWqdAveQRTghUI6cbpT2VVyjtymb9Vo7C8SR0epQIarzl69kYTxhswPy5H159UYsSCCJ1EQ3ZI3ylYEbCWaXF";
                v1.SourceSystemId = "Bg4tsJSyC0GuExi8UnG";
                v1.LastUpdateTime = DateTime.Parse("2024-03-15 10:38:06");
                v2.CreatePerson = "tVnVBkqAGlkAEMHiL5oF19tRATI3X9j0NHetTCIzvtqAqBqVD";
                v2.OrganizationId = v1.OrganizationId; 
                v2.BusinessDate = DateTime.Parse("2026-05-01 10:38:06");
                v2.SubmitDate = DateTime.Parse("2024-08-29 10:38:06");
                v2.DocNo = "rVe7ADUwvBtp";
                v2.DocType = "G0g6ghPJc7lHhJyMI5pWXWBilOrZckPEsSOOAhfznk";
                v2.SupplierId = v1.SupplierId; 
                v2.BizType = WMS.Model.BizTypeEnum.PM005;
                v2.InspectStatus = WMS.Model.PurchaseReceivementInspectStatusEnum.AllInspected;
                v2.Status = WMS.Model.PurchaseReceivementStatusEnum.AllReceive;
                v2.Memo = "ZWVM";
                v2.SourceSystemId = "Syg4qWJw9TTHG3a4Spwgm8M8znvTx5tu5zX3L";
                v2.LastUpdateTime = DateTime.Parse("2024-08-16 10:38:06");
                context.Set<PurchaseReceivement>().Add(v1);
                context.Set<PurchaseReceivement>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<PurchaseReceivement>().Find(v1.ID);
                var data2 = context.Set<PurchaseReceivement>().Find(v2.ID);
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
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "uGagEtZCCBU3bZVAfmkFrbP3nPjfD1qZLgnBlmpGluVS1uL0bq717ow78r67xvuqA6U0ZrHFjWY1B949zmRebAs01yyyKriOYhL0KhX15krjziK0pQokMKsVd6SFbNhq5zFZZH8pYRtwXAA6FNyeNzYA2GUkP40mERQausUbD4IxHBKuKFixw4FI0dW9QekuRSl4urbjYWV0zvQkVglndbrhA8anFJfXeWf361YpQztqoQKPDOfUibL9oHwaPwXgStycKtQnWQ9fPm0BSig8wCmNcOokpkVljod1YHDGbrRs2k9bd8JI5AqN1hGjFLlecBRm0pjYtFfDP3OXBmn0sWODRSF5rxq1QakUmImg8m79Ip6F4U2AZoGqXcDadtCmih3rVFbO1S4AoXondBW9yMva7oaIyMz78BrMYdEOB7H66s4wZh5hsw4hUKZ8NrYFNg730MZ5a5LN960O115mpEIVE7Sq47";
                v.Code = "GIaFAKUockOEH3ZVyEwTREuwEpUikgo";
                v.Name = "dPqCz71PfgKMtCIWF6JBalnNpPwb";
                v.SourceSystemId = "ccAx6Xwp8q38AyYPezmwhyJUwC5q804HX7";
                v.LastUpdateTime = DateTime.Parse("2024-01-25 10:38:06");
                context.Set<BaseOrganization>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseSupplier()
        {
            BaseSupplier v = new BaseSupplier();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "COTCD6m6yJhWLcIC88rz9b49OFHndw79WSPAPp";
                v.Name = "Ru80buswI64NVodbqjYs1aeLl6CMS3nEseRNz5hHvFUxKCvM8xHxNLd4TBsIDpol7aY6MPRWab37VBJX598CYLTRaZ";
                v.ShortName = "Mmd8kY4f9wZHpPCKfhyRjwbJrZY2j6LscjgMiCmNjZ";
                v.OrganizationId = AddBaseOrganization();
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "WFmR8CTVey5Zrm60nBfJyrlOoOdr0OknzAqzxll2z6V2RdQ2e7En0tk302BhlODI5vTlentdpYL5T1U6vCXpuRwJJdXgN3kuEEJGbneSolqdo7eaBcVsDrM2VcIfT7jtw3pZmVpV7GaadFYY5kyTSJ8zwKzYcexTc78w51Z5Ycq8RVDC7sWAONqdbUbZPkhxMOTlci9uyZQ5fYV5f13A7Bn6h0WpAX3qFq0Lblj7C9ACScgZlen2G8WI9CCOt3kYpag7jIo9Cg9CdnoD9Z5Gyp9Xr";
                v.SourceSystemId = "ksPA57CgH8ZE0PxgARssKzI4ZZP";
                v.LastUpdateTime = DateTime.Parse("2024-11-19 10:38:06");
                context.Set<BaseSupplier>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
