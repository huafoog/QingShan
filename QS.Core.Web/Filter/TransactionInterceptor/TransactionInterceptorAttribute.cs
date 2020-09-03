using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using QS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QS.Core.Web.Filter.Transaction
{
    /// <summary>
    /// 自动事务提交过滤器，在<see cref="ActionFilterAttribute.OnResultExecutionAsync(ResultExecutingContext, ResultExecutionDelegate)"/>方法中执行<see cref="EFContext.Database.BeginTransactionAsync()"/>进行事务提交
    /// <para>继承自<see cref="ServiceFilterAttribute"/>在过滤器<see cref="IAsyncActionFilter"/>中使用依赖注入</para>
    /// </summary>
    public class TransactionInterceptorAttribute : ServiceFilterAttribute
    {
        public TransactionInterceptorAttribute():base(typeof(TransactionInterceptorFilterImpl))
        {

        }

        //public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        //{
        //    var dbContext = context.HttpContext.RequestServices.GetService<EFContext>();
        //    //排除匿名访问
        //    if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(TransactionInterceptorAttribute)))
        //    {
        //        await base.OnResultExecutionAsync(context, next);
        //        return;
        //    }
        //    //先判断是否已经启用了事务
        //    if (dbContext.Database.CurrentTransaction == null)
        //    {
        //        await dbContext.Database.BeginTransactionAsync();
        //        try
        //        {
        //            dbContext.Database.CommitTransaction();
        //        }
        //        catch (Exception ex)
        //        {
        //            dbContext.Database.RollbackTransaction();
        //            throw ex;
        //        }
        //    }
        //    await base.OnResultExecutionAsync(context, next);
        //}
    }
}
