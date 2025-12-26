using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class InventoryOtherShipApiTest
    {
        private InventoryOtherShipApiController _controller;
        private string _seed;

        public InventoryOtherShipApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryOtherShipApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryOtherShipApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryOtherShipApiVM vm = _controller.Wtm.CreateVM<InventoryOtherShipApiVM>();
            InventoryOtherShip v = new InventoryOtherShip();
            
            v.ErpID = "G1nXvWeYoyH2kqobrgTw5u1Id1jwQP";
            v.BusinessDate = DateTime.Parse("2026-03-30 10:01:16");
            v.DocNo = "xO5Nwas9p1dS5DuRCV1c1EYivsQDLUTxdQhfywu";
            v.DocTypeId = AddInventoryOtherShipDocType();
            v.BenefitOrganizationId = AddBaseOrganization();
            v.BenefitDepartmentId = AddBaseDepartment();
            v.BenefitPersonId = AddBaseOperator();
            v.WhId = AddBaseWareHouse();
            v.OwnerOrganizationId = AddBaseOrganization();
            v.Memo = "ucsLNno23RAvoLMigKBcUsYMXCD0LK2R83IfLyrXfY5j5YdPlMxJrI";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryOtherShip>().Find(v.ID);
                
                Assert.AreEqual(data.ErpID, "G1nXvWeYoyH2kqobrgTw5u1Id1jwQP");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2026-03-30 10:01:16"));
                Assert.AreEqual(data.DocNo, "xO5Nwas9p1dS5DuRCV1c1EYivsQDLUTxdQhfywu");
                Assert.AreEqual(data.Memo, "ucsLNno23RAvoLMigKBcUsYMXCD0LK2R83IfLyrXfY5j5YdPlMxJrI");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryOtherShip v = new InventoryOtherShip();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ErpID = "G1nXvWeYoyH2kqobrgTw5u1Id1jwQP";
                v.BusinessDate = DateTime.Parse("2026-03-30 10:01:16");
                v.DocNo = "xO5Nwas9p1dS5DuRCV1c1EYivsQDLUTxdQhfywu";
                v.DocTypeId = AddInventoryOtherShipDocType();
                v.BenefitOrganizationId = AddBaseOrganization();
                v.BenefitDepartmentId = AddBaseDepartment();
                v.BenefitPersonId = AddBaseOperator();
                v.WhId = AddBaseWareHouse();
                v.OwnerOrganizationId = AddBaseOrganization();
                v.Memo = "ucsLNno23RAvoLMigKBcUsYMXCD0LK2R83IfLyrXfY5j5YdPlMxJrI";
                context.Set<InventoryOtherShip>().Add(v);
                context.SaveChanges();
            }

            InventoryOtherShipApiVM vm = _controller.Wtm.CreateVM<InventoryOtherShipApiVM>();
            var oldID = v.ID;
            v = new InventoryOtherShip();
            v.ID = oldID;
       		
            v.ErpID = "KxR6zGVdf7tB54svmJ9";
            v.BusinessDate = DateTime.Parse("2024-04-18 10:01:16");
            v.DocNo = "OmW2a37s33SPGgIzdDlG9I4XiYwKR15YiCTQX0XKs";
            v.Memo = "Ikz1FTwdg1NAs9oQCgo6LI7pfaA2v9PzwFMTb4n5GcJSbfWcnExqXRWLpMM2GWhuxGFcV3xVPad7kByLr73DmYmEjR3KixMjLvEMbcvbkrgSm2EyTj150Opx28AHQW83hdfY2QrnRpmM7TZTyUmJgvlnav1zXi6fCJ5KsDjtr5hxwQsWG2JRId3ycK2vz3aXEcgUsIHJBywLKtj2GNpch5lL831rU9tYspucf37keajne8kaLvU6tf4MxKJam2SBxVoPPZaPEBdgYhqlpT62Y209Ky7hXtjp8Am8LtxRJWudbs5EdY0AunmqkLwxTmcTZ3rhckvNwv1QlCA2DcjIfo";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ErpID", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.DocTypeId", "");
            vm.FC.Add("Entity.BenefitOrganizationId", "");
            vm.FC.Add("Entity.BenefitDepartmentId", "");
            vm.FC.Add("Entity.BenefitPersonId", "");
            vm.FC.Add("Entity.WhId", "");
            vm.FC.Add("Entity.OwnerOrganizationId", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryOtherShip>().Find(v.ID);
 				
                Assert.AreEqual(data.ErpID, "KxR6zGVdf7tB54svmJ9");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-04-18 10:01:16"));
                Assert.AreEqual(data.DocNo, "OmW2a37s33SPGgIzdDlG9I4XiYwKR15YiCTQX0XKs");
                Assert.AreEqual(data.Memo, "Ikz1FTwdg1NAs9oQCgo6LI7pfaA2v9PzwFMTb4n5GcJSbfWcnExqXRWLpMM2GWhuxGFcV3xVPad7kByLr73DmYmEjR3KixMjLvEMbcvbkrgSm2EyTj150Opx28AHQW83hdfY2QrnRpmM7TZTyUmJgvlnav1zXi6fCJ5KsDjtr5hxwQsWG2JRId3ycK2vz3aXEcgUsIHJBywLKtj2GNpch5lL831rU9tYspucf37keajne8kaLvU6tf4MxKJam2SBxVoPPZaPEBdgYhqlpT62Y209Ky7hXtjp8Am8LtxRJWudbs5EdY0AunmqkLwxTmcTZ3rhckvNwv1QlCA2DcjIfo");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryOtherShip v = new InventoryOtherShip();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ErpID = "G1nXvWeYoyH2kqobrgTw5u1Id1jwQP";
                v.BusinessDate = DateTime.Parse("2026-03-30 10:01:16");
                v.DocNo = "xO5Nwas9p1dS5DuRCV1c1EYivsQDLUTxdQhfywu";
                v.DocTypeId = AddInventoryOtherShipDocType();
                v.BenefitOrganizationId = AddBaseOrganization();
                v.BenefitDepartmentId = AddBaseDepartment();
                v.BenefitPersonId = AddBaseOperator();
                v.WhId = AddBaseWareHouse();
                v.OwnerOrganizationId = AddBaseOrganization();
                v.Memo = "ucsLNno23RAvoLMigKBcUsYMXCD0LK2R83IfLyrXfY5j5YdPlMxJrI";
                context.Set<InventoryOtherShip>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryOtherShip v1 = new InventoryOtherShip();
            InventoryOtherShip v2 = new InventoryOtherShip();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ErpID = "G1nXvWeYoyH2kqobrgTw5u1Id1jwQP";
                v1.BusinessDate = DateTime.Parse("2026-03-30 10:01:16");
                v1.DocNo = "xO5Nwas9p1dS5DuRCV1c1EYivsQDLUTxdQhfywu";
                v1.DocTypeId = AddInventoryOtherShipDocType();
                v1.BenefitOrganizationId = AddBaseOrganization();
                v1.BenefitDepartmentId = AddBaseDepartment();
                v1.BenefitPersonId = AddBaseOperator();
                v1.WhId = AddBaseWareHouse();
                v1.OwnerOrganizationId = AddBaseOrganization();
                v1.Memo = "ucsLNno23RAvoLMigKBcUsYMXCD0LK2R83IfLyrXfY5j5YdPlMxJrI";
                v2.ErpID = "KxR6zGVdf7tB54svmJ9";
                v2.BusinessDate = DateTime.Parse("2024-04-18 10:01:16");
                v2.DocNo = "OmW2a37s33SPGgIzdDlG9I4XiYwKR15YiCTQX0XKs";
                v2.DocTypeId = v1.DocTypeId; 
                v2.BenefitOrganizationId = v1.BenefitOrganizationId; 
                v2.BenefitDepartmentId = v1.BenefitDepartmentId; 
                v2.BenefitPersonId = v1.BenefitPersonId; 
                v2.WhId = v1.WhId; 
                v2.OwnerOrganizationId = v1.OwnerOrganizationId; 
                v2.Memo = "Ikz1FTwdg1NAs9oQCgo6LI7pfaA2v9PzwFMTb4n5GcJSbfWcnExqXRWLpMM2GWhuxGFcV3xVPad7kByLr73DmYmEjR3KixMjLvEMbcvbkrgSm2EyTj150Opx28AHQW83hdfY2QrnRpmM7TZTyUmJgvlnav1zXi6fCJ5KsDjtr5hxwQsWG2JRId3ycK2vz3aXEcgUsIHJBywLKtj2GNpch5lL831rU9tYspucf37keajne8kaLvU6tf4MxKJam2SBxVoPPZaPEBdgYhqlpT62Y209Ky7hXtjp8Am8LtxRJWudbs5EdY0AunmqkLwxTmcTZ3rhckvNwv1QlCA2DcjIfo";
                context.Set<InventoryOtherShip>().Add(v1);
                context.Set<InventoryOtherShip>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryOtherShip>().Find(v1.ID);
                var data2 = context.Set<InventoryOtherShip>().Find(v2.ID);
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
                v.Memo = "daWS6lFYkVQyiH7pQKY3AgX0cEQmT22kUF3vthTgieEK0vmBKZ0QhCiiNlhaK4RqH3Kwb7rQKrMSkTKetEUIzH9xAouEfqlEKlFEki3Glx3rW5OaeKs2M5Kg0G1I8rmcsWrlqAFclvEjltDUlZCOC4zX6HN3E5yvKnJnpZ0gkROTryr90ovIrwZqJNkLSsCiDDpfLRMsSnsFuGixXUklm8IYqJ0RJgVh2I3lPSMWSHeTOG7ThkcsfuIkDuyw0IpScvO0D0DHv6NVXlpTAwUQkl3OWpZkP92Mr2dgoceOUSJzfc4Hsg9WWDrRRDxEVLGwHTliMCSUihQvrq85qOEPYXPQ6MOysg5NirC3MrpOW1mj5O2LkIiMmW";
                v.Code = "3XxpjwO5td4N2BPxK15Y1TN7cRSVmiYwXqEpa7Ea0R";
                v.Name = "7SFIbhFhwSVLexSb5ARzLNUleFWdqfbexRrSrGvrxfD5Yw";
                v.SourceSystemId = "CZzBZPI9hr3rnEgFjuKnAICQK5CHDsgixgcw2sPUgMVeWq9a";
                v.LastUpdateTime = DateTime.Parse("2024-10-20 10:01:16");
                context.Set<BaseOrganization>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddInventoryOtherShipDocType()
        {
            InventoryOtherShipDocType v = new InventoryOtherShipDocType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.CreatePerson = "AANfqLFLdX9jjWO94ZejRqWYA8WB4aotiFIn";
                v.OrganizationId = AddBaseOrganization();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "Lf7kHr6PRI9O7ZFeg1BYgSwUysu2H4IySwWDBYPO6phXKx9drC83b2";
                v.Code = "rTQ2f8KYphKda78YmJMFUZQPPArjoZl4uCc5cCDg8zQHjv";
                v.Name = "RihrRzo8NeqKbhumWHSpFXSjmsTVZnXvB";
                v.SourceSystemId = "EmdYVbQb2eAGcAqhQwAEJC8Z1nK9Zcrq51QQ0Th1y";
                v.LastUpdateTime = DateTime.Parse("2024-09-08 10:01:16");
                context.Set<InventoryOtherShipDocType>().Add(v);
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
                v.IsMFG = false;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "ALVC5SJ3cOXBwDsHTckX8KaYFYEPnX8nrSGetgUYN80fOYuyvmsik416O0ShPyEVhizLKatmrWvQ4qLAsCLvMncmvzHDMKZFWgD4V66cG2lEi6rocZkiE2GFQPfAh8P1gskKQqfyukOoV1IaC1vyw7phNqC1fHGpg0KA5I97D60NyBXWN7L1xmbKrI9dNpT5Dk2FC5gl9SCSXMiT3xFrL6kZE30Sj6BbOwhp9ua9nio7o5dUBnGEkvdsRu728qWSRBStPPdm7tK5HM9";
                v.Code = "oXe4L";
                v.Name = "hkIlhGDhW9rNP0B";
                v.SourceSystemId = "YlBzEBXP6iccLvFaHTTwsIHsvPQWeuSte";
                v.LastUpdateTime = DateTime.Parse("2025-11-28 10:01:16");
                context.Set<BaseDepartment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseOperator()
        {
            BaseOperator v = new BaseOperator();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.JobID = "oHjuAmCDDncHArvQXlEJSCUogez5GeJW5Fmbmja";
                v.OAAccount = "OLf2mLEvkrqX9nr14p8JgDu9zEhUB01xz0YllPcjz";
                v.IDCard = "xdtb13tvyMb8TTPHyYplo48";
                v.TempAuthCode = "lN9uPaI01jBybNkL1GmGzHxJaZGznzpGuTkmSKB32QbV1Dk";
                v.TACExpired = DateTime.Parse("2024-03-25 10:01:16");
                v.DepartmentId = AddBaseDepartment();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Phone = "V";
                v.Memo = "VEpbmmALMU1Gd5n6qfQ9U31P8UCh8xcqPGPfIEQHF1QV2j3ly3SLRsKHcotqJsB7QjJDxUOOjs1SMD1Qzs52ZkamJiDXepgf0mTryZKWzq57YQFR3oWVQSfhiaRdlBhVdylhDSfosOuuob3VR7PSXU96ZJrwjcBWZvjxQo7Z3fm7fJYEWLik970d13IGN9OsHHwTgd48MQa4EqmWIJsZekDZcHClGvocSrpJJo6zYGTggSQqY2dHc2QN2Y4VIuYvW0hPxZZ1WLkos6Lsr1ITIk60DJ181yepWkbXOM5E9ne4eWSm11QpdmUB8aPe7LvdDUSv4tafMaQIb0w1qPUDhVdbpi9fgV1CrvrUwjQf04xtFoPlA8yqXLWlXH3azolOt7iGqeLEucev6aF3ieHxeG0RlAqTNViU53D17Pv7zeee2RP3ESGDPnS6Za2VF5VrSJxubPyitwrgoU1ADNNoGq";
                v.Code = "iXSIuGw7lrxFf8Pr6Kg";
                v.Name = "nzEFPm0PtJs8iFpmhM4YVweVW4yJqPGZ1";
                v.SourceSystemId = "bptE7irXydRHmC25bxa0BQUNIZLWHSoeijrSSgMvgi2yO84";
                v.LastUpdateTime = DateTime.Parse("2025-02-16 10:01:16");
                context.Set<BaseOperator>().Add(v);
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
                v.IsProduct = null;
                v.ShipType = WMS.Model.WhShipTypeEnum.WaitToShip;
                v.IsStacking = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "YnfAiGP9QOmJ9qo0YEMGTCUfawrDd68Ckf78087ysOhsDUUYtufrbF0LvOehd2SrpLZUQsOMivHezz";
                v.Code = "IBDGZW62gD6e7mVER";
                v.Name = "eB3g3qImrt";
                v.SourceSystemId = "NSYgk3v4C289kPSQfG";
                v.LastUpdateTime = DateTime.Parse("2026-03-24 10:01:16");
                context.Set<BaseWareHouse>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
