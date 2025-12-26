using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs;
using WMS.Model.InventoryManagement;
using WMS.DataAccess;


namespace WMS.Test
{
    [TestClass]
    public class InventoryUnfreezeApiTest
    {
        private InventoryUnfreezeApiController _controller;
        private string _seed;

        public InventoryUnfreezeApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<InventoryUnfreezeApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new InventoryUnfreezeApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            InventoryUnfreezeApiVM vm = _controller.Wtm.CreateVM<InventoryUnfreezeApiVM>();
            InventoryUnfreeze v = new InventoryUnfreeze();
            
            v.DocNo = "de5FWAdMIzjcrTSB21OHoyRPFy63Y6rOiVrXIZnUveCOxBvtc";
            v.Reason = "p97DVjMHdMckpKO9vsMyF9QJK4jlynVJXbxFS7Sn3i9tdwyx3cR6QzlFXoi9urfq7ObuduuqRyf7dh2oDPBqAwxY4yD88ISRYZZ3jiakMyvoyiDbAmNytUCt73RGMPSXj0c6E6HiJkD8B4wrDtMpqWFsQrOlLN3mZ3KpNafL7kMLSk428vw0NMsjrsjLgspbIn8cNTfSh07HnzhgPucV7ELg0iCCUtlaX6rXce9eDLdB3xxhFblKptfvdfBGl";
            v.Memo = "PkS1mc0MiQI2bDD2lbzQq49Nae1JNbaNKJln8o6W9Ni9qtRJb0F5U79hNhYunNnKeVz0g1YW0rSUpncEG1OECAiKuxcug4IMqpA5AV4witrvBv4QE51JWDT";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryUnfreeze>().Find(v.ID);
                
                Assert.AreEqual(data.DocNo, "de5FWAdMIzjcrTSB21OHoyRPFy63Y6rOiVrXIZnUveCOxBvtc");
                Assert.AreEqual(data.Reason, "p97DVjMHdMckpKO9vsMyF9QJK4jlynVJXbxFS7Sn3i9tdwyx3cR6QzlFXoi9urfq7ObuduuqRyf7dh2oDPBqAwxY4yD88ISRYZZ3jiakMyvoyiDbAmNytUCt73RGMPSXj0c6E6HiJkD8B4wrDtMpqWFsQrOlLN3mZ3KpNafL7kMLSk428vw0NMsjrsjLgspbIn8cNTfSh07HnzhgPucV7ELg0iCCUtlaX6rXce9eDLdB3xxhFblKptfvdfBGl");
                Assert.AreEqual(data.Memo, "PkS1mc0MiQI2bDD2lbzQq49Nae1JNbaNKJln8o6W9Ni9qtRJb0F5U79hNhYunNnKeVz0g1YW0rSUpncEG1OECAiKuxcug4IMqpA5AV4witrvBv4QE51JWDT");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            InventoryUnfreeze v = new InventoryUnfreeze();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DocNo = "de5FWAdMIzjcrTSB21OHoyRPFy63Y6rOiVrXIZnUveCOxBvtc";
                v.Reason = "p97DVjMHdMckpKO9vsMyF9QJK4jlynVJXbxFS7Sn3i9tdwyx3cR6QzlFXoi9urfq7ObuduuqRyf7dh2oDPBqAwxY4yD88ISRYZZ3jiakMyvoyiDbAmNytUCt73RGMPSXj0c6E6HiJkD8B4wrDtMpqWFsQrOlLN3mZ3KpNafL7kMLSk428vw0NMsjrsjLgspbIn8cNTfSh07HnzhgPucV7ELg0iCCUtlaX6rXce9eDLdB3xxhFblKptfvdfBGl";
                v.Memo = "PkS1mc0MiQI2bDD2lbzQq49Nae1JNbaNKJln8o6W9Ni9qtRJb0F5U79hNhYunNnKeVz0g1YW0rSUpncEG1OECAiKuxcug4IMqpA5AV4witrvBv4QE51JWDT";
                context.Set<InventoryUnfreeze>().Add(v);
                context.SaveChanges();
            }

            InventoryUnfreezeApiVM vm = _controller.Wtm.CreateVM<InventoryUnfreezeApiVM>();
            var oldID = v.ID;
            v = new InventoryUnfreeze();
            v.ID = oldID;
       		
            v.DocNo = "byOct2IzV1v9FAd72ttxfIt2Bmzi0cYk0MNRQn";
            v.Reason = "w7zYViuA8lQj929MI3zcb7tTfFxHLZl0d5Amrl2nRL79O5RvdEc0EttfXhBhxidD1urDevq5zo4QSqAmRV0c29reFwG95kXQq5Q1mSr4oc7rMYFC88Y8s59exa2KALvqrclHUyadOhwYUi68rdyMljCvuMTcFZv3rzsh";
            v.Memo = "wHqzIFi8eh7yqYoILNdAuca7jEYN4ItrCVpUe5BhTaSLy51rx0cLn3iPpiUTOe5XUy7OfI4l9XXQ6p65GjYD7DjlS9fWS9UTnXV2eCnmaP8P1gqc6oU5APjJUjanB815bZOSPCFRkDAh2kckMbeJWctdFQLinA48kONt8PR24OziUHWjwdeuQmxp3fOmP51PyWc36A3AtyC3AtqcAWaFHCcggqZ9sxp0eoxS3aFFF51QF2kgHCqZ";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.Reason", "");
            vm.FC.Add("Entity.Memo", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<InventoryUnfreeze>().Find(v.ID);
 				
                Assert.AreEqual(data.DocNo, "byOct2IzV1v9FAd72ttxfIt2Bmzi0cYk0MNRQn");
                Assert.AreEqual(data.Reason, "w7zYViuA8lQj929MI3zcb7tTfFxHLZl0d5Amrl2nRL79O5RvdEc0EttfXhBhxidD1urDevq5zo4QSqAmRV0c29reFwG95kXQq5Q1mSr4oc7rMYFC88Y8s59exa2KALvqrclHUyadOhwYUi68rdyMljCvuMTcFZv3rzsh");
                Assert.AreEqual(data.Memo, "wHqzIFi8eh7yqYoILNdAuca7jEYN4ItrCVpUe5BhTaSLy51rx0cLn3iPpiUTOe5XUy7OfI4l9XXQ6p65GjYD7DjlS9fWS9UTnXV2eCnmaP8P1gqc6oU5APjJUjanB815bZOSPCFRkDAh2kckMbeJWctdFQLinA48kONt8PR24OziUHWjwdeuQmxp3fOmP51PyWc36A3AtyC3AtqcAWaFHCcggqZ9sxp0eoxS3aFFF51QF2kgHCqZ");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            InventoryUnfreeze v = new InventoryUnfreeze();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DocNo = "de5FWAdMIzjcrTSB21OHoyRPFy63Y6rOiVrXIZnUveCOxBvtc";
                v.Reason = "p97DVjMHdMckpKO9vsMyF9QJK4jlynVJXbxFS7Sn3i9tdwyx3cR6QzlFXoi9urfq7ObuduuqRyf7dh2oDPBqAwxY4yD88ISRYZZ3jiakMyvoyiDbAmNytUCt73RGMPSXj0c6E6HiJkD8B4wrDtMpqWFsQrOlLN3mZ3KpNafL7kMLSk428vw0NMsjrsjLgspbIn8cNTfSh07HnzhgPucV7ELg0iCCUtlaX6rXce9eDLdB3xxhFblKptfvdfBGl";
                v.Memo = "PkS1mc0MiQI2bDD2lbzQq49Nae1JNbaNKJln8o6W9Ni9qtRJb0F5U79hNhYunNnKeVz0g1YW0rSUpncEG1OECAiKuxcug4IMqpA5AV4witrvBv4QE51JWDT";
                context.Set<InventoryUnfreeze>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            InventoryUnfreeze v1 = new InventoryUnfreeze();
            InventoryUnfreeze v2 = new InventoryUnfreeze();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DocNo = "de5FWAdMIzjcrTSB21OHoyRPFy63Y6rOiVrXIZnUveCOxBvtc";
                v1.Reason = "p97DVjMHdMckpKO9vsMyF9QJK4jlynVJXbxFS7Sn3i9tdwyx3cR6QzlFXoi9urfq7ObuduuqRyf7dh2oDPBqAwxY4yD88ISRYZZ3jiakMyvoyiDbAmNytUCt73RGMPSXj0c6E6HiJkD8B4wrDtMpqWFsQrOlLN3mZ3KpNafL7kMLSk428vw0NMsjrsjLgspbIn8cNTfSh07HnzhgPucV7ELg0iCCUtlaX6rXce9eDLdB3xxhFblKptfvdfBGl";
                v1.Memo = "PkS1mc0MiQI2bDD2lbzQq49Nae1JNbaNKJln8o6W9Ni9qtRJb0F5U79hNhYunNnKeVz0g1YW0rSUpncEG1OECAiKuxcug4IMqpA5AV4witrvBv4QE51JWDT";
                v2.DocNo = "byOct2IzV1v9FAd72ttxfIt2Bmzi0cYk0MNRQn";
                v2.Reason = "w7zYViuA8lQj929MI3zcb7tTfFxHLZl0d5Amrl2nRL79O5RvdEc0EttfXhBhxidD1urDevq5zo4QSqAmRV0c29reFwG95kXQq5Q1mSr4oc7rMYFC88Y8s59exa2KALvqrclHUyadOhwYUi68rdyMljCvuMTcFZv3rzsh";
                v2.Memo = "wHqzIFi8eh7yqYoILNdAuca7jEYN4ItrCVpUe5BhTaSLy51rx0cLn3iPpiUTOe5XUy7OfI4l9XXQ6p65GjYD7DjlS9fWS9UTnXV2eCnmaP8P1gqc6oU5APjJUjanB815bZOSPCFRkDAh2kckMbeJWctdFQLinA48kONt8PR24OziUHWjwdeuQmxp3fOmP51PyWc36A3AtyC3AtqcAWaFHCcggqZ9sxp0eoxS3aFFF51QF2kgHCqZ";
                context.Set<InventoryUnfreeze>().Add(v1);
                context.Set<InventoryUnfreeze>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<InventoryUnfreeze>().Find(v1.ID);
                var data2 = context.Set<InventoryUnfreeze>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
