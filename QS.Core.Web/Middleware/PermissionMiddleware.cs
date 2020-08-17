using Microsoft.AspNetCore.Http;
using QS.ServiceLayer.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using QS.Core.Web.Authorization;

namespace QS.Core.Web.Permission
{
    /// <summary>
    /// 权限中间件
    /// </summary>
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext httpContext)
        {
            var moduleService = httpContext.RequestServices.GetService<IModuleService>();
            var moduleManager = httpContext.RequestServices.GetService<IModuleManager>();

            await moduleService.CreateModules(moduleManager.GetModules());

            //var functions = functionService.PickupFunctions();
            //await permissionService.UpdatePermissionAsync(moduleInfos);
            await _next(httpContext);
        }
    }
}
