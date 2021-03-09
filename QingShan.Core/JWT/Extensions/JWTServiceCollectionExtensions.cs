using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QingShan.Core.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// JWT 授权服务拓展类
    /// </summary>
    public static class JWTServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 JWT 授权
        /// </summary>
        /// <param name="authenticationBuilder"></param>
        /// <param name="tokenValidationParameters">token 验证参数</param>
        /// <param name="jwtBearerConfigure"></param>
        /// <param name="enableGlobalAuthorize">启动全局授权</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwt(this AuthenticationBuilder authenticationBuilder, object tokenValidationParameters = default, Action<JwtBearerOptions> jwtBearerConfigure = null, bool enableGlobalAuthorize = false)
        {
            var services = authenticationBuilder.Services;

            // 配置 JWT 选项
            ConfigureJWTOptions(services);

            // 获取配置选项
            var jwtSettings = services.BuildServiceProvider().GetService<IOptions<JWTSettingsOptions>>().Value;

            // 添加授权
            authenticationBuilder.AddJwtBearer(options =>
            {
                options.TokenValidationParameters = (tokenValidationParameters as TokenValidationParameters) ?? JWTEncryption.CreateTokenValidationParameters(jwtSettings);

                // 添加自定义配置
                jwtBearerConfigure?.Invoke(options);
            });

            //启用全局授权
            if (enableGlobalAuthorize)
            {
                services.Configure<MvcOptions>(options =>
                {
                    options.Filters.Add(new AuthorizeFilter());
                });
            }

            return authenticationBuilder;
        }

        /// <summary>
        /// 添加 JWT 授权
        /// </summary>
        /// <param name="services"></param>
        /// <param name="authenticationConfigure">授权配置</param>
        /// <param name="tokenValidationParameters">token 验证参数</param>
        /// <param name="jwtBearerConfigure"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwt(this IServiceCollection services, Action<AuthenticationOptions> authenticationConfigure = null, object tokenValidationParameters = default, Action<JwtBearerOptions> jwtBearerConfigure = null)
        {
            // 配置 JWT 选项
            ConfigureJWTOptions(services);

            var jwtSettings = services.BuildServiceProvider().GetService<IOptions<JWTSettingsOptions>>().Value;

            // 添加默认授权
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                // 添加自定义配置
                authenticationConfigure?.Invoke(options);
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = (tokenValidationParameters as TokenValidationParameters) ?? JWTEncryption.CreateTokenValidationParameters(jwtSettings);

                // 添加自定义配置
                jwtBearerConfigure?.Invoke(options);
            });
        }

        /// <summary>
        /// 添加 JWT 授权
        /// </summary>
        /// <typeparam name="TAuthorizationHandler"></typeparam>
        /// <param name="services"></param>
        /// <param name="authenticationConfigure"></param>
        /// <param name="tokenValidationParameters"></param>
        /// <param name="jwtBearerConfigure"></param>
        /// <param name="enableGlobalAuthorize"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJwt<TAuthorizationHandler>(this IServiceCollection services, Action<AuthenticationOptions> authenticationConfigure = null, object tokenValidationParameters = default, Action<JwtBearerOptions> jwtBearerConfigure = null, bool enableGlobalAuthorize = false)
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            var furionAssembly = Assembly.Load("QingShan");

            // 获取添加授权类型
            var authorizationServiceCollectionExtensionsType = furionAssembly.GetType("Microsoft.Extensions.DependencyInjection.AuthorizationServiceCollectionExtensions");
            var addAppAuthorizationMethod = authorizationServiceCollectionExtensionsType
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(u => u.Name == "AddAppAuthorization" && u.IsGenericMethod && u.GetParameters().Length > 0 && u.GetParameters()[0].ParameterType == typeof(IServiceCollection)).First();

            // 添加策略授权服务
            addAppAuthorizationMethod.MakeGenericMethod(typeof(TAuthorizationHandler)).Invoke(null, new object[] { services, null, enableGlobalAuthorize });

            // 添加授权
            return services.AddJwt(authenticationConfigure, tokenValidationParameters, jwtBearerConfigure);
        }

        /// <summary>
        /// 添加 JWT 授权
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureJWTOptions(IServiceCollection services)
        {
            // 获取配置节点
            var jwtSettingsConfiguration = services.BuildServiceProvider()
                        .GetService<IConfiguration>()
                        .GetSection("JWTSettings");

            // 配置验证
            services.AddOptions<JWTSettingsOptions>()
                        .Bind(jwtSettingsConfiguration)
                        .ValidateDataAnnotations();

            // 选项后期配置
            services.PostConfigure<JWTSettingsOptions>(options =>
            {
                options.ValidateIssuerSigningKey ??= true;
                if (options.ValidateIssuerSigningKey == true)
                {
                    options.IssuerSigningKey ??= "lMLSKIgmkhkb!3q&*50$Z6ScT%Egxl@rqJXK";
                }
                options.ValidateIssuer ??= true;
                if (options.ValidateIssuer == true)
                {
                    options.ValidIssuer ??= "qingshan";
                }
                options.ValidateAudience ??= true;
                if (options.ValidateAudience == true)
                {
                    options.ValidAudience ??= "powerby qinshan";
                }
                options.ValidateLifetime ??= true;
                if (options.ValidateLifetime == true)
                {
                    options.ClockSkew ??= 10;
                }
                options.ExpiredTime ??= 20;
            });
        }
    }
}
