using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod ,TestCategory("SmokeTest"), TestCategory("ANA")]
        public void fourone()
        {
            Assert.IsFalse(100 == 10);
        }
        [TestMethod ,TestCategory("ANA"), TestCategory("SmokeTest")]
        public void fourtwo()
        {
            Assert.IsFalse(1000 == 10);
        }
    }
}