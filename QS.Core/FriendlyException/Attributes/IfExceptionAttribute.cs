﻿// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using QS.DependencyInjection;
using System;

namespace QS.Core.FriendlyException
{
    /// <summary>
    /// 异常复写特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class IfExceptionAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorCode">错误编码</param>
        /// <param name="args">格式化参数</param>
        public IfExceptionAttribute(object errorCode, params object[] args)
        {
            ErrorCode = errorCode;
            Args = args;
        }

        /// <summary>
        /// 错误编码
        /// </summary>
        public object ErrorCode { get; set; }

        /// <summary>
        /// 私有异常消息
        /// </summary>
        private string _errorMessage;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get => Oops.FormatErrorMessage(_errorMessage, Args);
            set => _errorMessage = value;
        }

        /// <summary>
        /// 格式化参数
        /// </summary>
        public object[] Args { get; set; }
    }
}