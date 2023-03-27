using JinianNet.JNTemplate;
using QingShan.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HuaFoog.CodeGenerator.CodeGenerator.Builders
{
    public static class CodeGeneratorBuilder
    {
        public static ConcurrentDictionary<string, string> TemplateCache = new ConcurrentDictionary<string, string>();
        public static ConcurrentDictionary<string, string> SuffixCache = new ConcurrentDictionary<string, string>();

        public static void CreateTemplate()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string templateDir = Path.Combine(baseDir, "CodeGenerator/Templates");
            var files = Directory.GetFiles(templateDir, "*", SearchOption.AllDirectories);
            if (files != null)
            {
                foreach (var item in files)
                {
                    var fileText = File.ReadAllText(item);
                    TemplateCache.TryAdd(Path.GetFileNameWithoutExtension(item), fileText);
                    SuffixCache.TryAdd(Path.GetFileNameWithoutExtension(item), Path.GetExtension(item));
                }
            }

        }
    }
}
