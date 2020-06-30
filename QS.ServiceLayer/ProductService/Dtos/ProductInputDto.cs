using QS.Core.Attributes;
using QS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QS.ServiceLayer.ProductService.Dtos
{
    [MapTo(typeof(Product))]
    public class ProductInputDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Address { get; set; }
    }

    public class IdDto
    {
        public int Id { get; set; }
    }
}
