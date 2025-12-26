using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util.Tests
{
    [TestClass()]
    public class CommonTests
    {
        [TestMethod()]
        public void GetRandom13Test()
        {
            string random13 = Common.GetRandom13();
            Assert.IsTrue(random13.Length == 13);
        }
    }
}