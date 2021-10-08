using QingShan.Core.Core.DatabaseAccessor.Enums;
using QingShan.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// <see cref="FreeSql.ISelect0"/>拓展
    /// </summary>
    public static class ISelectExtension
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TSoure">数据源</typeparam>
        /// <typeparam name="TReturn">映射Dto</typeparam>
        /// <param name="select"></param>
        /// <param name="pageInputDto"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public static async Task<PageOutputDto<TReturn>> ToPageResultAsync<TSoure, TReturn>(this ISelect<TSoure> select
            , PageBaseInputDto pageInputDto
            , Expression<Func<TSoure, bool>> where = null
            , Expression<Func<TSoure, object>> order = null, SortType sortType = SortType.DESC)
        {
            var query = select;
            if (where != null)
            {
                query = query.Where(where);
            }
            if (order != null)
            {
                switch (sortType)
                {
                    case SortType.ASC:
                        query = query.OrderBy(order);
                        break;
                    case SortType.DESC:
                        query = query.OrderByDescending(order);
                        break;
                }
            }
            var data = await query.Count(out var rowCount)
                .Page(pageInputDto.PageNo, pageInputDto.PageSize.Value)
                .ToListAsync<TReturn>();
            return new PageOutputDto<TReturn>()
            {
                PageNo = pageInputDto.PageNo,
                PageSize = pageInputDto.PageSize.Value,
                Data = data,
                TotalCount = rowCount
            };
        }

        #region 分页

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TReturn">映射Dto</typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="select"></param>
        /// <param name="pageInputDto"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public static async Task<PageOutputDto<TReturn>> ToPageResultAsync<T1, T2,T3, TReturn>(this ISelect<T1, T2, T3> select
            , PageBaseInputDto pageInputDto
             , Expression<Func<T1, T2, T3, TReturn>> mapper = null
            , Expression<Func<T1, T2, T3, bool>> where = null
            , Expression<Func<T1, T2, T3, object>> order = null
            , SortType sortType = SortType.DESC) where T1 : class where T2 : class where T3:class
        {
            var query = select;
            if (where != null)
            {
                query = query.Where(where);
            }
            if (order != null)
            {
                switch (sortType)
                {
                    case SortType.ASC:
                        query = query.OrderBy(order);
                        break;
                    case SortType.DESC:
                        query = query.OrderByDescending(order);
                        break;
                }
            }
            var data = await query.Count(out var rowCount)
              .Page(pageInputDto.PageNo, pageInputDto.PageSize.Value)
              .ToListAsync<T1,T2,T3,TReturn>(mapper);
            return new PageOutputDto<TReturn>()
            {
                PageNo = pageInputDto.PageNo,
                PageSize = pageInputDto.PageSize.Value,
                Data = data,
                TotalCount = rowCount
            };
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TReturn">映射Dto</typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="select"></param>
        /// <param name="pageInputDto"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public static async Task<PageOutputDto<TReturn>> ToPageResultAsync<T1, T2, TReturn>(this ISelect<T1, T2> select
            , PageBaseInputDto pageInputDto
            , Expression<Func<T1, T2, bool>> where = null
            , Expression<Func<T1, T2, object>> order = null
             , SortType sortType = SortType.DESC) where T1 : class where T2 : class
        {
            var query = select;
            if (where != null)
            {
                query = query.Where(where);
            }
            if (order != null)
            {
                switch (sortType)
                {
                    case SortType.ASC:
                        query = query.OrderBy(order);
                        break;
                    case SortType.DESC:
                        query = query.OrderByDescending(order);
                        break;
                }
            }
            var data = await query.Count(out var rowCount)
              .Page(pageInputDto.PageNo, pageInputDto.PageSize.Value)
              .ToListAsync<TReturn>();
            return new PageOutputDto<TReturn>()
            {
                PageNo = pageInputDto.PageNo,
                PageSize = pageInputDto.PageSize.Value,
                Data = data,
                TotalCount = rowCount
            };
        }
        #endregion


        /// <summary>
        /// Tolist
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="query"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public static Task<List<TReturn>> ToListAsync<T1, T2, T3, TReturn>(this ISelect<T1, T2 ,T3> query
            , Expression<Func<T1, T2, T3, TReturn>> select = null)
            where T1 : class 
            where T2 : class 
            where T3:class
        {
            if (select ==null)
            {
                return query.ToListAsync<TReturn>();
            }
            return query.ToListAsync(select);
        }

    }
}
