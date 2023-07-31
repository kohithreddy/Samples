using NUnit.Framework;

namespace NunitTest
{
    [TestFixture]
    public class UnitTest2
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Category("SmokeTest"), Category("ANA"), Category("ko"), Category("messi")]
        public void TwoOne()
        {
            Assert.IsFalse(100 == 10);
        }
        [Test]
        [Category("SmokeTest"), Category("ANA"), Category("ko"), Category("messi")]
        public void Twotwo()
        {
            Assert.IsFalse(1000 == 10);
        }
    }
}
