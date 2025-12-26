using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs;
using WMS.Model.PurchaseManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class PurchaseOutsourcingReturnApiTest
    {
        private PurchaseOutsourcingReturnApiController _controller;
        private string _seed;

        public PurchaseOutsourcingReturnApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<PurchaseOutsourcingReturnApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new PurchaseOutsourcingReturnApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            PurchaseOutsourcingReturnApiVM vm = _controller.Wtm.CreateVM<PurchaseOutsourcingReturnApiVM>();
            PurchaseOutsourcingReturn v = new PurchaseOutsourcingReturn();
            
            v.CreatePerson = "Xf5W3XjMWL94lyfLyyfi3CjAS56";
            v.OrganizationId = AddBaseOrganization();
            v.BusinessDate = DateTime.Parse("2024-08-02 14:51:26");
            v.SubmitDate = DateTime.Parse("2025-11-25 14:51:26");
            v.DocNo = "Wbs6ej5Fn6fz6UTpo";
            v.DocType = "0CBrAqm";
            v.SupplierId = AddBaseSupplier();
            v.Status = WMS.Model.PurchaseOutsourcingReturnStatusEnum.PartInWh;
            v.Memo = "A3UVCgJsZ47aihgtQj2tcn3HFgOLoPBWf4pDAneiLYtK90s";
            v.SourceSystemId = "2ZB9Vknll9tgDtxI";
            v.LastUpdateTime = DateTime.Parse("2025-04-07 14:51:26");
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<PurchaseOutsourcingReturn>().Find(v.ID);
                
                Assert.AreEqual(data.CreatePerson, "Xf5W3XjMWL94lyfLyyfi3CjAS56");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-08-02 14:51:26"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2025-11-25 14:51:26"));
                Assert.AreEqual(data.DocNo, "Wbs6ej5Fn6fz6UTpo");
                Assert.AreEqual(data.DocType, "0CBrAqm");
                Assert.AreEqual(data.Status, WMS.Model.PurchaseOutsourcingReturnStatusEnum.PartInWh);
                Assert.AreEqual(data.Memo, "A3UVCgJsZ47aihgtQj2tcn3HFgOLoPBWf4pDAneiLYtK90s");
                Assert.AreEqual(data.SourceSystemId, "2ZB9Vknll9tgDtxI");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2025-04-07 14:51:26"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            PurchaseOutsourcingReturn v = new PurchaseOutsourcingReturn();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.CreatePerson = "Xf5W3XjMWL94lyfLyyfi3CjAS56";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2024-08-02 14:51:26");
                v.SubmitDate = DateTime.Parse("2025-11-25 14:51:26");
                v.DocNo = "Wbs6ej5Fn6fz6UTpo";
                v.DocType = "0CBrAqm";
                v.SupplierId = AddBaseSupplier();
                v.Status = WMS.Model.PurchaseOutsourcingReturnStatusEnum.PartInWh;
                v.Memo = "A3UVCgJsZ47aihgtQj2tcn3HFgOLoPBWf4pDAneiLYtK90s";
                v.SourceSystemId = "2ZB9Vknll9tgDtxI";
                v.LastUpdateTime = DateTime.Parse("2025-04-07 14:51:26");
                context.Set<PurchaseOutsourcingReturn>().Add(v);
                context.SaveChanges();
            }

            PurchaseOutsourcingReturnApiVM vm = _controller.Wtm.CreateVM<PurchaseOutsourcingReturnApiVM>();
            var oldID = v.ID;
            v = new PurchaseOutsourcingReturn();
            v.ID = oldID;
       		
            v.CreatePerson = "BYGJoT9dcUYZgRe9nT5vYIpNGdFsL";
            v.BusinessDate = DateTime.Parse("2027-02-01 14:51:26");
            v.SubmitDate = DateTime.Parse("2025-04-01 14:51:26");
            v.DocNo = "BTkFYHy0tstbOG1XC2Q46I9AIiHpJOrHTNoyIwf";
            v.DocType = "iNgknf3XKUFU";
            v.Status = WMS.Model.PurchaseOutsourcingReturnStatusEnum.NotReceive;
            v.Memo = "dCzaqx";
            v.SourceSystemId = "HKSWbph9javhuceblb5MHZglJ";
            v.LastUpdateTime = DateTime.Parse("2026-08-20 14:51:26");
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
                var data = context.Set<PurchaseOutsourcingReturn>().Find(v.ID);
 				
                Assert.AreEqual(data.CreatePerson, "BYGJoT9dcUYZgRe9nT5vYIpNGdFsL");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2027-02-01 14:51:26"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2025-04-01 14:51:26"));
                Assert.AreEqual(data.DocNo, "BTkFYHy0tstbOG1XC2Q46I9AIiHpJOrHTNoyIwf");
                Assert.AreEqual(data.DocType, "iNgknf3XKUFU");
                Assert.AreEqual(data.Status, WMS.Model.PurchaseOutsourcingReturnStatusEnum.NotReceive);
                Assert.AreEqual(data.Memo, "dCzaqx");
                Assert.AreEqual(data.SourceSystemId, "HKSWbph9javhuceblb5MHZglJ");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2026-08-20 14:51:26"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            PurchaseOutsourcingReturn v = new PurchaseOutsourcingReturn();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.CreatePerson = "Xf5W3XjMWL94lyfLyyfi3CjAS56";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2024-08-02 14:51:26");
                v.SubmitDate = DateTime.Parse("2025-11-25 14:51:26");
                v.DocNo = "Wbs6ej5Fn6fz6UTpo";
                v.DocType = "0CBrAqm";
                v.SupplierId = AddBaseSupplier();
                v.Status = WMS.Model.PurchaseOutsourcingReturnStatusEnum.PartInWh;
                v.Memo = "A3UVCgJsZ47aihgtQj2tcn3HFgOLoPBWf4pDAneiLYtK90s";
                v.SourceSystemId = "2ZB9Vknll9tgDtxI";
                v.LastUpdateTime = DateTime.Parse("2025-04-07 14:51:26");
                context.Set<PurchaseOutsourcingReturn>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            PurchaseOutsourcingReturn v1 = new PurchaseOutsourcingReturn();
            PurchaseOutsourcingReturn v2 = new PurchaseOutsourcingReturn();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.CreatePerson = "Xf5W3XjMWL94lyfLyyfi3CjAS56";
                v1.OrganizationId = AddBaseOrganization();
                v1.BusinessDate = DateTime.Parse("2024-08-02 14:51:26");
                v1.SubmitDate = DateTime.Parse("2025-11-25 14:51:26");
                v1.DocNo = "Wbs6ej5Fn6fz6UTpo";
                v1.DocType = "0CBrAqm";
                v1.SupplierId = AddBaseSupplier();
                v1.Status = WMS.Model.PurchaseOutsourcingReturnStatusEnum.PartInWh;
                v1.Memo = "A3UVCgJsZ47aihgtQj2tcn3HFgOLoPBWf4pDAneiLYtK90s";
                v1.SourceSystemId = "2ZB9Vknll9tgDtxI";
                v1.LastUpdateTime = DateTime.Parse("2025-04-07 14:51:26");
                v2.CreatePerson = "BYGJoT9dcUYZgRe9nT5vYIpNGdFsL";
                v2.OrganizationId = v1.OrganizationId; 
                v2.BusinessDate = DateTime.Parse("2027-02-01 14:51:26");
                v2.SubmitDate = DateTime.Parse("2025-04-01 14:51:26");
                v2.DocNo = "BTkFYHy0tstbOG1XC2Q46I9AIiHpJOrHTNoyIwf";
                v2.DocType = "iNgknf3XKUFU";
                v2.SupplierId = v1.SupplierId; 
                v2.Status = WMS.Model.PurchaseOutsourcingReturnStatusEnum.NotReceive;
                v2.Memo = "dCzaqx";
                v2.SourceSystemId = "HKSWbph9javhuceblb5MHZglJ";
                v2.LastUpdateTime = DateTime.Parse("2026-08-20 14:51:26");
                context.Set<PurchaseOutsourcingReturn>().Add(v1);
                context.Set<PurchaseOutsourcingReturn>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<PurchaseOutsourcingReturn>().Find(v1.ID);
                var data2 = context.Set<PurchaseOutsourcingReturn>().Find(v2.ID);
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
                v.Memo = "XmrkkTIbe7FPTusAtVpX7UBexKjODrcz6CAyzk3mzyLCXPN5O9M6s04xSLEjA1luaB8zxtylU04LYON6pGgTQf0prFnNrUgJffOHyRFXyXLcEez2V87XdOlvMhX8gaN2fadZtXJzIAMjiQTWkQ923dHs2Peq6VU7f3bAWol9Lrfkntqkw4ea8nctyU8zj8q2OvxPbWD37Z0uRipdqMFuwC1Vg79h98vYnDghqvi";
                v.Code = "LfbrwfY5";
                v.Name = "WWO6pWvwneu8k3u5snJrRFD1zC";
                v.SourceSystemId = "KA5I";
                v.LastUpdateTime = DateTime.Parse("2025-10-19 14:51:26");
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

                v.Code = "bj0AyjQJ9dxHdSZXBqyXafXj8GhNMgFUU";
                v.Name = "Wxo4g5n0L6CQg7jbZRNRyZF4hu58mqR1x9Crbu4kwua8StUxU3Fswq3kGflS142Yl9urLYirDOulcASeE";
                v.ShortName = "na16JisGjzUjkg2GvqIXNYCzQgFKAalIxxUmB0S63lF8TFj3M7Jtuzux9Dkdbo";
                v.OrganizationId = AddBaseOrganization();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "izcIEwPCxI61wKL1qkGPIFKabFiCotVqtK26b4tQXYg0IgGfTbPR5NTblEUjm8clwxJrcY7hn8I";
                v.SourceSystemId = "wrHBB9JEz";
                v.LastUpdateTime = DateTime.Parse("2026-03-09 14:51:26");
                context.Set<BaseSupplier>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
