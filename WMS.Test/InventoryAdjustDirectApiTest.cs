using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class InventoryAdjustDirectApiTest
    {
        private InventoryAdjustDirectApiController _controller;
        private string _seed;

        public InventoryAdjustDirectApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryAdjustDirectApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryAdjustDirectApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryAdjustDirectApiVM vm = _controller.Wtm.CreateVM<InventoryAdjustDirectApiVM>();
            InventoryAdjustDirect v = new InventoryAdjustDirect();
            
            v.DocNo = "xlZG6p";
            v.OldInvId = AddBaseInventory();
            v.NewInvId = AddBaseInventory();
            v.DiffQty = 39;
            v.Memo = "U8ccVvSNMXzDdf3ZGPmW2vFaL8lAUQttZi80tlRTie9SSJ9T20jSjruaxa";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryAdjustDirect>().Find(v.ID);
                
                Assert.AreEqual(data.DocNo, "xlZG6p");
                Assert.AreEqual(data.DiffQty, 39);
                Assert.AreEqual(data.Memo, "U8ccVvSNMXzDdf3ZGPmW2vFaL8lAUQttZi80tlRTie9SSJ9T20jSjruaxa");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryAdjustDirect v = new InventoryAdjustDirect();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DocNo = "xlZG6p";
                v.OldInvId = AddBaseInventory();
                v.NewInvId = AddBaseInventory();
                v.DiffQty = 39;
                v.Memo = "U8ccVvSNMXzDdf3ZGPmW2vFaL8lAUQttZi80tlRTie9SSJ9T20jSjruaxa";
                context.Set<InventoryAdjustDirect>().Add(v);
                context.SaveChanges();
            }

            InventoryAdjustDirectApiVM vm = _controller.Wtm.CreateVM<InventoryAdjustDirectApiVM>();
            var oldID = v.ID;
            v = new InventoryAdjustDirect();
            v.ID = oldID;
       		
            v.DocNo = "cm";
            v.DiffQty = 47;
            v.Memo = "t37BlI3KJQXBI1wfsA4LeBDWAlsMCu29LV95nXM68OKGLifBdeRgeXL24QQ8QRVJrlrb5b8ztLsLB2GPxX8h0pUC6Zl2skghbpgPXKSP3d2Ev9CEfUyl96sm9q9TGOCKKHmnJ6FR08OlICrFkkISwZYvvEi9IMCIFz5oSHs8vZy2FhfcgYkeFyNeeE5Hz2H2o1OXAFyr31XEfBzw5cYD0Nz1FCPjFqYAH6j7k0lrKtFFTVhZHbTNagLZ6SDsbGONq6AyguIb9ffOw1A7Na415hsqNoSTiUxfEZ0SIXo6ma3F7YXchDEktXT11yUSIfqgpQPajMZEWMwRZdWQqaUzbCjblI6XBLlbBwBeQs5vXfDEVHuu6wkHmVuxhbdqe4VJzaaQzJQThvmtwDtw4q6te4hhc2PLyfBmgfiUhePAhpyqEKiwjHgUl50h3FWRzdx8wS";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.OldInvId", "");
            vm.FC.Add("Entity.NewInvId", "");
            vm.FC.Add("Entity.DiffQty", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryAdjustDirect>().Find(v.ID);
 				
                Assert.AreEqual(data.DocNo, "cm");
                Assert.AreEqual(data.DiffQty, 47);
                Assert.AreEqual(data.Memo, "t37BlI3KJQXBI1wfsA4LeBDWAlsMCu29LV95nXM68OKGLifBdeRgeXL24QQ8QRVJrlrb5b8ztLsLB2GPxX8h0pUC6Zl2skghbpgPXKSP3d2Ev9CEfUyl96sm9q9TGOCKKHmnJ6FR08OlICrFkkISwZYvvEi9IMCIFz5oSHs8vZy2FhfcgYkeFyNeeE5Hz2H2o1OXAFyr31XEfBzw5cYD0Nz1FCPjFqYAH6j7k0lrKtFFTVhZHbTNagLZ6SDsbGONq6AyguIb9ffOw1A7Na415hsqNoSTiUxfEZ0SIXo6ma3F7YXchDEktXT11yUSIfqgpQPajMZEWMwRZdWQqaUzbCjblI6XBLlbBwBeQs5vXfDEVHuu6wkHmVuxhbdqe4VJzaaQzJQThvmtwDtw4q6te4hhc2PLyfBmgfiUhePAhpyqEKiwjHgUl50h3FWRzdx8wS");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryAdjustDirect v = new InventoryAdjustDirect();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DocNo = "xlZG6p";
                v.OldInvId = AddBaseInventory();
                v.NewInvId = AddBaseInventory();
                v.DiffQty = 39;
                v.Memo = "U8ccVvSNMXzDdf3ZGPmW2vFaL8lAUQttZi80tlRTie9SSJ9T20jSjruaxa";
                context.Set<InventoryAdjustDirect>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryAdjustDirect v1 = new InventoryAdjustDirect();
            InventoryAdjustDirect v2 = new InventoryAdjustDirect();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DocNo = "xlZG6p";
                v1.OldInvId = AddBaseInventory();
                v1.NewInvId = AddBaseInventory();
                v1.DiffQty = 39;
                v1.Memo = "U8ccVvSNMXzDdf3ZGPmW2vFaL8lAUQttZi80tlRTie9SSJ9T20jSjruaxa";
                v2.DocNo = "cm";
                v2.OldInvId = v1.OldInvId; 
                v2.NewInvId = v1.NewInvId; 
                v2.DiffQty = 47;
                v2.Memo = "t37BlI3KJQXBI1wfsA4LeBDWAlsMCu29LV95nXM68OKGLifBdeRgeXL24QQ8QRVJrlrb5b8ztLsLB2GPxX8h0pUC6Zl2skghbpgPXKSP3d2Ev9CEfUyl96sm9q9TGOCKKHmnJ6FR08OlICrFkkISwZYvvEi9IMCIFz5oSHs8vZy2FhfcgYkeFyNeeE5Hz2H2o1OXAFyr31XEfBzw5cYD0Nz1FCPjFqYAH6j7k0lrKtFFTVhZHbTNagLZ6SDsbGONq6AyguIb9ffOw1A7Na415hsqNoSTiUxfEZ0SIXo6ma3F7YXchDEktXT11yUSIfqgpQPajMZEWMwRZdWQqaUzbCjblI6XBLlbBwBeQs5vXfDEVHuu6wkHmVuxhbdqe4VJzaaQzJQThvmtwDtw4q6te4hhc2PLyfBmgfiUhePAhpyqEKiwjHgUl50h3FWRzdx8wS";
                context.Set<InventoryAdjustDirect>().Add(v1);
                context.Set<InventoryAdjustDirect>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryAdjustDirect>().Find(v1.ID);
                var data2 = context.Set<InventoryAdjustDirect>().Find(v2.ID);
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
                v.Memo = "2xNtZ6MKuvFVtprHZ9NFRy18vXzVfA70tnD1PI6kYQFRJi7iG9sXKsMOv0jgCfuSFOgz9rtmjEqmGOkbDdZ0SoFcb5yw22TmwrE5Xwqt3ttuQ1DDdEBLmE0JgvZ6Yn11WSphICLGUbchL8XbIsakBamZwMlqEunLmwknLo6vlTOTn29OTemlmjNspWk2Hb0s0lDBp30iBuCDl2DUnHgeCWzCpO5XCmqkKewynZzfKufdLMJ0bSV83jxkFae3wfccBJ1ASxC0zqKtWFeo7OTbNd6qa7d7sPdIBGQZ14sZA3bWQSMrhlBbeT7hPWWAIC1njSXHayCGVEKJ2m6WxAbc7z71N3nrwPaJx";
                v.Code = "JQxI";
                v.Name = "w5iIbOn6olCNIud123gMHv4rusv014piJ9gUwet";
                v.SourceSystemId = "AW9NjSZbI1bzf0BpipJyowpkvTG6swaTSlU";
                v.LastUpdateTime = DateTime.Parse("2025-11-05 13:54:43");
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

                v.Code = "HJ2hVdptme1NnbnY3VGZ2KXdKiROZ";
                v.Name = "3rTQV1QW";
                v.SourceSystemId = "3U";
                v.LastUpdateTime = DateTime.Parse("2026-06-20 13:54:43");
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
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "uY2amSKsZrqXa09OVvwsBKBino5azWhZwFCqWoKJ0zfmEqtOmlfz3rpJKdBcCJkWdpAhGjwecXbOyWLi6s2k3HQv6ABWIpX1Ntk4svW3CuoLiBiW5HVZLwOJnYGlA041m1aenwnqWgsxm3ty5TZTVW93cnOwt0mBqYvq0DPRW3Msu8kJ5PTBoFQ3sKcricmddHF4oL6vqIMjqfx88ZUGFAtC72ZZGZUit9xtvs7b4kECU0LQbT5psYExA1x624JhqZyQJOcDIzsMs8XYX94xEjdbMJDH1DbTjZI32s65mOLmmHGe0rgHpuKucmF5h1PVOvO0Ntgh4m4UVgtTfCZlxZBwQPtfD95TfZYpfNLchViX61XWMsHTTgRuWguNVRy7VpAgQzbMCOmAF8E1cssOQYesiGi";
                v.Code = "u0FapVJtjl7euaKXBeEVnFbUPmlgoaM3O";
                v.Name = "zbuQIXx30w198hU871Vt9";
                v.SourceSystemId = "sPOtbX1rhBJaRmjG3dVpM6ZY1ZkoT";
                v.LastUpdateTime = DateTime.Parse("2024-06-19 13:54:43");
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
                v.Code = "fAwN7J7X67Be5AJE7lWJmURu2lvLJguXnnRnUW";
                v.Name = "ckJXHrMfSAx24yNoPPfGd";
                v.SourceSystemId = "KRqcZ9yGVEuuHyuCXb4CBW9yI7Pflm43AdAlOSHFbPjZuHWp";
                v.LastUpdateTime = DateTime.Parse("2024-06-05 13:54:43");
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

                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "PE7TjdWhV3s7QIveUBPZB9rAhrrqyBs4SqxeAEy4ia08heoat2vSMkNC8NsdICVk8gKWqC3CF3Mc47jvT7duGpV2hnuTo3E75WNfxsuiElo7BQZ8m3dBKnJW2huBFjHLY6141KM9ThNul9kd05bE22TvDhz0SAmx1UjmXfmEQPg3v4no5X18z3CtQ9coqsegehlJvg";
                v.Code = "0vgMvZjz2SbJ8WggZuJtgMo2p60QoDU7nQIkFKyX";
                v.Name = "TTZtpztWGD8D4QCppDrlip3AR0oLQ1PYjsmrw2OPzU0IAaxg";
                v.SourceSystemId = "oEGXR4YgsralT8hAuv6XDggpnP8VusC8KynOLeevO9NELB";
                v.LastUpdateTime = DateTime.Parse("2026-05-27 13:54:43");
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
                v.IsStacking = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "rH0u9IY8Z3kOGfZ9ZNoNV9PT8IkwKN0ZMDfwNJlryRFYbq7hj8kS4D3GILaOi96ge6yoZQvG1p0MXROCBHCGRGY";
                v.Code = "VE2oDCb3lXTRcfJofvTG2lz1TqNfkd";
                v.Name = "dwapggxgwPf6APDw";
                v.SourceSystemId = "7pt8xxR8wGVbZs1ASfKIK4ZKUVmvAsm7S";
                v.LastUpdateTime = DateTime.Parse("2026-08-06 13:54:43");
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
                v.SPECS = "LrBrOcwPixwJVCwFAvA05TZebXrjLYjZCMI3D61mViGxuI9c48rg";
                v.MateriaModel = "MhUW2vlIo5AfjIoAnKXdi2Bw4IgSSps0xZ0khT4JWBczFlbvQLHf76fOOB7yd7ueA49pY8jgekl5yvOEqyF3qeR8duCg4iUekzdILbrgQXt3yaBwDrtU9YHMeSmpiFfYg1RJuAyohcWoXW6fVl4yyxF2XQBydAEqgVwTWStcwMoMLQT7d6t1o0SqR7Kb6fakt9Sabcjxdrzk6dcLV301OquM1RcTjQrzlbJdaQHjZLBJBfa8yJ6YF4J3zOM8cSVolmGvGkZ7W1pgi8gvun2I9RP7UPRHGnHxqtARjAk77OtL5kMveCbqMaoc0pN2N9feNt5FpTX0cQk69xHNlxLYFiMUcsRg6Lz7nRpN9dNhJW7c4oDPlaVoNsVoG9SAfHNYoM4TqFJiqm4j72BPF4d26X7T";
                v.Description = "tCJidi1uL7mHGbu";
                v.StockUnitId = AddBaseUnit();
                v.ProductionOrgId = AddBaseOrganization();
                v.ProductionDeptId = AddBaseDepartment();
                v.WhId = AddBaseWareHouse();
                v.FormAttribute = WMS.Model.ItemFormAttributeEnum.SubcontractPart;
                v.MateriaAttribute = "F";
                v.GearRatio = 87;
                v.Power = 10;
                v.SafetyStockQty = 79;
                v.FixedLT = 58;
                v.BuildBatch = 42;
                v.NotAnalysisQty = 96;
                v.AnalysisTypeId = AddBaseAnalysisType();
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Code = "tSPd43";
                v.Name = "S3GLz1WdNZOe3906i4Jkf3Vxrb8OxUGOTjOX1Zllk5";
                v.SourceSystemId = "WWpv7xFMx7mukwXGJnk97ucHs7K7L5aWbFFSo2Bon";
                v.LastUpdateTime = DateTime.Parse("2025-06-20 13:54:43");
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

                v.Code = "D5U1tDPQI439";
                v.Name = "O";
                v.WareHouseId = AddBaseWareHouse();
                v.AreaType = WMS.Model.WhAreaEnum.UnQualified;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "EYDMm3OWctGrOX9ISB0ZXsIxJO2yzhP16nwTpKZ0jkZhIi3ibRjKs2h6hNsSrM8SinncWwtb8EazYVhqjsarHA4HnuKO2PvdgBHJHPAUlCGmDE5ky5TE6KGDkpZiwt6Qdx4rMXOmxwUtqJCJHctfsUO8ghHAaVEQZRXBqj1wGhIDZMVvmJdPvqMvPN0sCQhxMMyCKrsmsPJmRfNI2R78l6oaqu7Z7zAImm9fRCNmUJG5ZfgZJEGUdf9F8K0pPQEphYYrmbKMrCOkR9qsFDb1BBBImb4QCNfkEl4kAbKv7Om0o17BARLmv1dJAsxLXE9J8DdC0vDXpPiAsG96RA5wDYQyPt58OL0exXBcOYcgTSVhRPFE05CgDauze7mp2w0uWlQHFvce24j7ltZkmgHHNm5S6ktXdlgyT7";
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

                v.Code = "ie3xv9v3nXyljOeqRB6G6QAAyItoVZ70mZrleBFJA";
                v.Name = "4vLMYBne9iqi05FJHkDELsmdRiioKf";
                v.WhAreaId = AddBaseWhArea();
                v.AreaType = WMS.Model.WhLocationEnum.Normal;
                v.Locked = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "5GpAWWkXZ";
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
                v.BatchNumber = "z1g8tnUHLHzgV9VqBHb2Kmtki61jMBLHRVOL7";
                v.SerialNumber = "ohsk";
                v.Seiban = "rxwkBIOKfIOyHmGAOZ";
                v.Qty = 6;
                v.IsAbandoned = false;
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
