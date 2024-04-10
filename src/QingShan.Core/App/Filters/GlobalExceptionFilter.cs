using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using QingShan.Data;
using System;

namespace QingShan.Core.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            try
            {
                _logger.LogError(context.Exception, "全局异常");

                context.Result = new InternalServerErrorObjectResult(new StatusResult("服务器异常"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "异常中间件出错");
                throw;
            }
            context.ExceptionHandled = true; // 注意：如果不添加这句代码，程序不会自动断路，会继续向下进行。
        }
    }

    /// <summary>
    /// 服务器异常返回
    /// </summary>
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
