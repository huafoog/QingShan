using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// 实体框架LINQ相关的扩展方法
    /// </summary>
    public static class EntityFrameworkCoreExtensions
    {

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="startPage">页码</param>
        /// <param name="pageSize">单页数据数</param>
        /// <param name="rowCount">行数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public static IQueryable<TSoure> LoadPageList<TSoure>(this IQueryable<TSoure> soures,int startPage, int pageSize, out int rowCount, Expression<Func<TSoure, bool>> where = null, Expression<Func<TSoure, object>> order = null)
            where TSoure : EntityBaseById<int>
        {
            var result = from p in soures
                         select p;
            if (where != null)
                result = result.Where(where);
            if (order != null)
                result = result.OrderBy(order);
            else
                result = result.OrderBy(m => m.Id);
            rowCount = result.Count();
            return result.Skip((startPage - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 获取 <typeparamref name="TSource"/>不跟踪数据更改（NoTracking）的查询数据源  已筛选删除数据
        /// </summary>
        /// <typeparam name="TSource">数据源</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IQueryable<TSource> GetTrackEntities<TSource,TKey>(this IQueryable<TSource> source) 
            where TSource:EntityBase<TKey>,new()
        {
            return source.Where(o => o.DataState == DataState.Normal).AsNoTracking();
        }

        /// <summary>
        /// 获取 <typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源  已筛选删除数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IQueryable<TSource> GetEntities<TSource, TKey>(this IQueryable<TSource> source)
            where TSource : EntityBase<TKey>,new()
        {
            return source.Where(o => o.DataState == DataState.Normal);
        }

        /// <summary>
        /// 根据指定id删除信息
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<int> DeleteByIdAsync<TSource, TKey>(this IQueryable<TSource> source, TKey id) 
            where TSource : EntityBase<TKey>,new()
        {
            var i = await source.Where(o => o.Id.Equals(id)).BatchUpdateAsync(a => new TSource() { DataState = DataState.Delete });
            return i;
        }
        /// <summary>
        /// 批量删除指定id信息
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<int> DeleteByIdsAsync<TEntity, TKey>(this IQueryable<TEntity> source, TKey[] ids) 
            where TEntity : EntityBase<TKey>, new()
        {
            var i = await source.Where(o => ids.Contains(o.Id)).BatchUpdateAsync(a => new TEntity() { DataState = DataState.Delete });
            return i;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="dbSet"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async ValueTask<EntityEntry<TEntity>> AddTentityAsync<TEntity,Tkey>(this DbSet<TEntity> dbSet, TEntity entity) where TEntity: EntityBase<Tkey>,new()
        {
            entity.CreateTime = DateTime.Now;
            entity.DataState = DataState.Normal;
            var res = await dbSet.AddAsync(entity);
            return res;
        }
    }
}
