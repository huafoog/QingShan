using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QingShan.Attributes;
using QingShan.Data;
using QingShan.Services.Common.Dto.InputDto;
using QingShan.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QingShan.Web.Areas.Admin.Controllers
{
    public class CommonController : AdminBaseController
    {
        /// <summary>
        /// 获取枚举
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        //[LoggedIn]
        public async Task<StatusResult<List<EnumDto>>> GetEnum([FromQuery]EnumInputDto dto)
        {
            var dic = EnumHelper.GetEnumListByCode(dto.Code);
            if (dic == null)
            {
                return new StatusResult<List<EnumDto>>();
            }
            if (dto.IsAll.HasValue && dto.IsAll.Value)
            {
                dic.Add(new EnumDto() { 
                    Code = "All",
                    Label = "全部"
                });
            }
            await Task.CompletedTask;
            return new StatusResult<List<EnumDto>>(dic);
        }
    }
}
