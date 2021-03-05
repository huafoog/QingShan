using Microsoft.Extensions.DependencyInjection;
namespace QS.DependencyInjection
{
    /// <summary>
    /// 暂时服务注册依赖
    /// <para>实现此接口的类型将自动注册为<see cref="ServiceLifetime.Transient"/>模式</para>
    /// </summary>
    public interface ITransientDependency : IPrivateDependency
    {
    }
}