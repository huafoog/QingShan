using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.AddToServices();

            //ʹ��AddDbContext���Extension methodΪMyContext��Container�н���ע�ᣬ��Ĭ�ϵ���������ʹ��Scoped��
            //Scoped����������Ϊ����http����Ψһ
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
            services.AddControllers();
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "QS.Core API",
                    Description = "һ���򵥿�ܵ�api",
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
                //��Ӷ�ȡע�ͷ���
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                var xmlPath2 = Path.Combine(AppContext.BaseDirectory, "QS.ServiceLayer.xml");
                c.IncludeXmlComments(xmlPath2);
            });


            #region CORS
            //�����һ�ַ�������ע������������ԣ�Ȼ�����±�app�����ÿ����м��
            services.AddCors(c =>
            {
                c.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins("http://127.0.0.1:1818", "http://localhost:8080", "http://localhost:8021", "http://localhost:8081", "http://localhost:1818")//֧�ֶ�������˿ڣ�ע��˿ںź�Ҫ��/б�ˣ�����localhost:8000/���Ǵ��
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });

            // ���ǵڶ���ע��������ķ��������������壬���ֶ��߿���û�������뿴�±߽���
            //services.AddCors();
            #endregion

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //exists Ӧ����·�ɱ���������ƥ���Լ���� ʹ�� {area:...} �ǽ�·����ӵ��������򵥵Ļ��ơ�
                //��ӵ�����������·�ɣ�
                endpoints.MapAreaControllerRoute(name: "areas", "Admin",pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Test}/{action=Get}/{id?}");
            });
        }
    }
}
