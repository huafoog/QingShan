using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QS.Core.AutoMapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var container = services.BuildServiceProvider();
            MapperConfigurationExpression cfg = container.GetService<MapperConfigurationExpression>();
            cfg.AddProfile(typeof(AutoMapperConfig));

            return services;
        }
    }
}
