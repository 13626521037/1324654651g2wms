using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs;
using WMS.Model.PurchaseManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class PurchaseReturnApiTest
    {
        private PurchaseReturnApiController _controller;
        private string _seed;

        public PurchaseReturnApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<PurchaseReturnApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new PurchaseReturnApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            PurchaseReturnApiVM vm = _controller.Wtm.CreateVM<PurchaseReturnApiVM>();
            PurchaseReturn v = new PurchaseReturn();
            
            v.LastUpdatePerson = "GOXxI1V8XhTgPs9osbt1WI241paQT";
            v.OrganizationId = AddBaseOrganization();
            v.BusinessDate = DateTime.Parse("2024-03-27 14:55:18");
            v.CreateDate = DateTime.Parse("2024-11-16 14:55:18");
            v.DocNo = "WKoU4pyi5ya";
            v.DocType = "Re6nWR07GN1HWnbZX4Z";
            v.SupplierId = AddBaseSupplier();
            v.Status = WMS.Model.PurchaseReturnStatusEnum.AllShipped;
            v.Memo = "tX6TYor1gJ3FcIBLCylXv3gV4WeXiWU4O9p7iQM1TVaKZIvJTbV7wXxdppYZwcul8jkAe2jZJ3dmYeearwe1QxXMM9BVaAIJvRhIuJKiNQA64nU8JEc1jz4xzxGJEBu1Ux4xUSzjct2B4xNhRKZiqOK3cy9HW0OVvK8GdeDdKwo6RMusWeyss0GfWbeS4UcGqaZ0mVC7P67NMNTNRwEIzVQpQvBUecKviMBB68idUcmRgkKv0wMfLYcgmvPqDmeHsMu2DSOpwHv43fTVzEciDLmIWznzKKmucRJGU8XZi52HLo4pm98oBTLE7Q1FvrN25472i9RQKn3WlMJaQuTmLShRzzKxLXqt9kemR02J0bAwV6AxEt";
            v.SourceSystemId = "43Mv79Nuj69vL62XMhEF2Ko267tj99n3KHKh5L";
            v.LastUpdateTime = DateTime.Parse("2026-07-28 14:55:18");
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<PurchaseReturn>().Find(v.ID);
                
                Assert.AreEqual(data.LastUpdatePerson, "GOXxI1V8XhTgPs9osbt1WI241paQT");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-03-27 14:55:18"));
                Assert.AreEqual(data.CreateDate, DateTime.Parse("2024-11-16 14:55:18"));
                Assert.AreEqual(data.DocNo, "WKoU4pyi5ya");
                Assert.AreEqual(data.DocType, "Re6nWR07GN1HWnbZX4Z");
                Assert.AreEqual(data.Status, WMS.Model.PurchaseReturnStatusEnum.AllShipped);
                Assert.AreEqual(data.Memo, "tX6TYor1gJ3FcIBLCylXv3gV4WeXiWU4O9p7iQM1TVaKZIvJTbV7wXxdppYZwcul8jkAe2jZJ3dmYeearwe1QxXMM9BVaAIJvRhIuJKiNQA64nU8JEc1jz4xzxGJEBu1Ux4xUSzjct2B4xNhRKZiqOK3cy9HW0OVvK8GdeDdKwo6RMusWeyss0GfWbeS4UcGqaZ0mVC7P67NMNTNRwEIzVQpQvBUecKviMBB68idUcmRgkKv0wMfLYcgmvPqDmeHsMu2DSOpwHv43fTVzEciDLmIWznzKKmucRJGU8XZi52HLo4pm98oBTLE7Q1FvrN25472i9RQKn3WlMJaQuTmLShRzzKxLXqt9kemR02J0bAwV6AxEt");
                Assert.AreEqual(data.SourceSystemId, "43Mv79Nuj69vL62XMhEF2Ko267tj99n3KHKh5L");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2026-07-28 14:55:18"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            PurchaseReturn v = new PurchaseReturn();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.LastUpdatePerson = "GOXxI1V8XhTgPs9osbt1WI241paQT";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2024-03-27 14:55:18");
                v.CreateDate = DateTime.Parse("2024-11-16 14:55:18");
                v.DocNo = "WKoU4pyi5ya";
                v.DocType = "Re6nWR07GN1HWnbZX4Z";
                v.SupplierId = AddBaseSupplier();
                v.Status = WMS.Model.PurchaseReturnStatusEnum.AllShipped;
                v.Memo = "tX6TYor1gJ3FcIBLCylXv3gV4WeXiWU4O9p7iQM1TVaKZIvJTbV7wXxdppYZwcul8jkAe2jZJ3dmYeearwe1QxXMM9BVaAIJvRhIuJKiNQA64nU8JEc1jz4xzxGJEBu1Ux4xUSzjct2B4xNhRKZiqOK3cy9HW0OVvK8GdeDdKwo6RMusWeyss0GfWbeS4UcGqaZ0mVC7P67NMNTNRwEIzVQpQvBUecKviMBB68idUcmRgkKv0wMfLYcgmvPqDmeHsMu2DSOpwHv43fTVzEciDLmIWznzKKmucRJGU8XZi52HLo4pm98oBTLE7Q1FvrN25472i9RQKn3WlMJaQuTmLShRzzKxLXqt9kemR02J0bAwV6AxEt";
                v.SourceSystemId = "43Mv79Nuj69vL62XMhEF2Ko267tj99n3KHKh5L";
                v.LastUpdateTime = DateTime.Parse("2026-07-28 14:55:18");
                context.Set<PurchaseReturn>().Add(v);
                context.SaveChanges();
            }

            PurchaseReturnApiVM vm = _controller.Wtm.CreateVM<PurchaseReturnApiVM>();
            var oldID = v.ID;
            v = new PurchaseReturn();
            v.ID = oldID;
       		
            v.LastUpdatePerson = "4TbahLOddg9n9rz";
            v.BusinessDate = DateTime.Parse("2026-06-19 14:55:18");
            v.CreateDate = DateTime.Parse("2025-09-08 14:55:18");
            v.DocNo = "WysYhXKJ16NEdXQLSmx3wjMLeyCu484Y";
            v.DocType = "D3b3UCfhA803e";
            v.Status = WMS.Model.PurchaseReturnStatusEnum.PartShipped;
            v.Memo = "W5QRqHdpz96L6e2VvYdeIelCM7oFS0FmHePU0REexD4LZha4vxyTWxgtyxApxkowTPgwFVN9VvqXSxnwzMgobMyiwLMarSdLRFtQwIS3rJnNSgNZo6SNggQUDhYhk0e0N2JDxQWqEV37d1SeBPmRhQS8iafcVL4bjugv8rUVB7SqIhv05YsFQET5hEAeTkxpFeBHdSWQNtVsliLNkIkjhrROKfOd5g65jRGNNGA8RX8CwkzMDtAjcjfvn5cYtvIz9C225y3YFk0NI4Dzsg4czBxL5tXLqWmAz3ktRu7541DLI86dZrTKXkiruE6gHtTwZFcyaxlnw3AKzfx3b9tL5XI3wjyax6AC7PDqwE26gc4q3bta1CKrOlmLyIORCn9Eb1kJpxSOPTFH6y4VEJoI2zl0ItTPoxEywR7X2mpJecKYN8CgF90MPLoo9ea0GSCzedo7UntJwW6XaFdklTycmJciUTB";
            v.SourceSystemId = "s9tIPUdRn12VELq1j5rZl4H8aoREmEzsFKvb1OKA";
            v.LastUpdateTime = DateTime.Parse("2024-10-30 14:55:18");
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.LastUpdatePerson", "");
            vm.FC.Add("Entity.OrganizationId", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.CreateDate", "");
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
                var data = context.Set<PurchaseReturn>().Find(v.ID);
 				
                Assert.AreEqual(data.LastUpdatePerson, "4TbahLOddg9n9rz");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2026-06-19 14:55:18"));
                Assert.AreEqual(data.CreateDate, DateTime.Parse("2025-09-08 14:55:18"));
                Assert.AreEqual(data.DocNo, "WysYhXKJ16NEdXQLSmx3wjMLeyCu484Y");
                Assert.AreEqual(data.DocType, "D3b3UCfhA803e");
                Assert.AreEqual(data.Status, WMS.Model.PurchaseReturnStatusEnum.PartShipped);
                Assert.AreEqual(data.Memo, "W5QRqHdpz96L6e2VvYdeIelCM7oFS0FmHePU0REexD4LZha4vxyTWxgtyxApxkowTPgwFVN9VvqXSxnwzMgobMyiwLMarSdLRFtQwIS3rJnNSgNZo6SNggQUDhYhk0e0N2JDxQWqEV37d1SeBPmRhQS8iafcVL4bjugv8rUVB7SqIhv05YsFQET5hEAeTkxpFeBHdSWQNtVsliLNkIkjhrROKfOd5g65jRGNNGA8RX8CwkzMDtAjcjfvn5cYtvIz9C225y3YFk0NI4Dzsg4czBxL5tXLqWmAz3ktRu7541DLI86dZrTKXkiruE6gHtTwZFcyaxlnw3AKzfx3b9tL5XI3wjyax6AC7PDqwE26gc4q3bta1CKrOlmLyIORCn9Eb1kJpxSOPTFH6y4VEJoI2zl0ItTPoxEywR7X2mpJecKYN8CgF90MPLoo9ea0GSCzedo7UntJwW6XaFdklTycmJciUTB");
                Assert.AreEqual(data.SourceSystemId, "s9tIPUdRn12VELq1j5rZl4H8aoREmEzsFKvb1OKA");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2024-10-30 14:55:18"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            PurchaseReturn v = new PurchaseReturn();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.LastUpdatePerson = "GOXxI1V8XhTgPs9osbt1WI241paQT";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2024-03-27 14:55:18");
                v.CreateDate = DateTime.Parse("2024-11-16 14:55:18");
                v.DocNo = "WKoU4pyi5ya";
                v.DocType = "Re6nWR07GN1HWnbZX4Z";
                v.SupplierId = AddBaseSupplier();
                v.Status = WMS.Model.PurchaseReturnStatusEnum.AllShipped;
                v.Memo = "tX6TYor1gJ3FcIBLCylXv3gV4WeXiWU4O9p7iQM1TVaKZIvJTbV7wXxdppYZwcul8jkAe2jZJ3dmYeearwe1QxXMM9BVaAIJvRhIuJKiNQA64nU8JEc1jz4xzxGJEBu1Ux4xUSzjct2B4xNhRKZiqOK3cy9HW0OVvK8GdeDdKwo6RMusWeyss0GfWbeS4UcGqaZ0mVC7P67NMNTNRwEIzVQpQvBUecKviMBB68idUcmRgkKv0wMfLYcgmvPqDmeHsMu2DSOpwHv43fTVzEciDLmIWznzKKmucRJGU8XZi52HLo4pm98oBTLE7Q1FvrN25472i9RQKn3WlMJaQuTmLShRzzKxLXqt9kemR02J0bAwV6AxEt";
                v.SourceSystemId = "43Mv79Nuj69vL62XMhEF2Ko267tj99n3KHKh5L";
                v.LastUpdateTime = DateTime.Parse("2026-07-28 14:55:18");
                context.Set<PurchaseReturn>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            PurchaseReturn v1 = new PurchaseReturn();
            PurchaseReturn v2 = new PurchaseReturn();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LastUpdatePerson = "GOXxI1V8XhTgPs9osbt1WI241paQT";
                v1.OrganizationId = AddBaseOrganization();
                v1.BusinessDate = DateTime.Parse("2024-03-27 14:55:18");
                v1.CreateDate = DateTime.Parse("2024-11-16 14:55:18");
                v1.DocNo = "WKoU4pyi5ya";
                v1.DocType = "Re6nWR07GN1HWnbZX4Z";
                v1.SupplierId = AddBaseSupplier();
                v1.Status = WMS.Model.PurchaseReturnStatusEnum.AllShipped;
                v1.Memo = "tX6TYor1gJ3FcIBLCylXv3gV4WeXiWU4O9p7iQM1TVaKZIvJTbV7wXxdppYZwcul8jkAe2jZJ3dmYeearwe1QxXMM9BVaAIJvRhIuJKiNQA64nU8JEc1jz4xzxGJEBu1Ux4xUSzjct2B4xNhRKZiqOK3cy9HW0OVvK8GdeDdKwo6RMusWeyss0GfWbeS4UcGqaZ0mVC7P67NMNTNRwEIzVQpQvBUecKviMBB68idUcmRgkKv0wMfLYcgmvPqDmeHsMu2DSOpwHv43fTVzEciDLmIWznzKKmucRJGU8XZi52HLo4pm98oBTLE7Q1FvrN25472i9RQKn3WlMJaQuTmLShRzzKxLXqt9kemR02J0bAwV6AxEt";
                v1.SourceSystemId = "43Mv79Nuj69vL62XMhEF2Ko267tj99n3KHKh5L";
                v1.LastUpdateTime = DateTime.Parse("2026-07-28 14:55:18");
                v2.LastUpdatePerson = "4TbahLOddg9n9rz";
                v2.OrganizationId = v1.OrganizationId; 
                v2.BusinessDate = DateTime.Parse("2026-06-19 14:55:18");
                v2.CreateDate = DateTime.Parse("2025-09-08 14:55:18");
                v2.DocNo = "WysYhXKJ16NEdXQLSmx3wjMLeyCu484Y";
                v2.DocType = "D3b3UCfhA803e";
                v2.SupplierId = v1.SupplierId; 
                v2.Status = WMS.Model.PurchaseReturnStatusEnum.PartShipped;
                v2.Memo = "W5QRqHdpz96L6e2VvYdeIelCM7oFS0FmHePU0REexD4LZha4vxyTWxgtyxApxkowTPgwFVN9VvqXSxnwzMgobMyiwLMarSdLRFtQwIS3rJnNSgNZo6SNggQUDhYhk0e0N2JDxQWqEV37d1SeBPmRhQS8iafcVL4bjugv8rUVB7SqIhv05YsFQET5hEAeTkxpFeBHdSWQNtVsliLNkIkjhrROKfOd5g65jRGNNGA8RX8CwkzMDtAjcjfvn5cYtvIz9C225y3YFk0NI4Dzsg4czBxL5tXLqWmAz3ktRu7541DLI86dZrTKXkiruE6gHtTwZFcyaxlnw3AKzfx3b9tL5XI3wjyax6AC7PDqwE26gc4q3bta1CKrOlmLyIORCn9Eb1kJpxSOPTFH6y4VEJoI2zl0ItTPoxEywR7X2mpJecKYN8CgF90MPLoo9ea0GSCzedo7UntJwW6XaFdklTycmJciUTB";
                v2.SourceSystemId = "s9tIPUdRn12VELq1j5rZl4H8aoREmEzsFKvb1OKA";
                v2.LastUpdateTime = DateTime.Parse("2024-10-30 14:55:18");
                context.Set<PurchaseReturn>().Add(v1);
                context.Set<PurchaseReturn>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<PurchaseReturn>().Find(v1.ID);
                var data2 = context.Set<PurchaseReturn>().Find(v2.ID);
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
                v.Memo = "mkaWqAA6J3FMBOjfN0d5agXPxzWp06Kxk5rQ8765HHIXoJPWZAytxTERCnH47LkIGInoLwQriUTeXZpCBUFS7jVahKE3Gd3sDTjLPz1IMM0CBSogpusr7npufz2BUGrMDVJeWzm8QCgMpCPIuXpprNjndTp3ohvv5lrKkGa1dNNABFcZdyCDFXTtkbbLOhD74fzAcW3vKpovObr7eod5lzUlswS65Fm3GGLULhZYplE3B862Iv8ykKaknussPkGB5ZJ8VGWdsY2bBOBm7pkZE7bU66BjnKCeSNP9yd70gM6LK9f8lY3U77PhqUaXnmn50AVexqop5kinc2v5ofGCtPILWYza2w9tT";
                v.Code = "80E4Im8rbLhKkVmqOPKpBRTSbwuZmue5lcld";
                v.Name = "PJtP8OpjU7";
                v.SourceSystemId = "BWb5W0RjLw9M3eplTUAkjuUYDTgs";
                v.LastUpdateTime = DateTime.Parse("2024-05-04 14:55:18");
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

                v.Code = "JOUwd0AJDISb";
                v.Name = "tqFyPa4PEv3kNaP6q8PR4OwmuogIklhkMvqlJzT3540M";
                v.ShortName = "b6oFHJ9giRj37cbQvDwJQPdINHNDb4a1C4o6OYCBR1jRZFOJZChlZSqJzlsGUYRYmEM04ymL9Y3GJ9NwKxNhecM";
                v.OrganizationId = AddBaseOrganization();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "RZxEf6sYmhS2YxDPjbJ3xZYTnvILaT7icwhkDyz1KQ9Fu6F13XjGtCxVvyTi5KpSaBMPxhwGNzGtoMo26tJ7YI3tOuxs8cZySZxANVXYg7PWX4nIAieYHayvQgl3bBMMtaQuVrXsBJWqqrfvPmu0UPCf5L5PCbkX6m8ngzeUjTbU2PXKc0FuIuXMLElzGZakgqo7HrCC1Eq3pnyJwFUFdoUDoPCkOQJutdXye4Dl4o25nNSYkXtTNbWKzLGsjoN9g9EWNzRSftLjfYg1R0HCOWJCDLBi9Wlp6V6igQ8mPEKAdFgT1E1Z18mGvdH3JnqMiT8mLjJRbf9LsvxSWQZTPctQQmviRgrijubbTnMAoyvcgk6QqX3WMyTxSYmNYM628cRmJPX3fqDd7AANUTU244SR5Wgp8roR6O0RFEXjS1NSdfnAkFHc2572qb8iiSs0oM9I6xT6Lsew9rvH2No4V2HP33wvt7iKeb";
                v.SourceSystemId = "Mzfe6rEggR";
                v.LastUpdateTime = DateTime.Parse("2026-06-07 14:55:18");
                context.Set<BaseSupplier>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
