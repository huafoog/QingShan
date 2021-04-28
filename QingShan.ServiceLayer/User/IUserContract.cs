using QingShan.Data;
using QingShan.Services.User.Dtos.InputDto;
using QingShan.Services.User.Dtos.OutputDto;
using System.Threading.Tasks;

namespace QingShan.Services.User
{
    public interface IUserContract
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult<UserGetOutputDto>> GetAsync(string id);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        Task<StatusResult> Init();
        
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(UserAddInputDto input);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateAsync(UserUpdateInputDto input);


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<UserListOutputDto>> PageAsync(PageInputDto dto);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteAsync(string id);
    }
}
