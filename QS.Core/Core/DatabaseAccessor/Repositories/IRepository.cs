using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Core.DatabaseAccessor
{
    /// <summary>
    /// 仓储
    /// </summary>
    public interface IRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> 
        where TEntity : class,new()
    {

    }
}
