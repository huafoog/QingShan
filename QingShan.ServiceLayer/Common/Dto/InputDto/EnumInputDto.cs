using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.Common.Dto.InputDto
{
    /// <summary>
    /// 枚举输入参数
    /// </summary>
    public class EnumInputDto
    {
        /// <summary>
        /// 枚举代码
        /// </summary>
        [Required(ErrorMessage ="请输入枚举代码")]
        public string Code { get; set; }


        /// <summary>
        /// 是否显示全部
        /// </summary>
        public bool? IsAll { get; set; } = true;
    }
}
