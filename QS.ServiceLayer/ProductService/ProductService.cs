using Microsoft.EntityFrameworkCore;
using QS.Core.Data;
using QS.DataLayer.Entities;
using QS.ServiceLayer.ProductService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.ServiceLayer.ProductService
{
    public class ProductService:IProductService
    {
        private readonly EFContext _context;

        public ProductService(EFContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Get()
        {
            return await _context.Products.GetTrackEntities<Product,int>().ToListAsync();
        }

        public async Task<StatusResult> Delete(int id)
        {
            var res = await _context.Products.DeleteByIdAsync(id);
            var result = await _context.SaveChangesAsync();
            return new StatusResult(result > 0,"删除失败");
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult> Add(ProductInputDto dto)
        {
            var model = new Product() { 
                Address = dto.Address,
                Name = dto.Name,
                Price = dto.Price
            };
            await _context.Products.AddTentityAsync<Product,int>(model);
            var res = _context.SaveChangesAsync();
            return new StatusResult(res.Result>0,"添加失败");
        }
    }
}
