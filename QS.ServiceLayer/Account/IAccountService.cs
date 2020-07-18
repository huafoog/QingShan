using QS.Core.Data;
using QS.ServiceLayer.Account.Dto;
using QS.ServiceLayer.Account.Dto.OutputDto;
using System.Threading.Tasks;

namespace QS.ServiceLayer.Account
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
