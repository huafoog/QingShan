using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QingShan.DependencyInjection;
using QingShan.Core.FreeSql;
using QingShan.Data;
using FreeSql;
using Mapster;
using QingShan.Utilities;
using QingShan;
using $model.ContractNamespace;
using $model.DtoNamespace;
using $model.EntityNamespace;
namespace $model.Namespace
{
    /// <summary>
	/// $model.Remark
    /// </summary>
    public class ${model.Name}Service:I${model.Name}Contract,IScopeDependency
    {
        private readonly IRepository<$model.TableName> _${model.FullName}Repository;
        public ${model.Name}Service(IRepository<$model.TableName> ${model.FullName}Repository)
        {
            _${model.FullName}Repository = ${model.FullName}Repository;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<${model.Name}OutputDto>> PageAsync(Page${model.Name}InputDto dto)
        {
            return await _${model.FullName}Repository.Select.ToPageResultAsync<$model.TableName,${model.Name}OutputDto>(dto,null);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(${model.Name}InputDto input)
        {
            var entity = input.Adapt<${model.TableName}>();
            entity.Id = Snowflake.GenId();
            var result = await _${model.FullName}Repository.InsertAsync(entity);
            return new StatusResult(result == null, "添加失败");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(${model.Name}InputDto input)
        {
            var data = await _${model.FullName}Repository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (data == null)
            {
                return new StatusResult("数据不存在！");
            }
            $foreach(item in model.ColumnConfig)
            $if(item.PropName == "CreateTime")
            ${ elif(item.PropName == "DeleteTime")}
             ${ elif(item.PropName == "CreatedId")}
             ${ elif(item.PropName == "Id")}
            $else
            data.@(item.PropName) = input.@(item.PropName);
            $end
            $end
            int res = await _${model.FullName}Repository.UpdateAsync(data);
            return new StatusResult(res == 0, "修改失败");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(string id)
        {
            var res = await _${model.FullName}Repository.DeleteAsync(id);
            return new StatusResult(res == 0, "删除失败");
        }

    }
}
