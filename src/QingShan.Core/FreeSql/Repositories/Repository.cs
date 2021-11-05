using FreeSql;
using QingShan.DatabaseAccessor;
using QingShan.Permission;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace QingShan.Core.FreeSql
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : BaseRepository<TEntity, string>, IRepository<TEntity>
        where TEntity : EntityBase, new()
    {
        public readonly IFreeSql _fsql;
        private readonly IUserInfo _userInfo;

        public Repository(IFreeSql fsql, UnitOfWorkManager uowManger,IUserInfo userInfo) : base(uowManger?.Orm ?? fsql, null, null)
        {
            ApplyDataFilter("DeleteTime", a => !a.DeleteTime.HasValue);
            //filter.Apply<ISoftDeletable>("DeleteTime", a => !a.DeleteTime.HasValue)
            //事务管理绑定
            uowManger?.Binding(this);
            _fsql = fsql;
            _userInfo = userInfo;
        }

        #region 同步方法
        #region 重写删除 逻辑删除或物理删除
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
        public override int Delete(string id)
        {
            return Delete(o => o.Id.Equals(id));
        }
        #endregion
        #endregion

        #region 异步方法
        #region 重写删除

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
        {
            var entityList = new List<TEntity>();
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
            {
                List<TEntity> list = new List<TEntity>();
                foreach (var item in ids)
                {
                    var model = new TEntity() { Id = item };
                    Attach(model);
                    model.DeleteTime = DateTime.Now;
                    list.Add(model);
                }
                return await UpdateAsync(list, cancellationToken);
            }
            else
            {
                // 物理删除
                return await base.DeleteAsync(o => ids.Contains(o.Id), cancellationToken);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entityList = new List<TEntity>();
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
            {

                var models = await Where(predicate).ToListAsync(o =>o.Id, cancellationToken);

                List<TEntity> list = new List<TEntity>();
                foreach (var item in models)
                {
                    var model = new TEntity() { Id = item };
                    Attach(model);
                    model.DeleteTime = DateTime.Now;
                    list.Add(model);
                }
                return await UpdateAsync(list, cancellationToken);
            }
            else
            {
                // 物理删除
                return await base.DeleteAsync(predicate, cancellationToken);
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
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
        /// <summary>
        /// 添加或删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<TEntity> InsertOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.CreateTime = DateTime.Now;
            entity.CreatedId = _userInfo.Id;
            return base.InsertOrUpdateAsync(entity, cancellationToken);
        }

        public override Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.CreateTime = DateTime.Now;
            entity.CreatedId = _userInfo.Id;
            return base.InsertAsync(entity, cancellationToken);
        }

        public override Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entitys, CancellationToken cancellationToken = default)
        {
            var list = new List<TEntity>();
            foreach (var entity in entitys)
            {
                entity.CreateTime = DateTime.Now;
                entity.CreatedId = _userInfo.Id;
                list.Add(entity);
            }
            return base.InsertAsync(list, cancellationToken);
        }

        #endregion
    }
}
