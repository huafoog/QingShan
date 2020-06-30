using System;
using System.Collections.Generic;
using System.Text;

namespace QS.Core.Attributes
{
    /// <summary>
    /// 标注当前类型从源类型的Mapping映射关系
    /// </summary>
    public class MapFromAttribute : Attribute
    {
        public MapFromAttribute(params Type[] sourceTypes)
        {
            SourceTypes = sourceTypes;
        }

        /// <summary>
        /// 源类型
        /// </summary>
        public Type[] SourceTypes { get; }
    }
}
