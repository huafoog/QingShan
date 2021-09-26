using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using QingShan.Utilities;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
        [TestMethod]
        public void TestMethod2()
        {
            object obj = new
            {
                a = "1234534",
                b = "346456456",
                c = "3445346346"
            };
            var buff = obj.ToBytes();
            System.Console.WriteLine(); 
        }
        
    }
}
