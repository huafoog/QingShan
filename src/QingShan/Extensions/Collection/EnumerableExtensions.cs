using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 使用指定的分隔符连接对象数组的元素  
        /// <see cref="string.Join(string, object?[])"/>
        /// </summary>
        public static string ToStringJoin<T>(this IEnumerable<T> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }
    }
}
