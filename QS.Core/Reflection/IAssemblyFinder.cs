using System;
using System.Reflection;

namespace QS.Core.Reflection
{
    /// <summary>
    /// 定义程序集查找
    /// </summary>
    public interface IAssemblyFinder
    {
        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        Assembly[] Find(Func<Assembly, bool> predicate);

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <returns></returns>
        Assembly[] FindAll();
    }
}
