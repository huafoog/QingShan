using QS.Core.Data;
using QS.ServiceLayer.Account.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace QS.ServiceLayer.Account
{
    public interface IAccountService
    {
        StatusResult<string> Login(LoginInputDto dto);
    }
}
