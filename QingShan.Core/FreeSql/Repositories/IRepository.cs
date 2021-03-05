using FreeSql;

namespace QingShan.DatabaseAccessor
{
    /// <summary>
    /// 仓储
    /// </summary>
    public interface IRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> 
        where TEntity : class,new()
    {

    }
}
