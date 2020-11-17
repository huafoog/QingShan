using Microsoft.Extensions.DependencyInjection;

namespace QS.Core.Web.Services
{
    public static class CorsService
    {
        /// <summary>
        /// 跨域
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsService(this IServiceCollection services)
        {
            //跨域第一种方法，先注入服务，声明策略，然后再下边app中配置开启中间件
            services.AddCors(c =>
            {
                c.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins("http://localhost:8021", "http://zyaxin.com:8082", "http://localhost:9528")//支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });
        }
    }
}
