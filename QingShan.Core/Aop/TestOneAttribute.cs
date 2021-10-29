using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.Aop
{
    public class TestOneAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            Console.WriteLine("one start");
            await next(context);
            Console.WriteLine("one end");
        }
    }
}
