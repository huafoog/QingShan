using QS.Core.DependencyInjection;
using QS.Core.Permission;
using QS.DataLayer.Entities;

namespace QS.ServiceLayer.Permission
{
    /// <summary>
    /// 权限服务实现
    /// </summary>
    public class PermissionService : IPermissionService, IScopeDependency
    {

        private readonly IUserInfo _user;

        public PermissionService( IUserInfo user)
        {
            _user = user;
        }
    }
}
