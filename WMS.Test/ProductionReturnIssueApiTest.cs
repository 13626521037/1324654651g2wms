using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs;
using WMS.Model.ProductionManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class ProductionReturnIssueApiTest
    {
        private ProductionReturnIssueApiController _controller;
        private string _seed;

        public ProductionReturnIssueApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<ProductionReturnIssueApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new ProductionReturnIssueApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            ProductionReturnIssueApiVM vm = _controller.Wtm.CreateVM<ProductionReturnIssueApiVM>();
            ProductionReturnIssue v = new ProductionReturnIssue();
            
            v.CreatePerson = "HhcettR0yx87";
            v.OrganizationId = AddBaseOrganization();
            v.BusinessDate = DateTime.Parse("2025-11-11 09:19:59");
            v.SubmitDate = DateTime.Parse("2024-12-05 09:19:59");
            v.DocNo = "PE0OPiV1NgF6wGdCasSgB";
            v.DocType = "tKNkQ2AsFrFZMUPpgbTS2oHj1Da869i1oLliM6EwUnIx2";
            v.Status = WMS.Model.ProductionReturnIssueStatusEnum.PartReceive;
            v.Memo = "CYO80Q4D1rqbhQ2MKLiy0Gi3KdT9yKr34wCNIFJW6P5s4HMnOngi2bBDgLyBpIxlK0M4bQEuK2yL8M0MwYJWr3JUaAp8E1c7A2n0B8NUCKUIIpnjp0zunZY1TmhWYK0PEsZhxYkgAXoe4ZX";
            v.SourceSystemId = "eNhmECI";
            v.LastUpdateTime = DateTime.Parse("2025-10-04 09:19:59");
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ProductionReturnIssue>().Find(v.ID);
                
                Assert.AreEqual(data.CreatePerson, "HhcettR0yx87");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2025-11-11 09:19:59"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2024-12-05 09:19:59"));
                Assert.AreEqual(data.DocNo, "PE0OPiV1NgF6wGdCasSgB");
                Assert.AreEqual(data.DocType, "tKNkQ2AsFrFZMUPpgbTS2oHj1Da869i1oLliM6EwUnIx2");
                Assert.AreEqual(data.Status, WMS.Model.ProductionReturnIssueStatusEnum.PartReceive);
                Assert.AreEqual(data.Memo, "CYO80Q4D1rqbhQ2MKLiy0Gi3KdT9yKr34wCNIFJW6P5s4HMnOngi2bBDgLyBpIxlK0M4bQEuK2yL8M0MwYJWr3JUaAp8E1c7A2n0B8NUCKUIIpnjp0zunZY1TmhWYK0PEsZhxYkgAXoe4ZX");
                Assert.AreEqual(data.SourceSystemId, "eNhmECI");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2025-10-04 09:19:59"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            ProductionReturnIssue v = new ProductionReturnIssue();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.CreatePerson = "HhcettR0yx87";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2025-11-11 09:19:59");
                v.SubmitDate = DateTime.Parse("2024-12-05 09:19:59");
                v.DocNo = "PE0OPiV1NgF6wGdCasSgB";
                v.DocType = "tKNkQ2AsFrFZMUPpgbTS2oHj1Da869i1oLliM6EwUnIx2";
                v.Status = WMS.Model.ProductionReturnIssueStatusEnum.PartReceive;
                v.Memo = "CYO80Q4D1rqbhQ2MKLiy0Gi3KdT9yKr34wCNIFJW6P5s4HMnOngi2bBDgLyBpIxlK0M4bQEuK2yL8M0MwYJWr3JUaAp8E1c7A2n0B8NUCKUIIpnjp0zunZY1TmhWYK0PEsZhxYkgAXoe4ZX";
                v.SourceSystemId = "eNhmECI";
                v.LastUpdateTime = DateTime.Parse("2025-10-04 09:19:59");
                context.Set<ProductionReturnIssue>().Add(v);
                context.SaveChanges();
            }

            ProductionReturnIssueApiVM vm = _controller.Wtm.CreateVM<ProductionReturnIssueApiVM>();
            var oldID = v.ID;
            v = new ProductionReturnIssue();
            v.ID = oldID;
       		
            v.CreatePerson = "o4my";
            v.BusinessDate = DateTime.Parse("2026-03-26 09:19:59");
            v.SubmitDate = DateTime.Parse("2026-07-04 09:19:59");
            v.DocNo = "VuKeIsbPQZTkCdAVUiinQ7kDtPH84dMThtx3V";
            v.DocType = "3cEYWs89";
            v.Status = WMS.Model.ProductionReturnIssueStatusEnum.AllInWh;
            v.Memo = "yiJ5PFELiREsUiNDtw9XHhFKoIbyh0FENJ0e3VekPRys85IzFYo2lahg89OWesPuBerwmLAi01filAfykosbrFerO9QIeaTWGkNZPyaQQ1VM6ILz6vsAAttEwWQuqSOzG7Z65aUOktagv5q6zDeAluj6bapfu7d0AgC9xWAUgqHzk0ynBdz4zK41Xgw2D7kRRl2mrdC4HjOOsINFzVx1nzwfzju2fbLJJCyqL8GesPqF0mJMN84U60BU5fcOIMf3Bq6XkOOfCXLPe7s69Y3ClwmqFT3VUb36zOz7u8Y8ax";
            v.SourceSystemId = "yterbDr115VfrsfAnpB9Gld5hVPT7dQq5TkRvg7re";
            v.LastUpdateTime = DateTime.Parse("2026-03-23 09:19:59");
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.CreatePerson", "");
            vm.FC.Add("Entity.OrganizationId", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.SubmitDate", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.DocType", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.Memo", "");
            vm.FC.Add("Entity.SourceSystemId", "");
            vm.FC.Add("Entity.LastUpdateTime", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ProductionReturnIssue>().Find(v.ID);
 				
                Assert.AreEqual(data.CreatePerson, "o4my");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2026-03-26 09:19:59"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2026-07-04 09:19:59"));
                Assert.AreEqual(data.DocNo, "VuKeIsbPQZTkCdAVUiinQ7kDtPH84dMThtx3V");
                Assert.AreEqual(data.DocType, "3cEYWs89");
                Assert.AreEqual(data.Status, WMS.Model.ProductionReturnIssueStatusEnum.AllInWh);
                Assert.AreEqual(data.Memo, "yiJ5PFELiREsUiNDtw9XHhFKoIbyh0FENJ0e3VekPRys85IzFYo2lahg89OWesPuBerwmLAi01filAfykosbrFerO9QIeaTWGkNZPyaQQ1VM6ILz6vsAAttEwWQuqSOzG7Z65aUOktagv5q6zDeAluj6bapfu7d0AgC9xWAUgqHzk0ynBdz4zK41Xgw2D7kRRl2mrdC4HjOOsINFzVx1nzwfzju2fbLJJCyqL8GesPqF0mJMN84U60BU5fcOIMf3Bq6XkOOfCXLPe7s69Y3ClwmqFT3VUb36zOz7u8Y8ax");
                Assert.AreEqual(data.SourceSystemId, "yterbDr115VfrsfAnpB9Gld5hVPT7dQq5TkRvg7re");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2026-03-23 09:19:59"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            ProductionReturnIssue v = new ProductionReturnIssue();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.CreatePerson = "HhcettR0yx87";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2025-11-11 09:19:59");
                v.SubmitDate = DateTime.Parse("2024-12-05 09:19:59");
                v.DocNo = "PE0OPiV1NgF6wGdCasSgB";
                v.DocType = "tKNkQ2AsFrFZMUPpgbTS2oHj1Da869i1oLliM6EwUnIx2";
                v.Status = WMS.Model.ProductionReturnIssueStatusEnum.PartReceive;
                v.Memo = "CYO80Q4D1rqbhQ2MKLiy0Gi3KdT9yKr34wCNIFJW6P5s4HMnOngi2bBDgLyBpIxlK0M4bQEuK2yL8M0MwYJWr3JUaAp8E1c7A2n0B8NUCKUIIpnjp0zunZY1TmhWYK0PEsZhxYkgAXoe4ZX";
                v.SourceSystemId = "eNhmECI";
                v.LastUpdateTime = DateTime.Parse("2025-10-04 09:19:59");
                context.Set<ProductionReturnIssue>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            ProductionReturnIssue v1 = new ProductionReturnIssue();
            ProductionReturnIssue v2 = new ProductionReturnIssue();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.CreatePerson = "HhcettR0yx87";
                v1.OrganizationId = AddBaseOrganization();
                v1.BusinessDate = DateTime.Parse("2025-11-11 09:19:59");
                v1.SubmitDate = DateTime.Parse("2024-12-05 09:19:59");
                v1.DocNo = "PE0OPiV1NgF6wGdCasSgB";
                v1.DocType = "tKNkQ2AsFrFZMUPpgbTS2oHj1Da869i1oLliM6EwUnIx2";
                v1.Status = WMS.Model.ProductionReturnIssueStatusEnum.PartReceive;
                v1.Memo = "CYO80Q4D1rqbhQ2MKLiy0Gi3KdT9yKr34wCNIFJW6P5s4HMnOngi2bBDgLyBpIxlK0M4bQEuK2yL8M0MwYJWr3JUaAp8E1c7A2n0B8NUCKUIIpnjp0zunZY1TmhWYK0PEsZhxYkgAXoe4ZX";
                v1.SourceSystemId = "eNhmECI";
                v1.LastUpdateTime = DateTime.Parse("2025-10-04 09:19:59");
                v2.CreatePerson = "o4my";
                v2.OrganizationId = v1.OrganizationId; 
                v2.BusinessDate = DateTime.Parse("2026-03-26 09:19:59");
                v2.SubmitDate = DateTime.Parse("2026-07-04 09:19:59");
                v2.DocNo = "VuKeIsbPQZTkCdAVUiinQ7kDtPH84dMThtx3V";
                v2.DocType = "3cEYWs89";
                v2.Status = WMS.Model.ProductionReturnIssueStatusEnum.AllInWh;
                v2.Memo = "yiJ5PFELiREsUiNDtw9XHhFKoIbyh0FENJ0e3VekPRys85IzFYo2lahg89OWesPuBerwmLAi01filAfykosbrFerO9QIeaTWGkNZPyaQQ1VM6ILz6vsAAttEwWQuqSOzG7Z65aUOktagv5q6zDeAluj6bapfu7d0AgC9xWAUgqHzk0ynBdz4zK41Xgw2D7kRRl2mrdC4HjOOsINFzVx1nzwfzju2fbLJJCyqL8GesPqF0mJMN84U60BU5fcOIMf3Bq6XkOOfCXLPe7s69Y3ClwmqFT3VUb36zOz7u8Y8ax";
                v2.SourceSystemId = "yterbDr115VfrsfAnpB9Gld5hVPT7dQq5TkRvg7re";
                v2.LastUpdateTime = DateTime.Parse("2026-03-23 09:19:59");
                context.Set<ProductionReturnIssue>().Add(v1);
                context.Set<ProductionReturnIssue>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<ProductionReturnIssue>().Find(v1.ID);
                var data2 = context.Set<ProductionReturnIssue>().Find(v2.ID);
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
                v.IsSale = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "WOAnfCigsUrdQwqpEHSySQcBEbhVZq85sDDnSoUC4kFgtbP1jb9RjZ95s18sr6Aoq2rjzBJF9AtZlN6Y1Os5SeP6kJOAu0LtjvoJQns03VFLMXmPF0xVSOB4WqX5ZkqAHInmewNwnvOFzulpWUjRpURsjfglQb8HfASjCVvq2p1ijodhFBRRNXxYl1gE1B6ksZFQIo5zEobO0IFucW375d8WP9lKcYPpKWaN5MWSwkifmBhrD91OG6Q1xwgw7sjcyz32Rn3Hn0RPVsjm98m0JaaILEy7mF1lirHmGFSI4gB5Cqg5dyoAZQiqqPqnfFFXaqgWVNbGLsMKKmpZ3KarsRqcdGeTNOuaEHRJufrGunZ6hdnZ0ti9sxR86tcPy2K";
                v.Code = "JILiZENIiVwqOtalSSod";
                v.Name = "OYSwG";
                v.SourceSystemId = "3KJz20eZGmuEjibENMM4T9TeWSTbleFR3fMqKL";
                v.LastUpdateTime = DateTime.Parse("2025-12-10 09:19:59");
                v.CodeAndName = "nurBZ";
                context.Set<BaseOrganization>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
