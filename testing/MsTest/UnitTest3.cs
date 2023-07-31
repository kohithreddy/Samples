using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest
{

    [TestClass]
    public class UnitTest3
    {
        [TestMethod, TestCategory("SmokeTest"), TestCategory("ANA")]
        public void Threeone()
        {
            Assert.IsTrue(10==10);            
        }
        [TestMethod, TestCategory("ANA"), TestCategory("SmokeTest")]
        public void Threetwo()
        {
            Assert.IsTrue(100 == 100);
        }
    }
}
