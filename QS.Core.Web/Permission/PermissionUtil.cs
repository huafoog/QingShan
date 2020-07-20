using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Extensions;
using QS.Permission;
using QS.ServiceLayer.Permission;
using QS.ServiceLayer.Permission.Dto;
using QS.ServiceLayer.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QS.Core.Web.Permission
{

    public static class Permission
    {
        public static IApplicationBuilder UsePermission(this IApplicationBuilder app, IFunctionService functionService)
        {
            var data  = functionService.Pickup();
            var datas = functionService.PickupFunctions();

            return app ;
        }
    }
}
