using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.Permission.Dto
{
    /// <summary>
    /// id集合
    /// </summary>
    public class IdsInputDto
    {
        /// <summary>
        /// id集合
        /// </summary>
        [Required(ErrorMessage ="未获取到请求数据")]
        public List<string> Ids { get; set; }
    }
}
