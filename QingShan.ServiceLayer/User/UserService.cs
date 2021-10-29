using FreeSql;
using Mapster;
using QingShan.Data;
using QingShan.DatabaseAccessor;
using QingShan.DependencyInjection;
using QingShan.Encryption;
using QingShan.Permission;
using QingShan.DataLayer.Entities;
using QingShan.Services.User.Dtos.InputDto;
using QingShan.Services.User.Dtos.OutputDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QingShan.Core.FreeSql;
using QingShan.Utilities;
using QingShan.DataLayer;
using QingShan.Common.Data;
using QingShan.Cache;
using QingShan.Services.Permission;
using System.Linq;
using QingShan.Core.FreeSql.UnitOfWork.Attributes;

namespace QingShan.Services.User
{
    /// <summary>
    /// 用户管理-
    /// </summary>
    public class UserService : IUserContract, IScopeDependency
    {
        private readonly IUserInfo _user;
        private readonly ICache _cache;

        /// <summary>
        /// 用户仓储
        /// </summary>
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRepository<UserRoleEntity> _userRoleRepository;
        private readonly IPermissionContract _permissionService;

        public UserService(IUserInfo user,
            ICache cache,
            IRepository<UserEntity> userRepository,
            IRepository<UserRoleEntity> userRoleRepository,
            IPermissionContract permissionService
            )
        {
            _user = user;
            _cache = cache;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _permissionService = permissionService;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult<UserGetOutputDto>> GetAsync(string id)
        {
            var user = await _userRepository
                .Select
                .Where(o=>o.Id == id)
                .FirstAsync<UserGetOutputDto>();
            return new StatusResult<UserGetOutputDto>(user);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult<UserPermissionOutputDto>> GetUserInfoAsync(string id)
        {

            var user = new  UserPermissionOutputDto();
            user.UserInfo = await _userRepository
                .Select
                .Where(o => o.Id == id)
                .FirstAsync<UserGetOutputDto>();
            if (user.UserInfo == null)
            {
                return new StatusResult<UserPermissionOutputDto>();
            }
            user.Codes = await _permissionService.GetPermissionsAsync();
            user.Menu = await _permissionService.GetUserMeunAsync();

            return new StatusResult<UserPermissionOutputDto>(user);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public async Task<StatusResult> Init()
        {
            var user = await _userRepository
                .Select.FirstAsync();
            if (user != null)
            {
                return new StatusResult("当前已存在人员信息，无法初始化");
            }
            var password = MySecurity.MD5(MySecurity.MD5Lower("123456"));
            var model = new UserEntity()
            {
                Id = Snowflake.GenId(),
                Avatar = "",
                UserName = "123456",
                Password = password

            };
            _userRepository.Insert(model);
            return new StatusResult()
            {
                Message = "初始化人员成功，账号【123456】，密码【123456】"
            };
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<UserListOutputDto>> PageAsync(SearchUserInputDto dto)
        {

            var data = await _userRepository
                .WhereIf(dto.Status.HasValue, a => a.Status == dto.Status)
                .WhereIf(dto.Search.NotNull(), a => a.UserName.Contains(dto.Search) || a.NickName.Contains(dto.Search))
                .ToPageResultAsync<UserEntity,UserListOutputDto>(dto);
            if (data.Data.Count > 0)
            {
                var ids = data.Data.Select(o => o.Id);
                var roleData = await _userRoleRepository.Select
                    .From<RoleEntity>((s, b) => s.InnerJoin(a => a.RoleId == b.Id))
                    .Where((a, b) => ids.Contains(a.UserId))
                    .ToListAsync((a, b) => new RoleOutputDto()
                    {
                        RoleId = a.RoleId,
                        Name = b.Name,
                        UserId = a.UserId
                    });
                foreach (var item in data.Data)
                {
                    var roles = roleData.Where(o => o.UserId == item.Id).ToList();
                    item.RoleIds = roles.Select(o=>new RoleRequestDto() { 
                        key = o.RoleId,
                        label = o.Name,
                        value = o.RoleId
                    }).ToArray();
                    item.RoleNames = roles.Select(o => o.Name).ToStringJoin(",");
                }
            }

            return data;


        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(UserAddInputDto input)
        {
            if (_userRepository.Select.Any(o => o.UserName == input.UserName || o.Phone == input.Phone))
            {
                return new StatusResult("账号或手机号已存在");
            }

            var password = MySecurity.MD5(MySecurity.MD5Lower("123456"));
            var entity = input.Adapt<UserEntity>();
            entity.Id = Snowflake.GenId();
            entity.Password = password;
            var res = await _userRepository.InsertOrUpdateAsync(entity);

            var userRoles = input.RoleIds.Select(o=>new UserRoleEntity() { 
                RoleId = o.key,
                UserId = entity.Id,
                Id = Snowflake.GenId()
            });
            await _userRoleRepository.InsertAsync(userRoles);
            return new StatusResult(res == null, "添加失败");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(UserUpdateInputDto input)
        {
            var user = await _userRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (user == null)
            {
                return new StatusResult("用户不存在！");
            }
            var thisUser = new UserEntity()
            {
                Id = input.Id
            };
            _userRepository.Attach(thisUser);
            thisUser.Avatar = input.Avatar;
            thisUser.NickName = input.NickName;
            thisUser.Phone = input.Phone;
            thisUser.RealName = input.RealName;
            int res = await _userRepository.UpdateAsync(thisUser);
            await _userRoleRepository.DeleteAsync(o=>o.UserId == user.Id);
            var userRoles = input.RoleIds.Select(o => new UserRoleEntity()
            {
                RoleId = o.key,
                UserId = user.Id,
                Id = Snowflake.GenId()
            });
            await _userRoleRepository.InsertAsync(userRoles);

            return new StatusResult(res == 0, "修改失败");
        }

        /// <summary>
        /// 修改自身信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> SaveInfoAsync(SaveInfoInputDto input)
        {
            if (_userRepository.Select.Where(o => o.Id != _user.Id && input.Phone == o.Phone).Any())
            {
                return new StatusResult("当前手机号已存在");
            }
            var thisUser = new UserEntity()
            {
                Id = _user.Id
            };
            _userRepository.Attach(thisUser);
            thisUser.Avatar = input.Avatar;
            thisUser.NickName = input.NickName;
            thisUser.Phone = input.Phone;
            thisUser.Remark = input.Remark;
            int res = await _userRepository.UpdateAsync(thisUser);
            return new StatusResult(res == 0, "更新失败");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(string id)
        {
            var res = await _userRepository.DeleteAsync(id);
            return new StatusResult(res == 0, "删除失败");
        }

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DisableAsync(string id)
        {
            var model = await _userRepository.GetAsync(id);
            if (model == null)
            {
                return new StatusResult("未获取到用户信息");
            }
            model.Status =
                model.Status == DataLayer.Enums.EAdministratorStatus.Normal ?
                DataLayer.Enums.EAdministratorStatus.Stop : DataLayer.Enums.EAdministratorStatus.Normal;
            await _userRepository.UpdateAsync(model);
            return new StatusResult();
        }

    }
}
