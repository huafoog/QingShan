# 前端
使用`vue-element-admin-template`
[点击跳转](https://github.com/qingshan315/QS.Admin)
### 基于EF Core的Code First模式 正在完善
#### 整体框架设计
- QS.Core.Web：这是表示层，提供HTML页面和/或Web API。这没有数据库访问代码，但依赖于ServiceLayer。
- QS.ServiceLayer：它包含数据库访问代码，包括查询对象以及Create，Update和Delete方法。
- QS.DataLayer:它包含应用程序的DbContext和实体类。
```
0.ASP.NET Core 3.1
1.展示层+逻辑层+数据访问层三层架构
2.AutoMapper实现对象映射
3.Swagger文档
4.数据访问层直接使用EF Core提供数据访问支持
```

#### 服务注入
- 所有服务实例继承自`ITransientDependency`、`IScopeDependency`、`ISingletonDependency`接口，在启动时将自行注册
- 通过服务实例找到相应的服务 服务实例依赖的服务必须在第一位。
```
public class ProductService:IProductService, IScopeDependency
{

}
```

#### 关于EF Core上下文问题 

通过依赖关系注入在多个线程之间隐式共享 `DbContext` 实例

默认情况下， `AddDbContext`扩展方法会将`DbContext` 类型注册为范围生存期。

这对于大多数 ASP.NET Core 应用程序中的并发访问问题是安全的，因为在给定的时间只有一个线程在执行每个客户端请求，因为每个请求都将获取一个单独的依赖项注入范围（因而单独的 `DbContext` 实例）。 对于 Blazor 服务器托管模型，使用一个逻辑请求来维护 Blazor 用户线路，因此，如果使用默认注入作用域，则每个用户线路只能提供一个作用域内 `DbContext` 实例。

任何并行执行多个线程的代码都应确保 `DbContext` 实例不会同时访问。

使用依赖关系注入，这可以通过以下方式实现：将上下文注册为作用域，并为每个线程创建作用域（使用 IServiceScopeFactory），或将 DbContext 注册为暂时性（使用采用 `ServiceLifetime` 参数的 `AddDbContext` 的重载）。

[官方文档](https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/configuring-dbcontext)

可以理解为：注入的DbContext生命周期为单次HTTP请求唯一

关于仓储    来自百小僧
```
第一、EF Core自带的是DbContext，还不能说是仓储。

第二、DbContext很容易导致每一个层都依赖。

第三、有些时候我们需要拿一个数据，但是这个数据可能是数据库给的，也可能是Redis或者Xml给的。这个时候DbContext就难了。

而有了仓储，我们直接一个接口就可以了，业务层不关心数据的来源。如果直接DbContext，本身就说明了依赖。

第四、项目的Orm可能不一定是EFCore，有可能是其他ORM，如果强烈依赖了EFCore，那么以后是灾难，所以我们必须抽象出更上一层规范，也就是接口。

第五、通过仓储方式可以实现多EF Core同时存在
仓储是EF Core中的“仓储”的另一个抽象接口。可以实现任意更换数据库，任意更换表
如果你的DbContext是Mysql的，那你只能操作Mysql。
而通过仓储抽象之后，DbContext是动态创建的。

```

#### 缓存
#### redis缓存

appsettings.json 文件中写入如下 
```
{
  "Cache": {
    "Redis": {
      "ConnectionString": "127.0.0.1:6379"
    }
  }
}

```

若不填写配置文件就缓存在内存中

#### 日志

使用NLog记录日志
#### AOP事务
在控制器上打上标记`TransactionInterceptor` 例如
```
[HttpPost]
[TransactionInterceptor]
public async Task<StatusResult> Add(UserAddInputDto input)
{
    return await _userService.AddAsync(input);
}
```
若出现以下问题请检查 方法内是否开启了事务 
> The connection is already in a transaction and cannot participate in another transaction.
如需手动添加事务就不需要打上该标记