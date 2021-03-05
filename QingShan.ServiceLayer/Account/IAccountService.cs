using QingShan.Data;
using QingShan.Services.Account.Dto;
using QingShan.Services.Account.Dto.OutputDto;
using System.Threading.Tasks;

namespace QingShan.Services.Account
{
    public interface IAccountService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<StatusResult<AuthLoginOutputDto>> LoginAsync(LoginInputDto dto);
    }
}
