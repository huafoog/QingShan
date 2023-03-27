using QingShan.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QingShan.Utilities
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumDescriptionAttribute : Attribute
    {

    }

    public class EnumDto
    {
        /// <summary>
        /// 枚举code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int? Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Label { get; set; }
    }

    public class EnumException : Exception
    {
        public EnumException()
        {

        }
        public EnumException(string message) : base(message)
        {

        }
    }

    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumHelper
    {

        /// <summary>
        /// 能够被扫描的类型
        /// </summary>
        public static readonly IEnumerable<Type> EnumTypeList;
        static EnumHelper()
        {
            // 查找所有需要依赖注入的类型
            EnumTypeList = App.CanBeScanTypes.Where(u => u.IsEnum);
        }

        /// <summary>
        /// 根据枚举代码获取枚举列表
        /// </summary>
        /// <param name="enumCode">枚举名称</param>
        /// <returns></returns>
        public static List<EnumDto> GetEnumListByCode(string enumCode)
        {
            if (enumCode.IsNullOrEmpty())
            {
                return null;
            }
            var currentEnum = EnumTypeList.Where(o => o.Name.ToLower() == enumCode.ToLower()).FirstOrDefault();
            if (currentEnum == null)
            {
                return null;
            }
            var enums = Enum.GetValues(currentEnum);
            List<EnumDto> enumDic = new List<EnumDto>();
            foreach (Enum item in enums)
            {
                enumDic.Add(new EnumDto() { Code = item.ToString(), Value = Convert.ToInt32(item), Label = item.ToDescription() });
            }
            return enumDic;
        }
    }
}
