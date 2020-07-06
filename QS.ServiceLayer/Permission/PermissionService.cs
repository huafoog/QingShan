using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using QS.Core.Collections;
using QS.Core.Data.Modules;
using QS.Core.Dependency;
using QS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QS.ServiceLayer.Permission
{
    /// <summary>
    /// 权限服务实现
    /// </summary>
    public class PermissionService:IPermissionService,IScopeDependency
    {
        public readonly EFContext _context;
        public PermissionService(EFContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 将从程序集获取的功能信息同步到数据库中
        /// </summary>
        /// <param name="functions">程序集获取的功能信息</param>
        public void SyncToDatabase(Function[] functions)
        {
            if (functions.Length == 0)
            {
                return;
            }

            Function[] dbItems = _context.Functions.GetTrackEntities<Function, int>().ToList().ToArray();

            //删除的功能
            Function[] removeItems = dbItems.Except(functions,
                EqualityHelper<Function>.CreateComparer(m => m.Area + m.Controller + m.Action)).ToArray();
            int removeCount = removeItems.Length;
            //todo：由于外键关联不能物理删除的实体，需要实现逻辑删除
            _context.Functions.RemoveRange(removeItems);

            //新增的功能
            Function[] addItems = functions.Except(dbItems,
                EqualityHelper<Function>.CreateComparer(m => m.Area + m.Controller + m.Action)).ToArray();
            int addCount = addItems.Length;
            _context.Functions.AddRange(addItems);

            //更新的功能信息
            int updateCount = 0;
            foreach (Function item in dbItems.Except(removeItems))
            {
                bool isUpdate = false;
                Function function;
                try
                {
                    function = functions.Single(m =>
                        string.Equals(m.Area, item.Area, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(m.Controller, item.Controller, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(m.Action, item.Action, StringComparison.OrdinalIgnoreCase));
                }
                catch (InvalidOperationException)
                {
                    throw new Exception($"发现多个“{item.Area}-{item.Controller}-{item.Action}”的功能信息，不允许重名");
                }

                if (function == null)
                {
                    continue;
                }

                if (!string.Equals(item.Name, function.Name, StringComparison.OrdinalIgnoreCase))
                {
                    item.Name = function.Name;
                    isUpdate = true;
                }

                if (item.IsAjax != function.IsAjax)
                {
                    item.IsAjax = function.IsAjax;
                    isUpdate = true;
                }

                if (!item.IsAccessTypeChanged && item.AccessType != function.AccessType)
                {
                    item.AccessType = function.AccessType;
                    isUpdate = true;
                }

                if (isUpdate)
                {
                    _context.Functions.Update(item);
                    updateCount++;
                }
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// 重写以实现将提取到的模块信息同步到数据库中
        /// </summary>
        /// <param name="moduleInfos">从程序集中提取到的模块信息</param>
        protected virtual void SyncToDatabase(ModuleInfo[] moduleInfos)
        {
            //if (moduleInfos.Length == 0)
            //{
            //    return;
            //}


            ////删除数据库中多余的模块
            //Module[] modules = _context.Modules.ToArray();
            //var positionModules = modules.Select(m => new { m.Id, Position = GeModulePosition(modules, m) })
            //    .OrderByDescending(m => m.Position.Length).ToArray();
            //string[] deletePositions = positionModules.Select(m => m.Position)
            //    .Except(moduleInfos.Select(n => $"{n.Position}.{n.Code}"))
            //    .Except(new[] { "Root" })
            //    .ToArray();
            //int[] deleteModuleIds = positionModules.Where(m => deletePositions.Contains(m.Position)).Select(m => m.Id).ToArray();
            //foreach (int id in deleteModuleIds)
            //{
            //    _context.Modules.Where(o => o.Id == id).BatchDelete();
            //}

            ////新增或更新传入的模块
            //foreach (ModuleInfo info in moduleInfos)
            //{
            //    Module parent = GeModule(info.Position);
            //    //插入父级分类
            //    if (parent == null)
            //    {
            //        int lastIndex = info.Position.LastIndexOf('.');
            //        string parent1Position = info.Position.Substring(0, lastIndex);
            //        Module parent1 = GeModule(parent1Position);
            //        if (parent1 == null)
            //        {
            //            throw new Exception($"路径为“{parent1Position}”的模块信息无法找到");
            //        }
            //        string parentCode = info.Position.Substring(lastIndex + 1, info.Position.Length - lastIndex - 1);
            //        ModuleInfo parentInfo = new ModuleInfo() { Code = parentCode, Name = info.PositionName ?? parentCode, Position = parent1Position };
            //        ModuleInfo dto = GetDto(parentInfo, parent1, null);
            //        _context.Add(dto);
            //        parent = _context.Modules.First(m => m.ParentId.Equals(parent1.Id) && m.Code == parentCode);
            //    }
            //    Module module = _context.Modules.FirstOrDefault(m => m.ParentId.Equals(parent.Id) && m.Code == info.Code);
            //    //新建模块
            //    if (module == null)
            //    {
            //        Module dto = GetDto(info, parent, null);
            //        _context.Modules.Add(dto);
            //        module = _context.Modules.First(m => m.ParentId.Equals(parent.Id) && m.Code == info.Code);
            //    }
            //    else //更新模块
            //    {
            //        Module dto = GetDto(info, parent, module);
            //        _context.Update(dto);
            //    }
            //    //if (info.DependOnFunctions.Length > 0)
            //    //{
            //    //    Guid[] functionIds = info.DependOnFunctions.Select(m => m.Id).ToArray();
            //    //    OperationResult result = moduleFunctionStore.SeModuleFunctions(module.Id, functionIds).GetAwaiter().GetResult();
            //    //    if (result.Error)
            //    //    {
            //    //        throw new OsharpException(result.Message);
            //    //    }
            //    //}
            //}
            //_context.SaveChanges();
        }

        private readonly IDictionary<string, Module> _positionDictionary = new Dictionary<string, Module>();
        private Module GeModule(string position)
        {
            if (_positionDictionary.ContainsKey(position))
            {
                return _positionDictionary[position];
            }
            string[] codes = position.Split('.');
            if (codes.Length == 0)
            {
                return null;
            }
            string code = codes[0];
            Module module = _context.Modules.FirstOrDefault(m => m.Code == code);
            if (module == null)
            {
                return null;
            }
            for (int i = 1; i < codes.Length; i++)
            {
                code = codes[i];
                module = _context.Modules.FirstOrDefault(m => m.Code == code && m.ParentId.Equals(module.Id));
                if (module == null)
                {
                    return null;
                }
            }
            _positionDictionary.Add(position, module);
            return module;
        }

        //private static ModuleInputDto GetDto(ModuleInfo info, Module parent, Module existsModule)
        //{
        //    return new ModuleInputDto()
        //    {
        //        Id = existsModule?.Id ?? default(int),
        //        Name = info.Name,
        //        Code = info.Code,
        //        OrderCode = info.Order,
        //        Remark = $"{parent.Name}-{info.Name}",
        //        ParentId = parent.Id
        //    };
        //}

        //private static string GeModulePosition(Module[] source, Module module)
        //{
        //    string[] codes = module.TreePathIds.Select(id => source.First(n => n.Id.Equals(id)).Code).ToArray();
        //    return codes.ExpandAndToString(".");
        //}
    }
}
