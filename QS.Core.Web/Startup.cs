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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//注入Http请求上下文
            services.AddCacheService(Configuration);
            services.AddAutoMapper(typeof(AutoMapperConfig));
            services.AddToServices();
            services.AddEFService(Configuration);
            services.AddSwaggerService();
            services.AddAuthorizationService(Configuration);
            services.AddCorsService();
            services.AddControllers(o=> {
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
            
           
            app.UseCors("LimitRequests");
            //app.UseHttpsRedirection();
            app.UsePermission();
            app.UseRouting();
            //添加jwt验证
            app.UseAuthentication();
            // UseAuthentication() 在 UseRouting之后调用，以便路由信息可用于身份验证决策,
            // 在 UseEndpoints 之前调用，以便用户在经过身份验证后才能访问终结点
            // 在依赖于要进行身份验证的用户的所有中间件之前调用 UseAuthentication
            //授权
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //exists 应用了路由必须与区域匹配的约束。 使用 {area:...} 是将路由添加到区域的最简单的机制。
                //添加到启动的区域路由：
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
