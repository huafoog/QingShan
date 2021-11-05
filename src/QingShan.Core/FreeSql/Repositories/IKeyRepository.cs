using FreeSql;
using QingShan.DatabaseAccessor;

namespace QingShan.Core.FreeSql
{
    /// <summary>
    /// 仓储
    /// 动态的主键<typeparamref name="TKey"/>
    /// </summary>
    public interface IKeyRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>, new()
    {

    }
}
