using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace QS.Core.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            //try
            //{
                CreateHostBuilder(args).Build().Run();
            //}
            //catch (Exception exception)
            //{
            //    //NLog: catch setup errors
            //    logger.Error(exception, "Stopped program because of exception");
            //    throw;
            //}
            //finally
            //{
            //    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            //    NLog.LogManager.Shutdown();
            //}
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {

                //QS.Core基础功能注入
                webBuilder.Inject()
                .UseStartup<Startup>();
            });
            //.ConfigureLogging(logging =>
            //{
            //    logging.ClearProviders();
            //    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            //})
            //.UseNLog();  // NLog: Setup NLog for Dependency injection;
        }

        //public static IWebHostBuilder CreateHostBuilder(string[] args) =>
        //   WebHost.
        //   CreateDefaultBuilder(args)
        //   .UseStartup<Startup>()
        //   .UseKestrel()
        //   .UseUrls("http://*:9999")
        //   .ConfigureLogging(logging =>
        //   {
        //       logging.ClearProviders();
        //       logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        //   })
        //   .UseNLog();  // NLog: Setup NLog for Dependency injection;
    }
}
