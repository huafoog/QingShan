using Microsoft.AspNetCore.Http;
using QS.ServiceLayer.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

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
            //var functionService = httpContext.RequestServices.GetService<IFunctionService>();
            //var permissionService = httpContext.RequestServices.GetService<IPermissionService>();


            //var functions = functionService.PickupFunctions();
            //var moduleInfos = functionService.Pickup();
            //await permissionService.UpdatePermissionAsync(moduleInfos);
            await _next(httpContext);
        }
    }
}
