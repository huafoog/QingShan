using Microsoft.EntityFrameworkCore;
using QS.Core.Data;
using QS.Core.Dependency;
using QS.DataLayer.Entities;
using QS.ServiceLayer.ProductService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using QS.Core.DatabaseAccessor;

namespace QS.ServiceLayer.ProductService
{
    public class ProductService : IProductService, IScopeDependency
    {

        public readonly IRepository<Product, int> _productRepository;

        private readonly EFContext _context;
        public ProductService(IRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductOutputDto>> Get()
        {
            return await _productRepository.Select.ToListAsync<ProductOutputDto>();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> Delete(int id)
        {
            var result = await _productRepository.DeleteAsync(id);
            return new StatusResult(result > 0, "删除失败");
        }

        ///// <summary>
        ///// 添加
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //public async Task<StatusResult> Add(ProductInputDto dto)
        //{
        //    var model = _mapper.Map<Product>(dto);
        //    await _context.Products.AddTentityAsync<Product,int>(model);
        //    var res = _context.SaveChangesAsync();
        //    return new StatusResult(res.Result>0,"添加失败");
        //}


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult> Add(ProductInputDto dto)
        {



            var model = _mapper.Map<Product>(dto);
            var id = await _context.InsertEntityAsync<Product, int>(model);
            return new StatusResult(id > 0, "添加失败");
        }
    }
}
