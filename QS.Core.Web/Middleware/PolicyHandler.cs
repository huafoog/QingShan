﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QS.Core.Web.Permission
{
    public class PolicyHandler : AuthorizationHandler<PolicyRequirement>
    {

        private readonly IHttpContextAccessor _httpContext;

        public PolicyHandler(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            ////赋值用户权限
            var userPermissions = requirement.UserPermissions;
            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            var httpContext = _httpContext.HttpContext;
            //请求Url
            var questUrl = httpContext.Request.Path.Value.ToUpperInvariant();
            //是否经过验证
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                if (userPermissions.GroupBy(g => g.Url).Any(w => w.Key.ToUpperInvariant() == questUrl))
                {
                    //用户名
                    var userName = httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier).Value;
                    if (userPermissions.Any(w => w.UserName == userName && w.Url.ToUpperInvariant() == questUrl))
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        //无权限跳转到拒绝页面
                        httpContext.Response.Redirect(requirement.DeniedAction);
                    }
                }
                else
                {
                    context.Succeed(requirement);
                }
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
