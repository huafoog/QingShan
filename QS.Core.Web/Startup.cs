using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using QS.Core.AutoMapper;
using QS.Core.Extensions;
using QS.Core.Reflection;
using QS.Core.Web.Permission;
using QS.DataLayer.Entities;
using QS.Permission;
using QS.ServiceLayer.ProductService;

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
            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.AddToServices();

            //使用AddDbContext这个Extension method为MyContext在Container中进行注册，它默认的生命周期使是Scoped。
            //Scoped的生命周期为单次http请求唯一
            services.AddDbContext<EFContext>(o => 
                {
                    var db = Configuration["DB:UseDB"];
                    switch (db)
                    {
                        case "SqlServer":
                            o.UseSqlServer(Configuration["DB:SqlServer:ConnectionString"]);
                            break;
                        case "MySql":
                            o.UseMySql(Configuration["DB:MySql:ConnectionString"]);
                            break;
                        default:
                            o.UseSqlServer(Configuration["DB:SqlServer:ConnectionString"]);
                            break;
                    }
                } 
            );
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "QS.Core API",
                    Description = "一个简单框架的api",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                //添加读取注释服务
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                var xmlPath2 = Path.Combine(AppContext.BaseDirectory, "QS.ServiceLayer.xml");
                c.IncludeXmlComments(xmlPath2);
            });

            #region 授权

            //添加jwt验证：
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy => policy.Requirements.Add(new PolicyRequirement()));
            }).AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = Configuration["Audience:Audience"],//Audience
                        ValidIssuer = Configuration["Audience:Issuer"],//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Audience:Secret"]))//拿到SecurityKey
                    };
                });
            //注入授权Handler
            services.AddSingleton<IAuthorizationHandler, PolicyHandler>();
            #endregion

            #region CORS
            //跨域第一种方法，先注入服务，声明策略，然后再下边app中配置开启中间件
            services.AddCors(c =>
            {
                c.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins("http://127.0.0.1:1818", "http://localhost:8080", "http://localhost:8021", "http://localhost:8081", "http://localhost:1818")//支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });

            // 这是第二种注入跨域服务的方法，这里有歧义，部分读者可能没看懂，请看下边解释
            //services.AddCors();
            #endregion

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IFunctionService functionService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
           
            app.UseCors();
            app.UseHttpsRedirection();
            app.UsePermission(functionService);
            app.UseRouting();
            //添加jwt验证
            app.UseAuthentication();

            //授权
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //exists 应用了路由必须与区域匹配的约束。 使用 {area:...} 是将路由添加到区域的最简单的机制。
                //添加到启动的区域路由：
                endpoints.MapAreaControllerRoute(name: "areas", "Admin",pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Test}/{action=Get}/{id?}");
            });
        }
    }
}
