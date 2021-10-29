using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QingShan.Web.Test.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QingShan;

namespace QingShan.Options.Test
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            QingShanApplication.Configuration = Configuration;
            var models = Configuration.GetDefultOptions<PositionOptions>();
            Console.WriteLine(JsonConvert.SerializeObject(models));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {

                    var data = app.ApplicationServices.GetService<IOptionsMonitor<PositionOptions>>();
                    
                    await Write(context,JsonConvert.SerializeObject(data.CurrentValue));
                });
            });
        }

        private async Task Write(HttpContext context, string message)
        {
            await context.Response.WriteAsync(message);
        }
    }
}
