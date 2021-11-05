using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.CodeGeneratorWeb.CodeGenerator.Dto
{
    public class CodegeneratorInputDto
    {
        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// Dto
        /// </summary>
        public string Dto { get; set; }

        /// <summary>
        /// Service
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        /// IContract
        /// </summary>
        public string IContract { get; set; }


        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllerNamespace { get; set; }
        /// <summary>
        /// Dto
        /// </summary>
        public string DtoNamespace { get; set; }

        /// <summary>
        /// Service
        /// </summary>
        public string ServiceNamespace { get; set; }
        /// <summary>
        /// IContract
        /// </summary>
        public string IContractNamespace { get; set; }


        /// <summary>
        /// 实体命名空间
        /// </summary>
        public string EntityNamespace { get; set; }

    }
}
