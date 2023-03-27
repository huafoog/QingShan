using HuaFoog.CodeGenerator.CodeGenerator.Builders;
using HuaFoog.CodeGenerator.CodeGenerator;
using FreeSql;
using Microsoft.Extensions.Options;
using HuaFoog.CodeGenerator.CodeGenerator.Dto;
using HuaFoog.CodeGenerator.CodeGenerator.Models;
using JinianNet.JNTemplate;

namespace Huafoog.Codegenerator.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task Code_Generator_Test()
        {
            CodeGeneratorBuilder.CreateTemplate();
            var connectionString = $"User ID=sa;Initial Catalog=Estimate;Data Source=.;Password=81154364;Encrypt=True;TrustServerCertificate=True;Pooling=true;Min Pool Size=1";

            var freeSqlBuilder = new FreeSqlBuilder()
                   .UseConnectionString(DataType.SqlServer, connectionString)
                   .UseLazyLoading(false)
                   .UseNoneCommandParameter(true);
            var d = new QingShan.Core.FreeSql.Options.DatabaseAccessorSettingsOptions();
            IOptions<QingShan.Core.FreeSql.Options.DatabaseAccessorSettingsOptions>
                options = Options.Create<QingShan.Core.FreeSql.Options.DatabaseAccessorSettingsOptions>(d);

            var fsql = freeSqlBuilder.Build();
            ICodeGenService codeGenService = new CodeGenService(fsql, options);


            var dto = new CodegeneratorInputDto()
            {
                EntityNamespace = "Estimate.Api"
            };

            var model = new TableConfig()
            {
                ContractNamespace = "23"
            };
            var text = await codeGenService.GetTemplate("Controller", model);
            Console.WriteLine(text);
        }

        [TestMethod]
        public async Task Code()
        {
            CodeGeneratorBuilder.CreateTemplate();
            string fileName = "index";
            CodeGeneratorBuilder.TemplateCache.TryGetValue(fileName, out var fileText);
            var template = Engine.CreateTemplate(fileName, fileText);
            var model = new TableConfig()
            {
                ContractNamespace = "23",
                Name="Conas",
                ColumnConfig=new List<ColumnConfig>()
                {

                    new ColumnConfig()
                    {
                        ColumnName = "asd"
                    }
                }
            };
            template.Set("model", model);

            var text = await template.RenderAsync();
            Console.WriteLine(text);
        }
    }
}