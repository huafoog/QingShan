using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QS.Permission;
using System;
using System.Text;

namespace QS.Core.Web.Services
{
    public static class AuthorizationService
    {
        /// <summary>
        /// 添加授权
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddAuthorizationService(this IServiceCollection services, IConfiguration configuration)
        {
            //添加jwt验证：
            services.AddAuthorization(options =>
            {
                //基于策略的授权
                //options.AddPolicy("Admin", policy => policy.Requirements.Add(new PolicyRequirement()));
            }).AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = nameof(ResponseAuthenticationHandler); //401
                options.DefaultForbidScheme = nameof(ResponseAuthenticationHandler);    //403
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = configuration["Audience:Audience"],//Audience
                    ValidIssuer = configuration["Audience:Issuer"],//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Audience:Secret"]))//拿到SecurityKey
                };
            }).AddScheme<AuthenticationSchemeOptions, ResponseAuthenticationHandler>(nameof(ResponseAuthenticationHandler), o => { });
            //注入授权Handler
            //services.AddSingleton<IAuthorizationHandler, PolicyHandler>();
        }
    }
}
