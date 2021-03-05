# 依赖注入

## Attribute 特性

### SkipScanAttribute.cs

> 程序启动时默认会扫描所有程序集并缓存，若打上该标签就不会进行扫描 使用方式

``` C#
/// <summary>
/// 不被扫描和发现的特性
/// </summary>
/// <remarks>用于程序集扫描类型或方法时候</remarks>
[SkipScan]
public class Class{

}

```

### InjectionAttribute.cs

> 程序集启动时会扫描所有继承自`IPrivateDependency.cs`接口的类若该类实现了`InjectionAttribute`特性就获取该特性的值，若未继承则自己实力化一个InjectionAttribute类。

InjectionAttribute.cs 默认

- 添加服务方式 如果存在则覆盖

- 注册选项 自己和第一个接口

- 排序 0

使用`services.AddAutoScanInjection()`进行注入。

`AddAutoScanInjection()`是静态类，需要通过`AddDependencyInjection()`进行注入。

## Extensions 拓展

### DependencyInjectionExtensions.cs 依赖注入解析拓展

### DependencyInjectionServiceCollectionExtensions.cs 依赖注入拓展类

- `AddDependencyInjection(this IServiceCollection services)`
  - 添加依赖注入接口,该接口主要用于依赖注入，添加基础依赖服务。自动注入。
    使用
  
  - 对外开放接口，用于调用`AddAutoScanInjection()`
- `AddAutoScanInjection(this IServiceCollection services)`
  - 添加自动扫描注入
  - 扫描继承自`ITransient`、`IScoped`、`ISingleton`接口的类，并根据`InjectionAttribute`设置注入选项。
- `RegisterService(IServiceCollection services, RegisterType registerType, Type type, InjectionAttribute injectionAttribute, IEnumerable<Type> canInjectInterfaces)`
  - 注册服务 根据`injectionAttribute`注册自己或一个接口或多个接口
  