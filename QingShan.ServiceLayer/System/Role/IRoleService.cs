using QingShan.Data;
using QingShan.Services.System.Role.Dto.InputDto;
using QingShan.Services.System.Role.Dto.OutputDto;
using System.Threading.Tasks;

namespace QingShan.Services.System.Role
{
    public interface IRoleService
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult<RoleOutputDto>> GetAsync(int id);

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(RoleInputDto input);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateAsync(RoleInputDto input);


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<RoleOutputDto>> PageAsync(PageInputDto dto);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteAsync(int id);
    }
}
