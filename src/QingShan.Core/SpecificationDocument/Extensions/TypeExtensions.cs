using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace QingShan.Core.SpecificationDocument.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 判断类型是否是动态Controller
        /// </summary>
        public static bool IsDynamicController(this Type type)
        {
            return IsDynamicController(type.GetTypeInfo());
        }
        /// <summary>
        /// 判断类型是否是动态Controller
        /// </summary>
        public static bool IsDynamicController(this TypeInfo typeInfo)
        {
            //bool a = typeInfo.IsDefined(typeof(DynamicWebApiAttribute));
            //bool b = !typeInfo.IsDefined(typeof(NonControllerAttribute)) && (typeInfo.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
            //        && typeInfo.IsDefined(typeof(ControllerAttribute)));
            //return typeInfo.IsClass && !typeInfo.IsAbstract && typeInfo.IsPublic && !typeInfo.ContainsGenericParameters
            //    && (a || b);
            return false ;
        }
    }
}
