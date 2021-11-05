# 权限管理
- 简介
  - 使用到的实体模型为`ModuleEntity.cs`
  - 定义权限为`模块`、`菜单`、`方法`。其中`模块`和`菜单`无实用权限仅做展示用。`方法`包含了后台所有权限。
  - 我们约定一个控制器等于一个菜单，控制器中的方法=一个流程操作（例如用户管理、其中只包含用户管理相关功能不包含其他功能）
- 模块收集
  - 在控制器和方法中使用`[ModuleInfo]`特性
    - 在控制器中使用
      - 在控制器中使用时最少输入三个参数 如
        ```
        [ModuleInfo(URL = "/Admin/Url/Index",Module = Data.Enums.ModuleEnum.System,Sort = 0)]
        ```
        - Url表示当前菜单的跳转地址。
        - Module 使用枚举 表示当前父模块的信息
        - Sort 排序
    - 在方法中使用
      - 在方法上直接使用`[ModuleInfo]`特性，`UsePermission`会自动查找内容。
  - 在`QingShan.Core.Web.Authorization`服务中`GetModules()`会返回所有实现了`[ModuleInfo]`的数据
