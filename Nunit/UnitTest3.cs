using NUnit.Framework;

namespace DeviNunit
{
    [TestFixture]
    public class UnitTest3
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Category("SmokeTest"), Category("ANA"), Category("ko"), Category("messi")]
        public void ThreeOne()
        {
            Assert.IsTrue(10 == 10);
        }
        [Test]
        [Category("SmokeTest"), Category("ANA"), Category("ko"), Category("messi")]
        public void Threetwo()
        {
            Assert.IsTrue(100 == 100);
        }
    }
}