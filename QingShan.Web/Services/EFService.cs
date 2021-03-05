//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using QingShan.DataLayer.Entities;

//namespace QingShan.Core.Web.Services
//{
//    public static class EFService
//    {
//        /// <summary>
//        /// 添加EF服务
//        /// </summary>
//        /// <param name="services"></param>
//        /// <param name="Configuration"></param>
//        public static void AddEFService(this IServiceCollection services, IConfiguration Configuration)
//        {
//            //使用AddDbContext这个Extension method为MyContext在Container中进行注册，它默认的生命周期使是Scoped。
//            //Scoped的生命周期为单次http请求唯一
//            services.AddDbContext<EFContext>(o =>
//            {
//                var db = Configuration["DB:UseDB"];
//                switch (db)
//                {
//                    case "SqlServer":
//                        o.UseSqlServer(Configuration["DB:SqlServer:ConnectionString"]);
//                        break;
//                    case "MySql":
//                        o.UseMySql(Configuration["DB:MySql:ConnectionString"]);
//                        break;
//                    default:
//                        o.UseSqlServer(Configuration["DB:SqlServer:ConnectionString"]);
//                        break;
//                }
//            }
//            );
//        }
//    }
//}
