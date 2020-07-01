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



#### 为什么直接使用EF Core而不再封装一次

在这个框架里面，我觉得对于EF的封装完全没有必要。你说你要换数据库OK，我用EF可以平滑切换。你说你要换ORM，拜托 EF Core不香吗

#### 关于EF Core上下文问题 

通过依赖关系注入在多个线程之间隐式共享 `DbContext` 实例

默认情况下， `AddDbContext`扩展方法会将`DbContext` 类型注册为范围生存期。

这对于大多数 ASP.NET Core 应用程序中的并发访问问题是安全的，因为在给定的时间只有一个线程在执行每个客户端请求，因为每个请求都将获取一个单独的依赖项注入范围（因而单独的 `DbContext` 实例）。 对于 Blazor 服务器托管模型，使用一个逻辑请求来维护 Blazor 用户线路，因此，如果使用默认注入作用域，则每个用户线路只能提供一个作用域内 `DbContext` 实例。

任何并行执行多个线程的代码都应确保 `DbContext` 实例不会同时访问。

使用依赖关系注入，这可以通过以下方式实现：将上下文注册为作用域，并为每个线程创建作用域（使用 IServiceScopeFactory），或将 DbContext 注册为暂时性（使用采用 `ServiceLifetime` 参数的 `AddDbContext` 的重载）。

[官方文档](https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/configuring-dbcontext)

可以理解为：注入的DbContext周期为单次HTTP请求唯一