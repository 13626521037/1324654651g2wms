using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventorySplitVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class InventorySplitApiTest
    {
        private InventorySplitApiController _controller;
        private string _seed;

        public InventorySplitApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventorySplitApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventorySplitApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventorySplitApiVM vm = _controller.Wtm.CreateVM<InventorySplitApiVM>();
            InventorySplit v = new InventorySplit();
            
            v.DocNo = "MmjS46o9XkMqxiOL6";
            v.OldInvId = AddBaseInventory();
            v.NewInvId = AddBaseInventory();
            v.OrigQty = 352266175;
            v.SplitQty = 222108831;
            v.Memo = "ch4WsFGlcfJQScmOaMt9KOhFEQhyxb7eWpPl8ydOGzCdAQTNUsTdEC7r79Ax9VyGE03SoLdBEBlYnXUmXSz2O5srcCnqq50RJBBP7WsnyQ35QSNshIg4FY5cClIGOljnNHnmtBQciuOPcRxAWkFQxQHhqPlW7bFy6sI1dEh51O1mXsVQR5fhOGtMLwFbmXGAJ5A6XFw1VS91HBpQ4RdPO6jQVbbjl4j4S2IztkiIoPPGHbhI4bSBoYfri9g82eknSCUzHhe804uZdOOqUzvKPREcaPITg0ALXLJEUh8Mc9XPQyPUk8ohu52MsxlRgd2VfQ4QiQMjzbuCDn4iHW7mbdg92VmtINDQCkq0fKH3HAXa0syz5AYSSWTL6hwnkpaRSI8l1bn";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventorySplit>().Find(v.ID);
                
                Assert.AreEqual(data.DocNo, "MmjS46o9XkMqxiOL6");
                Assert.AreEqual(data.OrigQty, 352266175);
                Assert.AreEqual(data.SplitQty, 222108831);
                Assert.AreEqual(data.Memo, "ch4WsFGlcfJQScmOaMt9KOhFEQhyxb7eWpPl8ydOGzCdAQTNUsTdEC7r79Ax9VyGE03SoLdBEBlYnXUmXSz2O5srcCnqq50RJBBP7WsnyQ35QSNshIg4FY5cClIGOljnNHnmtBQciuOPcRxAWkFQxQHhqPlW7bFy6sI1dEh51O1mXsVQR5fhOGtMLwFbmXGAJ5A6XFw1VS91HBpQ4RdPO6jQVbbjl4j4S2IztkiIoPPGHbhI4bSBoYfri9g82eknSCUzHhe804uZdOOqUzvKPREcaPITg0ALXLJEUh8Mc9XPQyPUk8ohu52MsxlRgd2VfQ4QiQMjzbuCDn4iHW7mbdg92VmtINDQCkq0fKH3HAXa0syz5AYSSWTL6hwnkpaRSI8l1bn");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventorySplit v = new InventorySplit();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DocNo = "MmjS46o9XkMqxiOL6";
                v.OldInvId = AddBaseInventory();
                v.NewInvId = AddBaseInventory();
                v.OrigQty = 352266175;
                v.SplitQty = 222108831;
                v.Memo = "ch4WsFGlcfJQScmOaMt9KOhFEQhyxb7eWpPl8ydOGzCdAQTNUsTdEC7r79Ax9VyGE03SoLdBEBlYnXUmXSz2O5srcCnqq50RJBBP7WsnyQ35QSNshIg4FY5cClIGOljnNHnmtBQciuOPcRxAWkFQxQHhqPlW7bFy6sI1dEh51O1mXsVQR5fhOGtMLwFbmXGAJ5A6XFw1VS91HBpQ4RdPO6jQVbbjl4j4S2IztkiIoPPGHbhI4bSBoYfri9g82eknSCUzHhe804uZdOOqUzvKPREcaPITg0ALXLJEUh8Mc9XPQyPUk8ohu52MsxlRgd2VfQ4QiQMjzbuCDn4iHW7mbdg92VmtINDQCkq0fKH3HAXa0syz5AYSSWTL6hwnkpaRSI8l1bn";
                context.Set<InventorySplit>().Add(v);
                context.SaveChanges();
            }

            InventorySplitApiVM vm = _controller.Wtm.CreateVM<InventorySplitApiVM>();
            var oldID = v.ID;
            v = new InventorySplit();
            v.ID = oldID;
       		
            v.DocNo = "PCAZ2v6FvJxt8JNCwDBZBZlkTrOukUXQGYCNfHFqvy";
            v.OrigQty = 505016931;
            v.SplitQty = 762070629;
            v.Memo = "gOcSPBUWlrraTFLo8iEzt5Jw1XJ2FKML5eFpWH2eKRu9oo9pmAQGyywnajxK75dnqsxdcsThxynfMVdxOM5WX46er4zgDuSL9rUEpDEQaguntvdKbA7LSQNZtxgyJYSgApE4ExhpbcbRrub3zbnpOV6XsTOZIrjxOWhJnKvgBWveOxGOZHo47DpH86LNlIF2l0kUliBY6yJOzlcjGelCmv7jkwZ9JdmVCD1B2xwbVVnMEIvNkiOZNl9VUbGjl4Q3Nta1jvA1HdmawcApwru242gsF65XnbPXok8ey9ckeLt4jQw7H3rRa3bTfCRwsssR2x5qgXgDumUcR";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.OldInvId", "");
            vm.FC.Add("Entity.NewInvId", "");
            vm.FC.Add("Entity.OrigQty", "");
            vm.FC.Add("Entity.SplitQty", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventorySplit>().Find(v.ID);
 				
                Assert.AreEqual(data.DocNo, "PCAZ2v6FvJxt8JNCwDBZBZlkTrOukUXQGYCNfHFqvy");
                Assert.AreEqual(data.OrigQty, 505016931);
                Assert.AreEqual(data.SplitQty, 762070629);
                Assert.AreEqual(data.Memo, "gOcSPBUWlrraTFLo8iEzt5Jw1XJ2FKML5eFpWH2eKRu9oo9pmAQGyywnajxK75dnqsxdcsThxynfMVdxOM5WX46er4zgDuSL9rUEpDEQaguntvdKbA7LSQNZtxgyJYSgApE4ExhpbcbRrub3zbnpOV6XsTOZIrjxOWhJnKvgBWveOxGOZHo47DpH86LNlIF2l0kUliBY6yJOzlcjGelCmv7jkwZ9JdmVCD1B2xwbVVnMEIvNkiOZNl9VUbGjl4Q3Nta1jvA1HdmawcApwru242gsF65XnbPXok8ey9ckeLt4jQw7H3rRa3bTfCRwsssR2x5qgXgDumUcR");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventorySplit v = new InventorySplit();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DocNo = "MmjS46o9XkMqxiOL6";
                v.OldInvId = AddBaseInventory();
                v.NewInvId = AddBaseInventory();
                v.OrigQty = 352266175;
                v.SplitQty = 222108831;
                v.Memo = "ch4WsFGlcfJQScmOaMt9KOhFEQhyxb7eWpPl8ydOGzCdAQTNUsTdEC7r79Ax9VyGE03SoLdBEBlYnXUmXSz2O5srcCnqq50RJBBP7WsnyQ35QSNshIg4FY5cClIGOljnNHnmtBQciuOPcRxAWkFQxQHhqPlW7bFy6sI1dEh51O1mXsVQR5fhOGtMLwFbmXGAJ5A6XFw1VS91HBpQ4RdPO6jQVbbjl4j4S2IztkiIoPPGHbhI4bSBoYfri9g82eknSCUzHhe804uZdOOqUzvKPREcaPITg0ALXLJEUh8Mc9XPQyPUk8ohu52MsxlRgd2VfQ4QiQMjzbuCDn4iHW7mbdg92VmtINDQCkq0fKH3HAXa0syz5AYSSWTL6hwnkpaRSI8l1bn";
                context.Set<InventorySplit>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventorySplit v1 = new InventorySplit();
            InventorySplit v2 = new InventorySplit();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DocNo = "MmjS46o9XkMqxiOL6";
                v1.OldInvId = AddBaseInventory();
                v1.NewInvId = AddBaseInventory();
                v1.OrigQty = 352266175;
                v1.SplitQty = 222108831;
                v1.Memo = "ch4WsFGlcfJQScmOaMt9KOhFEQhyxb7eWpPl8ydOGzCdAQTNUsTdEC7r79Ax9VyGE03SoLdBEBlYnXUmXSz2O5srcCnqq50RJBBP7WsnyQ35QSNshIg4FY5cClIGOljnNHnmtBQciuOPcRxAWkFQxQHhqPlW7bFy6sI1dEh51O1mXsVQR5fhOGtMLwFbmXGAJ5A6XFw1VS91HBpQ4RdPO6jQVbbjl4j4S2IztkiIoPPGHbhI4bSBoYfri9g82eknSCUzHhe804uZdOOqUzvKPREcaPITg0ALXLJEUh8Mc9XPQyPUk8ohu52MsxlRgd2VfQ4QiQMjzbuCDn4iHW7mbdg92VmtINDQCkq0fKH3HAXa0syz5AYSSWTL6hwnkpaRSI8l1bn";
                v2.DocNo = "PCAZ2v6FvJxt8JNCwDBZBZlkTrOukUXQGYCNfHFqvy";
                v2.OldInvId = v1.OldInvId; 
                v2.NewInvId = v1.NewInvId; 
                v2.OrigQty = 505016931;
                v2.SplitQty = 762070629;
                v2.Memo = "gOcSPBUWlrraTFLo8iEzt5Jw1XJ2FKML5eFpWH2eKRu9oo9pmAQGyywnajxK75dnqsxdcsThxynfMVdxOM5WX46er4zgDuSL9rUEpDEQaguntvdKbA7LSQNZtxgyJYSgApE4ExhpbcbRrub3zbnpOV6XsTOZIrjxOWhJnKvgBWveOxGOZHo47DpH86LNlIF2l0kUliBY6yJOzlcjGelCmv7jkwZ9JdmVCD1B2xwbVVnMEIvNkiOZNl9VUbGjl4Q3Nta1jvA1HdmawcApwru242gsF65XnbPXok8ey9ckeLt4jQw7H3rRa3bTfCRwsssR2x5qgXgDumUcR";
                context.Set<InventorySplit>().Add(v1);
                context.Set<InventorySplit>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventorySplit>().Find(v1.ID);
                var data2 = context.Set<InventorySplit>().Find(v2.ID);
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
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "jZnuJzYWzTV8Pzi2IXOPGKQZoExA5H8VW5cmrD7d9peh6ShkdjsBeMPigCh8cju9Ci1m72IUnZ6lJEBGOwhm77A66cHpd5c79UTk5bxv6ZIM4IEXIggVo1Mjp7bhsYi4WQCQbQdWy0vLLTbUzQOvXyCBgZI9DBvrY2R92vSIoUe1WAvpT4gUa40UUdEM3IHrXxwcQUXMdOmjxTL7ZXx9BcSS4XlnUeo1DoYmiMjO8LoF5PRD7kMG1Hd1Hn1dap1xGLytGMsbAdHGwabJWEwPYxEa7RozC8KEc4h9iSTAxt6lJtcaE0SoLTWCCsxNzXmI6DeRDY6D8EKFQd2OTJP20ws53zZHJFFiT7BHSDAIKLK67ToV6x4x7avvq3U6N3YiUzJ0aNFve";
                v.Code = "AGDboIitysCpltaSO35khaaki3JKR2Xhwo";
                v.Name = "K2oUVI3";
                v.SourceSystemId = "iNl9PFv6yrKBkVjf8zfdM3B7B9amKuFvVxrtsVc6s9vOibGcI";
                v.LastUpdateTime = DateTime.Parse("2026-03-26 13:54:16");
                context.Set<BaseOrganization>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseAnalysisType()
        {
            BaseAnalysisType v = new BaseAnalysisType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "7v3VF";
                v.Name = "vDmGKKdCB2CxE5QEp94bt0104I09Cw";
                v.SourceSystemId = "IfR6sZH7KN9tt5u8g1oLByMTeYMvuwLbZ2sZ1H3WRu7NQVU6";
                v.LastUpdateTime = DateTime.Parse("2026-02-04 13:54:16");
                context.Set<BaseAnalysisType>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseDepartment()
        {
            BaseDepartment v = new BaseDepartment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.OrganizationId = AddBaseOrganization();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "QP82Vi6HhiBwhIlmMseSahwJWNEHmrsJxxp9MvxMOupv5HaO0pxGz7nt0hMdMJmr9OU1RA1rTZNynZTJg6fwx3e8Prq96uvrOvmJ24hfGhGQwO4pStka2UST2DQnp9huGkjiT8SDwQUwD5aHDHjBjXlidxkIr7kRdpcx67aVsE6TAcfdGD0wi4NTt1KuQfVw5HSDWonW4zD9O8SVOcGGWuv3UsyWLF8hoCJhMXFddi4qu3ci8g2TjbQcfj5rFYHQ44c4dDTVYELcNdmPWszPymYLSVm7bCCly0GxJ1cMWVlsyfRGdDS58OyFiJmYhURUiF4uy5mxQyCxYHtpV0LwwxBLRQBbtvjVC2uQlgIScUHXcewB4wGvui";
                v.Code = "5Rr4Eo";
                v.Name = "1GlxUFcNCASy";
                v.SourceSystemId = "H08ZpWJNAaVa7RhtoApGMhanc";
                v.LastUpdateTime = DateTime.Parse("2025-03-10 13:54:16");
                context.Set<BaseDepartment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseItemCategory()
        {
            BaseItemCategory v = new BaseItemCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.OrganizationId = AddBaseOrganization();
                v.AnalysisTypeId = AddBaseAnalysisType();
                v.DepartmentId = AddBaseDepartment();
                v.Code = "n9pNOO";
                v.Name = "TkbbZ7KZrPA1Rb7ISeuvrXwUH44QYrkTd";
                v.SourceSystemId = "EOHZNBXpFPMKQEzaCJjgyGGTFacfAfP";
                v.LastUpdateTime = DateTime.Parse("2026-05-21 13:54:16");
                context.Set<BaseItemCategory>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseUnit()
        {
            BaseUnit v = new BaseUnit();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "vrAeVYxT5YZi9eX5i1SlFE48ymBOf4KvtfEp3S90B1QKXv25mq7KsyeILRKyORJqO9ZQg9aa2RMilSmUG5uv2Y";
                v.Code = "KJfc";
                v.Name = "sCR75NfmalFRj8JcgbB4HT3oP0elyUTW";
                v.SourceSystemId = "ChF2Efuvbjiulz14m6Utos050GtDLD0IZH";
                v.LastUpdateTime = DateTime.Parse("2026-01-19 13:54:16");
                context.Set<BaseUnit>().Add(v);
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
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "Lef5WqqRAK2XjFkOXDx9Q3b8GEXPn4aEueTsgN5EiLsozLpaCucBrBO3xYgN34NnCmreBV3v1SoEDdBCLucQMI8L9RkGxnLB1XXDg4nXpg0894wmvrBwlnWxPFSQSDEE7FvAVvB5s8LVljaqSUVnmT1uhL8MNOWp6LGx3jWrDKblW5Mvettvy9SrUNK6DndoMNNAWPKYnokDqjuJ4hQoNWV7NqinJRPhtsvG9p2FrPTdeLcl66HxIEKIaxmr2DDxcA2PYxONuerVGWYPSO7s0rjsU3rx2mFAd1xiDaTfQlztGtUJBnsMLEkLZZY5YtbBADUNYzfBsQXcKg7TcP8quG2I1rzkqholyNAetUx57dSyaMTmVgyZCrP0p5YHFUGcR7dPgX0KtmAl7UZrtEtBPWgxWh0L5ThPWCLSc0sgldvbJD";
                v.Code = "xtWjfExWQ";
                v.Name = "1rS18gIs7n5XqbqMFNxe0oUHKFAfZv3A9iBw3GNiiFg";
                v.SourceSystemId = "rBCTnY1rAizFzXLMtLb";
                v.LastUpdateTime = DateTime.Parse("2024-02-27 13:54:16");
                context.Set<BaseWareHouse>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseItemMaster()
        {
            BaseItemMaster v = new BaseItemMaster();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.OrganizationId = AddBaseOrganization();
                v.ItemCategoryId = AddBaseItemCategory();
                v.SPECS = "uOLj91Z8WXPqRvRsClKz6H8dTkrHnweWlD9CM3kxGTYcbsj00DaanOfV529rWDONh9qUkl8IMHpfvR5O6ORxaTgbylN2bZl0yXAwkfApbTyI5fHMIrB4WHawB7UHYX3EOJ3TLQOJLdatuDxoXzTHI4zL4akEzLOR9FakOjUH1TQfyxYfps5WZCnOx7uvWkysinzo1oXRU04tTcVbjFIHNgaUDwqkbEWwkBZDK7IZRZ0VP4gIKbwpIcSarmsdCcT9vCINAvK4MMaeHuAjeeVH4nwVj5wGf6K3fbX9yryhOE3IeYRdb6D3zCXJ";
                v.MateriaModel = "umIfm82ZiMrSDrMu8EvYB7kHFGoJEkynYqXi7tt06tLMY3IIRGZE221SLsZoTBkbJioStzCB9LRJYuURa0THAL6nHVhA7YFqfk8hHhK7rNTUbToZ6VvlmP8D7G1VpH8LAQmtV4tqDjUrsHU0Xak0GKZ09Jj0sb8maQERgHTeTLIRnOY258DUATZhZtX72HkAvzBvOPwpjkh3fG0unbNUrnmalbsrPzV6RROuisJ31UEjDPCjMBr4wwsORGNZwkKekJVwfc2bwj36x5rNLQBZkNtI4CqlP6bXOgOtP80Ua8Vcq7rwS9dDq8rFn4WBUnDDFLAnTGaUGJWfc2";
                v.Description = "c7Y63N5v6ppFIbx";
                v.StockUnitId = AddBaseUnit();
                v.ProductionOrgId = AddBaseOrganization();
                v.ProductionDeptId = AddBaseDepartment();
                v.WhId = AddBaseWareHouse();
                v.FormAttribute = WMS.Model.ItemFormAttributeEnum.PurchasePart;
                v.MateriaAttribute = "W9p4iitP";
                v.GearRatio = 48;
                v.Power = 63;
                v.SafetyStockQty = 27;
                v.FixedLT = 90;
                v.BuildBatch = 65;
                v.NotAnalysisQty = 25;
                v.AnalysisTypeId = AddBaseAnalysisType();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Code = "Q8p33uh2mMLWSwy2j6U8sFb";
                v.Name = "pWV7IxNjNWLdofYfsQOvyWpSUt1qB4KLRjQMYb1PfJ4o";
                v.SourceSystemId = "hVLlzfxB1ejydBSra8bZjx0zL1w";
                v.LastUpdateTime = DateTime.Parse("2024-10-20 13:54:16");
                context.Set<BaseItemMaster>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseWhArea()
        {
            BaseWhArea v = new BaseWhArea();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "miOiUmFP";
                v.Name = "cVmhqY7JzHb";
                v.WareHouseId = AddBaseWareHouse();
                v.AreaType = WMS.Model.WhAreaEnum.Inspected;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "nUoPGt2SvKC8phCOLSWxs1pVfmmjkr0Pz2GX6S276xzSz2LeTywGTq";
                context.Set<BaseWhArea>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseWhLocation()
        {
            BaseWhLocation v = new BaseWhLocation();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "mnpZqQH66ghJeKGfi4j9ojDyfbd3dW";
                v.Name = "CswWpNrGXRLTCTV2OR";
                v.WhAreaId = AddBaseWhArea();
                v.AreaType = WMS.Model.WhLocationEnum.WaitForInspect;
                v.Locked = false;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "ajkb5MFdFVlDfG0XgpFfbzTDrDfZVkoqGF7GVHXBzAj";
                context.Set<BaseWhLocation>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseInventory()
        {
            BaseInventory v = new BaseInventory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ItemMasterId = AddBaseItemMaster();
                v.WhLocationId = AddBaseWhLocation();
                v.BatchNumber = "D";
                v.SerialNumber = "H";
                v.Seiban = "1zXSaJjpy1aioMU3XmYLruqzFODcEBH55s";
                v.Qty = 91;
                v.IsAbandoned = true;
                v.ItemSourceType = WMS.Model.ItemSourceTypeEnum.Buy;
                v.FrozenStatus = WMS.Model.FrozenStatusEnum.Freezed;
                context.Set<BaseInventory>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
