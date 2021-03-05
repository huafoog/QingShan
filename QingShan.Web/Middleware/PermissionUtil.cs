using Microsoft.AspNetCore.Builder;

namespace QingShan.Core.Web.Permission
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
