using QingShan.CodeGenerator.Builders;
using QingShan.CodeGenerator.Dto;
using QingShan.CodeGenerator.Models;
using QingShan.Utilities;
using RazorEngineCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.CodeGenerator
{
    public class CodeGenService
    {
        public static Dictionary<string, string> templist = new Dictionary<string, string>();

        public CodeGenService()
        {
        }
        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="fileName"></param>
        public static string GetTemplate(string fileName, object obj = null)
        {
            CodeGeneratorCode.TemplateCache.TryGetValue(fileName, out var template);
            return template.Run(obj);
        }


        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="fileName"></param>
        public static async Task<string> RunAsync(string fileName, object obj = null)
        {
            CodeGeneratorCode.TemplateCache.TryGetValue(fileName, out var template);
            return await template.RunAsync(obj);
        }

        private static async Task Generator(TableConfig model,string templateName,string path,bool isInterface = false)
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
        public static async Task<string> Generator(CodegeneratorInputDto dto)
        {
            var tables = GetDbTable(dto.DataBase);
            //var root = Directory.GetCurrentDirectory();
            ////先求出最后出现这个字符的下标
            //int index = root.LastIndexOf('\\');
            //root = root.Substring(0,index + 1);
            var path = dto.Output + "\\Gen\\{0}\\";
            foreach (var model in tables)
            {
                var name = model.TableName.Replace("Entity", "");
                model.FullName = name.GetFirstLowercase();

                model.ContractNamespace = $"{dto.IContractNamespace}.{name}";
                model.DtoNamespace = $"{dto.DtoNamespace}.{name}.Dto";
                model.EntityNamespace = dto.EntityNamespace;

                model.Namespace = dto.ControllerNamespace;
                await Generator(model, "Controller", path);

                model.Namespace = $"{dto.IContractNamespace}.{name}";
                await Generator(model, "IContract", path, true);

                model.Namespace = $"{dto.ServiceNamespace}.{name}";
                await Generator(model, "Service", path);


                model.Namespace = $"{dto.DtoNamespace}.{name}.Dto";
                await Generator(model, "InputDto", path);
                await Generator(model, "OutputDto", path);

                model.Namespace = $"{dto.EntityNamespace}";
                await Generator(model, "Entity", path);
            }
            return dto.Output + "\\Gen";

        }


        /// <summary>
        /// 获取库里的表
        /// </summary>
        /// <returns></returns>
        private static List<TableConfig> GetDbTable(string database)
        {
            var db = DB.MySql.DbFirst.GetTablesByDatabase(database);
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
