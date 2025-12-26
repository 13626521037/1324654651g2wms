using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.BaseData.BaseSysParaVMs;
using WMS.Model.BaseData;
using WMS.DataAccess;


namespace WMS.Test
{
    [TestClass]
    public class BaseSysParaControllerTest
    {
        private BaseSysParaController _controller;
        private string _seed;

        public BaseSysParaControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<BaseSysParaController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as BaseSysParaListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(BaseSysParaVM));

            BaseSysParaVM vm = rv.Model as BaseSysParaVM;
            BaseSysPara v = new BaseSysPara();
			
            v.Code = "quEoRskNXqNvqdMI9aW088oqMCArNs8CJUsb4P8f";
            v.Name = "gpyK92fnJ5vbQj1F49u3NwIoBEedyk3T81diWH";
            v.Value = "YPwq4RiNknWjKO8xLCH0au9yvTVOauICmRrhoMSmNTxbCkhJElQlgCPqHeNDib8Lyn0j9Co8ql4j3XHYGLApzwT0zsWtCCOsAP2QGmlH6URbMvjFVobSA4KFTdJnUyYTNgD6pSjBDdIitYcuE94bjcbPFYDHo93NrVaO0PpbpC8V7gv61H3tgyeyZcyUqgFa7iMCZQzPSPB62oazNL7q8W4QZIlYOVvt2BygfkRXGuIyH1TOSOVZ1gY7dJJAWNtBWq17kfjvNCk336wjzs5BNaCFVCPT7EhwClmYXRrGDrjE7VUvY4oPmkDF5z8ZSXatvGvdsKhK0oJhOIk3FpAEgk";
            v.Memo = "iE0ieqY5s7mFuHp9MtLHgbyrvW8VapN0Ez2pK1NtOtFh2f1xWOlVIsAMnYfPYopivwarSHga9BJMyKRYnDwrF2vKPRAK0IUNODzMlscsmx9VoCQft1go8HJ7NU54v6Ew7AAyKGGnCnmuxLQphFP0xDtTD3WEnaK6D5L09hTmpxgD2E4ktMPZmKT3TYnq6QdQGobYZ76xYgxb4TJaB2Y64VcEYoYXfgq5salobVgLhsgTVKdx2k0mWI1Hws";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BaseSysPara>().Find(v.ID);
				
                Assert.AreEqual(data.Code, "quEoRskNXqNvqdMI9aW088oqMCArNs8CJUsb4P8f");
                Assert.AreEqual(data.Name, "gpyK92fnJ5vbQj1F49u3NwIoBEedyk3T81diWH");
                Assert.AreEqual(data.Value, "YPwq4RiNknWjKO8xLCH0au9yvTVOauICmRrhoMSmNTxbCkhJElQlgCPqHeNDib8Lyn0j9Co8ql4j3XHYGLApzwT0zsWtCCOsAP2QGmlH6URbMvjFVobSA4KFTdJnUyYTNgD6pSjBDdIitYcuE94bjcbPFYDHo93NrVaO0PpbpC8V7gv61H3tgyeyZcyUqgFa7iMCZQzPSPB62oazNL7q8W4QZIlYOVvt2BygfkRXGuIyH1TOSOVZ1gY7dJJAWNtBWq17kfjvNCk336wjzs5BNaCFVCPT7EhwClmYXRrGDrjE7VUvY4oPmkDF5z8ZSXatvGvdsKhK0oJhOIk3FpAEgk");
                Assert.AreEqual(data.Memo, "iE0ieqY5s7mFuHp9MtLHgbyrvW8VapN0Ez2pK1NtOtFh2f1xWOlVIsAMnYfPYopivwarSHga9BJMyKRYnDwrF2vKPRAK0IUNODzMlscsmx9VoCQft1go8HJ7NU54v6Ew7AAyKGGnCnmuxLQphFP0xDtTD3WEnaK6D5L09hTmpxgD2E4ktMPZmKT3TYnq6QdQGobYZ76xYgxb4TJaB2Y64VcEYoYXfgq5salobVgLhsgTVKdx2k0mWI1Hws");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            BaseSysPara v = new BaseSysPara();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Code = "quEoRskNXqNvqdMI9aW088oqMCArNs8CJUsb4P8f";
                v.Name = "gpyK92fnJ5vbQj1F49u3NwIoBEedyk3T81diWH";
                v.Value = "YPwq4RiNknWjKO8xLCH0au9yvTVOauICmRrhoMSmNTxbCkhJElQlgCPqHeNDib8Lyn0j9Co8ql4j3XHYGLApzwT0zsWtCCOsAP2QGmlH6URbMvjFVobSA4KFTdJnUyYTNgD6pSjBDdIitYcuE94bjcbPFYDHo93NrVaO0PpbpC8V7gv61H3tgyeyZcyUqgFa7iMCZQzPSPB62oazNL7q8W4QZIlYOVvt2BygfkRXGuIyH1TOSOVZ1gY7dJJAWNtBWq17kfjvNCk336wjzs5BNaCFVCPT7EhwClmYXRrGDrjE7VUvY4oPmkDF5z8ZSXatvGvdsKhK0oJhOIk3FpAEgk";
                v.Memo = "iE0ieqY5s7mFuHp9MtLHgbyrvW8VapN0Ez2pK1NtOtFh2f1xWOlVIsAMnYfPYopivwarSHga9BJMyKRYnDwrF2vKPRAK0IUNODzMlscsmx9VoCQft1go8HJ7NU54v6Ew7AAyKGGnCnmuxLQphFP0xDtTD3WEnaK6D5L09hTmpxgD2E4ktMPZmKT3TYnq6QdQGobYZ76xYgxb4TJaB2Y64VcEYoYXfgq5salobVgLhsgTVKdx2k0mWI1Hws";
                context.Set<BaseSysPara>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(BaseSysParaVM));

            BaseSysParaVM vm = rv.Model as BaseSysParaVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new BaseSysPara();
            v.ID = vm.Entity.ID;
       		
            v.Code = "PwZpsXPpjrkFoh2cVTwZcarB";
            v.Name = "FGqgknmgCyma";
            v.Value = "DBiStcqhDFUOdriADIX5FjXOBVnr";
            v.Memo = "AI40XBdXYzAdnu3iL1SE6Us7sk60gILBBvuI5DKU49";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Value", "");
            vm.FC.Add("Entity.Memo", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BaseSysPara>().Find(v.ID);
 				
                Assert.AreEqual(data.Code, "PwZpsXPpjrkFoh2cVTwZcarB");
                Assert.AreEqual(data.Name, "FGqgknmgCyma");
                Assert.AreEqual(data.Value, "DBiStcqhDFUOdriADIX5FjXOBVnr");
                Assert.AreEqual(data.Memo, "AI40XBdXYzAdnu3iL1SE6Us7sk60gILBBvuI5DKU49");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            BaseSysPara v = new BaseSysPara();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Code = "quEoRskNXqNvqdMI9aW088oqMCArNs8CJUsb4P8f";
                v.Name = "gpyK92fnJ5vbQj1F49u3NwIoBEedyk3T81diWH";
                v.Value = "YPwq4RiNknWjKO8xLCH0au9yvTVOauICmRrhoMSmNTxbCkhJElQlgCPqHeNDib8Lyn0j9Co8ql4j3XHYGLApzwT0zsWtCCOsAP2QGmlH6URbMvjFVobSA4KFTdJnUyYTNgD6pSjBDdIitYcuE94bjcbPFYDHo93NrVaO0PpbpC8V7gv61H3tgyeyZcyUqgFa7iMCZQzPSPB62oazNL7q8W4QZIlYOVvt2BygfkRXGuIyH1TOSOVZ1gY7dJJAWNtBWq17kfjvNCk336wjzs5BNaCFVCPT7EhwClmYXRrGDrjE7VUvY4oPmkDF5z8ZSXatvGvdsKhK0oJhOIk3FpAEgk";
                v.Memo = "iE0ieqY5s7mFuHp9MtLHgbyrvW8VapN0Ez2pK1NtOtFh2f1xWOlVIsAMnYfPYopivwarSHga9BJMyKRYnDwrF2vKPRAK0IUNODzMlscsmx9VoCQft1go8HJ7NU54v6Ew7AAyKGGnCnmuxLQphFP0xDtTD3WEnaK6D5L09hTmpxgD2E4ktMPZmKT3TYnq6QdQGobYZ76xYgxb4TJaB2Y64VcEYoYXfgq5salobVgLhsgTVKdx2k0mWI1Hws";
                context.Set<BaseSysPara>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(BaseSysParaVM));

            BaseSysParaVM vm = rv.Model as BaseSysParaVM;
            v = new BaseSysPara();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BaseSysPara>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            BaseSysPara v = new BaseSysPara();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Code = "quEoRskNXqNvqdMI9aW088oqMCArNs8CJUsb4P8f";
                v.Name = "gpyK92fnJ5vbQj1F49u3NwIoBEedyk3T81diWH";
                v.Value = "YPwq4RiNknWjKO8xLCH0au9yvTVOauICmRrhoMSmNTxbCkhJElQlgCPqHeNDib8Lyn0j9Co8ql4j3XHYGLApzwT0zsWtCCOsAP2QGmlH6URbMvjFVobSA4KFTdJnUyYTNgD6pSjBDdIitYcuE94bjcbPFYDHo93NrVaO0PpbpC8V7gv61H3tgyeyZcyUqgFa7iMCZQzPSPB62oazNL7q8W4QZIlYOVvt2BygfkRXGuIyH1TOSOVZ1gY7dJJAWNtBWq17kfjvNCk336wjzs5BNaCFVCPT7EhwClmYXRrGDrjE7VUvY4oPmkDF5z8ZSXatvGvdsKhK0oJhOIk3FpAEgk";
                v.Memo = "iE0ieqY5s7mFuHp9MtLHgbyrvW8VapN0Ez2pK1NtOtFh2f1xWOlVIsAMnYfPYopivwarSHga9BJMyKRYnDwrF2vKPRAK0IUNODzMlscsmx9VoCQft1go8HJ7NU54v6Ew7AAyKGGnCnmuxLQphFP0xDtTD3WEnaK6D5L09hTmpxgD2E4ktMPZmKT3TYnq6QdQGobYZ76xYgxb4TJaB2Y64VcEYoYXfgq5salobVgLhsgTVKdx2k0mWI1Hws";
                context.Set<BaseSysPara>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            BaseSysPara v1 = new BaseSysPara();
            BaseSysPara v2 = new BaseSysPara();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Code = "quEoRskNXqNvqdMI9aW088oqMCArNs8CJUsb4P8f";
                v1.Name = "gpyK92fnJ5vbQj1F49u3NwIoBEedyk3T81diWH";
                v1.Value = "YPwq4RiNknWjKO8xLCH0au9yvTVOauICmRrhoMSmNTxbCkhJElQlgCPqHeNDib8Lyn0j9Co8ql4j3XHYGLApzwT0zsWtCCOsAP2QGmlH6URbMvjFVobSA4KFTdJnUyYTNgD6pSjBDdIitYcuE94bjcbPFYDHo93NrVaO0PpbpC8V7gv61H3tgyeyZcyUqgFa7iMCZQzPSPB62oazNL7q8W4QZIlYOVvt2BygfkRXGuIyH1TOSOVZ1gY7dJJAWNtBWq17kfjvNCk336wjzs5BNaCFVCPT7EhwClmYXRrGDrjE7VUvY4oPmkDF5z8ZSXatvGvdsKhK0oJhOIk3FpAEgk";
                v1.Memo = "iE0ieqY5s7mFuHp9MtLHgbyrvW8VapN0Ez2pK1NtOtFh2f1xWOlVIsAMnYfPYopivwarSHga9BJMyKRYnDwrF2vKPRAK0IUNODzMlscsmx9VoCQft1go8HJ7NU54v6Ew7AAyKGGnCnmuxLQphFP0xDtTD3WEnaK6D5L09hTmpxgD2E4ktMPZmKT3TYnq6QdQGobYZ76xYgxb4TJaB2Y64VcEYoYXfgq5salobVgLhsgTVKdx2k0mWI1Hws";
                v2.Code = "PwZpsXPpjrkFoh2cVTwZcarB";
                v2.Name = "FGqgknmgCyma";
                v2.Value = "DBiStcqhDFUOdriADIX5FjXOBVnr";
                v2.Memo = "AI40XBdXYzAdnu3iL1SE6Us7sk60gILBBvuI5DKU49";
                context.Set<BaseSysPara>().Add(v1);
                context.Set<BaseSysPara>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(BaseSysParaBatchVM));

            BaseSysParaBatchVM vm = rv.Model as BaseSysParaBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BaseSysPara>().Find(v1.ID);
                var data2 = context.Set<BaseSysPara>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            BaseSysPara v1 = new BaseSysPara();
            BaseSysPara v2 = new BaseSysPara();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Code = "quEoRskNXqNvqdMI9aW088oqMCArNs8CJUsb4P8f";
                v1.Name = "gpyK92fnJ5vbQj1F49u3NwIoBEedyk3T81diWH";
                v1.Value = "YPwq4RiNknWjKO8xLCH0au9yvTVOauICmRrhoMSmNTxbCkhJElQlgCPqHeNDib8Lyn0j9Co8ql4j3XHYGLApzwT0zsWtCCOsAP2QGmlH6URbMvjFVobSA4KFTdJnUyYTNgD6pSjBDdIitYcuE94bjcbPFYDHo93NrVaO0PpbpC8V7gv61H3tgyeyZcyUqgFa7iMCZQzPSPB62oazNL7q8W4QZIlYOVvt2BygfkRXGuIyH1TOSOVZ1gY7dJJAWNtBWq17kfjvNCk336wjzs5BNaCFVCPT7EhwClmYXRrGDrjE7VUvY4oPmkDF5z8ZSXatvGvdsKhK0oJhOIk3FpAEgk";
                v1.Memo = "iE0ieqY5s7mFuHp9MtLHgbyrvW8VapN0Ez2pK1NtOtFh2f1xWOlVIsAMnYfPYopivwarSHga9BJMyKRYnDwrF2vKPRAK0IUNODzMlscsmx9VoCQft1go8HJ7NU54v6Ew7AAyKGGnCnmuxLQphFP0xDtTD3WEnaK6D5L09hTmpxgD2E4ktMPZmKT3TYnq6QdQGobYZ76xYgxb4TJaB2Y64VcEYoYXfgq5salobVgLhsgTVKdx2k0mWI1Hws";
                v2.Code = "PwZpsXPpjrkFoh2cVTwZcarB";
                v2.Name = "FGqgknmgCyma";
                v2.Value = "DBiStcqhDFUOdriADIX5FjXOBVnr";
                v2.Memo = "AI40XBdXYzAdnu3iL1SE6Us7sk60gILBBvuI5DKU49";
                context.Set<BaseSysPara>().Add(v1);
                context.Set<BaseSysPara>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(BaseSysParaBatchVM));

            BaseSysParaBatchVM vm = rv.Model as BaseSysParaBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BaseSysPara>().Find(v1.ID);
                var data2 = context.Set<BaseSysPara>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as BaseSysParaListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
