using QingShan.Core.Core.DatabaseAccessor.Enums;
using QingShan.Data;
using System;
using System.Linq.Expressions;
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
        /// <typeparam name="TResult">映射Dto</typeparam>
        /// <param name="select"></param>
        /// <param name="pageInputDto"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public static async Task<PageOutputDto<TResult>> ToPageResultAsync<TSoure, TResult>(this ISelect<TSoure> select
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
            var data  = await query.Count(out var rowCount)
                .Page(pageInputDto.PageNo, pageInputDto.PageSize.Value)
                .ToListAsync<TResult>();
            return new PageOutputDto<TResult>()
            {
                PageNo = pageInputDto.PageNo,
                PageSize = pageInputDto.PageSize.Value,
                Data = data,
                TotalCount = rowCount
            };
        }
    }
}
