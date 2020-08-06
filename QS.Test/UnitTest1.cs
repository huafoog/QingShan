using Microsoft.VisualStudio.TestTools.UnitTesting;
using QS.Core.Entity;
using System.IO;
using System.Reflection;

namespace QS.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string projectPath = (@"D:\code\.net core\QS.Core\");
            //string solutionPath = projectPath;

            string modelFile = Path.Combine(projectPath, @"QS.Core.Web\bin\Debug\netcoreapp3.1\QS.DataLayer.dll");
            byte[] fileData = File.ReadAllBytes(modelFile);
            Assembly assembly = Assembly.Load(fileData);
            var baseType = typeof(IEntity<>);
            var modelTypes = assembly.GetTypes();
            //IEnumerable<Type> modelTypes = assembly.GetTypes();
        }
    }
}
