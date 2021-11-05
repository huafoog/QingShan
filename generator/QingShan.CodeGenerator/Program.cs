using QingShan.CodeGenerator.Builders;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QingShan.CodeGenerator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var db = args.GetValue(0,"");
            CodeGeneratorCode.Init();
            DB.Init(db);

            var data = new Dto.CodegeneratorInputDto()
            {
                ControllerNamespace= args.GetValue(1, ""),
                DtoNamespace = args.GetValue(2, ""),
                ServiceNamespace = args.GetValue(3, ""),
                IContractNamespace = args.GetValue(4, ""),
                EntityNamespace = args.GetValue(5, ""),
                Output = args.GetValue(6, ""),
                DataBase = args.GetValue(7, "")
            };
            var result = await CodeGenService.Generator(data);
            Console.WriteLine($"操作成功！输出目录【{result}】");
            Console.ReadKey();
        }
    }
}
