﻿using System;

namespace QingShan.Attributes
{
    /// <summary>
    /// 标注当前类型映射到目标类型的Mapping映射关系
    /// </summary>
    public class MapToAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个<see cref="MapToAttribute"/>类型的新实例
        /// </summary>
        public MapToAttribute(params Type[] targetTypes)
        {
            TargetTypes = targetTypes;
        }

        /// <summary>
        /// 目标类型
        /// </summary>
        public Type[] TargetTypes { get; }
    }
}
