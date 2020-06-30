using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QS.Core.Reflection
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
            string[] filters =
                {
                    "mscorlib",
                    "netstandard",
                    "dotnet",
                    "api-ms-win-core",
                    "runtime.",
                    "System",
                    "Microsoft",
                    "Window",
                    "Swashbuckle"
                };
            List<string> names = new List<string>();
            string[] dllNames = DependencyContext.Default.CompileLibraries.SelectMany(m => m.Assemblies).Distinct().Select(m => m.Replace(".dll", ""))
               .OrderBy(m => m).ToArray();
            if (dllNames.Length > 0)
            {
                names = (from name in dllNames
                         let i = name.LastIndexOf('/') + 1
                         select name.Substring(i, name.Length - i)).Distinct()
                    .Where(name => !filters.Any(name.StartsWith))
                    .OrderBy(m => m).ToList();
            }
            return LoadFiles(names);
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
