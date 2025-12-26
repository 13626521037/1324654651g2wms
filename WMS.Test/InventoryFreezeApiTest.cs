using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryFreezeVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;


namespace WMS.Test
{
    [TestClass]
    public class InventoryFreezeApiTest
    {
        private InventoryFreezeApiController _controller;
        private string _seed;

        public InventoryFreezeApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryFreezeApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryFreezeApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryFreezeApiVM vm = _controller.Wtm.CreateVM<InventoryFreezeApiVM>();
            InventoryFreeze v = new InventoryFreeze();
            
            v.DocNo = "JRHs7KZJAYh33W4YUWEo5ST5HLd";
            v.Reason = "eEzv9pIBnTeUC6oP8vhQs8Ion33QRXWuFyR6fKFsI9RS08sDysOUVWWZMgTFN183RfTshtqzlG7Y2Ti93O0aqubxqQdafkQbkaMJH2RQWPziQUlPyrlY3JmEMTg2L913YhiRYN7B6wZqjvw0ucIly6QNaBUhCF5PEMs9w3H3hztHurXxCnMJQPd02Ya3LRXu5mQbaQcfOgaPwUuSFZ6qFMslcVqqsMB5ZvuL5HMZFSHzFIWzybsCHQIEojvjSOJH5ZXBSoeK4BuwpoM9meBHS53q27StR9a4s1bMaEzDy58d1QmgaTaTht09aLgmSml0pC7BqASFjJsmuwI0I8FuDoazc94tDDIFXujqSaL8laUMvfe0PrYhRc1po0XwExxEMVbb8iJG5i9oW6B8MJdlqxEg2Z25DcbTSRuGTrYDd8wySQQ1CdqFZFHkqLDdGlZ";
            v.Memo = "PWkz5tM4Zac6B1zARid7zbVVDxuWP22MqBwIKQn4f1sKCtumivA3jovUpXGuvCikkuUR5OmzEqGZttQJDvjPgTxbjRwzmHdubv1eBjcZ0NrtwKh5AzH3gooOor1Qyuqm2ECs7zPmWn5QcmN7YbLPkZ5SIG9fhndMnYEMBGTnq5tZSOzjTJMpYerxBTPzjDoGeC2Rbkow83OAQ2xave1iF6SeNgzJRQN6DKO3FagwVgYQubiWhXN1Hg3OZ7x8whGxYcNihMjBt";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryFreeze>().Find(v.ID);
                
                Assert.AreEqual(data.DocNo, "JRHs7KZJAYh33W4YUWEo5ST5HLd");
                Assert.AreEqual(data.Reason, "eEzv9pIBnTeUC6oP8vhQs8Ion33QRXWuFyR6fKFsI9RS08sDysOUVWWZMgTFN183RfTshtqzlG7Y2Ti93O0aqubxqQdafkQbkaMJH2RQWPziQUlPyrlY3JmEMTg2L913YhiRYN7B6wZqjvw0ucIly6QNaBUhCF5PEMs9w3H3hztHurXxCnMJQPd02Ya3LRXu5mQbaQcfOgaPwUuSFZ6qFMslcVqqsMB5ZvuL5HMZFSHzFIWzybsCHQIEojvjSOJH5ZXBSoeK4BuwpoM9meBHS53q27StR9a4s1bMaEzDy58d1QmgaTaTht09aLgmSml0pC7BqASFjJsmuwI0I8FuDoazc94tDDIFXujqSaL8laUMvfe0PrYhRc1po0XwExxEMVbb8iJG5i9oW6B8MJdlqxEg2Z25DcbTSRuGTrYDd8wySQQ1CdqFZFHkqLDdGlZ");
                Assert.AreEqual(data.Memo, "PWkz5tM4Zac6B1zARid7zbVVDxuWP22MqBwIKQn4f1sKCtumivA3jovUpXGuvCikkuUR5OmzEqGZttQJDvjPgTxbjRwzmHdubv1eBjcZ0NrtwKh5AzH3gooOor1Qyuqm2ECs7zPmWn5QcmN7YbLPkZ5SIG9fhndMnYEMBGTnq5tZSOzjTJMpYerxBTPzjDoGeC2Rbkow83OAQ2xave1iF6SeNgzJRQN6DKO3FagwVgYQubiWhXN1Hg3OZ7x8whGxYcNihMjBt");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryFreeze v = new InventoryFreeze();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DocNo = "JRHs7KZJAYh33W4YUWEo5ST5HLd";
                v.Reason = "eEzv9pIBnTeUC6oP8vhQs8Ion33QRXWuFyR6fKFsI9RS08sDysOUVWWZMgTFN183RfTshtqzlG7Y2Ti93O0aqubxqQdafkQbkaMJH2RQWPziQUlPyrlY3JmEMTg2L913YhiRYN7B6wZqjvw0ucIly6QNaBUhCF5PEMs9w3H3hztHurXxCnMJQPd02Ya3LRXu5mQbaQcfOgaPwUuSFZ6qFMslcVqqsMB5ZvuL5HMZFSHzFIWzybsCHQIEojvjSOJH5ZXBSoeK4BuwpoM9meBHS53q27StR9a4s1bMaEzDy58d1QmgaTaTht09aLgmSml0pC7BqASFjJsmuwI0I8FuDoazc94tDDIFXujqSaL8laUMvfe0PrYhRc1po0XwExxEMVbb8iJG5i9oW6B8MJdlqxEg2Z25DcbTSRuGTrYDd8wySQQ1CdqFZFHkqLDdGlZ";
                v.Memo = "PWkz5tM4Zac6B1zARid7zbVVDxuWP22MqBwIKQn4f1sKCtumivA3jovUpXGuvCikkuUR5OmzEqGZttQJDvjPgTxbjRwzmHdubv1eBjcZ0NrtwKh5AzH3gooOor1Qyuqm2ECs7zPmWn5QcmN7YbLPkZ5SIG9fhndMnYEMBGTnq5tZSOzjTJMpYerxBTPzjDoGeC2Rbkow83OAQ2xave1iF6SeNgzJRQN6DKO3FagwVgYQubiWhXN1Hg3OZ7x8whGxYcNihMjBt";
                context.Set<InventoryFreeze>().Add(v);
                context.SaveChanges();
            }

            InventoryFreezeApiVM vm = _controller.Wtm.CreateVM<InventoryFreezeApiVM>();
            var oldID = v.ID;
            v = new InventoryFreeze();
            v.ID = oldID;
       		
            v.DocNo = "4gQ";
            v.Reason = "bZ4kPJsgGHf6XUQdjz4YyJqqNQEh60ynxQ3HjKsVdPrdEPotCFiS5CVFyxJMbKguyxUioilZKmsJ5qyjUZnKPBVLFc2TW604th8GeuAfROcPE3QUiWM9SJ11l2FM5QD60FF21KRNy6e9Jm3LoL0dVgyozFV5HiVUCuV2D";
            v.Memo = "f2HATlzFG0lprB0R3Q9kgVIuJ7wV2kdxJKbMrUQkAgDtKm5lItirsRHwnt5pm5fT3RucvG1CI0s7NIF7eFb8dwDCfYlgTivUuuuF6X0d8gSY0ypamL28fcLK8uNbJoUiyeYlIqY1lA9lA4LjCrVC0rJw0kpnZCugUdOcApdMz3fNn6fbUiEDRnO9HNjZefKsSxouuzFNjdvgiBTnkLQUw863qbaUtlYm3DNCdhldW1YFHOXEh0f4nOtWU61nGEerd7WurwO6Zhqu8R9SIhxq2DPssZxvhV3hp3Tt82VtQv78IVPLTbe8LboXUTd83kgTm0SBRWRrcYXZWdsTqYEcJx4KlXYMAaC2x9QI5jz8H4MJPPGM8edvO";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.Reason", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryFreeze>().Find(v.ID);
 				
                Assert.AreEqual(data.DocNo, "4gQ");
                Assert.AreEqual(data.Reason, "bZ4kPJsgGHf6XUQdjz4YyJqqNQEh60ynxQ3HjKsVdPrdEPotCFiS5CVFyxJMbKguyxUioilZKmsJ5qyjUZnKPBVLFc2TW604th8GeuAfROcPE3QUiWM9SJ11l2FM5QD60FF21KRNy6e9Jm3LoL0dVgyozFV5HiVUCuV2D");
                Assert.AreEqual(data.Memo, "f2HATlzFG0lprB0R3Q9kgVIuJ7wV2kdxJKbMrUQkAgDtKm5lItirsRHwnt5pm5fT3RucvG1CI0s7NIF7eFb8dwDCfYlgTivUuuuF6X0d8gSY0ypamL28fcLK8uNbJoUiyeYlIqY1lA9lA4LjCrVC0rJw0kpnZCugUdOcApdMz3fNn6fbUiEDRnO9HNjZefKsSxouuzFNjdvgiBTnkLQUw863qbaUtlYm3DNCdhldW1YFHOXEh0f4nOtWU61nGEerd7WurwO6Zhqu8R9SIhxq2DPssZxvhV3hp3Tt82VtQv78IVPLTbe8LboXUTd83kgTm0SBRWRrcYXZWdsTqYEcJx4KlXYMAaC2x9QI5jz8H4MJPPGM8edvO");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryFreeze v = new InventoryFreeze();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DocNo = "JRHs7KZJAYh33W4YUWEo5ST5HLd";
                v.Reason = "eEzv9pIBnTeUC6oP8vhQs8Ion33QRXWuFyR6fKFsI9RS08sDysOUVWWZMgTFN183RfTshtqzlG7Y2Ti93O0aqubxqQdafkQbkaMJH2RQWPziQUlPyrlY3JmEMTg2L913YhiRYN7B6wZqjvw0ucIly6QNaBUhCF5PEMs9w3H3hztHurXxCnMJQPd02Ya3LRXu5mQbaQcfOgaPwUuSFZ6qFMslcVqqsMB5ZvuL5HMZFSHzFIWzybsCHQIEojvjSOJH5ZXBSoeK4BuwpoM9meBHS53q27StR9a4s1bMaEzDy58d1QmgaTaTht09aLgmSml0pC7BqASFjJsmuwI0I8FuDoazc94tDDIFXujqSaL8laUMvfe0PrYhRc1po0XwExxEMVbb8iJG5i9oW6B8MJdlqxEg2Z25DcbTSRuGTrYDd8wySQQ1CdqFZFHkqLDdGlZ";
                v.Memo = "PWkz5tM4Zac6B1zARid7zbVVDxuWP22MqBwIKQn4f1sKCtumivA3jovUpXGuvCikkuUR5OmzEqGZttQJDvjPgTxbjRwzmHdubv1eBjcZ0NrtwKh5AzH3gooOor1Qyuqm2ECs7zPmWn5QcmN7YbLPkZ5SIG9fhndMnYEMBGTnq5tZSOzjTJMpYerxBTPzjDoGeC2Rbkow83OAQ2xave1iF6SeNgzJRQN6DKO3FagwVgYQubiWhXN1Hg3OZ7x8whGxYcNihMjBt";
                context.Set<InventoryFreeze>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryFreeze v1 = new InventoryFreeze();
            InventoryFreeze v2 = new InventoryFreeze();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DocNo = "JRHs7KZJAYh33W4YUWEo5ST5HLd";
                v1.Reason = "eEzv9pIBnTeUC6oP8vhQs8Ion33QRXWuFyR6fKFsI9RS08sDysOUVWWZMgTFN183RfTshtqzlG7Y2Ti93O0aqubxqQdafkQbkaMJH2RQWPziQUlPyrlY3JmEMTg2L913YhiRYN7B6wZqjvw0ucIly6QNaBUhCF5PEMs9w3H3hztHurXxCnMJQPd02Ya3LRXu5mQbaQcfOgaPwUuSFZ6qFMslcVqqsMB5ZvuL5HMZFSHzFIWzybsCHQIEojvjSOJH5ZXBSoeK4BuwpoM9meBHS53q27StR9a4s1bMaEzDy58d1QmgaTaTht09aLgmSml0pC7BqASFjJsmuwI0I8FuDoazc94tDDIFXujqSaL8laUMvfe0PrYhRc1po0XwExxEMVbb8iJG5i9oW6B8MJdlqxEg2Z25DcbTSRuGTrYDd8wySQQ1CdqFZFHkqLDdGlZ";
                v1.Memo = "PWkz5tM4Zac6B1zARid7zbVVDxuWP22MqBwIKQn4f1sKCtumivA3jovUpXGuvCikkuUR5OmzEqGZttQJDvjPgTxbjRwzmHdubv1eBjcZ0NrtwKh5AzH3gooOor1Qyuqm2ECs7zPmWn5QcmN7YbLPkZ5SIG9fhndMnYEMBGTnq5tZSOzjTJMpYerxBTPzjDoGeC2Rbkow83OAQ2xave1iF6SeNgzJRQN6DKO3FagwVgYQubiWhXN1Hg3OZ7x8whGxYcNihMjBt";
                v2.DocNo = "4gQ";
                v2.Reason = "bZ4kPJsgGHf6XUQdjz4YyJqqNQEh60ynxQ3HjKsVdPrdEPotCFiS5CVFyxJMbKguyxUioilZKmsJ5qyjUZnKPBVLFc2TW604th8GeuAfROcPE3QUiWM9SJ11l2FM5QD60FF21KRNy6e9Jm3LoL0dVgyozFV5HiVUCuV2D";
                v2.Memo = "f2HATlzFG0lprB0R3Q9kgVIuJ7wV2kdxJKbMrUQkAgDtKm5lItirsRHwnt5pm5fT3RucvG1CI0s7NIF7eFb8dwDCfYlgTivUuuuF6X0d8gSY0ypamL28fcLK8uNbJoUiyeYlIqY1lA9lA4LjCrVC0rJw0kpnZCugUdOcApdMz3fNn6fbUiEDRnO9HNjZefKsSxouuzFNjdvgiBTnkLQUw863qbaUtlYm3DNCdhldW1YFHOXEh0f4nOtWU61nGEerd7WurwO6Zhqu8R9SIhxq2DPssZxvhV3hp3Tt82VtQv78IVPLTbe8LboXUTd83kgTm0SBRWRrcYXZWdsTqYEcJx4KlXYMAaC2x9QI5jz8H4MJPPGM8edvO";
                context.Set<InventoryFreeze>().Add(v1);
                context.Set<InventoryFreeze>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryFreeze>().Find(v1.ID);
                var data2 = context.Set<InventoryFreeze>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
