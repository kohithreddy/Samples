using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod, TestCategory("ANA"), TestCategory("SmokeTest")]
        public void twoone()
        {
            Assert.IsFalse(100 == 10);
        }
        [TestMethod, TestCategory("SmokeTest"), TestCategory("ANA")]
        public void twotwo()
        {
            Assert.IsFalse(1000 == 10);
        }
    }
}