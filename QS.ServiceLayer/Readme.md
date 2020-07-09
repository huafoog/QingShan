# AutoMapper
## EF Core 动态查询查询字段
 > 传统方式使用`IQueryable<TSource>().Select(o=>new T(){  });`的方式，及其繁琐有没有能够简化的方式，在AutoMapper中就有这样的方法
### AutoMapper 在EF Core中动态组装查询字段
> 当使用带有`AutoMapper`标准`mapper.Map`功能的ORM（例如NHibernate或Entity Framework）时,`AutoMapper`会尝试将结果映射到目标类型，ORM将会查询所有对象的所有字段。不太友好。`AutoMapper`的`QueryableExtensions`拓展方法可以解决这一问题。

> [官方文档](https://docs.automapper.org/en/latest/Queryable-Extensions.html)
- 单表使用方式
  - 具体配置还是和`mapper.Map`的配置方式一样，在Dto类中使用特性`[MapTo]`或`[MapFrom]`进行关系映射配置
  - 注入`IConfigurationProvider`接口。
  - 使用拓展方法`.ProjectTo<TDestination>()`
  - 给定以下实体
    ``` C#
    public class OrderLine{
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Item { get; set; }
        public decimal Quantity { get; set; }
    }
     ```
  - 以及以下Dto
    ``` C#
    public class OrderLineDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Item { get; set; }
        public decimal Quantity { get; set; }
    }
    ```
  - 使用方式 
    ``` C#
    private readonly IMapper _mapper;
    private readonly EFContext _context;
    private readonly IConfigurationProvider _configurationProvider;
    public UserService(IMapper mapper, 
        EFContext context,
        IConfigurationProvider configurationProvider
        )
    {
        _user = user;
        _mapper = mapper;
        _context = context;
        _configurationProvider = configurationProvider;
    }
    public async Task<OrderLineDTO> GetAsync(int id){
        return await _context.OrderLine.ProjectTo<OrderLineDTO>    (_configurationProvider).FirstOrDefaultAsync(o => o.Id == id);
    }
    ```
    该`.ProjectTo<OrderLineDTO>()`会告诉AutoMapper的映射引擎会自动处理`select`子句，它只需要查询项目表的名称列一样。和你手动投射IQueryable到OrderLineDTO一样。