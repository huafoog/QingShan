﻿using System;

namespace QS.Attributes
{
    /// <summary>
    /// 标注当前Action不作为Function信息进行收集
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NonFunctionAttribute : Attribute
    { }
}
