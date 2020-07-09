# 拓展方法说明
### 1.QueryableExtentions.cs
- `Select<TSource, TTarget>(this IQueryable<TSource> query)`

> 该方法使用方式，在进行查询是需要`select(o=> new { })`很多参数非常麻烦，所以在查询时可以使用该方法简化字段查询

