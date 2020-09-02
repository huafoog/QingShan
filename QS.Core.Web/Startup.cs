using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using QS.Core.Attributes;
using QS.Core.AutoMapper;
using QS.Core.Extensions;
using QS.Core.Reflection;
using QS.Core.Web.Filter;
using QS.Core.Web.Permission;
using QS.Core.Web.Services;

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

            services.AddSingleton<IAssemblyFinder, AssemblyFinder>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//ע��Http����������
            services.AddCacheService(Configuration);
            services.AddAutoMapper(typeof(AutoMapperConfig));
            services.AddToServices();
            services.AddEFService(Configuration);
            services.AddSwaggerService();
            services.AddAuthorizationService(Configuration);
            services.AddCorsService();
            services.AddControllers(o=> {
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
            
           
            app.UseCors("LimitRequests");
            //app.UseHttpsRedirection();
            app.UsePermission();
            app.UseRouting();
            //���jwt��֤
            app.UseAuthentication();
            // UseAuthentication() �� UseRouting֮����ã��Ա�·����Ϣ�����������֤����,
            // �� UseEndpoints ֮ǰ���ã��Ա��û��ھ��������֤����ܷ����ս��
            // ��������Ҫ���������֤���û��������м��֮ǰ���� UseAuthentication
            //��Ȩ
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //exists Ӧ����·�ɱ���������ƥ���Լ���� ʹ�� {area:...} �ǽ�·����ӵ��������򵥵Ļ��ơ�
                //��ӵ�����������·�ɣ�
                endpoints.MapAreaControllerRoute(name: "areas", "area",pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Test}/{action=Get}/{id?}");
            });
            app.UsePermission();
            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            #endregion
        }
    }
}
