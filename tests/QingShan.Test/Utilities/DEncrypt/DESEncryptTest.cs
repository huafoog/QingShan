using Microsoft.VisualStudio.TestTools.UnitTesting;
using QingShan.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Test.Utilities.DEncrypt
{
    [TestClass]
    public class DESEncryptTest
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Encrypt_Test()
        {
            var result = DESEncrypt.Encrypt("张三丰");
            //Console.WriteLine(result);
            Assert.AreEqual(result, "Wmp7NDhI5S/U/H0qf0YWBw==");
        }
        [TestMethod]
        public void Decrypt_Test()
        {
            var result = DESEncrypt.Decrypt("Wmp7NDhI5S/U/H0qf0YWBw==");
            //Console.WriteLine(result);
            Assert.AreEqual(result, "张三丰");
        }
    }
}
