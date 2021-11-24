using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.CodeGenerator
{
    public class DB
    {
        public static IFreeSql MySql;
        public static void Init(string connectionString)
        {
            MySql = new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.MySql, connectionString).Build();
        }
    }
}
