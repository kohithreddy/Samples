using NUnit.Framework;

namespace NunitTest
{
    [TestFixture]
    public class UnitTest1
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Category("SmokeTest"), Category("ANA"), Category("ko"), Category("messi")]
        public void OneOne()
        {
            Assert.IsTrue(10 == 10);
        }
        [Test]
        [Category("SmokeTest"), Category("ANA"), Category("ko"), Category("messi")]
        public void onetwo()
        {
            Assert.IsTrue(100 == 100);
        }
    }
}
