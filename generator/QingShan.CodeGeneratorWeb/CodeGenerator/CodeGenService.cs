using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using QingShan.CodeGeneratorWeb.CodeGenerator;
using QingShan.CodeGeneratorWeb.CodeGenerator.Builders;
using QingShan.CodeGeneratorWeb.CodeGenerator.Dto;
using QingShan.CodeGeneratorWeb.CodeGenerator.Models;
using QingShan.Core.FreeSql.Options;
using QingShan.Utilities;
using RazorEngineCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.CodeGenerator
{
    public class CodeGenService : ICodeGenService
    {
        public Dictionary<string, string> templist = new Dictionary<string, string>();
        private readonly IFreeSql _freeSql;
        private readonly IOptions<DatabaseAccessorSettingsOptions> _dbConfig;

        public CodeGenService(IFreeSql freeSql,IOptions<DatabaseAccessorSettingsOptions> dbConfig)
        {
            _freeSql = freeSql;
            _dbConfig = dbConfig;
        }
        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="fileName"></param>
        public string GetTemplate(string fileName, object obj = null)
        {
            CodeGeneratorCode.TemplateCache.TryGetValue(fileName, out var template);
            return template.Run(obj);
        }


        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="fileName"></param>
        public async Task<string> RunAsync(string fileName, object obj = null)
        {
            CodeGeneratorCode.TemplateCache.TryGetValue(fileName, out var template);
            return await template.RunAsync(obj);
        }

        private async Task Generator(TableConfig model,string templateName,string path,bool isInterface = false)
        {
            var name = model.TableName.Replace("Entity", "");
            var res = await RunAsync(templateName, model);
            var dic = model.Namespace.Replace(".", "\\");
            if (isInterface)
            {
                templateName = templateName.Replace("I","");
            }

            var fileName = $"{(isInterface ? "I" : "")}{name}{templateName}.cs";
            var filePath = path.ToFormat(dic);

            FileHelper.CreateFile(fileName, res, filePath);
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        public async Task<string> Generator(CodegeneratorInputDto dto)
        {
            var tables = GetDbTable();
            var root = Directory.GetCurrentDirectory();
            //先求出最后出现这个字符的下标
            int index = root.LastIndexOf('\\');
            root = root.Substring(0,index + 1);
            var path = root + "Gen\\{0}\\";
            foreach (var model in tables)
            {
                var name = model.TableName.Replace("Entity", "");
                model.FullName = name.GetFirstLowercase();
                model.ContractNamespace = $"{dto.IContractNamespace}.{name}";
                model.DtoNamespace = $"{dto.DtoNamespace}.{name}.Dto";
                model.EntityNamespace = dto.EntityNamespace;

                if (dto.Controller.NotNull())
                {
                    model.Namespace = dto.ControllerNamespace;
                    await Generator(model,"Controller", path);
                }
                if (dto.IContract.NotNull())
                {
                    model.Namespace = $"{dto.IContractNamespace}.{name}";
                    await Generator(model, "IContract", path, true);
                }
                if (dto.Service.NotNull())
                {
                    model.Namespace = $"{dto.ServiceNamespace}.{name}";
                    await Generator(model, "Service", path);
                }
                if (dto.Dto.NotNull())
                {
                    model.Namespace = $"{dto.DtoNamespace}.{name}.Dto";
                    await Generator(model, "InputDto", path);
                    await Generator(model, "OutputDto", path);
                }
                if (dto.EntityNamespace.NotNull())
                {
                    model.Namespace = $"{dto.EntityNamespace}";
                    await Generator(model, "Entity", path);
                }
            }
            return root + "Gen";

        }


        /// <summary>
        /// 获取库里的表
        /// </summary>
        /// <returns></returns>
        private List<TableConfig> GetDbTable()
        {
            var db = _freeSql.DbFirst.GetTablesByDatabase(_dbConfig.Value.Database);
            List<TableConfig> list_table = new List<TableConfig>();
            foreach (var table in db)
            {
                var name = table.Name.LineToCamelCase();
                TableConfig tableConfig = new TableConfig()
                {
                    Id = Guid.NewGuid().ToString(),
                    TableName = name,
                    FullName = name.GetFirstLowercase(),
                    Name = name.Replace("Entity",""),
                    RealName = table.Name,
                    Remark = table.Comment.Replace("\r\n", ""),
                    ColumnConfig = new List<ColumnConfig>()
                };
                foreach (var column in table.Columns)
                {
                    tableConfig.ColumnConfig.Add(new ColumnConfig()
                    {
                        ColumnName = column.Name.LineToCamelCase(),
                        CsType = column.CsType.ToString(),
                        Remark = column.Coment.Replace("\r\n", "")
                    });
                }
                list_table.Add(tableConfig);
            }
            return list_table;
        }
    }
}
