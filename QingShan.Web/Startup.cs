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
using QingShan.Attributes;
using QingShan.Core.ConfigurableOptions;
using QingShan.DatabaseAccessor;
using QingShan.Utilities;
using QingShan.Reflection;
using QingShan.Core.Web.Filter;
using QingShan.Core.Web.Permission;
using QingShan.Core.Web.Services;
using System.IO;
using System;

namespace QingShan.Core.Web
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
            services.AddStaticFile();
            services.AddJwt();
            services.AddCorsAccessor();
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseApp(options =>
            {
                app.UseSpecificationDocuments();
            });
            app.UseStaticFile();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
