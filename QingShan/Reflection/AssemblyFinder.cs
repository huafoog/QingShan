using QingShan.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace QingShan.Reflection
{
    /// <summary>
    /// 应用程序目录程序集查找
    /// </summary>
    public class AssemblyFinder : IAssemblyFinder
    {
        public Assembly[] Find(Func<Assembly, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        public Assembly[] FindAll()
        {
            return FindAllItems();
        }

        /// <summary>
        /// 重写以实现程序集的查找
        /// </summary>
        /// <returns></returns>
        protected Assembly[] FindAllItems()
        {
            return RuntimeHelper.GetAllAssemblies().ToArray();
        }
        private static Assembly[] LoadFiles(IEnumerable<string> files)
        {
            List<Assembly> assemblies = new List<Assembly>();
            foreach (string file in files)
            {
                AssemblyName name = new AssemblyName(file);
                try
                {
                    assemblies.Add(Assembly.Load(name));
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies.ToArray();
        }
    }
}
