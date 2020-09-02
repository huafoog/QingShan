using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QS.Core.Data;
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
        public static async Task<PageOutputDto<TResult>> LoadPageListAsync<TSoure, TResult>(this IQueryable<TSoure> soures, PageInputDto pageInputDto,Expression<Func<TSoure,TResult>> map, Expression<Func<TSoure, bool>> where = null, Expression<Func<TSoure, object>> order = null)
            where TSoure : IEntity<int>
        {
            var result = from p in soures
                         select p;
            if (where != null)
                result = result.Where(where);
            if (order != null)
                result = result.OrderBy(order);
            else
                result = result.OrderBy(m => m.Id);
            int rowCount = result.Count();
            var data = await result.Skip((pageInputDto.PageIndex - 1) * pageInputDto.PageSize).Take(pageInputDto.PageSize).Select(map).ToListAsync();
            return new PageOutputDto<TResult>()
            {
                PageIndex = pageInputDto.PageIndex,
                PageSize = pageInputDto.PageSize,
                Data = data,
                Total = rowCount
            };
        }

        /// <summary>
        /// 获取 <typeparamref name="TSource"/>不跟踪数据更改（NoTracking）的查询数据源  已筛选删除数据
        /// </summary>
        /// <typeparam name="TSource">数据源</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IQueryable<TSource> GetTrackEntities<TSource>(this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> expression = null)
            where TSource : class, IDataState
        {
            source = source.Where(o => o.DataState == DataState.Normal);
            if (expression != null)
            {
                source = source.Where(expression);
            }
            return source.AsNoTracking();
        }

        /// <summary>
        /// 获取 <typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源  已筛选删除数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IQueryable<TSource> GetEntities<TSource>(this IQueryable<TSource> source)
            where TSource : ICreatedTime, IDataState, new()
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
            where TSource : class, IEntity<TKey>, IDataState, new()
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
        /// 添加实体信息 返回主键Id
        /// </summary>
        /// <typeparam name="TEntity">实体模型</typeparam>
        /// <typeparam name="Tkey">主键</typeparam>
        /// <param name="context"></param>
        /// <param name="entity">实体</param>
        /// <returns>实体Id</returns>
        public static async Task<TKey> InsertEntityReIdAsync<TEntity, TKey>(this DbContext context, TEntity entity)
            where TEntity : class, ICreatedTime, IDataState, IEntity<TKey>, new()
            where TKey : struct
        {
            entity.CreateTime = DateTime.Now;
            entity.DataState = DataState.Normal;
            var res = await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        /// <summary>
        /// 添加实体信息
        /// </summary>
        /// <typeparam name="TEntity">实体模型</typeparam>
        /// <typeparam name="Tkey">主键</typeparam>
        /// <param name="context"></param>
        /// <param name="entity">实体</param>
        /// <returns>实体Id</returns>
        public static async Task<int> InsertEntityAsync<TEntity, TKey>(this DbContext context, TEntity entity)
            where TEntity : class, ICreatedTime, IDataState, IEntity<TKey>, new()
            where TKey : struct
        {
            entity.CreateTime = DateTime.Now;
            entity.DataState = DataState.Normal;
            var res = await context.Set<TEntity>().AddAsync(entity);
            return await context.SaveChangesAsync();
        }

        /// <summary>
        /// 添加实体信息
        /// </summary>
        /// <typeparam name="TEntity">实体模型</typeparam>
        /// <typeparam name="Tkey">主键</typeparam>
        /// <param name="context"></param>
        /// <param name="entity">实体</param>
        /// <returns>实体Id</returns>
        public static async Task<int> InsertEntitiesAsync<TEntity>(this DbContext context, TEntity[] entities)
            where TEntity : class, ICreatedTime, IDataState, new()
        {
            for (int i = 0; i < entities.Length; i++)
            {
                entities[i].CreateTime = DateTime.Now;
                entities[i].DataState = DataState.Normal;
            }
            await context.Set<TEntity>().AddRangeAsync(entities);
            return await context.SaveChangesAsync();
        }

        /// <summary>
        /// 修改实体信息
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="context"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static async Task<int> UpdateEntitiesAsync<TEntity>(this DbContext context, TEntity entities)
            where TEntity : class, ICreatedTime, IDataState, new()
        {
             context.Set<TEntity>().Update(entities);
            return await context.SaveChangesAsync();
        }
    }
}
