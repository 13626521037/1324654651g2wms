using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.ProductionManagement.ProductionIssueVMs;
using WMS.Model.ProductionManagement;
using WMS.DataAccess;
using WMS.Model.BaseData;


namespace WMS.Test
{
    [TestClass]
    public class ProductionIssueApiTest
    {
        private ProductionIssueApiController _controller;
        private string _seed;

        public ProductionIssueApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<ProductionIssueApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new ProductionIssueApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            ProductionIssueApiVM vm = _controller.Wtm.CreateVM<ProductionIssueApiVM>();
            ProductionIssue v = new ProductionIssue();
            
            v.CreatePerson = "LOmdpRsbO8zNcAg9tiXIiej9s8j77AuISNn04TaRDDy2a9Wi1";
            v.OrganizationId = AddBaseOrganization();
            v.BusinessDate = DateTime.Parse("2026-02-18 14:15:53");
            v.SubmitDate = DateTime.Parse("2026-03-03 14:15:53");
            v.DocNo = "l4PuCDdMVKrXfNCRfHoqWGgPZ3Ap7dszQ5coTVYJU7";
            v.DocType = "lBAodD7Z7vAjzYrPpAR4J7pFQxRCB";
            v.Status = WMS.Model.ProductionIssueStatusEnum.PartOff;
            v.Memo = "Vb6gYGK15tpdaagfwA36FXZCQXK0scczQw44AROH5zHJ9Gku7pXeoms2arQ26ljVrnMFtYi26e6xyyVVdhqMPNfllHYwZjtjJSmT99YD5JQx6kvg9vfhDthpVn2Npqpvekt648NDowoCzqH3dXlod6QWS8WI3Bm9NRKJ1dxrtzm8tYLvyyIwzhGp8EiANdMYSPCznVANA6adsiANj3WcdpGLWMPwiX6gev9xvzYBPTIqKpNHGa4Ss0FLAhZ7yjsUUMurLQTxKwXO9Pt7NxtZVEkbzG1DH0zbRsG2slO6CvtrCAV8SWKBVCAWKdfXcRRXWA2d6dEHT9zNl7rikLkoDD4j7bdw85SGr3mNMmQ9GKFcA7CBGNszEf2GCVcA401stbnECef8NwjyNO3tENHguVf4tOFcjuRhCryaYMR9";
            v.SourceSystemId = "4G3U5EpauTT4aS9z53vrvH0";
            v.LastUpdateTime = DateTime.Parse("2026-11-08 14:15:53");
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ProductionIssue>().Find(v.ID);
                
                Assert.AreEqual(data.CreatePerson, "LOmdpRsbO8zNcAg9tiXIiej9s8j77AuISNn04TaRDDy2a9Wi1");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2026-02-18 14:15:53"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2026-03-03 14:15:53"));
                Assert.AreEqual(data.DocNo, "l4PuCDdMVKrXfNCRfHoqWGgPZ3Ap7dszQ5coTVYJU7");
                Assert.AreEqual(data.DocType, "lBAodD7Z7vAjzYrPpAR4J7pFQxRCB");
                Assert.AreEqual(data.Status, WMS.Model.ProductionIssueStatusEnum.PartOff);
                Assert.AreEqual(data.Memo, "Vb6gYGK15tpdaagfwA36FXZCQXK0scczQw44AROH5zHJ9Gku7pXeoms2arQ26ljVrnMFtYi26e6xyyVVdhqMPNfllHYwZjtjJSmT99YD5JQx6kvg9vfhDthpVn2Npqpvekt648NDowoCzqH3dXlod6QWS8WI3Bm9NRKJ1dxrtzm8tYLvyyIwzhGp8EiANdMYSPCznVANA6adsiANj3WcdpGLWMPwiX6gev9xvzYBPTIqKpNHGa4Ss0FLAhZ7yjsUUMurLQTxKwXO9Pt7NxtZVEkbzG1DH0zbRsG2slO6CvtrCAV8SWKBVCAWKdfXcRRXWA2d6dEHT9zNl7rikLkoDD4j7bdw85SGr3mNMmQ9GKFcA7CBGNszEf2GCVcA401stbnECef8NwjyNO3tENHguVf4tOFcjuRhCryaYMR9");
                Assert.AreEqual(data.SourceSystemId, "4G3U5EpauTT4aS9z53vrvH0");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2026-11-08 14:15:53"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            ProductionIssue v = new ProductionIssue();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.CreatePerson = "LOmdpRsbO8zNcAg9tiXIiej9s8j77AuISNn04TaRDDy2a9Wi1";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2026-02-18 14:15:53");
                v.SubmitDate = DateTime.Parse("2026-03-03 14:15:53");
                v.DocNo = "l4PuCDdMVKrXfNCRfHoqWGgPZ3Ap7dszQ5coTVYJU7";
                v.DocType = "lBAodD7Z7vAjzYrPpAR4J7pFQxRCB";
                v.Status = WMS.Model.ProductionIssueStatusEnum.PartOff;
                v.Memo = "Vb6gYGK15tpdaagfwA36FXZCQXK0scczQw44AROH5zHJ9Gku7pXeoms2arQ26ljVrnMFtYi26e6xyyVVdhqMPNfllHYwZjtjJSmT99YD5JQx6kvg9vfhDthpVn2Npqpvekt648NDowoCzqH3dXlod6QWS8WI3Bm9NRKJ1dxrtzm8tYLvyyIwzhGp8EiANdMYSPCznVANA6adsiANj3WcdpGLWMPwiX6gev9xvzYBPTIqKpNHGa4Ss0FLAhZ7yjsUUMurLQTxKwXO9Pt7NxtZVEkbzG1DH0zbRsG2slO6CvtrCAV8SWKBVCAWKdfXcRRXWA2d6dEHT9zNl7rikLkoDD4j7bdw85SGr3mNMmQ9GKFcA7CBGNszEf2GCVcA401stbnECef8NwjyNO3tENHguVf4tOFcjuRhCryaYMR9";
                v.SourceSystemId = "4G3U5EpauTT4aS9z53vrvH0";
                v.LastUpdateTime = DateTime.Parse("2026-11-08 14:15:53");
                context.Set<ProductionIssue>().Add(v);
                context.SaveChanges();
            }

            ProductionIssueApiVM vm = _controller.Wtm.CreateVM<ProductionIssueApiVM>();
            var oldID = v.ID;
            v = new ProductionIssue();
            v.ID = oldID;
       		
            v.CreatePerson = "bCIDEHwsvgRqP2mSp13tSyGHXuVX8I2f44TMCttyChrPxWhqQ";
            v.BusinessDate = DateTime.Parse("2024-10-17 14:15:53");
            v.SubmitDate = DateTime.Parse("2025-12-31 14:15:53");
            v.DocNo = "V7gwcZhf1dYAHg";
            v.DocType = "eQlUsYjWtjBZ9vDQ9S2w2kBfOFVFQV3r4JXNhLdq44j";
            v.Status = WMS.Model.ProductionIssueStatusEnum.PartShipped;
            v.Memo = "LRFoFzHMrhlOgvXThrI0A8TzplG0Dhp2S8GdtzrDBbVFnsyBIK9wGy3FLOriNOsktkrfas75uim957vxkAHgH4JLAa1UI8W0XT2LR5eWMc6mxsdwzICUip01bM3KI3NVjmym5TVJcezmLFuW913Tqzk8aEyhEK1hwgSc3ThKjpvKPKXH1PPHkbzBRxHmav7XBDXpt3raERvEw7jB7zU2VyTLxreI2lJKQw4kvOFGdHVmpIfPMDaJfCumFM50nFteehM5QNEDW8yQ8vsxXgIDiEcqkanyth0Ds1KDuLuiP6q3JBN16CPOKPcogeCMT90BjFc5iMD2slHQkC6Kn0YSDq6DewodqefMfawiJ7";
            v.SourceSystemId = "Jb9rnl4OqQHUuK8RjPnqS";
            v.LastUpdateTime = DateTime.Parse("2025-10-05 14:15:53");
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
                var data = context.Set<ProductionIssue>().Find(v.ID);
 				
                Assert.AreEqual(data.CreatePerson, "bCIDEHwsvgRqP2mSp13tSyGHXuVX8I2f44TMCttyChrPxWhqQ");
                Assert.AreEqual(data.BusinessDate, DateTime.Parse("2024-10-17 14:15:53"));
                Assert.AreEqual(data.SubmitDate, DateTime.Parse("2025-12-31 14:15:53"));
                Assert.AreEqual(data.DocNo, "V7gwcZhf1dYAHg");
                Assert.AreEqual(data.DocType, "eQlUsYjWtjBZ9vDQ9S2w2kBfOFVFQV3r4JXNhLdq44j");
                Assert.AreEqual(data.Status, WMS.Model.ProductionIssueStatusEnum.PartShipped);
                Assert.AreEqual(data.Memo, "LRFoFzHMrhlOgvXThrI0A8TzplG0Dhp2S8GdtzrDBbVFnsyBIK9wGy3FLOriNOsktkrfas75uim957vxkAHgH4JLAa1UI8W0XT2LR5eWMc6mxsdwzICUip01bM3KI3NVjmym5TVJcezmLFuW913Tqzk8aEyhEK1hwgSc3ThKjpvKPKXH1PPHkbzBRxHmav7XBDXpt3raERvEw7jB7zU2VyTLxreI2lJKQw4kvOFGdHVmpIfPMDaJfCumFM50nFteehM5QNEDW8yQ8vsxXgIDiEcqkanyth0Ds1KDuLuiP6q3JBN16CPOKPcogeCMT90BjFc5iMD2slHQkC6Kn0YSDq6DewodqefMfawiJ7");
                Assert.AreEqual(data.SourceSystemId, "Jb9rnl4OqQHUuK8RjPnqS");
                Assert.AreEqual(data.LastUpdateTime, DateTime.Parse("2025-10-05 14:15:53"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            ProductionIssue v = new ProductionIssue();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.CreatePerson = "LOmdpRsbO8zNcAg9tiXIiej9s8j77AuISNn04TaRDDy2a9Wi1";
                v.OrganizationId = AddBaseOrganization();
                v.BusinessDate = DateTime.Parse("2026-02-18 14:15:53");
                v.SubmitDate = DateTime.Parse("2026-03-03 14:15:53");
                v.DocNo = "l4PuCDdMVKrXfNCRfHoqWGgPZ3Ap7dszQ5coTVYJU7";
                v.DocType = "lBAodD7Z7vAjzYrPpAR4J7pFQxRCB";
                v.Status = WMS.Model.ProductionIssueStatusEnum.PartOff;
                v.Memo = "Vb6gYGK15tpdaagfwA36FXZCQXK0scczQw44AROH5zHJ9Gku7pXeoms2arQ26ljVrnMFtYi26e6xyyVVdhqMPNfllHYwZjtjJSmT99YD5JQx6kvg9vfhDthpVn2Npqpvekt648NDowoCzqH3dXlod6QWS8WI3Bm9NRKJ1dxrtzm8tYLvyyIwzhGp8EiANdMYSPCznVANA6adsiANj3WcdpGLWMPwiX6gev9xvzYBPTIqKpNHGa4Ss0FLAhZ7yjsUUMurLQTxKwXO9Pt7NxtZVEkbzG1DH0zbRsG2slO6CvtrCAV8SWKBVCAWKdfXcRRXWA2d6dEHT9zNl7rikLkoDD4j7bdw85SGr3mNMmQ9GKFcA7CBGNszEf2GCVcA401stbnECef8NwjyNO3tENHguVf4tOFcjuRhCryaYMR9";
                v.SourceSystemId = "4G3U5EpauTT4aS9z53vrvH0";
                v.LastUpdateTime = DateTime.Parse("2026-11-08 14:15:53");
                context.Set<ProductionIssue>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            ProductionIssue v1 = new ProductionIssue();
            ProductionIssue v2 = new ProductionIssue();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.CreatePerson = "LOmdpRsbO8zNcAg9tiXIiej9s8j77AuISNn04TaRDDy2a9Wi1";
                v1.OrganizationId = AddBaseOrganization();
                v1.BusinessDate = DateTime.Parse("2026-02-18 14:15:53");
                v1.SubmitDate = DateTime.Parse("2026-03-03 14:15:53");
                v1.DocNo = "l4PuCDdMVKrXfNCRfHoqWGgPZ3Ap7dszQ5coTVYJU7";
                v1.DocType = "lBAodD7Z7vAjzYrPpAR4J7pFQxRCB";
                v1.Status = WMS.Model.ProductionIssueStatusEnum.PartOff;
                v1.Memo = "Vb6gYGK15tpdaagfwA36FXZCQXK0scczQw44AROH5zHJ9Gku7pXeoms2arQ26ljVrnMFtYi26e6xyyVVdhqMPNfllHYwZjtjJSmT99YD5JQx6kvg9vfhDthpVn2Npqpvekt648NDowoCzqH3dXlod6QWS8WI3Bm9NRKJ1dxrtzm8tYLvyyIwzhGp8EiANdMYSPCznVANA6adsiANj3WcdpGLWMPwiX6gev9xvzYBPTIqKpNHGa4Ss0FLAhZ7yjsUUMurLQTxKwXO9Pt7NxtZVEkbzG1DH0zbRsG2slO6CvtrCAV8SWKBVCAWKdfXcRRXWA2d6dEHT9zNl7rikLkoDD4j7bdw85SGr3mNMmQ9GKFcA7CBGNszEf2GCVcA401stbnECef8NwjyNO3tENHguVf4tOFcjuRhCryaYMR9";
                v1.SourceSystemId = "4G3U5EpauTT4aS9z53vrvH0";
                v1.LastUpdateTime = DateTime.Parse("2026-11-08 14:15:53");
                v2.CreatePerson = "bCIDEHwsvgRqP2mSp13tSyGHXuVX8I2f44TMCttyChrPxWhqQ";
                v2.OrganizationId = v1.OrganizationId; 
                v2.BusinessDate = DateTime.Parse("2024-10-17 14:15:53");
                v2.SubmitDate = DateTime.Parse("2025-12-31 14:15:53");
                v2.DocNo = "V7gwcZhf1dYAHg";
                v2.DocType = "eQlUsYjWtjBZ9vDQ9S2w2kBfOFVFQV3r4JXNhLdq44j";
                v2.Status = WMS.Model.ProductionIssueStatusEnum.PartShipped;
                v2.Memo = "LRFoFzHMrhlOgvXThrI0A8TzplG0Dhp2S8GdtzrDBbVFnsyBIK9wGy3FLOriNOsktkrfas75uim957vxkAHgH4JLAa1UI8W0XT2LR5eWMc6mxsdwzICUip01bM3KI3NVjmym5TVJcezmLFuW913Tqzk8aEyhEK1hwgSc3ThKjpvKPKXH1PPHkbzBRxHmav7XBDXpt3raERvEw7jB7zU2VyTLxreI2lJKQw4kvOFGdHVmpIfPMDaJfCumFM50nFteehM5QNEDW8yQ8vsxXgIDiEcqkanyth0Ds1KDuLuiP6q3JBN16CPOKPcogeCMT90BjFc5iMD2slHQkC6Kn0YSDq6DewodqefMfawiJ7";
                v2.SourceSystemId = "Jb9rnl4OqQHUuK8RjPnqS";
                v2.LastUpdateTime = DateTime.Parse("2025-10-05 14:15:53");
                context.Set<ProductionIssue>().Add(v1);
                context.Set<ProductionIssue>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<ProductionIssue>().Find(v1.ID);
                var data2 = context.Set<ProductionIssue>().Find(v2.ID);
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
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "mQXRif3iJ89YBgWlMqLUfDMNBDNK0yH8B6rflHh1EieJ58Hz8rtbQet34xkorLnlSOwqm9Yd8OlBBNkSPFl8nMa1YJbNkcVSowzmUvcNJBgxM1Kk02nZNoFlHvIZ3aBpqLTV0t3P2KRYsreMyqbfTjK7pZt2jcYCOchhqRROi8kOzUHp2zrwWOFCDq2UkJvhS1Cy5pn7uqVcQY6Y2Nga9bITvCikqEjgbCB7Uz28TpzyoKx0Gj003YhsHUifx8Rt6nqRMGjHezFmCcNWWGy70sTYgL5m1SsqSP9vHt9QK2lf7OzuiTgS4x8h8WJiNKa4C0GbbSI7J19JbNuBXyqL172TEO0MqBYjVgniR6nynHwnaWqBogXpPReqZMhxT85wBT1mGTkDa9Y38mSiY39B480jKlE";
                v.Code = "jskCWPHM7KPXuoVHbEnZPReHuT72COaI0";
                v.Name = "RqIBk70Qsn4VMWdE9bLpG5HUjGndkft1h0FWBZANQqwqL4";
                v.SourceSystemId = "M7yWBnaQcpOwWvm8VsEIL9";
                v.LastUpdateTime = DateTime.Parse("2025-11-19 14:15:53");
                v.CodeAndName = "5UzjHb";
                context.Set<BaseOrganization>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
