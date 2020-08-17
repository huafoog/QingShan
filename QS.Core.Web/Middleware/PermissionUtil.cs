using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Extensions;
using QS.ServiceLayer.Permission;
using QS.ServiceLayer.Permission.Dto;
using QS.ServiceLayer.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QS.Core.Web.Permission
{

    public static class Permission
    {
        /// <summary>
        /// 使用权限收集
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePermission(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PermissionMiddleware>();
        }
    }
}
