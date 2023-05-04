using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaFoog.CodeGenerator.CodeGenerator.Builders
{
    public class CheckField
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsShowWithId(string fieldName)
        {
            string[] fields = new string[] { "CreateTime".ToLower(), "DeleteTime".ToLower(), "CreatedId".ToLower(), "Id".ToLower() };

            if (fields.Contains(fieldName.ToLower()))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsShow(string fieldName)
        {
            string[] fields = new string[] { "CreateTime".ToLower(), "DeleteTime".ToLower(), "CreatedId".ToLower() };

            if (fields.Contains(fieldName.ToLower()))
            {
                return false;
            }
            return true;
        }

        public static string GetFirstLowerName(string name)
        {
            return name.GetFirstLowercase();
        }
    }
}
