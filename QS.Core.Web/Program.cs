using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace QS.Core.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging((hostingContext, builder) =>
            {
                //���˵�ϵͳĬ�ϵ�һЩ��־
                builder.AddFilter("System", LogLevel.Error);
                builder.AddFilter("Microsoft", LogLevel.Error);
                //builder.AddFilter("Blog.Core.AuthHelper.ApiResponseHandler", LogLevel.Error);

                //�������ļ�
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config");
                builder.AddLog4Net(path);
            }).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
