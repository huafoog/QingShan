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
    internal class TransactionInterceptorFilterImpl : IAsyncActionFilter,IOrderedFilter
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
        /// 过滤器执行的顺序是由Microsoft.AspNetCore.Mvc.Filters.IOrderedFilter.Order的升序排序决定的
        /// <para>该值越低执行越靠前</para>
        /// </summary>
        public int Order => -100;

        /// <summary>
        /// 在操作之前和模型绑定完成之后异步调用。
        /// <parm>在这里开启事务</parm>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 是否开启事务提交
            var checkHasTransaction = context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(TransactionInterceptorAttribute));
            if (!checkHasTransaction)
            {
                await next();
                return;
            }

            //先判断是否已经启用了事务
            if (_context.Database.CurrentTransaction == null)
            {
                _logger.LogInformation("开启事务");
                await _context.Database.BeginTransactionAsync();
            }
            // 继续执行
            var resultContext = await next();
            // 判断是否出现异常
            if (resultContext.Exception == null)
            {
                var trans = _context.Database.CurrentTransaction;
                //先判断是否已经启用了事务
                if (trans != null)
                {
                    await trans.CommitAsync();
                }
            }
            else
            {
                var trans = _context.Database.CurrentTransaction;
                //先判断是否已经启用了事务
                if (trans != null)
                {
                    await trans.RollbackAsync();
                    _logger.LogError(resultContext.Exception, "提交事务出错");
                    throw resultContext.Exception;
                }
            }
        }
    }
}
