using System;
using System.Collections.Generic;
using System.Text;

namespace QS.Core.Web.Permission
{
    /// <summary>
    /// 权限筛选
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute: Attribute
    {
        public int Code { get; set; }

        public PermissionAttribute(PermCode perm)
        {
            Code = (int)perm;
        }
    }
}
