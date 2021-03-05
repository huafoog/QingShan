﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using QingShan.Permission;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QingShan.Attributes.Permission
{
    /// <summary>
    /// 启用权限
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute : AuthorizeAttribute, IAuthorizationFilter, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="context"></param>
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            //排除匿名访问
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute)))
            {
                return;
            }
            //登录验证
            var user = context.HttpContext.RequestServices.GetService<IUserInfo>();
            if (user == null || !(user?.Id > 0))
            {
                context.Result = new ChallengeResult();
                return;
            }
            //排除登录接口
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(LoggedInAttribute)))
            {
                return;
            }

            //权限验证
            var httpMethod = context.HttpContext.Request.Method;
            var api = context.ActionDescriptor.AttributeRouteInfo.Template;
            //var permissionHandler = context.HttpContext.RequestServices.GetService<IPermissionHandler>();
            //var isValid = await permissionHandler.ValidateAsync(api, httpMethod);
            //if (!isValid)
            //{
            //    context.Result = new ForbidResult();
            //}
            await Task.CompletedTask;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            OnAuthorization(context);
            return Task.CompletedTask;
        }
    }
}
