using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using QingShan.Attributes;
using QingShan.Core.Filter;
using QingShan.Web.Authorization;

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
            //注入配置信息
            services.AddConfigurable(Configuration);
            // 添加 HttContext 访问器
            services.AddHttpContextAccessor();
            // 注册全局依赖注入
            services.AddDependencyInjection();
            services.AddSpecificationDocuments();
            services.AddCache();
            services.AddDatabaseAccessor();
            services.AddStaticFile();
            services.AddRateLimit();
            services.AddCorsAccessor();
            services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
            services.AddDefaultController();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseRateLimit();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSpecificationDocuments();
            app.UseCorsAccessor();
            app.UseStaticFile();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
