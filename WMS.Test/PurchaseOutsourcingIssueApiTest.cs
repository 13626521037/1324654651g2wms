using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs;
using WMS.Model.PurchaseManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class PurchaseOutsourcingIssueApiTest
    {
        private PurchaseOutsourcingIssueApiController _controller;
        private string _seed;

        public PurchaseOutsourcingIssueApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<PurchaseOutsourcingIssueApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new PurchaseOutsourcingIssueApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            PurchaseOutsourcingIssueApiVM vm = _controller.Wtm.CreateVM<PurchaseOutsourcingIssueApiVM>();
            PurchaseOutsourcingIssue v = new PurchaseOutsourcingIssue();
            
            v.CreatePerson = "8U5WEJFERDn1aQu8GUpvY76oyt";
            v.OrganizationId = AddBaseOrganization();
            v.BusinessDate = DateTime.Parse("2026-12-02 14:49:07");
            v.SubmitDate = DateTime.Parse("2026-01-07 14:49:07");
            v.DocNo = "LsR8BpBpTtJrx4IpTn7ThNZ7B2RoF1cTgm0jrVJG18AgKz";
            v.DocType = "9uNYFQjJLgZGQcjY1glyG3KzOLTo";
            v.SupplierId = AddBaseSupplier();
            v.Status = WMS.Model.PurchaseOutsourcingIssueStatusEnum.AllShipped;
            v.Memo = "nY17CdrhW8ny7jMXFdzy4KSLiD0Z5DA8dSi1Srer5dCK5F0wIZyLGqEpSA0LbwVhVT1aTe18siLnHv9Vdj46JLAMOL9dMvnyRR8t7TMGYHdB32DeYX6GMxuCfUq0BMVB54T0gI0dFYMe5O4CDVuXWrOgorLyCHqj93JugcRgQ8PUcNF9AU9XJtlIbGUEskEqn20FiaoEr5refCKAkiPmfNinIUKpJre4FlgY13WHytQ1Hy1zDADoQB3G4UhPNIHjbrWJR3rjz0RkLd813f0DF1VfUpio3gVBtdggYbnqM0X58X5mi5AhWnRMFGd0yotwdIJLVyxsi0Fuwlzs9ZyIUzmatiat8x9hBRxxQpx37jKrGFerZ0mQ7FWQWE7EdQdHPFxMKrWaAkcl5FRqr3OOMeVq90FJLRzSX1tI1EvvP6";
            v.SourceSystemId = "5HzHbfvvmDgPlUD2uPM0mtYk7VoGh0zVPws";
            v.LastUpdateTime = DateTime.Parse("2027-02-11 14:49:07");
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<PurchaseOutsourcingIssue>().Find(v.ID);
                
                Assert.AreEqual(data.CreatePerson, "8U5WEJFERDn1aQu8GUpvY76oyt");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2026-12-02 14:49:07"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2026-01-07 14:49:07"));
                Assert.AreEqual(data.DocNo, "LsR8BpBpTtJrx4IpTn7ThNZ7B2RoF1cTgm0jrVJG18AgKz");
                Assert.AreEqual(data.DocType, "9uNYFQjJLgZGQcjY1glyG3KzOLTo");
                Assert.AreEqual(data.Status, WMS.Model.PurchaseOutsourcingIssueStatusEnum.AllShipped);
                Assert.AreEqual(data.Memo, "nY17CdrhW8ny7jMXFdzy4KSLiD0Z5DA8dSi1Srer5dCK5F0wIZyLGqEpSA0LbwVhVT1aTe18siLnHv9Vdj46JLAMOL9dMvnyRR8t7TMGYHdB32DeYX6GMxuCfUq0BMVB54T0gI0dFYMe5O4CDVuXWrOgorLyCHqj93JugcRgQ8PUcNF9AU9XJtlIbGUEskEqn20FiaoEr5refCKAkiPmfNinIUKpJre4FlgY13WHytQ1Hy1zDADoQB3G4UhPNIHjbrWJR3rjz0RkLd813f0DF1VfUpio3gVBtdggYbnqM0X58X5mi5AhWnRMFGd0yotwdIJLVyxsi0Fuwlzs9ZyIUzmatiat8x9hBRxxQpx37jKrGFerZ0mQ7FWQWE7EdQdHPFxMKrWaAkcl5FRqr3OOMeVq90FJLRzSX1tI1EvvP6");
                Assert.AreEqual(data.SourceSystemId, "5HzHbfvvmDgPlUD2uPM0mtYk7VoGh0zVPws");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2027-02-11 14:49:07"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            PurchaseOutsourcingIssue v = new PurchaseOutsourcingIssue();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.CreatePerson = "8U5WEJFERDn1aQu8GUpvY76oyt";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2026-12-02 14:49:07");
                v.SubmitDate = DateTime.Parse("2026-01-07 14:49:07");
                v.DocNo = "LsR8BpBpTtJrx4IpTn7ThNZ7B2RoF1cTgm0jrVJG18AgKz";
                v.DocType = "9uNYFQjJLgZGQcjY1glyG3KzOLTo";
                v.SupplierId = AddBaseSupplier();
                v.Status = WMS.Model.PurchaseOutsourcingIssueStatusEnum.AllShipped;
                v.Memo = "nY17CdrhW8ny7jMXFdzy4KSLiD0Z5DA8dSi1Srer5dCK5F0wIZyLGqEpSA0LbwVhVT1aTe18siLnHv9Vdj46JLAMOL9dMvnyRR8t7TMGYHdB32DeYX6GMxuCfUq0BMVB54T0gI0dFYMe5O4CDVuXWrOgorLyCHqj93JugcRgQ8PUcNF9AU9XJtlIbGUEskEqn20FiaoEr5refCKAkiPmfNinIUKpJre4FlgY13WHytQ1Hy1zDADoQB3G4UhPNIHjbrWJR3rjz0RkLd813f0DF1VfUpio3gVBtdggYbnqM0X58X5mi5AhWnRMFGd0yotwdIJLVyxsi0Fuwlzs9ZyIUzmatiat8x9hBRxxQpx37jKrGFerZ0mQ7FWQWE7EdQdHPFxMKrWaAkcl5FRqr3OOMeVq90FJLRzSX1tI1EvvP6";
                v.SourceSystemId = "5HzHbfvvmDgPlUD2uPM0mtYk7VoGh0zVPws";
                v.LastUpdateTime = DateTime.Parse("2027-02-11 14:49:07");
                context.Set<PurchaseOutsourcingIssue>().Add(v);
                context.SaveChanges();
            }

            PurchaseOutsourcingIssueApiVM vm = _controller.Wtm.CreateVM<PurchaseOutsourcingIssueApiVM>();
            var oldID = v.ID;
            v = new PurchaseOutsourcingIssue();
            v.ID = oldID;
       		
            v.CreatePerson = "PnlXouskk0v2sC7eJ6xpuciQKeouZjWS09x4oMOTYQkj8TrN";
            v.BusinessDate = DateTime.Parse("2024-12-28 14:49:07");
            v.SubmitDate = DateTime.Parse("2026-07-08 14:49:07");
            v.DocNo = "XN4RH1kX16C8dXWsuMgUuz0k6cj3SnmlH";
            v.DocType = "YiwxY6EPoo8BPA4jSQ963mhnvtBoYVMvQyCiJoFsIlpUJixF1";
            v.Status = WMS.Model.PurchaseOutsourcingIssueStatusEnum.PartShipped;
            v.Memo = "RwqJmlS8W0LH";
            v.SourceSystemId = "3woUD9E5Pk";
            v.LastUpdateTime = DateTime.Parse("2025-03-11 14:49:07");
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.CreatePerson", "");
            vm.FC.Add("Entity.OrganizationId", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.SubmitDate", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.DocType", "");
            vm.FC.Add("Entity.SupplierId", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.Memo", "");
            vm.FC.Add("Entity.SourceSystemId", "");
            vm.FC.Add("Entity.LastUpdateTime", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<PurchaseOutsourcingIssue>().Find(v.ID);
 				
                Assert.AreEqual(data.CreatePerson, "PnlXouskk0v2sC7eJ6xpuciQKeouZjWS09x4oMOTYQkj8TrN");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-12-28 14:49:07"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2026-07-08 14:49:07"));
                Assert.AreEqual(data.DocNo, "XN4RH1kX16C8dXWsuMgUuz0k6cj3SnmlH");
                Assert.AreEqual(data.DocType, "YiwxY6EPoo8BPA4jSQ963mhnvtBoYVMvQyCiJoFsIlpUJixF1");
                Assert.AreEqual(data.Status, WMS.Model.PurchaseOutsourcingIssueStatusEnum.PartShipped);
                Assert.AreEqual(data.Memo, "RwqJmlS8W0LH");
                Assert.AreEqual(data.SourceSystemId, "3woUD9E5Pk");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2025-03-11 14:49:07"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            PurchaseOutsourcingIssue v = new PurchaseOutsourcingIssue();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.CreatePerson = "8U5WEJFERDn1aQu8GUpvY76oyt";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2026-12-02 14:49:07");
                v.SubmitDate = DateTime.Parse("2026-01-07 14:49:07");
                v.DocNo = "LsR8BpBpTtJrx4IpTn7ThNZ7B2RoF1cTgm0jrVJG18AgKz";
                v.DocType = "9uNYFQjJLgZGQcjY1glyG3KzOLTo";
                v.SupplierId = AddBaseSupplier();
                v.Status = WMS.Model.PurchaseOutsourcingIssueStatusEnum.AllShipped;
                v.Memo = "nY17CdrhW8ny7jMXFdzy4KSLiD0Z5DA8dSi1Srer5dCK5F0wIZyLGqEpSA0LbwVhVT1aTe18siLnHv9Vdj46JLAMOL9dMvnyRR8t7TMGYHdB32DeYX6GMxuCfUq0BMVB54T0gI0dFYMe5O4CDVuXWrOgorLyCHqj93JugcRgQ8PUcNF9AU9XJtlIbGUEskEqn20FiaoEr5refCKAkiPmfNinIUKpJre4FlgY13WHytQ1Hy1zDADoQB3G4UhPNIHjbrWJR3rjz0RkLd813f0DF1VfUpio3gVBtdggYbnqM0X58X5mi5AhWnRMFGd0yotwdIJLVyxsi0Fuwlzs9ZyIUzmatiat8x9hBRxxQpx37jKrGFerZ0mQ7FWQWE7EdQdHPFxMKrWaAkcl5FRqr3OOMeVq90FJLRzSX1tI1EvvP6";
                v.SourceSystemId = "5HzHbfvvmDgPlUD2uPM0mtYk7VoGh0zVPws";
                v.LastUpdateTime = DateTime.Parse("2027-02-11 14:49:07");
                context.Set<PurchaseOutsourcingIssue>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            PurchaseOutsourcingIssue v1 = new PurchaseOutsourcingIssue();
            PurchaseOutsourcingIssue v2 = new PurchaseOutsourcingIssue();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.CreatePerson = "8U5WEJFERDn1aQu8GUpvY76oyt";
                v1.OrganizationId = AddBaseOrganization();
                v1.BusinessDate = DateTime.Parse("2026-12-02 14:49:07");
                v1.SubmitDate = DateTime.Parse("2026-01-07 14:49:07");
                v1.DocNo = "LsR8BpBpTtJrx4IpTn7ThNZ7B2RoF1cTgm0jrVJG18AgKz";
                v1.DocType = "9uNYFQjJLgZGQcjY1glyG3KzOLTo";
                v1.SupplierId = AddBaseSupplier();
                v1.Status = WMS.Model.PurchaseOutsourcingIssueStatusEnum.AllShipped;
                v1.Memo = "nY17CdrhW8ny7jMXFdzy4KSLiD0Z5DA8dSi1Srer5dCK5F0wIZyLGqEpSA0LbwVhVT1aTe18siLnHv9Vdj46JLAMOL9dMvnyRR8t7TMGYHdB32DeYX6GMxuCfUq0BMVB54T0gI0dFYMe5O4CDVuXWrOgorLyCHqj93JugcRgQ8PUcNF9AU9XJtlIbGUEskEqn20FiaoEr5refCKAkiPmfNinIUKpJre4FlgY13WHytQ1Hy1zDADoQB3G4UhPNIHjbrWJR3rjz0RkLd813f0DF1VfUpio3gVBtdggYbnqM0X58X5mi5AhWnRMFGd0yotwdIJLVyxsi0Fuwlzs9ZyIUzmatiat8x9hBRxxQpx37jKrGFerZ0mQ7FWQWE7EdQdHPFxMKrWaAkcl5FRqr3OOMeVq90FJLRzSX1tI1EvvP6";
                v1.SourceSystemId = "5HzHbfvvmDgPlUD2uPM0mtYk7VoGh0zVPws";
                v1.LastUpdateTime = DateTime.Parse("2027-02-11 14:49:07");
                v2.CreatePerson = "PnlXouskk0v2sC7eJ6xpuciQKeouZjWS09x4oMOTYQkj8TrN";
                v2.OrganizationId = v1.OrganizationId; 
                v2.BusinessDate = DateTime.Parse("2024-12-28 14:49:07");
                v2.SubmitDate = DateTime.Parse("2026-07-08 14:49:07");
                v2.DocNo = "XN4RH1kX16C8dXWsuMgUuz0k6cj3SnmlH";
                v2.DocType = "YiwxY6EPoo8BPA4jSQ963mhnvtBoYVMvQyCiJoFsIlpUJixF1";
                v2.SupplierId = v1.SupplierId; 
                v2.Status = WMS.Model.PurchaseOutsourcingIssueStatusEnum.PartShipped;
                v2.Memo = "RwqJmlS8W0LH";
                v2.SourceSystemId = "3woUD9E5Pk";
                v2.LastUpdateTime = DateTime.Parse("2025-03-11 14:49:07");
                context.Set<PurchaseOutsourcingIssue>().Add(v1);
                context.Set<PurchaseOutsourcingIssue>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<PurchaseOutsourcingIssue>().Find(v1.ID);
                var data2 = context.Set<PurchaseOutsourcingIssue>().Find(v2.ID);
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
                v.IsSale = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "xByaAqByk1R0Eukxc6Uyw6";
                v.Code = "Mc3sRMWHh3Unba0yNiONe3";
                v.Name = "4bDVBL7PoXGo2ArT6dFkfpxhut";
                v.SourceSystemId = "GzEDyg";
                v.LastUpdateTime = DateTime.Parse("2026-06-28 14:49:07");
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

                v.Code = "HP1beweRMb84jrQqORYJoZ7Casj4LyUEwcu2klCdMe8wpRsJL";
                v.Name = "pHxxlMeuknlXDE2AoV4TgEliJ8g5rbbb3PxNNJ28DC6OLeHUVTte4Yxtpr6r1iW3";
                v.ShortName = "WvqmjM1WQJD23UrkUsl9dIMhM6sXoAjLAzAe6bafd58ezkHdBUtPEblFDQ";
                v.OrganizationId = AddBaseOrganization();
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "Z33f3w7YYC3tx2bV7mF25p1uyaLieFSFXrERyTIHJ4uSgW5QuUegPVk90vbWDZ83tFGmMa1H8iIr";
                v.SourceSystemId = "NK";
                v.LastUpdateTime = DateTime.Parse("2026-01-16 14:49:07");
                context.Set<BaseSupplier>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
