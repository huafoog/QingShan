using FreeSql;
using QingShan.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace QingShan.Core.FreeSql
{
    /// <summary>
    /// 仓储
    /// </summary>
    public interface IRepository<TEntity> : IBaseRepository<TEntity, string>
        where TEntity : EntityBase, new()
    {
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);
    }
}
