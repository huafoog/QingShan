# 选项
## 什么是选项
选项是 ASP.NET Core 推荐的动态读取配置的方式，这种方式将配置文件数据用一个强类型来托管，能够实现配置验证、默认值配置、实时读取等功能。
## 与配置的区别
选项实际上也是配置，但在后者的基础上添加了配置验证、默认值/后期配置设定及提供了多种接口读取配置信息，同时还支持供配置更改通知等强大灵活功能。

所以，除了一次性读取使用的配置以外，都应该选用 选项 替换 配置。

# 基本概念

选项模式使用类来提供对相关设置组的强类型访问。 当[配置设置](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1)由方案隔离到单独的类时，应用遵循两个重要软件工程原则：

- [接口分隔原则 (ISP) 或封装](https://docs.microsoft.com/zh-cn/dotnet/standard/modern-web-apps-azure-architecture/architectural-principles#encapsulation)：依赖于配置设置的方案（类）仅依赖于其使用的配置设置。

- [关注点分离](https://docs.microsoft.com/zh-cn/dotnet/standard/modern-web-apps-azure-architecture/architectural-principles#separation-of-concerns)：应用的不同部件的设置不彼此依赖或相互耦合。

# 使用

## 绑定分层配置

在json文件中添加

```json
{
    "Position": {
    "Title": "Editor",
    "Name": "Joe Smith"
  }
}
```

创建`PositionOptions`类

```C#
public class PositionOptions
{
    public const string Position = "Position";

    public string Title { get; set; }
    public string Name { get; set; }
}
```

选项类：

- 必须是包含公共无参数构造函数的非抽象类。
- 类型的所有公共读写属性都已绑定。
- 不会绑定字段。 在上面的代码中，`Position` 未绑定。 由于使用了 `Position` 属性，因此在将类绑定到配置提供程序时，不需要在应用中对字符串 `"Position"` 进行硬编码。

下面的代码：

- 调用`ConfigurationBinder.Bind`将 `PositionOptions` 类绑定到 `Position` 部分。
- 显示 `Position` 配置数据。

```C#
public class Test22Model : PageModel
{
    private readonly IConfiguration Configuration;

    public Test22Model(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public ContentResult OnGet()
    {
        var positionOptions = new PositionOptions();
        Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);

        return Content($"Title: {positionOptions.Title} \n" +
                       $"Name: {positionOptions.Name}");
    }
}
```

在上面的代码中，默认读取在应用启动后对 JSON 配置文件所做的更改。

`ConfigurationBinder.Get`绑定并返回指定的类型。 使用 `ConfigurationBinder.Get<T>` 可能比使用 `ConfigurationBinder.Bind` 更方便。 下面的代码演示如何将 `ConfigurationBinder.Get<T>` 与 `PositionOptions` 类配合使用：

```C#
public class Test21Model : PageModel
{
    private readonly IConfiguration Configuration;
    public PositionOptions positionOptions { get; private set; }

    public Test21Model(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public ContentResult OnGet()
    {            
        positionOptions = Configuration.GetSection(PositionOptions.Position)
                                                     .Get<PositionOptions>();

        return Content($"Title: {positionOptions.Title} \n" +
                       $"Name: {positionOptions.Name}");
    }
}
```

## 依赖注入选项

使用选项模式时的替代方法是绑定 `Position` 部分并将它添加到[依赖项注入服务容器](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1)。 在以下代码中，`PositionOptions` 已通过 `Configure`被添加到了服务容器并已绑定到了配置：

```C#
public void ConfigureServices(IServiceCollection services)
{
    services.Configure<PositionOptions>(Configuration.GetSection(
                                        PositionOptions.Position));
    services.AddRazorPages();
}
```

```C#
public class Test2Model : PageModel
{
    private readonly PositionOptions _options;

    public Test2Model(IOptions<PositionOptions> options)
    {
        _options = options.Value;
    }

    public ContentResult OnGet()
    {
        return Content($"Title: {_options.Title} \n" +
                       $"Name: {_options.Name}");
    }
}
```

# 选项接口

- `IOptions<IOptions>`:
  - 不支持：
    - 在应用启动后读取配置数据。
    - [命名选项](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#named)
  - 注册为[单一实例](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#singleton)且可以注入到任何[服务生存期](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#service-lifetimes)。

- `IOptionsSnapshot<IOptions>`:
  - 在每次请求时应重新计算选项的方案中有用。 有关详细信息，请参阅[使用 IOptionsSnapshot 读取已更新的数据](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#ios)。
  - 注册为[范围内](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#scoped)，因此无法注入到单一实例服务。
  - 支持[命名选项](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#named)

- `IOptionsMonitor<IOptions>`:
  - 用于检索选项并管理 `TOptions` 实例的选项通知。
  - 注册为[单一实例](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#singleton)且可以注入到任何[服务生存期](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#service-lifetimes)。
  - 支持：
    - 更改通知
    - [命名选项](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#named-options-support-with-iconfigurenamedoptions)
    - [可重载配置](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1#ios)
    - 选择性选项失效 ([IOptionsMonitorCache](https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.options.ioptionsmonitorcache-1))

> `IOptions`方式无法在应用启动后读取配置数据
>
> `IOptionsSnapshot`、`IOptionsMonitor`方式可以读取已更新的数据

`IOptionsMonitor` 和 `IOptionsSnapshot` 之间的区别在于：

- `IOptionsMonitor` 是一种[单一示例服务](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#singleton)，可随时检索当前选项值，这在单一实例依赖项中尤其有用。
- `IOptionsSnapshot` 是一种[作用域服务](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1#scoped)，并在构造 `IOptionsSnapshot<T>` 对象时提供选项的快照。 选项快照旨在用于暂时性和有作用域的依赖项。

## IChnageToken

# 框架使用

在本框架中，由于部分服务需要用到配置，而使用`BuildServiceProvider`方式又会创建一个副本。

不推荐以下用法获取实例

```C#
public static Options GetOptions<Options>(this IServiceCollection services)
	where Options:class
{
var build = services.BuildServiceProvider();

var option = build.GetService<IOptions<Options>>().Value;
return option?? default;
}
```

虽然启动时不使用单一实例服务，考虑到官方建议不使用所以在启动时，需要获取配置的地方通过`绑定分层配置`方法使用例如

```C#
var options = QingShan.QingShanApplication.Configuration.GetDefultOptions<TOptions>();
```

该方式不会读取在应用启动后对 JSON 配置文件所做的更改