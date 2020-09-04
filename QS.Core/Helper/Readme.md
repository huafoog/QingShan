# 根据枚举的类型名称获取枚举类型列表
## 前言
> 前端进行搜索或其他操作时需要获取枚举列表，每个枚举写一个接口比较繁琐，所以这里根据枚举类型名获取列表
## 代码
### EnumDto.cs 返回的枚举信息类
``` C#
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
```
### EnumExpansion.cs 枚举拓展类
``` C#
using System.ComponentModel;
using System.Reflection;

namespace System
{
    public static class EnumExpansion
    {
        /// <summary>
        /// 获取枚举项上的<see cref="DescriptionAttribute"/>特性的文字描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs.Length == 0)    //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }
    }
}
```
### EnumException.cs 枚举异常类
``` C#
public class EnumException:Exception
{
    public EnumException()
    {

    }
    public EnumException(string message):base(message)
    {

    }
}
```
### EnumHelper.cs 枚举帮助类
``` C#
/// <summary>
/// 枚举帮助类
/// </summary>
public class EnumHelper
{

    private static ConcurrentDictionary<string, Assembly> AssemblyList = new ConcurrentDictionary<string, Assembly>();

    /// <summary>
    /// 根据枚举代码获取枚举列表
    /// </summary>
    /// <param name="assembly">程序集名称集合</param>
    /// <param name="namespaces">命名空间名称集合</param>
    /// <param name="code">枚举名称</param>
    /// <returns></returns>
    public static List<EnumDto> GetEnumListByCode(IEnumerable<string> assemblys,IEnumerable<string> namespaces,string EnumCode)
    {
        assemblys = assemblys.Distinct();
        namespaces = namespaces.Distinct();
        var Inexistentassembly = assemblys.Except(AssemblyList?.Select(o=>o.Key));
        foreach (var assembly in Inexistentassembly)
            AssemblyList.TryAdd(assembly, System.Reflection.Assembly.Load(assembly));
        List<Type> enumlist = new List<Type>();
        List<string> enumNamespanList = new List<string>();
        foreach (var enumNamespace in namespaces)
        {
            foreach (var assembly in AssemblyList)
            {
                var enumInfo = assembly.Value.CreateInstance($"{enumNamespace}.{EnumCode}", false);
                if (enumInfo != null)
                {
                    enumNamespanList.Add(enumNamespace);
                    enumlist.Add(enumInfo.GetType());
                }
            }
        }
        if (enumlist.Count == 0) return default;
        if (enumlist.Count > 1)
            throw new EnumException($"枚举【{EnumCode}】存在多个，请检查命名空间【{string.Join(',', enumNamespanList)}】");
        var enums = Enum.GetValues(enumlist.FirstOrDefault());
        List<EnumDto> enumDic = new List<EnumDto>(); 
        foreach (Enum item in enums)
        {
            enumDic.Add(new EnumDto() { Code = item.ToString(), Value = Convert.ToInt32(item), Description = item.ToDescription() });
        }
        return enumDic;
    }
}
```
如果多个程序集 和 多个命名空间出现一样的枚举类型将会抛出异常
### 使用
运行当前方法的程序集需要引用使用到的程序集

例如当前使用到的程序集名称为`EnumList`

当前使到的命名空间为`EnumList.Enums`

当前需要获取的枚举类型为`UserType`

``` C#
static void Main(string[] args)
{

    var enumName = "UserType";
    var dic = EnumHelper.GetEnumListByCode(new[] { "EnumList" },new[] { "EnumList.Enums" }, enumName);
    foreach (var item in dic)
    {
        Console.WriteLine($"code:{item.Code},value:{item.Value},描述:{item.Description}");
    }
    Console.ReadKey();
}
```
