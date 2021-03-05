using QingShan.DependencyInjection;
using QingShan.Permission;
using QingShan.DataLayer.Entities;

namespace QingShan.Services.Permission
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
