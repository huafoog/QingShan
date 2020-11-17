using QS.Core.Dependency;
using QS.Core.Permission;
using QS.DataLayer.Entities;

namespace QS.ServiceLayer.Permission
{
    /// <summary>
    /// 权限服务实现
    /// </summary>
    public class PermissionService : IPermissionService, IScopeDependency
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
