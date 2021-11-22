选中项目->右键菜单->编辑csproj文件，在 `PropertyGroup`节点下加入：

`<PackAsTool>true</PackAsTool>`

```
<Project Sdk="Microsoft.NET.Sdk">
	<PropertyGroup>
		<OutputType>Exe</OutputType>
		<PackAsTool>true</PackAsTool>
		<TargetFramework>net5.0</TargetFramework>
		<Version>1.0.2</Version>
	</PropertyGroup>
</Project>
```

然后打包就可以了

> 我们可以使用 `dotnet pack` 命令来进行打包，也可以使用VS提供的菜单来进行打包：选中项目->右键菜单->打包 （项目配置选为 Release），然后在 `bin\Release`目录下，就可以找到我们打包的nuget包。

打包的 NuGet 包，可以通过下面命令安装

```
dotnet tool install -g QingShan.CodeGenerator --add-source ./
```
然后发布到nuget.org
