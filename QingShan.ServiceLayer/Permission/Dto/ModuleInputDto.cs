using QingShan.Attributes;
using QingShan.Data.Enums;
using QingShan.DataLayer.Entities;
using System;

namespace QingShan.Services.Permission.Dto
{
    [MapTo(typeof(ModuleEntity))]
    public class ModuleInputDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 父节点Id
        /// </summary>
        public int Pid { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        public ModuleLevel Level { get; set; }
    }
}
