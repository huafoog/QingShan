//using Microsoft.Extensions.DependencyInjection;
//using QingShan.DependencyInjection;
//using QingShan.Core.Reflection;
//using System;
//using System.Linq;

//namespace QingShan.Extensions
//{
//    public static class ServiceExtension
//    {
//        /// <summary>
//        /// 将服务实现类型注册到服务集合中
//        /// </summary>
//        /// <param name="services"></param>
//        public static IServiceCollection AddToServices(this IServiceCollection services)
//        {
//            //services.AddScoped<IUserService, UserService>();
//            //var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
//            //var referencedAssemblies = System.IO.Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
//            //var types = referencedAssemblies
//            //    .SelectMany(a => a.DefinedTypes)
//            //    .Select(type => type.AsType())
//            //    .Where(x => x != scopeType && scopeType.IsAssignableFrom(x)).ToArray();
//            Type[] baseTypes = new[] { typeof(ISingletonDependency), typeof(IScopeDependency), typeof(ITransientDependency) };

//            var container = services.BuildServiceProvider();
//            var _assemblyFinder = container.GetService<IAssemblyFinder>();
//            var types = _assemblyFinder.FindAll().SelectMany(o => o.DefinedTypes).Select(type => type.AsType())
//                .Where(type => baseTypes.Any(b => b.IsAssignableFrom(type))
//                && type.IsClass && !type.IsInterface && !type.IsAbstract);
//            foreach (var implementType in types)
//            {
//                var serviceType = implementType.GetImplementedInterfaces();
//                if (serviceType.Length > 0)
//                {
//                    if (implementType.IsDeriveClassFrom<ITransientDependency>())
//                    {
//                        services.AddTransient(serviceType[0], implementType);
//                    }

//                    if (implementType.IsDeriveClassFrom<IScopeDependency>())
//                    {
//                        services.AddScoped(serviceType[0], implementType);
//                    }

//                    if (implementType.IsDeriveClassFrom<ISingletonDependency>())
//                    {
//                        services.AddSingleton(serviceType[0], implementType);
//                    }
//                }
//            }


//            return services;
//        }
//    }
//}
