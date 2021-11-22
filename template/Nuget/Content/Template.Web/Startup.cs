using Template.Web.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using QingShan.Attributes;
using QingShan.Core.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Web
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
            services.AddConfigurable(Configuration);
            // 添加 HttContext 访问器
            services.AddHttpContextAccessor();
            // 注册全局依赖注入
            services.AddDependencyInjection();
            // 文档
            services.AddSpecificationDocuments();
            // 数据访问
            services.AddDatabaseAccessor();
            // 静态资源
            services.AddStaticFile();
            // Cors跨域
            services.AddCorsAccessor();
            // jwt授权
            services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
            // 控制器
            services.AddDefaultController();
            #region AddApp中注入的服务 
            //
            ////可单独使用AddApp注入所有服务
            //services.AddApp<JwtHandler>(Configuration);


            //services.AddConfigurable(Configuration);
            //// 添加 HttContext 访问器
            //services.AddHttpContextAccessor();
            //// 注册全局依赖注入
            //services.AddDependencyInjection();
            //// 文档
            //services.AddSpecificationDocuments();
            //// 缓存
            //services.AddCache();
            //// 数据访问
            //services.AddDatabaseAccessor();
            //// 静态资源
            //services.AddStaticFile();
            //// 限流
            //services.AddRateLimit();
            //// Cors跨域
            //services.AddCorsAccessor();
            //// jwt授权
            //services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
            //// 控制器
            //services.AddDefaultController();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ////IP限流  注入AddApp() 或者 AddRateLimit()时使用
            //app.UseRateLimit();
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
