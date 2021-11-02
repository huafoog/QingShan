using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.CodeGenerator
{
    public interface IViewRenderService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <param name="viewDate"></param>
        /// <returns></returns>
        ViewContext RenderToView(string viewName, object model, Dictionary<string, object> viewDate = null);

        /// <summary>
        /// 视图引擎
        /// </summary>
        /// <param name="viewName">视图模板相对路径</param>
        /// <param name="model">viewModel对象</param>
        /// <param name="viewDate">额外参数</param>
        /// <returns></returns>
        Task<string> RenderToStringAsync(string viewName, object model, Dictionary<string, object> viewDate = null);
    }
}
