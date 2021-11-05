using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Regular
{
    /// <summary>
    /// 正则表达式
    /// </summary>
    public static class RegularPatterns
    {

        /// <summary>
        /// 手机正则
        /// </summary>
        public const string Phone = @"^1[3-9](\d){9}$";

        /// <summary>
        /// Email
        /// </summary>
        public const string EMail = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        /// <summary>
        /// 域名
        /// </summary>
        public const string Host = @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+\.?";


        /// <summary>
        /// 身份证
        /// </summary>
        public const string IDCard = @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)";


        #region 密码
        /// <summary>
        /// 密码(以字母开头，长度在6~18之间，只能包含字母、数字和下划线)
        /// </summary>
        public const string Password = @"^[a-zA-Z]\w{5,17}$";

        /// <summary>
        /// 强密码(必须包含大小写字母和数字的组合，不能使用特殊字符，长度在 8-10 之间)
        /// </summary>
        public const string PasswordStrongCipherNo = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]{8,10}$";
        /// <summary>
        /// 强密码(必须包含大小写字母和数字的组合，可以使用特殊字符，长度在8-10之间)
        /// </summary>
        public const string PasswordStrongCipherYes = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$";
        #endregion
    }
}
