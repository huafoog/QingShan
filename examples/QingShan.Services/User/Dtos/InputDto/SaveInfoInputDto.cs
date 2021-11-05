using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.User.Dtos.InputDto
{
    public class SaveInfoInputDto
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Required(ErrorMessage ="请上传头像")]
        public string Avatar { get; set; }
        [Required(ErrorMessage ="请填写手机号")]
        [RegularExpression(QingShan.Regular.RegularPatterns.Phone,ErrorMessage ="请输入正确的手机号")]
        public string Phone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
