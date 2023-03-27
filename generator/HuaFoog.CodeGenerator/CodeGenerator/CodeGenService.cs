using JinianNet.JNTemplate;
using HuaFoog.CodeGenerator.CodeGenerator.Builders;
using HuaFoog.CodeGenerator.CodeGenerator.Dto;
using HuaFoog.CodeGenerator.CodeGenerator.Models;
using Microsoft.Extensions.Options;
using QingShan.Utilities;
using QingShan.DependencyInjection;

namespace HuaFoog.CodeGenerator.CodeGenerator
{
    public class CodeGenService : ICodeGenService, ITransientDependency
    {
        public Dictionary<string, string> templist = new Dictionary<string, string>();
        private readonly IFreeSql _freeSql;
        private readonly IOptions<QingShan.Core.FreeSql.Options.DatabaseAccessorSettingsOptions> _dbConfig;

        public CodeGenService(IFreeSql freeSql, IOptions<QingShan.Core.FreeSql.Options.DatabaseAccessorSettingsOptions> dbConfig)
        {
            _freeSql = freeSql;
            _dbConfig = dbConfig;
        }
        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="fileName"></param>
        public async Task<string> GetTemplate(string fileName, TableConfig config)
        {
            CodeGeneratorBuilder.TemplateCache.TryGetValue(fileName, out var fileText);
            var template = Engine.CreateTemplate(fileName, fileText);
            template.Set("model", config);

            return await template.RenderAsync();
        }

        private async Task Generator(TableConfig model,
            string file,
            string templateName,
            string path,
            bool isReplace = true,
            bool isFirst = false)
        {
            if (isFirst)
            {
                model.Name = model.Name.GetFirstLowercase();
                model.TableName = model.TableName.GetFirstLowercase();
            }
            var name = model.TableName.Replace("Entity", "");
            
            var res = await GetTemplate(file, model);
            CodeGeneratorBuilder.SuffixCache.TryGetValue(file, out var fileSuffix);

            var dic = model.Namespace.Replace(".", "\\");
            string fileName;
            if (isReplace)
            {
                fileName = string.Format(templateName + "{1}", name,fileSuffix);
            }
            else
            {
                fileName = $"{name}{templateName}{fileSuffix}";
            }

            var filePath = path.ToFormat(dic);

            FileHelper.CreateFile(fileName, res, filePath);
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        public async Task<string> Generator(CodegeneratorInputDto dto)
        {
            var tables = GetDbTable(dto.Tables);
            var root = Directory.GetCurrentDirectory();
            //先求出最后出现这个字符的下标
            int index = root.LastIndexOf('\\');
            root = root.Substring(0, index + 1);

            var genRoot = root+"Gen\\";

            var path = genRoot + "{0}\\";

            FileHelper.DeleteFilesInFolder(genRoot);   
            foreach (var model in tables)
            {

                if (!model.TableName.EndsWith("Entity"))
                {
                    model.TableName += "Entity";
                }

                var name = model.TableName.Replace("Entity", "");
                model.FullName = name.GetFirstLowercase();
                if (!dto.Namespace.IsNullOrEmpty())
                {
                    dto.IContractNamespace = $"{dto.Namespace}";
                    dto.DtoNamespace = $"{dto.Namespace}";
                    dto.ServiceNamespace = $"{dto.Namespace}";
                    dto.EntityNamespace = $"{dto.Namespace}.Data.Entities";
                    dto.ControllerNamespace = $"{dto.Namespace}.Controllers";
                }
                model.ContractNamespace = $"{dto.IContractNamespace}.{name}";
                model.DtoNamespace = $"{dto.DtoNamespace}.{name}.Dto";
                model.EntityNamespace = dto.EntityNamespace;

                if (dto.Controller.NotNull())
                {
                    model.Namespace = dto.ControllerNamespace;
                    await Generator(model, "Controller","{0}Controller", path);
                }
                if (dto.IContract.NotNull())
                {
                    model.Namespace = $"{dto.IContractNamespace}.{name}";
                    await Generator(model, "IContract","I{0}Contract", path);
                }
                if (dto.Service.NotNull())
                {
                    model.Namespace = $"{dto.ServiceNamespace}.{name}";
                    await Generator(model, "Service","{0}Service", path);
                }
                if (dto.Dto.NotNull())
                {
                    model.Namespace = $"{dto.DtoNamespace}.{name}.Dto";
                    await Generator(model, "InputDto","{0}InputDto", path);
                    await Generator(model, "OutputDto","{0}OutputDto", path);
                }
                if (dto.EntityNamespace.NotNull())
                {
                    model.Namespace = $"{dto.EntityNamespace}";
                    await Generator(model, "Entity","{0}Entity", path);
                }
                model.Namespace = dto.Namespace;
                await GenView(model, path);

            }
            return root + "Gen";

        }

        private async Task GenView(TableConfig table, string path)
        {
            var name = table.TableName.Replace("Entity", "").GetFirstLowercase();
            await Generator(table, "AddOrUpdate","AddOrUpdate", path+$"/web/view/{name}/form/",true,true);
            await Generator(table, "api","{0}", path+$"/web/api/",true,true);
            await Generator(table, "index","index", path+$"/web/view/{name}/",true,true);
        }


        /// <summary>
        /// 获取库里的表
        /// </summary>
        /// <returns></returns>
        private List<TableConfig> GetDbTable(string[] tables)
        {
            var db = _freeSql.DbFirst.GetTablesByDatabase(_dbConfig.Value.Database).Where(o => o.Type == FreeSql.DatabaseModel.DbTableType.TABLE && tables.Contains(o.Name));
            List<TableConfig> list_table = new List<TableConfig>();
            foreach (var table in db)
            {
                var name = table.Name.LineToCamelCase();
                TableConfig tableConfig = new TableConfig()
                {
                    Id = Guid.NewGuid().ToString(),
                    TableName = name,
                    FullName = name.GetFirstLowercase(),
                    Name = name.Replace("Entity", ""),
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
