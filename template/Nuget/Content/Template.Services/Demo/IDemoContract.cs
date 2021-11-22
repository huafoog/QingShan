using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Services.Demo
{
    public interface IDemoContract
    {
        Task<string> TestAsync();
    }
}
