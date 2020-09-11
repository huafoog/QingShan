using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using QS.Core.Data;
using QS.Core.Web.Filter.Transaction;
using QS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QS.Core.Web.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public ILogger<GlobalExceptionFilter> _logger;
        private readonly EFContext _context;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, EFContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnException(ExceptionContext context)
        {
            try
            {
                // 开启事务提交
                if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(TransactionInterceptorAttribute)))
                {
                    //先判断是否已经启用了事务
                    if (_context.Database.CurrentTransaction != null)
                    {
                        var trans = _context.Database.CurrentTransaction;
                        _logger.LogInformation("事务回滚");
                        trans.Rollback();
                    }
                }
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
