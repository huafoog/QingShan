using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        ///获取选项
        /// </summary>
        public static Options GetOptions<Options>(this IServiceCollection services)
            where Options:class
        {
            var build = services.BuildServiceProvider();

            var option = build.GetService<IOptions<Options>>().Value;
            return option;
        }
        /// <summary>
        ///获取选项
        /// </summary>
        public static Options GetOptions<Options>(this ServiceProvider serviceProvider)
            where Options : class
        {
            var option = serviceProvider.GetService<IOptions<Options>>().Value;
            return option;
        }
    }
}
