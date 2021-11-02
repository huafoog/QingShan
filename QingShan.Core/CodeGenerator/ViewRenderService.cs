﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using QingShan.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QingShan.Core.CodeGenerator
{
    public class ViewRenderService : IViewRenderService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public ViewRenderService(IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        public ViewContext RenderToView(string viewName, object model, Dictionary<string, object> viewDate = null)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                //可以直接根据路由指定(Controller/Action),这里有个神奇的问题,如果Action不存在,会找到两个视图,导致错误
                //var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);

                //直接根据视图的相对位置寻找(这里直接拿视图)
                var viewResult = _razorViewEngine.GetView("", viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model

                };
                //添加额外参数(ViewDate)
                if (viewDate != null)
                {
                    foreach (var item in viewDate.Keys)
                    {
                        viewDictionary.Add(item, viewDate[item]);
                    }
                }


                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                return viewContext;
            }
        }
        public async Task<string> RenderToStringAsync(string viewName, object model, Dictionary<string, object> viewDate = null)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                //可以直接根据路由指定(Controller/Action),这里有个神奇的问题,如果Action不存在,会找到两个视图,导致错误
                //var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);

                //直接根据视图的相对位置寻找(这里直接拿视图)
                var viewResult = _razorViewEngine.GetView("", viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model

                };
                //添加额外参数(ViewDate)
                if (viewDate != null)
                {
                    foreach (var item in viewDate.Keys)
                    {
                        viewDictionary.Add(item, viewDate[item]);
                    }
                }


                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }


        public async Task Generator()
        {
            ////查询所有需要生成的模板信息
            //var list_temp = _sqliteFreeSql.Select<TemplateConfig>().Where(p => temps.Contains(p.Id)).ToList();

            //foreach (var item in tables)
            //{
            //    var talbe = _Cache.Get<TableConfig>(item);
            //    foreach (var temp in list_temp)
            //    {
            //        if (!string.IsNullOrEmpty(temp.FileName))
            //        {
            //            talbe.FullName = temp.FileName.Replace("{TableName}", talbe.TableName);
            //        }
            //        else
            //        {
            //            talbe.FullName = talbe.TableName;
            //        }
            //        var result = await _viewRenderService.RenderToStringAsync(temp.TempatePaht, talbe, null);
            //        result = result.Replace("<pre>", "").Replace("</pre>", "");
            //        var name = $"{talbe.TableName}{temp.FileSuffix}";
            //        var url = temp.FilePath;
            //        await WriteViewAsync(url, name, result);
            //    }
            //}
        }
    }
}
