using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.StaticFile.Models
{
    public class StaticFileFolderModel
    {
        /// <summary>
        /// 文件夹
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// 设置未识别的MIME类型一个默认z值
        /// </summary>
        public string DefaultContentType { get; set; }
        /// <summary>
        /// 如果文件不是可识别的内容类型，是否应该提供该文件?默认值:false。
        /// </summary>
        public bool ServeUnknownFileTypes { get; set; }

        /// <summary>
        /// 请求路径 默认文件夹路径
        /// </summary>
        public string RequestPath { get; set; }
    }
}
