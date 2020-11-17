using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QS.Core.Data;
using QS.Core.Dependency;
using QS.DataLayer.Entities;
using QS.ServiceLayer.ProductService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QS.ServiceLayer.ProductService
{
    public class ProductService : IProductService, IScopeDependency
    {
        private readonly EFContext _context;
        private readonly IMapper _mapper;
        public ProductService(EFContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductOutputDto>> Get()
        {
            var data = await _context.Products.GetTrackEntities().ToListAsync();

            return _mapper.Map<List<ProductOutputDto>>(data);
        }

        public async Task<StatusResult> Delete(int id)
        {
            var res = await _context.Products.DeleteByIdAsync(id);
            var result = await _context.SaveChangesAsync();
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
