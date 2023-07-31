using NUnit.Framework;

namespace PriorityTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        [Order(1)]
        public void alpha()
        {
            Console.WriteLine("alpha test case, order 1");
            Assert.Pass();
        }
        [Test]
        [Order(3)]
        public void beta()
        {
            Console.WriteLine("beta test case, order 3");
            Assert.Pass();
        }

        [Test]
        [Order(2)]
        public void gama()
        {
            Console.WriteLine("game test case, order 2");
            Assert.Pass();
        }

    }
}