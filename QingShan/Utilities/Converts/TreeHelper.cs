using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Utilities
{
    /// <summary>
    /// 树形结构
    /// </summary>
    public static class TreeHelper
    {
        #region 无限极树形
        /// <summary>
        /// 获取无限极树形结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> GetTreeByParentId<T>(List<T> list)
           where T : TreeNodeDto<T>
        {
            var root = list.Where(o => o.ParentId == null).ToList();
            foreach (var item in root)
            {
                var newList = new List<T>();
                item.Children = list.Where(a => a.ParentId == item.Id).ToList();
                GetParentList(list, item.Id);
            }
            return list.Where(o => o.ParentId == null).ToList();
        }

        public static void GetParentList<T>(List<T> list, string parentId)
             where T : TreeNodeDto<T>
        {
            var children = list.Where(o => o.ParentId == parentId).ToList();
            if (children == null || children.Count == 0)
            {
                return;
            }
            foreach (var item in children)
            {
                item.Children = list.Where(a => a.ParentId == item.Id).ToList();
                GetParentList(list, item.Id);
            }
        }
        #endregion

    }

    public class TreeNodeDto<T>
    {

        public TreeNodeDto()
        {
            Children = new List<T>();
        }

        /// <summary>
        /// id
        /// </summary>

        public string Id { get; set; }

        public string Key => Id;

        public string Name { get; set; }

        /// <summary>
        /// 父id
        /// </summary>
        public string ParentId { get; set; }

        public List<T> Children { get; set; }
    }
}
