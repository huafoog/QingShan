using System;

namespace QS.Attributes
{
    public class DbDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 初始化新的实例
        /// </summary>
        /// <param name="description">说明内容</param>
        public DbDescriptionAttribute(string description)
        {
            Description = description;
        }

        /// <summary>
        /// 说明
        /// </summary>
        public virtual string Description { get; }
    }
}
