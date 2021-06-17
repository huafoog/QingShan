using Microsoft.AspNetCore.Mvc;
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
        /// 添加菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<StatusResult<List<EnumDto>>> GetEnum([FromQuery]EnumInputDto dto)
        {
            var dic = EnumHelper.GetEnumListByCode(new[] { "QingShan.DataLayer" }, new[] { "QingShan.DataLayer.Enums" }, dto.Code);
            if (dto.IsAll??true)
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
