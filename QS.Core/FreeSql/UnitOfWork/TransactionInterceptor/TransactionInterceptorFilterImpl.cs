using FreeSql;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QS.DatabaseAccessor
{
    /// <summary>
    /// 事务拦截注入
    /// </summary>
    internal class TransactionInterceptorFilterImpl : IAsyncActionFilter,IOrderedFilter
    {
        private readonly UnitOfWorkManager _unitOfWorkManager;
        private readonly ILogger _logger;

        /// <summary>
        /// 初始化一个<see cref="TransactionInterceptorFilterImpl"/>类型的新实例
        /// </summary>
        public TransactionInterceptorFilterImpl(IServiceProvider serviceProvider)
        {
            _unitOfWorkManager = serviceProvider.GetService<UnitOfWorkManager>();
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

            var attr = (TransactionInterceptorAttribute)context.ActionDescriptor.EndpointMetadata
                .Where(m => m.GetType() == typeof(TransactionInterceptorAttribute))
                .FirstOrDefault();
            if (attr == null)
            {
                //不开启事务
                await next();
                return;
            }
            using var trans = _unitOfWorkManager.Begin(attr.Propagation,attr.IsolationLevel);
            // 继续执行
            var resultContext = await next();
            // 判断是否出现异常
            if (resultContext.Exception == null)
            {
                trans.Commit();
            }
            else
            {
                trans.Rollback();
                _logger.LogError(resultContext.Exception, "提交事务出错");
                throw resultContext.Exception;
            }
        }
    }
}
