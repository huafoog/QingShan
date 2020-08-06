using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using QS.Core.Collections;
using QS.Core.Data;
using QS.Core.Dependency;
using QS.Core.Permission;
using QS.Core.Permission.Authorization.Modules;
using QS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.ServiceLayer.Permission
{
    /// <summary>
    /// 权限服务实现
    /// </summary>
    public class PermissionService:IPermissionService,IScopeDependency
    {
        private readonly EFContext _context;

        private readonly IUserInfo _user;

        public PermissionService(EFContext context, IUserInfo user)
        {
            _context = context;
            _user = user;
        }
    }
}
