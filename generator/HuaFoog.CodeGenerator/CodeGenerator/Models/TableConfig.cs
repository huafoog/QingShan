using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuaFoog.CodeGenerator.CodeGenerator.Models
{
    public class TableConfig
    {
        public string Id { get; set; }
        /// <summary>
        /// 表名 - _转驼峰
        /// </summary>
        public string TableName { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Contract命名空间
        /// </summary>
        public string ContractNamespace { get; set; }

        /// <summary>
        /// 全名
        /// </summary>
        public string FullName { get; set; }


        /// <summary>
        /// 库名
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        public List<ColumnConfig> ColumnConfig { get; set; }
        /// <summary>
        /// 表备注
        /// </summary>
        public string Remark { get; set; }
        public string DtoNamespace { get; internal set; }
        public string EntityNamespace { get; internal set; }
        /// <summary>
        /// 真实表名
        /// </summary>
        public string RealName { get; set; }
    }


}
