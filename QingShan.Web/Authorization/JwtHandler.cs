﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QingShan.Authorization;
using QingShan.Core.JWT;
using QingShan.Permission;
using QingShan.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using QingShan.Data.Constants;
using System.Security.Claims;
using QingShan.Services.Permission;

namespace QingShan.Web.Authorization
{
    public class JwtHandler : AppAuthorizeHandler
    {
        /// <summary>
        /// 重写 Handler 添加自动刷新收取逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task HandleAsync(AuthorizationHandlerContext context)
        {
            // 自动刷新 token
            //if (JWTEncryption.AutoRefreshToken(context, context.GetCurrentHttpContext()))
            //{
            await AuthorizeHandleAsync(context);
            //}
            //else
            //{
            //    context.Fail();
            //}    // 授权失败
        }

        /// <summary>
        /// 验证管道，也就是验证核心代码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {

            var serviceProvider = context.GetCurrentHttpContext().Request.HttpContext.RequestServices;
            var _permissionContract = serviceProvider.GetService<IPermissionContract>();
            // 检查权限，如果方法时异步的就不用 Task.FromResult 包裹，直接使用 async/await 即可
            var check = await _permissionContract.CheckPermission();
            if (!check)
            {
                return false;
            }
            return true;
        }
    }
}
