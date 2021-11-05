using Microsoft.VisualStudio.TestTools.UnitTesting;
using QingShan.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Test.Utilities.DEncrypt
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class MySecurityTest
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void MD5_Test()
        {
            var result = MySecurity.MD5(MySecurity.MD5Lower("123456"));
            Console.WriteLine(result);
        }
    }
}
