using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Core.DatabaseAccessor
{
    /// <summary>
    /// 逻辑删除
    /// </summary>
    public interface ISoftDeletable
    {
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }
}
