using Microsoft.Extensions.DependencyInjection;

namespace QS.Core.Dependency
{
    /// <summary>
    /// 实现此接口的类型将自动注册为<see cref="ServiceLifetime.Scope"/>模式
    /// </summary>
    public interface IScopeDependency
    {
    }
}
