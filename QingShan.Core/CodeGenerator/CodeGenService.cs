using Microsoft.Extensions.Options;
using QingShan.Core.CodeGenerator.Builders;
using QingShan.Core.CodeGenerator.Models;
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
        /// 获取page页
        /// </summary>
        /// <param name="fileName"></param>
        public string GetPage(string fileName, object obj = null)
        {
            CodeGeneratorCode.PagetemplateCache.TryGetValue(fileName, out var template);
            return template.Run(obj);
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
        /// 生成
        /// </summary>
        /// <returns></returns>
        public async Task Generator()
        {
            var tables = GetDbTable();
            int i = 0;
            foreach (var temp in CodeGeneratorCode.TemplateCache)
            {
                foreach (var model in tables)
                {
                   var res = await temp.Value.RunAsync(model);
                    FileHelper.CreateFile($"admin{i}.cs",res);
                }
                i++;
            }

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
                TableConfig tableConfig = new TableConfig()
                {
                    Id = Guid.NewGuid().ToString(),
                    TableName = table.Name,
                    ColumnConfig = new List<ColumnConfig>()
                };
                foreach (var column in table.Columns)
                {
                    tableConfig.ColumnConfig.Add(new ColumnConfig()
                    {
                        ColumnName = column.Name,
                        CsType = column.CsType.ToString(),
                        Remark = column.Coment
                    });
                }
                list_table.Add(tableConfig);
            }
            return list_table;
        }
    }
}
