# 前端
使用`vue-element-admin-template`
[点击跳转](https://github.com/qingshan315/QS.Admin)

#### 整体框架设计
- QS.Core.Web：这是表示层，提供HTML页面和/或Web API。这没有数据库访问代码，但依赖于ServiceLayer。
- QS.ServiceLayer：它包含数据库访问代码，包括查询对象以及Create，Update和Delete方法。
- QS.DataLayer:它包含应用程序的DbContext和实体类。
```
0.ASP.NET Core 3.1
1.展示层+逻辑层+数据访问层三层架构
2.Mapster实现对象映射
3.Swagger文档
4.数据访问层使用FreeSql仓储模式
```

#### 服务注入
- 所有服务实例继承自`ITransientDependency`、`IScopeDependency`、`ISingletonDependency`接口，在启动时将自行注册
- 通过服务实例找到相应的服务 服务实例依赖的服务必须在第一位。
```
public class ProductService:IProductService, IScopeDependency
{

}
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

#### 数据库

> 数据库使用[FreeSql](http://freesql.net/)

```csharp
IFreeSql 在项目中应以单例声明，而不是在每次使用的时候创建。
```

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
> 如需手动添加事务就不需要打上该标记

# 一键搭建

下载dotnet 模板

`dotnet new --install QingShan.Template.NetCore::*`

运行模板文件

```
dotnet new qswebapi -n 项目名 -o 路径
```

例如 

当前项目为demo 输出到当前目录

```
dotnet new qswebapi -n Demo -o .
```

