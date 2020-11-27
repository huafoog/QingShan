using Microsoft.Extensions.Configuration;
using QS.Core.ConfigurableOptions;

namespace QS.Core.DatabaseAccessor
{
    /// <summary>
    /// 数据库配置选项
    /// </summary>
    [OptionsSettings("AppSettings:DatabaseAccessorSettings")]
    public sealed class DatabaseAccessorSettingsOptions : IConfigurableOptions<DatabaseAccessorSettingsOptions>
    {

        /// <summary>
        /// 数据库类型
        /// </summary>
        public FreeSql.DataType Type { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 【开发环境必备】自动同步实体结构到数据库，程序运行中检查实体表是否存在，然后创建或修改
        /// <para>注意：生产环境中谨慎使用</para>
        /// </summary>
        public bool SyncStructure { get; set; }

        /// <summary>
        /// 选项后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(DatabaseAccessorSettingsOptions options, IConfiguration configuration)
        {
        }
    }
}
