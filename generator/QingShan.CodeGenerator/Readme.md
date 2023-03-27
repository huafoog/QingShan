# 代码生成工具

> 该代码生成工具主要用于生成`Services` 、`Dto`、`Conotract`和`Controller` 
> 
> 默认增删改查功能

## 下载代码生成工具

```
dotnet tool install -g QingShan.CodeGenerator
```

输出如下

```
可使用以下命令调用工具: QingShan.CodeGenerator
已成功安装工具“qingshan.codegenerator”(版本“1.0.0”)。
```

如果没有反应按下`CTRL`+`C`

## 运行代码生成

`QingShan.CodeGenerator "参数1" "参数2" "参数3"`

```
QingShan.CodeGenerator "Data Source=127.0.0.1;Port=3306;User ID=root;Password=root; Initial Catalog=数据库名称;Charset=utf8; SslMode=none;Min pool size=1" "QingShan.Web.Areas.Admin.Controllers" "QingShan.Services" "QingShan.Services" "QingShan.Services" "QingShan.DataLayer.Entities" "D:\Code\dotnet\QingShan\ADC" "QingShan"
```

### 参数说明

1. 数据库连接字符串 `Data Source=127.0.0.1;Port=3306;User ID=root;Password=root; Initial Catalog=数据库名称;Charset=utf8; SslMode=none;Min pool size=1`
2. Controller命名空间 
3. Dto
4. Service
5. IContract
6. 实体命名空间
7. 输出位置 `D:\Code\dotnet\QingShan\ADC`
8. 数据库名称

`dotnet tool`命令如下

```
使用情况: dotnet tool [选项] [命令]

选项:
  -h, --help   显示帮助信息。

命令:
  install <PACKAGE_ID>     安装在命令行上使用的工具。
  uninstall <PACKAGE_ID>   卸载工具。
  update <PACKAGE_ID>      将工具更新为最新稳定版本以供使用。
  list                     列出当前开发环境中的已安装工具。
```
