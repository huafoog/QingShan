using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Test
{

    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// 测试
        /// </summary>
        [TestMethod]
        public void LineToCamelCaseTest()
        {
            var str = "asfsf_gfhrty_tyjyti_ryeret_werwer_qwe";
            Console.WriteLine("QingShan".LineToCamelCase());
            Console.WriteLine(str.LineToCamelCase());
            Console.WriteLine("qing_shan".LineToCamelCase());
            Assert.AreEqual("QingShan","qing_shan".LineToCamelCase());
            Assert.AreEqual("Qing", "qing".LineToCamelCase());
            //Assert.AreEqual("QingShan", "QingShan".LineToCamelCase());
        }
        /// <summary>
        /// 测试
        /// </summary>
        [TestMethod]
        public void Test1()
        {
            var str = "CreateTime";


            Console.WriteLine(!(str == "CreateTime" || str == "DeleteTime" || str == "CreatedId" || str == "Id"));
        }

        [TestMethod]
        public void Test2()
        {
            var services = new ServiceCollection();
            services.AddDependencyInjection();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (IServiceScope serviceScope = serviceProvider.CreateScope())
            {
                //var personOne = serviceScope.ServiceProvider.GetService<Person>();
                //Console.WriteLine(person.Name);
            }
        }
    }
}
