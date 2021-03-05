using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace QS.DatabaseAccessor
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class Repository<TEntity, TKey> :BaseRepository<TEntity, TKey>,IRepository<TEntity, TKey>
        where TEntity : class,IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {

        public readonly IFreeSql _fsql;

        public Repository(IFreeSql fsql, UnitOfWorkManager uowManger) :base(uowManger?.Orm ?? fsql, null,null)
        {
            //事务管理绑定
            uowManger?.Binding(this);
            _fsql = fsql;

        }

        #region 同步方法
        #region 重写删除 逻辑删除或物理删除
        public override int Delete(IEnumerable<TEntity> entitys)
        {
            var entityList = new List<TEntity>();
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
            {
                // 逻辑删除
                foreach (TEntity entity in entitys)
                {
                    ((ISoftDeletable)entity).DeleteTime = DateTime.Now;
                    entityList.Add(entity);
                }
                BeginEdit(entityList);
                return 1;
            }
            else
            {
                // 物理删除
                return base.Delete(entitys);
            }
        }

        public override int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entityList = new List<TEntity>();
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
            {
                return _fsql
                    .Update<TEntity>()
                    .SetDto(new { DeleteTime = DateTime.Now })
                    .Where(predicate)
                    .ExecuteAffrows();
            }
            else
            {
                // 物理删除
                return base.Delete(predicate);
            }
        }
        public override int Delete(TEntity entity)
        {
            return Delete(new List<TEntity>() { new TEntity() });
        }
        public override int Delete(TKey id)
        {
            return base.Delete(o=>o.Id.Equals(id));
        }
        #endregion
        #endregion

        #region 异步方法
        #region 重写删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = new TEntity() { Id = id };
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
            {
                // 逻辑删除
                base.Attach(entity);
                ((ISoftDeletable)entity).DeleteTime = DateTime.Now;
                return await base.UpdateAsync(entity, cancellationToken);
            }
            else
            {
                //物理删除
                return await base.DeleteAsync(id, cancellationToken);
            }
        }
        #endregion
        #endregion
    }
}
