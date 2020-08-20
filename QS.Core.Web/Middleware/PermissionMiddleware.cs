using Microsoft.AspNetCore.Http;
using QS.ServiceLayer.Permission;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using QS.Core.Web.Authorization;
using Microsoft.Extensions.Configuration;

namespace QS.Core.Web.Permission
{
    /// <summary>
    /// 权限中间件
    /// </summary>
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IConfiguration _configuration;

        public PermissionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext httpContext)
        {
            if (_configuration["Config:InitModule"] == "1")
            {
                var moduleService = httpContext.RequestServices.GetService<IModuleService>();
                var moduleManager = httpContext.RequestServices.GetService<IModuleManager>();

                await moduleService.CreateModules(moduleManager.GetModules());
            }
            await _next(httpContext);
        }
    }
}
