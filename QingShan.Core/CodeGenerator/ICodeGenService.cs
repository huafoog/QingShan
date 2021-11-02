using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.CodeGenerator
{
    public interface ICodeGenService
    {
        /// <summary>
        /// 获取page页
        /// </summary>
        /// <param name="fileName"></param>
        string GetPage(string fileName, object obj = null);

        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="fileName"></param>
        string GetTemplate(string fileName, object obj = null);

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        Task Generator();
    }
}
