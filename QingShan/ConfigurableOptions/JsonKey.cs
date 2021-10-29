using Microsoft.Extensions.Options;
using QingShan.Core.ConfigurableOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.ConfigurableOptions
{
    public static class JsonKey
    {
        /// <summary>
        /// 获取选项键
        /// </summary>
        /// <param name="optionsSettings">选项配置特性</param>
        /// <param name="optionsType">选项类型</param>
        /// <returns></returns>
        public static string GetOptionsJsonKey(OptionsSettingsAttribute optionsSettings, Type optionsType)
        {
            // 默认后缀
            var defaultStuffx = nameof(Options);

            return optionsSettings switch
            {
                // // 没有贴 [OptionsSettings]，如果选项类以 `Options` 结尾，则移除，否则返回类名称
                null => optionsType.Name.EndsWith(defaultStuffx) ? optionsType.Name[0..^defaultStuffx.Length] : optionsType.Name,
                // 如果贴有 [OptionsSettings] 特性，但未指定 JsonKey 参数，则直接返回类名，否则返回 JsonKey
                _ => optionsSettings != null && string.IsNullOrEmpty(optionsSettings.JsonKey) ? optionsType.Name : optionsSettings.JsonKey,
            };
        }
    }
}
