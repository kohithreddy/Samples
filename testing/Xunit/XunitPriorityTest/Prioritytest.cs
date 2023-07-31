namespace XunitPriorityTest
{
    public class Prioritytest
    {
        [Fact]
        [Trait("Category", "Priority1")]
        public void XUnit_alpha()
        {
            Console.WriteLine("alpha test case, order 1");
            Assert.True(true);
        }

        [Fact]
        [Trait("Category", "Priority3")]
        public void XUnit_beta()
        {
            Console.WriteLine("beta test case, order 3");
            Assert.True(true);
        }

        [Fact]
        [Trait("Category", "Priority2")]
        public void XUnit_gama()
        {
            Console.WriteLine("game test case, order 2");
            Assert.True(true);
        }
    }
}