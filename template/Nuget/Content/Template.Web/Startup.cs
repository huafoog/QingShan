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
            // ��� HttContext ������
            services.AddHttpContextAccessor();
            // ע��ȫ������ע��
            services.AddDependencyInjection();
            // �ĵ�
            services.AddSpecificationDocuments();
            // ���ݷ���
            services.AddDatabaseAccessor();
            // ��̬��Դ
            services.AddStaticFile();
            // Cors����
            services.AddCorsAccessor();
            // jwt��Ȩ
            services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
            // ������
            services.AddDefaultController();
            #region AddApp��ע��ķ��� 
            //
            ////�ɵ���ʹ��AddAppע�����з���
            //services.AddApp<JwtHandler>(Configuration);


            //services.AddConfigurable(Configuration);
            //// ��� HttContext ������
            //services.AddHttpContextAccessor();
            //// ע��ȫ������ע��
            //services.AddDependencyInjection();
            //// �ĵ�
            //services.AddSpecificationDocuments();
            //// ����
            //services.AddCache();
            //// ���ݷ���
            //services.AddDatabaseAccessor();
            //// ��̬��Դ
            //services.AddStaticFile();
            //// ����
            //services.AddRateLimit();
            //// Cors����
            //services.AddCorsAccessor();
            //// jwt��Ȩ
            //services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
            //// ������
            //services.AddDefaultController();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ////IP����  ע��AddApp() ���� AddRateLimit()ʱʹ��
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
