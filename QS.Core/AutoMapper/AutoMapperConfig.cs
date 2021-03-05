//using AutoMapper;
//using QS.Attributes;
//using QS.Core.Extensions;
//using QS.Core.Helper;
//using System;
//using System.Linq;

//namespace QS.Core.AutoMapper
//{
//    /// <summary>
//    /// AutoMapper 配置文件
//    /// </summary>
//    public class AutoMapperConfig : Profile
//    {
//        /// <summary>
//        /// 初始化映射关系
//        /// </summary>
//        public AutoMapperConfig()
//        {
//            CreateMap();
//        }

//        /// <summary>
//        /// 创建映射
//        /// </summary>
//        public void CreateMap()
//        {
//            var source = RuntimeHelper.GetAllAssemblies().SelectMany(assembly => assembly.GetTypes());
//            Type[] types = source.Where(o => o.HasAttribute<MapFromAttribute>(true)).ToArray();
//            foreach (Type targetType in types)
//            {
//                MapFromAttribute attribute = targetType.GetAttribute<MapFromAttribute>(true);
//                foreach (Type sourceType in attribute.SourceTypes)
//                {
//                    CreateMap(sourceType, targetType);
//                }
//            }

//            types = source.Where(o => o.HasAttribute<MapToAttribute>(true)).ToArray();
//            foreach (Type sourceType in types)
//            {
//                MapToAttribute attribute = sourceType.GetAttribute<MapToAttribute>(true);
//                foreach (Type targetType in attribute.TargetTypes)
//                {
//                    CreateMap(sourceType, targetType);
//                }
//            }
//        }
//    }
//}
