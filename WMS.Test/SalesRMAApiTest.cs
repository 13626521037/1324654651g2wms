using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.SalesManagement.SalesRMAVMs;
using WMS.Model.SalesManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class SalesRMAApiTest
    {
        private SalesRMAApiController _controller;
        private string _seed;

        public SalesRMAApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<SalesRMAApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new SalesRMAApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            SalesRMAApiVM vm = _controller.Wtm.CreateVM<SalesRMAApiVM>();
            SalesRMA v = new SalesRMA();
            
            v.CreatePerson = "jZurkkZ8eGoniReGcN1RDM2kjlsk";
            v.OrganizationId = AddBaseOrganization();
            v.BusinessDate = DateTime.Parse("2024-12-19 08:33:57");
            v.ApproveDate = DateTime.Parse("2024-11-01 08:33:57");
            v.DocNo = "SdZlz9UA8KWA4ewjsjvY7wwjW";
            v.DocType = "2GNf";
            v.Operators = "lAgm2aJi57tiav21bTnyLthb4WFzfQ4pELn2LSYMq9TK";
            v.CustomerId = AddBaseCustomer();
            v.Status = WMS.Model.SalesRMAStatusEnum.PartReceive;
            v.Memo = "InRNHZArvMRxl8UP4ezENtmcsxHsQEmtFoBAGqJxA06lKeQ8uQn3Y4OyxDLCjLkNJAXac8tiRutU89zIdEpVOFy1OKkdBCc";
            v.SourceSystemId = "7yrfDSGNFcNiJrH46pyNhipMNoVZRWsPsKT13";
            v.LastUpdateTime = DateTime.Parse("2025-10-28 08:33:57");
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<SalesRMA>().Find(v.ID);
                
                Assert.AreEqual(data.CreatePerson, "jZurkkZ8eGoniReGcN1RDM2kjlsk");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-12-19 08:33:57"));
                Assert.AreEqual(data.ApproveDate, DateTime.Parse("2024-11-01 08:33:57"));
                Assert.AreEqual(data.DocNo, "SdZlz9UA8KWA4ewjsjvY7wwjW");
                Assert.AreEqual(data.DocType, "2GNf");
                Assert.AreEqual(data.Operators, "lAgm2aJi57tiav21bTnyLthb4WFzfQ4pELn2LSYMq9TK");
                Assert.AreEqual(data.Status, WMS.Model.SalesRMAStatusEnum.PartReceive);
                Assert.AreEqual(data.Memo, "InRNHZArvMRxl8UP4ezENtmcsxHsQEmtFoBAGqJxA06lKeQ8uQn3Y4OyxDLCjLkNJAXac8tiRutU89zIdEpVOFy1OKkdBCc");
                Assert.AreEqual(data.SourceSystemId, "7yrfDSGNFcNiJrH46pyNhipMNoVZRWsPsKT13");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2025-10-28 08:33:57"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            SalesRMA v = new SalesRMA();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.CreatePerson = "jZurkkZ8eGoniReGcN1RDM2kjlsk";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2024-12-19 08:33:57");
                v.ApproveDate = DateTime.Parse("2024-11-01 08:33:57");
                v.DocNo = "SdZlz9UA8KWA4ewjsjvY7wwjW";
                v.DocType = "2GNf";
                v.Operators = "lAgm2aJi57tiav21bTnyLthb4WFzfQ4pELn2LSYMq9TK";
                v.CustomerId = AddBaseCustomer();
                v.Status = WMS.Model.SalesRMAStatusEnum.PartReceive;
                v.Memo = "InRNHZArvMRxl8UP4ezENtmcsxHsQEmtFoBAGqJxA06lKeQ8uQn3Y4OyxDLCjLkNJAXac8tiRutU89zIdEpVOFy1OKkdBCc";
                v.SourceSystemId = "7yrfDSGNFcNiJrH46pyNhipMNoVZRWsPsKT13";
                v.LastUpdateTime = DateTime.Parse("2025-10-28 08:33:57");
                context.Set<SalesRMA>().Add(v);
                context.SaveChanges();
            }

            SalesRMAApiVM vm = _controller.Wtm.CreateVM<SalesRMAApiVM>();
            var oldID = v.ID;
            v = new SalesRMA();
            v.ID = oldID;
       		
            v.CreatePerson = "ikDT0ucHMTTQ20Zafg63I5bies9l8d";
            v.BusinessDate = DateTime.Parse("2024-09-01 08:33:57");
            v.ApproveDate = DateTime.Parse("2025-03-24 08:33:57");
            v.DocNo = "qEAAc8WTKMykoeOd8otESC3lBHXSeDwn";
            v.DocType = "Nuacd5Y90OqfizV5Y90Vd9bBsyrQmc5g0VjU";
            v.Operators = "MfQ5tW8UuNnZdDXH431ZCN2ITb4naHY";
            v.Status = WMS.Model.SalesRMAStatusEnum.AllReceive;
            v.Memo = "sJCON1cRs1eLwj8zVWMUu6pGxd7g1H6TTjG8Q7EYvVzUkrIAVmUs8M6CyAuOMPXuZNYVMWf6WmqaxXXadzh0fF4uCV5dadeV3xpBcwAqhwP0DBJndZQKCj7l1Jo2kwWnFyhdQX7dWlJhKeEXDjehMCe2ZLG9q3mqgCrEZ5nqp3B74xxqHCRFnOOop63kEcveNQaaJ4HqMcAxn73R6rZWpUxuby3FfJZcN60tUuiqaHFAKVkRwWJalzwMqjS5VF6EzofWqeNbNnug2qLYPzxtwnApeOZPTxX5cSfV6bJsfY4L6n2zNWU5bVgHx3WnKXT27PFuf7ShJz5HnsqSlRz5SU2zlKwQuaBNlFs6I2yww2616ZhkyrBN6P1zhwEbOiQbMVx3BmLGV76k6MWYvX3m7L";
            v.SourceSystemId = "Cbv2wvTtWQnSTB7y9hL4joYrpLRLiikbpaNr3APtJd";
            v.LastUpdateTime = DateTime.Parse("2024-11-09 08:33:57");
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.CreatePerson", "");
            vm.FC.Add("Entity.OrganizationId", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.ApproveDate", "");
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.DocType", "");
            vm.FC.Add("Entity.Operators", "");
            vm.FC.Add("Entity.CustomerId", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.Memo", "");
            vm.FC.Add("Entity.SourceSystemId", "");
            vm.FC.Add("Entity.LastUpdateTime", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<SalesRMA>().Find(v.ID);
 				
                Assert.AreEqual(data.CreatePerson, "ikDT0ucHMTTQ20Zafg63I5bies9l8d");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-09-01 08:33:57"));
                Assert.AreEqual(data.ApproveDate, DateTime.Parse("2025-03-24 08:33:57"));
                Assert.AreEqual(data.DocNo, "qEAAc8WTKMykoeOd8otESC3lBHXSeDwn");
                Assert.AreEqual(data.DocType, "Nuacd5Y90OqfizV5Y90Vd9bBsyrQmc5g0VjU");
                Assert.AreEqual(data.Operators, "MfQ5tW8UuNnZdDXH431ZCN2ITb4naHY");
                Assert.AreEqual(data.Status, WMS.Model.SalesRMAStatusEnum.AllReceive);
                Assert.AreEqual(data.Memo, "sJCON1cRs1eLwj8zVWMUu6pGxd7g1H6TTjG8Q7EYvVzUkrIAVmUs8M6CyAuOMPXuZNYVMWf6WmqaxXXadzh0fF4uCV5dadeV3xpBcwAqhwP0DBJndZQKCj7l1Jo2kwWnFyhdQX7dWlJhKeEXDjehMCe2ZLG9q3mqgCrEZ5nqp3B74xxqHCRFnOOop63kEcveNQaaJ4HqMcAxn73R6rZWpUxuby3FfJZcN60tUuiqaHFAKVkRwWJalzwMqjS5VF6EzofWqeNbNnug2qLYPzxtwnApeOZPTxX5cSfV6bJsfY4L6n2zNWU5bVgHx3WnKXT27PFuf7ShJz5HnsqSlRz5SU2zlKwQuaBNlFs6I2yww2616ZhkyrBN6P1zhwEbOiQbMVx3BmLGV76k6MWYvX3m7L");
                Assert.AreEqual(data.SourceSystemId, "Cbv2wvTtWQnSTB7y9hL4joYrpLRLiikbpaNr3APtJd");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2024-11-09 08:33:57"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            SalesRMA v = new SalesRMA();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.CreatePerson = "jZurkkZ8eGoniReGcN1RDM2kjlsk";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2024-12-19 08:33:57");
                v.ApproveDate = DateTime.Parse("2024-11-01 08:33:57");
                v.DocNo = "SdZlz9UA8KWA4ewjsjvY7wwjW";
                v.DocType = "2GNf";
                v.Operators = "lAgm2aJi57tiav21bTnyLthb4WFzfQ4pELn2LSYMq9TK";
                v.CustomerId = AddBaseCustomer();
                v.Status = WMS.Model.SalesRMAStatusEnum.PartReceive;
                v.Memo = "InRNHZArvMRxl8UP4ezENtmcsxHsQEmtFoBAGqJxA06lKeQ8uQn3Y4OyxDLCjLkNJAXac8tiRutU89zIdEpVOFy1OKkdBCc";
                v.SourceSystemId = "7yrfDSGNFcNiJrH46pyNhipMNoVZRWsPsKT13";
                v.LastUpdateTime = DateTime.Parse("2025-10-28 08:33:57");
                context.Set<SalesRMA>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            SalesRMA v1 = new SalesRMA();
            SalesRMA v2 = new SalesRMA();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.CreatePerson = "jZurkkZ8eGoniReGcN1RDM2kjlsk";
                v1.OrganizationId = AddBaseOrganization();
                v1.BusinessDate = DateTime.Parse("2024-12-19 08:33:57");
                v1.ApproveDate = DateTime.Parse("2024-11-01 08:33:57");
                v1.DocNo = "SdZlz9UA8KWA4ewjsjvY7wwjW";
                v1.DocType = "2GNf";
                v1.Operators = "lAgm2aJi57tiav21bTnyLthb4WFzfQ4pELn2LSYMq9TK";
                v1.CustomerId = AddBaseCustomer();
                v1.Status = WMS.Model.SalesRMAStatusEnum.PartReceive;
                v1.Memo = "InRNHZArvMRxl8UP4ezENtmcsxHsQEmtFoBAGqJxA06lKeQ8uQn3Y4OyxDLCjLkNJAXac8tiRutU89zIdEpVOFy1OKkdBCc";
                v1.SourceSystemId = "7yrfDSGNFcNiJrH46pyNhipMNoVZRWsPsKT13";
                v1.LastUpdateTime = DateTime.Parse("2025-10-28 08:33:57");
                v2.CreatePerson = "ikDT0ucHMTTQ20Zafg63I5bies9l8d";
                v2.OrganizationId = v1.OrganizationId; 
                v2.BusinessDate = DateTime.Parse("2024-09-01 08:33:57");
                v2.ApproveDate = DateTime.Parse("2025-03-24 08:33:57");
                v2.DocNo = "qEAAc8WTKMykoeOd8otESC3lBHXSeDwn";
                v2.DocType = "Nuacd5Y90OqfizV5Y90Vd9bBsyrQmc5g0VjU";
                v2.Operators = "MfQ5tW8UuNnZdDXH431ZCN2ITb4naHY";
                v2.CustomerId = v1.CustomerId; 
                v2.Status = WMS.Model.SalesRMAStatusEnum.AllReceive;
                v2.Memo = "sJCON1cRs1eLwj8zVWMUu6pGxd7g1H6TTjG8Q7EYvVzUkrIAVmUs8M6CyAuOMPXuZNYVMWf6WmqaxXXadzh0fF4uCV5dadeV3xpBcwAqhwP0DBJndZQKCj7l1Jo2kwWnFyhdQX7dWlJhKeEXDjehMCe2ZLG9q3mqgCrEZ5nqp3B74xxqHCRFnOOop63kEcveNQaaJ4HqMcAxn73R6rZWpUxuby3FfJZcN60tUuiqaHFAKVkRwWJalzwMqjS5VF6EzofWqeNbNnug2qLYPzxtwnApeOZPTxX5cSfV6bJsfY4L6n2zNWU5bVgHx3WnKXT27PFuf7ShJz5HnsqSlRz5SU2zlKwQuaBNlFs6I2yww2616ZhkyrBN6P1zhwEbOiQbMVx3BmLGV76k6MWYvX3m7L";
                v2.SourceSystemId = "Cbv2wvTtWQnSTB7y9hL4joYrpLRLiikbpaNr3APtJd";
                v2.LastUpdateTime = DateTime.Parse("2024-11-09 08:33:57");
                context.Set<SalesRMA>().Add(v1);
                context.Set<SalesRMA>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<SalesRMA>().Find(v1.ID);
                var data2 = context.Set<SalesRMA>().Find(v2.ID);
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
                v.IsSale = false;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "l1kyGG9QGXRMKXOhrTzrocM7iwu1wxy";
                v.Code = "Ls6zDXzKvomD5FNMwE8Aw6hd6fu";
                v.Name = "yGm0QUnWzkOyZbqENC0C9GpcSsYxSmiYT";
                v.SourceSystemId = "Cvf0uvd2eSSrhQsWZElyFGRzPdhDHpeq8i4MUEo";
                v.LastUpdateTime = DateTime.Parse("2027-03-09 08:33:57");
                context.Set<BaseOrganization>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseCustomer()
        {
            BaseCustomer v = new BaseCustomer();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ShortName = "A8BmCnW9zar3Qd6yL4AVZqD0wXQGQZUnpGgPfJQuF0QW7pUNdU98H6TMO8rR65z3p6KwQEeojw7Uj0B6K9H3aN489ssZtx9slFZXrwGAtW2DXAww89D82H1rZ86ulIealfHuO0wJbhHzV6HqRnM3Ecd2dADGwp79c2Y3OZW26SFjJMLYNVKV5d6TJPXzT5zJpKEZrzCirAKJNh24Z3Q7571wo9u7wXQZOMEoEh3GlUB9UogkU9rXSVysUW1lnFDnf9LB0HXliWBkoYnw7DHIBmkNfPlJQalYcgOUwSJuu1iyDtLsbYvhsRnTnJLCtEzqUPOLE9tZIPaG5GqG9PGjJo9df0ya2o";
                v.EnglishShortName = "Nc7mw1U0Ljlc5YVFjZjjugPCB8kO9x5wIvCD";
                v.OrganizationId = AddBaseOrganization();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "aH1YMBBGknVAhZ01YVXsokSJxp0pM4giHcunjmlpbEz6iRq11Q0m1obMVyDxoDN9CzyvSuH6xuzviaQiL9vvsHFGZ2lC1HdRPclzRfJkrUmp85RbX7KMZPylZtSXMofNrRIDXXFubVuyQXp9xaPfhNKpwEebKCswTmAfAQZa4QYmbYj6T30TspAtPSuNe0SfIuukHkWXw3yvJr86CTgmkZ0FvVF893TzT21yQV1f0v40jcZWeKdOJfM7u8hMtkReDjVz0mmZZ1kCgQ87dCQbpbuQXehGf5mFKP2RSpnunrNVsLNYsuY4kskxoKq8HqaavD0pGBJiKhrTlKFm1p8ImNsN7gHsO8X98D7DjnNIMEjyUcZSDlbPFAjJhf22fHlYFImzYNOavWRkQ9bIDdf44hAfqOhBfGFiRB4";
                v.Code = "wY8DJxaH8QTmJgNbwVp";
                v.Name = "Hb6ylYEyHHoFFZ9JLlv6h0CkEC9ufBpnvixgIOZHU7HjmAr";
                v.SourceSystemId = "rdXMy6";
                v.LastUpdateTime = DateTime.Parse("2027-03-02 08:33:57");
                context.Set<BaseCustomer>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
