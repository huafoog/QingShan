using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QS.DataLayer.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QS.Core.Web.Filter.Transaction
{
    /// <summary>
    /// 事务拦截注入
    /// </summary>
    internal class TransactionInterceptorFilterImpl : IAsyncActionFilter, IAsyncResultFilter
    {
        private readonly EFContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// 初始化一个<see cref="TransactionInterceptorFilterImpl"/>类型的新实例
        /// </summary>
        public TransactionInterceptorFilterImpl(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<EFContext>();
            _logger = serviceProvider.GetService<ILogger<TransactionInterceptorFilterImpl>>();
        }
        /// <summary>
        /// 在操作之前和模型绑定完成之后异步调用。
        /// <parm>在这里开启事务</parm>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 开启事务提交
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(TransactionInterceptorAttribute)))
            {
                //先判断是否已经启用了事务
                if (_context.Database.CurrentTransaction == null)
                {
                    _logger.LogInformation("开启事务");
                    await _context.Database.BeginTransactionAsync();
                }
            }
            await next();
        }

        /// <summary>
        /// 在操作结果之前异步调用。
        /// <para>在这里执行事务提交操作</para>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // 开启事务提交
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(TransactionInterceptorAttribute)))
            {
                //先判断是否已经启用了事务
                if (_context.Database.CurrentTransaction != null)
                {
                    var trans = _context.Database.CurrentTransaction;
                    try
                    {
                        _logger.LogInformation("提交事务");
                        await trans.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await trans.RollbackAsync();
                        _logger.LogError(ex, "提交事务出错");
                        throw ex.InnerException;
                    }
                }
            }
            await next();
        }
    }
}
