using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Common.Data
{
    /// <summary>
    /// 缓存Key
    /// </summary>
    public static class CacheKey
    {
        /// <summary>
        /// 用户权限信息
        /// <para>使用<see cref="string.Format(string, object?)"/>进行替换</para>
        /// </summary>
        public static string UserPermissions = "UserPermissions_{0}";
    }
}
