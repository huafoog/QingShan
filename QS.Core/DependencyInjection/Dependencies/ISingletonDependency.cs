using Microsoft.Extensions.DependencyInjection;
namespace QS.Core.DependencyInjection
{
    /// <summary>
    /// 单例服务注册依赖
    /// <para>实现此接口的类型将自动注册为<see cref="ServiceLifetime.Singleton"/>模式</para>
    /// </summary>
    public interface ISingletonDependency : IPrivateDependency
    {
    }
}