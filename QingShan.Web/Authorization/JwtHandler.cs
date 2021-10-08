using Microsoft.AspNetCore.Authorization;
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
using QingShan.Data;
using Newtonsoft.Json;
using QingShan.Core;

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
            // 判断请求报文头中是否有 "Authorization" 报文头
            var bearerToken = context.GetCurrentHttpContext().Request.Headers["Authorization"].ToString();
            if (bearerToken.NotNull())
            {
                await AuthorizeHandleAsync(context);
            }
            else
            {
                context.Fail();
            }


            //// 自动刷新 token
            //if (JWTEncryption.AutoRefreshToken(context, context.GetCurrentHttpContext()))
            //{
            //    await AuthorizeHandleAsync(context);
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

            // 管理员跳过判断
            var user = serviceProvider.GetService<IUserInfo>();
            if (user.IsSuper) return true;

            // 路由名称
            var routeName = httpContext.Request.Path.Value.Substring(1).Replace("/", ".");


            if (App.LoggedCodes.Contains(routeName.ToLower()))
            {
                //当前权限只需要登录
                return true;
            }


            var _permissionContract = serviceProvider.GetService<IPermissionContract>();
            // 检查权限，如果方法时异步的就不用 Task.FromResult 包裹，直接使用 async/await 即可
            //var check = await _permissionContract.CheckPermission(routeName);
            //if (!check)
            //{
            //    return false;
            //}
            return true;
        }
    }
}
