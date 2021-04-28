using Microsoft.AspNetCore.Mvc;
using QingShan.Data;
using QingShan.Services.Permission;
using QingShan.Services.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 权限
    /// </summary>
    public class PermissionController : AdminBaseController
    {   
        private readonly IPermissionContract _permissionContract;

        public PermissionController(IPermissionContract permissionContract)
        {
            _permissionContract = permissionContract;
        }
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<StatusResult> InsertPermission(PermissionInputDto dto) => _permissionContract.InsertPermission(dto);

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<StatusResult> UpdatePermission(UpdatePermissionInputDto dto) => _permissionContract.UpdatePermission(dto);
    }
}
