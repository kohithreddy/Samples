using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod, TestCategory("SmokeTest"), TestCategory("ANA")]
        public void oneone()
        {
            Assert.IsTrue(10==10);            
        }
        [TestMethod, TestCategory("ANA"), TestCategory("SmokeTest")]
        public void onetwo()
        {
            Assert.IsTrue(100 == 100);

        }
    }
}
