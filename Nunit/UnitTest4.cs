using NUnit.Framework;

namespace DeviNunit
{
    [TestFixture]
    public class UnitTest4
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Category("SmokeTest"), Category("ANA"), Category("ko"), Category("messi")]
        public void FourOne()
        {
            Assert.IsFalse(100 == 10);
        }
        [Test]
        [Category("SmokeTest"), Category("ANA"), Category("ko"), Category("messi")]
        public void Fourtwo()
        {
            Assert.IsFalse(1000 == 10);

        }
    }
}