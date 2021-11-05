using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Data.Constants
{
    /// <summary>
    /// 限流常量
    /// </summary>
    public class RateLimitConst
    {
        /// <summary>
        /// 用户行为缓存
        ///<para> <see cref="string.Format(string, object?)"/></para>
        /// <para>string.Format(RateLimitConst.UserBehaviorCache,id)</para>
        /// </summary>
        public static string UserBehaviorCache = "USER_BEHAVIOR_CACHE_{0}";
    }
}
