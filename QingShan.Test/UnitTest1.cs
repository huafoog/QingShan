using Microsoft.VisualStudio.TestTools.UnitTesting;
using QingShan.Utilities;

namespace QingShan.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            for (int i = 0; i < 1000; i++)
            {
                System.Console.WriteLine(Snowflake.GenId());
            }
            System.Console.WriteLine("123");
        }
    }
}
