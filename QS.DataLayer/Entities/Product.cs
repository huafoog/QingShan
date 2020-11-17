using QS.Core.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Product : EntityBase<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        /// <summary>
        /// 添加地址
        /// </summary>

        [Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; }
    }
}
