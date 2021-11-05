using Microsoft.Extensions.Configuration;
using QingShan.Core.ConfigurableOptions;
using QingShan.Core.StaticFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.StaticFile
{
    /// <summary>
    /// 静态文件配置类
    /// </summary>
    [OptionsSettings("StaticFileSettings")]
    public sealed class StaticFileSettingsOption : IConfigurableOptions
    {
        public bool UseDirectoryBrowser { get; set; }

        /// <summary>
        /// 映射
        /// </summary>
        public StaticFileMapModel[] StaticFileMap { get; set; }

        /// <summary>
        /// 文件夹
        /// </summary>
        public StaticFileFolderModel[] StaticFileFolder { get; set; }

        /// <summary>
        /// 后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(StaticFileSettingsOption options, IConfiguration configuration)
        {
            options.UseDirectoryBrowser = false;
        }
    }
}
