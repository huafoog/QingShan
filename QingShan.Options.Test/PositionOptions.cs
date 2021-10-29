using QingShan.Core.ConfigurableOptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace QingShan.Web.Test.Options
{
    [OptionsSettings("Position")]
    public class PositionOptions: IConfigurableOptions
    {
        public string Title { get; set; }
        public string Name { get; set; }
    }
}
