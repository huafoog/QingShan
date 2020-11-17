using QS.Core.Attributes;
using QS.DataLayer.Entities;

namespace QS.ServiceLayer.ProductService.Dtos
{
    /// <summary>
    /// 商品输入参数
    /// </summary>
    [MapTo(typeof(Product))]
    public class ProductInputDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }

    /// <summary>
    /// 商品输入参数
    /// </summary>
    [MapFrom(typeof(Product))]
    public class ProductOutputDto
    {

        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }

    /// <summary>
    /// id输入
    /// </summary>
    public class IdDto
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
    }
}
