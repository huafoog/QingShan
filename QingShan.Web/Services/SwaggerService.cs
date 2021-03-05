using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using QingShan.Core.ConfigurableOptions;
using QingShan.Core.SpecificationDocument;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace QingShan.Core.Web.Services
{
    public static class SwaggerService
    {
        public static void AddSwaggerService(this IServiceCollection services)
        {
            // 添加配置
            services.AddConfigurableOptions<SpecificationDocumentSettingsOptions>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerGeneratorOptions.DescribeAllParametersInCamelCase = true;
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "QingShan.Core API",
                    Description = "一个简单框架的api",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                //添加设置Token的按钮
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                //添加Jwt验证设置
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    });

                //添加读取注释服务
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                var xmlPath2 = Path.Combine(AppContext.BaseDirectory, "QingShan.Services.xml");
                c.IncludeXmlComments(xmlPath2);
            });
        }
    }
}
