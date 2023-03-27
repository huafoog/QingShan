using HuaFoog.CodeGenerator.CodeGenerator.Dto;
using HuaFoog.CodeGenerator.CodeGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaFoog.CodeGenerator.CodeGenerator
{
    public interface ICodeGenService
    {
        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="fileName"></param>
        Task<string> GetTemplate(string fileName, TableConfig config);

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        Task<string> Generator(CodegeneratorInputDto dto);
    }
}
