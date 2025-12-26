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
    public class U9ApiHelperTests
    {
        [TestMethod()]
        public void GetUnitsTest()
        {
            U9ApiHelper api = new U9ApiHelper("http://192.168.250.20/U9", "2504");
            var rr = api.GetUnits("",null);
            Assert.AreEqual(rr.Success, true);
        }
    }
}