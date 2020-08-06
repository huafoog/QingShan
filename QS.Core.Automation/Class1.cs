using Microsoft.VisualStudio.TestTools.UnitTesting;
using QS.Core.Entity;
using System.IO;
using System.Reflection;

namespace QS.Core.Automation
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void Test()
        {
            string currentPath = Path.GetDirectoryName("");
            string projectPath = currentPath.Substring(0, currentPath.IndexOf(@"\QS.Core.Automation\ServiceLayer"));
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
