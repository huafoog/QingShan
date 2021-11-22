using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QingShan.Authorization;
using System;
using System.Threading.Tasks;

namespace Template.Web.Authorization
{
    public class JwtHandler : AppAuthorizeHandler
    {
        /// <summary>
        /// 重写 Handler 添加自动刷新授权逻辑
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
        }

        /// <summary>
        /// 验证管道，也就是验证核心代码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            //var serviceProvider = context.GetCurrentHttpContext().Request.HttpContext.RequestServices;
            //这里写验证逻辑
            return await Task.FromResult(true);
        }
    }
}
