using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QingShan.DependencyInjection;
using System.Reflection;
using QingShan.Test.DependencyInjection.Services;

namespace QingShan.Test.DependencyInjection
{
    [TestClass]
    public class DispatchProxyTest
    {


        [TestMethod]
        public void CreateDispatchProxyTest()
        {
            //////使用DispatchProxy类的静态方法Create生成代理类，其中Create是个泛型方法，泛型有两个值，第一个值必须是接口，第二个值必须是DispatchProxy的子类
            //IMessage messageDispatchProxy = DispatchProxy.Create<IMessage, TransactionDispatchProxy>();
            //////创建一个实现了IMessage接口的类的实例，并赋值给代理类的TargetClass属性
            //((TransactionDispatchProxy)messageDispatchProxy).TargetClass = new EmailMessage();
            var DispatchCreateMethod = typeof(DispatchProxy).GetMethod(nameof(DispatchProxy.Create));
            IMessage proxy = (IMessage)DispatchCreateMethod.MakeGenericMethod(typeof(IMessage), typeof(TransactionDispatchProxy)).Invoke(typeof(IMessage).GetMethod("Send"), null);
            proxy.Send("早上好");
            Console.WriteLine("=======================================");
            proxy.Receive("中午好");
        }
        public interface targetInterface
        {
            void Write(string writesome);
        }
    }
}
