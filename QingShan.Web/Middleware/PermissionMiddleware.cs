using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QingShan.Core.Web.Authorization;
using QingShan.Services.Permission;
using System.Threading.Tasks;

namespace QingShan.Core.Web.Permission
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
                var moduleService = httpContext.RequestServices.GetService<IPermissionContract>();
                var moduleManager = httpContext.RequestServices.GetService<IModuleManager>();

            }
            await _next(httpContext);
        }
    }
}
