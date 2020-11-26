//using AutoMapper;
//using Microsoft.Extensions.DependencyInjection;
//using QS.Core.Reflection;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace QS.Core.Extensions
//{
//    /// <summary>
//    /// AutoMapper扩展类
//    /// </summary>
//    public static class AutoMapperExtensions
//    {
//        /// <summary>
//        /// 通过反射批量注入AutoMapper映射规则
//        /// </summary>
//        /// <param name="services">服务</param>
//        /// <param name="assemblyNames">程序集数组 如:["TianYa.DotNetShare.Repository","TianYa.DotNetShare.Service"]，无需写dll</param>
//        public static void RegisterAutoMapperProfiles(this IServiceCollection services, params string[] assemblyNames)
//        {
//            foreach (string assemblyName in assemblyNames)
//            {
//                var listProfile = new List<Type>();
//                var parentType = typeof(Profile);
//                //所有继承于Profile的类
//                var types = assemblyName.GetAssembly().GetTypes()
//                    .Where(item => item.BaseType != null && item.BaseType.Name == parentType.Name);

//                if (types != null && types.Count() > 0)
//                {
//                    listProfile.AddRange(types);
//                }

//                if (listProfile.Count() > 0)
//                {
//                    //映射规则注入
//                    services.AddAutoMapper(listProfile.ToArray());
//                }
//            }
//        }

//        /// <summary>
//        /// 通过反射批量注入AutoMapper映射规则
//        /// </summary>
//        /// <param name="services">服务</param>
//        /// <param name="profileTypes">Profile的子类</param>
//        public static void RegisterAutoMapperProfiles(this IServiceCollection services, params Type[] profileTypes)
//        {
//            var listProfile = new List<Type>();
//            var parentType = typeof(Profile);

//            foreach (var item in profileTypes)
//            {
//                if (item.BaseType != null && item.BaseType.Name == parentType.Name)
//                {
//                    listProfile.Add(item);
//                }
//            }

//            if (listProfile.Count() > 0)
//            {
//                //映射规则注入
//                services.AddAutoMapper(listProfile.ToArray());
//            }
//        }
//    }
//}
