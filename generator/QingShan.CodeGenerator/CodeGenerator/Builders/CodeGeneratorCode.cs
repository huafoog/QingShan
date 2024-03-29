﻿using RazorEngineCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.CodeGenerator.Builders
{
    public static class CodeGeneratorCode
    {
        public static ConcurrentDictionary<string, IRazorEngineCompiledTemplate> TemplateCache = new ConcurrentDictionary<string, IRazorEngineCompiledTemplate>();

        public static ConcurrentDictionary<string, IRazorEngineCompiledTemplate> PagetemplateCache = new ConcurrentDictionary<string, IRazorEngineCompiledTemplate>();

        public static void Init()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string templateDir = Path.Combine(baseDir, "CodeGenerator/Templates");
            var files = Directory.GetFiles(templateDir, "*", SearchOption.AllDirectories);
            IRazorEngine razorEngine = new RazorEngine();
            foreach (var item in files)
            {
                IRazorEngineCompiledTemplate template = razorEngine.Compile(File.ReadAllText(item));
                TemplateCache.TryAdd(Path.GetFileNameWithoutExtension(item), template);
            }
            var filePages = Directory.GetFiles(Path.Combine(baseDir, "CodeGenerator/Pages"), "*", SearchOption.AllDirectories);
            foreach (var item in filePages)
            {
                IRazorEngineCompiledTemplate template = razorEngine.Compile(File.ReadAllText(item));
                PagetemplateCache.TryAdd(Path.GetFileNameWithoutExtension(item), template);
            }
        }
    }
}
