using QS.Data;
using QS.Services.Account.Dto;
using QS.Services.Account.Dto.OutputDto;
using System.Threading.Tasks;

namespace QS.Services.Account
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
