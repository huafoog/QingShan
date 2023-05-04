using QingShan.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Test
{
    public class Service2 : IService,ITransientDependency
    {
        public string GetName()
        {
            return "Service2";
        }
    }
}
