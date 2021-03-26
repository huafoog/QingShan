using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QingShan.Utilities
{

    public class EnumDto
    {
        /// <summary>
        /// 枚举code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
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

        private static ConcurrentDictionary<string, Assembly> AssemblyList = new ConcurrentDictionary<string, Assembly>();

        /// <summary>
        /// 根据枚举代码获取枚举列表
        /// </summary>
        /// <param name="assemblys">程序集名称集合</param>
        /// <param name="namespaces">命名空间名称集合</param>
        /// <param name="enumCode">枚举名称</param>
        /// <returns></returns>
        public static List<EnumDto> GetEnumListByCode(IEnumerable<string> assemblys, IEnumerable<string> namespaces, string enumCode)
        {
            assemblys = assemblys.Distinct();
            namespaces = namespaces.Distinct();
            var Inexistentassembly = assemblys.Except(AssemblyList?.Select(o => o.Key));
            foreach (var assembly in Inexistentassembly)
            {
                //AppAssembly.Assemblies.Where(o=>o.FullName == assembly);
                AssemblyList.TryAdd(assembly, System.Reflection.Assembly.Load(assembly));
            }

            List<Type> enumlist = new List<Type>();
            List<string> enumNamespanList = new List<string>();
            foreach (var enumNamespace in namespaces)
            {
                foreach (var assembly in AssemblyList)
                {
                    var enumInfo = assembly.Value.CreateInstance($"{enumNamespace}.{enumCode}", false);
                    if (enumInfo != null)
                    {
                        enumNamespanList.Add(enumNamespace);
                        enumlist.Add(enumInfo.GetType());
                    }
                }
            }
            if (enumlist.Count == 0)
            {
                return default;
            }

            if (enumlist.Count > 1)
            {
                throw new EnumException($"枚举【{enumCode}】存在多个，请检查命名空间【{string.Join(",", enumNamespanList)}】");
            }

            var enums = Enum.GetValues(enumlist.FirstOrDefault());
            List<EnumDto> enumDic = new List<EnumDto>();
            foreach (Enum item in enums)
            {
                enumDic.Add(new EnumDto() { Code = item.ToString(), Value = Convert.ToInt32(item), Description = item.ToDescription() });
            }
            return enumDic;
        }
    }
}
