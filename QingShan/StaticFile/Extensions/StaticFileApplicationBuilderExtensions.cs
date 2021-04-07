using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QingShan.StaticFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using QingShan.Utilities;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.AspNetCore.Builder
{
    public static class StaticFileApplicationBuilderExtensions
    {
        /// <summary>
        /// 添加静态文件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStaticFile(this IApplicationBuilder app)
        {
            var pro = app.ApplicationServices;
            var staticFileSettings = pro.GetService<IOptions<StaticFileSettingsOption>>().Value;
            //var env = pro.GetService<IWebHostEnvironment>();
            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (staticFileSettings.StaticFileFolder?.Length > 0)
            {
                FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
                if (staticFileSettings.StaticFileMap != null)
                {
                    foreach (var map in staticFileSettings.StaticFileMap)
                    {
                        provider.Mappings[map.Suffix] = map.FileType;
                    }
                }
                foreach (var folder in staticFileSettings.StaticFileFolder)
                {
                    FileHelper.CreateDirectory(folder.Folder, path);
                    var requestPath = folder.RequestPath;

                    app.UseStaticFiles(new StaticFileOptions
                    {

                        FileProvider = new PhysicalFileProvider(Path.Combine(path, folder.Folder)),
                        ServeUnknownFileTypes = folder.ServeUnknownFileTypes,
                        ContentTypeProvider = provider,
                        RequestPath = requestPath,
                        DefaultContentType = folder.DefaultContentType, // 设置未识别的MIME类型一个默认z值
                    });
                    if (staticFileSettings.UseDirectoryBrowser)
                    {
                        app.UseDirectoryBrowser(new DirectoryBrowserOptions()
                        {
                            FileProvider = new PhysicalFileProvider(Path.Combine(path, folder.Folder)), // 制定目录
                            RequestPath = new PathString(requestPath)
                        });
                    }
                }
            }
            return app;
        }
    }
}
