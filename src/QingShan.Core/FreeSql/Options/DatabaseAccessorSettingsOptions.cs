using FreeSql;
using Microsoft.Extensions.Configuration;
using QingShan.Core.ConfigurableOptions;

namespace QingShan.Core.FreeSql.Options
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
        public DataType Type { get; set; }


        public string Host { get; set; }
        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string QingShan { get; set; }

        public string Extension { get; set; }

        /// <summary>
        /// 【开发环境必备】自动同步实体结构到数据库，程序运行中检查实体表是否存在，然后创建或修改
        /// <para>注意：生产环境中谨慎使用</para>
        /// </summary>
        public bool SyncStructure { get; set; }

        /// <summary>
        /// 打印sql
        /// </summary>
        public bool PrintingSQL { get; set; }

        /// <summary>
        /// 全局过滤
        /// </summary>
        public bool GlobalFilter { get; set; }

        /// <summary>
        /// 返回创建sql
        /// </summary>
		public bool ReturnCreateSql { get; set; }

        public string Database { get; set; }

        /// <summary>
        /// 选项后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(DatabaseAccessorSettingsOptions options, IConfiguration configuration)
        {
            GlobalFilter = true;
        }
    }
}
