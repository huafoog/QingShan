using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 模块功能信息
    /// </summary>
    public class ModuleFunctionEntity:EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 模块编号
        /// </summary>
        [DisplayName("模块编号")]
        public int ModuleId { get; set; }

        /// <summary>
        /// 获取或设置 功能编号
        /// </summary>
        [DisplayName("功能编号")]
        public Guid FunctionId { get; set; }
    }
}
