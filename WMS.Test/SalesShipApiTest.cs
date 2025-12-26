using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.SalesManagement.SalesShipVMs;
using WMS.Model.SalesManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class SalesShipApiTest
    {
        private SalesShipApiController _controller;
        private string _seed;

        public SalesShipApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<SalesShipApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new SalesShipApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            SalesShipApiVM vm = _controller.Wtm.CreateVM<SalesShipApiVM>();
            SalesShip v = new SalesShip();
            
            v.CreatePerson = "zHFXD4ZqxOoao";
            v.OrganizationId = AddBaseOrganization();
            v.BusinessDate = DateTime.Parse("2024-12-10 10:13:18");
            v.SubmitDate = DateTime.Parse("2024-08-10 10:13:18");
            v.DocNo = "MB37VIAgdUrP8lpe8NyqFPGEcUYharsj16rYKlb0";
            v.DocType = "K9hC7pwCTPpUj1KJBvHGwPyLDWR";
            v.Operators = "u81SJsrYeYcrOBPfEIVN4dPIDHlTVIEyo8HHB88Qc0dNfYBAeQXlY38eCW";
            v.CustomerId = AddBaseCustomer();
            v.Status = WMS.Model.SalesShipStatusEnum.AllShipped;
            v.Memo = "GsZd5KUBFN0nLzujr0WDlPLuquaijxN100q3NTQG8FSq3fE1nm44zAgHLZZweRMbblF7AhZdtxCj9iqySdenR3QBlK7ask8tiIXZL3OkL0Bt1daCPzczvRRsYLj6gxiBAUX6NKncny7FbEYOllpHInoOpYw5P5yvH08ovxPecz8DSGmhat0ENwEgYyTYwpxJKb3apOsJ0ioBaegsL6uAFgVABh5OIMMhW4dQOV8vqaAmkcWNTBJOkapKKaNE2iLKGEDuCFJjd9JaHpgJAWWUj9SxOx4rkd7qADhpEjKloajwSRd4ZKKiQbcJWs58rIARk";
            v.SourceSystemId = "hSt0EQGSVxH6jasfsSUrmdgPyrgoHj1A17";
            v.LastUpdateTime = DateTime.Parse("2025-12-15 10:13:18");
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<SalesShip>().Find(v.ID);
                
                Assert.AreEqual(data.CreatePerson, "zHFXD4ZqxOoao");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-12-10 10:13:18"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2024-08-10 10:13:18"));
                Assert.AreEqual(data.DocNo, "MB37VIAgdUrP8lpe8NyqFPGEcUYharsj16rYKlb0");
                Assert.AreEqual(data.DocType, "K9hC7pwCTPpUj1KJBvHGwPyLDWR");
                Assert.AreEqual(data.Operators, "u81SJsrYeYcrOBPfEIVN4dPIDHlTVIEyo8HHB88Qc0dNfYBAeQXlY38eCW");
                Assert.AreEqual(data.Status, WMS.Model.SalesShipStatusEnum.AllShipped);
                Assert.AreEqual(data.Memo, "GsZd5KUBFN0nLzujr0WDlPLuquaijxN100q3NTQG8FSq3fE1nm44zAgHLZZweRMbblF7AhZdtxCj9iqySdenR3QBlK7ask8tiIXZL3OkL0Bt1daCPzczvRRsYLj6gxiBAUX6NKncny7FbEYOllpHInoOpYw5P5yvH08ovxPecz8DSGmhat0ENwEgYyTYwpxJKb3apOsJ0ioBaegsL6uAFgVABh5OIMMhW4dQOV8vqaAmkcWNTBJOkapKKaNE2iLKGEDuCFJjd9JaHpgJAWWUj9SxOx4rkd7qADhpEjKloajwSRd4ZKKiQbcJWs58rIARk");
                Assert.AreEqual(data.SourceSystemId, "hSt0EQGSVxH6jasfsSUrmdgPyrgoHj1A17");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2025-12-15 10:13:18"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            SalesShip v = new SalesShip();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.CreatePerson = "zHFXD4ZqxOoao";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2024-12-10 10:13:18");
                v.SubmitDate = DateTime.Parse("2024-08-10 10:13:18");
                v.DocNo = "MB37VIAgdUrP8lpe8NyqFPGEcUYharsj16rYKlb0";
                v.DocType = "K9hC7pwCTPpUj1KJBvHGwPyLDWR";
                v.Operators = "u81SJsrYeYcrOBPfEIVN4dPIDHlTVIEyo8HHB88Qc0dNfYBAeQXlY38eCW";
                v.CustomerId = AddBaseCustomer();
                v.Status = WMS.Model.SalesShipStatusEnum.AllShipped;
                v.Memo = "GsZd5KUBFN0nLzujr0WDlPLuquaijxN100q3NTQG8FSq3fE1nm44zAgHLZZweRMbblF7AhZdtxCj9iqySdenR3QBlK7ask8tiIXZL3OkL0Bt1daCPzczvRRsYLj6gxiBAUX6NKncny7FbEYOllpHInoOpYw5P5yvH08ovxPecz8DSGmhat0ENwEgYyTYwpxJKb3apOsJ0ioBaegsL6uAFgVABh5OIMMhW4dQOV8vqaAmkcWNTBJOkapKKaNE2iLKGEDuCFJjd9JaHpgJAWWUj9SxOx4rkd7qADhpEjKloajwSRd4ZKKiQbcJWs58rIARk";
                v.SourceSystemId = "hSt0EQGSVxH6jasfsSUrmdgPyrgoHj1A17";
                v.LastUpdateTime = DateTime.Parse("2025-12-15 10:13:18");
                context.Set<SalesShip>().Add(v);
                context.SaveChanges();
            }

            SalesShipApiVM vm = _controller.Wtm.CreateVM<SalesShipApiVM>();
            var oldID = v.ID;
            v = new SalesShip();
            v.ID = oldID;
       		
            v.CreatePerson = "Cnjru4RnABlUm0ezMhtP2fBn4P33PQeotOe";
            v.BusinessDate = DateTime.Parse("2025-05-05 10:13:18");
            v.SubmitDate = DateTime.Parse("2025-10-21 10:13:18");
            v.DocNo = "ttq";
            v.DocType = "JqFApmEF2jy3ph4I1bsqbIPFQXsWgXzzbrrXwffoMHWwrt";
            v.Operators = "omcoj8eQMmP9VQ40rANLiVRXJvZLCDWtDIu2h5aaqQe3A9ccMVEgS5vDt9pA";
            v.Status = WMS.Model.SalesShipStatusEnum.AllOff;
            v.Memo = "2G7MXJSnZAzZIsDtYWLFZuZpiznsUW8bxqSsjRJ3KOnp2AZkOBCyvHDtBOoU5lVMHlfANmi6LuKwc5cjdqP97ODYXbTmNP5w7JDUkojUXccjeNneuTWdGe70kMOXIQ0D2eOHttKLX4Cn4MjyLrUwfXODXMqIMQsTWlN8erP7vPnXECbAtljWhYFCg3RUOvGPxO1KN229QrxiS9s5bdTWKaD6lkhdwjslxehI5AnRCZtEnjkKS3wEG8B4s0BWe6r5KzNJ1YC3PDTN1PkodGG4DgULMwT0MFXQknwx8Ky7OtVMdEws7hwOV48lBwsakhf9WH9GNL5ajd38Z";
            v.SourceSystemId = "yTMMxYkB6WtX5Ln2BN1DBwsNL03lORUda3WGtlP1Zx0K";
            v.LastUpdateTime = DateTime.Parse("2025-10-27 10:13:18");
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.CreatePerson", "");
            vm.FC.Add("Entity.OrganizationId", "");
            vm.FC.Add("Entity.BusinessDate", "");
            vm.FC.Add("Entity.SubmitDate", "");
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
                var data = context.Set<SalesShip>().Find(v.ID);
 				
                Assert.AreEqual(data.CreatePerson, "Cnjru4RnABlUm0ezMhtP2fBn4P33PQeotOe");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2025-05-05 10:13:18"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2025-10-21 10:13:18"));
                Assert.AreEqual(data.DocNo, "ttq");
                Assert.AreEqual(data.DocType, "JqFApmEF2jy3ph4I1bsqbIPFQXsWgXzzbrrXwffoMHWwrt");
                Assert.AreEqual(data.Operators, "omcoj8eQMmP9VQ40rANLiVRXJvZLCDWtDIu2h5aaqQe3A9ccMVEgS5vDt9pA");
                Assert.AreEqual(data.Status, WMS.Model.SalesShipStatusEnum.AllOff);
                Assert.AreEqual(data.Memo, "2G7MXJSnZAzZIsDtYWLFZuZpiznsUW8bxqSsjRJ3KOnp2AZkOBCyvHDtBOoU5lVMHlfANmi6LuKwc5cjdqP97ODYXbTmNP5w7JDUkojUXccjeNneuTWdGe70kMOXIQ0D2eOHttKLX4Cn4MjyLrUwfXODXMqIMQsTWlN8erP7vPnXECbAtljWhYFCg3RUOvGPxO1KN229QrxiS9s5bdTWKaD6lkhdwjslxehI5AnRCZtEnjkKS3wEG8B4s0BWe6r5KzNJ1YC3PDTN1PkodGG4DgULMwT0MFXQknwx8Ky7OtVMdEws7hwOV48lBwsakhf9WH9GNL5ajd38Z");
                Assert.AreEqual(data.SourceSystemId, "yTMMxYkB6WtX5Ln2BN1DBwsNL03lORUda3WGtlP1Zx0K");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2025-10-27 10:13:18"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            SalesShip v = new SalesShip();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.CreatePerson = "zHFXD4ZqxOoao";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2024-12-10 10:13:18");
                v.SubmitDate = DateTime.Parse("2024-08-10 10:13:18");
                v.DocNo = "MB37VIAgdUrP8lpe8NyqFPGEcUYharsj16rYKlb0";
                v.DocType = "K9hC7pwCTPpUj1KJBvHGwPyLDWR";
                v.Operators = "u81SJsrYeYcrOBPfEIVN4dPIDHlTVIEyo8HHB88Qc0dNfYBAeQXlY38eCW";
                v.CustomerId = AddBaseCustomer();
                v.Status = WMS.Model.SalesShipStatusEnum.AllShipped;
                v.Memo = "GsZd5KUBFN0nLzujr0WDlPLuquaijxN100q3NTQG8FSq3fE1nm44zAgHLZZweRMbblF7AhZdtxCj9iqySdenR3QBlK7ask8tiIXZL3OkL0Bt1daCPzczvRRsYLj6gxiBAUX6NKncny7FbEYOllpHInoOpYw5P5yvH08ovxPecz8DSGmhat0ENwEgYyTYwpxJKb3apOsJ0ioBaegsL6uAFgVABh5OIMMhW4dQOV8vqaAmkcWNTBJOkapKKaNE2iLKGEDuCFJjd9JaHpgJAWWUj9SxOx4rkd7qADhpEjKloajwSRd4ZKKiQbcJWs58rIARk";
                v.SourceSystemId = "hSt0EQGSVxH6jasfsSUrmdgPyrgoHj1A17";
                v.LastUpdateTime = DateTime.Parse("2025-12-15 10:13:18");
                context.Set<SalesShip>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            SalesShip v1 = new SalesShip();
            SalesShip v2 = new SalesShip();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.CreatePerson = "zHFXD4ZqxOoao";
                v1.OrganizationId = AddBaseOrganization();
                v1.BusinessDate = DateTime.Parse("2024-12-10 10:13:18");
                v1.SubmitDate = DateTime.Parse("2024-08-10 10:13:18");
                v1.DocNo = "MB37VIAgdUrP8lpe8NyqFPGEcUYharsj16rYKlb0";
                v1.DocType = "K9hC7pwCTPpUj1KJBvHGwPyLDWR";
                v1.Operators = "u81SJsrYeYcrOBPfEIVN4dPIDHlTVIEyo8HHB88Qc0dNfYBAeQXlY38eCW";
                v1.CustomerId = AddBaseCustomer();
                v1.Status = WMS.Model.SalesShipStatusEnum.AllShipped;
                v1.Memo = "GsZd5KUBFN0nLzujr0WDlPLuquaijxN100q3NTQG8FSq3fE1nm44zAgHLZZweRMbblF7AhZdtxCj9iqySdenR3QBlK7ask8tiIXZL3OkL0Bt1daCPzczvRRsYLj6gxiBAUX6NKncny7FbEYOllpHInoOpYw5P5yvH08ovxPecz8DSGmhat0ENwEgYyTYwpxJKb3apOsJ0ioBaegsL6uAFgVABh5OIMMhW4dQOV8vqaAmkcWNTBJOkapKKaNE2iLKGEDuCFJjd9JaHpgJAWWUj9SxOx4rkd7qADhpEjKloajwSRd4ZKKiQbcJWs58rIARk";
                v1.SourceSystemId = "hSt0EQGSVxH6jasfsSUrmdgPyrgoHj1A17";
                v1.LastUpdateTime = DateTime.Parse("2025-12-15 10:13:18");
                v2.CreatePerson = "Cnjru4RnABlUm0ezMhtP2fBn4P33PQeotOe";
                v2.OrganizationId = v1.OrganizationId; 
                v2.BusinessDate = DateTime.Parse("2025-05-05 10:13:18");
                v2.SubmitDate = DateTime.Parse("2025-10-21 10:13:18");
                v2.DocNo = "ttq";
                v2.DocType = "JqFApmEF2jy3ph4I1bsqbIPFQXsWgXzzbrrXwffoMHWwrt";
                v2.Operators = "omcoj8eQMmP9VQ40rANLiVRXJvZLCDWtDIu2h5aaqQe3A9ccMVEgS5vDt9pA";
                v2.CustomerId = v1.CustomerId; 
                v2.Status = WMS.Model.SalesShipStatusEnum.AllOff;
                v2.Memo = "2G7MXJSnZAzZIsDtYWLFZuZpiznsUW8bxqSsjRJ3KOnp2AZkOBCyvHDtBOoU5lVMHlfANmi6LuKwc5cjdqP97ODYXbTmNP5w7JDUkojUXccjeNneuTWdGe70kMOXIQ0D2eOHttKLX4Cn4MjyLrUwfXODXMqIMQsTWlN8erP7vPnXECbAtljWhYFCg3RUOvGPxO1KN229QrxiS9s5bdTWKaD6lkhdwjslxehI5AnRCZtEnjkKS3wEG8B4s0BWe6r5KzNJ1YC3PDTN1PkodGG4DgULMwT0MFXQknwx8Ky7OtVMdEws7hwOV48lBwsakhf9WH9GNL5ajd38Z";
                v2.SourceSystemId = "yTMMxYkB6WtX5Ln2BN1DBwsNL03lORUda3WGtlP1Zx0K";
                v2.LastUpdateTime = DateTime.Parse("2025-10-27 10:13:18");
                context.Set<SalesShip>().Add(v1);
                context.Set<SalesShip>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<SalesShip>().Find(v1.ID);
                var data2 = context.Set<SalesShip>().Find(v2.ID);
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
                v.IsSale = null;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "ZzzIauWPEQJ6KuRFFCqBslOqVR1f0YMdw0YPEQSFsMLYwnokTxZH6Whndinq4V2VU1Et9iF69OvDbcdgWEHlPvoinMB0b5lIVaS2eObbAlUFKSoPbT0JZrS6zWn88eZn2Lc5XAhwwfv4I6Tp5r39YyiWw9RAFavcyuSZMaQ5ng28TofI8xQlELMfaFL7knBjZTPG0DEXySu9nTAvcbwXUK8KFEl2k4Z81SkciDZEWnML6GxA2BG5XQHzLTzmxDOQuvRK6h2sKSLrRSg33SICgrinyV8Ingnlf7tInS03jv6Ibg54EdiVRgLSBeHKCxZAOxwAjaj8OyBJMpltZszuy6Lg5hH5C2U0WmLJIK5blsyXkphk1GzsNkqcxlYEMURh0lDiSTDsXhS7yBF8xugahxE4GgXrsWohG6QMqJ6ldHFaKp2SPPStgX6k8P66Qdtu";
                v.Code = "m0gDiTVmt5EJVXQXcj";
                v.Name = "4Qr0bGWIHBh38dtsS4ehxE3oR1niNRuJ";
                v.SourceSystemId = "DW0wdh5Dmq8u";
                v.LastUpdateTime = DateTime.Parse("2025-03-03 10:13:18");
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

                v.ShortName = "oxrRg5KVSUDCDTHvXlFKJoh6UuZlpLAC8gq09xNE4EanrklT26BtI7xhTciilAr6h3wYUEMVPuqDXIjFFfW0J4RGWfWy64GMbD2JRBCBgziUvoK1ekYeQWQqXbX77REj6yEVNDKZhaJQ8tgQp8KssuyGIMXOrWcFMYjzniybv44Pb8QG95L9PfIVwFcdPQGyOhcyRssHXDJoOdBB9TCZZBc0CWcdc4awlHKTYK2XX47bOT3lYZUX2M1RZIHCN3cDuLfNFY2YRK";
                v.EnglishShortName = "4BinUFJ3EEbB2530hAJF8Y";
                v.OrganizationId = AddBaseOrganization();
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "sNnQ0GKdKeRKMD4PuQArUCoEa6WPpvktqSmJK4rTutsDgHPtE32aChG7LSm9w09eS4Et0MytwNqREQmP6HgRRTJeIQ9MkiwYGXVQ7WmJjeU9FNbPAz5POzG24YI9N9fgM39FThDHO3RSFFu1qaushHlVhYzFENtyipgefrmaeb3hWTCdfNgdzTqLqW0Tu2Mo6nT1k4Mmr5tTSW8qtmSPraO0h7iGxSxgmIGUkjUPu3U7NeGjpHgLlznmx6j";
                v.Code = "MA55Re9Fd3K7";
                v.Name = "cYBeoOE9cKqStBOSZlXRFS5";
                v.SourceSystemId = "y5rCKCbU";
                v.LastUpdateTime = DateTime.Parse("2026-09-10 10:13:18");
                context.Set<BaseCustomer>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
