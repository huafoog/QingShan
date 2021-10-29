using Microsoft.Extensions.Configuration;
using QingShan.ConfigurableOptions;
using QingShan.Core.ConfigurableOptions;
using System;
using System.Reflection;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// 选项拓展
    /// </summary>
    public static class ConfiguableOptionExtension
    {
        /// <summary>
        /// 直接获取选项配置
        /// <para>该方式未通过依赖注入实现，无法使用热更新</para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="configuation"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TOptions GetDefultOptions<TOptions>(this IConfiguration configuation,string key = null)
            where TOptions: IConfigurableOptions,new()
        {
            var optionsType = typeof(TOptions);
            var optionsSettings = optionsType.GetCustomAttribute<OptionsSettingsAttribute>(false);

            // 获取键名
            var jsonKey = JsonKey.GetOptionsJsonKey(optionsSettings, optionsType);
            return configuation.GetSection(jsonKey).Get<TOptions>();
        }
    }
}
