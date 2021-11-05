using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.CodeGenerator
{
    internal class DB
    {
        static Lazy<IFreeSql> mysqlLazy;

        public static void Init(string connectionString)
        {
            mysqlLazy = new Lazy<IFreeSql>(() => new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.MySql, connectionString).Build());
        }


        public static IFreeSql MySql => mysqlLazy.Value;
    }
}
