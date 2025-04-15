using SampleCLRComp;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            string name = "World";
            string result = SampleUnmanagedClass.CallDotNetCore(name);
            Assert.AreEqual("Hello, World", result);

        }

        [TestMethod]
        public void TestMethod2()
        {
            
        }

    }
}