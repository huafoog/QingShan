using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.DatabaseAccessor
{
    public interface ICreatedId<TKey>
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public TKey CreatedId { get; set; }
    }
}
