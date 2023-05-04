using QingShan.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Test
{
    public class ServiceFactor:ITransientDependency
    {
        private readonly IEnumerable<IService> _services;

        public ServiceFactor(IEnumerable<IService> services)
        {
            _services = services;
        }

        public void Get()
        {

        }
    }
}
