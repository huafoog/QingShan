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
                //ȫ���쳣
                o.Filters.Add<GlobalExceptionFilter>();
                //ע��ģ����֤��������ȫ��
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
                //�ر�Ĭ��ģ����֤
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


            #region ��̬�ļ�
            //FileHelper.CreateDir("www");
            //FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            //provider.Mappings[".jpg"] = "image/jpeg";
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "www")),
            //    ServeUnknownFileTypes = true,
            //    ContentTypeProvider = provider,
            //    RequestPath = new PathString("/www"),
            //    DefaultContentType = "application/x-msdownload", // ����δʶ���MIME����һ��Ĭ��zֵ
            //});
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "www")), // �ƶ�Ŀ¼
            //    RequestPath = new PathString("/www")
            //});
            #endregion
            //app.UseCors("LimitRequests");
            ////app.UseHttpsRedirection();
            //app.UsePermission();
            //app.UseRouting();
            ////���jwt��֤
            //// UseAuthentication() �� UseRouting֮����ã��Ա�·����Ϣ�����������֤����,
            //// �� UseEndpoints ֮ǰ���ã��Ա��û��ھ��������֤����ܷ����ս��
            //// ��������Ҫ���������֤���û��������м��֮ǰ���� UseAuthentication
            ////��Ȩ
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
