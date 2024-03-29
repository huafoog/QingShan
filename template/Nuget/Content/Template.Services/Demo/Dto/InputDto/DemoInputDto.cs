﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Services.Demo.Dto
{
    /// <summary>
    /// demo
    /// </summary>
    public class DemoInputDto
    {
        /// <summary>
        /// 名称
        /// </summary>
		[Required(ErrorMessage ="请输入名称")]
        public string Name { get; set; }
    }
}
