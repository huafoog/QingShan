using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using QS.Attributes;
using QS.Core.ConfigurableOptions;
using QS.DatabaseAccessor;
using QS.Extensions;
using QS.Helper;
using QS.Reflection;
using QS.Core.Web.Filter;
using QS.Core.Web.Permission;
using QS.Core.Web.Services;
using System.IO;

namespace QS.Core.Web
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        public IWebHostEnvironment Env { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInject();
            services.AddConfigurableOptions<DatabaseAccessorSettingsOptions>();
            services.AddControllers(o =>
            {
                //全局异常
                o.Filters.Add<GlobalExceptionFilter>();
                //注册模型验证过滤器到全局
                o.Filters.Add<ApiResponseFilterAttribute>();
            }).AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                }
            ).ConfigureApiBehaviorOptions(option =>
            {
                //关闭默认模型验证
                option.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseApp(options =>
            {
                //options.UseSpecificationDocuments();
            });


            #region 静态文件
            //FileHelper.CreateDir("www");
            //FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            //provider.Mappings[".jpg"] = "image/jpeg";
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "www")),
            //    ServeUnknownFileTypes = true,
            //    ContentTypeProvider = provider,
            //    RequestPath = new PathString("/www"),
            //    DefaultContentType = "application/x-msdownload", // 设置未识别的MIME类型一个默认z值
            //});
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "www")), // 制定目录
            //    RequestPath = new PathString("/www")
            //});
            #endregion
            //app.UseCors("LimitRequests");
            ////app.UseHttpsRedirection();
            //app.UsePermission();
            //app.UseRouting();
            ////添加jwt验证
            //// UseAuthentication() 在 UseRouting之后调用，以便路由信息可用于身份验证决策,
            //// 在 UseEndpoints 之前调用，以便用户在经过身份验证后才能访问终结点
            //// 在依赖于要进行身份验证的用户的所有中间件之前调用 UseAuthentication
            ////授权
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
