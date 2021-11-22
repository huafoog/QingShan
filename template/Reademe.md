﻿﻿﻿﻿# 生成项目模板

[打造自己的.NET Core项目模板 - Catcher8 - 博客园 (cnblogs.com)](https://www.cnblogs.com/catcher1994/p/10061470.html)

[使用 .NET CORE 创建 项目模板，模板项目，Template - DeepThought - 博客园 (cnblogs.com)](https://www.cnblogs.com/deepthought/p/11373537.html)

## 打包

在`~\template\Nuget`路径下内创建一个 nuspec 文件：QingShan.Template.NetCore.nuspec，内容如下：

```
<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd">
  <metadata>
    <id>QingShan.Template.NetCore</id>
    <version>1.0.0</version>
    <description>
      QingShan WebApi Template
    </description>
    <authors>QingShan</authors>
    <packageTypes>
      <packageType name="Template" />
    </packageTypes>
  </metadata>
</package>
```

更新需要改变version

当前目录下打包

```
nuget pack QingShan.Template.NetCore.nuspec -OutputDirectory .
```

会生成这个`QingShan.Template.NetCore.1.0.0.nupkg`包，上传到[NuGet](https://www.nuget.org/)





