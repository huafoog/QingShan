using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QingShan.Data;
using System.Linq;

namespace QingShan.Attributes
{
    /// <summary>
    /// Api action统一处理过滤器
    /// </summary>
    public class ApiResponseFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //模型验证
            if (!context.ModelState.IsValid)
            {
                try
                {
                    context.Result = new OkObjectResult(new StatusResult(context.ModelState.Values.First().Errors[0].ErrorMessage));
                }
                catch
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
